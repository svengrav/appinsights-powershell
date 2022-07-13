using AppInsights.Context;
using AppInsights.Exceptions;
using Microsoft.ApplicationInsights.Extensibility;
using System;
using System.Collections;

namespace AppInsights.Utils
{
    internal class CustomDimensions : IExtension
    {
        private readonly CustomDimensionsFormatter _formatter = new CustomDimensionsFormatter();

        private CommandContext _commandContext;
        private HostContext _hostContext;
        private Hashtable _metrics; 
        private Hashtable _properties;

        public CustomDimensions AddMetrics(Hashtable metrics)
        {
            ThrowIfMetricsAreInvalid(metrics);

            _metrics = metrics;
            return this;
        }

        public CustomDimensions AddProperties(Hashtable properties)
        {
            ThrowIfPropertiesAreInvalid(properties);

            _properties = properties;
            return this;
        }

        public CustomDimensions AddCommandContext(CommandContext commandContext)
        {
            _commandContext = commandContext;
            return this;
        }

        public CustomDimensions AddHostContext(HostContext hostContext)
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
            _formatter.AddCustomProperty("host_context", _hostContext);
            _formatter.AddCustomProperty("command_context", _commandContext);
            _formatter.AddCustomProperty("custom_properties", _properties);
            _formatter.AddCustomProperty("custom_metrics", _metrics);

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
            foreach(var key in hashtable.Keys)
                if (!(key is string))
                    throw new HashtableInvalidException("Key has to be from type string.");
        }

        private void ThrowIfMetricIsInvalid(Hashtable metrics)
        {
            foreach (var value in metrics.Values)
                if (!(value is double || value is int))
                    throw new HashtableInvalidException("Metric has to be from type double.");
        }
    }
}
