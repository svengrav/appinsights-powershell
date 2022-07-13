using AppInsights.Commands;
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
    [TestCategory("Send-AppInsightsEvent Tests")]
    public class SendAppInsightsEventCommandTests
    {
        [TestMethod]
        public void a_valid_event_is_sent_successfully()
        {
            // Arrange
            var eventTelemetryMock = TelemetryRepository.CreateEventTelemetry();
            var telemetryProcessorMock = new TelemetryProcessorMock();

            var command = new SendAppInsightsEventCommand();

            command.TelemetryProcessor = telemetryProcessorMock;
            command.InstrumentationKey = Guid.NewGuid();
            command.Properties = TelemetryRepository.PropertiesHashtable;

            command.Name = eventTelemetryMock.Name;
            command.Timestamp = eventTelemetryMock.Timestamp;
            command.Metrics = TelemetryRepository.MetricsHashtable;

            // Act
            command.Exec();

            // Assert
            Assert.AreEqual(eventTelemetryMock.Name, telemetryProcessorMock.EventTelemetry.Name);
            Assert.AreEqual(eventTelemetryMock.Timestamp, telemetryProcessorMock.EventTelemetry.Timestamp);
        }

    }
}
