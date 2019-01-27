namespace PratiCanto.SPPATHSustainedVowel
{
    partial class frmSustVowel
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSustVowel));
            this.splitCt = new System.Windows.Forms.SplitContainer();
            this.panSustVowelCfg = new System.Windows.Forms.Panel();
            this.btnOpen = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnReplay = new System.Windows.Forms.Button();
            this.cmbInputDevice = new System.Windows.Forms.ComboBox();
            this.btnAnalyze = new System.Windows.Forms.Button();
            this.picWaveForm = new System.Windows.Forms.PictureBox();
            this.lblFreqIntens = new System.Windows.Forms.Label();
            this.btnClient = new System.Windows.Forms.Button();
            this.btnStartTest = new System.Windows.Forms.Button();
            this.txtVarPenalty = new System.Windows.Forms.TextBox();
            this.txtNFreqs = new System.Windows.Forms.TextBox();
            this.txtChangePenalty = new System.Windows.Forms.TextBox();
            this.picPlay = new System.Windows.Forms.PictureBox();
            this.txtDPenalty = new System.Windows.Forms.TextBox();
            this.lblSpecInfo = new System.Windows.Forms.Label();
            this.picRec = new System.Windows.Forms.PictureBox();
            this.picStop = new System.Windows.Forms.PictureBox();
            this.picControlBar = new System.Windows.Forms.PictureBox();
            this.GLPicIntens = new OpenTK.GLControl();
            this.glPicSpectre = new OpenTK.GLControl();
            this.btnSummary = new System.Windows.Forms.Button();
            this.panVoiceEvol = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.numAmplif = new System.Windows.Forms.NumericUpDown();
            this.numEvolPts = new System.Windows.Forms.NumericUpDown();
            this.picVoiceQualityEvol = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnDetails = new System.Windows.Forms.Button();
            this.panDetails = new System.Windows.Forms.Panel();
            this.panDetNumDetails = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.txtRegInfoDetails = new System.Windows.Forms.TextBox();
            this.panDetFreqs = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.lstNumInfo = new System.Windows.Forms.ListBox();
            this.cmbNumList = new System.Windows.Forms.ComboBox();
            this.panSummary = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.picVoiceParams = new System.Windows.Forms.PictureBox();
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
            ((System.ComponentModel.ISupportInitialize)(this.splitCt)).BeginInit();
            this.splitCt.Panel1.SuspendLayout();
            this.splitCt.Panel2.SuspendLayout();
            this.splitCt.SuspendLayout();
            this.panSustVowelCfg.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picWaveForm)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picPlay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picRec)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picStop)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picControlBar)).BeginInit();
            this.panVoiceEvol.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numAmplif)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numEvolPts)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picVoiceQualityEvol)).BeginInit();
            this.panDetails.SuspendLayout();
            this.panDetNumDetails.SuspendLayout();
            this.panDetFreqs.SuspendLayout();
            this.panSummary.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picVoiceParams)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitCt
            // 
            resources.ApplyResources(this.splitCt, "splitCt");
            this.splitCt.Name = "splitCt";
            // 
            // splitCt.Panel1
            // 
            this.splitCt.Panel1.Controls.Add(this.panSustVowelCfg);
            this.splitCt.Panel1.Resize += new System.EventHandler(this.splitContainer1_Panel1_Resize);
            // 
            // splitCt.Panel2
            // 
            this.splitCt.Panel2.Controls.Add(this.btnSummary);
            this.splitCt.Panel2.Controls.Add(this.panVoiceEvol);
            this.splitCt.Panel2.Controls.Add(this.btnDetails);
            this.splitCt.Panel2.Controls.Add(this.panDetails);
            this.splitCt.Panel2.Controls.Add(this.panSummary);
            // 
            // panSustVowelCfg
            // 
            resources.ApplyResources(this.panSustVowelCfg, "panSustVowelCfg");
            this.panSustVowelCfg.Controls.Add(this.btnOpen);
            this.panSustVowelCfg.Controls.Add(this.btnSave);
            this.panSustVowelCfg.Controls.Add(this.btnReplay);
            this.panSustVowelCfg.Controls.Add(this.cmbInputDevice);
            this.panSustVowelCfg.Controls.Add(this.btnAnalyze);
            this.panSustVowelCfg.Controls.Add(this.picWaveForm);
            this.panSustVowelCfg.Controls.Add(this.lblFreqIntens);
            this.panSustVowelCfg.Controls.Add(this.btnClient);
            this.panSustVowelCfg.Controls.Add(this.btnStartTest);
            this.panSustVowelCfg.Controls.Add(this.txtVarPenalty);
            this.panSustVowelCfg.Controls.Add(this.txtNFreqs);
            this.panSustVowelCfg.Controls.Add(this.txtChangePenalty);
            this.panSustVowelCfg.Controls.Add(this.picPlay);
            this.panSustVowelCfg.Controls.Add(this.txtDPenalty);
            this.panSustVowelCfg.Controls.Add(this.lblSpecInfo);
            this.panSustVowelCfg.Controls.Add(this.picRec);
            this.panSustVowelCfg.Controls.Add(this.picStop);
            this.panSustVowelCfg.Controls.Add(this.picControlBar);
            this.panSustVowelCfg.Controls.Add(this.GLPicIntens);
            this.panSustVowelCfg.Controls.Add(this.glPicSpectre);
            this.panSustVowelCfg.Name = "panSustVowelCfg";
            this.panSustVowelCfg.Paint += new System.Windows.Forms.PaintEventHandler(this.panSustVowelCfg_Paint);
            // 
            // btnOpen
            // 
            this.btnOpen.BackColor = System.Drawing.Color.Transparent;
            this.btnOpen.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnOpen.Image = global::PratiCanto.Properties.Resources.open;
            resources.ApplyResources(this.btnOpen, "btnOpen");
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.UseVisualStyleBackColor = false;
            this.btnOpen.Click += new System.EventHandler(this.btnOpenFile_Click);
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.Transparent;
            this.btnSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSave.Image = global::PratiCanto.Properties.Resources.save;
            resources.ApplyResources(this.btnSave, "btnSave");
            this.btnSave.Name = "btnSave";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSaveRecord_Click);
            // 
            // btnReplay
            // 
            this.btnReplay.BackColor = System.Drawing.Color.Transparent;
            this.btnReplay.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnReplay.Image = global::PratiCanto.Properties.Resources.play;
            resources.ApplyResources(this.btnReplay, "btnReplay");
            this.btnReplay.Name = "btnReplay";
            this.btnReplay.UseVisualStyleBackColor = false;
            this.btnReplay.Click += new System.EventHandler(this.btnPlay_Click);
            // 
            // cmbInputDevice
            // 
            resources.ApplyResources(this.cmbInputDevice, "cmbInputDevice");
            this.cmbInputDevice.FormattingEnabled = true;
            this.cmbInputDevice.Name = "cmbInputDevice";
            // 
            // btnAnalyze
            // 
            this.btnAnalyze.BackColor = System.Drawing.Color.Transparent;
            this.btnAnalyze.Cursor = System.Windows.Forms.Cursors.Hand;
            resources.ApplyResources(this.btnAnalyze, "btnAnalyze");
            this.btnAnalyze.Name = "btnAnalyze";
            this.btnAnalyze.UseVisualStyleBackColor = false;
            this.btnAnalyze.Click += new System.EventHandler(this.btnAnalyze_Click);
            // 
            // picWaveForm
            // 
            this.picWaveForm.BackColor = System.Drawing.Color.Transparent;
            this.picWaveForm.Image = global::PratiCanto.Properties.Resources.WaveForm;
            resources.ApplyResources(this.picWaveForm, "picWaveForm");
            this.picWaveForm.Name = "picWaveForm";
            this.picWaveForm.TabStop = false;
            // 
            // lblFreqIntens
            // 
            resources.ApplyResources(this.lblFreqIntens, "lblFreqIntens");
            this.lblFreqIntens.BackColor = System.Drawing.Color.Transparent;
            this.lblFreqIntens.Name = "lblFreqIntens";
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
            // txtVarPenalty
            // 
            resources.ApplyResources(this.txtVarPenalty, "txtVarPenalty");
            this.txtVarPenalty.Name = "txtVarPenalty";
            // 
            // txtNFreqs
            // 
            resources.ApplyResources(this.txtNFreqs, "txtNFreqs");
            this.txtNFreqs.Name = "txtNFreqs";
            // 
            // txtChangePenalty
            // 
            resources.ApplyResources(this.txtChangePenalty, "txtChangePenalty");
            this.txtChangePenalty.Name = "txtChangePenalty";
            // 
            // picPlay
            // 
            resources.ApplyResources(this.picPlay, "picPlay");
            this.picPlay.Name = "picPlay";
            this.picPlay.TabStop = false;
            // 
            // txtDPenalty
            // 
            resources.ApplyResources(this.txtDPenalty, "txtDPenalty");
            this.txtDPenalty.Name = "txtDPenalty";
            // 
            // lblSpecInfo
            // 
            resources.ApplyResources(this.lblSpecInfo, "lblSpecInfo");
            this.lblSpecInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblSpecInfo.Name = "lblSpecInfo";
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
            // picControlBar
            // 
            resources.ApplyResources(this.picControlBar, "picControlBar");
            this.picControlBar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picControlBar.Cursor = System.Windows.Forms.Cursors.Cross;
            this.picControlBar.Name = "picControlBar";
            this.picControlBar.TabStop = false;
            // 
            // GLPicIntens
            // 
            resources.ApplyResources(this.GLPicIntens, "GLPicIntens");
            this.GLPicIntens.BackColor = System.Drawing.Color.Black;
            this.GLPicIntens.Cursor = System.Windows.Forms.Cursors.Cross;
            this.GLPicIntens.Name = "GLPicIntens";
            this.GLPicIntens.VSync = false;
            // 
            // glPicSpectre
            // 
            resources.ApplyResources(this.glPicSpectre, "glPicSpectre");
            this.glPicSpectre.BackColor = System.Drawing.Color.Black;
            this.glPicSpectre.Cursor = System.Windows.Forms.Cursors.Cross;
            this.glPicSpectre.Name = "glPicSpectre";
            this.glPicSpectre.VSync = false;
            // 
            // btnSummary
            // 
            this.btnSummary.Cursor = System.Windows.Forms.Cursors.Hand;
            resources.ApplyResources(this.btnSummary, "btnSummary");
            this.btnSummary.Name = "btnSummary";
            this.btnSummary.UseVisualStyleBackColor = true;
            this.btnSummary.Click += new System.EventHandler(this.btnSummary_Click);
            this.btnSummary.Paint += new System.Windows.Forms.PaintEventHandler(this.btnSummary_Paint);
            // 
            // panVoiceEvol
            // 
            resources.ApplyResources(this.panVoiceEvol, "panVoiceEvol");
            this.panVoiceEvol.Controls.Add(this.label6);
            this.panVoiceEvol.Controls.Add(this.pictureBox2);
            this.panVoiceEvol.Controls.Add(this.label1);
            this.panVoiceEvol.Controls.Add(this.numAmplif);
            this.panVoiceEvol.Controls.Add(this.numEvolPts);
            this.panVoiceEvol.Controls.Add(this.picVoiceQualityEvol);
            this.panVoiceEvol.Controls.Add(this.label2);
            this.panVoiceEvol.Name = "panVoiceEvol";
            this.panVoiceEvol.Paint += new System.Windows.Forms.PaintEventHandler(this.panVoiceEvol_Paint);
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Name = "label6";
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox2.Image = global::PratiCanto.Properties.Resources.arrow;
            resources.ApplyResources(this.pictureBox2, "pictureBox2");
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.TabStop = false;
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // numAmplif
            // 
            this.numAmplif.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            resources.ApplyResources(this.numAmplif, "numAmplif");
            this.numAmplif.Minimum = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.numAmplif.Name = "numAmplif";
            this.numAmplif.Value = new decimal(new int[] {
            16,
            0,
            0,
            0});
            this.numAmplif.ValueChanged += new System.EventHandler(this.numAmplif_ValueChanged);
            // 
            // numEvolPts
            // 
            this.numEvolPts.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            resources.ApplyResources(this.numEvolPts, "numEvolPts");
            this.numEvolPts.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numEvolPts.Name = "numEvolPts";
            this.numEvolPts.Value = new decimal(new int[] {
            14,
            0,
            0,
            0});
            this.numEvolPts.ValueChanged += new System.EventHandler(this.numEvolPts_ValueChanged);
            // 
            // picVoiceQualityEvol
            // 
            resources.ApplyResources(this.picVoiceQualityEvol, "picVoiceQualityEvol");
            this.picVoiceQualityEvol.BackgroundImage = global::PratiCanto.Properties.Resources.logowith_bgSuave2;
            this.picVoiceQualityEvol.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picVoiceQualityEvol.Name = "picVoiceQualityEvol";
            this.picVoiceQualityEvol.TabStop = false;
            this.picVoiceQualityEvol.Paint += new System.Windows.Forms.PaintEventHandler(this.picVoiceQualityEvol_Paint);
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // btnDetails
            // 
            this.btnDetails.Cursor = System.Windows.Forms.Cursors.Hand;
            resources.ApplyResources(this.btnDetails, "btnDetails");
            this.btnDetails.Name = "btnDetails";
            this.btnDetails.UseVisualStyleBackColor = true;
            this.btnDetails.Click += new System.EventHandler(this.btnDetails_Click);
            this.btnDetails.Paint += new System.Windows.Forms.PaintEventHandler(this.btnDetails_Paint);
            // 
            // panDetails
            // 
            resources.ApplyResources(this.panDetails, "panDetails");
            this.panDetails.Controls.Add(this.panDetNumDetails);
            this.panDetails.Controls.Add(this.panDetFreqs);
            this.panDetails.Name = "panDetails";
            this.panDetails.Paint += new System.Windows.Forms.PaintEventHandler(this.panDetails_Paint);
            // 
            // panDetNumDetails
            // 
            resources.ApplyResources(this.panDetNumDetails, "panDetNumDetails");
            this.panDetNumDetails.Controls.Add(this.label5);
            this.panDetNumDetails.Controls.Add(this.txtRegInfoDetails);
            this.panDetNumDetails.Name = "panDetNumDetails";
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Name = "label5";
            // 
            // txtRegInfoDetails
            // 
            resources.ApplyResources(this.txtRegInfoDetails, "txtRegInfoDetails");
            this.txtRegInfoDetails.Name = "txtRegInfoDetails";
            this.txtRegInfoDetails.ReadOnly = true;
            // 
            // panDetFreqs
            // 
            resources.ApplyResources(this.panDetFreqs, "panDetFreqs");
            this.panDetFreqs.Controls.Add(this.label4);
            this.panDetFreqs.Controls.Add(this.lstNumInfo);
            this.panDetFreqs.Controls.Add(this.cmbNumList);
            this.panDetFreqs.Name = "panDetFreqs";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Name = "label4";
            // 
            // lstNumInfo
            // 
            resources.ApplyResources(this.lstNumInfo, "lstNumInfo");
            this.lstNumInfo.FormattingEnabled = true;
            this.lstNumInfo.Name = "lstNumInfo";
            // 
            // cmbNumList
            // 
            resources.ApplyResources(this.cmbNumList, "cmbNumList");
            this.cmbNumList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbNumList.FormattingEnabled = true;
            this.cmbNumList.Items.AddRange(new object[] {
            resources.GetString("cmbNumList.Items"),
            resources.GetString("cmbNumList.Items1"),
            resources.GetString("cmbNumList.Items2"),
            resources.GetString("cmbNumList.Items3"),
            resources.GetString("cmbNumList.Items4"),
            resources.GetString("cmbNumList.Items5"),
            resources.GetString("cmbNumList.Items6")});
            this.cmbNumList.Name = "cmbNumList";
            this.cmbNumList.SelectedIndexChanged += new System.EventHandler(this.cmbNumList_SelectedIndexChanged);
            // 
            // panSummary
            // 
            resources.ApplyResources(this.panSummary, "panSummary");
            this.panSummary.Controls.Add(this.label3);
            this.panSummary.Controls.Add(this.pictureBox1);
            this.panSummary.Controls.Add(this.picVoiceParams);
            this.panSummary.Name = "panSummary";
            this.panSummary.Paint += new System.Windows.Forms.PaintEventHandler(this.panSummary_Paint);
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Name = "label3";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Image = global::PratiCanto.Properties.Resources.arrow;
            resources.ApplyResources(this.pictureBox1, "pictureBox1");
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.TabStop = false;
            // 
            // picVoiceParams
            // 
            resources.ApplyResources(this.picVoiceParams, "picVoiceParams");
            this.picVoiceParams.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picVoiceParams.Name = "picVoiceParams";
            this.picVoiceParams.TabStop = false;
            this.picVoiceParams.Paint += new System.Windows.Forms.PaintEventHandler(this.picVoiceParams_Paint);
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
            this.btnSaveRecord.Click += new System.EventHandler(this.btnSaveRecord_Click);
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
            // frmSustVowel
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.splitCt);
            this.Controls.Add(this.toolStrip1);
            this.Name = "frmSustVowel";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmSustVowel_FormClosing);
            this.Load += new System.EventHandler(this.frmSustVowel_Load);
            this.Resize += new System.EventHandler(this.frmSustVowel_Resize);
            this.splitCt.Panel1.ResumeLayout(false);
            this.splitCt.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitCt)).EndInit();
            this.splitCt.ResumeLayout(false);
            this.panSustVowelCfg.ResumeLayout(false);
            this.panSustVowelCfg.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picWaveForm)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picPlay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picRec)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picStop)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picControlBar)).EndInit();
            this.panVoiceEvol.ResumeLayout(false);
            this.panVoiceEvol.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numAmplif)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numEvolPts)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picVoiceQualityEvol)).EndInit();
            this.panDetails.ResumeLayout(false);
            this.panDetNumDetails.ResumeLayout(false);
            this.panDetNumDetails.PerformLayout();
            this.panDetFreqs.ResumeLayout(false);
            this.panDetFreqs.PerformLayout();
            this.panSummary.ResumeLayout(false);
            this.panSummary.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picVoiceParams)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnRec;
        private System.Windows.Forms.ToolStripButton btnPlay;
        private System.Windows.Forms.ToolStripButton btnSaveRecord;
        private System.Windows.Forms.PictureBox picRec;
        private System.Windows.Forms.PictureBox picStop;
        private System.Windows.Forms.PictureBox picPlay;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripLabel lblNote;
        private System.Windows.Forms.ToolStripTextBox txtAnnotation;
        private System.Windows.Forms.Label lblSpecInfo;
        private OpenTK.GLControl glPicSpectre;
        private System.Windows.Forms.PictureBox picControlBar;
        private OpenTK.GLControl GLPicIntens;
        private System.Windows.Forms.TextBox txtVarPenalty;
        private System.Windows.Forms.TextBox txtNFreqs;
        private System.Windows.Forms.TextBox txtChangePenalty;
        private System.Windows.Forms.TextBox txtDPenalty;
        private System.Windows.Forms.NumericUpDown numAmplif;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox picVoiceQualityEvol;
        private System.Windows.Forms.ToolStripButton btnStartStop;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numEvolPts;
        private System.Windows.Forms.PictureBox picVoiceParams;
        private System.Windows.Forms.ToolStripButton btnOpenFile;
        private System.Windows.Forms.Panel panSustVowelCfg;
        private System.Windows.Forms.Button btnStartTest;
        private System.Windows.Forms.Button btnClient;
        private System.Windows.Forms.Label lblFreqIntens;
        private System.Windows.Forms.PictureBox picWaveForm;
        private System.Windows.Forms.Button btnAnalyze;
        private System.Windows.Forms.ComboBox cmbInputDevice;
        private System.Windows.Forms.Button btnReplay;
        private System.Windows.Forms.Button btnOpen;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnSummary;
        private System.Windows.Forms.Button btnDetails;
        private System.Windows.Forms.Panel panSummary;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panDetails;
        private System.Windows.Forms.Panel panDetNumDetails;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtRegInfoDetails;
        private System.Windows.Forms.Panel panDetFreqs;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ListBox lstNumInfo;
        private System.Windows.Forms.ComboBox cmbNumList;
        private System.Windows.Forms.Panel panVoiceEvol;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.SplitContainer splitCt;
    }
}