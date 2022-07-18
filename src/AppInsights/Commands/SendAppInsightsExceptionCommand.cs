using AppInsights.Builders;
using Microsoft.ApplicationInsights.DataContracts;
using System;
using System.Collections;
using System.Management.Automation;

namespace AppInsights.Commands
{
    [Cmdlet(VerbsCommunications.Send, "AppInsightsException")]
    public class SendAppInsightsExceptionCommand : AppInsightsBaseCommand
    {
        #region Parameters
        [Parameter(
            Position = 0,
            Mandatory = true,
            HelpMessage = "The exception that is transmitted."
        )]
        public Exception Exception { get; set; }

        [Parameter(
            HelpMessage = "Optional dictionary with custom metrics."
        )]
        public Hashtable Metrics { get; set; } = new Hashtable();

        [Parameter(
            HelpMessage = "Set optional exception message."
        )]
        public string Message { get; set; } = "";

        [Parameter(
            HelpMessage = "The datetime when telemetry was recorded. Default is UTC.Now."
        )]
        [Alias("StartTime")]
        public DateTimeOffset Timestamp { get; set; } = DateTimeOffset.UtcNow;

        [Parameter(
            HelpMessage = "The message severity (Verbose, Information, Warning, Error, Critical). Default is Information."
        )]
        public SeverityLevel Severity { get; set; } = SeverityLevel.Information;

        [Parameter(
            HelpMessage = "The exception problem ID."
        )]
        public string ProblemId { get; set; }

        #endregion

        protected override void ProcessRecord()
        {
            try
            {
                WriteVerbose(BuildExceptionVerboseMessage());
                TelemetryProcessor.TrackException(CreateExceptionTelemetry());
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        private string BuildExceptionVerboseMessage()
            => $"Track Exception (Exception={Exception?.Message}; PropertyCount={Properties.Count}; MetricCount={Metrics.Count})";

        private ExceptionTelemetry CreateExceptionTelemetry()
            => ExceptionTelemetryBuilder
                .Create(Exception)
                .AddPowerShellContext(HostContext, CommandContext)
                .AddMessage(Message)
                .AddMetrics(Metrics)
                .AddProperties(Properties)
                .AddProblemId(ProblemId)
                .AddTimestamp(Timestamp)
                .AddSeverity(Severity)
                .Build();
    }
}
