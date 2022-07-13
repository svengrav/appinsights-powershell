using AppInsights.Context;
using AppInsights.Utils;
using Microsoft.ApplicationInsights.DataContracts;
using System;
using System.Collections;

namespace AppInsights.Telemetry
{
    public class EventTelemetryBuilder
    {
        private readonly EventTelemetry _telemetry;
        private readonly CustomDimensions _customDimensions = new CustomDimensions();

        private EventTelemetryBuilder(string eventName)
        {
            _telemetry = new EventTelemetry(eventName);
            _telemetry.Extension = _customDimensions;
        }

        internal static EventTelemetryBuilder Create(string eventName)
            => new EventTelemetryBuilder(eventName);

        internal EventTelemetry Build()
             => _telemetry;

        internal EventTelemetryBuilder AddCommandContext(CommandContext commandContext)
        {
            _customDimensions.AddCommandContext(commandContext);
            return this;
        }

        internal EventTelemetryBuilder AddHostContext(HostContext hostContext)
        {
            _customDimensions.AddHostContext(hostContext);
            return this;
        }

        internal EventTelemetryBuilder AddProperties(Hashtable properties)
        {
            _customDimensions.AddProperties(properties);
            return this;
        }

        internal EventTelemetryBuilder AddTimestamp(DateTimeOffset timestamp)
        {
            _telemetry.Timestamp = timestamp;
            return this;
        }

        internal EventTelemetryBuilder AddMetrics (Hashtable metrics)
        {
            _customDimensions.AddMetrics(metrics);
            return this;
        }
    }
}
