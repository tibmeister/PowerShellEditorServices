using System;
using System.Management.Automation;

namespace Microsoft.PowerShell.EditorServices.Extensions
{
    public class EditorObject
    {
        #region Fields

        private IExtensionService extensionService;

        #endregion

        #region Properties

        public Version EditorServicesVersion
        {
            get { return this.GetType().Assembly.GetName().Version; }
        }

        #endregion

        internal EditorObject(IExtensionService extensionService)
        {
            this.extensionService = extensionService;
        }

        // NOTES:
        // - This class should be easily mockable.  Either that or we should be able
        //   to ship a module which replicates the cmdlet shapes for use in test
        //   frameworks.  Even better if we can provide a Pester test library for
        //   extensions written against the extension API.

        public void RegisterCommand(
            string commandName,
            string displayName,
            ScriptBlock scriptBlock,
            string keyBinding)
        {
            this.extensionService.RegisterCommand(
                new ExtensionCommand(
                    commandName,
                    displayName,
                    scriptBlock));
        }

        public void RegisterCommand(
            string commandName,
            string displayName,
            string cmdletNameToInvoke,
            string keyBinding)
        {
            this.extensionService.RegisterCommand(
                new ExtensionCommand(
                    commandName,
                    displayName,
                    cmdletNameToInvoke));
        }

        public EditorContext GetEditorContext()
        {
            //this.extensionService.GetEditorContext();
            return null;
        }
    }
}
