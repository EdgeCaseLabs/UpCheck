using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Diagnostics;

namespace UpCheck
{
    /// <summary>
    /// Trace logging class use for application wide tracing.
    /// </summary>
    /// <remarks>
    /// IDs in use:
    /// 
    /// 10 - general info
    /// 50 - exceptions
    /// </remarks>
    public class Logging
    {
        private static TraceSource _default = new TraceSource("Default", SourceLevels.All);


        public static void TraceEvent(string message)
        {
            TraceEvent(TraceEventType.Information, 10, "{0}", message);
        }

        public static void TraceEvent(string message, params object[] args)
        {
            TraceEvent(TraceEventType.Information, 10, message, args);
        }

        public static void TraceEvent(TraceEventType eventType, int id, string message)
        {
            TraceEvent(eventType, id, "{0}", message);
        }

        public static void TraceEvent(TraceEventType eventType, int id, string format, params object[] args)
        {
            _default.TraceEvent(eventType, id, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "; " + format, args);
        }
    }
}