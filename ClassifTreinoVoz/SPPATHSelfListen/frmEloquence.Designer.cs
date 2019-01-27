namespace PratiCanto
{
    partial class frmEloquence
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmEloquence));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnClient = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.btnOpenFile = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.btnStartStop = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.cmbInputDevice = new System.Windows.Forms.ToolStripComboBox();
            this.btnRec = new System.Windows.Forms.ToolStripButton();
            this.btnPlay = new System.Windows.Forms.ToolStripButton();
            this.btnSaveRecord = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.lblNote = new System.Windows.Forms.ToolStripLabel();
            this.txtAnnotation = new System.Windows.Forms.ToolStripTextBox();
            this.lblFPS = new System.Windows.Forms.ToolStripLabel();
            this.picStop = new System.Windows.Forms.PictureBox();
            this.picPlay = new System.Windows.Forms.PictureBox();
            this.picRec = new System.Windows.Forms.PictureBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.numTolerance = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.lblIdentWord = new System.Windows.Forms.Label();
            this.lstAnnotations = new System.Windows.Forms.ListBox();
            this.bWIdentify = new System.ComponentModel.BackgroundWorker();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picStop)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picPlay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picRec)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numTolerance)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnClient,
            this.toolStripSeparator3,
            this.btnOpenFile,
            this.toolStripSeparator4,
            this.btnStartStop,
            this.toolStripSeparator2,
            this.cmbInputDevice,
            this.btnRec,
            this.btnPlay,
            this.btnSaveRecord,
            this.toolStripSeparator1,
            this.lblNote,
            this.txtAnnotation,
            this.lblFPS});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(687, 39);
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
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 39);
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
            this.btnOpenFile.Text = "Open reference";
            this.btnOpenFile.ToolTipText = "Open reference";
            this.btnOpenFile.Click += new System.EventHandler(this.btnOpenFile_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 39);
            // 
            // btnStartStop
            // 
            this.btnStartStop.BackColor = System.Drawing.Color.Lime;
            this.btnStartStop.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnStartStop.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Bold);
            this.btnStartStop.Image = ((System.Drawing.Image)(resources.GetObject("btnStartStop.Image")));
            this.btnStartStop.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnStartStop.Name = "btnStartStop";
            this.btnStartStop.Size = new System.Drawing.Size(117, 36);
            this.btnStartStop.Text = "Start / stop";
            this.btnStartStop.Click += new System.EventHandler(this.btnRec_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 39);
            // 
            // cmbInputDevice
            // 
            this.cmbInputDevice.Name = "cmbInputDevice";
            this.cmbInputDevice.Size = new System.Drawing.Size(151, 39);
            // 
            // btnRec
            // 
            this.btnRec.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnRec.Image = ((System.Drawing.Image)(resources.GetObject("btnRec.Image")));
            this.btnRec.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnRec.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnRec.Name = "btnRec";
            this.btnRec.Size = new System.Drawing.Size(34, 36);
            this.btnRec.ToolTipText = "Start/stop recording";
            this.btnRec.Visible = false;
            this.btnRec.Click += new System.EventHandler(this.btnRec_Click);
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
            this.btnPlay.Visible = false;
            // 
            // btnSaveRecord
            // 
            this.btnSaveRecord.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnSaveRecord.Image = ((System.Drawing.Image)(resources.GetObject("btnSaveRecord.Image")));
            this.btnSaveRecord.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnSaveRecord.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSaveRecord.Name = "btnSaveRecord";
            this.btnSaveRecord.Size = new System.Drawing.Size(36, 36);
            this.btnSaveRecord.ToolTipText = "Save current audio to file";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 39);
            // 
            // lblNote
            // 
            this.lblNote.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold);
            this.lblNote.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.lblNote.Name = "lblNote";
            this.lblNote.Size = new System.Drawing.Size(22, 36);
            this.lblNote.Text = "  ";
            // 
            // txtAnnotation
            // 
            this.txtAnnotation.Font = new System.Drawing.Font("Segoe UI", 14.25F);
            this.txtAnnotation.Name = "txtAnnotation";
            this.txtAnnotation.Size = new System.Drawing.Size(150, 39);
            this.txtAnnotation.Text = "Notation";
            this.txtAnnotation.ToolTipText = "Phonetic notation";
            this.txtAnnotation.Visible = false;
            // 
            // lblFPS
            // 
            this.lblFPS.Name = "lblFPS";
            this.lblFPS.Size = new System.Drawing.Size(13, 36);
            this.lblFPS.Text = "0";
            // 
            // picStop
            // 
            this.picStop.Image = ((System.Drawing.Image)(resources.GetObject("picStop.Image")));
            this.picStop.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.picStop.Location = new System.Drawing.Point(57, 98);
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
            this.picPlay.Location = new System.Drawing.Point(94, 98);
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
            this.picRec.Location = new System.Drawing.Point(19, 99);
            this.picRec.Name = "picRec";
            this.picRec.Size = new System.Drawing.Size(30, 30);
            this.picRec.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.picRec.TabIndex = 20;
            this.picRec.TabStop = false;
            this.picRec.Visible = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.numTolerance);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.lblIdentWord);
            this.groupBox1.Controls.Add(this.picRec);
            this.groupBox1.Controls.Add(this.picStop);
            this.groupBox1.Controls.Add(this.picPlay);
            this.groupBox1.Controls.Add(this.lstAnnotations);
            this.groupBox1.Location = new System.Drawing.Point(8, 40);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox1.Size = new System.Drawing.Size(665, 222);
            this.groupBox1.TabIndex = 21;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Annotations";
            // 
            // numTolerance
            // 
            this.numTolerance.DecimalPlaces = 2;
            this.numTolerance.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.numTolerance.Location = new System.Drawing.Point(71, 26);
            this.numTolerance.Margin = new System.Windows.Forms.Padding(2);
            this.numTolerance.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numTolerance.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            -2147483648});
            this.numTolerance.Name = "numTolerance";
            this.numTolerance.Size = new System.Drawing.Size(45, 20);
            this.numTolerance.TabIndex = 23;
            this.numTolerance.Value = new decimal(new int[] {
            2,
            0,
            0,
            131072});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 26);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 22;
            this.label1.Text = "Tolerance:";
            // 
            // lblIdentWord
            // 
            this.lblIdentWord.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblIdentWord.Font = new System.Drawing.Font("Microsoft Sans Serif", 28F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblIdentWord.Location = new System.Drawing.Point(175, 51);
            this.lblIdentWord.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblIdentWord.Name = "lblIdentWord";
            this.lblIdentWord.Size = new System.Drawing.Size(469, 144);
            this.lblIdentWord.TabIndex = 21;
            // 
            // lstAnnotations
            // 
            this.lstAnnotations.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lstAnnotations.FormattingEnabled = true;
            this.lstAnnotations.Location = new System.Drawing.Point(12, 51);
            this.lstAnnotations.Margin = new System.Windows.Forms.Padding(2);
            this.lstAnnotations.Name = "lstAnnotations";
            this.lstAnnotations.Size = new System.Drawing.Size(105, 160);
            this.lstAnnotations.TabIndex = 0;
            // 
            // bWIdentify
            // 
            this.bWIdentify.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bWIdentify_DoWork);
            // 
            // frmEloquence
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(687, 270);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.toolStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "frmEloquence";
            this.Text = "Eloquence";
            this.Load += new System.EventHandler(this.frmEloquence_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picStop)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picPlay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picRec)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numTolerance)).EndInit();
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
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.ComponentModel.BackgroundWorker bWIdentify;
        private System.Windows.Forms.ToolStripLabel lblFPS;
        private System.Windows.Forms.ListBox lstAnnotations;
        private System.Windows.Forms.Label lblIdentWord;
        private System.Windows.Forms.NumericUpDown numTolerance;
        private System.Windows.Forms.Label label1;
    }
}