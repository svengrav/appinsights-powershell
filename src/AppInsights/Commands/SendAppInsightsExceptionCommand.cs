using AppInsights.Commands;
using AppInsights.Telemetry;
using Microsoft.ApplicationInsights.DataContracts;
using System;
using System.Collections;
using System.Management.Automation;

namespace AppInsights
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
            HelpMessage = "Defines whether the process was successfully processed. Default is true."
        )]
        public bool Success {get; set; } = true;

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
            => $"Track Exception (Exception={Exception.Message}; PropertyCount={Properties.Count}; MetricCount={Metrics.Count})";

        private ExceptionTelemetry CreateExceptionTelemetry()
            => ExceptionTelemetryBuilder.Create(Exception)
                .AddMessage(Message)
                .AddMetrics(Metrics)
                .AddProperties(Properties)
                .AddCommandContext(CommandContext)
                .Build();
    }
}
