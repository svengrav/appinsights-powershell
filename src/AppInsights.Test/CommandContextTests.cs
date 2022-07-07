using AppInsights.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Globalization;

namespace AppInsights.Test
{
    [TestClass]
    [TestCategory("CommandContext")]
    public class CommandContextTests
    {
        [TestMethod]
        public void a_command_context_is_created_by_ps_call_stack()
        {
            var powerShellHost = new PowerShellAdapterMock();

            // Act
            var commandContext = new CommandContext(powerShellHost);

            // Assert
            Assert.AreEqual(powerShellHost.GetCommandCall(0), commandContext.GetCommandCall().Name);
            Assert.AreEqual(powerShellHost.GetCommandCallArgumments(0), commandContext.GetCommandCall().Arguments);
            Assert.AreEqual(powerShellHost.GetCommandCallScriptLineNumber(0), commandContext.GetCommandCall().ScriptLineNumber);

            Assert.AreEqual(powerShellHost.GetCommandCall(1), commandContext.GetCommandCall(1).Name);
            Assert.AreEqual(powerShellHost.GetCommandCallArgumments(1), commandContext.GetCommandCall(1).Arguments);
            Assert.AreEqual(powerShellHost.GetCommandCallScriptLineNumber(1), commandContext.GetCommandCall(1).ScriptLineNumber);

            Assert.AreEqual(powerShellHost.GetHostCulture(), commandContext.GetHost().Culture);
            Assert.AreEqual(powerShellHost.GetHostVersion(), commandContext.GetHost().Version);
            Assert.AreEqual(powerShellHost.GetHostName(), commandContext.GetHost().Name);

        }

    }
}