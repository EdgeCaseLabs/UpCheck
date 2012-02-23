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
                RemoteServiceChecker.Validate();
                
                context.Response.ContentType = "text/plain";
                context.Response.Write("OK: Everything's peachy!");

            }
            catch (Exception ex)
            {
                
                if (HttpContext.Current.IsDebuggingEnabled)
                {
                    Logging.TraceEvent(TraceEventType.Error, 50, "{0}\n{1}", ex.Message, ex.StackTrace);
                    context.AddError(ex);
                }
                else
                {
                    Logging.TraceEvent(TraceEventType.Error, 50, "{0}", ex.Message);
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

}