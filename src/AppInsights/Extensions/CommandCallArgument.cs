
namespace AppInsights.Extensions
{
    public class CommandCallArgument
    {
        public string Name { get; set; }
        public string Value { get; set; }

        public override string ToString()
            => $"{Name}={Value}";
    }
 }
