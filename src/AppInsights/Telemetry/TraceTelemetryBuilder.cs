using AppInsights.Context;
using AppInsights.Extensions;
using Microsoft.ApplicationInsights.DataContracts;
using System.Collections;

namespace AppInsights.Telemetry
{
    public class TraceTelemetryBuilder
    {
        private readonly TraceTelemetry _traceTelemetry;

        private TraceTelemetryBuilder(string message)
        {
            _traceTelemetry = new TraceTelemetry(message);
        }

        internal static TraceTelemetryBuilder Create(string message)
            => new TraceTelemetryBuilder(message);

        internal TraceTelemetry Build()
             => _traceTelemetry;

        internal TraceTelemetryBuilder AddCommandContext(CommandContext commandContext)
        {
            _traceTelemetry.Extension = commandContext;
            return this;
        }

        internal TraceTelemetryBuilder AddSeverity(SeverityLevel severity)
        {
            _traceTelemetry.SeverityLevel = severity;
            return this;
        }

        internal TraceTelemetryBuilder AddProperties(Hashtable properties)
        {
            _traceTelemetry.Properties.MergeDictionary(properties.ToPropertyDictionary());
            return this;
        }
    }
}
