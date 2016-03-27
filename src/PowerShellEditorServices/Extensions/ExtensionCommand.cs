using System.Management.Automation;

namespace Microsoft.PowerShell.EditorServices.Extensions
{
    public class ExtensionCommand
    {
        public string Name { get; private set; }

        public string DisplayName { get; private set; }

        public ScriptBlock ScriptBlock { get; private set; }

        public ExtensionCommand(
            string commandName,
            string displayName,
            string cmdletName)
            : this(
                  commandName,
                  displayName,
                  ScriptBlock.Create(
                      string.Format(
                          "param($context) {0} $context",
                          cmdletName)))
        {
        }

        public ExtensionCommand(
            string commandName,
            string displayName,
            ScriptBlock scriptBlock)
        {
            this.Name = commandName;
            this.DisplayName = displayName;
            this.ScriptBlock = scriptBlock;
        }
    }
}
