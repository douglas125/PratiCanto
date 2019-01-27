namespace ClassifTreinoVoz
{
    partial class frmMDIParent
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMDIParent));
            this.lblFindLicense = new System.Windows.Forms.Label();
            this.lblNotRegistered = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblFindLicense
            // 
            resources.ApplyResources(this.lblFindLicense, "lblFindLicense");
            this.lblFindLicense.Name = "lblFindLicense";
            // 
            // lblNotRegistered
            // 
            resources.ApplyResources(this.lblNotRegistered, "lblNotRegistered");
            this.lblNotRegistered.Name = "lblNotRegistered";
            // 
            // frmMDIParent
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblFindLicense);
            this.Controls.Add(this.lblNotRegistered);
            this.IsMdiContainer = true;
            this.Name = "frmMDIParent";
            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
            this.Load += new System.EventHandler(this.frmMDIParent_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblFindLicense;
        private System.Windows.Forms.Label lblNotRegistered;
    }
}