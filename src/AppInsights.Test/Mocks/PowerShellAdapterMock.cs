using AppInsights.Adapters;
using System.Collections.ObjectModel;
using System.Management.Automation;

namespace AppInsights.Test
{
    internal class PowerShellAdapterMock : IPowerShellAdapter
    {
        private readonly Collection<PSObject> _powerShellCallStack;

        public PowerShellAdapterMock()
        {
            var newTreeCmd = new { Command = "New-Tree", Arguments = "Type=AppleTree", ScriptLineNumber = 5 };
            var newBranchCmd = new { Command = "New-Branch", Arguments = "Color=Brown", ScriptLineNumber = 10 };

            _powerShellCallStack = new Collection<PSObject>();
            _powerShellCallStack.Add(new PSObject(newBranchCmd));
            _powerShellCallStack.Add(new PSObject(newTreeCmd));
        }

        public Collection<PSObject> GetCallStack()
            => _powerShellCallStack;

        public PSObject GetCommandCall(int level)
            => _powerShellCallStack[level].Properties["Command"].Value.ToString();

        public string GetCommandCallArgumments(int level)
            => _powerShellCallStack[level].Properties["Arguments"].Value.ToString();

        public int GetCommandCallScriptLineNumber(int level)
            => (int) _powerShellCallStack[level].Properties["ScriptLineNumber"].Value;

        public string GetHostCulture()
            => "en-US";

        public string GetHostName()
            => "ConsoleHost";

        public string GetHostVersion()
            => "5.1.0.0";
    }
}
