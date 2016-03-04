//
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
//

using Microsoft.PowerShell.EditorServices.Utility;
using System;
using System.IO;
using System.Linq;
using System.Management.Automation.Runspaces;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.PowerShell.EditorServices
{
    /// <summary>
    /// Provides a high-level service for performing semantic analysis
    /// of PowerShell scripts.
    /// </summary>
    public class AnalysisService : IDisposable
    {
        #region Private Fields

        /// <summary>
        /// Defines the list of Script Analyzer rules to include by default.
        /// In the future, a default rule set from Script Analyzer may be used.
        /// </summary>
        private static readonly string[] IncludedRules = new string[]
        {
            "PSUseApprovedVerbs",
            "PSReservedCmdletChar",
            "PSReservedParams",
            "PSShouldProcess",
            "PSMissingModuleManifestField",
            "PSAvoidDefaultValueSwitchParameter",
            "PSUseDeclaredVarsMoreThanAssigments"
        };

        #endregion

        #region Constructors

        /// <summary>
        /// Creates an instance of the AnalysisService class.
        /// </summary>
        public AnalysisService()
        {
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Performs semantic analysis on the given ScriptFile and returns
        /// an array of ScriptFileMarkers.
        /// </summary>
        /// <param name="file">The ScriptFile which will be analyzed for semantic markers.</param>
        /// <returns>An array of ScriptFileMarkers containing semantic analysis results.</returns>
        public ScriptFileMarker[] GetSemanticMarkers(ScriptFile file)
        {
            // Return an empty marker list
            // TODO: Re-enable ScriptAnalyzer using other means
            return new ScriptFileMarker[0];
        }

        /// <summary>
        /// Disposes the runspace being used by the analysis service.
        /// </summary>
        public void Dispose()
        {
        }

        #endregion
    }
}
