using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace dk.ModularSystems.Sparql
{
    public partial class ErrorView : Form
    {
        protected readonly Int32 _fullHeight;
        protected readonly Int32 _compactHeight;

        public ErrorView()
        {
            InitializeComponent();

            _fullHeight = this.ClientSize.Height;
            _compactHeight = this.txtErrorType.Location.Y - 2;
            HideDetails();
        }

        public ErrorView(Exception exception) :
            this()
        {
            SetError(exception);
        }

        public Exception Exception { get; protected set; }

        public void SetError(Exception exception)
        {
            Exception = exception;
            txtError.Text = exception.Message;
            txtStackTrace.Text = exception.StackTrace;
            txtErrorType.Text = exception.GetType().Name;
            btnInnerException.Enabled = exception.InnerException != null;
        }

        public void ShowInnerException()
        {
            Debug.Assert(Exception != null);
            Debug.Assert(Exception.InnerException != null);

            var errorView = new ErrorView(Exception.InnerException);
            errorView.ShowDialog(this);
        }

        public void ShowDetails()
        {
            this.ClientSize = new Size(this.ClientSize.Width, _fullHeight);
        }

        public void HideDetails()
        {
            this.ClientSize = new Size(this.ClientSize.Width, _compactHeight);
        }

        private void btnInnerException_Click(object sender, EventArgs e)
        {
            ShowInnerException();
        }

        private void btnMoreDetails_Click(object sender, EventArgs e)
        {
            ShowDetails();
        }

        private void btnLessDetails_Click(object sender, EventArgs e)
        {
            HideDetails();
        }

    }
}
