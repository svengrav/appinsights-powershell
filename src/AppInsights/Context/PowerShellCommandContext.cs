using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace AppInsights.Context
{
    public class PowerShellCommandContext
    {
        private readonly IPowerShellAdapter _powerShellAdapter;

        private readonly ICollection<PowerShellCommandCall> _callStack;

        private int _contextLevel = 0;

        public PowerShellCommandContext(IPowerShellAdapter powerShellAdapter, int contextLevel = 0)
        {
            _contextLevel = contextLevel;
            _powerShellAdapter = powerShellAdapter;
            _callStack = CreateCallStack();
        }

        public PowerShellCommandCall GetCommandCall(int contextLevel)
             => _callStack.ElementAt(contextLevel);

        public PowerShellCommandCall GetCommandCall()
            => _callStack.ElementAt(_contextLevel);

        public PowerShellCommandContext SetCommandLevel(int level)
        {
            _contextLevel = level;
            return this;
        }

        private ICollection<PowerShellCommandCall> CreateCallStack()
        {
            var commandCallStack = new Collection<PowerShellCommandCall>();
            foreach (var powerShellCall in _powerShellAdapter.GetCallStack())
                commandCallStack.Add(CreateCommandCall(powerShellCall));

            return commandCallStack;
        }

        private static PowerShellCommandCall CreateCommandCall(PowerShellStackItem powerShellCall)
            => new PowerShellCommandCall(powerShellCall.Command, powerShellCall.ScriptLineNumber)
                .AddArguments(powerShellCall.Arguments);

    }
}
