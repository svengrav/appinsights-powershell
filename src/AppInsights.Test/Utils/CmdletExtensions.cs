using AppInsights.Commands;
using AppInsights.Extensions;

namespace AppInsights.Test
{
    internal static class CmdletExtensions
    {
        public static CommandResult Exec(this AppInsightsBaseCommand cmdlet)
        {
            var commandResults = new CommandResult();
            cmdlet.CommandContext = new CommandContext();
            cmdlet.CommandRuntime = new CommandRuntimeMock(commandResults);
            cmdlet.Execute();

            return commandResults;
        }
    }
}
