using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using dk.ModularSystems.Sparql.Ontology;
using VDS.RDF.Query;

namespace dk.ModularSystems.Sparql.Contracts
{
    public interface IQueryController : IController
    {
        IEnumerable<OntologyDictionary> Ontologies { get; }

        void OnSubmitQuery(IQueryView sender, String queryString);
        void OnSubmitQuery(IQueryView sender, SparqlQuery query);
        void OnParseQuery(IQueryView sender);
        void OnSaveQuery(IQueryView sender);
        void OnSaveQueryAs(IQueryView sender);
    }
}
