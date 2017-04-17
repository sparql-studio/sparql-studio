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
    public partial class TextEditorView : UserControl, ITextEditorView
    {
        #region IView

        private ITextEditorController _controller;
        public IController Controller 
        {
            get { return _controller; }
            set { _controller = (ITextEditorController)value; } 
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
                    _controller.OnSave(this);
                    canClose = (IsDirty == false);
                }
            }
            return canClose;
        }

        #endregion IView
            
        public TextEditorView()
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

                AutocompleteEnabled = true;

                textEditorControl1.TextAreaDialogKeyPress += textEditorControl1_TextAreaDialogKeyPress;
                textEditorControl1.TextAreaKeyPress += textEditorControl1_TextAreaKeyPress;

                textEditorControl1.ActiveTextAreaControl.Caret.PositionChanged += Caret_PositionChanged;
            }
        }

        void textEditorControl1_TextAreaDialogKeyPress(object sender, KeyEventArgs e)
        {

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
        }

        void Document_DocumentChanged(object sender, DigitalRune.Windows.TextEditor.Document.DocumentEventArgs e)
        {
            IsDirty = true;
            FireContentChanged();
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

        public TextEditorView(IQueryController controller)
        {
            Controller = controller;
        }


        protected void InsertText(String text)
        {
            this.textEditorControl1.ActiveTextAreaControl.TextArea.InsertString(text);
        }

        #region ITextEditorView

        // Not supported for now
        public Boolean AutocompleteEnabled
        {
            get; set;
        }

        public Boolean IsDirty { get; protected set; }
        public String  PathName { get; protected set; }
        public Boolean IsUntitled 
        {
            get
            {
                return String.IsNullOrEmpty(PathName);
            }
        }

        public String Title
        {
            get
            {
                return IsUntitled ? "<Untitled text>" : Path.GetFileName(PathName);
            }
        }

        public void SetText(String text)
        {
            // TODO: Save existing query
            //
            textEditorControl1.Text = text;
        }

        public void Save(String pathName)
        {
            textEditorControl1.SaveFile(pathName);
            this.IsDirty = false;
            this.PathName = pathName;
            FireContentSaved();
        }

        public void Open(String pathName)
        {
            textEditorControl1.LoadFile(pathName);
            this.PathName = pathName;
            this.IsDirty = false;
            FireContentLoaded();
        }

        public Boolean ReadOnly
        {
            get { return textEditorControl1.IsReadOnly; }
            set { textEditorControl1.IsReadOnly = value; }
        }

        #endregion

    }
}
