using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;

namespace UpCheck
{
    public class SettingsManager
    {
        public static RemoteServicesSettingsCollection RemoteServices
        {
            get
            {
                object section = ConfigurationManager.GetSection("remoteServices");
                if (section == null || section.GetType() != typeof(RemoteServicesSection))
                    throw new ConfigurationErrorsException("Invalid remoteServices sections");
                else
                    return ((RemoteServicesSection)section).RemoteServices;
            }
        }
    }
}