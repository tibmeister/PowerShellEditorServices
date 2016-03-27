using System;
using System.Collections.Generic;
using System.IO;
using System.Management.Automation;
using System.Threading.Tasks;

namespace Microsoft.PowerShell.EditorServices.Extensions
{
    public class ExtensionService : IExtensionService, IDisposable
    {
        #region Fields

        private bool ownsPowerShellContext = false;

        private Dictionary<string, ExtensionCommand> extensionCommands =
            new Dictionary<string, ExtensionCommand>();

        #endregion

        #region Properties

        public EditorObject EditorObject { get; private set; }

        public PowerShellContext PowerShellContext { get; private set; }

        #endregion

        #region Constructors

        public ExtensionService(): this(new PowerShellContext())
        {
            this.ownsPowerShellContext = true;
        }

        public ExtensionService(PowerShellContext powerShellContext)
        {
            this.PowerShellContext = powerShellContext;
        }

        #endregion

        #region Public Methods

        public async Task Initialize()
        {
            this.EditorObject = new EditorObject(this);

            // Register the editor object in the runspace
            PSCommand variableCommand = new PSCommand();
            using (RunspaceHandle handle = await this.PowerShellContext.GetRunspaceHandle())
            {
                handle.Runspace.SessionStateProxy.PSVariable.Set(
                    "psEditor",
                    this.EditorObject);
            }

            // Load the cmdlet interface
            Type thisType = this.GetType();
            Stream resourceStream =
                thisType.Assembly.GetManifestResourceStream(
                    thisType.Namespace + ".CmdletInterface.ps1");

            using (StreamReader reader = new StreamReader(resourceStream))
            {
                string cmdletInterfaceScript = reader.ReadToEnd();
                await this.PowerShellContext.ExecuteScriptString(cmdletInterfaceScript);
            }
        }

        public async Task InvokeCommand(string commandName, EditorContext commandContext)
        {
            ExtensionCommand extensionCommand;

            if (this.extensionCommands.TryGetValue(commandName, out extensionCommand))
            {
                PSCommand executeCommand = new PSCommand();
                executeCommand.AddCommand("Invoke-Command");
                executeCommand.AddParameter("ScriptBlock", extensionCommand.ScriptBlock);
                executeCommand.AddParameter("ArgumentList", new object[] { commandContext });

                await this.PowerShellContext.ExecuteCommand<object>(executeCommand, true, true);
            }
            else
            {
                // TODO: Throw exception?
            }
        }

        public void RegisterCommand(ExtensionCommand extensionCommand)
        {
            bool extensionExists =
                this.extensionCommands.ContainsKey(
                    extensionCommand.Name);

            // Add or replace the extension command
            this.extensionCommands[extensionCommand.Name] = extensionCommand;

            if (!extensionExists)
            {
                this.PowerShellContext.WriteOutput(
                    "Registered new extension " + extensionCommand.Name,
                    true,
                    OutputType.Verbose);

                this.OnExtensionAdded(extensionCommand);
            }
            else
            {
                this.PowerShellContext.WriteOutput(
                    "Overriding existing extension " + extensionCommand.Name,
                    true,
                    OutputType.Verbose);

                this.OnExtensionUpdated(extensionCommand);
            }
        }

        public Task<EditorContext> GetEditorContext()
        {
            // TODO!
            return null;
        }

        public void Dispose()
        {
            // TODO: Should we do any extension cleanup here?  Shutdown notifications?

            if (this.ownsPowerShellContext && this.PowerShellContext != null)
            {
                this.PowerShellContext.Dispose();
            }
        }

        #endregion

        #region Events

        public event EventHandler<ExtensionCommand> ExtensionAdded;

        private void OnExtensionAdded(ExtensionCommand extension)
        {
            this.ExtensionAdded?.Invoke(this, extension);
        }

        public event EventHandler<ExtensionCommand> ExtensionUpdated;

        private void OnExtensionUpdated(ExtensionCommand extension)
        {
            this.ExtensionUpdated?.Invoke(this, extension);
        }

        public event EventHandler<ExtensionCommand> ExtensionRemoved;

        private void OnExtensionRemoved(ExtensionCommand extension)
        {
            this.ExtensionRemoved?.Invoke(this, extension);
        }

        #endregion

        #region Private Methods

        #endregion
    }
}
