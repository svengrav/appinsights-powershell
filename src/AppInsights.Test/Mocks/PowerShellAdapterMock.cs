using AppInsights.Context;
using System.Collections.Generic;
using System.Linq;

namespace AppInsights.Test
{
    internal class PowerShellAdapterMock : IPowerShellAdapter
    {
        public ICollection<PowerShellStackItem> GetCallStack()
            => new List<PowerShellStackItem>()
            {
                new PowerShellStackItem("New-Tree", 5, "Script", new Dictionary<string, object>()
                {
                    { "Type", "AppleTree" }
                }),
                new PowerShellStackItem("New-Branch", 10, "Script", new Dictionary<string, object>()
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

        public PowerShellStackItem GetCommandCall(int index)
            => GetCallStack().ToArray()[index];
    }
}
