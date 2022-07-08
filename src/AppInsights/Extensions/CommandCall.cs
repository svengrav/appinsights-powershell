
using Microsoft.ApplicationInsights.Extensibility;

namespace AppInsights.Extensions
{
    public class CommandCall : ISerializableWithWriter
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

        public void Serialize(ISerializationWriter serializationWriter)
        {
            serializationWriter.WriteProperty("command" + nameof(Name), Name);
            serializationWriter.WriteProperty("command" + nameof(ScriptLineNumber), ScriptLineNumber.ToString());
            serializationWriter.WriteProperty("command" + nameof(Arguments), Arguments);
        }
    }
}
