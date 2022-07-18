using AppInsights.Context;
using AppInsights.ErrorRecords;
using AppInsights.Exceptions;
using AppInsights.Telemetry;
using System;
using System.Collections;
using System.Management.Automation;

namespace AppInsights.Commands
{
    public abstract partial class AppInsightsBaseCommand : PSCmdlet
    {
        [Parameter(
            Position = 1,
            HelpMessage = "The Application Insights Instrumentation Key."
        )]
        public Guid InstrumentationKey { get; set; }

        [Parameter(
            Position = 2,
            HelpMessage = "Hashtables with custom properties. Default is a empty hashtable."
        )]
        public Hashtable Properties { get; set; } = new Hashtable();

        [Parameter(
            HelpMessage = "The role name. Default is the machine name."
        )]
        public string RoleName { get; set; } = Environment.MachineName;

        [Parameter(
            HelpMessage = "The role instance. Default is the machine name."
        )]
        public string RoleInstance { get; set; } = Environment.MachineName;

        [Parameter(
            ParameterSetName = "CaptureCommand",
            HelpMessage = "Defines which level in the call stack is taken into account for the command context."
        )]
        public int CaptureLevel { get; set; } = 0;

        [Parameter(
            ParameterSetName = "CaptureCommand",
            HelpMessage = "Enables the capturing for the PowerShell command context."
        )]
        public SwitchParameter CaptureCommand
        {
            get { return _captureCommand; }
            set { _captureCommand = value; }
        }
        private bool _captureCommand;

        internal protected PowerShellCommandContext CommandContext { get; internal set; }

        internal protected PowerShellHostContext HostContext { get; internal set; }

        internal protected ITelemetryProcessor TelemetryProcessor { get; internal set; }

        private TelemetryInstrumentationKey _instrumentationKey;

        protected override void BeginProcessing()
        {
            base.BeginProcessing();

            CreateInstrumentationKey();

            if (TelemetryProcessorNotExists())
                CreateTelemetryProcessor();

            if (CommandContextNotExists())
                AddCommandContext();

            if (HostContextNotExists())
                AddHostContext();
        }

        private bool HostContextNotExists()
            => HostContext is null;

        private bool CommandContextNotExists()
            => CommandContext is null;

        private bool TelemetryProcessorNotExists()
            => TelemetryProcessor is null;

        private void CreateInstrumentationKey()
        {
            _instrumentationKey = new TelemetryInstrumentationKey(InstrumentationKey);
        }

        private void CreateTelemetryProcessor()
        {
            TelemetryProcessor = new TelemetryProcessor(_instrumentationKey, RoleName, RoleInstance);
        }

        private void AddCommandContext()
        {
            if (_captureCommand)
                CommandContext = this.GetCommandContext(CaptureLevel);
        }

        private void AddHostContext()
        {
            HostContext = this.GetHostContext();
        }

        private protected void HandleException(Exception ex)
        {
            if (ex is InvalidInstrumentationKeyException)
                ThrowTerminatingError(new InvalidInstrumentationKeyRecord(ex, this));

            if (ex is InvalidHashtableException)
                ThrowTerminatingError(new InvalidHashtableRecord(ex, this));
        }

        internal void Execute()
        {
            BeginProcessing();
            ProcessRecord();
            EndProcessing();
        }
    }
}
