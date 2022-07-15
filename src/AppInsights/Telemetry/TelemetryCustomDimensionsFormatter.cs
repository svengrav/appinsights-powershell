using Microsoft.ApplicationInsights.Extensibility;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;

namespace AppInsights.Telemetry
{
    internal class TelemetryCustomDimensionsFormatter : IExtension
    {
        private readonly IDictionary<string, object> _customDimensions = new Dictionary<string, object>();

        public IExtension DeepClone() => null;

        public TelemetryCustomDimensionsFormatter AddCustomProperty(string key, object value)
        {
            _customDimensions.Add(key, value);
            return this;
        }

        public void Serialize(ISerializationWriter serializationWriter)
        {
            foreach (var key in _customDimensions.Keys)
            {
                if (TryWriteStringProperty(serializationWriter, key))
                    continue;

                if (TryWriteBoolProperty(serializationWriter, key))
                    continue;

                if (TryWriteDateTimeOffsetProperty(serializationWriter, key))
                    continue;

                if (TryWriteTimeSpanProperty(serializationWriter, key))
                    continue;

                if (TryWriteIntProperty(serializationWriter, key))
                    continue;

                if (TryWriteDoubleProperty(serializationWriter, key))
                    continue;

                WriteComplexProperty(serializationWriter, key);
            }
        }

        private bool TryWriteStringProperty(ISerializationWriter serializationWriter, string key)
        {
            if (_customDimensions[key] is string)
            {
                serializationWriter.WriteProperty(key, (string)_customDimensions[key]);
                return true;
            }

            return false;
        }

        private bool TryWriteIntProperty(ISerializationWriter serializationWriter, string key)
        {
            if (_customDimensions[key] is int)
            {
                serializationWriter.WriteProperty(key, (int)_customDimensions[key]);
                return true;
            }

            return false;
        }

        private bool TryWriteBoolProperty(ISerializationWriter serializationWriter, string key)
        {
            if (_customDimensions[key] is bool)
            {
                serializationWriter.WriteProperty(key, (bool)_customDimensions[key]);
                return true;
            }

            return false;
        }

        private bool TryWriteDoubleProperty(ISerializationWriter serializationWriter, string key)
        {
            if (_customDimensions[key] is double)
            {
                serializationWriter.WriteProperty(key, (double)_customDimensions[key]);
                return true;
            }

            return false;
        }

        private bool TryWriteTimeSpanProperty(ISerializationWriter serializationWriter, string key)
        {
            if (_customDimensions[key] is TimeSpan)
            {
                serializationWriter.WriteProperty(key, (TimeSpan)_customDimensions[key]);
                return true;
            }

            return false;
        }

        private bool TryWriteDateTimeOffsetProperty(ISerializationWriter serializationWriter, string key)
        {
            if (_customDimensions[key] is DateTimeOffset)
            {
                serializationWriter.WriteProperty(key, (DateTimeOffset)_customDimensions[key]);
                return true;
            }

            return false;
        }

        private void WriteComplexProperty(ISerializationWriter serializationWriter, string key)
        {
            serializationWriter.WriteProperty(key, ConvertToJson(_customDimensions[key]));
        }

        private string ConvertToJson(object objectToConvert)
            => JsonConvert.SerializeObject(objectToConvert, new JsonSerializerSettings()
            {
                NullValueHandling = NullValueHandling.Ignore,
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                MaxDepth = 1
            });
    }
}
