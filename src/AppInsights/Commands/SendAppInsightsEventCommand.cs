using AppInsights.Builders;
using Microsoft.ApplicationInsights.DataContracts;
using System;
using System.Collections;
using System.Management.Automation;

namespace AppInsights.Commands
{
    [Cmdlet(VerbsCommunications.Send, "AppInsightsEvent")]
    public class SendAppInsightsEventCommand : AppInsightsBaseCommand
    {
        #region Parameters
        [Parameter(
            Position = 0,
            Mandatory = true,
            HelpMessage = "The Event that is transmitted."
        )]
        [Alias("EventName")]
        public string Name { get; set; }

        [Parameter(
            HelpMessage = "The datetime when telemetry was recorded. Default is UTC.Now."
        )]
        [Alias("StartTime")]
        public DateTimeOffset Timestamp { get; set; } = DateTimeOffset.UtcNow;

        [Parameter(
            HelpMessage = "Optional dictionary with custom metrics."
        )]
        public Hashtable Metrics { get; set; } = new Hashtable();
        #endregion

        protected override void ProcessRecord()
        {
            try
            {
                WriteVerbose(BuildEventVerboseMessage());
                TelemetryProcessor.TrackEvent(CreateEventTelemetry());
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        private string BuildEventVerboseMessage()
            => $"Track Event (EventName={Name}; PropertyCount={Properties.Count}; MetricCount={Metrics.Count})";

        private EventTelemetry CreateEventTelemetry()
            => EventTelemetryBuilder
                .Create(Name)
                .AddPowerShellContext(HostContext, CommandContext)
                .AddProperties(Properties)
                .AddTimestamp(Timestamp)
                .AddMetrics(Metrics)
                .Build();
    }
}
