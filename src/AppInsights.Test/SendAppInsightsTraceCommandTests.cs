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
    [TestCategory("Send-AppInsightsTrace")]
    public class SendAppInsightsTraceCommandTests
    {
        [TestMethod]
        public void a_valid_trace_telementry_is_send()
        {
            // Arrange
            var traceTelemetryMock = TelemetryRepository.CreateTraceTelemetry();
            var telemetryProcessorMock = new TelemetryProcessorMock();
            var commandContext = new CommandContext();

            var command = new SendAppInsightsTraceCommand()
            {
                TelemetryProcessor = telemetryProcessorMock,
                CommandContext = commandContext,
                Message = traceTelemetryMock.Message,
                Properties = TelemetryRepository.PropertiesHashtable,
                Severity = TelemetryRepository.SeverityLevel,
                InstrumentationKey = Guid.NewGuid()
            };

            // Act
            command.Exec();

            // Assert
            Assert.AreEqual(traceTelemetryMock.Message, telemetryProcessorMock.TraceTelemetry.Message);
            Assert.AreEqual(traceTelemetryMock.Properties.Count, telemetryProcessorMock.TraceTelemetry.Properties.Count);
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
            var invalidProperties = new Hashtable() { { new {}, "Property Value 1"} };

            var command = new SendAppInsightsTraceCommand()
            {
                TelemetryProcessor = telemetryProcessorMock,
                CommandContext = commandContext,
                Message = traceTelemetryMock.Message,
                Properties = invalidProperties,
                Severity = TelemetryRepository.SeverityLevel,
                InstrumentationKey = Guid.NewGuid()
            };

            // Act
            var commandResult = command.Exec();

            // Assert
            Assert.AreEqual(traceTelemetryMock.Message, telemetryProcessorMock.TraceTelemetry.Message);
            Assert.AreEqual(traceTelemetryMock.Properties.Count, telemetryProcessorMock.TraceTelemetry.Properties.Count);

            Assert.IsTrue(commandResult.Errors.First() is HashtableInvalidRecord);

        }
    }
}
