using System.Management.Automation.Language;

namespace Microsoft.PowerShell.EditorServices.Extensions
{
    public class FileContext
    {
        private ScriptFile scriptFile;
        private EditorContext editorContext;
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
            EditorContext editorContext,
            IEditorOperations editorOperations)
        {
            this.scriptFile = scriptFile;
            this.editorContext = editorContext;
            this.editorOperations = editorOperations;
        }

        public void InsertText(string newText)
        {
            // Is there a selection?
            if (this.editorContext.SelectedRange.HasRange)
            {
                this.InsertText(
                    newText,
                    this.editorContext.SelectedRange);
            }
            else
            {
                this.InsertText(
                    newText,
                    this.editorContext.CursorPosition);
            }
        }

        public void InsertText(string newText, BufferPosition insertPosition)
        {
            this.InsertText(
                newText,
                new BufferRange(insertPosition, insertPosition));
        }

        public void InsertText(
            string newText,
            int startLine,
            int startColumn,
            int endLine,
            int endColumn)
        {
            this.InsertText(
                newText,
                new BufferRange(
                    startLine,
                    startColumn,
                    endLine,
                    endColumn));
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
            this.CurrentFile = new FileContext(currentFile, this, editorOperations);
            this.SelectedRange = selectedRange;
            this.CursorPosition = cursorPosition;
        }

        public void SetSelection(
            int startLine,
            int startColumn,
            int endLine,
            int endColumn)
        {
            BufferRange selectionRange =
                new BufferRange(
                    startLine, startColumn,
                    endLine, endColumn);

            this.editorOperations
                .SetSelection(selectionRange)
                .Wait();
        }
    }
}
