// Author: Antonello Salvatucci as@modularsystems.dk
//
// This code is copyright (c) by Antonello Salvatucci and Modular Systems
// See the LICENSE file at the root of the repository for licensing details.
// 
// THIS CODE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED
// ----------------------------------------------------------------------------
//
using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using DigitalRune.Windows.TextEditor;
using dk.ModularSystems.Sparql.Contracts;
using dk.ModularSystems.Sparql.Ontology;
using dk.ModularSystems.Sparql.SparqlAutoCompletion;
using VDS.RDF;
using VDS.RDF.Parsing.Validation;
using VDS.RDF.Query;
using VDS.RDF.Parsing;
using System.Diagnostics;
using DigitalRune.Windows.TextEditor.Highlighting;
using System.Reflection;
using System.IO;

namespace dk.ModularSystems.Sparql
{
    public partial class QueryEditorView : UserControl, IQueryView
    {
        #region IView

        private IQueryController _controller;
        public IController Controller 
        {
            get { return _controller; }
            set { _controller = (IQueryController)value; } 
        }

        public event Action<Object> ContentChanged;
        public event Action<Object> ContentSaved;
        public event Action<Object> ContentLoaded;

        public Boolean CanClose()
        {
            Boolean canClose = true;
            if (IsDirty)
            {
                DialogResult dlgRes = PromptSaveChanges();
                if (dlgRes == DialogResult.Cancel)
                {
                    canClose = false;
                }
                else if (dlgRes != DialogResult.No)
                {
                    _controller.OnSaveQuery(this);
                    canClose = (IsDirty == false);
                }
            }
            return canClose;
        }

        #endregion IView

        static SparqlCompletionDataProvider _completionDataProvider = new SparqlCompletionDataProvider();

        public QueryEditorView()
        {
            InitializeComponent();

            if (this.DesignMode == false)
            {
                // The control fails to load within the VS designer ... it complains about the syntax highlighting subfolder
                //
                String currentPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                HighlightingManager.Manager.AddSyntaxModeFileProvider(new FileSyntaxModeProvider(Path.Combine(currentPath, "SparqlSyntaxHighlighing")));
                textEditorControl1.Document.HighlightingStrategy = HighlightingManager.Manager.FindHighlighter("SPARQL");
                textEditorControl1.CompletionRequest += textEditorControl1_CompletionRequest;
                textEditorControl1.Document.DocumentChanged += Document_DocumentChanged;

                // Enable drag and drop
                this.AllowDrop = true;

                QueryValidationEnabled = true;
                AutocompleteEnabled = true;
                btnAutocompleteToggle.Checked = AutocompleteEnabled;

                textEditorControl1.TextAreaDialogKeyPress += textEditorControl1_TextAreaDialogKeyPress;
                textEditorControl1.TextAreaKeyPress += textEditorControl1_TextAreaKeyPress;

                textEditorControl1.ActiveTextAreaControl.Caret.PositionChanged += Caret_PositionChanged;
            }
        }

        void textEditorControl1_TextAreaDialogKeyPress(object sender, KeyEventArgs e)
        {
             // TODO: Handle this in a more flexible way
             // This just inserts a comment line on ctrl+enter
             
            if ((e.Control) && (!e.Alt) && (!e.Shift))
            {
                if (e.KeyCode == Keys.Enter)
                {
                    e.Handled = true;
                    this.InsertCommentLine();
                }
            }
        }

        void textEditorControl1_TextAreaKeyPress(object sender, KeyPressEventArgs e)
        {
            // TODO: Handle this in a more flexible way
            //

        }

        void Caret_PositionChanged(object sender, EventArgs e)
        {
            Caret caret = (Caret)sender;
            lbCoords.Text = String.Format("{0},{1}", caret.Column+1, caret.Line+1);
        }

        void textEditorControl1_CompletionRequest(object sender, DigitalRune.Windows.TextEditor.Completion.CompletionEventArgs e)
        {
            if (AutocompleteEnabled == false)
            {
                return;
            }
            if (SparqlCompletionDataProvider.Ontologies == null)
            {
                if (Controller is IQueryController)
                {
                    SparqlCompletionDataProvider.Ontologies = ((IQueryController)Controller).Ontologies;
                }
            }
            textEditorControl1.ShowCompletionWindow(_completionDataProvider, e.Key, closeAutomatically: true);
        }

        async void Document_DocumentChanged(object sender, DigitalRune.Windows.TextEditor.Document.DocumentEventArgs e)
        {
            IsDirty = true;
            FireContentChanged();
            
            // TODO: It should start a timer and validate only after some "quiet time"
            if (QueryValidationEnabled)
            {
                Task<Boolean> validationCompletion = ValidateQueryAsync();
                Boolean queryValid = await validationCompletion;
            }
        }

        protected async Task<Boolean> ValidateQueryAsync()
        {
            String queryText = textEditorControl1.Document.TextContent;

            SparqlQueryValidator validator = new SparqlQueryValidator();
            ISyntaxValidationResults validationResult = null;
            ClearQueryValidationErrors();
            await Task.Factory.StartNew(() =>
            {
                validationResult = validator.Validate(queryText);
            });

            if ((validationResult != null) && (validationResult.IsValid == false))
            {
                ShowQueryValidationError(validationResult.Error, EditorErrorMarkingStyle.ValidationErrorMarkingStyle);
                return false;
            }
            return true;
        }

        protected void ClearQueryValidationErrors()
        {
            this.textEditorControl1.Document.MarkerStrategy.Clear();
            lbErrorText.Text = "";
        }

        protected void ShowQueryValidationError(Exception ex, EditorErrorMarkingStyle markingStyle)
        {
            if (ex is RdfParseException)
            {
                RdfParseException validationError = (RdfParseException) ex;
                if (validationError.HasPositionInformation)
                {
                    var errorLineMarker = new EditorErrorMarker(textEditorControl1.Document, validationError, markingStyle);
                    this.textEditorControl1.Document.MarkerStrategy.AddMarker(errorLineMarker);
                }
                lbErrorText.Text = validationError.Message;
                lbErrorText.BackColor = SystemColors.ActiveCaption;
                lbErrorText.ForeColor = SystemColors.ActiveCaptionText;
            }
            else
            {
                lbErrorText.Text = ex.Message;
                lbErrorText.BackColor = SystemColors.ActiveCaption;
                lbErrorText.ForeColor = SystemColors.ActiveCaptionText;
            }
        }

        protected DialogResult PromptSaveChanges()
        {
            String saveMessage = String.Format("{0} has changed. Do you want to save it?", this.Title);
            return MessageBox.Show(this, saveMessage, "SPARQL Studio", MessageBoxButtons.YesNoCancel);
        }

        protected void FireContentChanged()
        {
            if (ContentChanged != null)
            {
                ContentChanged(this);
            }
        }

        protected void FireContentSaved()
        {
            if (ContentSaved != null)
            {
                ContentSaved(this);
            }
        }

        protected void FireContentLoaded()
        {
            if (ContentLoaded != null)
            {
                ContentLoaded(this);
            }
        }

        public QueryEditorView(IQueryController controller)
        {
            Controller = controller;
        }

        private void btnRunQuery_Click(object sender, EventArgs e)
        {
            Debug.Assert(Controller != null);
            SparqlQuery query = this.ParseQuery();
            if (query != null)
            {
                _controller.OnSubmitQuery(this, ParseQuery());
            }
        }

        protected void InsertCommentLine()
        {
            InsertText(Environment.NewLine + "# ");
        }

        protected void InsertText(String text)
        {
            this.textEditorControl1.ActiveTextAreaControl.TextArea.InsertString(text);
        }

        #region IQueryView

        private Boolean _autoCompleteEnabled = true;
        public Boolean AutocompleteEnabled
        {
            get { return _autoCompleteEnabled; }
            set
            {
                if (value != _autoCompleteEnabled)
                {
                    _autoCompleteEnabled = value;
                    btnAutocompleteToggle.Checked = value;
                }
            }
        }

        private Boolean _queryValidationEnabled = true;

        public Boolean QueryValidationEnabled
        {
            get { return _queryValidationEnabled; }
            set
            {
                if (value != _queryValidationEnabled)
                {
                    _queryValidationEnabled = value;
                    btnQueryValidationToggle.Checked = value;
                }
            }
        }

        public OntologyDictionary Ontologies { get; set; }

        public Boolean IsDirty { get; protected set; }
        public String  QueryPathName { get; protected set; }
        public Boolean IsUntitled 
        {
            get
            {
                return String.IsNullOrEmpty(QueryPathName);
            }
        }

        public String Title
        {
            get
            {
                return IsUntitled ? "<Untitled query>" : Path.GetFileName(QueryPathName);
            }
        }

        public VDS.RDF.Query.SparqlQuery ParseQuery()
        {
            var qparser = new SparqlQueryParser();
            String queryText = this.textEditorControl1.Text;
            ClearQueryValidationErrors();
            
            // TEST
            try
            {
                SparqlQuery query = qparser.ParseFromString(queryText);
                lbErrorText.Text = "Ready";
                return query;
            }
            catch (RdfParseException ex)
            {
                ShowQueryValidationError(ex, EditorErrorMarkingStyle.ParseErrorMarkingStyle);
                return null;
            }
            catch (RdfException ex)
            {
                ShowQueryValidationError(ex, EditorErrorMarkingStyle.ParseErrorMarkingStyle);
                return null;
            }
            finally
            {
                this.Invalidate(invalidateChildren:true);
            }
        }

        public void SetQuery(VDS.RDF.Query.SparqlQuery query)
        {
            SetQueryText(query.ToString());
        }

        public void SetQueryText(String queryText)
        {
            // TODO: Save existing query
            //
            textEditorControl1.Text = queryText;
        }

        public void SaveQuery(String pathName)
        {
            textEditorControl1.SaveFile(pathName);
            this.IsDirty = false;
            this.QueryPathName = pathName;
            FireContentSaved();
        }

        public void OpenQuery(String pathName)
        {
            textEditorControl1.LoadFile(pathName);
            this.QueryPathName = pathName;
            this.IsDirty = false;
            FireContentLoaded();
        }


        #endregion

        private void btnParse_Click(object sender, EventArgs e)
        {
            _controller.OnParseQuery(this);
        }

        private void btnAutocompleteToggle_Click(object sender, EventArgs e)
        {
            Boolean isChecked = ((ToolStripButton) sender).Checked;
            if (AutocompleteEnabled != isChecked)
            {
                this.AutocompleteEnabled = isChecked;
            }
        }

        private void btnQueryValidationToggle_Click(object sender, EventArgs e)
        {
            Boolean isChecked = ((ToolStripButton)sender).Checked;
            if ( QueryValidationEnabled != isChecked)
            {
                this.QueryValidationEnabled = isChecked;
            }
        }

        private void btnInsertCommentLine_Click(object sender, EventArgs e)
        {
            InsertCommentLine();
        }

    }
}
