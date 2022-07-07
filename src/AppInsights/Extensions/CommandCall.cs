
using System.Collections.Generic;

namespace AppInsights.Extensions
{
    public class CommandCall
    {
        public readonly string Name;
        
        public readonly int ScriptLineNumber;

        public string Arguments { get; private set; }

        public CommandCall(string name, int scriptLineNumber = 0)
        {
            Name = name;
            ScriptLineNumber = scriptLineNumber;
        }

        public CommandCall AddArguments(string arguments)
        {
            Arguments = arguments;
            return this;
        }

        public IDictionary<string, string> ToDictionary()
            => new Dictionary<string, string>
            {
                { nameof(Name).ToLower(), Name },
                { nameof(ScriptLineNumber).ToLower(), ScriptLineNumber.ToString()},
                { nameof(Arguments).ToLower(), Arguments }
            };
    }
}
