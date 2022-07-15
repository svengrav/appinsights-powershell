using System.Collections.Generic;

namespace AppInsights.Context
{
    public class PowerShellCommandCall
    {
        public readonly string Name;
        public readonly int ScriptLineNumber;
        public IDictionary<string, object> Arguments { get; private set; }

        public PowerShellCommandCall(string name, int scriptLineNumber = 0)
        {
            Name = name;
            ScriptLineNumber = scriptLineNumber;
        }

        public PowerShellCommandCall AddArguments(IDictionary<string, object> arguments)
        {
            Arguments = arguments;
            return this;
        }
    }
}
