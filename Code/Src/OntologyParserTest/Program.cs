// Author: Antonello Salvatucci as@modularsystems.dk
//
// This code is copyright (c) by Antonello Salvatucci and Modular Systems
// See the LICENSE file at the root of the repository for licensing details.
// 
// THIS CODE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED
// ----------------------------------------------------------------------------
//
using System;
using dk.ModularSystems.Sparql.Ontology;


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
