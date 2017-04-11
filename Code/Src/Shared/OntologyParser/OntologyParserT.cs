using System;
using System.IO;
using VDS.RDF;

namespace dk.ModularSystems.Sparql.Ontology
{

    public class OntologyParser<T> : OntologyParserBase
        where T : IRdfReader,
              new()
    {
        public override Int32 Parse(System.IO.Stream inputStream, out OntologyDictionary ontology)
        {
            ontology = new OntologyDictionary();

            IRdfReader parser = new T();
            OntologyDictionaryRdfHandler handler = new OntologyDictionaryRdfHandler(ontology);

            using (StreamReader reader = new StreamReader(inputStream))
            {
                parser.Load(handler, reader);
            }

            return ontology.Count;
        }
    }
}
