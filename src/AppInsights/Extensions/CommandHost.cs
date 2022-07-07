using System.Collections.Generic;

namespace AppInsights.Extensions
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

        public IDictionary<string, string> ToDictionary()
            => new Dictionary<string, string>
            {
                { nameof(Name).ToLower(), Name },
                { nameof(Version).ToLower(), Version},
                { nameof(Culture).ToLower(), Culture }
            };
    }
}
