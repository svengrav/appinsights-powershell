using AppInsights.ErrorRecords;
using AppInsights.Exceptions;
using AppInsights.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections;
using System.Linq;

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

            command.Name = availabilityTelemetryMock.Name;
            command.Duration = availabilityTelemetryMock.Duration;
            command.Message = availabilityTelemetryMock.Message;
            command.Metrics = TelemetryRepository.MetricsHashtable;
            command.Timestamp = availabilityTelemetryMock.Timestamp;

            // Act
            command.Exec();

            // Assert
            Assert.AreEqual(availabilityTelemetryMock.Name, telemetryProcessorMock.AvailabilityTelemetry.Name);
            Assert.AreEqual(availabilityTelemetryMock.Properties.Count, telemetryProcessorMock.AvailabilityTelemetry.Properties.Count);
            Assert.AreEqual(availabilityTelemetryMock.Metrics.Count, telemetryProcessorMock.AvailabilityTelemetry.Metrics.Count);
            Assert.AreEqual(availabilityTelemetryMock.Duration, telemetryProcessorMock.AvailabilityTelemetry.Duration);
            Assert.AreEqual(availabilityTelemetryMock.Timestamp, telemetryProcessorMock.AvailabilityTelemetry.Timestamp);
            Assert.AreEqual(availabilityTelemetryMock.Message, telemetryProcessorMock.AvailabilityTelemetry.Message);
        }
    }
}
