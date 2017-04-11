namespace dk.ModularSystems.Sparql
{
    partial class SettingsView
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsView));
            this.lvSettingsPages = new System.Windows.Forms.ListView();
            this.imgListSettingsIcons = new System.Windows.Forms.ImageList(this.components);
            this.SuspendLayout();
            // 
            // lvSettingsPages
            // 
            this.lvSettingsPages.Dock = System.Windows.Forms.DockStyle.Left;
            this.lvSettingsPages.Location = new System.Drawing.Point(0, 0);
            this.lvSettingsPages.Name = "lvSettingsPages";
            this.lvSettingsPages.Size = new System.Drawing.Size(117, 561);
            this.lvSettingsPages.TabIndex = 0;
            this.lvSettingsPages.UseCompatibleStateImageBehavior = false;
            // 
            // imgListSettingsIcons
            // 
            this.imgListSettingsIcons.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgListSettingsIcons.ImageStream")));
            this.imgListSettingsIcons.TransparentColor = System.Drawing.Color.Transparent;
            this.imgListSettingsIcons.Images.SetKeyName(0, "settings_2_32.png.png");
            this.imgListSettingsIcons.Images.SetKeyName(1, "connection_32.png");
            // 
            // SettingsView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lvSettingsPages);
            this.Name = "SettingsView";
            this.Size = new System.Drawing.Size(893, 561);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lvSettingsPages;
        private System.Windows.Forms.ImageList imgListSettingsIcons;
    }
}
