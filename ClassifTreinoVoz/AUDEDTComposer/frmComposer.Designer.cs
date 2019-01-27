namespace PratiCanto
{
    partial class frmComposer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmComposer));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnClient = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.btnStartStop = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnRec = new System.Windows.Forms.ToolStripButton();
            this.btnPlay = new System.Windows.Forms.ToolStripButton();
            this.btnSaveRecord = new System.Windows.Forms.ToolStripButton();
            this.btnOpenFile = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.lblNote = new System.Windows.Forms.ToolStripLabel();
            this.txtAnnotation = new System.Windows.Forms.ToolStripTextBox();
            this.gbChooseFileOpen = new System.Windows.Forms.GroupBox();
            this.btnPlayOrigFile = new System.Windows.Forms.Button();
            this.btnStopOrigFile = new System.Windows.Forms.Button();
            this.btnOpenFileComment = new System.Windows.Forms.Button();
            this.picComposition = new System.Windows.Forms.PictureBox();
            this.btnPauseOrigFile = new System.Windows.Forms.Button();
            this.gbAudioCom = new System.Windows.Forms.GroupBox();
            this.lblSaveInfo = new System.Windows.Forms.Label();
            this.numVolAudio = new System.Windows.Forms.NumericUpDown();
            this.numVolComment = new System.Windows.Forms.NumericUpDown();
            this.lblAudioVol = new System.Windows.Forms.Label();
            this.lblCommVol = new System.Windows.Forms.Label();
            this.chkVoiceOver = new System.Windows.Forms.CheckBox();
            this.cmbInputDevice = new System.Windows.Forms.ComboBox();
            this.btnStartAddComment = new System.Windows.Forms.Button();
            this.btnDeleteComment = new System.Windows.Forms.Button();
            this.btnPlaySelCmt = new System.Windows.Forms.Button();
            this.lblDuration = new System.Windows.Forms.Label();
            this.txtDescrip = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.numCommentTime = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSaveWithCmt = new System.Windows.Forms.Button();
            this.lblComments = new System.Windows.Forms.Label();
            this.lstComments = new System.Windows.Forms.ListBox();
            this.picRec = new System.Windows.Forms.PictureBox();
            this.picStop = new System.Windows.Forms.PictureBox();
            this.picPlay = new System.Windows.Forms.PictureBox();
            this.toolStrip1.SuspendLayout();
            this.gbChooseFileOpen.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picComposition)).BeginInit();
            this.gbAudioCom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numVolAudio)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numVolComment)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCommentTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picRec)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picStop)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picPlay)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnClient,
            this.toolStripSeparator3,
            this.btnStartStop,
            this.toolStripSeparator2,
            this.btnRec,
            this.btnPlay,
            this.btnSaveRecord,
            this.btnOpenFile,
            this.toolStripSeparator1,
            this.lblNote,
            this.txtAnnotation});
            resources.ApplyResources(this.toolStrip1, "toolStrip1");
            this.toolStrip1.Name = "toolStrip1";
            // 
            // btnClient
            // 
            resources.ApplyResources(this.btnClient, "btnClient");
            this.btnClient.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnClient.Name = "btnClient";
            this.btnClient.Click += new System.EventHandler(this.btnClient_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            resources.ApplyResources(this.toolStripSeparator3, "toolStripSeparator3");
            // 
            // btnStartStop
            // 
            this.btnStartStop.BackColor = System.Drawing.Color.Lime;
            this.btnStartStop.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            resources.ApplyResources(this.btnStartStop, "btnStartStop");
            this.btnStartStop.Name = "btnStartStop";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            resources.ApplyResources(this.toolStripSeparator2, "toolStripSeparator2");
            // 
            // btnRec
            // 
            this.btnRec.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.btnRec, "btnRec");
            this.btnRec.Name = "btnRec";
            this.btnRec.Click += new System.EventHandler(this.btnRec_Click);
            // 
            // btnPlay
            // 
            resources.ApplyResources(this.btnPlay, "btnPlay");
            this.btnPlay.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnPlay.Name = "btnPlay";
            // 
            // btnSaveRecord
            // 
            this.btnSaveRecord.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.btnSaveRecord, "btnSaveRecord");
            this.btnSaveRecord.Name = "btnSaveRecord";
            // 
            // btnOpenFile
            // 
            resources.ApplyResources(this.btnOpenFile, "btnOpenFile");
            this.btnOpenFile.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnOpenFile.Name = "btnOpenFile";
            this.btnOpenFile.Click += new System.EventHandler(this.btnOpenFile_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            resources.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
            // 
            // lblNote
            // 
            resources.ApplyResources(this.lblNote, "lblNote");
            this.lblNote.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.lblNote.Name = "lblNote";
            // 
            // txtAnnotation
            // 
            resources.ApplyResources(this.txtAnnotation, "txtAnnotation");
            this.txtAnnotation.Name = "txtAnnotation";
            // 
            // gbChooseFileOpen
            // 
            resources.ApplyResources(this.gbChooseFileOpen, "gbChooseFileOpen");
            this.gbChooseFileOpen.Controls.Add(this.btnPlayOrigFile);
            this.gbChooseFileOpen.Controls.Add(this.btnStopOrigFile);
            this.gbChooseFileOpen.Controls.Add(this.btnOpenFileComment);
            this.gbChooseFileOpen.Controls.Add(this.picComposition);
            this.gbChooseFileOpen.Controls.Add(this.btnPauseOrigFile);
            this.gbChooseFileOpen.Name = "gbChooseFileOpen";
            this.gbChooseFileOpen.TabStop = false;
            this.gbChooseFileOpen.Paint += new System.Windows.Forms.PaintEventHandler(this.gbChooseFileOpen_Paint);
            // 
            // btnPlayOrigFile
            // 
            resources.ApplyResources(this.btnPlayOrigFile, "btnPlayOrigFile");
            this.btnPlayOrigFile.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPlayOrigFile.Name = "btnPlayOrigFile";
            this.btnPlayOrigFile.UseVisualStyleBackColor = true;
            this.btnPlayOrigFile.Click += new System.EventHandler(this.btnPlayOrigFile_Click);
            // 
            // btnStopOrigFile
            // 
            resources.ApplyResources(this.btnStopOrigFile, "btnStopOrigFile");
            this.btnStopOrigFile.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnStopOrigFile.Name = "btnStopOrigFile";
            this.btnStopOrigFile.UseVisualStyleBackColor = true;
            this.btnStopOrigFile.Click += new System.EventHandler(this.btnStopOrigFile_Click);
            // 
            // btnOpenFileComment
            // 
            this.btnOpenFileComment.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnOpenFileComment.Image = global::PratiCanto.Properties.Resources.Openfolder_orange;
            resources.ApplyResources(this.btnOpenFileComment, "btnOpenFileComment");
            this.btnOpenFileComment.Name = "btnOpenFileComment";
            this.btnOpenFileComment.UseVisualStyleBackColor = true;
            this.btnOpenFileComment.Click += new System.EventHandler(this.btnOpenFile_Click);
            // 
            // picComposition
            // 
            resources.ApplyResources(this.picComposition, "picComposition");
            this.picComposition.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picComposition.Cursor = System.Windows.Forms.Cursors.Cross;
            this.picComposition.Name = "picComposition";
            this.picComposition.TabStop = false;
            this.picComposition.Paint += new System.Windows.Forms.PaintEventHandler(this.picComposition_Paint);
            this.picComposition.MouseClick += new System.Windows.Forms.MouseEventHandler(this.picComposition_MouseClick);
            // 
            // btnPauseOrigFile
            // 
            resources.ApplyResources(this.btnPauseOrigFile, "btnPauseOrigFile");
            this.btnPauseOrigFile.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPauseOrigFile.Name = "btnPauseOrigFile";
            this.btnPauseOrigFile.UseVisualStyleBackColor = true;
            this.btnPauseOrigFile.Click += new System.EventHandler(this.btnPauseOrigFile_Click);
            // 
            // gbAudioCom
            // 
            resources.ApplyResources(this.gbAudioCom, "gbAudioCom");
            this.gbAudioCom.Controls.Add(this.lblSaveInfo);
            this.gbAudioCom.Controls.Add(this.numVolAudio);
            this.gbAudioCom.Controls.Add(this.numVolComment);
            this.gbAudioCom.Controls.Add(this.lblAudioVol);
            this.gbAudioCom.Controls.Add(this.lblCommVol);
            this.gbAudioCom.Controls.Add(this.chkVoiceOver);
            this.gbAudioCom.Controls.Add(this.cmbInputDevice);
            this.gbAudioCom.Controls.Add(this.btnStartAddComment);
            this.gbAudioCom.Controls.Add(this.btnDeleteComment);
            this.gbAudioCom.Controls.Add(this.btnPlaySelCmt);
            this.gbAudioCom.Controls.Add(this.lblDuration);
            this.gbAudioCom.Controls.Add(this.txtDescrip);
            this.gbAudioCom.Controls.Add(this.label3);
            this.gbAudioCom.Controls.Add(this.label2);
            this.gbAudioCom.Controls.Add(this.numCommentTime);
            this.gbAudioCom.Controls.Add(this.label1);
            this.gbAudioCom.Controls.Add(this.btnSaveWithCmt);
            this.gbAudioCom.Controls.Add(this.lblComments);
            this.gbAudioCom.Controls.Add(this.lstComments);
            this.gbAudioCom.Name = "gbAudioCom";
            this.gbAudioCom.TabStop = false;
            this.gbAudioCom.Paint += new System.Windows.Forms.PaintEventHandler(this.gbAudioCom_Paint);
            // 
            // lblSaveInfo
            // 
            this.lblSaveInfo.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.lblSaveInfo, "lblSaveInfo");
            this.lblSaveInfo.Name = "lblSaveInfo";
            // 
            // numVolAudio
            // 
            resources.ApplyResources(this.numVolAudio, "numVolAudio");
            this.numVolAudio.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numVolAudio.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numVolAudio.Name = "numVolAudio";
            this.numVolAudio.Value = new decimal(new int[] {
            75,
            0,
            0,
            0});
            this.numVolAudio.ValueChanged += new System.EventHandler(this.numVolAudio_ValueChanged);
            // 
            // numVolComment
            // 
            resources.ApplyResources(this.numVolComment, "numVolComment");
            this.numVolComment.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numVolComment.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numVolComment.Name = "numVolComment";
            this.numVolComment.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numVolComment.ValueChanged += new System.EventHandler(this.numVolComment_ValueChanged);
            // 
            // lblAudioVol
            // 
            resources.ApplyResources(this.lblAudioVol, "lblAudioVol");
            this.lblAudioVol.Name = "lblAudioVol";
            // 
            // lblCommVol
            // 
            resources.ApplyResources(this.lblCommVol, "lblCommVol");
            this.lblCommVol.Name = "lblCommVol";
            // 
            // chkVoiceOver
            // 
            resources.ApplyResources(this.chkVoiceOver, "chkVoiceOver");
            this.chkVoiceOver.BackColor = System.Drawing.Color.Transparent;
            this.chkVoiceOver.Checked = true;
            this.chkVoiceOver.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkVoiceOver.Name = "chkVoiceOver";
            this.chkVoiceOver.UseVisualStyleBackColor = false;
            this.chkVoiceOver.CheckedChanged += new System.EventHandler(this.chkVoiceOver_CheckedChanged);
            // 
            // cmbInputDevice
            // 
            resources.ApplyResources(this.cmbInputDevice, "cmbInputDevice");
            this.cmbInputDevice.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbInputDevice.FormattingEnabled = true;
            this.cmbInputDevice.Name = "cmbInputDevice";
            // 
            // btnStartAddComment
            // 
            this.btnStartAddComment.BackColor = System.Drawing.Color.Transparent;
            this.btnStartAddComment.Cursor = System.Windows.Forms.Cursors.Hand;
            resources.ApplyResources(this.btnStartAddComment, "btnStartAddComment");
            this.btnStartAddComment.ForeColor = System.Drawing.Color.White;
            this.btnStartAddComment.Image = global::PratiCanto.Properties.Resources.startTest;
            this.btnStartAddComment.Name = "btnStartAddComment";
            this.btnStartAddComment.UseVisualStyleBackColor = false;
            this.btnStartAddComment.Click += new System.EventHandler(this.btnRec_Click);
            // 
            // btnDeleteComment
            // 
            resources.ApplyResources(this.btnDeleteComment, "btnDeleteComment");
            this.btnDeleteComment.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDeleteComment.Name = "btnDeleteComment";
            this.btnDeleteComment.UseVisualStyleBackColor = true;
            this.btnDeleteComment.Click += new System.EventHandler(this.btnDeleteComment_Click);
            // 
            // btnPlaySelCmt
            // 
            resources.ApplyResources(this.btnPlaySelCmt, "btnPlaySelCmt");
            this.btnPlaySelCmt.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPlaySelCmt.Name = "btnPlaySelCmt";
            this.btnPlaySelCmt.UseVisualStyleBackColor = true;
            this.btnPlaySelCmt.Click += new System.EventHandler(this.btnPlaySelCmt_Click);
            // 
            // lblDuration
            // 
            resources.ApplyResources(this.lblDuration, "lblDuration");
            this.lblDuration.BackColor = System.Drawing.Color.Transparent;
            this.lblDuration.Name = "lblDuration";
            // 
            // txtDescrip
            // 
            resources.ApplyResources(this.txtDescrip, "txtDescrip");
            this.txtDescrip.Name = "txtDescrip";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Name = "label3";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Name = "label2";
            // 
            // numCommentTime
            // 
            this.numCommentTime.DecimalPlaces = 2;
            resources.ApplyResources(this.numCommentTime, "numCommentTime");
            this.numCommentTime.Name = "numCommentTime";
            this.numCommentTime.ValueChanged += new System.EventHandler(this.numCommentTime_ValueChanged);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Name = "label1";
            // 
            // btnSaveWithCmt
            // 
            this.btnSaveWithCmt.BackColor = System.Drawing.Color.Transparent;
            this.btnSaveWithCmt.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSaveWithCmt.Image = global::PratiCanto.Properties.Resources.saveRaw;
            resources.ApplyResources(this.btnSaveWithCmt, "btnSaveWithCmt");
            this.btnSaveWithCmt.Name = "btnSaveWithCmt";
            this.btnSaveWithCmt.UseVisualStyleBackColor = false;
            this.btnSaveWithCmt.Click += new System.EventHandler(this.btnSaveWithCmt_Click);
            // 
            // lblComments
            // 
            resources.ApplyResources(this.lblComments, "lblComments");
            this.lblComments.BackColor = System.Drawing.Color.Transparent;
            this.lblComments.Name = "lblComments";
            // 
            // lstComments
            // 
            resources.ApplyResources(this.lstComments, "lstComments");
            this.lstComments.FormattingEnabled = true;
            this.lstComments.Name = "lstComments";
            this.lstComments.SelectedIndexChanged += new System.EventHandler(this.lstComments_SelectedIndexChanged);
            // 
            // picRec
            // 
            resources.ApplyResources(this.picRec, "picRec");
            this.picRec.Name = "picRec";
            this.picRec.TabStop = false;
            // 
            // picStop
            // 
            resources.ApplyResources(this.picStop, "picStop");
            this.picStop.Name = "picStop";
            this.picStop.TabStop = false;
            // 
            // picPlay
            // 
            resources.ApplyResources(this.picPlay, "picPlay");
            this.picPlay.Name = "picPlay";
            this.picPlay.TabStop = false;
            // 
            // frmComposer
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.gbAudioCom);
            this.Controls.Add(this.gbChooseFileOpen);
            this.Controls.Add(this.picRec);
            this.Controls.Add(this.picStop);
            this.Controls.Add(this.picPlay);
            this.Controls.Add(this.toolStrip1);
            this.Name = "frmComposer";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmComposer_FormClosing);
            this.Load += new System.EventHandler(this.frmSelfListen_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.gbChooseFileOpen.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picComposition)).EndInit();
            this.gbAudioCom.ResumeLayout(false);
            this.gbAudioCom.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numVolAudio)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numVolComment)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCommentTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picRec)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picStop)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picPlay)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnClient;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton btnStartStop;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton btnRec;
        private System.Windows.Forms.ToolStripButton btnPlay;
        private System.Windows.Forms.ToolStripButton btnSaveRecord;
        private System.Windows.Forms.ToolStripButton btnOpenFile;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripLabel lblNote;
        private System.Windows.Forms.ToolStripTextBox txtAnnotation;
        private System.Windows.Forms.PictureBox picStop;
        private System.Windows.Forms.PictureBox picPlay;
        private System.Windows.Forms.PictureBox picRec;
        private System.Windows.Forms.GroupBox gbChooseFileOpen;
        private System.Windows.Forms.PictureBox picComposition;
        private System.Windows.Forms.Button btnStopOrigFile;
        private System.Windows.Forms.Button btnPauseOrigFile;
        private System.Windows.Forms.Button btnOpenFileComment;
        private System.Windows.Forms.Button btnPlayOrigFile;
        private System.Windows.Forms.GroupBox gbAudioCom;
        private System.Windows.Forms.ListBox lstComments;
        private System.Windows.Forms.Label lblComments;
        private System.Windows.Forms.Label lblDuration;
        private System.Windows.Forms.TextBox txtDescrip;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numCommentTime;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSaveWithCmt;
        private System.Windows.Forms.Button btnStartAddComment;
        private System.Windows.Forms.ComboBox cmbInputDevice;
        private System.Windows.Forms.Button btnPlaySelCmt;
        private System.Windows.Forms.Button btnDeleteComment;
        private System.Windows.Forms.CheckBox chkVoiceOver;
        private System.Windows.Forms.NumericUpDown numVolAudio;
        private System.Windows.Forms.NumericUpDown numVolComment;
        private System.Windows.Forms.Label lblAudioVol;
        private System.Windows.Forms.Label lblCommVol;
        private System.Windows.Forms.Label lblSaveInfo;
    }
}