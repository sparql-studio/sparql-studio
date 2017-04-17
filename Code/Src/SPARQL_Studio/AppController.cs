// Author: Antonello Salvatucci as@modularsystems.dk
//
// This code is copyright (c) by Antonello Salvatucci and Modular Systems
// See the LICENSE file at the root of the repository for licensing details.
// 
// THIS CODE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED
// ----------------------------------------------------------------------------
//
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using dk.ModularSystems.Sparql.Contracts;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Threading;
using dk.ModularSystems.Sparql.Contracts.Controllers;
using dk.ModularSystems.Sparql.Ontology;
using dk.ModularSystems.Sparql.Settings;
using VDS.RDF.Parsing;
using VDS.RDF.Query;
using System.Net.Http;

namespace dk.ModularSystems.Sparql
{
    public class AppController : IAppController, IQueryController, IQueryResultsController, IResourceBrowserController, IFilePickerController, ITextEditorController
    {
        public IAppView AppView { get; private set;}
        public String SparqlEndPointUriString { get; set; }

        public String DefaultPrefixStatements = "";

        public const String DefaultQueryStatement = "SELECT ?obj ?pred ?subj WHERE\r\n" +  
                                                    "{\r\n" + 
                                                    "    ?obj ?pred ?subject .\r\n" + 
                                                    "}\r\n" +
                                                    "LIMIT 10";

        SynchronizationContext _uiContext;

        public String AppFolder { get; protected set; }

        public AppController()
        {
            SparqlEndPointUriString = "http://dbpedia.org/sparql";
            _uiContext = SynchronizationContext.Current;
            AppFolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        }

        #region IAppController

        public async void Initialize(String[] args)
        {
            EndpointConfigurationSettings endpointSettings = System.Configuration.ConfigurationManager.GetSection("endpointConfiguration") as EndpointConfigurationSettings;
            NamespaceConfigurationSettings namespaceSettings = System.Configuration.ConfigurationManager.GetSection("namespaceConfiguration") as NamespaceConfigurationSettings;

            _ontologies.Clear();
            await new TaskFactory().StartNew(() =>
                {
                    LoadOntologies(namespaceSettings.Namespaces);
                });

            Debug.Assert(this.AppView != null);

            List<String> endpoints = new List<String>();
            foreach (EndpointElement endpointElement in endpointSettings.Endpoints)
            {
                endpoints.Add(endpointElement.EndpointUriString);
            }
            AppView.SetEndpoints(endpoints);

            for (int i=0; i<args.Length; i++)
            {
                String filePath = args[i];
                _uiContext.Post( (s) => AppView.OpenQuery(this, filePath), this);
            }
        }

        public void OnAbout(IAppView sender)
        {
            sender.ShowAppInfo(this);
        }

        public void OnTutorial(IAppView sender)
        {
            ShowWebPage("http://www.cambridgesemantics.com/semantic-university/sparql-by-example");
        }

        public void OnExit()
        {
            AppView.ExitApplication();
        }

        public void OnNewQueryView(IAppView sender)
        {
            Debug.Assert(sender == AppView);
            IQueryView queryView = AppView.CreateNewQueryView(this);

            queryView.SetQueryText( DefaultPrefixStatements + DefaultQueryStatement );
        }

        public void OnOpenQuery(IAppView sender)
        {
            sender.OpenQuery(this);
        }

        public void OnOpenQuery(IAppView sender, String queryPath)
        {
            AppView.OpenQuery(this, queryPath);
        }

        public void OnSaveQuery(IAppView sender, IQueryView queryView)
        {
            if (queryView.IsUntitled == false)
            {
                sender.SaveQuery(queryView, queryView.QueryPathName);
                return;
            }
            this.OnSaveQueryAs(sender, queryView);
        }

        public void OnSaveQueryAs(IAppView sender, IQueryView queryView)
        {
            sender.SaveQueryWithNewName(queryView);
        }

        public void OnException(Exception exception)
        {
            AppView.ReportError(exception);
        }

        public void OnTutorialQueries(IAppView sender)
        {
            String folder = Path.Combine(AppFolder, "Tutorial");
            sender.CreateNewFilePickerView(this, folder, "*.SPARQL");
        }

        public void OnOfficialSpecs(IAppView sender)
        {
            ShowWebPage("http://www.w3.org/TR/2013/REC-sparql11-query-20130321/");
        }

        public void OnSparqlPublicEndpoints(IAppView sender)
        {
            ShowWebPage("http://www.w3.org/wiki/SparqlEndpoints");
        }

        #endregion IAppController

        #region IFilePickerController

        public void OnFilesSelected(IEnumerable<string> filePaths)
        {
            foreach (String filePath in filePaths)
            {
                // TODO: Check that the file is actually a sparql query file
                AppView.OpenQuery(this, filePath);
            }
        }

        #endregion IFilePickerController

        #region IQueryController

        private readonly List<OntologyDictionary> _ontologies = new List<OntologyDictionary>();
        public IEnumerable<OntologyDictionary> Ontologies
        {
            get { return _ontologies; }
        }

        public void OnSubmitQuery(IQueryView sender, String queryString)
        {
            SubmitQuery(queryString);
        }

        public void OnSubmitQuery(IQueryView sender, SparqlQuery query)
        {
            SubmitQuery(query);
        }

        public void OnParseQuery(IQueryView sender)
        {
            ParseQuery(sender);
        }

        public void OnSaveQuery(IQueryView sender)
        {
            this.OnSaveQuery(AppView, sender);
        }

        public void OnSaveQueryAs(IQueryView sender)
        {
            this.OnSaveQueryAs(AppView, sender);
        }

        #endregion IQueryController

        #region ITextEditorController

        public void OnSave(ITextEditorView sender)
        {
            throw new NotImplementedException();
        }

        public void OnSaveAs(ITextEditorView sender)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IQueryResultsController

        public async void OnResultSelected(SparqlResult result, Object field, String fieldName)
        {
            if (field.ToString().ToLower().ToString().StartsWith("http://"))
            {
                String url = field.ToString();
                var httpClient = new HttpClient();
                HttpResponseMessage response = await httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Head, url));
                if ( response.Content.Headers.ContentType.MediaType == "text/html")
                {
                    AppView.CreateNewResourceBrowserView(field.ToString(), this);
                }
                else 
                {
                    response = await httpClient.GetAsync(url);
                    String responseText = await response.Content.ReadAsStringAsync();

                    if (response.Content.Headers.ContentType.MediaType == "application/json")
                    {
                        responseText = Newtonsoft.Json.Linq.JValue.Parse(responseText).ToString(Newtonsoft.Json.Formatting.Indented);
                    }
                    AppView.CreateNewTextViewerView(responseText, this);
                }
            }
        }

        #endregion IQueryResultsController

        public void SetView(IAppView appView)
        {
            _uiContext = SynchronizationContext.Current;
            AppView = appView;
            // this.OnNewQueryView(appView);
        }

        protected void ShowWebPage(String uriString)
        {
            AppView.CreateNewResourceBrowserView(uriString, this);
        }

        protected void LoadOntologies(NamespaceElementCollection namespaces)
        {
            _ontologies.Clear();

            // Load ontology vocabularies
            //

            IOntologyParser rdfXmlParser = new OntologyParserRdfXml();
            IOntologyParser ttlParser = new OntologyParserTtl();
            
            StringBuilder prefixStatementsBuilder = new StringBuilder();

            foreach (NamespaceElement nsElement in namespaces)
            {
                if (nsElement.AutoComplete == true)
                {
                    OntologyDictionary ontology;
                    String ontologyPath;
                    ontologyPath = Path.IsPathRooted(nsElement.SchemaPath)  
                                                    ? nsElement.SchemaPath 
                                                    : Path.Combine(AppFolder, nsElement.SchemaPath);

                    IOntologyParser parser = ontologyPath.EndsWith("rdf.xml", StringComparison.InvariantCultureIgnoreCase)
                                                    ? rdfXmlParser
                                                    : ttlParser;

                    parser.Parse(ontologyPath, out ontology);
                    _ontologies.Add(ontology);
                }

                if (nsElement.AutoPrefix == true)
                {
                    prefixStatementsBuilder.Append(String.Format("PREFIX {0}: <{1}> # {2}",
                                                    nsElement.Name, 
                                                    nsElement.NamespaceUriString, 
                                                    nsElement.Description));
                    prefixStatementsBuilder.Append(Environment.NewLine);
                }
            }

            prefixStatementsBuilder.Append(Environment.NewLine);
            DefaultPrefixStatements = prefixStatementsBuilder.ToString();
        }

        protected void LoadOntologies(String folder)
        {
            _ontologies.Clear();

            // Load ontology vocabularies
            //
            // var prefixes = new PrefixDictionary();

            IOntologyParser parser = new OntologyParserRdfXml(); // OntologyParserXml();
            String ontologyFolder = Path.Combine(AppFolder, "Ontologies");
            foreach (String ontologyFilePath in Directory.EnumerateFiles(ontologyFolder, "*.rdf.xml"))
            {
                OntologyDictionary ontology;
                parser.Parse( ontologyFilePath, out ontology );

                //String ontPrefix = Path.GetFileNameWithoutExtension(ontologyFilePath);
                //if (ontPrefix != null)
                //{
                //    ontology.Prefix = ontPrefix.Substring(0, ontPrefix.IndexOf('.'));
                //}
                _ontologies.Add(ontology);
            }
            parser = new OntologyParserTtl();
            foreach (String ontologyFilePath in Directory.EnumerateFiles(ontologyFolder, "*.ttl"))
            {
                OntologyDictionary ontology;
                parser.Parse(ontologyFilePath, out ontology);

                //String ontPrefix = Path.GetFileName(ontologyFilePath);
                //if (ontPrefix != null)
                //{
                //    ontology.Prefix = ontPrefix.Substring(0, ontPrefix.IndexOf('.'));
                //}
                _ontologies.Add(ontology);
            }
            foreach (String ontologyFilePath in Directory.EnumerateFiles(ontologyFolder, "*.nt"))
            {
                OntologyDictionary ontology;
                parser.Parse(ontologyFilePath, out ontology);

                //String ontPrefix = Path.GetFileName(ontologyFilePath);
                //if (ontPrefix != null)
                //{
                //    ontology.Prefix = ontPrefix.Substring(0, ontPrefix.IndexOf('.'));
                //}
                _ontologies.Add(ontology);
            }

            // Setup default prefix statements

            var prefixes = new List<String>();
            foreach (OntologyDictionary ontology in Ontologies)
            {
                foreach (NamespaceDescription nsdesc in ontology.NamespaceDescriptions)
                {
                    String prefix = String.Format("{0}: <{1}>", nsdesc.NamespacePrefix, nsdesc.NamespaceName);
                    if (prefixes.Contains(prefix) == false)
                    {
                        prefixes.Add(prefix);
                    }
                }
            }
            StringBuilder sb = new StringBuilder();
            foreach (String prefix in prefixes)
            {
                sb.Append("PREFIX ");
                sb.Append(prefix);
                sb.Append(Environment.NewLine);
            }
            sb.Append(Environment.NewLine);
            DefaultPrefixStatements = sb.ToString();
        }

        protected void SubmitQuery(String queryText)
        {
            SparqlQueryParser parser = new SparqlQueryParser();
            SparqlQuery query = parser.ParseFromString(queryText);
            SubmitQuery(query);
        }

        protected void ParseQuery(IQueryView queryView)
        {
            queryView.ParseQuery();
        }

        protected void SubmitQuery(SparqlQuery query)
        {
            AppView.SetStatusBusy("Submitting query...");
            String endpointUriString = AppView.GetCurrentEndpoint();
            Task.Factory.StartNew(() =>
            {
                try
                {
                    var endpoint = new SparqlRemoteEndpoint(new Uri(endpointUriString));
                    ISparqlQueryProcessor qproc = new RemoteQueryProcessor(endpoint);
                    // Object qresult = qproc.ProcessQuery(query);
                    Object resultObject = qproc.ProcessQuery(query);
                    Debug.Assert(resultObject is SparqlResultSet);
                    Debug.Assert(_uiContext != null);

                    SparqlResultSet result = (SparqlResultSet)resultObject;
                    SparqlQueryResultsInfo resultInfo = new SparqlQueryResultsInfo(query, result,endpoint);
                    _uiContext.Post(
                        new SendOrPostCallback(s =>
                        {
                            AppView.CreateNewQueryResultsView(resultInfo, this);
                        }), this);
                }
                catch (Exception ex)
                {
                    _uiContext.Post(
                        new SendOrPostCallback(s =>
                        {
                            this.OnException(ex);
                        }), this);
                }
                
            }).ContinueWith( (prevTask) =>
                {
                    AppView.SetStatusReady();
                });

        }

        //void GraphCallback(IGraph g, Object state)
        //{
        //    // MessageBox.Show("Hello from graph callback");
        //}

        //void SparqlResultCallback(SparqlResultSet result, Object state)
        //{
        //    Debug.Assert(_uiContext != null);
        //    _uiContext.Post(
        //        new SendOrPostCallback(s =>
        //        {

        //            AppView.CreateNewQueryResultsView(result, this);
        //        }), state);
        //}

    }
}
