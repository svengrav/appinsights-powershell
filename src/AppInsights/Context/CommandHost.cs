
namespace AppInsights.Context
{
    public class CommandHost
    {
        public string Name;

        public string Version;

        public string Culture;

        public CommandHost(string name, string version, string culture)
        {
            Name = name;
            Version = version;
            Culture = culture;
        }
    }
}
