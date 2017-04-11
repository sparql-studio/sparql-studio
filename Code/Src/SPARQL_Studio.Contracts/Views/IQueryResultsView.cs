using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VDS.RDF.Query;

namespace dk.ModularSystems.Sparql.Contracts
{
    public interface IQueryResultsView : IView
    {
        void SetQueryResults(SparqlResultSet results);
        void SetQueryInfo(SparqlQuery query, SparqlRemoteEndpoint endpoint);
    }
}
