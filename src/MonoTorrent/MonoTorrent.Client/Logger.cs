using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using MonoTorrent.Client.Connections;

namespace MonoTorrent.Client
{
    public static class Logger
    {
        private static List<TraceListener> listeners;

        static Logger()
        {
            listeners = new List<TraceListener>();
        }

        public static void AddListener(TraceListener listener)
        {
            if (listener == null)
                throw new ArgumentNullException("listener");

            lock (listeners)
                listeners.Add(listener);
        }
        
        public static void Flush()
        {
            lock (listeners)
                listeners.ForEach (delegate (TraceListener l) { l.Flush(); } );
        }

        [Conditional("DO_NOT_ENABLE")]
        internal static void Log(IConnection connection, string message)
        {
            Log(connection, message, null);
        }

        private static StringBuilder sb = new StringBuilder();
        [Conditional("DO_NOT_ENABLE")]
        internal static void Log(IConnection connection, string message, params object[] formatting)
        {
            lock (listeners)
            {
                sb.Remove(0, sb.Length);
                sb.Append(Environment.TickCount);
                sb.Append(": ");

                if (connection != null)
                    sb.Append(connection.EndPoint.ToString());

                if (formatting != null)
                    sb.Append(string.Format(message, formatting));
                else
                    sb.Append(message);
                string s = sb.ToString();
                listeners.ForEach(delegate(TraceListener l) { l.WriteLine(s); });
            }
        }
    }
}
