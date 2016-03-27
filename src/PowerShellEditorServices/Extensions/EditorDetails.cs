using System;

namespace Microsoft.PowerShell.EditorServices.Extensions
{
    public class EditorDetails
    {
        public string Name { get; private set; }

        public Version Version { get; private set; }

        public Version EditorServicesVersion
        {
            get { return this.GetType().Assembly.GetName().Version; }
        }

        public EditorDetails(string name, Version version)
        {
            this.Name = name;
            this.Version = version;
        }
    }
}
