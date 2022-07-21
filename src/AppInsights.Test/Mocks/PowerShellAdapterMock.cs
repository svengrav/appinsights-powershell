using AppInsights.Context;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AppInsights.Test
{
    internal class PowerShellAdapterMock : IPowerShellAdapter
    {

        private List<PowerShellStackItem> _callStack = new List<PowerShellStackItem>();
        
        public PowerShellAdapterMock(int callStackSize = 5)
        {
            CreateCallStack(callStackSize);
        }

        public ICollection<PowerShellStackItem> GetCallStack()
            => _callStack;

        public string GetHostCulture()
            => "en-US";

        public string GetHostName()
            => "ConsoleHost";

        public string GetHostVersion()
            => "5.1.0.0";

        public PowerShellStackItem GetCommandCall(int index)
            => GetCallStack().ToArray()[index];

        private string NewRandomCommandName()
            => Guid.NewGuid().ToString();


        private string NewRandomCommandLocation()
            => Guid.NewGuid().ToString();

        private int NewRandomCommandScriptLineNumber()
            => new Random().Next(100);

        private Dictionary<string, object> NewRandomCommandArguments()
            => new Dictionary<string, object>()
                {
                    { "Type", "AppleTree" }
                };

        private void CreateCallStack(int callStackSize)
        {
            for (int i = 0; i < callStackSize; i++)
                _callStack.Add(
                    new PowerShellStackItem(NewRandomCommandName(),
                        NewRandomCommandScriptLineNumber(),
                        NewRandomCommandLocation(),
                        NewRandomCommandArguments()));
        }
    }
}
