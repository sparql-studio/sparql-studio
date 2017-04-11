using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace dk.ModularSystems.Sparql.Ontology
{
    public interface IOntologyParser
    {
        Int32 Parse(String filePath, out OntologyDictionary ontology);
        Int32 Parse(Stream inputStream, out OntologyDictionary ontology);
    }
}
