using AppInsights.Context;
using AppInsights.Exceptions;
using Microsoft.ApplicationInsights.Extensibility;
using System;
using System.Collections;

namespace AppInsights.Telemetry
{
    internal class TelemetryCustomDimensions : IExtension
    {
#pragma warning disable IDE1006
        internal const string HOST_CONTEXT_ID = "host_context";
        internal const string COMMAND_CONTEXT_ID = "command_context";
        internal const string CUSTOM_PROPERTIES_ID = "custom_properties";
        internal const string CUSTOM_METRICS_ID = "custom_metrics";
#pragma warning restore IDE1006

        private readonly TelemetryCustomDimensionsFormatter _formatter = new TelemetryCustomDimensionsFormatter();

        private PowerShellCommandContext _commandContext;
        private PowerShellHostContext _hostContext;
        private Hashtable _metrics;
        private Hashtable _properties;

        public TelemetryCustomDimensions AddMetrics(Hashtable metrics)
        {
            ThrowIfMetricsAreInvalid(metrics);

            _metrics = metrics;
            return this;
        }

        public TelemetryCustomDimensions AddProperties(Hashtable properties)
        {
            ThrowIfPropertiesAreInvalid(properties);

            _properties = properties;
            return this;
        }

        public TelemetryCustomDimensions AddCommandContext(PowerShellCommandContext commandContext)
        {
            _commandContext = commandContext;
            return this;
        }

        public TelemetryCustomDimensions AddHostContext(PowerShellHostContext hostContext)
        {
            _hostContext = hostContext;
            return this;
        }

        public IExtension DeepClone()
        {
            throw new NotImplementedException();
        }

        public void Serialize(ISerializationWriter serializationWriter)
        {
            _formatter.AddCustomProperty(HOST_CONTEXT_ID, _hostContext);
            _formatter.AddCustomProperty(COMMAND_CONTEXT_ID, _commandContext);
            _formatter.AddCustomProperty(CUSTOM_PROPERTIES_ID, _properties);
            _formatter.AddCustomProperty(CUSTOM_METRICS_ID, _metrics);

            serializationWriter.WriteProperty(_formatter);
        }

        private void ThrowIfMetricsAreInvalid(Hashtable metrics)
        {
            ThrowIfKeyIsInvalid(metrics);
            ThrowIfMetricIsInvalid(metrics);
        }

        private void ThrowIfPropertiesAreInvalid(Hashtable properties)
        {
            ThrowIfKeyIsInvalid(properties);
        }

        private void ThrowIfKeyIsInvalid(Hashtable hashtable)
        {
            foreach (var key in hashtable.Keys)
                if (!(key is string))
                    throw new InvalidHashtableException("Key has to be from type string.");
        }

        private void ThrowIfMetricIsInvalid(Hashtable metrics)
        {
            foreach (var value in metrics.Values)
                if (!(value is double || value is int))
                    throw new InvalidHashtableException("Metric has to be from type double.");
        }
    }
}
