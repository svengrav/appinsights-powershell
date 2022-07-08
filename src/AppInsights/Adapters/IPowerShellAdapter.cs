
using System.Collections.Generic;

namespace AppInsights.Adapters
{
    public interface IPowerShellAdapter
    {
        ICollection<PowerShellCommandCall> GetCallStack();
        string GetHostCulture();
        string GetHostName();
        string GetHostVersion();
    }
}