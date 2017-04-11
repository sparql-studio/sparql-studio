using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VDS.RDF.Query;

namespace dk.ModularSystems.Sparql.Contracts
{
    public interface IQueryResultsController : IController
    {
        void OnResultSelected(SparqlResult result, Object field, String fieldName);
    }
}
