// Author: Antonello Salvatucci as@modularsystems.dk
//
// This code is copyright (c) by Antonello Salvatucci and Modular Systems
// See the LICENSE file at the root of the repository for licensing details.
// 
// THIS CODE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED
// ----------------------------------------------------------------------------
//
using System;
using System.Web;

namespace dk.ModularSystems.Sparql.Ontology
{
    public enum RdfElementType
    {
        Class,
        Property
    }

    public class OntologyElement
    {
        // TODO: Support descriptions and labels in multiple languages.
        //       For now only English.
        //
        public String           Uri { get; protected set; }
        public String           Label { get; set; }
        public String           Description { get; set; }
        public RdfElementType   ElementType { get; set; }
        public String           Name { get; protected set; }
        public String Namespace { get; protected set; }

        public static Char[] NameSeparators = new[] {'#', '\\', '/'};

        public OntologyElement(RdfElementType elementType, String uri, String label = "", String description = "")
        {

            ElementType = elementType;
            Uri = uri;
            Label = label;
            Description = description;
            Int32 i = uri.LastIndexOfAny(NameSeparators);
            Name = uri.Substring(i + 1);
            Name = HttpUtility.UrlDecode(Name);
            Namespace = uri.Substring(0, i + 1);
        }
    }
}
