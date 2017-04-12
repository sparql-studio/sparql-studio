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
