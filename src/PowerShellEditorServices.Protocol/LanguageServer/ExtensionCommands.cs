using Microsoft.PowerShell.EditorServices.Protocol.MessageProtocol;

namespace Microsoft.PowerShell.EditorServices.Protocol.LanguageServer
{
    public class ExtensionCommandAddedNotification
    {
        public static readonly
            EventType<ExtensionCommandAddedNotification> Type =
            EventType<ExtensionCommandAddedNotification>.Create("powerShell/extensionCommandAdded");

        public string Name { get; set; }

        public string DisplayName { get; set; }
    }

    public class ExtensionCommandUpdatedNotification
    {
        public static readonly
            EventType<ExtensionCommandUpdatedNotification> Type =
            EventType<ExtensionCommandUpdatedNotification>.Create("powerShell/extensionCommandUpdated");

        public string Name { get; set; }
    }

    public class ExtensionCommandRemovedNotification
    {
        public static readonly
            EventType<ExtensionCommandRemovedNotification> Type =
            EventType<ExtensionCommandRemovedNotification>.Create("powerShell/extensionCommandRemoved");

        public string Name { get; set; }
    }

    public class ClientEditorContext
    {
        public string CurrentFilePath { get; set; }

        public Position CursorPosition { get; set; }

        public Range SelectionRange { get; set; }

    }

    public class InvokeExtensionCommandRequest
    {
        public static readonly
            RequestType<InvokeExtensionCommandRequest, string> Type =
            RequestType<InvokeExtensionCommandRequest, string>.Create("powerShell/invokeExtensionCommand");

        public string Name { get; set; }

        public ClientEditorContext Context { get; set; }
    }

    public class InsertTextRequest
    {
        public static readonly
            RequestType<InsertTextRequest, string> Type =
            RequestType<InsertTextRequest, string>.Create("editor/insertText");

        public string FilePath { get; set; }

        public string InsertText { get; set; }

        public Range InsertRange { get; set; }
    }

    public class SetSelectionRequest
    {
        public static readonly
            RequestType<SetSelectionRequest, string> Type =
            RequestType<SetSelectionRequest, string>.Create("editor/setSelection");

        public Range SelectionRange { get; set; }
    }

    public class SetCursorPositionRequest
    {
        public static readonly
            RequestType<SetCursorPositionRequest, string> Type =
            RequestType<SetCursorPositionRequest, string>.Create("editor/setCursorPosition");

        public Position CursorPosition { get; set; }
    }
}
