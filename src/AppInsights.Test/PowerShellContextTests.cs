using AppInsights.Context;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AppInsights.Test
{
    [TestClass]
    [TestCategory("CommandContext")]
    public class PowerShellContextTests
    {
        [TestMethod]
        public void a_command_context_is_created_by_ps_call_stack()
        {
            // Arrange
            var powerShellAdapterMock = new PowerShellAdapterMock();

            // Act
            var commandContext = new PowerShellCommandContext(powerShellAdapterMock);

            // Assert
            Assert.AreEqual(powerShellAdapterMock.GetCommandCall(0).Command, commandContext.GetCommandCall().Name);
            Assert.AreEqual(powerShellAdapterMock.GetCommandCall(0).ScriptLineNumber, commandContext.GetCommandCall().ScriptLineNumber);

            Assert.AreEqual(powerShellAdapterMock.GetCommandCall(1).Command, commandContext.GetCommandCall(1).Name);
            Assert.AreEqual(powerShellAdapterMock.GetCommandCall(1).ScriptLineNumber, commandContext.GetCommandCall(1).ScriptLineNumber);

            Assert.IsTrue(commandContext.GetCommandCall(0).Arguments.ContainsKey("Type"));
            Assert.IsTrue(commandContext.GetCommandCall(1).Arguments.ContainsKey("Color"));
        }

        [TestMethod]
        public void a_host_context_is_valid()
        {
            // Arrange
            var powerShellAdapterMock = new PowerShellAdapterMock();

            // Act
            var hostContext = new PowerShellHostContext(powerShellAdapterMock);

            // Assert
            Assert.AreEqual(powerShellAdapterMock.GetHostCulture(), hostContext.Culture);
            Assert.AreEqual(powerShellAdapterMock.GetHostVersion(), hostContext.Version);
            Assert.AreEqual(powerShellAdapterMock.GetHostName(), hostContext.Name);

        }

    }
}