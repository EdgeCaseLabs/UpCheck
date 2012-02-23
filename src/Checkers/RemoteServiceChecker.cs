using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Management;
using System.Diagnostics;
using System.Configuration;

namespace UpCheck
{
    /// <summary>
    /// Loop through all defined remote services and validate that the state matches what is expected.
    /// </summary>
    public class RemoteServiceChecker
    {
        public static void Validate()
        {
            Logging.TraceEvent(TraceEventType.Information, 10, "BEGIN RemoteServiceChecker Validation");
            for (int i = 0; i < SettingsManager.RemoteServices.Count; i++)
            {
                Logging.TraceEvent(TraceEventType.Information, 10, "RemoteServiceChecker: {0}", SettingsManager.RemoteServices[i].ServiceName);
                
                checkService(SettingsManager.RemoteServices[i]);
            }
            Logging.TraceEvent(TraceEventType.Information, 10, "END RemoteServiceChecker Validation");
        }

        private static void checkService(RemoteServiceSettings settings)
        {
            ManagementScope scope = null;
            if (settings.Path != null && settings.Path.Trim().Length > 0)
            {
                ConnectionOptions options = null;
                if (settings.Username != null && settings.Username.Trim().Length > 0)
                {
                    options = new ConnectionOptions();
                    options.Username = settings.Username;
                    options.Password = settings.Password;
                    if (settings.Authority.Trim().Length > 0)
                    {
                        options.Authority = settings.Authority;
                    }
                }

                scope = new ManagementScope(settings.Path, options);
            }
            else
            {
                scope = new ManagementScope();
            }

            scope.Connect();

            ObjectQuery query = new ObjectQuery(string.Format("SELECT * FROM Win32_Service WHERE Name = '{0}'", settings.ServiceName));

            using (var searcher = new ManagementObjectSearcher(scope, query))
            {
                bool found = false;
                foreach (ManagementObject queryObj in searcher.Get())
                {
                    found = true;
                    if (settings.State.ToLower() != queryObj["State"].ToString().ToLower())
                    {
                        throw new Exception(string.Format("Service {0} was not in expected state {1}; actual state: {2}", settings.ServiceName, settings.State, queryObj["State"]));
                    }
                    else
                    {
                        Logging.TraceEvent("Service {0} was in expected state {1}", settings.ServiceName, settings.State);
                    }
                    //Logging.TraceEvent(string.Format("Caption: {0}", queryObj["Caption"]));
                    //Logging.TraceEvent(string.Format("Description: {0}", queryObj["Description"]));
                    //Logging.TraceEvent(string.Format("Name: {0}", queryObj["Name"]));
                    //Logging.TraceEvent(string.Format("PathName: {0}", queryObj["PathName"]));
                    //Logging.TraceEvent(string.Format("State: {0}", queryObj["State"]));
                    //Logging.TraceEvent(string.Format("Status: {0}", queryObj["Status"]));
                }
                if (!found)
                {
                    Logging.TraceEvent("Unable to find service {0}", settings.ServiceName);
                    throw new Exception(string.Format("Unable to find service {0}", settings.ServiceName));
                }
            }
        }



    }
}