using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Diagnostics;
using System.Configuration;
using System.Data.SqlClient;

namespace UpCheck
{
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
}