using Microsoft.ApplicationInsights.Extensibility;

namespace AppInsights.Extensions
{
    public class CommandHost : ISerializableWithWriter
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

        public void Serialize(ISerializationWriter serializationWriter)
        {
            serializationWriter.WriteProperty("host" + nameof(Name), Name);
            serializationWriter.WriteProperty("host" + nameof(Version), Version);
            serializationWriter.WriteProperty("host" + nameof(Culture), Culture);
        }
    }
}
