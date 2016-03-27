using System.Management.Automation.Language;

namespace Microsoft.PowerShell.EditorServices.Extensions
{
    public class FileContext
    {
        private ScriptFile scriptFile;
        private IEditorOperations editorOperations;

        public string Path
        {
            get { return this.scriptFile.FilePath; }
        }

        public Ast Ast
        {
            get { return this.scriptFile.ScriptAst; }
        }

        public string Contents
        {
            get { return this.scriptFile.Contents;  }
        }

        public BufferRange FileRange
        {
            get { return this.scriptFile.FileRange; }
        }

        public FileContext(
            ScriptFile scriptFile,
            IEditorOperations editorOperations)
        {
            this.scriptFile = scriptFile;
            this.editorOperations = editorOperations;
        }

        public void InsertText(string newText)
        {
            this.InsertText(
                newText,
                BufferRange.None);
        }

        public void InsertText(string newText, BufferPosition insertPosition)
        {
            this.InsertText(
                newText,
                new BufferRange(insertPosition, insertPosition));
        }

        public void InsertText(string newText, BufferRange insertRange)
        {
            this.editorOperations
                .InsertText(this.scriptFile.ClientFilePath, newText, insertRange)
                .Wait();
        }
    }

    public class EditorContext
    {
        private IEditorOperations editorOperations;

        public FileContext CurrentFile { get; private set; }

        public BufferRange SelectedRange { get; private set; }

        public BufferPosition CursorPosition { get; private set; }

        public EditorContext(
            IEditorOperations editorOperations,
            ScriptFile currentFile,
            BufferPosition cursorPosition,
            BufferRange selectedRange)
        {
            this.editorOperations = editorOperations;
            this.CurrentFile = new FileContext(currentFile, editorOperations);
            this.SelectedRange = selectedRange;
            this.CursorPosition = cursorPosition;
        }

        public void SetSelection(BufferRange selectionRange)
        {
            this.editorOperations
                .SetSelection(selectionRange)
                .Wait();
        }
    }
}
