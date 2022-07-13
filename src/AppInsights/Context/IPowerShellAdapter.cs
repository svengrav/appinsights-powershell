using System.Collections.Generic;

namespace AppInsights.Context
{
    public interface IPowerShellAdapter
    {
        ICollection<PowerShellStackItem> GetCallStack();
        string GetHostCulture();
        string GetHostName();
        string GetHostVersion();
    }
}