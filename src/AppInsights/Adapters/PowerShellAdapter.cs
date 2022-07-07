using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Management.Automation;

namespace AppInsights.Adapters
{
    public class PowerShellAdapter : IPowerShellAdapter
    {
        private readonly PSCmdlet _psCmdlet;

        public PowerShellAdapter(PSCmdlet psCmdlet)
        {
            _psCmdlet = psCmdlet;
        }

        public Collection<PSObject> GetCallStack()
        {
            var callStack = _psCmdlet.InvokeCommand.InvokeScript("Get-PSCallStack");
            callStack.RemoveAt(0);
            return callStack;
        }

        public string GetHostName()
            => _psCmdlet.Host.Name;

        public string GetHostVersion()
            => _psCmdlet.Host.Version.ToString();

        public string GetHostCulture()
            => _psCmdlet.Host.CurrentCulture.ToString();
    }
}
