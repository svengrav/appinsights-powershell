using AppInsights.Extensions;
using Microsoft.ApplicationInsights.DataContracts;
using System;
using System.Collections;

namespace AppInsights.Telemetry
{
    public class EventTelemetryBuilder
    {
        private readonly EventTelemetry _telemetry;

        private EventTelemetryBuilder(string eventName)
        {
            _telemetry = new EventTelemetry(eventName);
        }

        internal static EventTelemetryBuilder Create(string eventName)
            => new EventTelemetryBuilder(eventName);

        internal EventTelemetry Build()
             => _telemetry;

        internal EventTelemetryBuilder AddCommandContext(CommandContext commandContext)
        {
            _telemetry.Extension = commandContext;
            return this;
        }

        internal EventTelemetryBuilder AddProperties(Hashtable properties)
        {
            _telemetry.Properties.MergeDictionary(properties.ToPropertyDictionary());
            return this;
        }

        internal EventTelemetryBuilder AddTimestamp(DateTimeOffset timestamp)
        {
            _telemetry.Timestamp = timestamp;
            return this;
        }

        internal EventTelemetryBuilder AddMetrics (Hashtable metrics)
        {
            _telemetry.Metrics.MergeDictionary(metrics.ToMetricDictionary());
            return this;
        }
    }
}
