using System.IO;
using System.Reflection;
using DigitalRune.Windows.TextEditor.Completion;
using DigitalRune.Windows.TextEditor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DigitalRune.Windows.TextEditor.Document;
using DigitalRune.Windows.TextEditor.Highlighting;
using dk.ModularSystems.Sparql.Ontology;

namespace dk.ModularSystems.Sparql.SparqlAutoCompletion
{
    public class SparqlCompletionDataProvider : AbstractCompletionDataProvider
    {
        public static String[] Keywords;
        public static IEnumerable<OntologyDictionary> Ontologies { get; set; }

        public SparqlCompletionDataProvider() :
            base()
        {
            _images = new ImageList();

            // Remove the : from the commit keys
            // Otherwise the colon will cause whatever is selected in the autocomplete window
            // to be committed
            //
            List<Char> commitKeys = base.CommitKeys.ToList();
            commitKeys.Remove(':');
            base.CommitKeys = commitKeys.ToArray();

            // Load keyword list if needed
            //
            if (Keywords == null)
            {
                String assemblyFolder = Path.GetDirectoryName(Assembly.GetAssembly(this.GetType()).Location);
                Keywords = File.ReadAllLines(Path.Combine(assemblyFolder, "SparqlAutoCompletion\\Keywords.txt"));
            }
        }

        public override ICompletionData[] GenerateCompletionData(string fileName, TextArea textArea, char charTyped)
        {
            if (ImageList.Images.Count == 0)
            {
                ImageList.Images.Add(AutoCompleteImages.Keyword);
                ImageList.Images.Add(AutoCompleteImages.Rdf);
            }
            var completionData = new List<ICompletionData>();
            foreach (String keyword in Keywords)
            {
                completionData.Add(new DefaultCompletionData(keyword, keyword, 0)); // TODO: Make imagelist indexes into constants
            }

            foreach (OntologyDictionary ontology in Ontologies)
            {
                foreach (OntologyElement item in ontology.Values)
                {
                    NamespaceDescription nsdesc = ontology.NamespaceDescriptions.Find(nd => { return nd.NamespaceName == item.Namespace; });
                    if (nsdesc != null)
                    {
                        completionData.Add(new DefaultCompletionData(nsdesc.NamespacePrefix + ":" + item.Name, item.Label, 1)); // TODO: Make imagelist indexes into constants
                    }
                }
            }
            return completionData.ToArray();
        }

        protected ImageList _images;
        public override System.Windows.Forms.ImageList ImageList
        {
            get { return _images; }
        }

        public override bool InsertAction(ICompletionData data, DigitalRune.Windows.TextEditor.TextArea textArea, int insertionOffset, char key)
        {
            return base.InsertAction(data, textArea, insertionOffset, key);
        }

        public override CompletionDataProviderKeyResult ProcessKey(char key)
        {
            return base.ProcessKey(key);
        }
    }
}
