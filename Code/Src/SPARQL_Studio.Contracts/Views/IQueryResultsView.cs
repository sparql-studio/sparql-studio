// Author: Antonello Salvatucci as@modularsystems.dk
//
// This code is copyright (c) by Antonello Salvatucci and Modular Systems
// See the LICENSE file at the root of the repository for licensing details.
// 
// THIS CODE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED
// ----------------------------------------------------------------------------
//
using VDS.RDF.Query;

namespace dk.ModularSystems.Sparql.Contracts
{
    public interface IQueryResultsView : IView
    {
        void SetQueryResults(SparqlResultSet results);
        void SetQueryInfo(SparqlQuery query, SparqlRemoteEndpoint endpoint);
    }
}
