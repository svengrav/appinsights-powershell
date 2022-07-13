using AppInsights.Adapters;
using AppInsights.Context;
using System.Management.Automation;

namespace AppInsights.Extensions
{
    /// <summary>
    /// Extends the PSCmldet with a function to get the call stack.
    /// </summary>
    internal static class PSCmdletExtension
    {
        internal static CommandContext GetCommandContext(this PSCmdlet psCmdlet, int contextLevel = 0)
            => new CommandContext(new PowerShellAdapter(psCmdlet), contextLevel);

        internal static HostContext GetHostContext(this PSCmdlet psCmdlet)
            => new HostContext(new PowerShellAdapter(psCmdlet));
    }
}
