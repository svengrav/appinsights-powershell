using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Management.Automation;
using AppInsights.Adapters;
using Microsoft.ApplicationInsights.Extensibility;

namespace AppInsights.Extensions
{
    public class CommandContext : IExtension
    {        
        private readonly IPowerShellAdapter _powerShellAdapter;

        private readonly ICollection<CommandCall> _callStack;

        private readonly CommandHost _commandHost;

        private int _commandLevel = 0;

        public CommandContext(IPowerShellAdapter powerShellAdapter) {
            _powerShellAdapter = powerShellAdapter;
            _callStack = CreateCallStack();
            _commandHost = new CommandHost(GetHostName(), GetHostVersion(), GetHostCulture());
        }

        public CommandCall GetCommandCall(int level = 0)
            =>  _callStack.ElementAt(level);

        public IExtension DeepClone()
            => null;

        public void Serialize(ISerializationWriter serializationWriter)
        {
            serializationWriter.WriteProperty(_commandHost);
            serializationWriter.WriteProperty(GetCommandCall(_commandLevel));
        }

        public CommandContext SetCommandLevel(int level)
        {
            _commandLevel = level;
            return this;
        }

        public CommandHost GetHost()
            => _commandHost;
             
        private ICollection<CommandCall> CreateCallStack()
        {
            var commandCallStack = new Collection<CommandCall>();
            foreach (var powerShellCall in _powerShellAdapter.GetCallStack())
                commandCallStack.Add(CreateCommandCall(powerShellCall));

            return commandCallStack;
        }

        private static CommandCall CreateCommandCall(PSObject powerShellCall)
            => new CommandCall(GetCommandName(powerShellCall), GetScriptLineNumber(powerShellCall))
                .AddArguments(GetCommandArguments(powerShellCall));

        private string GetHostName() 
            => _powerShellAdapter.GetHostName();

        private string GetHostVersion() 
            => _powerShellAdapter.GetHostVersion();

        private string GetHostCulture() 
            => _powerShellAdapter.GetHostCulture();

        private static string GetCommandArguments(PSObject psObject)
             => psObject.Properties["InvocationInfo"].Value.ToString();

        private static string GetCommandName(PSObject psObject)
            => psObject.Properties["Command"].Value.ToString();

        private static int GetScriptLineNumber(PSObject psObject)
            => (int)psObject.Properties["ScriptLineNumber"].Value;

    }
}
