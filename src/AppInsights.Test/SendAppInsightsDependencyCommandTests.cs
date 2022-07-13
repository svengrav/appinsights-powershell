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
    [TestCategory("Send-AppInsightsDependency Tests")]
    public class SendAppInsightsDependencyCommandTests
    {
        [TestMethod]
        public void a_valid_dependency_trace_is_sent_successfully()
        {
            // Arrange
            var dependencyTelemetryMock = TelemetryRepository.CreateDependencyTelemetry();
            var telemetryProcessorMock = new TelemetryProcessorMock();

            var command = new SendAppInsightsDependencyCommand();

            command.TelemetryProcessor = telemetryProcessorMock;
            command.InstrumentationKey = Guid.NewGuid();
            command.Properties = TelemetryRepository.PropertiesHashtable;

            command.Name = dependencyTelemetryMock.Name;
            command.Duration = dependencyTelemetryMock.Duration;
            command.Timestamp = dependencyTelemetryMock.Timestamp;
            command.Data = dependencyTelemetryMock.Data;
            command.Target = dependencyTelemetryMock.Target;
            command.Metrics = TelemetryRepository.MetricsHashtable;

            // Act
            command.Exec();

            // Assert
            Assert.AreEqual(dependencyTelemetryMock.Name, telemetryProcessorMock.DependencyTelemetry.Name);
            Assert.AreEqual(dependencyTelemetryMock.Duration, telemetryProcessorMock.DependencyTelemetry.Duration);
            Assert.AreEqual(dependencyTelemetryMock.Data, telemetryProcessorMock.DependencyTelemetry.Data);
            Assert.AreEqual(dependencyTelemetryMock.Target, telemetryProcessorMock.DependencyTelemetry.Target);
            Assert.AreEqual(dependencyTelemetryMock.Timestamp, telemetryProcessorMock.DependencyTelemetry.Timestamp);
        }
    }
}
