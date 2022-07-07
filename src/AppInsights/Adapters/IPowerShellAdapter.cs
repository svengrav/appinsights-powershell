using System.Collections.ObjectModel;
using System.Management.Automation;

namespace AppInsights.Adapters
{
    public interface IPowerShellAdapter
    {
        Collection<PSObject> GetCallStack();
        string GetHostCulture();
        string GetHostName();
        string GetHostVersion();
    }
}