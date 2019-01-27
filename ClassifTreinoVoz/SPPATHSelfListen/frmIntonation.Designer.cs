namespace PratiCanto
{
    partial class frmIntonation
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmIntonation));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtSpeakText = new System.Windows.Forms.TextBox();
            this.gbIntonAnalysis = new System.Windows.Forms.GroupBox();
            this.btnStopOrigFile = new System.Windows.Forms.Button();
            this.btnSaveAll = new System.Windows.Forms.Button();
            this.btnOpenFileInton = new System.Windows.Forms.Button();
            this.btnReplayPrevAudio = new System.Windows.Forms.Button();
            this.cmbReplayTogether = new System.Windows.Forms.ComboBox();
            this.cmbReplay = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cmbInputDevice = new System.Windows.Forms.ComboBox();
            this.btnStartTest = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.numMaxTime = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.numMinFreq = new System.Windows.Forms.NumericUpDown();
            this.numMaxFreq = new System.Windows.Forms.NumericUpDown();
            this.numMinIntens = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.picIntonation = new System.Windows.Forms.PictureBox();
            this.btnDelCurves = new System.Windows.Forms.Button();
            this.timerSample = new System.Windows.Forms.Timer(this.components);
            this.btnClient = new System.Windows.Forms.Button();
            this.picRec = new System.Windows.Forms.PictureBox();
            this.picStop = new System.Windows.Forms.PictureBox();
            this.picPlay = new System.Windows.Forms.PictureBox();
            this.toolStrip1.SuspendLayout();
            this.gbIntonAnalysis.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numMaxTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMinFreq)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMaxFreq)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMinIntens)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picIntonation)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picRec)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picStop)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picPlay)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
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
            this.btnStartStop.Click += new System.EventHandler(this.btnRec_Click);
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
            this.btnPlay.Click += new System.EventHandler(this.btnPlay_Click);
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
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // txtSpeakText
            // 
            resources.ApplyResources(this.txtSpeakText, "txtSpeakText");
            this.txtSpeakText.Name = "txtSpeakText";
            // 
            // gbIntonAnalysis
            // 
            resources.ApplyResources(this.gbIntonAnalysis, "gbIntonAnalysis");
            this.gbIntonAnalysis.Controls.Add(this.btnStopOrigFile);
            this.gbIntonAnalysis.Controls.Add(this.btnSaveAll);
            this.gbIntonAnalysis.Controls.Add(this.btnOpenFileInton);
            this.gbIntonAnalysis.Controls.Add(this.btnReplayPrevAudio);
            this.gbIntonAnalysis.Controls.Add(this.cmbReplayTogether);
            this.gbIntonAnalysis.Controls.Add(this.cmbReplay);
            this.gbIntonAnalysis.Controls.Add(this.label6);
            this.gbIntonAnalysis.Controls.Add(this.cmbInputDevice);
            this.gbIntonAnalysis.Controls.Add(this.btnStartTest);
            this.gbIntonAnalysis.Controls.Add(this.label5);
            this.gbIntonAnalysis.Controls.Add(this.label4);
            this.gbIntonAnalysis.Controls.Add(this.numMaxTime);
            this.gbIntonAnalysis.Controls.Add(this.label3);
            this.gbIntonAnalysis.Controls.Add(this.numMinFreq);
            this.gbIntonAnalysis.Controls.Add(this.numMaxFreq);
            this.gbIntonAnalysis.Controls.Add(this.numMinIntens);
            this.gbIntonAnalysis.Controls.Add(this.label2);
            this.gbIntonAnalysis.Controls.Add(this.picIntonation);
            this.gbIntonAnalysis.Controls.Add(this.btnDelCurves);
            this.gbIntonAnalysis.Name = "gbIntonAnalysis";
            this.gbIntonAnalysis.TabStop = false;
            this.gbIntonAnalysis.Paint += new System.Windows.Forms.PaintEventHandler(this.gbIntonAnalysis_Paint);
            // 
            // btnStopOrigFile
            // 
            this.btnStopOrigFile.Cursor = System.Windows.Forms.Cursors.Hand;
            resources.ApplyResources(this.btnStopOrigFile, "btnStopOrigFile");
            this.btnStopOrigFile.Name = "btnStopOrigFile";
            this.btnStopOrigFile.UseVisualStyleBackColor = true;
            this.btnStopOrigFile.Click += new System.EventHandler(this.btnStopOrigFile_Click);
            // 
            // btnSaveAll
            // 
            this.btnSaveAll.BackColor = System.Drawing.Color.Transparent;
            this.btnSaveAll.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSaveAll.Image = global::PratiCanto.Properties.Resources.saveRaw;
            resources.ApplyResources(this.btnSaveAll, "btnSaveAll");
            this.btnSaveAll.Name = "btnSaveAll";
            this.btnSaveAll.UseVisualStyleBackColor = false;
            this.btnSaveAll.Click += new System.EventHandler(this.btnSaveAll_Click);
            // 
            // btnOpenFileInton
            // 
            this.btnOpenFileInton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnOpenFileInton.Image = global::PratiCanto.Properties.Resources.Openfolder_orange;
            resources.ApplyResources(this.btnOpenFileInton, "btnOpenFileInton");
            this.btnOpenFileInton.Name = "btnOpenFileInton";
            this.btnOpenFileInton.UseVisualStyleBackColor = true;
            this.btnOpenFileInton.Click += new System.EventHandler(this.btnOpenFileInton_Click);
            // 
            // btnReplayPrevAudio
            // 
            this.btnReplayPrevAudio.Cursor = System.Windows.Forms.Cursors.Hand;
            resources.ApplyResources(this.btnReplayPrevAudio, "btnReplayPrevAudio");
            this.btnReplayPrevAudio.Name = "btnReplayPrevAudio";
            this.btnReplayPrevAudio.UseVisualStyleBackColor = true;
            this.btnReplayPrevAudio.Click += new System.EventHandler(this.btnReplayPrevAudio_Click);
            // 
            // cmbReplayTogether
            // 
            this.cmbReplayTogether.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.cmbReplayTogether, "cmbReplayTogether");
            this.cmbReplayTogether.FormattingEnabled = true;
            this.cmbReplayTogether.Name = "cmbReplayTogether";
            // 
            // cmbReplay
            // 
            this.cmbReplay.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.cmbReplay, "cmbReplay");
            this.cmbReplay.FormattingEnabled = true;
            this.cmbReplay.Name = "cmbReplay";
            this.cmbReplay.SelectedIndexChanged += new System.EventHandler(this.cmbReplay_SelectedIndexChanged);
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Name = "label6";
            // 
            // cmbInputDevice
            // 
            this.cmbInputDevice.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.cmbInputDevice, "cmbInputDevice");
            this.cmbInputDevice.FormattingEnabled = true;
            this.cmbInputDevice.Name = "cmbInputDevice";
            // 
            // btnStartTest
            // 
            this.btnStartTest.BackColor = System.Drawing.Color.Transparent;
            this.btnStartTest.Cursor = System.Windows.Forms.Cursors.Hand;
            resources.ApplyResources(this.btnStartTest, "btnStartTest");
            this.btnStartTest.ForeColor = System.Drawing.Color.White;
            this.btnStartTest.Image = global::PratiCanto.Properties.Resources.startTest;
            this.btnStartTest.Name = "btnStartTest";
            this.btnStartTest.UseVisualStyleBackColor = false;
            this.btnStartTest.Click += new System.EventHandler(this.btnRec_Click);
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Name = "label5";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Name = "label4";
            // 
            // numMaxTime
            // 
            resources.ApplyResources(this.numMaxTime, "numMaxTime");
            this.numMaxTime.Maximum = new decimal(new int[] {
            1000000000,
            0,
            0,
            0});
            this.numMaxTime.Minimum = new decimal(new int[] {
            20,
            0,
            0,
            -2147483648});
            this.numMaxTime.Name = "numMaxTime";
            this.numMaxTime.Value = new decimal(new int[] {
            7,
            0,
            0,
            0});
            this.numMaxTime.ValueChanged += new System.EventHandler(this.numMaxTime_ValueChanged);
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Name = "label3";
            // 
            // numMinFreq
            // 
            resources.ApplyResources(this.numMinFreq, "numMinFreq");
            this.numMinFreq.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numMinFreq.Minimum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.numMinFreq.Name = "numMinFreq";
            this.numMinFreq.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.numMinFreq.ValueChanged += new System.EventHandler(this.numMinFreq_ValueChanged);
            // 
            // numMaxFreq
            // 
            resources.ApplyResources(this.numMaxFreq, "numMaxFreq");
            this.numMaxFreq.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numMaxFreq.Minimum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.numMaxFreq.Name = "numMaxFreq";
            this.numMaxFreq.Value = new decimal(new int[] {
            400,
            0,
            0,
            0});
            this.numMaxFreq.ValueChanged += new System.EventHandler(this.numMaxFreq_ValueChanged);
            // 
            // numMinIntens
            // 
            resources.ApplyResources(this.numMinIntens, "numMinIntens");
            this.numMinIntens.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numMinIntens.Minimum = new decimal(new int[] {
            20,
            0,
            0,
            -2147483648});
            this.numMinIntens.Name = "numMinIntens";
            this.numMinIntens.Value = new decimal(new int[] {
            4,
            0,
            0,
            0});
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Name = "label2";
            // 
            // picIntonation
            // 
            resources.ApplyResources(this.picIntonation, "picIntonation");
            this.picIntonation.BackgroundImage = global::PratiCanto.Properties.Resources.logowith_bgSuave2;
            this.picIntonation.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picIntonation.Name = "picIntonation";
            this.picIntonation.TabStop = false;
            this.picIntonation.Paint += new System.Windows.Forms.PaintEventHandler(this.picIntonation_Paint);
            this.picIntonation.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picIntonation_MouseDown);
            // 
            // btnDelCurves
            // 
            this.btnDelCurves.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDelCurves.Image = global::PratiCanto.Properties.Resources.trash;
            resources.ApplyResources(this.btnDelCurves, "btnDelCurves");
            this.btnDelCurves.Name = "btnDelCurves";
            this.btnDelCurves.UseVisualStyleBackColor = true;
            this.btnDelCurves.Click += new System.EventHandler(this.txtDelCurves_Click);
            // 
            // timerSample
            // 
            this.timerSample.Enabled = true;
            this.timerSample.Interval = 5;
            this.timerSample.Tick += new System.EventHandler(this.timerSample_Tick);
            // 
            // btnClient
            // 
            this.btnClient.BackColor = System.Drawing.Color.Transparent;
            this.btnClient.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClient.Image = global::PratiCanto.Properties.Resources.DefaultClient;
            resources.ApplyResources(this.btnClient, "btnClient");
            this.btnClient.Name = "btnClient";
            this.btnClient.UseVisualStyleBackColor = false;
            this.btnClient.Click += new System.EventHandler(this.btnClient_Click);
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
            // frmIntonation
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.btnClient);
            this.Controls.Add(this.gbIntonAnalysis);
            this.Controls.Add(this.txtSpeakText);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.picRec);
            this.Controls.Add(this.picStop);
            this.Controls.Add(this.picPlay);
            this.Controls.Add(this.toolStrip1);
            this.Name = "frmIntonation";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmIntonation_FormClosing);
            this.Load += new System.EventHandler(this.frmSelfListen_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.gbIntonAnalysis.ResumeLayout(false);
            this.gbIntonAnalysis.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numMaxTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMinFreq)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMaxFreq)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMinIntens)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picIntonation)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picRec)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picStop)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picPlay)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
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
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtSpeakText;
        private System.Windows.Forms.GroupBox gbIntonAnalysis;
        private System.Windows.Forms.Button btnDelCurves;
        private System.Windows.Forms.Timer timerSample;
        private System.Windows.Forms.PictureBox picIntonation;
        private System.Windows.Forms.NumericUpDown numMinIntens;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numMinFreq;
        private System.Windows.Forms.NumericUpDown numMaxFreq;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown numMaxTime;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnStartTest;
        private System.Windows.Forms.ComboBox cmbInputDevice;
        private System.Windows.Forms.ComboBox cmbReplay;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnReplayPrevAudio;
        private System.Windows.Forms.Button btnOpenFileInton;
        private System.Windows.Forms.Button btnClient;
        private System.Windows.Forms.Button btnSaveAll;
        private System.Windows.Forms.ComboBox cmbReplayTogether;
        private System.Windows.Forms.Button btnStopOrigFile;
    }
}