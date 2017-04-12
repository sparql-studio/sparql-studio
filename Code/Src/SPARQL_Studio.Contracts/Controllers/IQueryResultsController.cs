// Author: Antonello Salvatucci as@modularsystems.dk
//
// This code is copyright (c) by Antonello Salvatucci and Modular Systems
// See the LICENSE file at the root of the repository for licensing details.
// 
// THIS CODE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED
// ----------------------------------------------------------------------------
//
using System;
using VDS.RDF.Query;

namespace dk.ModularSystems.Sparql.Contracts
{
    public interface IQueryResultsController : IController
    {
        void OnResultSelected(SparqlResult result, Object field, String fieldName);
    }
}
