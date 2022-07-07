using AppInsights.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace AppInsights.Test
{
    [TestClass]
    [TestCategory("Send-AppInsightsException Tests")]
    public class SendAppInsightsExceptionCommandTests
    {
        [TestMethod]
        public void a_valid_exception_trace_is_sent_successfully()
        {
            // Arrange
            var exceptionTelemetryMock = TelemetryRepository.CreateExceptionTelemetry();
            var telemetryProcessorMock = new TelemetryProcessorMock();

            var command = new SendAppInsightsExceptionCommand();

            command.TelemetryProcessor = telemetryProcessorMock;
            command.InstrumentationKey = Guid.NewGuid();
            command.Properties = TelemetryRepository.PropertiesHashtable;

            command.Exception = exceptionTelemetryMock.Exception;
            command.Timestamp = exceptionTelemetryMock.Timestamp;
            command.Metrics = TelemetryRepository.MetricsHashtable;
            command.ProblemId = exceptionTelemetryMock.ProblemId;
            command.Severity = exceptionTelemetryMock.SeverityLevel.Value;

            // Act
            command.Exec();

            // Assert
            Assert.AreEqual(exceptionTelemetryMock.Timestamp, telemetryProcessorMock.ExceptionTelemetry.Timestamp);
            Assert.AreEqual(exceptionTelemetryMock.ProblemId, telemetryProcessorMock.ExceptionTelemetry.ProblemId);
            Assert.AreEqual(exceptionTelemetryMock.SeverityLevel, telemetryProcessorMock.ExceptionTelemetry.SeverityLevel);
            Assert.AreEqual(exceptionTelemetryMock.Timestamp, telemetryProcessorMock.ExceptionTelemetry.Timestamp);
            Assert.AreEqual(exceptionTelemetryMock.Properties.Count, telemetryProcessorMock.ExceptionTelemetry.Properties.Count);
            Assert.AreEqual(exceptionTelemetryMock.Metrics.Count, telemetryProcessorMock.ExceptionTelemetry.Metrics.Count);
        }
    }
}
