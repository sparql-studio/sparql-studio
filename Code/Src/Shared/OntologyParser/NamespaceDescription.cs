using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dk.ModularSystems.Sparql.Ontology
{
    public class NamespaceDescription
    {
        public String NamespaceName { get; protected set; }
        public String NamespacePrefix { get; protected set; }

        public NamespaceDescription(String ns, String prefix)
        {
            NamespaceName = ns;
            NamespacePrefix = prefix;
        }
    }
}
