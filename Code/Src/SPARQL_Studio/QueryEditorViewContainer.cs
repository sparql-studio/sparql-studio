using dk.ModularSystems.Sparql.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace dk.ModularSystems.Sparql
{
    public partial class QueryEditorViewContainer : DockContent, IViewContainer<IQueryView>
    {
        public QueryEditorViewContainer()
        {
            InitializeComponent();
        }

        public QueryEditorViewContainer(IQueryController queryController) :
            this()
        {
            queryEditorControl1.Controller = queryController;

            queryEditorControl1.ContentChanged += queryEditorControl1_UpdateTitle;
            queryEditorControl1.ContentSaved += queryEditorControl1_UpdateTitle;
            queryEditorControl1.ContentSaved += queryEditorControl1_UpdateTitle;
        }

        void queryEditorControl1_UpdateTitle(object sender)
        {
            this.Text = queryEditorControl1.Title + (queryEditorControl1.IsDirty ? "*" : "");
        }

        public IQueryView GetView()
        {
            return queryEditorControl1;
        }
    }
}
