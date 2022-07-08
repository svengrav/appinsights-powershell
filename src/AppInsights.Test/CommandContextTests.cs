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
            var powerShellAdapterMock = new PowerShellAdapterMock();

            // Act
            var commandContext = new CommandContext(powerShellAdapterMock);

            // Assert
            Assert.AreEqual(powerShellAdapterMock.GetCommandCall(0), commandContext.GetCommandCall().Name);
            Assert.AreEqual(powerShellAdapterMock.GetCommandCallArgumments(0), commandContext.GetCommandCall().Arguments);
            Assert.AreEqual(powerShellAdapterMock.GetCommandCallScriptLineNumber(0), commandContext.GetCommandCall().ScriptLineNumber);

            Assert.AreEqual(powerShellAdapterMock.GetCommandCall(1), commandContext.GetCommandCall(1).Name);
            Assert.AreEqual(powerShellAdapterMock.GetCommandCallArgumments(1), commandContext.GetCommandCall(1).Arguments);
            Assert.AreEqual(powerShellAdapterMock.GetCommandCallScriptLineNumber(1), commandContext.GetCommandCall(1).ScriptLineNumber);

            Assert.AreEqual(powerShellAdapterMock.GetHostCulture(), commandContext.GetHost().Culture);
            Assert.AreEqual(powerShellAdapterMock.GetHostVersion(), commandContext.GetHost().Version);
            Assert.AreEqual(powerShellAdapterMock.GetHostName(), commandContext.GetHost().Name);

        }

    }
}