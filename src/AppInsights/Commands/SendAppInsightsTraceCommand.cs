using AppInsights.Telemetry;
using Microsoft.ApplicationInsights.DataContracts;
using System;
using System.Management.Automation;

namespace AppInsights.Commands
{
    [Cmdlet(VerbsCommunications.Send, "AppInsightsTrace")]
    public class SendAppInsightsTraceCommand : AppInsightsBaseCommand
    {
        #region Parameters

        [ValidateLength(minLength: 0, maxLength: 30000)]
        [Parameter(
            Position = 0,
            Mandatory = true,
            HelpMessage = "The message that is transmitted.",
            ValueFromPipeline = true
        )]
        public string Message { get; set; }

        [Parameter(
            HelpMessage = "The message severity (Verbose, Information, Warning, Error, Critical). Default is Information."
        )]
        public SeverityLevel Severity { get; set; } = SeverityLevel.Information;

        #endregion Parameters

        protected override void ProcessRecord()
        {
            try
            {
                WriteVerbose(BuildTraceVerboseMessage());
                TelemetryProcessor.TrackTrace(CreateTraceTelemetry());
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        private string BuildTraceVerboseMessage()
            => $"Track Trace (Message={Message}; Severity={Severity}; Properties={Properties.Count})";

        private TraceTelemetry CreateTraceTelemetry()
            => TraceTelemetryBuilder
                .Create(Message)
                .AddProperties(Properties)
                .AddSeverity(Severity)
                .AddCommandContext(CommandContext)
                .Build();
    }
}
