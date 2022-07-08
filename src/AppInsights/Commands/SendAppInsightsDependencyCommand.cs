using AppInsights.Telemetry;
using Microsoft.ApplicationInsights.DataContracts;
using System;
using System.Collections;
using System.Management.Automation;

namespace AppInsights.Commands
{
    [Cmdlet(VerbsCommunications.Send, "AppInsightsDependency")]
    public class SendAppInsightsDependencyCommand : AppInsightsBaseCommand
    {
        #region Parameters
        [ValidateNotNullOrEmpty]
        [Parameter(
            Position = 0,
            Mandatory = true,
            HelpMessage = "The name of the dependency type being tracked."
        )]
        public string Type { get; set; }

        [ValidateNotNullOrEmpty]
        [Parameter(
            Mandatory = true,
            HelpMessage = "The name of the command initiated with dependency call."
        )]
        public string Name { get; set; }

        [ValidateNotNullOrEmpty]
        [Parameter(
            Mandatory = true,
            HelpMessage = "The target of the command initiated with dependency call."
        )]
        public string Target { get; set; }

        [Parameter(
            Mandatory = true,
            HelpMessage = "The amount of time it took the application to handle the request."
        )]
        public TimeSpan Duration { get; set; }

        [Parameter(
            Mandatory = true,
            HelpMessage = "The dependency call result code."
        )]
        public string ResultCode { get; set; }

        [Parameter(
            HelpMessage = "The data associated with the dependency instance."
        )]
        public string Data { get; set; }

        [Parameter(
            HelpMessage = "Optional dictionary with custom request metrics."
        )]
        public Hashtable Metrics { get; set; } = new Hashtable();

        [Parameter(
            HelpMessage = "The datetime when telemetry was recorded. Default is UTC.Now."
        )]
        [Alias("StartTime")]
        public DateTimeOffset Timestamp { get; set; } = DateTimeOffset.UtcNow;

        [Parameter(
            HelpMessage = "Defines whether the process was successfully processed. Default is true."
        )]
        public bool Success { get; set; } = true;

        #endregion

        protected override void ProcessRecord()
        {
            try
            {
                WriteVerbose(BuildDependencyVerboseMessage());
                TelemetryProcessor.TrackDependency(CreateDependencyTelemetry());
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        private string BuildDependencyVerboseMessage()
            => $"Track Dependency (Name={Name}; Type={Type}; Target={Target}; Input={Data}; StartTime={Timestamp }; Duration={Duration}; ResultCode={ResultCode}; Success={Success})";

        private DependencyTelemetry CreateDependencyTelemetry()
            => DependencyTelemetryBuilder
                .Create(Type, Target, Name, Data)
                .AddStartTime(Timestamp)
                .AddSuccess(Success)
                .AddDuration(Duration)
                .AddResultCode(ResultCode)
                .AddProperties(Properties)
                .AddMetrics(Metrics)
                .AddCommandContext(CommandContext)
                .Build();
    }
}
