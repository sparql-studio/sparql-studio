namespace dk.ModularSystems.Sparql
{
    partial class QueryEditorViewContainer
    {
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.queryEditorControl1 = new dk.ModularSystems.Sparql.QueryEditorView();
            this.SuspendLayout();
            // 
            // queryEditorControl1
            // 
            this.queryEditorControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.queryEditorControl1.Location = new System.Drawing.Point(0, 0);
            this.queryEditorControl1.Name = "queryEditorControl1";
            this.queryEditorControl1.Size = new System.Drawing.Size(777, 408);
            this.queryEditorControl1.TabIndex = 0;
            // 
            // QueryView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(777, 408);
            this.Controls.Add(this.queryEditorControl1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "QueryView";
            this.Text = "new query";
            this.ResumeLayout(false);

        }

        #endregion

        private QueryEditorView queryEditorControl1;
    }
}