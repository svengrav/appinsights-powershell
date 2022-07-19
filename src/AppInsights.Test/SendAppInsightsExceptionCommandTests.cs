using AppInsights.Commands;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace AppInsights.Test
{
    [TestClass]
    [TestCategory("Send-AppInsightsException Tests")]
    public class SendAppInsightsExceptionCommandTests
    {
        [TestMethod]
        public void trace_is_sent_successfully()
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
            command.Message = "Custom message";

            // Act
            command.Exec();

            // Assert
            Assert.AreEqual(exceptionTelemetryMock.Timestamp, telemetryProcessorMock.ExceptionTelemetry.Timestamp);
            Assert.AreEqual(exceptionTelemetryMock.ProblemId, telemetryProcessorMock.ExceptionTelemetry.ProblemId);
            Assert.AreEqual(exceptionTelemetryMock.SeverityLevel, telemetryProcessorMock.ExceptionTelemetry.SeverityLevel);
            Assert.AreEqual(exceptionTelemetryMock.Exception, telemetryProcessorMock.ExceptionTelemetry.Exception);
            Assert.IsFalse(string.IsNullOrEmpty(telemetryProcessorMock.ExceptionTelemetry.Message));
        }

        [TestMethod]
        public void trace_without_custom_message_will_use_exception_message()
        {
            // Arrange
            var exceptionTelemetryMock = TelemetryRepository.CreateExceptionTelemetry();
            var telemetryProcessorMock = new TelemetryProcessorMock();

            var command = new SendAppInsightsExceptionCommand();

            command.TelemetryProcessor = telemetryProcessorMock;
            command.InstrumentationKey = Guid.NewGuid();

            command.Exception = exceptionTelemetryMock.Exception;

            // Act
            command.Exec();

            // Assert
            Assert.IsFalse(string.IsNullOrEmpty(telemetryProcessorMock.ExceptionTelemetry.Message));
            Assert.AreEqual(exceptionTelemetryMock.Exception.Message, telemetryProcessorMock.ExceptionTelemetry.Message);
        }

        [TestMethod]
        public void trace_which_is_null_is_successfully_sent()
        {
            // Arrange
            var telemetryProcessorMock = new TelemetryProcessorMock();

            var command = new SendAppInsightsExceptionCommand();

            command.TelemetryProcessor = telemetryProcessorMock;
            command.InstrumentationKey = Guid.NewGuid();

            command.Exception = null;

            // Act
            command.Exec();

            // Assert
            Assert.AreEqual("n/a", telemetryProcessorMock.ExceptionTelemetry.Exception.Message);
        }
    }
}
