using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using AppInsights.Adapters;
using Microsoft.ApplicationInsights.Extensibility;
using Newtonsoft.Json;

namespace AppInsights.Context
{
    public class CommandContext : IExtension
    {
        private readonly IPowerShellAdapter _powerShellAdapter;

        private readonly ICollection<CommandCall> _callStack;

        private readonly CommandHost _commandHost;

        private int _contextLevel = 0;

        public CommandContext(IPowerShellAdapter powerShellAdapter, int contextLevel = 0)
        {
            _contextLevel = contextLevel;
            _powerShellAdapter = powerShellAdapter;
            _callStack = CreateCallStack();
            _commandHost = new CommandHost(GetHostName(), GetHostVersion(), GetHostCulture());
        }

        public CommandCall GetCommandCall(int level = 0)
            => _callStack.ElementAt(level);

        public IExtension DeepClone()
            => null;

        public void Serialize(ISerializationWriter serializationWriter)
        {
            serializationWriter.WriteProperty("hostContext", ConvertToJson(GetHost()));
            serializationWriter.WriteProperty("commandContext", ConvertToJson(GetCommandCall(_contextLevel)));
        }

        public CommandContext SetCommandLevel(int level)
        {
            _contextLevel = level;
            return this;
        }

        public CommandHost GetHost()
            => _commandHost;

        private string ConvertToJson(object objectToConvert)
            => JsonConvert.SerializeObject(objectToConvert);

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

        private string GetHostName()
            => _powerShellAdapter.GetHostName();

        private string GetHostVersion()
            => _powerShellAdapter.GetHostVersion();

        private string GetHostCulture()
            => _powerShellAdapter.GetHostCulture();

    }
}
