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
using VDS.RDF.Query;
using WeifenLuo.WinFormsUI.Docking;

namespace dk.ModularSystems.Sparql
{
    public partial class QueryResultViewContainer : DockContent, IViewContainer<IQueryResultsView>
    {
        public QueryResultViewContainer()
        {
            InitializeComponent();
        }

        public QueryResultViewContainer(IQueryResultsController controller) :
            this()
        {
            queryResultView1.Controller = controller;
        }

        public IQueryResultsView GetView()
        {
            return queryResultView1;
        }

    }
}
