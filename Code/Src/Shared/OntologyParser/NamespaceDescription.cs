// Author: Antonello Salvatucci as@modularsystems.dk
//
// This code is copyright (c) by Antonello Salvatucci and Modular Systems
// See the LICENSE file at the root of the repository for licensing details.
// 
// THIS CODE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED
// ----------------------------------------------------------------------------
//
using System;

namespace dk.ModularSystems.Sparql.Ontology
{
    public class NamespaceDescription
    {
        public String NamespaceName { get; protected set; }
        public String NamespacePrefix { get; protected set; }

        public NamespaceDescription(String ns, String prefix)
        {
            NamespaceName = ns;
            NamespacePrefix = prefix;
        }
    }
}
