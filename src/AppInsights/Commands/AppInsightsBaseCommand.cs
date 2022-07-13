using AppInsights.Context;
using AppInsights.ErrorRecords;
using AppInsights.Exceptions;
using AppInsights.Extensions;
using AppInsights.Telemetry;
using AppInsights.Utils;
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

        internal protected CommandContext CommandContext { get; internal set; }

        internal protected HostContext HostContext { get; internal set; }

        internal protected ITelemetryProcessor TelemetryProcessor { get; internal set; }

        private InstrumentationKey _instrumentationKey;

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

        private void CreateInstrumentationKey() {
            _instrumentationKey = new InstrumentationKey(InstrumentationKey);
        }

        private void CreateTelemetryProcessor()
        {
            TelemetryProcessor = new TelemetryProcessor(_instrumentationKey, RoleName, RoleInstance);
        }

        private void CreateCommandContext()
        {
            CommandContext = this.GetCommandContext(ContextLevel);
        }

        private void CreateHostContext()
        {
            HostContext = this.GetHostContext();
        }

        private protected void HandleException(Exception ex)
        {
            if(ex is InstrumentationKeyInvalidException)
                ThrowTerminatingError(new InstrumentationKeyInvalidRecord(ex, this));

            if (ex is HashtableInvalidException)
                ThrowTerminatingError(new HashtableInvalidRecord(ex, this));
        }

        internal void Execute()
        {
            BeginProcessing();
            ProcessRecord();
            EndProcessing();
        }
    }
}
