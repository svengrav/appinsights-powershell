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
    [TestCategory("Send-AppInsightsTrace Tests")]
    public class SendAppInsightsTraceCommandTests
    {
        [TestMethod]
        public void a_valid_trace_telementry_is_send()
        {
            // Arrange
            var traceTelemetryMock = TelemetryRepository.CreateTraceTelemetry();
            var telemetryProcessorMock = new TelemetryProcessorMock();
            var commandContext = new CommandContext();

            var command = new SendAppInsightsTraceCommand();

            command.CommandContext = new CommandContext();
            command.TelemetryProcessor = telemetryProcessorMock;
            command.InstrumentationKey = Guid.NewGuid();
            command.Properties = TelemetryRepository.PropertiesHashtable;

            command.Message = traceTelemetryMock.Message;
            command.Severity = traceTelemetryMock.SeverityLevel.Value;

            // Act
            command.Exec();

            // Assert
            Assert.AreEqual(traceTelemetryMock.Message, telemetryProcessorMock.TraceTelemetry.Message);
            Assert.AreEqual(traceTelemetryMock.SeverityLevel, telemetryProcessorMock.TraceTelemetry.SeverityLevel);
        }

        [TestMethod]
        [ExpectedException(typeof(HashtableInvalidException))]
        public void a_valid_trace_telementry_is_invalid_send()
        {
            // Arrange
            var traceTelemetryMock = TelemetryRepository.CreateTraceTelemetry();

            var telemetryProcessorMock = new TelemetryProcessorMock();
            var commandContext = new CommandContext();

            var command = new SendAppInsightsTraceCommand();

            command.CommandContext = new CommandContext();
            command.TelemetryProcessor = telemetryProcessorMock;
            command.InstrumentationKey = Guid.NewGuid();
            command.Properties = new Hashtable() { { new {}, "Property Value 1"} };

            command.Message = traceTelemetryMock.Message;
            command.Severity = traceTelemetryMock.SeverityLevel.Value;

            // Act
            var commandResult = command.Exec();

            // Assert
            Assert.IsTrue(commandResult.Errors.First() is HashtableInvalidRecord);
        }
    }
}
