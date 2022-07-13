using AppInsights.Commands;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace AppInsights.Test
{
    [TestClass]
    [TestCategory("Send-AppInsightsRequest Tests")]
    public class SendAppInsightsRequestCommandTests
    {
        [TestMethod]
        public void a_valid_request_trace_is_sent_successfully()
        {
            // Arrange
            var requestTelemetryMock = TelemetryRepository.CreateRequestTelemetry();
            var telemetryProcessorMock = new TelemetryProcessorMock();

            var command = new SendAppInsightsRequestCommand();

            command.TelemetryProcessor = telemetryProcessorMock;
            command.InstrumentationKey = Guid.NewGuid();
            command.Properties = TelemetryRepository.PropertiesHashtable;

            command.Timestamp = requestTelemetryMock.Timestamp;
            command.Metrics = TelemetryRepository.MetricsHashtable;
            command.Duration = requestTelemetryMock.Duration;
            command.Name = requestTelemetryMock.Name;
            command.ResponseCode = requestTelemetryMock.ResponseCode;
            command.Source = requestTelemetryMock.Source;
            command.Id = requestTelemetryMock.Id;
            command.Url = requestTelemetryMock.Url.ToString();

            // Act
            command.Exec();

            // Assert
            Assert.AreEqual(requestTelemetryMock.Name, telemetryProcessorMock.RequestTelemetry.Name);
            Assert.AreEqual(requestTelemetryMock.ResponseCode, telemetryProcessorMock.RequestTelemetry.ResponseCode);
            Assert.AreEqual(requestTelemetryMock.Timestamp, telemetryProcessorMock.RequestTelemetry.Timestamp);
            Assert.AreEqual(requestTelemetryMock.Duration, telemetryProcessorMock.RequestTelemetry.Duration);
            Assert.AreEqual(requestTelemetryMock.Id, telemetryProcessorMock.RequestTelemetry.Id);
            Assert.AreEqual(requestTelemetryMock.Url, telemetryProcessorMock.RequestTelemetry.Url);
            Assert.AreEqual(requestTelemetryMock.Source, telemetryProcessorMock.RequestTelemetry.Source);
        }
    }
}
