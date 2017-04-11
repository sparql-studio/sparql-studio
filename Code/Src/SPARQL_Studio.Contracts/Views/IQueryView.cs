using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dk.ModularSystems.Sparql.Ontology;
using VDS.RDF.Query;

namespace dk.ModularSystems.Sparql.Contracts
{
    public interface IQueryView : IView
    {
        SparqlQuery ParseQuery();
        void SetQuery(SparqlQuery query);
        void SetQueryText(String queryText);
        void SaveQuery(String pathName);
        void OpenQuery(String pathName);

        Boolean AutocompleteEnabled { get; set; }
        Boolean QueryValidationEnabled { get; set; }
        String QueryPathName { get; }
        OntologyDictionary Ontologies { get; set; }
    }
}
