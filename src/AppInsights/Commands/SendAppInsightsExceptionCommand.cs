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
            Mandatory = true,
            ParameterSetName = "Exception",
            Position = 0,
            HelpMessage = "The exception that is transmitted."
        )]
        public Exception Exception { get; set; }

        [Parameter(
            Mandatory = true,
            ParameterSetName = "ErrorRecord",
            Position = 0,
            HelpMessage = "The error record that is transmitted."
        )]
        public ErrorRecord ErrorRecord { get; set; }

        [Parameter(
            HelpMessage = "Set optional exception message."
        )]
        public string Message { get; set; }

        [Parameter(
            HelpMessage = "Add optional hashtable with custom metrics."
        )]
        public Hashtable Metrics { get; set; } = new Hashtable();

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
                TryGetExceptionFromErrorRecord();
                WriteVerbose(BuildExceptionVerboseMessage());
                TelemetryProcessor.TrackException(CreateExceptionTelemetry());
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        private void TryGetExceptionFromErrorRecord()
        {
            if (ErrorRecord != null)
                Exception = ErrorRecord.Exception;
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
