namespace dk.ModularSystems.Sparql
{
    partial class QueryEditorView
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(QueryEditorView));
            this.textEditorControl1 = new DigitalRune.Windows.TextEditor.TextEditorControl();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnParse = new System.Windows.Forms.ToolStripButton();
            this.btnRunQuery = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnAutocompleteToggle = new System.Windows.Forms.ToolStripButton();
            this.btnQueryValidationToggle = new System.Windows.Forms.ToolStripButton();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lbCoords = new System.Windows.Forms.ToolStripStatusLabel();
            this.lbErrorText = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnInsertCommentLine = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // textEditorControl1
            // 
            this.textEditorControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textEditorControl1.Location = new System.Drawing.Point(3, 28);
            this.textEditorControl1.Name = "textEditorControl1";
            this.textEditorControl1.Size = new System.Drawing.Size(784, 370);
            this.textEditorControl1.TabIndex = 0;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnParse,
            this.btnRunQuery,
            this.toolStripSeparator1,
            this.btnAutocompleteToggle,
            this.btnQueryValidationToggle,
            this.toolStripSeparator2,
            this.btnInsertCommentLine});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(790, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnParse
            // 
            this.btnParse.Image = ((System.Drawing.Image)(resources.GetObject("btnParse.Image")));
            this.btnParse.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnParse.Name = "btnParse";
            this.btnParse.Size = new System.Drawing.Size(96, 22);
            this.btnParse.Text = "Check syntax";
            this.btnParse.Click += new System.EventHandler(this.btnParse_Click);
            // 
            // btnRunQuery
            // 
            this.btnRunQuery.Image = global::dk.ModularSystems.Sparql.Properties.Resources.run_16;
            this.btnRunQuery.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnRunQuery.Name = "btnRunQuery";
            this.btnRunQuery.Size = new System.Drawing.Size(81, 22);
            this.btnRunQuery.Tag = "";
            this.btnRunQuery.Text = "Run query";
            this.btnRunQuery.ToolTipText = "Run query";
            this.btnRunQuery.Click += new System.EventHandler(this.btnRunQuery_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // btnAutocompleteToggle
            // 
            this.btnAutocompleteToggle.Checked = true;
            this.btnAutocompleteToggle.CheckOnClick = true;
            this.btnAutocompleteToggle.CheckState = System.Windows.Forms.CheckState.Checked;
            this.btnAutocompleteToggle.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnAutocompleteToggle.Image = ((System.Drawing.Image)(resources.GetObject("btnAutocompleteToggle.Image")));
            this.btnAutocompleteToggle.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAutocompleteToggle.Name = "btnAutocompleteToggle";
            this.btnAutocompleteToggle.Size = new System.Drawing.Size(23, 22);
            this.btnAutocompleteToggle.ToolTipText = "Toggle Autocomplete on/off";
            this.btnAutocompleteToggle.Click += new System.EventHandler(this.btnAutocompleteToggle_Click);
            // 
            // btnQueryValidationToggle
            // 
            this.btnQueryValidationToggle.Checked = true;
            this.btnQueryValidationToggle.CheckOnClick = true;
            this.btnQueryValidationToggle.CheckState = System.Windows.Forms.CheckState.Checked;
            this.btnQueryValidationToggle.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnQueryValidationToggle.Image = ((System.Drawing.Image)(resources.GetObject("btnQueryValidationToggle.Image")));
            this.btnQueryValidationToggle.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnQueryValidationToggle.Name = "btnQueryValidationToggle";
            this.btnQueryValidationToggle.Size = new System.Drawing.Size(23, 22);
            this.btnQueryValidationToggle.Text = "Syntax check";
            this.btnQueryValidationToggle.ToolTipText = "Toggle automatic SPARQL syntax checking";
            this.btnQueryValidationToggle.Click += new System.EventHandler(this.btnQueryValidationToggle_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lbCoords,
            this.lbErrorText});
            this.statusStrip1.Location = new System.Drawing.Point(0, 401);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(790, 24);
            this.statusStrip1.SizingGrip = false;
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // lbCoords
            // 
            this.lbCoords.Name = "lbCoords";
            this.lbCoords.Size = new System.Drawing.Size(43, 19);
            this.lbCoords.Text = "coords";
            // 
            // lbErrorText
            // 
            this.lbErrorText.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.lbErrorText.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.lbErrorText.Name = "lbErrorText";
            this.lbErrorText.Size = new System.Drawing.Size(43, 19);
            this.lbErrorText.Text = "Ready";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // btnInsertCommentLine
            // 
            this.btnInsertCommentLine.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnInsertCommentLine.Image = ((System.Drawing.Image)(resources.GetObject("btnInsertCommentLine.Image")));
            this.btnInsertCommentLine.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnInsertCommentLine.Name = "btnInsertCommentLine";
            this.btnInsertCommentLine.Size = new System.Drawing.Size(23, 22);
            this.btnInsertCommentLine.Text = "Comment";
            this.btnInsertCommentLine.ToolTipText = "Insert comment line (Ctrl + Enter)";
            this.btnInsertCommentLine.Click += new System.EventHandler(this.btnInsertCommentLine_Click);
            // 
            // QueryEditorView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.textEditorControl1);
            this.Name = "QueryEditorView";
            this.Size = new System.Drawing.Size(790, 425);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DigitalRune.Windows.TextEditor.TextEditorControl textEditorControl1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnRunQuery;
        private System.Windows.Forms.ToolStripButton btnParse;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel lbCoords;
        private System.Windows.Forms.ToolStripStatusLabel lbErrorText;
        private System.Windows.Forms.ToolStripButton btnAutocompleteToggle;
        private System.Windows.Forms.ToolStripButton btnQueryValidationToggle;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton btnInsertCommentLine;
    }
}
