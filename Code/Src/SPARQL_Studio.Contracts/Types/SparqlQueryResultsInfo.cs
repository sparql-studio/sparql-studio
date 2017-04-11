using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using VDS.RDF.Query;

namespace dk.ModularSystems.Sparql.Contracts
{
    public class SparqlQueryResultsInfo
    {
        public SparqlQuery              Query { get; protected set; }
        public SparqlResultSet          QueryResults { get; protected set; }
        public SparqlRemoteEndpoint     Endpoint { get; protected set; }

        public SparqlQueryResultsInfo(SparqlQuery query, SparqlResultSet results, SparqlRemoteEndpoint endpoint= null)
        {
            Query = query;
            QueryResults = results;
            Endpoint = endpoint;
        }

    }
}
