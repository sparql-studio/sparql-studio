// Author: Antonello Salvatucci as@modularsystems.dk
//
// This code is copyright (c) by Antonello Salvatucci and Modular Systems
// See the LICENSE file at the root of the repository for licensing details.
// 
// THIS CODE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED
// ----------------------------------------------------------------------------
//

using System;
using System.IO;

namespace dk.ModularSystems.Sparql.Ontology
{
    public interface IOntologyParser
    {
        Int32 Parse(String filePath, out OntologyDictionary ontology);
        Int32 Parse(Stream inputStream, out OntologyDictionary ontology);
    }
}
