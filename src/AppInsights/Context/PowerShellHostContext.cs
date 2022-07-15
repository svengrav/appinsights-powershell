namespace AppInsights.Context
{
    public class PowerShellHostContext
    {
        private readonly IPowerShellAdapter _powerShellAdapter;
        public readonly string Name;
        public readonly string Version;
        public readonly string Culture;

        public PowerShellHostContext(IPowerShellAdapter powerShellAdapter)
        {
            _powerShellAdapter = powerShellAdapter;
            Name = GetHostName();
            Version = GetHostVersion();
            Culture = GetHostCulture();
        }

        private string GetHostName()
            => _powerShellAdapter.GetHostName();

        private string GetHostVersion()
            => _powerShellAdapter.GetHostVersion();

        private string GetHostCulture()
            => _powerShellAdapter.GetHostCulture();
    }
}
