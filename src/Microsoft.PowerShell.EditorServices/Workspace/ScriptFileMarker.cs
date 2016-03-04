//
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
//

using Microsoft.PowerShell.EditorServices.Utility;
using System;
using System.Management.Automation.Language;

namespace Microsoft.PowerShell.EditorServices
{
    /// <summary>
    /// Defines the message level of a script file marker.
    /// </summary>
    public enum ScriptFileMarkerLevel
    {
        /// <summary>
        /// The marker represents an informational message.
        /// </summary>
        Information = 0,

        /// <summary>
        /// The marker represents a warning message.
        /// </summary>
        Warning,

        /// <summary>
        /// The marker represents an error message.
        /// </summary>
        Error
    };

    /// <summary>
    /// Contains details about a marker that should be displayed
    /// for the a script file.  The marker information could come
    /// from syntax parsing or semantic analysis of the script.
    /// </summary>
    public class ScriptFileMarker
    {
        #region Properties

        /// <summary>
        /// Gets or sets the marker's message string.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets the marker's message level.
        /// </summary>
        public ScriptFileMarkerLevel Level { get; set; }

        /// <summary>
        /// Gets or sets the ScriptRegion where the marker should appear.
        /// </summary>
        public ScriptRegion ScriptRegion { get; set; }

        #endregion

        #region Public Methods

        internal static ScriptFileMarker FromParseError(
            ParseError parseError)
        {
            Validate.IsNotNull("parseError", parseError);

            return new ScriptFileMarker
            {
                Message = parseError.Message,
                Level = ScriptFileMarkerLevel.Error,
                ScriptRegion = ScriptRegion.Create(parseError.Extent)
            };
        }

        #endregion
    }
}
