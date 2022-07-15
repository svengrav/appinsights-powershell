using System.Collections.Generic;

namespace AppInsights.Context
{
    public class PowerShellStackItem
    {
        public readonly string Command;
        public readonly int ScriptLineNumber;
        public readonly string Location;
        public readonly Dictionary<string, object> Arguments;

        public PowerShellStackItem(string commandName, int? scriptLineNumber, string location, 
            Dictionary<string, object> arguments)
        {
            Command = commandName ?? "";
            ScriptLineNumber = scriptLineNumber ?? 0;
            Arguments = arguments ?? new Dictionary<string, object>();
            Location = location ?? "";
        }
    }
}
