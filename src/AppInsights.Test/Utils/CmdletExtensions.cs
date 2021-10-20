using AppInsights.Commands;

namespace AppInsights.Test
{
    internal static class CmdletExtensions
    {
        public static CommandResult Exec(this AppInsightsBaseCommand cmdlet)
        {
            var commandResults = new CommandResult();
            cmdlet.CommandRuntime = new CommandRuntimeMock(commandResults);
            cmdlet.Execute();

            return commandResults;
        }
    }
}
