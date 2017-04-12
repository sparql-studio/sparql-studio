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
    public abstract class OntologyParserBase : IOntologyParser
    {
        public Int32 Parse(String filePath, out OntologyDictionary ontology)
        {
            if (File.Exists(filePath) == false)
            {
                ontology = null;
                return 0;
            }
            using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                return Parse(fs, out ontology);
            }
        }

        public abstract int Parse(System.IO.Stream inputStream, out OntologyDictionary ontology);
    }
}
