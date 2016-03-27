using System.Threading.Tasks;

namespace Microsoft.PowerShell.EditorServices.Extensions
{
    /// <summary>
    /// Provides an interface that must be implemented by an editor
    /// host to perform operations invoked by extensions written in
    /// PowerShell.
    /// </summary>
    public interface IEditorOperations
    {
        Task InsertText(string filePath, string insertText, BufferRange insertRange);

        Task SetSelection(BufferRange selectionRange);
    }
}
