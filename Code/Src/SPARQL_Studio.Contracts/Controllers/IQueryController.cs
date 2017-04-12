// Author: Antonello Salvatucci as@modularsystems.dk
//
// This code is copyright (c) by Antonello Salvatucci and Modular Systems
// See the LICENSE file at the root of the repository for licensing details.
// 
// THIS CODE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED
// ----------------------------------------------------------------------------
//
using System;
using System.Collections.Generic;
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
