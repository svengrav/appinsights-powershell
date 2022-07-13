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
            HelpMessage = "Defines which level in the call stack is taken into account for the command context."
        )]
        public int ContextLevel { get; set; } = 0;

        [Parameter(
            HelpMessage = "Disables the capturing for the PowerShell command context. For instance, if sensitive data would be captured."
        )]
        public SwitchParameter DisableContext
        {
            get { return _disableContext; }
            set { _disableContext = value; }
        }
        private bool _disableContext;

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
                CreateCommandContext();

            if (HostContextNotExists())
                CreateHostContext();
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

        private void CreateCommandContext()
        {
            if (_disableContext)
                return;

            CommandContext = this.GetCommandContext(ContextLevel);
        }

        private void CreateHostContext()
        {
            if (_disableContext)
                return;

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
