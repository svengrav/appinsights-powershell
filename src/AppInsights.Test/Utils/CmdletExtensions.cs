using AppInsights.Commands;
using AppInsights.Context;

namespace AppInsights.Test
{
    internal static class CmdletExtensions
    {
        public static CommandResult Exec(this AppInsightsBaseCommand cmdlet)
        {
            var commandResults = new CommandResult();
            cmdlet.CommandContext = new PowerShellCommandContext(new PowerShellAdapterMock());
            cmdlet.HostContext = new PowerShellHostContext(new PowerShellAdapterMock());
            cmdlet.CommandRuntime = new CommandRuntimeMock(commandResults);
            cmdlet.Execute();

            return commandResults;
        }
    }
}
