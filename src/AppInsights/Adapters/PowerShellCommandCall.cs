using System.Collections.Generic;

namespace AppInsights.Adapters
{
    public class PowerShellCommandCall
    {
        public readonly string Command;
        public readonly int ScriptLineNumber;
        public readonly Dictionary<string, object> Arguments;
        public readonly string Location;

        public PowerShellCommandCall(string commandName, int scriptLineNumber, string location, Dictionary<string, object> arguments)
        {
            Command = commandName;
            ScriptLineNumber = scriptLineNumber;
            Arguments = arguments;
            Location = location;
        }
    }
}
