namespace PratiCanto
{
    partial class frmSpeechRecognizer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSpeechRecognizer));
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
            this.txtAns = new System.Windows.Forms.TextBox();
            this.btnFromAudio = new System.Windows.Forms.Button();
            this.bgW = new System.ComponentModel.BackgroundWorker();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblCurIntens = new System.Windows.Forms.Label();
            this.numIntensThresh = new System.Windows.Forms.NumericUpDown();
            this.btnPushToTalk = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.lblInQueue = new System.Windows.Forms.Label();
            this.chkContinuousRecog = new System.Windows.Forms.CheckBox();
            this.timerContRecog = new System.Windows.Forms.Timer(this.components);
            this.label4 = new System.Windows.Forms.Label();
            this.numReqInterval = new System.Windows.Forms.NumericUpDown();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtHints = new System.Windows.Forms.TextBox();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picStop)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picPlay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picRec)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numIntensThresh)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numReqInterval)).BeginInit();
            this.groupBox1.SuspendLayout();
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
            // picStop
            // 
            this.picStop.Image = ((System.Drawing.Image)(resources.GetObject("picStop.Image")));
            this.picStop.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.picStop.Location = new System.Drawing.Point(419, 36);
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
            this.picPlay.Location = new System.Drawing.Point(455, 36);
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
            this.picRec.Location = new System.Drawing.Point(380, 37);
            this.picRec.Name = "picRec";
            this.picRec.Size = new System.Drawing.Size(30, 30);
            this.picRec.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.picRec.TabIndex = 20;
            this.picRec.TabStop = false;
            this.picRec.Visible = false;
            // 
            // txtAns
            // 
            this.txtAns.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtAns.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAns.Location = new System.Drawing.Point(261, 95);
            this.txtAns.Margin = new System.Windows.Forms.Padding(2);
            this.txtAns.Multiline = true;
            this.txtAns.Name = "txtAns";
            this.txtAns.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtAns.Size = new System.Drawing.Size(416, 170);
            this.txtAns.TabIndex = 21;
            // 
            // btnFromAudio
            // 
            this.btnFromAudio.Location = new System.Drawing.Point(109, 62);
            this.btnFromAudio.Margin = new System.Windows.Forms.Padding(2);
            this.btnFromAudio.Name = "btnFromAudio";
            this.btnFromAudio.Size = new System.Drawing.Size(113, 27);
            this.btnFromAudio.TabIndex = 23;
            this.btnFromAudio.Text = "From audio file...";
            this.btnFromAudio.UseVisualStyleBackColor = true;
            this.btnFromAudio.Click += new System.EventHandler(this.btnFromAudio_Click);
            // 
            // bgW
            // 
            this.bgW.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgW_DoWork);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(441, 49);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(110, 13);
            this.label1.TabIndex = 24;
            this.label1.Text = "Current intensity (dBr):";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(441, 70);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(120, 13);
            this.label2.TabIndex = 24;
            this.label2.Text = "Intensity threshold (dBr):";
            // 
            // lblCurIntens
            // 
            this.lblCurIntens.AutoSize = true;
            this.lblCurIntens.Location = new System.Drawing.Point(567, 49);
            this.lblCurIntens.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblCurIntens.Name = "lblCurIntens";
            this.lblCurIntens.Size = new System.Drawing.Size(13, 13);
            this.lblCurIntens.TabIndex = 24;
            this.lblCurIntens.Text = "0";
            // 
            // numIntensThresh
            // 
            this.numIntensThresh.DecimalPlaces = 2;
            this.numIntensThresh.Location = new System.Drawing.Point(567, 68);
            this.numIntensThresh.Margin = new System.Windows.Forms.Padding(2);
            this.numIntensThresh.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numIntensThresh.Minimum = new decimal(new int[] {
            40,
            0,
            0,
            -2147483648});
            this.numIntensThresh.Name = "numIntensThresh";
            this.numIntensThresh.Size = new System.Drawing.Size(62, 20);
            this.numIntensThresh.TabIndex = 25;
            this.numIntensThresh.Value = new decimal(new int[] {
            120,
            0,
            0,
            0});
            this.numIntensThresh.ValueChanged += new System.EventHandler(this.numIntensThresh_ValueChanged);
            // 
            // btnPushToTalk
            // 
            this.btnPushToTalk.Location = new System.Drawing.Point(8, 62);
            this.btnPushToTalk.Margin = new System.Windows.Forms.Padding(2);
            this.btnPushToTalk.Name = "btnPushToTalk";
            this.btnPushToTalk.Size = new System.Drawing.Size(97, 27);
            this.btnPushToTalk.TabIndex = 26;
            this.btnPushToTalk.Text = "Click-to-talk";
            this.btnPushToTalk.UseVisualStyleBackColor = true;
            this.btnPushToTalk.Click += new System.EventHandler(this.btnPushToTalk_Click);
            this.btnPushToTalk.KeyDown += new System.Windows.Forms.KeyEventHandler(this.btnPushToTalk_KeyDown);
            this.btnPushToTalk.KeyUp += new System.Windows.Forms.KeyEventHandler(this.btnPushToTalk_KeyUp);
            this.btnPushToTalk.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnPushToTalk_MouseDown);
            this.btnPushToTalk.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnPushToTalk_MouseUp);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(348, 55);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 13);
            this.label3.TabIndex = 24;
            this.label3.Text = "In queue:";
            // 
            // lblInQueue
            // 
            this.lblInQueue.AutoSize = true;
            this.lblInQueue.Location = new System.Drawing.Point(416, 55);
            this.lblInQueue.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblInQueue.Name = "lblInQueue";
            this.lblInQueue.Size = new System.Drawing.Size(13, 13);
            this.lblInQueue.TabIndex = 24;
            this.lblInQueue.Text = "0";
            // 
            // chkContinuousRecog
            // 
            this.chkContinuousRecog.AutoSize = true;
            this.chkContinuousRecog.Location = new System.Drawing.Point(9, 37);
            this.chkContinuousRecog.Margin = new System.Windows.Forms.Padding(2);
            this.chkContinuousRecog.Name = "chkContinuousRecog";
            this.chkContinuousRecog.Size = new System.Drawing.Size(134, 17);
            this.chkContinuousRecog.TabIndex = 27;
            this.chkContinuousRecog.Text = "Continuous recognition";
            this.chkContinuousRecog.UseVisualStyleBackColor = true;
            this.chkContinuousRecog.CheckedChanged += new System.EventHandler(this.chkContinuousRecog_CheckedChanged);
            // 
            // timerContRecog
            // 
            this.timerContRecog.Interval = 30000;
            this.timerContRecog.Tick += new System.EventHandler(this.timerContRecog_Tick);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(155, 38);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(101, 13);
            this.label4.TabIndex = 28;
            this.label4.Text = "Request interval (s):";
            // 
            // numReqInterval
            // 
            this.numReqInterval.Location = new System.Drawing.Point(259, 37);
            this.numReqInterval.Margin = new System.Windows.Forms.Padding(2);
            this.numReqInterval.Maximum = new decimal(new int[] {
            80,
            0,
            0,
            0});
            this.numReqInterval.Minimum = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.numReqInterval.Name = "numReqInterval";
            this.numReqInterval.Size = new System.Drawing.Size(48, 20);
            this.numReqInterval.TabIndex = 29;
            this.numReqInterval.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.numReqInterval.ValueChanged += new System.EventHandler(this.numReqInterval_ValueChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox1.Controls.Add(this.txtHints);
            this.groupBox1.Location = new System.Drawing.Point(8, 95);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(248, 163);
            this.groupBox1.TabIndex = 30;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Hints (1 phrase per line)";
            // 
            // txtHints
            // 
            this.txtHints.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtHints.Location = new System.Drawing.Point(7, 20);
            this.txtHints.Multiline = true;
            this.txtHints.Name = "txtHints";
            this.txtHints.Size = new System.Drawing.Size(235, 137);
            this.txtHints.TabIndex = 0;
            this.txtHints.Text = "praticanto\r\nboa tarde";
            this.txtHints.TextChanged += new System.EventHandler(this.txtHints_TextChanged);
            // 
            // frmSpeechRecognizer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(687, 270);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.numReqInterval);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.chkContinuousRecog);
            this.Controls.Add(this.btnPushToTalk);
            this.Controls.Add(this.numIntensThresh);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblInQueue);
            this.Controls.Add(this.lblCurIntens);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnFromAudio);
            this.Controls.Add(this.txtAns);
            this.Controls.Add(this.picRec);
            this.Controls.Add(this.picStop);
            this.Controls.Add(this.picPlay);
            this.Controls.Add(this.toolStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "frmSpeechRecognizer";
            this.Text = "Speech Recognition";
            this.Load += new System.EventHandler(this.frmSelfListen_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picStop)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picPlay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picRec)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numIntensThresh)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numReqInterval)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
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
        private System.Windows.Forms.TextBox txtAns;
        private System.Windows.Forms.Button btnFromAudio;
        private System.ComponentModel.BackgroundWorker bgW;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblCurIntens;
        private System.Windows.Forms.NumericUpDown numIntensThresh;
        private System.Windows.Forms.Button btnPushToTalk;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblInQueue;
        private System.Windows.Forms.CheckBox chkContinuousRecog;
        private System.Windows.Forms.Timer timerContRecog;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown numReqInterval;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtHints;
    }
}