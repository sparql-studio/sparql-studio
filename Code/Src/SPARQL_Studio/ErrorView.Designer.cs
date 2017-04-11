namespace dk.ModularSystems.Sparql
{
    partial class ErrorView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ErrorView));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.txtError = new System.Windows.Forms.TextBox();
            this.btnInnerException = new System.Windows.Forms.Button();
            this.btnMoreDetails = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.txtStackTrace = new System.Windows.Forms.TextBox();
            this.btnLessDetails = new System.Windows.Forms.Button();
            this.txtErrorType = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(12, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(64, 64);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // txtError
            // 
            this.txtError.Location = new System.Drawing.Point(82, 12);
            this.txtError.Multiline = true;
            this.txtError.Name = "txtError";
            this.txtError.ReadOnly = true;
            this.txtError.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtError.Size = new System.Drawing.Size(513, 64);
            this.txtError.TabIndex = 1;
            // 
            // btnInnerException
            // 
            this.btnInnerException.Location = new System.Drawing.Point(12, 82);
            this.btnInnerException.Name = "btnInnerException";
            this.btnInnerException.Size = new System.Drawing.Size(123, 23);
            this.btnInnerException.TabIndex = 2;
            this.btnInnerException.Text = "&Inner Exception";
            this.btnInnerException.UseVisualStyleBackColor = true;
            this.btnInnerException.Click += new System.EventHandler(this.btnInnerException_Click);
            // 
            // btnMoreDetails
            // 
            this.btnMoreDetails.Location = new System.Drawing.Point(425, 82);
            this.btnMoreDetails.Name = "btnMoreDetails";
            this.btnMoreDetails.Size = new System.Drawing.Size(82, 23);
            this.btnMoreDetails.TabIndex = 2;
            this.btnMoreDetails.Text = "&More";
            this.btnMoreDetails.UseVisualStyleBackColor = true;
            this.btnMoreDetails.Click += new System.EventHandler(this.btnMoreDetails_Click);
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(513, 82);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(82, 23);
            this.btnClose.TabIndex = 2;
            this.btnClose.Text = "&Close";
            this.btnClose.UseVisualStyleBackColor = true;
            // 
            // txtStackTrace
            // 
            this.txtStackTrace.Location = new System.Drawing.Point(12, 145);
            this.txtStackTrace.Multiline = true;
            this.txtStackTrace.Name = "txtStackTrace";
            this.txtStackTrace.ReadOnly = true;
            this.txtStackTrace.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtStackTrace.Size = new System.Drawing.Size(583, 145);
            this.txtStackTrace.TabIndex = 1;
            // 
            // btnLessDetails
            // 
            this.btnLessDetails.Location = new System.Drawing.Point(513, 294);
            this.btnLessDetails.Name = "btnLessDetails";
            this.btnLessDetails.Size = new System.Drawing.Size(82, 23);
            this.btnLessDetails.TabIndex = 2;
            this.btnLessDetails.Text = "&Less";
            this.btnLessDetails.UseVisualStyleBackColor = true;
            this.btnLessDetails.Click += new System.EventHandler(this.btnLessDetails_Click);
            // 
            // txtErrorType
            // 
            this.txtErrorType.Location = new System.Drawing.Point(12, 120);
            this.txtErrorType.Name = "txtErrorType";
            this.txtErrorType.ReadOnly = true;
            this.txtErrorType.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtErrorType.Size = new System.Drawing.Size(583, 20);
            this.txtErrorType.TabIndex = 3;
            // 
            // ErrorView
            // 
            this.AcceptButton = this.btnClose;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(609, 326);
            this.Controls.Add(this.txtErrorType);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnLessDetails);
            this.Controls.Add(this.btnMoreDetails);
            this.Controls.Add(this.btnInnerException);
            this.Controls.Add(this.txtStackTrace);
            this.Controls.Add(this.txtError);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ErrorView";
            this.Text = "Error";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TextBox txtError;
        private System.Windows.Forms.Button btnInnerException;
        private System.Windows.Forms.Button btnMoreDetails;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.TextBox txtStackTrace;
        private System.Windows.Forms.Button btnLessDetails;
        private System.Windows.Forms.TextBox txtErrorType;
    }
}