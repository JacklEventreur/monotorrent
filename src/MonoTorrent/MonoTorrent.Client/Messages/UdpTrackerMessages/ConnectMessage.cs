//
// ConnectMessage.cs
//
// Authors:
//   Alan McGovern alan.mcgovern@gmail.com
//
// Copyright (C) 2008 Alan McGovern
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



using System;
using System.Net;

namespace MonoTorrent.Client.Messages.UdpTracker
{
    public class ConnectMessage : UdpTrackerMessage
    {
        private long connectionId;

        public long ConnectionId
        {
            get { return connectionId; }
        }

        public ConnectMessage() : base(0, DateTime.Now.GetHashCode())
        {
            connectionId = IPAddress.NetworkToHostOrder(0x41727101980); // Init connectionId as per spec
        }

        public override int ByteLength
        {
            get { return 8 + 4 + 4; }
        }

        public override void Decode(byte[] buffer, int offset, int length)
        {
            connectionId = ReadLong(buffer, ref offset);
            if (Action != ReadInt(buffer, ref offset))
                ThrowInvalidActionException();
            TransactionId = ReadInt(buffer, ref offset);
        }

        public override int Encode(byte[] buffer, int offset)
        {
            int written = offset;

            written += Write(buffer, written, connectionId);
            written += Write(buffer, written, Action);
            written += Write(buffer, written, TransactionId);

            return written - offset;
        }
    }
}
