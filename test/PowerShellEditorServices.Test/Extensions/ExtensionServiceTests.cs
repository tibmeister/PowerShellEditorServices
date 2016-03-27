using Microsoft.PowerShell.EditorServices.Extensions;
using Microsoft.PowerShell.EditorServices.Utility;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Microsoft.PowerShell.EditorServices.Test.Extensions
{
    public class ExtensionServiceTests : IAsyncLifetime
    {
        private ScriptFile currentFile;
        private EditorContext commandContext;
        private ExtensionService extensionService;
        private TestEditorOperations editorOperations;

        private AsyncQueue<Tuple<EventType, ExtensionCommand>> extensionEventQueue =
            new AsyncQueue<Tuple<EventType, ExtensionCommand>>();

        private enum EventType
        {
            Add,
            Update,
            Remove
        }

        public async Task InitializeAsync()
        {
            this.extensionService = new ExtensionService();
            this.editorOperations = new TestEditorOperations();

            this.extensionService.ExtensionAdded += ExtensionService_ExtensionAdded;
            this.extensionService.ExtensionUpdated += ExtensionService_ExtensionUpdated;
            this.extensionService.ExtensionRemoved += ExtensionService_ExtensionRemoved;

            await this.extensionService.Initialize();

            var filePath = @"c:\Test\Test.ps1";
            this.currentFile = new ScriptFile(filePath, filePath, string.Empty, new Version("5.0"));
            this.commandContext =
                new EditorContext(
                    this.editorOperations,
                    currentFile,
                    BufferPosition.None,
                    BufferRange.None);
        }

        public Task DisposeAsync()
        {
            this.extensionService.Dispose();
            return Task.FromResult(true);
        }

        [Fact]
        public async Task CanRegisterAndInvokeCommandWithCmdletName()
        {
            await extensionService.PowerShellContext.ExecuteScriptString(
                "function Invoke-Extension { Write-Output \"Extension output!\" }\r\n" +
                "$psEditor.RegisterCommand(\"test.function\", \"Function extension\", \"Invoke-Extension\", $null);");

            // Wait for the add event
            await this.AssertExtensionEvent(EventType.Add, "test.getprocess");

            // Invoke the command
            await extensionService.InvokeCommand("test.getprocess", this.commandContext);

            // TODO: Assert!
        }

        [Fact]
        public async Task CanRegisterAndInvokeCommandWithScriptBlock()
        {
            await extensionService.PowerShellContext.ExecuteScriptString(
                "$psEditor.RegisterCommand(\"test.scriptblock\", \"ScriptBlock extension\", { Write-Output \"Extension output!\" }, $null);");

            await extensionService.InvokeCommand("test.scriptblock", this.commandContext);

            // TODO: Assert!
        }

        [Fact]
        public async Task ExtensionCanInsertText()
        {
            await extensionService.PowerShellContext.ExecuteScriptString(
                "function Invoke-InsertText($context) { $context.CurrentFile.InsertText(\"Inserted text!\"); }\r\n" +
                "$psEditor.RegisterCommand(\"test.inserttext\", \"Function extension\", \"Invoke-InsertText\", $null);");

            // Wait for the add event
            await this.AssertExtensionEvent(EventType.Add, "test.inserttext");

            // Invoke the command
            await extensionService.InvokeCommand("test.inserttext", this.commandContext);

            // TODO: Assert!
        }

        private async Task<ExtensionCommand> AssertExtensionEvent(EventType expectedEventType, string expectedExtensionName)
        {
            var eventExtensionTuple =
                await this.extensionEventQueue.DequeueAsync(
                    new CancellationTokenSource(5000).Token);

            Assert.Equal(expectedEventType, eventExtensionTuple.Item1);
            Assert.Equal(expectedExtensionName, eventExtensionTuple.Item2.Name);

            return eventExtensionTuple.Item2;
        }

        private async void ExtensionService_ExtensionAdded(object sender, ExtensionCommand e)
        {
            await this.extensionEventQueue.EnqueueAsync(
                new Tuple<EventType, ExtensionCommand>(EventType.Add, e));
        }

        private async void ExtensionService_ExtensionUpdated(object sender, ExtensionCommand e)
        {
            await this.extensionEventQueue.EnqueueAsync(
                new Tuple<EventType, ExtensionCommand>(EventType.Update, e));
        }

        private async void ExtensionService_ExtensionRemoved(object sender, ExtensionCommand e)
        {
            await this.extensionEventQueue.EnqueueAsync(
                new Tuple<EventType, ExtensionCommand>(EventType.Remove, e));
        }
    }

    public class TestEditorOperations : IEditorOperations
    {
        public Task InsertText(string filePath, string text, BufferRange insertRange)
        {
            throw new NotImplementedException();
        }

        public Task SetSelection(BufferRange selectionRange)
        {
            throw new NotImplementedException();
        }
    }
}
