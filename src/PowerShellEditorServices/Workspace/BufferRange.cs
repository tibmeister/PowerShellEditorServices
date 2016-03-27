//
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
//

using System.Diagnostics;

namespace Microsoft.PowerShell.EditorServices
{
    /// <summary>
    /// Provides details about a range between two positions in
    /// a file buffer.
    /// </summary>
    [DebuggerDisplay("Start = {Start.Line}:{Start.Column}, End = {End.Line}:{End.Column}")]
    public struct BufferRange
    {
        public static readonly BufferRange None = new BufferRange();

        /// <summary>
        /// Gets the start position of the range in the buffer.
        /// </summary>
        public BufferPosition Start { get; private set; }

        /// <summary>
        /// Gets the end position of the range in the buffer.
        /// </summary>
        public BufferPosition End { get; private set; }

        /// <summary>
        /// Creates a new instance of the BufferRange class.
        /// </summary>
        /// <param name="start">The start position of the range.</param>
        /// <param name="end">The end position of the range.</param>
        public BufferRange(BufferPosition start, BufferPosition end)
        {
            this.Start = start;
            this.End = end;
        }

        public BufferRange(
            int startLine,
            int startColumn,
            int endLine,
            int endColumn)
        {
            this.Start = new BufferPosition(startLine, startColumn);
            this.End = new BufferPosition(endLine, endColumn);
        }
    }
}

