using AppInsights.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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
            Assert.AreEqual(powerShellAdapterMock.GetCommandCall(0).Command, commandContext.GetCommandCall().Name);
            Assert.AreEqual(powerShellAdapterMock.GetCommandCall(0).ScriptLineNumber, commandContext.GetCommandCall().ScriptLineNumber);

            Assert.AreEqual(powerShellAdapterMock.GetCommandCall(1).Command, commandContext.GetCommandCall(1).Name);
            Assert.AreEqual(powerShellAdapterMock.GetCommandCall(1).ScriptLineNumber, commandContext.GetCommandCall(1).ScriptLineNumber);

            Assert.IsTrue(commandContext.GetCommandCall(0).Arguments.ContainsKey("Type"));
            Assert.IsTrue(commandContext.GetCommandCall(1).Arguments.ContainsKey("Color"));

            Assert.AreEqual(powerShellAdapterMock.GetHostCulture(), commandContext.GetHost().Culture);
            Assert.AreEqual(powerShellAdapterMock.GetHostVersion(), commandContext.GetHost().Version);
            Assert.AreEqual(powerShellAdapterMock.GetHostName(), commandContext.GetHost().Name);

        }

    }
}