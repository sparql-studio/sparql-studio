namespace dk.ModularSystems.Sparql
{
    partial class QueryResultViewContainer
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
            this.queryResultView1 = new dk.ModularSystems.Sparql.QueryResultView();
            this.SuspendLayout();
            // 
            // queryResultView1
            // 
            this.queryResultView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.queryResultView1.Location = new System.Drawing.Point(0, 0);
            this.queryResultView1.Name = "queryResultView1";
            this.queryResultView1.Size = new System.Drawing.Size(804, 561);
            this.queryResultView1.TabIndex = 0;
            // 
            // QueryResultViewContainer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(804, 561);
            this.Controls.Add(this.queryResultView1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "QueryResultViewContainer";
            this.Text = "QueryResultView";
            this.ResumeLayout(false);

        }

        #endregion

        private QueryResultView queryResultView1;

    }
}