using AppInsights.Context;
using AppInsights.Utils;
using Microsoft.ApplicationInsights.DataContracts;
using System.Collections;

namespace AppInsights.Telemetry
{
    public class TraceTelemetryBuilder
    {
        private readonly TraceTelemetry _telemetry;
        private readonly CustomDimensions _customDimensions = new CustomDimensions();

        private TraceTelemetryBuilder(string message)
        {
            _telemetry = new TraceTelemetry(message);
            _telemetry.Extension = _customDimensions;
        }

        internal static TraceTelemetryBuilder Create(string message)
            => new TraceTelemetryBuilder(message);

        internal TraceTelemetry Build()
             => _telemetry;

        internal TraceTelemetryBuilder AddCommandContext(CommandContext commandContext)
        {
            _customDimensions.AddCommandContext(commandContext);
            return this;
        }

        internal TraceTelemetryBuilder AddSeverity(SeverityLevel severity)
        {
            _telemetry.SeverityLevel = severity;
            return this;
        }

        internal TraceTelemetryBuilder AddProperties(Hashtable properties)
        {
            _customDimensions.AddProperties(properties);
            return this;
        }
    }
}
