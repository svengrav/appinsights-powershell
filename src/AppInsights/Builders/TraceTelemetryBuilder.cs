using AppInsights.Context;
using AppInsights.Telemetry;
using Microsoft.ApplicationInsights.DataContracts;
using System.Collections;

namespace AppInsights.Builders
{
    public class TraceTelemetryBuilder
    {
        private readonly TraceTelemetry _telemetry;
        private readonly TelemetryCustomDimensions _customDimensions = new TelemetryCustomDimensions();

        private TraceTelemetryBuilder(string message)
        {
            _telemetry = new TraceTelemetry(message);
            _telemetry.Extension = _customDimensions.GetFormatter();
        }

        internal static TraceTelemetryBuilder Create(string message)
            => new TraceTelemetryBuilder(message);

        internal TraceTelemetry Build()
             => _telemetry;

        internal TraceTelemetryBuilder AddPowerShellContext(PowerShellHostContext hostContext, PowerShellCommandContext commandContext)
        {
            _customDimensions.AddHostContext(hostContext);
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
