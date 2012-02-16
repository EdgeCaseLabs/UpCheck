using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Management;
using System.Diagnostics;
using System.Configuration;

namespace UpCheck
{
    public class RemoteServiceChecker
    {
        public static void Validate()
        {
            Logging.TraceEvent(TraceEventType.Information, 10, "BEGIN RemoteServiceChecker Validation");
            for (int i = 0; i < SettingsManager.RemoteServices.Count; i++)
            {
                Logging.TraceEvent(TraceEventType.Information, 10, "RemoteServiceChecker: {0}", SettingsManager.RemoteServices[i].Name);
                
                checkService(SettingsManager.RemoteServices[i]);
            }
            Logging.TraceEvent(TraceEventType.Information, 10, "END RemoteServiceChecker Validation");
        }

        private static void checkService(RemoteServiceSettings settings)
        {
            var options = new ConnectionOptions();
            options.Username = settings.Username;
            options.Password = settings.Password;
            options.Authority = settings.Authority;
            
            var scope = new ManagementScope(settings.Path, options);
            //var scope = new ManagementScope();

            scope.Connect();

            ObjectQuery query = new ObjectQuery(string.Format("SELECT * FROM Win32_Service WHERE Name = '{0}'", settings.Name));

            using (var searcher = new ManagementObjectSearcher(scope, query))
            {
                bool found = false;
                foreach (ManagementObject queryObj in searcher.Get())
                {
                    found = true;
                    if (settings.State.ToLower() != queryObj["State"].ToString().ToLower())
                    {
                        throw new Exception(string.Format("Service {0} was not in expected state {1}; actual state: {2}", settings.Name, settings.State, queryObj["State"]));
                    }
                    else
                    {
                        Logging.TraceEvent("Service {0} was in expected state {1}", settings.Name, settings.State);
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
                    Logging.TraceEvent("Unable to find service {0}", settings.Name);
                    throw new Exception(string.Format("Unable to find service {0}", settings.Name));
                }
            }
        }



    }
}