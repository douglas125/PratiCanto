namespace AudioComparer
{
    /// <summary>Client form</summary>
    partial class frmClient
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmClient));
            this.gbPersonalInfo = new System.Windows.Forms.GroupBox();
            this.cmbVoiceType = new System.Windows.Forms.ComboBox();
            this.dtBirth = new System.Windows.Forms.DateTimePicker();
            this.txtNotes = new System.Windows.Forms.TextBox();
            this.txtName = new System.Windows.Forms.TextBox();
            this.txtImportantNotes = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblAge = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.gbDocs = new System.Windows.Forms.GroupBox();
            this.btnViewFolder = new System.Windows.Forms.Button();
            this.btnAddDoc = new System.Windows.Forms.Button();
            this.lstClientDocs = new System.Windows.Forms.ListBox();
            this.gbPic = new System.Windows.Forms.GroupBox();
            this.picClientPic = new System.Windows.Forms.PictureBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.lblSelectClientFromList = new System.Windows.Forms.Label();
            this.lblClientExists = new System.Windows.Forms.Label();
            this.lblConfirmDelete = new System.Windows.Forms.Label();
            this.txtClientFolder = new System.Windows.Forms.TextBox();
            this.lstClient = new System.Windows.Forms.ListBox();
            this.btnRemoveUser = new System.Windows.Forms.Button();
            this.btnEditUser = new System.Windows.Forms.Button();
            this.btnAddUser = new System.Windows.Forms.Button();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.gbPersonalInfo.SuspendLayout();
            this.gbDocs.SuspendLayout();
            this.gbPic.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picClientPic)).BeginInit();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbPersonalInfo
            // 
            resources.ApplyResources(this.gbPersonalInfo, "gbPersonalInfo");
            this.gbPersonalInfo.Controls.Add(this.cmbVoiceType);
            this.gbPersonalInfo.Controls.Add(this.dtBirth);
            this.gbPersonalInfo.Controls.Add(this.txtNotes);
            this.gbPersonalInfo.Controls.Add(this.txtName);
            this.gbPersonalInfo.Controls.Add(this.txtImportantNotes);
            this.gbPersonalInfo.Controls.Add(this.label4);
            this.gbPersonalInfo.Controls.Add(this.label3);
            this.gbPersonalInfo.Controls.Add(this.lblAge);
            this.gbPersonalInfo.Controls.Add(this.label6);
            this.gbPersonalInfo.Controls.Add(this.label5);
            this.gbPersonalInfo.Controls.Add(this.label2);
            this.gbPersonalInfo.Controls.Add(this.label1);
            this.gbPersonalInfo.Name = "gbPersonalInfo";
            this.gbPersonalInfo.TabStop = false;
            this.toolTip.SetToolTip(this.gbPersonalInfo, resources.GetString("gbPersonalInfo.ToolTip"));
            this.gbPersonalInfo.Paint += new System.Windows.Forms.PaintEventHandler(this.gbPersonalInfo_Paint);
            // 
            // cmbVoiceType
            // 
            resources.ApplyResources(this.cmbVoiceType, "cmbVoiceType");
            this.cmbVoiceType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbVoiceType.FormattingEnabled = true;
            this.cmbVoiceType.Items.AddRange(new object[] {
            resources.GetString("cmbVoiceType.Items"),
            resources.GetString("cmbVoiceType.Items1"),
            resources.GetString("cmbVoiceType.Items2"),
            resources.GetString("cmbVoiceType.Items3")});
            this.cmbVoiceType.Name = "cmbVoiceType";
            this.toolTip.SetToolTip(this.cmbVoiceType, resources.GetString("cmbVoiceType.ToolTip"));
            this.cmbVoiceType.Leave += new System.EventHandler(this.cmbVoiceType_Leave);
            // 
            // dtBirth
            // 
            resources.ApplyResources(this.dtBirth, "dtBirth");
            this.dtBirth.Name = "dtBirth";
            this.toolTip.SetToolTip(this.dtBirth, resources.GetString("dtBirth.ToolTip"));
            this.dtBirth.ValueChanged += new System.EventHandler(this.dtBirth_ValueChanged);
            this.dtBirth.Leave += new System.EventHandler(this.dtBirth_Leave);
            // 
            // txtNotes
            // 
            resources.ApplyResources(this.txtNotes, "txtNotes");
            this.txtNotes.Name = "txtNotes";
            this.toolTip.SetToolTip(this.txtNotes, resources.GetString("txtNotes.ToolTip"));
            this.txtNotes.Leave += new System.EventHandler(this.txtNotes_Leave);
            // 
            // txtName
            // 
            resources.ApplyResources(this.txtName, "txtName");
            this.txtName.Name = "txtName";
            this.toolTip.SetToolTip(this.txtName, resources.GetString("txtName.ToolTip"));
            this.txtName.Leave += new System.EventHandler(this.txtName_Leave);
            // 
            // txtImportantNotes
            // 
            resources.ApplyResources(this.txtImportantNotes, "txtImportantNotes");
            this.txtImportantNotes.Name = "txtImportantNotes";
            this.toolTip.SetToolTip(this.txtImportantNotes, resources.GetString("txtImportantNotes.ToolTip"));
            this.txtImportantNotes.Leave += new System.EventHandler(this.txtImportantNotes_Leave);
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            this.toolTip.SetToolTip(this.label4, resources.GetString("label4.ToolTip"));
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            this.toolTip.SetToolTip(this.label3, resources.GetString("label3.ToolTip"));
            // 
            // lblAge
            // 
            resources.ApplyResources(this.lblAge, "lblAge");
            this.lblAge.Name = "lblAge";
            this.toolTip.SetToolTip(this.lblAge, resources.GetString("lblAge.ToolTip"));
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            this.toolTip.SetToolTip(this.label6, resources.GetString("label6.ToolTip"));
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            this.toolTip.SetToolTip(this.label5, resources.GetString("label5.ToolTip"));
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            this.toolTip.SetToolTip(this.label2, resources.GetString("label2.ToolTip"));
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            this.toolTip.SetToolTip(this.label1, resources.GetString("label1.ToolTip"));
            // 
            // gbDocs
            // 
            resources.ApplyResources(this.gbDocs, "gbDocs");
            this.gbDocs.Controls.Add(this.btnViewFolder);
            this.gbDocs.Controls.Add(this.btnAddDoc);
            this.gbDocs.Controls.Add(this.lstClientDocs);
            this.gbDocs.Name = "gbDocs";
            this.gbDocs.TabStop = false;
            this.toolTip.SetToolTip(this.gbDocs, resources.GetString("gbDocs.ToolTip"));
            this.gbDocs.Paint += new System.Windows.Forms.PaintEventHandler(this.gbDocs_Paint);
            // 
            // btnViewFolder
            // 
            resources.ApplyResources(this.btnViewFolder, "btnViewFolder");
            this.btnViewFolder.Name = "btnViewFolder";
            this.toolTip.SetToolTip(this.btnViewFolder, resources.GetString("btnViewFolder.ToolTip"));
            this.btnViewFolder.UseVisualStyleBackColor = true;
            this.btnViewFolder.Click += new System.EventHandler(this.btnViewFolder_Click);
            // 
            // btnAddDoc
            // 
            resources.ApplyResources(this.btnAddDoc, "btnAddDoc");
            this.btnAddDoc.Name = "btnAddDoc";
            this.toolTip.SetToolTip(this.btnAddDoc, resources.GetString("btnAddDoc.ToolTip"));
            this.btnAddDoc.UseVisualStyleBackColor = true;
            this.btnAddDoc.Click += new System.EventHandler(this.btnAddDoc_Click);
            // 
            // lstClientDocs
            // 
            resources.ApplyResources(this.lstClientDocs, "lstClientDocs");
            this.lstClientDocs.FormattingEnabled = true;
            this.lstClientDocs.Name = "lstClientDocs";
            this.toolTip.SetToolTip(this.lstClientDocs, resources.GetString("lstClientDocs.ToolTip"));
            this.lstClientDocs.DoubleClick += new System.EventHandler(this.lstClientDocs_DoubleClick);
            // 
            // gbPic
            // 
            resources.ApplyResources(this.gbPic, "gbPic");
            this.gbPic.Controls.Add(this.picClientPic);
            this.gbPic.Name = "gbPic";
            this.gbPic.TabStop = false;
            this.toolTip.SetToolTip(this.gbPic, resources.GetString("gbPic.ToolTip"));
            this.gbPic.Paint += new System.Windows.Forms.PaintEventHandler(this.gbPic_Paint);
            // 
            // picClientPic
            // 
            resources.ApplyResources(this.picClientPic, "picClientPic");
            this.picClientPic.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picClientPic.Name = "picClientPic";
            this.picClientPic.TabStop = false;
            this.toolTip.SetToolTip(this.picClientPic, resources.GetString("picClientPic.ToolTip"));
            this.picClientPic.Click += new System.EventHandler(this.picClientPic_Click);
            // 
            // groupBox4
            // 
            resources.ApplyResources(this.groupBox4, "groupBox4");
            this.groupBox4.Controls.Add(this.lblSelectClientFromList);
            this.groupBox4.Controls.Add(this.lblClientExists);
            this.groupBox4.Controls.Add(this.lblConfirmDelete);
            this.groupBox4.Controls.Add(this.txtClientFolder);
            this.groupBox4.Controls.Add(this.lstClient);
            this.groupBox4.Controls.Add(this.btnRemoveUser);
            this.groupBox4.Controls.Add(this.btnEditUser);
            this.groupBox4.Controls.Add(this.btnAddUser);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.TabStop = false;
            this.toolTip.SetToolTip(this.groupBox4, resources.GetString("groupBox4.ToolTip"));
            this.groupBox4.Paint += new System.Windows.Forms.PaintEventHandler(this.groupBox4_Paint);
            // 
            // lblSelectClientFromList
            // 
            resources.ApplyResources(this.lblSelectClientFromList, "lblSelectClientFromList");
            this.lblSelectClientFromList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblSelectClientFromList.Name = "lblSelectClientFromList";
            this.toolTip.SetToolTip(this.lblSelectClientFromList, resources.GetString("lblSelectClientFromList.ToolTip"));
            // 
            // lblClientExists
            // 
            resources.ApplyResources(this.lblClientExists, "lblClientExists");
            this.lblClientExists.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblClientExists.Name = "lblClientExists";
            this.toolTip.SetToolTip(this.lblClientExists, resources.GetString("lblClientExists.ToolTip"));
            // 
            // lblConfirmDelete
            // 
            resources.ApplyResources(this.lblConfirmDelete, "lblConfirmDelete");
            this.lblConfirmDelete.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblConfirmDelete.Name = "lblConfirmDelete";
            this.toolTip.SetToolTip(this.lblConfirmDelete, resources.GetString("lblConfirmDelete.ToolTip"));
            // 
            // txtClientFolder
            // 
            resources.ApplyResources(this.txtClientFolder, "txtClientFolder");
            this.txtClientFolder.Name = "txtClientFolder";
            this.toolTip.SetToolTip(this.txtClientFolder, resources.GetString("txtClientFolder.ToolTip"));
            this.txtClientFolder.TextChanged += new System.EventHandler(this.txtClientFolder_TextChanged);
            // 
            // lstClient
            // 
            resources.ApplyResources(this.lstClient, "lstClient");
            this.lstClient.FormattingEnabled = true;
            this.lstClient.Name = "lstClient";
            this.lstClient.Sorted = true;
            this.toolTip.SetToolTip(this.lstClient, resources.GetString("lstClient.ToolTip"));
            this.lstClient.SelectedIndexChanged += new System.EventHandler(this.lstClient_SelectedIndexChanged);
            this.lstClient.DoubleClick += new System.EventHandler(this.lstClient_DoubleClick);
            // 
            // btnRemoveUser
            // 
            resources.ApplyResources(this.btnRemoveUser, "btnRemoveUser");
            this.btnRemoveUser.Name = "btnRemoveUser";
            this.toolTip.SetToolTip(this.btnRemoveUser, resources.GetString("btnRemoveUser.ToolTip"));
            this.btnRemoveUser.UseVisualStyleBackColor = true;
            this.btnRemoveUser.Click += new System.EventHandler(this.btnRemoveUser_Click);
            // 
            // btnEditUser
            // 
            resources.ApplyResources(this.btnEditUser, "btnEditUser");
            this.btnEditUser.Name = "btnEditUser";
            this.toolTip.SetToolTip(this.btnEditUser, resources.GetString("btnEditUser.ToolTip"));
            this.btnEditUser.UseVisualStyleBackColor = true;
            this.btnEditUser.Click += new System.EventHandler(this.btnEditUser_Click);
            // 
            // btnAddUser
            // 
            resources.ApplyResources(this.btnAddUser, "btnAddUser");
            this.btnAddUser.Name = "btnAddUser";
            this.toolTip.SetToolTip(this.btnAddUser, resources.GetString("btnAddUser.ToolTip"));
            this.btnAddUser.UseVisualStyleBackColor = true;
            this.btnAddUser.Click += new System.EventHandler(this.btnAddUser_Click);
            // 
            // frmClient
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.gbPic);
            this.Controls.Add(this.gbDocs);
            this.Controls.Add(this.gbPersonalInfo);
            this.Name = "frmClient";
            this.toolTip.SetToolTip(this, resources.GetString("$this.ToolTip"));
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmClient_FormClosing);
            this.Load += new System.EventHandler(this.frmClient_Load);
            this.gbPersonalInfo.ResumeLayout(false);
            this.gbPersonalInfo.PerformLayout();
            this.gbDocs.ResumeLayout(false);
            this.gbPic.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picClientPic)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbPersonalInfo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox gbDocs;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox gbPic;
        private System.Windows.Forms.DateTimePicker dtBirth;
        private System.Windows.Forms.TextBox txtNotes;
        private System.Windows.Forms.ComboBox cmbVoiceType;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button btnRemoveUser;
        private System.Windows.Forms.Button btnEditUser;
        private System.Windows.Forms.Button btnAddUser;
        private System.Windows.Forms.ListBox lstClient;
        private System.Windows.Forms.Label lblAge;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtClientFolder;
        private System.Windows.Forms.Label lblConfirmDelete;
        /// <summary>Picture of client</summary>
        public System.Windows.Forms.PictureBox picClientPic;
        private System.Windows.Forms.ListBox lstClientDocs;
        private System.Windows.Forms.Button btnAddDoc;
        private System.Windows.Forms.Button btnViewFolder;
        private System.Windows.Forms.ToolTip toolTip;
        /// <summary>Important notes from client. To display outside</summary>
        public System.Windows.Forms.TextBox txtImportantNotes;
        private System.Windows.Forms.Label lblClientExists;
        private System.Windows.Forms.Label lblSelectClientFromList;
    }
}