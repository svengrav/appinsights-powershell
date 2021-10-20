using System.Collections.ObjectModel;
using System.Management.Automation;

namespace AppInsights.Extensions
{
    /// <summary>
    /// Extends the PSCmldet with a function to get the call stack.
    /// </summary>
    internal static class CommandContextExtension
    {
        internal static CommandContext GetCommandContext(Collection<PSObject> callStack)
            => CreateCommandContext(callStack);

        internal static CommandContext GetCommandContext(this PSCmdlet psCmdlet)
            => CreateCommandContext(GetPowerShellCallStack(psCmdlet));

        private static CommandContext CreateCommandContext(Collection<PSObject> powerShellCallStack)
        {
            var commandContext = new CommandContext();
            
            foreach (var powerShellCall in powerShellCallStack)
            {
                var commandName = GetCommandName(powerShellCall);

                var commandArguments = GetCommandArguments(powerShellCall);

                var scriptLineNumber = GetScriptLineNumber(powerShellCall);

                commandContext.AddCommandCall(new CommandCall(commandName, scriptLineNumber)
                    .AddArguments(commandArguments));
            }

            return commandContext;
        }

        private static string GetCommandArguments(PSObject commanCall)
            => commanCall.Properties["Arguments"].Value.ToString();

        private static string GetCommandName(PSObject commanCall)
            => commanCall.Properties["Command"].Value.ToString();

        private static int GetScriptLineNumber(PSObject commanCall)
            => (int) commanCall.Properties["ScriptLineNumber"].Value;

        private static Collection<PSObject> GetPowerShellCallStack(PSCmdlet psCmdlet)
        {
            var callStack = psCmdlet.InvokeCommand.InvokeScript("Get-PSCallStack");
            callStack.RemoveAt(0);
            return callStack;
        }
           
    }
}
