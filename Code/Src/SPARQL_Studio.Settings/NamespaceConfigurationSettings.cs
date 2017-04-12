// Author: Antonello Salvatucci as@modularsystems.dk
//
// This code is copyright (c) by Antonello Salvatucci and Modular Systems
// See the LICENSE file at the root of the repository for licensing details.
// 
// THIS CODE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED
// ----------------------------------------------------------------------------
//
using System;
using System.Configuration;

namespace dk.ModularSystems.Sparql.Settings
{
    public class NamespaceConfigurationSettings : ConfigurationSection
    {
        [ConfigurationProperty("namespaces")]
        public NamespaceElementCollection Namespaces
        {
            get { return this["namespaces"] as NamespaceElementCollection; }
        }
    }

    public class NamespaceElementCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new NamespaceElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((NamespaceElement)element).Name;
        }

        public override ConfigurationElementCollectionType CollectionType
        {
            get
            {
                return ConfigurationElementCollectionType.BasicMap;
            }
        }

        protected override String ElementName
        {
            get
            {
                { return "namespace"; }
            }
        }
    }

    public class NamespaceElement : ConfigurationElement
    {
        [ConfigurationProperty("uri", IsRequired = true)]
        public String NamespaceUriString
        {
            get { return (String)base["uri"]; }
            set { base["uri"] = value; }
        }

        [ConfigurationProperty("name", IsRequired = true)]
        public String Name
        {
            get { return (String)base["name"]; }
            set { base["name"] = value; }
        }

        [ConfigurationProperty("schemaPath", IsRequired = false, DefaultValue = "")]
        public String SchemaPath        
        {
            get { return (String)base["schemaPath"]; }
            set { base["schemaPath"] = value; }
        }

        [ConfigurationProperty("description", IsRequired = false, DefaultValue = "")]
        public String Description
        {
            get { return (String)base["description"]; }
            set { base["description"] = value; }
        }

        [ConfigurationProperty("autoPrefix", DefaultValue = true, IsRequired = false)]
        public Boolean AutoPrefix
        {
            get { return (Boolean)base["autoPrefix"]; }
            set { base["autoPrefix"] = value; }
        }

        [ConfigurationProperty("autoComplete", DefaultValue = true, IsRequired = false)]
        public Boolean AutoComplete
        {
            get { return (Boolean)base["autoComplete"]; }
            set { base["autoComplete"] = value; }
        }

    }

}
