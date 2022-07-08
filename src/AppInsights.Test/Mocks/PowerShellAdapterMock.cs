using AppInsights.Adapters;
using System.Collections.Generic;
using System.Linq;

namespace AppInsights.Test
{
    internal class PowerShellAdapterMock : IPowerShellAdapter
    {
        public ICollection<PowerShellCommandCall> GetCallStack()
            => new List<PowerShellCommandCall>()
            {
                new PowerShellCommandCall("New-Tree", 5, "Script", new Dictionary<string, object>()
                {
                    { "Type", "AppleTree" }
                }),
                new PowerShellCommandCall("New-Branch", 10, "Script", new Dictionary<string, object>()
                {
                    { "Color", "Brown" }
                })
            };

        public string GetHostCulture()
            => "en-US";

        public string GetHostName()
            => "ConsoleHost";

        public string GetHostVersion()
            => "5.1.0.0";

        public PowerShellCommandCall GetCommandCall(int index)
            => GetCallStack().ToArray()[index];
    }
}
