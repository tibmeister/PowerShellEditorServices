using System;
using System.Threading.Tasks;

namespace Microsoft.PowerShell.EditorServices.Extensions
{
    public interface IExtensionService
    {
        Task InvokeCommand(
            string commandName,
            EditorContext commandContext);

        void RegisterCommand(ExtensionCommand extensionCommand);

        Task<EditorContext> GetEditorContext();

        // TODO: This may be the wrong way to expose this, think a bit more...
        event EventHandler<ExtensionCommand> ExtensionAdded;
        event EventHandler<ExtensionCommand> ExtensionUpdated;
        event EventHandler<ExtensionCommand> ExtensionRemoved;
    }
}
