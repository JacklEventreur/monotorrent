/*
// KeepAliveMessage.cs
//
// Authors:
//   Alan McGovern alan.mcgovern@gmail.com
//
// Copyright (C) 2006 Alan McGovern
//
// Permission is hereby granted, free of charge, to any person obtaining
// a copy of this software and associated documentation files (the
// "Software"), to deal in the Software without restriction, including
// without limitation the rights to use, copy, modify, merge, publish,
// distribute, sublicense, and/or sell copies of the Software, and to
// permit persons to whom the Software is furnished to do so, subject to
// the following conditions:
// 
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
// OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
// WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
//
*/

namespace MonoTorrent.Client.Messages.Standard
{
    /// <summary>
    /// Represents a "KeepAlive" message
    /// </summary>
    public class KeepAliveMessage : PeerMessage
    {
        private const int messageLength = 0;   // has no payload
        internal static readonly byte MessageId = 0;
        private static readonly byte[] payload = new byte[] { 0, 0, 0, 0 };

        #region Constructors
        /// <summary>
        /// Creates a new KeepAliveMessage
        /// </summary>
        public KeepAliveMessage()
        {
        }
        #endregion


        #region Methods
        public override int Encode(byte[] buffer, int offset)
        {
            int written = offset;

            written += Write(buffer, written, payload);

            return CheckWritten(written - offset);
        }

        public override void Decode(byte[] buffer, int offset, int length)
        {
            // No decoding needed
        } 

        /// <summary>
        /// Returns the length of the message in bytes
        /// </summary>
        public override int ByteLength
        {
            get { return (4); }
        }
        #endregion


        #region Overridden Methods
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return "KeepAliveMessage";
        }

        public override bool Equals(object obj)
        {
            return (obj is KeepAliveMessage);
        }


        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }
        #endregion
    }
}