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
            => _psCmdlet.Host.Name;

        public string GetHostVersion()
            => _psCmdlet.Host.Version.ToString();

        public string GetHostCulture()
            => _psCmdlet.Host.CurrentCulture.ToString();

        public ICollection<PowerShellStackItem> GetCallStack()
        {
            var commandCallList = new List<PowerShellStackItem>();
            var powerShellCallStack = _psCmdlet.InvokeCommand.InvokeScript("Get-PSCallStack");
            RemoveGetPSCallStackFromCallStack(powerShellCallStack);

            foreach (var psObject in powerShellCallStack)
                commandCallList.Add(CreatePowerShellCommandCall(CastToCallStackFrame(psObject)));

            return commandCallList;
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
            => callStackFrame.InvocationInfo.MyCommand.Name;

        private int GetScriptLineNumber(CallStackFrame callStackFrame)
            => callStackFrame.InvocationInfo.ScriptLineNumber;

        private string GetLocation(CallStackFrame callStackFrame)
            => callStackFrame.GetScriptLocation();

        private Dictionary<string, object> GetArgumentDictionary(CallStackFrame callStackFrame)
            => callStackFrame.InvocationInfo.BoundParameters;
    }
}
