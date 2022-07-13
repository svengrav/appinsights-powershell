using AppInsights.Telemetry;
using AppInsights.Test.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;

namespace AppInsights.Test
{
    [TestClass]
    public class TelemetryCustomDimensionsFormatterTests
    {
        [TestMethod]
        public void a_base_value_is_a_valid_custom_dimension_property()
        {
            // Arrange
            var serializationWriterMock = new SerializationWriterMock();
            var customDimensionsFormatter = new TelemetryCustomDimensionsFormatter();

            // Act
            customDimensionsFormatter.AddCustomProperty("type_string", "string_value");
            customDimensionsFormatter.AddCustomProperty("type_int", int.MaxValue);
            customDimensionsFormatter.AddCustomProperty("type_bool", false);
            customDimensionsFormatter.AddCustomProperty("type_double", double.MaxValue);
            customDimensionsFormatter.AddCustomProperty("type_timeSpan", TimeSpan.MaxValue);
            customDimensionsFormatter.AddCustomProperty("type_dateTimeOffset", DateTimeOffset.MaxValue);

            customDimensionsFormatter.Serialize(serializationWriterMock);

            // Assert
            Assert.IsTrue(serializationWriterMock.StringProperties["type_string"] == "string_value");
            Assert.IsTrue(serializationWriterMock.IntegerProperties["type_int"] == int.MaxValue);
            Assert.IsTrue(serializationWriterMock.BooleanProperties["type_bool"] == false);
            Assert.IsTrue(serializationWriterMock.DoubleProperties["type_double"] == double.MaxValue);
            Assert.IsTrue(serializationWriterMock.TimespanProperties["type_timeSpan"] == TimeSpan.MaxValue);
            Assert.IsTrue(serializationWriterMock.DateTimeOffsetProperties["type_dateTimeOffset"] == DateTimeOffset.MaxValue);
        }

        [TestMethod]
        public void a_complex_object_value_is_a_valid_custom_dimension_property()
        {
            // Arrange
            var serializationWriterMock = new SerializationWriterMock();
            var customDimensionsFormatter = new TelemetryCustomDimensionsFormatter();
            var complexObjectMock = new { Name = "test_name", Age = 20 };
            var complexObjectJsonMock = JsonConvert.SerializeObject(complexObjectMock);

            // Act
            customDimensionsFormatter.AddCustomProperty("type_complex", complexObjectMock);
            customDimensionsFormatter.Serialize(serializationWriterMock);

            // Assert
            Assert.IsTrue(serializationWriterMock.StringProperties["type_complex"] == complexObjectJsonMock);
        }

    }

}
