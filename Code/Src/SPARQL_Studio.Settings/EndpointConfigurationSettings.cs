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
    public class EndpointConfigurationSettings : ConfigurationSection
    {
        [ConfigurationProperty("endpoints")]
        public EndpointElementCollection Endpoints
        {
            get { return this["endpoints"] as EndpointElementCollection; }
        }

        public EndpointConfigurationSettings() :
            base()
        { }

    }

    public class EndpointElementCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new EndpointElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((EndpointElement) element).EndpointUriString;
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
                { return "endpoint";}
            }
        }

        public EndpointElementCollection() :
            base()
        {
            
        }
    }

    public class EndpointElement : ConfigurationElement
    {
        [ConfigurationProperty("name", IsRequired = true)]
        public String Name
        {
            get { return (String)base["name"]; }
            set { base["name"] = value; }
        }

        [ConfigurationProperty("uri", IsRequired=true, IsKey = true)]
        public String EndpointUriString
        {
            get { return (String)base["uri"]; }
            set { base["uri"] = value; }
        }

        [ConfigurationProperty("description", IsRequired=false, DefaultValue = "")]
        public String Description
        {
            get { return (String)base["description"]; }
            set { base["description"] = value; }
        }

        [ConfigurationProperty("shortList", DefaultValue = true, IsRequired = false)]
        public Boolean ShortList
        {
            get { return (Boolean)base["shortList"]; }
            set { base["shortList"] = value; }
        }

        public EndpointElement()
        {
            
        }
    }

}
