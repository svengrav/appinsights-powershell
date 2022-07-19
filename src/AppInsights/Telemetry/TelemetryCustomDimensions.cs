using AppInsights.Context;
using AppInsights.Exceptions;
using Microsoft.ApplicationInsights.Extensibility;
using System.Collections;

namespace AppInsights.Telemetry
{
    internal class TelemetryCustomDimensions
    {
#pragma warning disable IDE1006
        internal const string HOST_CONTEXT_ID = "hostContext";
        internal const string COMMAND_CONTEXT_ID = "commandContext";
        internal const string CUSTOM_PROPERTIES_ID = "customProperties";
        internal const string CUSTOM_METRICS_ID = "customMetrics";
#pragma warning restore IDE1006

        private readonly TelemetryCustomDimensionsFormatter _formatter = new TelemetryCustomDimensionsFormatter();

        public IExtension GetFormatter()
            => _formatter;

        public TelemetryCustomDimensions AddMetrics(Hashtable metrics)
        {
            ThrowIfMetricsAreInvalid(metrics);

            if (HashtableIsNotNullOrEmpty(metrics))
                _formatter.AddCustomProperty(CUSTOM_METRICS_ID, metrics);

            return this;
        }

        public TelemetryCustomDimensions AddProperties(Hashtable properties)
        {
            ThrowIfPropertiesAreInvalid(properties);

            if (HashtableIsNotNullOrEmpty(properties))
                _formatter.AddCustomProperty(CUSTOM_PROPERTIES_ID, properties);

            return this;
        }

        public TelemetryCustomDimensions AddCommandContext(PowerShellCommandContext commandContext)
        {
            if (HasValue(commandContext))
                _formatter.AddCustomProperty(COMMAND_CONTEXT_ID, commandContext.GetCommandCall());

            return this;
        }

        public TelemetryCustomDimensions AddHostContext(PowerShellHostContext hostContext)
        {
            if (HasValue(hostContext))
                _formatter.AddCustomProperty(HOST_CONTEXT_ID, hostContext);

            return this;
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

        private static bool HashtableIsNotNullOrEmpty(Hashtable properties)
            => properties != null && properties.Count > 0;

        private static bool HasValue(object instance)
            => !(instance is null);
    }
}
