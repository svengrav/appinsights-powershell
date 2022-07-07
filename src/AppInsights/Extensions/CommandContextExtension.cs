using AppInsights.Adapters;
using System.Management.Automation;

namespace AppInsights.Extensions
{
    /// <summary>
    /// Extends the PSCmldet with a function to get the call stack.
    /// </summary>
    internal static class CommandContextExtension
    {
        internal static CommandContext GetCommandContext(this PSCmdlet psCmdlet)
            => new CommandContext(new PowerShellAdapter(psCmdlet));      
    }
}
