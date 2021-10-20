using AppInsights.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.ObjectModel;
using System.Management.Automation;

namespace AppInsights.Test
{
    [TestClass]
    [TestCategory("CommandContext")]
    public class CommandContextTests
    {
        [TestMethod]
        public void a_command_context_is_created_by_ps_call_stack()
        {
            var scriptBlockCommand = new { Command = "<ScriptBlock>", Arguments = "", ScriptLineNumber = 0 };
            var artworkCommand = new { Command = "New-Artwork", Arguments = "Name=Apple", ScriptLineNumber = 5 };
            var brushCommand = new { Command = "New-Brush", Arguments = "Color=Orange", ScriptLineNumber = 10 };

            // Arrange
            var commandCallStack = new Collection<PSObject>();
            commandCallStack.Add(new PSObject(scriptBlockCommand));
            commandCallStack.Add(new PSObject(artworkCommand));
            commandCallStack.Add(new PSObject(brushCommand));

            // Act
            var commandContext = CommandContextExtension.GetCommandContext(commandCallStack);

            // Assert
            Assert.AreEqual(brushCommand.Command, commandContext.GetCommandCall().Command);
            Assert.AreEqual(brushCommand.Arguments, commandContext.GetCommandCall().Arguments);
            Assert.AreEqual(brushCommand.ScriptLineNumber, commandContext.GetCommandCall().ScriptLineNumber);

            Assert.AreEqual(artworkCommand.Command, commandContext.GetCommandCall(1).Command);
            Assert.AreEqual(artworkCommand.Arguments, commandContext.GetCommandCall(1).Arguments);
            Assert.AreEqual(artworkCommand.ScriptLineNumber, commandContext.GetCommandCall(1).ScriptLineNumber);
        }

    }
}