<%@ WebHandler Language="C#" Class="UpCheck.Up" %>

using System;
using System.Collections.Generic;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;
using System.Diagnostics;

namespace UpCheck
{
    public class Up : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            try
            {
                DatabaseChecker.Validate();

                context.Response.ContentType = "text/plain";
                context.Response.Write("OK: Everything's peachy!");

            }
            catch (Exception ex)
            {
                Logging.TraceEvent(TraceEventType.Error, 50, "{0}\n{1}", ex.Message, ex.StackTrace);
                if (HttpContext.Current.IsDebuggingEnabled)
                {
                    context.AddError(ex);
                }
                else
                {
                    context.AddError(new Exception("ERROR: Bummer. Nothing good."));
                }
            }
            
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }


    public class DatabaseChecker
    {
        /// <summary>
        /// Loop through all connection strings and validate that a connection can be opened.
        /// </summary>
        public static void Validate()
        {
            Logging.TraceEvent(TraceEventType.Information, 10, "BEGIN DatabaseChcker Validation");
            for (int i = 0; i < ConfigurationManager.ConnectionStrings.Count; i++)
            {
                Logging.TraceEvent(TraceEventType.Information, 10, "DatabaseChcker: {0}", ConfigurationManager.ConnectionStrings[i].Name);
                var conn = new SqlConnection(ConfigurationManager.ConnectionStrings[i].ConnectionString);
                try
                {
                    conn.Open();
                    Logging.TraceEvent(TraceEventType.Information, 10, "{0}: opened", ConfigurationManager.ConnectionStrings[i].Name);
                }
                finally
                {
                    if (conn.State == System.Data.ConnectionState.Open)
                    {
                        conn.Close();
                        conn.Dispose();
                        Logging.TraceEvent(TraceEventType.Information, 10, "{0}: closed", ConfigurationManager.ConnectionStrings[i].Name);
                    }
                }
            }
            Logging.TraceEvent(TraceEventType.Information, 10, "END DatabaseChcker Validation");
        }
    }

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