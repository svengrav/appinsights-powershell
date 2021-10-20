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
    [TestCategory("Send-AppInsightsEvent")]
    public class SendAppInsightsEventCommandTests
    {
        [TestMethod]
        public void a_valid_event_is_sent_successfully()
        {
            // Arrange
            var eventTelemetryMock = TelemetryRepository.CreateEventTelemetry();
            var telemetryProcessorMock = new TelemetryProcessorMock();
            var commandContext = new CommandContext();

            var command = new SendAppInsightsEventCommand()
            {
                TelemetryProcessor = telemetryProcessorMock,
                CommandContext = commandContext,
                Name = eventTelemetryMock.Name,
                Properties = TelemetryRepository.PropertiesHashtable,
                Metrics = TelemetryRepository.MetricsHashtable,
                InstrumentationKey = Guid.NewGuid()
            };

            // Act
            command.Exec();

            // Assert
            Assert.AreEqual(eventTelemetryMock.Name, telemetryProcessorMock.EventTelemetry.Name);
            Assert.AreEqual(eventTelemetryMock.Properties.Count, telemetryProcessorMock.EventTelemetry.Properties.Count);
            Assert.AreEqual(eventTelemetryMock.Metrics.Count, telemetryProcessorMock.EventTelemetry.Metrics.Count);

        }

        [TestMethod]
        [ExpectedException(typeof(HashtableInvalidException))]
        public void a_invalid_hashtable_leads_to_an_error_record()
        {
            // Arrange
            var eventTelemetryMock = TelemetryRepository.CreateEventTelemetry();

            var telemetryProcessorMock = new TelemetryProcessorMock();
            var commandContext = new CommandContext();
            var invalidProperties = new Hashtable() { { new {}, "Property Value 1"} };

            var command = new SendAppInsightsEventCommand()
            {
                TelemetryProcessor = telemetryProcessorMock,
                CommandContext = commandContext,
                Name = eventTelemetryMock.Name,
                Properties = invalidProperties,
                InstrumentationKey = Guid.NewGuid()
            };

            // Act
            var commandResult = command.Exec();

            // Assert
            Assert.AreEqual(eventTelemetryMock.Name, telemetryProcessorMock.EventTelemetry.Name);
            Assert.AreEqual(eventTelemetryMock.Properties.Count, telemetryProcessorMock.EventTelemetry.Properties.Count);

            Assert.IsTrue(commandResult.Errors.First() is HashtableInvalidRecord);

        }
    }
}
