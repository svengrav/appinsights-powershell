using AppInsights.Context;
using AppInsights.Telemetry;
using Microsoft.ApplicationInsights.DataContracts;
using System;
using System.Collections;

namespace AppInsights.Builders
{
    public class EventTelemetryBuilder
    {
        private readonly EventTelemetry _telemetry;
        private readonly TelemetryCustomDimensions _customDimensions = new TelemetryCustomDimensions();

        private EventTelemetryBuilder(string eventName)
        {
            _telemetry = new EventTelemetry(eventName);
            _telemetry.Extension = _customDimensions.GetFormatter();
        }

        internal static EventTelemetryBuilder Create(string eventName)
            => new EventTelemetryBuilder(eventName);

        internal EventTelemetry Build()
             => _telemetry;

        internal EventTelemetryBuilder AddPowerShellContext(PowerShellHostContext hostContext, PowerShellCommandContext commandContext)
        {
            _customDimensions.AddHostContext(hostContext);
            _customDimensions.AddCommandContext(commandContext);
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

        internal EventTelemetryBuilder AddMetrics(Hashtable metrics)
        {
            _customDimensions.AddMetrics(metrics);
            return this;
        }
    }
}
