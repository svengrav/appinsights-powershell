using AppInsights.Commands;
using AppInsights.Context;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;

namespace AppInsights.Test
{
    [TestClass]
    [TestCategory("CommandContext")]
    public class PowerShellContextTests
    {
        [TestMethod]
        public void command_context_is_created_by_ps_call_stack()
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
        public void host_context_is_valid()
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

        [TestMethod]
        public void context_level_is_greater_then_callstack_returns_last_command()
        {
            // Arrange
            var powerShellAdapterMock = new PowerShellAdapterMock();

            // Act
            var commandContext = new PowerShellCommandContext(powerShellAdapterMock);

            // Assert
            Assert.AreEqual(powerShellAdapterMock.GetCallStack().Last().Command, commandContext.GetCommandCall(100).Name);
        }

        [TestMethod]
        public void context_level_is_negative_returns_first_command()
        {
            // Arrange
            var powerShellAdapterMock = new PowerShellAdapterMock();

            // Act
            var commandContext = new PowerShellCommandContext(powerShellAdapterMock);

            // Assert
            Assert.AreEqual(powerShellAdapterMock.GetCallStack().First().Command, commandContext.GetCommandCall(-100).Name);
        }


        [TestMethod]
        public void command_context_is_empty_if_powershell_innvocation_is_null()
        {
            // Arrange
            var powerShelLAdapter = new PowerShellAdapter(new SendAppInsightsTraceCommand());
            var commandContext = new PowerShellCommandContext(powerShelLAdapter);

            // Assert
            Assert.AreEqual("", commandContext.GetCommandCall(0).Name);
            Assert.AreEqual(0, commandContext.GetCommandCall(0).Arguments.Count);
            Assert.AreEqual(0, commandContext.GetCommandCall(0).ScriptLineNumber);
        }

        [TestMethod]
        public void host_context_is_empty_if_powershell_is_null()
        {
            // Arrange
            var powerShelLAdapter = new PowerShellAdapter(new SendAppInsightsTraceCommand());
            var hostContext = new PowerShellHostContext(powerShelLAdapter);

            // Assert
            Assert.AreEqual("", hostContext.Name);
            Assert.AreEqual("", hostContext.Version);
            Assert.AreEqual("", hostContext.Culture);
        }

        [TestMethod]
        public void command_call_has_no_complex_arguments()
        {
            // Arrange
            var powerShellCommandCall = new PowerShellCommandCall("command_name", 1);
            powerShellCommandCall.AddArguments(new Dictionary<string, object> {
                { "Number" ,0 },
                { "Boolean" , true },
                { "String" , "String" },
                { "Double" , double.MaxValue },
                { "TimeSpan" , TimeSpan.MaxValue },
                { "DateTimeOffset" , DateTimeOffset.MaxValue },
                { "ComplexObject", new { Name = "Name", Adress = new { Street = "Street" } } },
                { "SwitchParameter", new SwitchParameter(true) }

            });

            // Assert
            Assert.AreEqual(0, powerShellCommandCall.Arguments["Number"]);
            Assert.AreEqual(true, powerShellCommandCall.Arguments["Boolean"]);
            Assert.AreEqual("String", powerShellCommandCall.Arguments["String"]);
            Assert.AreEqual(double.MaxValue, powerShellCommandCall.Arguments["Double"]);

            Assert.AreEqual(TimeSpan.MaxValue.ToString(), powerShellCommandCall.Arguments["TimeSpan"]);
            Assert.AreEqual(DateTimeOffset.MaxValue.ToString(), powerShellCommandCall.Arguments["DateTimeOffset"]);
            Assert.IsTrue(powerShellCommandCall.Arguments["ComplexObject"] is string);
            Assert.IsTrue(powerShellCommandCall.Arguments["SwitchParameter"] is string);
        }

    }
}