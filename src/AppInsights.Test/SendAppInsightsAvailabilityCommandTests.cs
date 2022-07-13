using AppInsights.Commands;
using AppInsights.Test.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace AppInsights.Test
{
    [TestClass]
    [TestCategory("Send-AppInsightsAvailability Tests")]
    public class SendAppInsightsAvailabilityCommandTests
    {
        [TestMethod]
        public void a_valid_availiability_trace_is_sent_successfully()
        {
            // Arrange
            var availabilityTelemetryMock = TelemetryRepository.CreateAvailabilityTelemetry();
            var telemetryProcessorMock = new TelemetryProcessorMock();

            var command = new SendAppInsightsAvailabilityCommand();

            command.TelemetryProcessor = telemetryProcessorMock;
            command.Properties = TelemetryRepository.PropertiesHashtable;
            command.InstrumentationKey = Guid.NewGuid();
            command.DisableContext = true;

            command.Name = availabilityTelemetryMock.Name;
            command.Duration = availabilityTelemetryMock.Duration;
            command.Message = availabilityTelemetryMock.Message;
            command.Metrics = TelemetryRepository.MetricsHashtable;
            command.Timestamp = availabilityTelemetryMock.Timestamp;

            // Act
            command.Exec();

            // Assert
            Assert.AreEqual(availabilityTelemetryMock.Name, telemetryProcessorMock.AvailabilityTelemetry.Name);
            Assert.AreEqual(availabilityTelemetryMock.Duration, telemetryProcessorMock.AvailabilityTelemetry.Duration);
            Assert.AreEqual(availabilityTelemetryMock.Timestamp, telemetryProcessorMock.AvailabilityTelemetry.Timestamp);
            Assert.AreEqual(availabilityTelemetryMock.Message, telemetryProcessorMock.AvailabilityTelemetry.Message);
        }

        [TestMethod]
        public void a_valid_availiability_trace_with_metrcis_is_sent_successfully()
        {
            // Arrange
            var availabilityTelemetryMock = TelemetryRepository.CreateAvailabilityTelemetry();
            var telemetryProcessorMock = new TelemetryProcessorMock();
            var serializationWriterMock = new SerializationWriterMock();
            var metricJsonString = JsonConvert.ConvertToJson(TelemetryRepository.MetricsHashtable);

            var command = new SendAppInsightsAvailabilityCommand();

            command.TelemetryProcessor = telemetryProcessorMock;
            command.InstrumentationKey = Guid.NewGuid();

            command.Name = availabilityTelemetryMock.Name;
            command.Metrics = TelemetryRepository.MetricsHashtable;

            // Act
            command.Exec();

            // Assert
            telemetryProcessorMock.AvailabilityTelemetry.Extension.Serialize(serializationWriterMock);

            Assert.AreEqual(availabilityTelemetryMock.Name, telemetryProcessorMock.AvailabilityTelemetry.Name);
            Assert.AreEqual(metricJsonString, serializationWriterMock.StringProperties["customMetrics"]);
        }
    }
}
