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
            Arguments = new Dictionary<string, object>();
        }

        public PowerShellCommandCall AddArguments(IDictionary<string, object> arguments)
        {
            foreach(var argument in arguments)
                TransformArgumentToPrimitiveType(argument);

            return this;
        }

        private void TransformArgumentToPrimitiveType(KeyValuePair<string, object> argument)
        {
            if (ArgumentIsPrimitive(argument.Value))
                Arguments.Add(argument.Key, argument.Value);
            else
                Arguments.Add(argument.Key, argument.Value.ToString());
        }

        private bool ArgumentIsPrimitive(object argumentValue)
            => argumentValue.GetType().IsPrimitive;
    }
}
