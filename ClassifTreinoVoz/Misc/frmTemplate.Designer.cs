namespace PratiCanto.Template
{
    partial class frmTemplate
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmTemplate));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnClient = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.btnStartStop = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.cmbInputDevice = new System.Windows.Forms.ToolStripComboBox();
            this.btnRec = new System.Windows.Forms.ToolStripButton();
            this.btnPlay = new System.Windows.Forms.ToolStripButton();
            this.btnSaveRecord = new System.Windows.Forms.ToolStripButton();
            this.btnOpenFile = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.lblNote = new System.Windows.Forms.ToolStripLabel();
            this.txtAnnotation = new System.Windows.Forms.ToolStripTextBox();
            this.picStop = new System.Windows.Forms.PictureBox();
            this.picPlay = new System.Windows.Forms.PictureBox();
            this.picRec = new System.Windows.Forms.PictureBox();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picStop)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picPlay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picRec)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnClient,
            this.toolStripSeparator3,
            this.btnStartStop,
            this.toolStripSeparator2,
            this.cmbInputDevice,
            this.btnRec,
            this.btnPlay,
            this.btnSaveRecord,
            this.btnOpenFile,
            this.toolStripSeparator1,
            this.lblNote,
            this.txtAnnotation});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1031, 45);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnClient
            // 
            this.btnClient.AutoSize = false;
            this.btnClient.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnClient.Image = ((System.Drawing.Image)(resources.GetObject("btnClient.Image")));
            this.btnClient.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnClient.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnClient.Name = "btnClient";
            this.btnClient.Size = new System.Drawing.Size(36, 36);
            this.btnClient.Text = "Play";
            this.btnClient.ToolTipText = "Select client";
            this.btnClient.Click += new System.EventHandler(this.btnClient_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 45);
            // 
            // btnStartStop
            // 
            this.btnStartStop.BackColor = System.Drawing.Color.Lime;
            this.btnStartStop.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnStartStop.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Bold);
            this.btnStartStop.Image = ((System.Drawing.Image)(resources.GetObject("btnStartStop.Image")));
            this.btnStartStop.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnStartStop.Name = "btnStartStop";
            this.btnStartStop.Size = new System.Drawing.Size(227, 42);
            this.btnStartStop.Text = "Start / stop test";
            this.btnStartStop.Click += new System.EventHandler(this.btnStartStop_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 45);
            // 
            // cmbInputDevice
            // 
            this.cmbInputDevice.Name = "cmbInputDevice";
            this.cmbInputDevice.Size = new System.Drawing.Size(224, 45);
            // 
            // btnRec
            // 
            this.btnRec.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnRec.Image = ((System.Drawing.Image)(resources.GetObject("btnRec.Image")));
            this.btnRec.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnRec.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnRec.Name = "btnRec";
            this.btnRec.Size = new System.Drawing.Size(34, 42);
            this.btnRec.ToolTipText = "Start/stop recording";
            // 
            // btnPlay
            // 
            this.btnPlay.AutoSize = false;
            this.btnPlay.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnPlay.Image = ((System.Drawing.Image)(resources.GetObject("btnPlay.Image")));
            this.btnPlay.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnPlay.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnPlay.Name = "btnPlay";
            this.btnPlay.Size = new System.Drawing.Size(36, 36);
            this.btnPlay.Text = "Play";
            this.btnPlay.ToolTipText = "Selection play/stop";
            // 
            // btnSaveRecord
            // 
            this.btnSaveRecord.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnSaveRecord.Image = ((System.Drawing.Image)(resources.GetObject("btnSaveRecord.Image")));
            this.btnSaveRecord.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnSaveRecord.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSaveRecord.Name = "btnSaveRecord";
            this.btnSaveRecord.Size = new System.Drawing.Size(36, 42);
            this.btnSaveRecord.ToolTipText = "Save current audio to file";
            // 
            // btnOpenFile
            // 
            this.btnOpenFile.AutoSize = false;
            this.btnOpenFile.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnOpenFile.Image = ((System.Drawing.Image)(resources.GetObject("btnOpenFile.Image")));
            this.btnOpenFile.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnOpenFile.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnOpenFile.Name = "btnOpenFile";
            this.btnOpenFile.Size = new System.Drawing.Size(36, 36);
            this.btnOpenFile.Text = "Replay recorded audio";
            this.btnOpenFile.ToolTipText = "Replay recorded audio";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 45);
            // 
            // lblNote
            // 
            this.lblNote.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold);
            this.lblNote.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.lblNote.Name = "lblNote";
            this.lblNote.Size = new System.Drawing.Size(33, 42);
            this.lblNote.Text = "  ";
            // 
            // txtAnnotation
            // 
            this.txtAnnotation.Font = new System.Drawing.Font("Segoe UI", 14.25F);
            this.txtAnnotation.Name = "txtAnnotation";
            this.txtAnnotation.Size = new System.Drawing.Size(223, 45);
            this.txtAnnotation.Text = "Notation";
            this.txtAnnotation.ToolTipText = "Phonetic notation";
            this.txtAnnotation.Visible = false;
            // 
            // picStop
            // 
            this.picStop.Image = ((System.Drawing.Image)(resources.GetObject("picStop.Image")));
            this.picStop.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.picStop.Location = new System.Drawing.Point(273, 110);
            this.picStop.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.picStop.Name = "picStop";
            this.picStop.Size = new System.Drawing.Size(32, 32);
            this.picStop.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.picStop.TabIndex = 19;
            this.picStop.TabStop = false;
            this.picStop.Visible = false;
            // 
            // picPlay
            // 
            this.picPlay.Image = ((System.Drawing.Image)(resources.GetObject("picPlay.Image")));
            this.picPlay.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.picPlay.Location = new System.Drawing.Point(328, 110);
            this.picPlay.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.picPlay.Name = "picPlay";
            this.picPlay.Size = new System.Drawing.Size(32, 32);
            this.picPlay.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.picPlay.TabIndex = 18;
            this.picPlay.TabStop = false;
            this.picPlay.Visible = false;
            // 
            // picRec
            // 
            this.picRec.Image = ((System.Drawing.Image)(resources.GetObject("picRec.Image")));
            this.picRec.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.picRec.Location = new System.Drawing.Point(215, 112);
            this.picRec.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.picRec.Name = "picRec";
            this.picRec.Size = new System.Drawing.Size(30, 30);
            this.picRec.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.picRec.TabIndex = 20;
            this.picRec.TabStop = false;
            this.picRec.Visible = false;
            // 
            // frmTemplate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1031, 415);
            this.Controls.Add(this.picRec);
            this.Controls.Add(this.picStop);
            this.Controls.Add(this.picPlay);
            this.Controls.Add(this.toolStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmTemplate";
            this.Text = "Template";
            this.Load += new System.EventHandler(this.frmSelfListen_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picStop)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picPlay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picRec)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnClient;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton btnStartStop;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripComboBox cmbInputDevice;
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
    }
}