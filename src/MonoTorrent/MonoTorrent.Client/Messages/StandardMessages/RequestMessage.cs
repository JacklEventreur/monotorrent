//
// RequestMessage.cs
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

using System.Text;

namespace MonoTorrent.Client.Messages.Standard
{
    public class RequestMessage : PeerMessage
    {
        internal static readonly byte MessageId = 6;
        private const int messageLength = 13;

        internal const int MaxSize = 65536 + 64;
        internal const int MinSize = 4096;

        #region Private Fields
        private int startOffset;
        private int pieceIndex;
        private int requestLength;
        #endregion


        #region Public Properties

        public override int ByteLength
        {
            get { return (messageLength + 4); }
        }

        public int StartOffset
        {
            get { return startOffset; }
        }

        public int PieceIndex
        {
            get { return pieceIndex; }
        }

        public int RequestLength
        {
            get { return requestLength; }
        }

        #endregion


        #region Constructors

        public RequestMessage()
        {
        }

        public RequestMessage(int pieceIndex, int startOffset, int requestLength)
        {
            this.pieceIndex = pieceIndex;
            this.startOffset = startOffset;
            this.requestLength = requestLength;
        }

        #endregion


        #region Methods

        public override void Decode(byte[] buffer, int offset, int length)
        {
            pieceIndex = ReadInt(buffer, ref offset);
            startOffset = ReadInt(buffer, ref offset);
            requestLength = ReadInt(buffer, ref offset);
        }

        public override int Encode(byte[] buffer, int offset)
        {
            int written = offset;
            
            written += Write(buffer, written, messageLength);
            written += Write(buffer, written, MessageId);
            written += Write(buffer, written, pieceIndex);
            written += Write(buffer, written, startOffset);
            written += Write(buffer, written, requestLength);

            return CheckWritten(written - offset);
        }

        public override bool Equals(object obj)
        {
            RequestMessage msg = obj as RequestMessage;
            return (msg != null) && (pieceIndex == msg.pieceIndex
                                      && startOffset == msg.startOffset
                                      && requestLength == msg.requestLength);
        }

        public override int GetHashCode()
        {
            return (pieceIndex.GetHashCode() ^ requestLength.GetHashCode() ^ startOffset.GetHashCode());
        }

        public override string ToString()
        {

            StringBuilder sb = new StringBuilder();
            sb.Append("RequestMessage ");
            sb.Append(" Index ");
            sb.Append(pieceIndex);
            sb.Append(" Offset ");
            sb.Append(startOffset);
            sb.Append(" Length ");
            sb.Append(requestLength);
            return sb.ToString();
        }

        #endregion
    }
}