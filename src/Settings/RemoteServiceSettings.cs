using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Collections;
using System.ComponentModel;

namespace UpCheck
{

    public sealed class RemoteServicesSection : ConfigurationSection
    {
        private static readonly ConfigurationProperty _propRemoteServices = new ConfigurationProperty((string)null, typeof(RemoteServicesSettingsCollection), (object)null, ConfigurationPropertyOptions.IsDefaultCollection);
        private static ConfigurationPropertyCollection _properties = new ConfigurationPropertyCollection();

        protected override ConfigurationPropertyCollection Properties
        {
            get
            {
                return RemoteServicesSection._properties;
            }
        }

        [ConfigurationProperty("", Options = ConfigurationPropertyOptions.IsDefaultCollection)]
        public RemoteServicesSettingsCollection RemoteServices
        {
            get
            {
                return (RemoteServicesSettingsCollection)this[RemoteServicesSection._propRemoteServices];
            }
        }

        static RemoteServicesSection()
        {
            RemoteServicesSection._properties.Add(RemoteServicesSection._propRemoteServices);
        }

        public RemoteServicesSection()
        {
        }

        protected override object GetRuntimeObject()
        {
            this.SetReadOnly();
            return (object)this;
        }
    }


    [ConfigurationCollection(typeof(RemoteServiceSettings))]
    public sealed class RemoteServicesSettingsCollection : ConfigurationElementCollection
    {
        private static ConfigurationPropertyCollection _properties = new ConfigurationPropertyCollection();

        protected override ConfigurationPropertyCollection Properties
        {
            get
            {
                return RemoteServicesSettingsCollection._properties;
            }
        }

        public RemoteServiceSettings this[int index]
        {
            get
            {
                return (RemoteServiceSettings)this.BaseGet(index);
            }
            set
            {
                if (this.BaseGet(index) != null)
                    this.BaseRemoveAt(index);
                this.BaseAdd(index, (ConfigurationElement)value);
            }
        }

        public new RemoteServiceSettings this[string name]
        {
            get
            {
                return (RemoteServiceSettings)this.BaseGet((object)name);
            }
        }

        static RemoteServicesSettingsCollection()
        {
        }

        public RemoteServicesSettingsCollection()
            : base((IComparer)StringComparer.OrdinalIgnoreCase)
        {
        }

        public int IndexOf(RemoteServiceSettings settings)
        {
            return this.BaseIndexOf((ConfigurationElement)settings);
        }

        protected override void BaseAdd(int index, ConfigurationElement element)
        {
            if (index == -1)
                base.BaseAdd(element, false);
            else
                base.BaseAdd(index, element);
        }

        public void Add(RemoteServiceSettings settings)
        {
            this.BaseAdd((ConfigurationElement)settings);
        }

        public void Remove(RemoteServiceSettings settings)
        {
            if (this.BaseIndexOf((ConfigurationElement)settings) < 0)
                return;
            this.BaseRemove((object)settings.Key);
        }

        public void RemoveAt(int index)
        {
            this.BaseRemoveAt(index);
        }

        public void Remove(string name)
        {
            this.BaseRemove((object)name);
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return (ConfigurationElement)new RemoteServiceSettings();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return (object)((RemoteServiceSettings)element).Key;
        }

        public void Clear()
        {
            this.BaseClear();
        }
    }

    public sealed class RemoteServiceSettings : ConfigurationElement
    {
        //private static readonly ConfigurationProperty _propName = new ConfigurationProperty("name", typeof(string), (object)null);//, (TypeConverter)null, ConfigurationProperty.NonEmptyStringValidator, ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey);
        private static readonly ConfigurationProperty _propName = new ConfigurationProperty("name", typeof(string), (object)string.Empty, ConfigurationPropertyOptions.IsRequired);
        private static readonly ConfigurationProperty _propUsername = new ConfigurationProperty("username", typeof(string), (object)string.Empty, ConfigurationPropertyOptions.IsRequired);
        private static readonly ConfigurationProperty _propPassword = new ConfigurationProperty("password", typeof(string), (object)string.Empty, ConfigurationPropertyOptions.IsRequired);
        private static readonly ConfigurationProperty _propAuthority = new ConfigurationProperty("authority", typeof(string), (object)string.Empty, ConfigurationPropertyOptions.IsRequired);
        private static readonly ConfigurationProperty _propPath = new ConfigurationProperty("path", typeof(string), (object)string.Empty, ConfigurationPropertyOptions.IsRequired);
        private static readonly ConfigurationProperty _propState = new ConfigurationProperty("state", typeof(string), (object)string.Empty, ConfigurationPropertyOptions.IsRequired);
        private static ConfigurationPropertyCollection _properties = new ConfigurationPropertyCollection();

        internal string Key
        {
            get
            {
                return this.Name;
            }
        }

        protected override ConfigurationPropertyCollection Properties
        {
            get
            {
                return RemoteServiceSettings._properties;
            }
        }


        [ConfigurationProperty("name", DefaultValue = "", Options = ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey)]
        public string Name
        {
            get
            {
                return (string)this[RemoteServiceSettings._propName];
            }
            set
            {
                this[RemoteServiceSettings._propName] = (object)value;
            }
        }


        [ConfigurationProperty("username", DefaultValue = "", Options = ConfigurationPropertyOptions.IsRequired)]
        public string Username
        {
            get
            {
                return (string)this[RemoteServiceSettings._propUsername];
            }
            set
            {
                this[RemoteServiceSettings._propUsername] = (object)value;
            }
        }


        [ConfigurationProperty("password", DefaultValue = "", Options = ConfigurationPropertyOptions.IsRequired)]
        public string Password
        {
            get
            {
                return (string)this[RemoteServiceSettings._propPassword];
            }
            set
            {
                this[RemoteServiceSettings._propPassword] = (object)value;
            }
        }

        [ConfigurationProperty("authority", DefaultValue = "", Options = ConfigurationPropertyOptions.IsRequired)]
        public string Authority
        {
            get
            {
                return (string)this[RemoteServiceSettings._propAuthority];
            }
            set
            {
                this[RemoteServiceSettings._propAuthority] = (object)value;
            }
        }

        [ConfigurationProperty("path", DefaultValue = "", Options = ConfigurationPropertyOptions.IsRequired)]
        public string Path
        {
            get
            {
                return (string)this[RemoteServiceSettings._propPath];
            }
            set
            {
                this[RemoteServiceSettings._propPath] = (object)value;
            }
        }

        [ConfigurationProperty("state", DefaultValue = "", Options = ConfigurationPropertyOptions.IsRequired)]
        public string State
        {
            get
            {
                return (string)this[RemoteServiceSettings._propState];
            }
            set
            {
                this[RemoteServiceSettings._propState] = (object)value;
            }
        }

        static RemoteServiceSettings()
        {
            RemoteServiceSettings._properties.Add(RemoteServiceSettings._propName);
            RemoteServiceSettings._properties.Add(RemoteServiceSettings._propUsername);
            RemoteServiceSettings._properties.Add(RemoteServiceSettings._propPassword);
            RemoteServiceSettings._properties.Add(RemoteServiceSettings._propPath);
            RemoteServiceSettings._properties.Add(RemoteServiceSettings._propState);
            RemoteServiceSettings._properties.Add(RemoteServiceSettings._propAuthority);
        }

        public RemoteServiceSettings()
        {
        }

        public override string ToString()
        {
            return this.Name;
        }
    }
}