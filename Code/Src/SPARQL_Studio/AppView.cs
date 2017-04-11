using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using dk.ModularSystems.Sparql.Contracts;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using dk.ModularSystems.Sparql.Contracts.Controllers;
using dk.ModularSystems.Sparql.Contracts.Views;
using WeifenLuo.WinFormsUI.Docking;

namespace dk.ModularSystems.Sparql
{
    public partial class AppView : Form, IAppView
    {
        public IController Controller 
        {
            get { return _controller; }
            set { _controller = (IAppController)value; } 
        }
        IAppController _controller;

        //delegate void SetControlText(String text);
        //delegate String GetControlText(Control ctl);

        public static ProgressBarStyle DefaultProgressBarStyle = ProgressBarStyle.Blocks;
        public static String TextReady = "Ready";

        #region IAppView members

        // protected readonly List<DockContent> _viewsContainers = new List<DockContent>();
        public IEnumerable<IView> GetViews()
        {
            foreach (DockContent viewContainer in dockPanel1.Contents)
            {
                if (viewContainer is IViewContainer<IView>)
                    yield return  ((IViewContainer<IView>)viewContainer).GetView();
            }
        }

        public IView GetCurrentView()
        {
            IViewContainer<IView> container = dockPanel1.ActiveDocument as IViewContainer<IView>;
            if (container != null)
            {
                return container.GetView();
            }
            return null;
        }

        public void ExitApplication()
        {
            this.Close();
        }

        public void ShowAppInfo(IAppController controller)
        {
            var dlg = new DlgAbout();
            dlg.FillInformation(Assembly.GetExecutingAssembly());
            dlg.ShowDialog(this);
        }

        public IQueryView CreateNewQueryView(IQueryController controller)
        {
            var queryViewContainer = new DockViewContainer<QueryEditorView, IQueryController>(controller);
            
            InsertInDockPanel(dockPanel1, queryViewContainer);
            return queryViewContainer.GetView();
        }

        public IFilePickerView CreateNewFilePickerView(IFilePickerController controller, String folder, String filter)
        {
            var filePickerContainer = new DockViewContainer<FilePickerView, IFilePickerController>(controller);
            IFilePickerView view = filePickerContainer.GetView();
            view.SetFileList(folder, filter);
            InsertInDockPanel(dockPanel1, filePickerContainer, DockState.DockLeft);
            return view;
        }

        public IQueryView CreateNewQueryView(IQueryController controller, String queryPathName)
        {
            var queryViewContainer = new DockViewContainer<QueryEditorView, IQueryController>(controller);

            InsertInDockPanel(dockPanel1, queryViewContainer);
            IQueryView queryView = queryViewContainer.GetView();
            queryView.OpenQuery(queryPathName);
            return queryView;
        }

        public IQueryResultsView CreateNewQueryResultsView(SparqlQueryResultsInfo resultsInfo, IQueryResultsController controller)
        {
            var resultContainer = new DockViewContainer<QueryResultView, IQueryResultsController>(controller);
            IQueryResultsView resultsView = resultContainer.GetView();
            resultsView.SetQueryResults(resultsInfo.QueryResults);
            resultsView.SetQueryInfo(resultsInfo.Query, resultsInfo.Endpoint);

            InsertInDockPanel(dockPanel1, resultContainer, DockState.DockBottom);
            return resultsView;
        }

        public IResourceBrowserView CreateNewResourceBrowserView(String resourceUri, IResourceBrowserController controller)
        {
            var resBrowserContainer = new DockViewContainer<ResourceBrowserView, IResourceBrowserController>(controller);
            InsertInDockPanel(dockPanel1, resBrowserContainer);
            ResourceBrowserView resBrowserView = resBrowserContainer.GetView();

            resBrowserView.GotoResource(resourceUri);
            return resBrowserView;
        }

        public void ReportError(Exception exception)
        {
            var errorView = new ErrorView(exception);
            errorView.ShowDialog(this);
        }

        public void SetStatusReady()
        {
            SetStatus(DefaultProgressBarStyle, 0, TextReady);
        }

        public void SetStatusBusy(String statusText)
        {
            SetStatus(ProgressBarStyle.Marquee, 0, statusText);
        }

        public void SetStatusBusy(String statusText, Int32 progressPct)
        {
            SetStatus(DefaultProgressBarStyle, progressPct, statusText);
        }

        public IEnumerable<String> GetEndpoints()
        {
            foreach (var endpointName in cbEndpointName.Items)
            {
                yield return endpointName.ToString();
            }
        }

        public void SetEndpoints(IEnumerable<String> endpointUriStrings)
        {
            cbEndpointName.Items.Clear();
            if (endpointUriStrings != null)
            {
                foreach (String endpointUriString in endpointUriStrings)
                {
                    cbEndpointName.Items.Add(endpointUriString);
                }
            }
        }

        public string GetCurrentEndpoint()
        {
            return cbEndpointName.Text;
        }

        public void SetCurrentEndpoint(string endpointUriString)
        {
            // cbEndpointName.Text = endpointUriString;
            cbEndpointName.SelectedItem = endpointUriString;
        }

        public void SaveQueryWithNewName(IQueryView queryView)
        {

            var dlg = new SaveFileDialog();
            dlg.DefaultExt = ".sparql";

            dlg.Filter = "SPARQL queries (*.sparql)|*.sparql|All files (*.*)|*.*";
            dlg.FilterIndex = 1;
            dlg.RestoreDirectory = true;
            if (dlg.ShowDialog(this) != DialogResult.OK)
            {
                return;
            }
            SaveQuery(queryView, dlg.FileName);
        }

        public void SaveQuery(IQueryView queryView, String pathName)
        {
            queryView.SaveQuery(pathName);
        }

        public void OpenQuery(IQueryController controller)
        {
            // var dlg = new OpenFileDialog();
            OpenFileDialog dlg = openFileDialog1;
            dlg.Reset();
            dlg.DefaultExt = ".sparql";
            dlg.Filter = "SPARQL queries (*.sparql)|*.sparql|All files (*.*)|*.*";
            dlg.FilterIndex = 1;
            dlg.RestoreDirectory = true;
            //dlg.Multiselect = false;
            //dlg.CheckFileExists = true;
            //dlg.CheckPathExists = true;
            //dlg.ShowReadOnly = true;
            //dlg.AutoUpgradeEnabled = false;
            try
            {
                if (dlg.ShowDialog() != DialogResult.OK)
                {
                    return;
                }
            }
            catch( Exception ex)
            {
                var exDlg = new ErrorView(ex);
                exDlg.Show(this);
            }
            this.OpenQuery(controller, dlg.FileName);
        }

        public void OpenQuery(IQueryController controller, String filePath)
        {
            this.CreateNewQueryView(controller, filePath);
        }
        
        #endregion

        #region IView members

        public Boolean CanClose()
        {
            Boolean canClose = true;
            foreach (IView view in this.GetViews())
            {
                if (view.CanClose() == false)
                {
                    canClose = false;
                    break;
                }
            }
            return canClose;
        }

        public Boolean IsDirty
        {
            get 
            { 
                // TODO: check to see if any of the child views is in dirty state
                return false;
            }
        }

        public Boolean IsUntitled
        {
            get { return false; }
        }

        public String Title
        {
            get { return this.Text; }
        }

        public event Action<object> ContentChanged { add { } remove { } }
        public event Action<object> ContentSaved { add { } remove { } }
        public event Action<object> ContentLoaded { add { } remove { } }

        #endregion

        protected override void OnClosing(CancelEventArgs e)
        {
            if (this.CanClose())
            {
                base.OnClosing(e);
            }
            else
            {
                e.Cancel = true;
            }
        }

        #region Utilities

        protected void InsertInDockPanel(DockPanel dockPanel, DockContent viewContainer, DockState dockState = DockState.Document)
        {
            viewContainer.TabPageContextMenuStrip = ctxGeneral;
            if (dockPanel.DocumentStyle == DocumentStyle.SystemMdi)
            {
                viewContainer.MdiParent = this;
                viewContainer.Show();
            }
            else
            {
                // If there is any pane containing a view of the same kind, put it there
                // 
                DockPane parentPane = null;
                IDockContent previousContent = null;
                foreach (DockPane pane in dockPanel1.Panes)
                {
                    foreach (IDockContent content in pane.Contents)
                    {
                        if (content.GetType() == viewContainer.GetType())
                        {
                            // Use this pane!
                            parentPane = pane;
                            previousContent = content;
                            break;
                        }
                    }

                    if (parentPane != null)
                    {
                        break;
                    }
                }

                if (parentPane != null)
                {
                    Debug.Assert(previousContent != null);
                    viewContainer.Show(parentPane, previousContent);
                }
                else
                {
                    viewContainer.Show(dockPanel1, dockState);
                }
            }
        }

        protected void SetStatus(ProgressBarStyle pbStyle, Int32 pbValue, String statusText)
        {
            if (InvokeRequired)
            {
                Invoke((Action<ProgressBarStyle, Int32, String>)DoSetStatus, pbStyle, pbValue, statusText);
            }
            else
            {
                DoSetStatus(pbStyle, pbValue, statusText);
            }
        }

        protected void DoSetStatus(ProgressBarStyle pbStyle, Int32 pbValue, String statusText)
        {
            progressBar.Style = pbStyle;
            progressBar.Value = pbValue;
            lblCurrentStatus.Text = statusText;
        }

        #endregion Utilities

        public AppView()
        {
            InitializeComponent();
            dockPanel1.DockBottomPortion = 0.5;

            SetCurrentEndpoint("http://dbpedia.org/sparql");
        }

        public AppView(IAppController vm) :
            this()
        {
            _controller = vm;
            _controller.SetView(this);
        }

        protected void OnSaveQueryAs()
        {
            var currentView = GetCurrentView() as IQueryView;
            if (currentView == null)
            {
                return;
            }
            _controller.OnSaveQueryAs(this, currentView);
        }

        protected void OnSaveQuery()
        {
            var currentView = GetCurrentView() as IQueryView;
            if (currentView == null)
            {
                return;
            }
            _controller.OnSaveQuery(this,currentView);
        }

        protected void OnOpenQuery()
        {
            _controller.OnOpenQuery(this);
        }

        protected void OnAbout()
        {
            _controller.OnAbout(this);
        }

        protected void OnTutorial()
        {
            _controller.OnTutorial(this);
        }

        private void openQueryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OnOpenQuery();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _controller.OnExit();
        }

        private void newQueryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _controller.OnNewQueryView(this);
        }

        private void saveQueryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.OnSaveQuery();
        }

        private void saveQueryasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.OnSaveQueryAs();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.OnAbout();
        }

        private void tutorialMenuItem_Click(object sender, EventArgs e)
        {
            this.OnTutorial();
        }

        private void AppView_DragDrop(object sender, DragEventArgs e)
        {
            String[] files = (String[])e.Data.GetData(DataFormats.FileDrop);
            foreach (String file in files)
            {
                _controller.OnOpenQuery(this, file);
            }
        }

        private void AppView_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
        }

        private void tutorialQueriesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _controller.OnTutorialQueries(this);
        }

        protected void CloseActiveView()
        {
            dockPanel1.ActiveContent.DockHandler.Close();
        }

        protected void CloseAllViews()
        {
            // IDockContent[] allContents = dockPanel1.Contents.ToArray();
            IDockContent[] allContents = dockPanel1.ActivePane.Contents.ToArray();
            foreach (IDockContent content in allContents)
            {
                content.DockHandler.Close();
            }
        }

        protected void CloseAllViewsExceptActive()
        {
            IDockContent activeContent = dockPanel1.ActivePane.ActiveContent;
            IDockContent[] allContents = dockPanel1.ActivePane.Contents.ToArray();
            foreach (IDockContent content in allContents)
            {
                if (content != activeContent)
                {
                    content.DockHandler.Close();
                }
            }
        }

        protected void DuplicateActiveView()
        {
            IDockContent activeContent = dockPanel1.ActiveContent;
            if (activeContent != null)
            {
                
            }
        }

        private void ctxBtnClose_Click(object sender, EventArgs e)
        {
            CloseActiveView();
        }

        private void ctxBtnCloseAll_Click(object sender, EventArgs e)
        {
            CloseAllViews();
        }

        private void ctvBtnCloseAllOthers_Click(object sender, EventArgs e)
        {
            CloseAllViewsExceptActive();
        }

        private void btnOfficialSpecs_Click(object sender, EventArgs e)
        {
            _controller.OnOfficialSpecs(this);
        }

        private void publicSparqlEndpointsMenuItem_Click(object sender, EventArgs e)
        {
            _controller.OnSparqlPublicEndpoints(this);
        }

    }
}
