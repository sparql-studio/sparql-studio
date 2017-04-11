using System;
using dk.ModularSystems.Sparql.Ontology;
using VDS.RDF.Parsing;


namespace OntologyParserTest
{
    class Program
    {
        static void Main(string[] args)
        {
            //String filePath = @"..\..\..\SPARQL_Studio\Ontologies\skos.rdf.xml";
            // String filePath = @"..\..\..\SPARQL_Studio\Ontologies\rdf.rdf.xml";
            // String filePath = @"..\..\..\SPARQL_Studio\Ontologies\dbo.rdf.xml";
            //String filePath = @"..\..\..\SPARQL_Studio\Ontologies\foaf.rdf.xml";
            String filePath = @"..\..\..\SPARQL_Studio\Ontologies\dbprop.ttl";
            //String filePath = @"..\..\..\SPARQL_Studio\Ontologies\dcterms.ttl";

            // var ontologyParser = new OntologyParserRdfXml();
            var ontologyParser = new OntologyParserTtl();
            OntologyDictionary ontology;
            Int32 foundElements = ontologyParser.Parse(filePath, out ontology);

            foreach (OntologyElement item in ontology.Values)
            {
                Console.WriteLine("{0,-8} {1,-20} {2}", item.ElementType, item.Name, item.Label);
            }
            Console.WriteLine("done :-)");
            Console.WriteLine("Found {0} elements", foundElements );
            Console.ReadKey(intercept: true);
        }

    }
}
