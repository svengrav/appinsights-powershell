using AppInsights.Commands;
using AppInsights.Telemetry;
using Microsoft.ApplicationInsights.DataContracts;
using System;
using System.Management.Automation;

namespace AppInsights
{
    [Cmdlet(VerbsCommunications.Send, "AppInsightsAvailability")]
    public class SendAppInsightsAvailabilityCommand : AppInsightsBaseCommand
    {
        #region Parameters
        [ValidateLength(minLength: 0, maxLength: 300)]
        [Parameter(
            Position = 0,
            Mandatory = true,
            HelpMessage = "The availability test name.",
            ValueFromPipeline = true
        )]
        public string Name { get; set; }

        [Parameter(
            Mandatory = true,
            HelpMessage = "The availability test run id.",
            ValueFromPipeline = true
        )]
        public string Id { get; set; }

        [Parameter(
            Mandatory = true,
            HelpMessage = "The availability test duration."
        )]
        public TimeSpan Duration { get; set; }

        [Parameter(
            Mandatory = true,
            HelpMessage = "The location where the availability test was run."
        )]
        public string RunLocation { get; set; }

        [Parameter(
            HelpMessage = "The datetime when telemetry was recorded. Default is UTC.Now."
        )]
        [Alias("StartTime")]
        public DateTimeOffset Timestamp { get; set; } = DateTimeOffset.UtcNow;

        [Parameter(
            HelpMessage = "Sets optional availability message."
        )]
        public string Message { get; set; }

        [Parameter(
            HelpMessage = "Defines whether the process was successfully processed. Default is true."
        )]
        public bool Success {get; set; } = true;

        #endregion Parameters

        protected override void ProcessRecord()
        {   
            try
            {
                WriteVerbose(BuildTraceVerboseMessage());
                TelemetryProcessor.TrackAvailability(CreateAvailabilityTelemetry());
            } 
            catch(Exception ex)
            {
                HandleException(ex);
            }
        }

        private string BuildTraceVerboseMessage()
            => $"Track Availability (Name={Name}; Id={Id}; Message={Message}; RunLocation={RunLocation}; Success={Success}; Properties={Properties.Count})";

        private AvailabilityTelemetry CreateAvailabilityTelemetry()
            => AvailabilityTelemetryBuilder
                .Create(Name, Timestamp, Duration, RunLocation)
                .AddId(Id)
                .AddProperties(Properties)
                .AddSuccess(Success)
                .AddCommandContext(CommandContext)
                .Build();
    }
}
