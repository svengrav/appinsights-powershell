using AppInsights.Exceptions;
using AppInsights.Telemetry;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace AppInsights.Test
{
    [TestClass]
    [TestCategory("InstrumentationKey")]
    public class TelemetryInstrumentationKeyTests
    {
        [TestInitialize]
        public void InitTest()
        {
            // Resets the environment variable.
            Environment.SetEnvironmentVariable("AI_INSTRUMENTATION_KEY", "");
        }

        [TestMethod]
        public void a_guid_is_a_valid_instrumentation_key()
        {
            // Arrange
            var instrumentationKeyMock = Guid.NewGuid();

            // Act
            var instrumentationKey = new TelemetryInstrumentationKey(instrumentationKeyMock);

            // Assert
            Assert.AreEqual(instrumentationKeyMock, instrumentationKey.GetKey());
        }

        [TestMethod]
        public void a_environment_key_is_a_valid_instrumentation_key()
        {
            // Arrange
            var instrumentationKeyMock = Guid.NewGuid();
            var emptyGuid = new Guid();
            Environment.SetEnvironmentVariable("AI_INSTRUMENTATION_KEY", instrumentationKeyMock.ToString());

            // Act
            var instrumentationKey = new TelemetryInstrumentationKey(emptyGuid);

            // Assert
            Assert.AreEqual(instrumentationKeyMock, instrumentationKey.GetKey());
        }

        [TestMethod]
        public void a_missing_instrumentation_key_leads_to_exception()
        {
            // Arrange
            var exceptionMessage = "Instrumentation Key is missing or not correct. Set the instrumentation key as an environment variable or add it as a parameter when calling the command.";
            var emptyGuid = new Guid();

            // Act / Assert
            var invalidException = Assert.ThrowsException<InvalidInstrumentationKeyException>(() => new TelemetryInstrumentationKey(emptyGuid));
            Assert.AreEqual(exceptionMessage, invalidException.Message);
        }
    }
}