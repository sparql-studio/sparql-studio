using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using dk.ModularSystems.Sparql.Contracts;
using VDS.RDF.Nodes;
using VDS.RDF.Query;
using VDS.RDF;
using System.Diagnostics;
using VDS.RDF.Query.Expressions;

namespace dk.ModularSystems.Sparql
{
    public partial class QueryResultView : UserControl, IQueryResultsView
    {
        private IQueryResultsController _controller;
        public IController Controller
        {
            get { return _controller; }
            set { _controller = (IQueryResultsController)value; }
        }

        public QueryResultView()
        {
            InitializeComponent();
        }

        public QueryResultView(IQueryResultsController controller) :
            this()
        {
            Controller = controller;
        }

        #region IQueryResultsView

        public void SetQueryResults(SparqlResultSet results)
        {
            lbResultCount.Text = results.Count.ToString();

            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();
            foreach (var v in results.Variables)
            {
                Int32 colIndex = dataGridView1.Columns.Add(v,v);
                dataGridView1.Columns[colIndex].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            }

            Object[] cellValues = new Object[results.Variables.Count()];
            foreach (var result in results)
            {
                Int32 i = 0;
                foreach (var kvp in result)
                {
                    if (kvp.Value == null)
                    {
                        cellValues[i] = "";
                    }
                    else if (kvp.Value is BaseLiteralNode)
                    {
                        IValuedNode ivn = kvp.Value.AsValuedNode();
                        switch (ivn.NumericType)
                        {
                        case SparqlNumericType.Decimal:
                            cellValues[i] = ivn.AsDecimal();
                            break;
                        case SparqlNumericType.Double:
                            cellValues[i] = ivn.AsDouble();
                            break;
                        case SparqlNumericType.Float:
                            cellValues[i] = ivn.AsFloat();
                            break;
                        case SparqlNumericType.Integer:
                            cellValues[i] = ivn.AsInteger();
                            break;
                        case SparqlNumericType.NaN:
                        default:
                            cellValues[i] = ((BaseLiteralNode)kvp.Value).Value;
                            break;
                        }
                    }
                    else
                    {
                        cellValues[i] = kvp.Value.ToString();
                    }
                    i++;
                    Debug.Assert(i <= cellValues.Length);
                }
                Int32 rowIndex = dataGridView1.Rows.Add(cellValues);
                dataGridView1.Rows[rowIndex].Tag = result;
            }
        }

        public void SetQueryInfo(SparqlQuery query, SparqlRemoteEndpoint endpoint)
        {
            lbExecTime.Text = query.QueryExecutionTime.ToString();
            lbEndpoint.Text = endpoint.Uri.ToString();
            txtQuery.Text = query.ToString();
        }

        #endregion

        protected void OnCellSelected(Int32 rowIndex, Int32 colIndex)
        {
            DataGridViewRow selectedRow = dataGridView1.Rows[rowIndex];
            DataGridViewCell selectedCell = selectedRow.Cells[colIndex];
            _controller.OnResultSelected((SparqlResult)selectedRow.Tag, selectedCell.Value, dataGridView1.Columns[colIndex].Name);
        }


        private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            OnCellSelected(e.RowIndex, e.ColumnIndex);
        }


        #region IView

        public Boolean CanClose()
        {
            return true;
        }


        public bool IsDirty
        {
            get { return false; }
        }

        public bool IsUntitled
        {
            get { return false; }
        }

        public string Title
        {
            get { return "Query results"; }
        }

        public event Action<object> ContentChanged { add { } remove { } }
        public event Action<object> ContentSaved { add { } remove { } }
        public event Action<object> ContentLoaded { add { } remove { } }

        #endregion
    }
}
