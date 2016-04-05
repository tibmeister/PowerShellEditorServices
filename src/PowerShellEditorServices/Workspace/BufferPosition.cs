//
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
//

using System.Diagnostics;

namespace Microsoft.PowerShell.EditorServices
{
    /// <summary>
    /// Provides details about a position in a file buffer.  All
    /// positions are expressed in 1-based positions (i.e. the
    /// first line and column in the file is position 1,1).
    /// </summary>
    [DebuggerDisplay("Position = {Line}:{Character}")]
    public struct BufferPosition
    {
        public static readonly BufferPosition None = new BufferPosition(-1, -1);

        /// <summary>
        /// Gets the line number of the position in the buffer.
        /// </summary>
        public int Line { get; private set; }

        /// <summary>
        /// Gets the column number of the position in the buffer.
        /// </summary>
        public int Column { get; private set; }

        /// <summary>
        /// Creates a new instance of the BufferPosition class.
        /// </summary>
        /// <param name="line">The line number of the position.</param>
        /// <param name="column">The column number of the position.</param>
        public BufferPosition(int line, int column)
        {
            this.Line = line;
            this.Column = column;
        }
    }
}

