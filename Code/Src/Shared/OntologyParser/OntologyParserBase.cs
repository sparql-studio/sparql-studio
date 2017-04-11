using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
