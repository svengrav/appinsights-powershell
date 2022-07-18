using System.Management.Automation;

namespace AppInsights.Context
{
    /// <summary>
    /// Extends the PSCmldet with a function to get the call stack.
    /// </summary>
    internal static class PowerShellCmdletExtension
    {
        internal static PowerShellCommandContext GetCommandContext(this PSCmdlet psCmdlet, int captureLevel)
            => new PowerShellCommandContext(new PowerShellAdapter(psCmdlet), captureLevel);

        internal static PowerShellHostContext GetHostContext(this PSCmdlet psCmdlet)
            => new PowerShellHostContext(new PowerShellAdapter(psCmdlet));
    }
}
