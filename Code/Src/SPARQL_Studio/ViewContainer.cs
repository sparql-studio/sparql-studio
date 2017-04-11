using dk.ModularSystems.Sparql.Contracts;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace dk.ModularSystems.Sparql
{
    class DockViewContainer<V, IC> : DockContent, IViewContainer<V>
        where V : UserControl, IView, new()
        where IC : IController
    {
        private readonly V _view;
        public V GetView()
        {
            return _view;
        }
        
        public DockViewContainer()
        {
            this.SuspendLayout();
            
            _view = new V();
            _view.Dock = System.Windows.Forms.DockStyle.Fill;
            _view.Location = new System.Drawing.Point(0, 0);
            _view.Size = this.Size;
            _view.Name = "_view";
            this.Controls.Add(_view);
            this.Text = _view.Title;

            this.ResumeLayout(performLayout:false);
        }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            if (_view.CanClose())
            {
                base.OnClosing(e);
            }
            else
            {
                e.Cancel = true;
            }                
        }

        public DockViewContainer(IC controller) :
            this()
        {
            _view.Controller = controller;

            _view.ContentChanged += view_UpdateTitle;
            _view.ContentSaved   += view_UpdateTitle;
            _view.ContentLoaded  += view_UpdateTitle;
        }

        void view_UpdateTitle(object sender)
        {
            this.Text = _view.Title + (_view.IsDirty ? "*" : "");
        }

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
