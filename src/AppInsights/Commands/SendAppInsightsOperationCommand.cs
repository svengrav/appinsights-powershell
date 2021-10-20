using AppInsights.Utils;
using Microsoft.ApplicationInsights.DataContracts;
using System;
using System.Management.Automation;

namespace AppInsights.Commands
{
    [Cmdlet(VerbsCommunications.Send, "AppInsightsOperation")]
    public class SendAppInsightsOperationCommand : AppInsightsBaseCommand
    {
        #region Parameters
        [Parameter(
            Position = 0,
            Mandatory = true,
            HelpMessage = "Name of the operation."
        )]
        public string Name { get; set; }

        [Parameter(
            Position = 0,
            Mandatory = false,
            HelpMessage = "Name of the initiator or the calling command. If nothing is specified, this is determined using the StackTrace."
        )]
        public string InvokedBy { get; set; }

        [Parameter(
            Position = 0,
            Mandatory = false,
            HelpMessage = "Name of the receiver or command. If nothing is specified, this is determined using the StackTrace."
        )]
        public string ReceivedBy { get; set; }

        [Parameter(
            Mandatory = false,
            HelpMessage = "Operation duration. If not specified, the duration is calculated from the start time."
        )]
        public TimeSpan Duration { get; set; }

        [Parameter(
            Mandatory = true,
            HelpMessage = "The start time of the operation call."
        )]
        public DateTimeOffset StartTime { get; set; }

        [ValidateNotNullOrEmpty]
        [Parameter(
            Mandatory = false,
            HelpMessage = "Result of a request execution. Default is undefined"
        )]
        public string ResultCode { get; set; } = "Undefined.";

        [Parameter(
            Mandatory = false,
            HelpMessage = "Defines whether the process was successfully processed. Default is true."
        )]
        public bool Success { get; set; } = true;
        #endregion

        protected override void ProcessRecord()
        {
            WriteWarning("This is an experimental feature. Please do not use it in production.");
            if (Duration == TimeSpan.Zero)
                Duration = DateTime.Now - StartTime;

            string data = "";
            var result = InvokeCommand.InvokeScript("Get-PSCallStack");

            if (string.IsNullOrEmpty(InvokedBy))
            {
                if (result.Count < 3)
                    throw new Exception("CalledBy Parameter is missing and Stacktrace is not available.");

                var command = result[2].Properties["Command"].Value.ToString();
                if (command == "<ScriptBlock>")
                    command = "Console";

                InvokedBy = command;
            }

            if (string.IsNullOrEmpty(ReceivedBy))
            {
                if (result.Count < 2)
                    throw new Exception("CurrentCommand Parameter is missing and Stacktrace is not available.");

                var command = result[1].Properties["Command"].Value.ToString();
                if (command == "<ScriptBlock>")
                    command = "Console";

                ReceivedBy = command;
                data = result[1].Properties["Arguments"].Value.ToString();
            }

            WriteVerbose($"Track Operation (Name={Name}; Command={ReceivedBy}; InvokedBy={InvokedBy}; StartTime={StartTime}; Duration={Duration}; ResponseCode={ResultCode}; Success={Success};)");

            var rootId = Guid.NewGuid().ToString().Substring(24);
            var depId = Guid.NewGuid().ToString().Substring(24);
            var reqId = Guid.NewGuid().ToString().Substring(24);

            var client = TelemetryHelper.CreateTelemetryClient(InstrumentationKey, InvokedBy);
            var dependency = new DependencyTelemetry("PowerShell", ReceivedBy, ReceivedBy, data, StartTime, Duration, ResultCode, Success);

            dependency.Id = depId;
            dependency.Context.Operation.ParentId = rootId;
            dependency.Context.Operation.Id = rootId;
            dependency.Context.Operation.Name = rootId;

            client.TrackDependency(dependency);
            client.Flush();

            client = TelemetryHelper.CreateTelemetryClient(InstrumentationKey, ReceivedBy);
            var request = new RequestTelemetry(Name, StartTime, Duration, ResultCode, true);

            request.Id = reqId;
            request.Context.Operation.ParentId = depId;
            request.Context.Operation.Id = rootId;
            request.Context.Operation.Name = rootId;

            client.TrackRequest(request);
            client.Flush();
        }
    }
}
