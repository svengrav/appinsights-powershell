using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using AppInsights.Adapters;

namespace AppInsights.Context
{
    public class CommandContext
    {
        private readonly IPowerShellAdapter _powerShellAdapter;

        private readonly ICollection<CommandCall> _callStack;

        private int _contextLevel = 0;

        public CommandContext(IPowerShellAdapter powerShellAdapter, int contextLevel = 0)
        {
            _contextLevel = contextLevel;
            _powerShellAdapter = powerShellAdapter;
            _callStack = CreateCallStack();
        }

        public CommandCall GetCommandCall(int contextLevel)
             => _callStack.ElementAt(contextLevel);

        public CommandCall GetCommandCall()
            => _callStack.ElementAt(_contextLevel);

        public CommandContext SetCommandLevel(int level)
        {
            _contextLevel = level;
            return this;
        }

        private ICollection<CommandCall> CreateCallStack()
        {
            var commandCallStack = new Collection<CommandCall>();
            foreach (var powerShellCall in _powerShellAdapter.GetCallStack())
                commandCallStack.Add(CreateCommandCall(powerShellCall));

            return commandCallStack;
        }

        private static CommandCall CreateCommandCall(PowerShellCommandCall powerShellCall)
            => new CommandCall(powerShellCall.Command, powerShellCall.ScriptLineNumber)
                .AddArguments(powerShellCall.Arguments);

    }
}
