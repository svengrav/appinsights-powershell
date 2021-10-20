using Microsoft.ApplicationInsights.Extensibility;
using System.Collections.Generic;
using System.Linq;

namespace AppInsights.Extensions
{
    public class CommandContext : IExtension
    {
        private Stack<CommandCall> CallStack { get; set; } = new Stack<CommandCall>();

        public IExtension DeepClone()
        {
            return null;
        }

        public CommandCall GetCommandCall(int level = 0)
        {
            return CallStack.ElementAt(level);
        }

        public void Serialize(ISerializationWriter serializationWriter)
        {
            serializationWriter.WriteProperty("Command", "");
        }

        public CommandContext AddCommandCall(CommandCall commandCall)
        {
            CallStack.Push(commandCall);
            return this;
        }

        //public Dictionary<string, object> SerializeIntoDictionary()
        //{
        //    return new Dictionary<string, object>{
        //        { "Command", Command},
        //        { "Arguments", Arguments}
        //    };
        //}
    }
}
