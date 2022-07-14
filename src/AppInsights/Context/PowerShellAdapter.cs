using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Management.Automation;

namespace AppInsights.Context
{
    public class PowerShellAdapter : IPowerShellAdapter
    {
        private readonly PSCmdlet _psCmdlet;

        public PowerShellAdapter(PSCmdlet psCmdlet)
        {
            _psCmdlet = psCmdlet;
        }

        public string GetHostName()
            => HasValue(_psCmdlet?.Host?.Name) ? _psCmdlet.Host.Name : "";

        public string GetHostVersion()
            => HasValue(_psCmdlet?.Host?.Version) ? _psCmdlet.Host.Version.ToString() : "";

        public string GetHostCulture()
            => HasValue(_psCmdlet?.Host?.CurrentCulture) ? _psCmdlet.Host.CurrentCulture.ToString() : "";

        public ICollection<PowerShellStackItem> GetCallStack()
        {
            try
            {
                var commandCallList = new List<PowerShellStackItem>();
                var powerShellCallStack = _psCmdlet.InvokeCommand.InvokeScript("Get-PSCallStack");
                RemoveGetPSCallStackFromCallStack(powerShellCallStack);

                foreach (var psObject in powerShellCallStack)
                    commandCallList.Add(CreatePowerShellCommandCall(CastToCallStackFrame(psObject)));

                return commandCallList;
            } 
            catch 
            {
                return new List<PowerShellStackItem>()
                {
                    new PowerShellStackItem("", 0, "", null)
                };
            }
        }

        private static CallStackFrame CastToCallStackFrame(PSObject callStackFrame)
            => (CallStackFrame)callStackFrame.BaseObject;

        private static void RemoveGetPSCallStackFromCallStack(Collection<PSObject> callStack)
        {
            callStack.RemoveAt(0);
        }

        private PowerShellStackItem CreatePowerShellCommandCall(CallStackFrame callStackFrame)
            => new PowerShellStackItem(GetCommandName(callStackFrame),
                GetScriptLineNumber(callStackFrame),
                GetLocation(callStackFrame),
                GetArgumentDictionary(callStackFrame));

        private string GetCommandName(CallStackFrame callStackFrame)
            => HasValue(callStackFrame?.InvocationInfo?.MyCommand?.Name) ? callStackFrame.InvocationInfo.MyCommand.Name : "";

        private int GetScriptLineNumber(CallStackFrame callStackFrame)
             => HasValue(callStackFrame?.InvocationInfo?.ScriptLineNumber) ? callStackFrame.InvocationInfo.ScriptLineNumber : 0;

        private Dictionary<string, object> GetArgumentDictionary(CallStackFrame callStackFrame)
            => HasValue(callStackFrame?.InvocationInfo?.BoundParameters) ? callStackFrame.InvocationInfo.BoundParameters : new Dictionary<string, object>();

        private string GetLocation(CallStackFrame callStackFrame)
            => HasValue(callStackFrame) ? callStackFrame.GetScriptLocation() : "";

        private bool HasValue(object value)
            => value != null;

    }
}
