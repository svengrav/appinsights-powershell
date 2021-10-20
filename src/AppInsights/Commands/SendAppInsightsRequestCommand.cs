using AppInsights.Commands;
using AppInsights.Telemetry;
using Microsoft.ApplicationInsights.DataContracts;
using System;
using System.Collections;
using System.Management.Automation;

namespace AppInsights
{
    [Cmdlet(VerbsCommunications.Send, "AppInsightsRequest")]
    public class SendAppInsightsRequestCommand : AppInsightsBaseCommand
    {
        #region Parameters
        [Parameter(
            Position = 0,
            Mandatory = true,
            HelpMessage = "The name of the requested page."
        )]
        public string Name { get; set; }

        [Parameter(
            HelpMessage = "The Request ID. Default is emtpy."
        )]
        public string Id { get; set; } = "";

        [Parameter(
            Mandatory = true,
            HelpMessage = "The the amount of time it took the application to handle the request."
        )]
        public TimeSpan Duration { get; set; }

        [Parameter(
            HelpMessage = "The datetime when telemetry was recorded. Default is UTC.Now."
        )]
        [Alias("StartTime")]
        public DateTimeOffset Timestamp { get; set; } = DateTimeOffset.UtcNow;

        [Parameter(
            Mandatory = true,
            HelpMessage = "The response code returned by the application after handling a request."
        )]
        public string ResponseCode { get; set; }

        [Parameter(
            HelpMessage = "Defines whether the request was successfully processed. Default is true."
        )]
        public bool Success { get; set; } = true;

        [Parameter(
            HelpMessage = "Optional dictionary with custom request metrics."
        )]
        public Hashtable Metrics { get; set; } = new Hashtable();
        #endregion

        protected override void ProcessRecord()
        {
            try
            {
                WriteVerbose(BuildRequestVerboseMessage());
                TelemetryProcessor.TrackRequest(CreateRequestTelemetry());
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        private string BuildRequestVerboseMessage()
            => $"Track Request (Name={Name}; StartTime={Timestamp}; Duration={Duration}; ResponseCode={ResponseCode}; Success={Success};)";

        private RequestTelemetry CreateRequestTelemetry()
            => RequestTelemetryBuilder
                .Create(Name, Timestamp, Duration, ResponseCode, Success)
                .AddDuration(Duration)
                .AddId(Id)
                .AddProperties(Properties)
                .AddMetrics(Metrics)
                .AddCommandContext(CommandContext)
                .Build();
    }
}


