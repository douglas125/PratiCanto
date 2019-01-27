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
            this.btnRec = new System.Windows.Forms.ToolStripButton();
            this.btnPlay = new System.Windows.Forms.ToolStripButton();
            this.btnSaveRecord = new System.Windows.Forms.ToolStripButton();
            this.btnOpenFile = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.lblNote = new System.Windows.Forms.ToolStripLabel();
            this.txtAnnotation = new System.Windows.Forms.ToolStripTextBox();
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
            this.label5 = new System.Windows.Forms.Label();
            this.cmbRecogLanguage = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtAPIKey = new System.Windows.Forms.TextBox();
            this.panCfg = new System.Windows.Forms.Panel();
            this.cmbInputDevice = new System.Windows.Forms.ComboBox();
            this.btnStartTest = new System.Windows.Forms.Button();
            this.picPlay = new System.Windows.Forms.PictureBox();
            this.picStop = new System.Windows.Forms.PictureBox();
            this.picRec = new System.Windows.Forms.PictureBox();
            this.lblAPIKey = new System.Windows.Forms.Label();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numIntensThresh)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numReqInterval)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.panCfg.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picPlay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picStop)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picRec)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            resources.ApplyResources(this.toolStrip1, "toolStrip1");
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
            resources.ApplyResources(this.toolStripSeparator3, "toolStripSeparator3");
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            // 
            // btnStartStop
            // 
            resources.ApplyResources(this.btnStartStop, "btnStartStop");
            this.btnStartStop.BackColor = System.Drawing.Color.Lime;
            this.btnStartStop.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnStartStop.Name = "btnStartStop";
            this.btnStartStop.Click += new System.EventHandler(this.btnRec_Click);
            // 
            // toolStripSeparator2
            // 
            resources.ApplyResources(this.toolStripSeparator2, "toolStripSeparator2");
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            // 
            // btnRec
            // 
            resources.ApplyResources(this.btnRec, "btnRec");
            this.btnRec.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
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
            resources.ApplyResources(this.btnSaveRecord, "btnSaveRecord");
            this.btnSaveRecord.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
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
            resources.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
            this.toolStripSeparator1.Name = "toolStripSeparator1";
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
            // txtAns
            // 
            resources.ApplyResources(this.txtAns, "txtAns");
            this.txtAns.Name = "txtAns";
            // 
            // btnFromAudio
            // 
            resources.ApplyResources(this.btnFromAudio, "btnFromAudio");
            this.btnFromAudio.Name = "btnFromAudio";
            this.btnFromAudio.UseVisualStyleBackColor = true;
            this.btnFromAudio.Click += new System.EventHandler(this.btnFromAudio_Click);
            // 
            // bgW
            // 
            this.bgW.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgW_DoWork);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Name = "label1";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Name = "label2";
            // 
            // lblCurIntens
            // 
            resources.ApplyResources(this.lblCurIntens, "lblCurIntens");
            this.lblCurIntens.BackColor = System.Drawing.Color.Transparent;
            this.lblCurIntens.Name = "lblCurIntens";
            // 
            // numIntensThresh
            // 
            resources.ApplyResources(this.numIntensThresh, "numIntensThresh");
            this.numIntensThresh.DecimalPlaces = 2;
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
            this.numIntensThresh.Value = new decimal(new int[] {
            120,
            0,
            0,
            0});
            this.numIntensThresh.ValueChanged += new System.EventHandler(this.numIntensThresh_ValueChanged);
            // 
            // btnPushToTalk
            // 
            resources.ApplyResources(this.btnPushToTalk, "btnPushToTalk");
            this.btnPushToTalk.Name = "btnPushToTalk";
            this.btnPushToTalk.UseVisualStyleBackColor = true;
            this.btnPushToTalk.KeyDown += new System.Windows.Forms.KeyEventHandler(this.btnPushToTalk_KeyDown);
            this.btnPushToTalk.KeyUp += new System.Windows.Forms.KeyEventHandler(this.btnPushToTalk_KeyUp);
            this.btnPushToTalk.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnPushToTalk_MouseDown);
            this.btnPushToTalk.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnPushToTalk_MouseUp);
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Name = "label3";
            // 
            // lblInQueue
            // 
            resources.ApplyResources(this.lblInQueue, "lblInQueue");
            this.lblInQueue.BackColor = System.Drawing.Color.Transparent;
            this.lblInQueue.Name = "lblInQueue";
            // 
            // chkContinuousRecog
            // 
            resources.ApplyResources(this.chkContinuousRecog, "chkContinuousRecog");
            this.chkContinuousRecog.BackColor = System.Drawing.Color.Transparent;
            this.chkContinuousRecog.Name = "chkContinuousRecog";
            this.chkContinuousRecog.UseVisualStyleBackColor = false;
            this.chkContinuousRecog.CheckedChanged += new System.EventHandler(this.chkContinuousRecog_CheckedChanged);
            // 
            // timerContRecog
            // 
            this.timerContRecog.Interval = 30000;
            this.timerContRecog.Tick += new System.EventHandler(this.timerContRecog_Tick);
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Name = "label4";
            // 
            // numReqInterval
            // 
            resources.ApplyResources(this.numReqInterval, "numReqInterval");
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
            this.numReqInterval.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.numReqInterval.ValueChanged += new System.EventHandler(this.numReqInterval_ValueChanged);
            // 
            // groupBox1
            // 
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Controls.Add(this.txtHints);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            this.groupBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.groupBox1_Paint);
            // 
            // txtHints
            // 
            resources.ApplyResources(this.txtHints, "txtHints");
            this.txtHints.Name = "txtHints";
            this.txtHints.TextChanged += new System.EventHandler(this.txtHints_TextChanged);
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Name = "label5";
            // 
            // cmbRecogLanguage
            // 
            resources.ApplyResources(this.cmbRecogLanguage, "cmbRecogLanguage");
            this.cmbRecogLanguage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRecogLanguage.FormattingEnabled = true;
            this.cmbRecogLanguage.Items.AddRange(new object[] {
            resources.GetString("cmbRecogLanguage.Items"),
            resources.GetString("cmbRecogLanguage.Items1")});
            this.cmbRecogLanguage.Name = "cmbRecogLanguage";
            this.cmbRecogLanguage.SelectedIndexChanged += new System.EventHandler(this.cmbRecogLanguage_SelectedIndexChanged);
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Name = "label6";
            // 
            // txtAPIKey
            // 
            resources.ApplyResources(this.txtAPIKey, "txtAPIKey");
            this.txtAPIKey.Name = "txtAPIKey";
            this.txtAPIKey.UseSystemPasswordChar = true;
            this.txtAPIKey.Leave += new System.EventHandler(this.txtAPIKey_Leave);
            // 
            // panCfg
            // 
            resources.ApplyResources(this.panCfg, "panCfg");
            this.panCfg.Controls.Add(this.cmbInputDevice);
            this.panCfg.Controls.Add(this.btnStartTest);
            this.panCfg.Controls.Add(this.txtAPIKey);
            this.panCfg.Controls.Add(this.picPlay);
            this.panCfg.Controls.Add(this.label6);
            this.panCfg.Controls.Add(this.picStop);
            this.panCfg.Controls.Add(this.cmbRecogLanguage);
            this.panCfg.Controls.Add(this.picRec);
            this.panCfg.Controls.Add(this.label5);
            this.panCfg.Controls.Add(this.btnFromAudio);
            this.panCfg.Controls.Add(this.label1);
            this.panCfg.Controls.Add(this.label3);
            this.panCfg.Controls.Add(this.numReqInterval);
            this.panCfg.Controls.Add(this.lblCurIntens);
            this.panCfg.Controls.Add(this.label4);
            this.panCfg.Controls.Add(this.lblInQueue);
            this.panCfg.Controls.Add(this.chkContinuousRecog);
            this.panCfg.Controls.Add(this.label2);
            this.panCfg.Controls.Add(this.btnPushToTalk);
            this.panCfg.Controls.Add(this.numIntensThresh);
            this.panCfg.Name = "panCfg";
            this.panCfg.Paint += new System.Windows.Forms.PaintEventHandler(this.panCfg_Paint);
            // 
            // cmbInputDevice
            // 
            resources.ApplyResources(this.cmbInputDevice, "cmbInputDevice");
            this.cmbInputDevice.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbInputDevice.FormattingEnabled = true;
            this.cmbInputDevice.Name = "cmbInputDevice";
            // 
            // btnStartTest
            // 
            resources.ApplyResources(this.btnStartTest, "btnStartTest");
            this.btnStartTest.BackColor = System.Drawing.Color.Transparent;
            this.btnStartTest.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnStartTest.ForeColor = System.Drawing.Color.White;
            this.btnStartTest.Image = global::PratiCanto.Properties.Resources.startTest;
            this.btnStartTest.Name = "btnStartTest";
            this.btnStartTest.UseVisualStyleBackColor = false;
            this.btnStartTest.Click += new System.EventHandler(this.btnRec_Click);
            // 
            // picPlay
            // 
            resources.ApplyResources(this.picPlay, "picPlay");
            this.picPlay.Name = "picPlay";
            this.picPlay.TabStop = false;
            // 
            // picStop
            // 
            resources.ApplyResources(this.picStop, "picStop");
            this.picStop.Name = "picStop";
            this.picStop.TabStop = false;
            // 
            // picRec
            // 
            resources.ApplyResources(this.picRec, "picRec");
            this.picRec.Name = "picRec";
            this.picRec.TabStop = false;
            // 
            // lblAPIKey
            // 
            resources.ApplyResources(this.lblAPIKey, "lblAPIKey");
            this.lblAPIKey.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblAPIKey.Name = "lblAPIKey";
            // 
            // frmSpeechRecognizer
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.lblAPIKey);
            this.Controls.Add(this.panCfg);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.txtAns);
            this.Controls.Add(this.toolStrip1);
            this.Name = "frmSpeechRecognizer";
            this.Load += new System.EventHandler(this.frmSelfListen_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numIntensThresh)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numReqInterval)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panCfg.ResumeLayout(false);
            this.panCfg.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picPlay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picStop)).EndInit();
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
        private System.Windows.Forms.Button btnStartTest;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cmbRecogLanguage;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtAPIKey;
        private System.Windows.Forms.Panel panCfg;
        private System.Windows.Forms.ComboBox cmbInputDevice;
        private System.Windows.Forms.Label lblAPIKey;
    }
}