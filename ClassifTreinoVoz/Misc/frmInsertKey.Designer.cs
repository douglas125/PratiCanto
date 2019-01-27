namespace PratiCanto
{
    partial class frmInsertKey
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmInsertKey));
            this.label1 = new System.Windows.Forms.Label();
            this.txtLicenseKey = new System.Windows.Forms.TextBox();
            this.panInfo = new System.Windows.Forms.Panel();
            this.lblMsgs = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtUserName = new System.Windows.Forms.TextBox();
            this.btnGetLicense = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.panInfo.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Name = "label1";
            // 
            // txtLicenseKey
            // 
            resources.ApplyResources(this.txtLicenseKey, "txtLicenseKey");
            this.txtLicenseKey.Name = "txtLicenseKey";
            // 
            // panInfo
            // 
            resources.ApplyResources(this.panInfo, "panInfo");
            this.panInfo.Controls.Add(this.lblMsgs);
            this.panInfo.Controls.Add(this.label2);
            this.panInfo.Controls.Add(this.label1);
            this.panInfo.Controls.Add(this.txtUserName);
            this.panInfo.Controls.Add(this.txtLicenseKey);
            this.panInfo.Name = "panInfo";
            this.panInfo.Paint += new System.Windows.Forms.PaintEventHandler(this.panInfo_Paint);
            // 
            // lblMsgs
            // 
            resources.ApplyResources(this.lblMsgs, "lblMsgs");
            this.lblMsgs.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblMsgs.Name = "lblMsgs";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Name = "label2";
            // 
            // txtUserName
            // 
            resources.ApplyResources(this.txtUserName, "txtUserName");
            this.txtUserName.Name = "txtUserName";
            // 
            // btnGetLicense
            // 
            resources.ApplyResources(this.btnGetLicense, "btnGetLicense");
            this.btnGetLicense.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGetLicense.ForeColor = System.Drawing.Color.White;
            this.btnGetLicense.Name = "btnGetLicense";
            this.btnGetLicense.UseVisualStyleBackColor = true;
            this.btnGetLicense.Click += new System.EventHandler(this.btnGetLicense_Click);
            // 
            // btnCancel
            // 
            resources.ApplyResources(this.btnCancel, "btnCancel");
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // frmInsertKey
            // 
            this.AcceptButton = this.btnGetLicense;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.CancelButton = this.btnCancel;
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnGetLicense);
            this.Controls.Add(this.panInfo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "frmInsertKey";
            this.Load += new System.EventHandler(this.frmInsertKey_Load);
            this.panInfo.ResumeLayout(false);
            this.panInfo.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtLicenseKey;
        private System.Windows.Forms.Panel panInfo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtUserName;
        private System.Windows.Forms.Button btnGetLicense;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblMsgs;
    }
}