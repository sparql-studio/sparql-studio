// Author: Antonello Salvatucci as@modularsystems.dk
//
// This code is copyright (c) by Antonello Salvatucci and Modular Systems
// See the LICENSE file at the root of the repository for licensing details.
// 
// THIS CODE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED
// ----------------------------------------------------------------------------
//
using System;
using VDS.RDF;
using VDS.RDF.Parsing.Handlers;

namespace dk.ModularSystems.Sparql.Ontology
{
    internal class OntologyDictionaryRdfHandler : BaseRdfHandler
    {
        private readonly OntologyDictionary _ontology;

        public OntologyDictionaryRdfHandler(OntologyDictionary ontology)
        {
            _ontology = ontology;
        }

        public override bool AcceptsAll
        {
            get { return true; }
        }

        protected override Boolean HandleNamespaceInternal(String prefix, Uri namespaceUri)
        {
            _ontology.NamespaceDescriptions.Add(new NamespaceDescription(namespaceUri.ToString(), prefix));
            return base.HandleNamespaceInternal(prefix,namespaceUri);
        }

        protected override bool HandleTripleInternal(Triple t)
        {
            String subject = t.Subject.ToString();
            if (_ontology.ContainsKey(subject) == false)
            {
                _ontology.Add(subject, new OntologyElement(RdfElementType.Property, subject));
                if (_ontology.Description.Namespace == null)
                {
                    _ontology.Description.Namespace = _ontology[subject].Namespace;
                }
            }
            if (t.Predicate.ToString().ToLower() == "http://www.w3.org/2000/01/rdf-schema#label")
            {
                _ontology[subject].Label = t.Object.ToString();
            }
            else if (t.Predicate.ToString().ToLower() == "http://www.w3.org/2000/01/rdf-schema#comment")
            {
                _ontology[subject].Description = t.Object.ToString();
            }
            else if (t.Predicate.ToString().ToLower() == "http://purl.org/dc/terms/title")
            {
                _ontology.Description.Title = t.Object.ToString();
            }
            return true;
        }
    }

}
