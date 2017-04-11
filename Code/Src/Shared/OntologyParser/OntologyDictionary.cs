using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dk.ModularSystems.Sparql.Ontology
{
    public class OntologyDictionary : Dictionary<String, OntologyElement>
    {
        public OntologyDescription Description { get; set; }
        public List<NamespaceDescription> NamespaceDescriptions { get; set; }

        public OntologyDictionary() :
            base()
        {
            Description = new OntologyDescription();
            NamespaceDescriptions = new List<NamespaceDescription>();
        }
    }
}
