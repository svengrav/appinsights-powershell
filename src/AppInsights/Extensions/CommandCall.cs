using System.Collections.Generic;

namespace AppInsights.Extensions
{
    public class CommandCall
    {
        public readonly string Command;
        public readonly int ScriptLineNumber;

        public string Arguments { get; private set; }

        public CommandCall(string name, int scriptLineNumber = 0)
        {
            Command = name;
            ScriptLineNumber = scriptLineNumber;
        }

        public CommandCall AddArguments(string arguments)
        {
            Arguments = arguments;
            return this;
        }
    }
}
