namespace ClassifTreinoVoz
{
    partial class frmPractice
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPractice));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveLastPracticeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.replayToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.compareToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.voiceTypeWizardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.noteGridToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.intensityGraphToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.soundToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.waveShapeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pianoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sineWaveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.squareWaveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gbMelody = new System.Windows.Forms.GroupBox();
            this.cmbInitNote = new System.Windows.Forms.ComboBox();
            this.numTempo = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.chkModulateFirstNote = new System.Windows.Forms.CheckBox();
            this.btnIdentifySound = new System.Windows.Forms.Button();
            this.btnLoadMelody = new System.Windows.Forms.Button();
            this.gbViewMelody = new System.Windows.Forms.GroupBox();
            this.lblFindLicense = new System.Windows.Forms.Label();
            this.lblNotRegistered = new System.Windows.Forms.Label();
            this.picRec = new System.Windows.Forms.PictureBox();
            this.picPlay = new System.Windows.Forms.PictureBox();
            this.btnPlayMelody = new System.Windows.Forms.Button();
            this.picStop = new System.Windows.Forms.PictureBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.picViewMelody = new System.Windows.Forms.PictureBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnClient = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.btnExtensionWizard = new System.Windows.Forms.ToolStripButton();
            this.btnEditMelodies = new System.Windows.Forms.ToolStripButton();
            this.btnAudioAnalyzer = new System.Windows.Forms.ToolStripButton();
            this.cmbInputDevice = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnFreeSinging = new System.Windows.Forms.ToolStripButton();
            this.btnTranscribe = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.btnGuidedSinging = new System.Windows.Forms.ToolStripButton();
            this.txtMelodyName = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnStopAll = new System.Windows.Forms.ToolStripButton();
            this.btnOpenFile = new System.Windows.Forms.ToolStripButton();
            this.btnSaveSings = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.lblNote = new System.Windows.Forms.ToolStripLabel();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.menuStrip1.SuspendLayout();
            this.gbMelody.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numTempo)).BeginInit();
            this.gbViewMelody.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picRec)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picPlay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picStop)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picViewMelody)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.viewToolStripMenuItem,
            this.soundToolStripMenuItem,
            this.helpToolStripMenuItem});
            resources.ApplyResources(this.menuStrip1, "menuStrip1");
            this.menuStrip1.Name = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveLastPracticeToolStripMenuItem,
            this.replayToolStripMenuItem,
            this.compareToolStripMenuItem,
            this.toolStripMenuItem2,
            this.voiceTypeWizardToolStripMenuItem,
            this.toolStripMenuItem1,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            resources.ApplyResources(this.fileToolStripMenuItem, "fileToolStripMenuItem");
            // 
            // saveLastPracticeToolStripMenuItem
            // 
            this.saveLastPracticeToolStripMenuItem.Name = "saveLastPracticeToolStripMenuItem";
            resources.ApplyResources(this.saveLastPracticeToolStripMenuItem, "saveLastPracticeToolStripMenuItem");
            this.saveLastPracticeToolStripMenuItem.Click += new System.EventHandler(this.btnSaveSings_Click);
            // 
            // replayToolStripMenuItem
            // 
            this.replayToolStripMenuItem.Name = "replayToolStripMenuItem";
            resources.ApplyResources(this.replayToolStripMenuItem, "replayToolStripMenuItem");
            this.replayToolStripMenuItem.Click += new System.EventHandler(this.replayToolStripMenuItem_Click);
            // 
            // compareToolStripMenuItem
            // 
            resources.ApplyResources(this.compareToolStripMenuItem, "compareToolStripMenuItem");
            this.compareToolStripMenuItem.Name = "compareToolStripMenuItem";
            this.compareToolStripMenuItem.Click += new System.EventHandler(this.compareToolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            resources.ApplyResources(this.toolStripMenuItem2, "toolStripMenuItem2");
            // 
            // voiceTypeWizardToolStripMenuItem
            // 
            this.voiceTypeWizardToolStripMenuItem.Name = "voiceTypeWizardToolStripMenuItem";
            resources.ApplyResources(this.voiceTypeWizardToolStripMenuItem, "voiceTypeWizardToolStripMenuItem");
            this.voiceTypeWizardToolStripMenuItem.Click += new System.EventHandler(this.voiceTypeWizardToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            resources.ApplyResources(this.toolStripMenuItem1, "toolStripMenuItem1");
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            resources.ApplyResources(this.exitToolStripMenuItem, "exitToolStripMenuItem");
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.noteGridToolStripMenuItem,
            this.intensityGraphToolStripMenuItem});
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            resources.ApplyResources(this.viewToolStripMenuItem, "viewToolStripMenuItem");
            // 
            // noteGridToolStripMenuItem
            // 
            this.noteGridToolStripMenuItem.Checked = true;
            this.noteGridToolStripMenuItem.CheckOnClick = true;
            this.noteGridToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.noteGridToolStripMenuItem.Name = "noteGridToolStripMenuItem";
            resources.ApplyResources(this.noteGridToolStripMenuItem, "noteGridToolStripMenuItem");
            this.noteGridToolStripMenuItem.Click += new System.EventHandler(this.noteGridToolStripMenuItem_Click);
            // 
            // intensityGraphToolStripMenuItem
            // 
            this.intensityGraphToolStripMenuItem.Checked = true;
            this.intensityGraphToolStripMenuItem.CheckOnClick = true;
            this.intensityGraphToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.intensityGraphToolStripMenuItem.Name = "intensityGraphToolStripMenuItem";
            resources.ApplyResources(this.intensityGraphToolStripMenuItem, "intensityGraphToolStripMenuItem");
            // 
            // soundToolStripMenuItem
            // 
            this.soundToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.waveShapeToolStripMenuItem});
            this.soundToolStripMenuItem.Name = "soundToolStripMenuItem";
            resources.ApplyResources(this.soundToolStripMenuItem, "soundToolStripMenuItem");
            // 
            // waveShapeToolStripMenuItem
            // 
            this.waveShapeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.pianoToolStripMenuItem,
            this.sineWaveToolStripMenuItem,
            this.squareWaveToolStripMenuItem});
            this.waveShapeToolStripMenuItem.Name = "waveShapeToolStripMenuItem";
            resources.ApplyResources(this.waveShapeToolStripMenuItem, "waveShapeToolStripMenuItem");
            // 
            // pianoToolStripMenuItem
            // 
            this.pianoToolStripMenuItem.Name = "pianoToolStripMenuItem";
            resources.ApplyResources(this.pianoToolStripMenuItem, "pianoToolStripMenuItem");
            this.pianoToolStripMenuItem.Click += new System.EventHandler(this.pianoToolStripMenuItem_Click);
            // 
            // sineWaveToolStripMenuItem
            // 
            this.sineWaveToolStripMenuItem.Name = "sineWaveToolStripMenuItem";
            resources.ApplyResources(this.sineWaveToolStripMenuItem, "sineWaveToolStripMenuItem");
            this.sineWaveToolStripMenuItem.Click += new System.EventHandler(this.sineWaveToolStripMenuItem_Click);
            // 
            // squareWaveToolStripMenuItem
            // 
            this.squareWaveToolStripMenuItem.Name = "squareWaveToolStripMenuItem";
            resources.ApplyResources(this.squareWaveToolStripMenuItem, "squareWaveToolStripMenuItem");
            this.squareWaveToolStripMenuItem.Click += new System.EventHandler(this.squareWaveToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            resources.ApplyResources(this.helpToolStripMenuItem, "helpToolStripMenuItem");
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            resources.ApplyResources(this.aboutToolStripMenuItem, "aboutToolStripMenuItem");
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // gbMelody
            // 
            resources.ApplyResources(this.gbMelody, "gbMelody");
            this.gbMelody.Controls.Add(this.cmbInitNote);
            this.gbMelody.Controls.Add(this.numTempo);
            this.gbMelody.Controls.Add(this.label1);
            this.gbMelody.Controls.Add(this.chkModulateFirstNote);
            this.gbMelody.Controls.Add(this.btnIdentifySound);
            this.gbMelody.Controls.Add(this.btnLoadMelody);
            this.gbMelody.Controls.Add(this.gbViewMelody);
            this.gbMelody.Name = "gbMelody";
            this.gbMelody.TabStop = false;
            // 
            // cmbInitNote
            // 
            this.cmbInitNote.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.cmbInitNote, "cmbInitNote");
            this.cmbInitNote.FormattingEnabled = true;
            this.cmbInitNote.Name = "cmbInitNote";
            this.cmbInitNote.SelectedIndexChanged += new System.EventHandler(this.cmbInitNote_SelectedIndexChanged);
            // 
            // numTempo
            // 
            this.numTempo.DecimalPlaces = 2;
            resources.ApplyResources(this.numTempo, "numTempo");
            this.numTempo.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numTempo.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numTempo.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.numTempo.Name = "numTempo";
            this.numTempo.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numTempo.ValueChanged += new System.EventHandler(this.numTempo_ValueChanged);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // chkModulateFirstNote
            // 
            resources.ApplyResources(this.chkModulateFirstNote, "chkModulateFirstNote");
            this.chkModulateFirstNote.Checked = true;
            this.chkModulateFirstNote.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkModulateFirstNote.Name = "chkModulateFirstNote";
            this.chkModulateFirstNote.UseVisualStyleBackColor = true;
            this.chkModulateFirstNote.CheckedChanged += new System.EventHandler(this.chkModulateFirstNote_CheckedChanged);
            // 
            // btnIdentifySound
            // 
            resources.ApplyResources(this.btnIdentifySound, "btnIdentifySound");
            this.btnIdentifySound.Name = "btnIdentifySound";
            this.toolTip.SetToolTip(this.btnIdentifySound, resources.GetString("btnIdentifySound.ToolTip"));
            this.btnIdentifySound.UseVisualStyleBackColor = true;
            this.btnIdentifySound.Click += new System.EventHandler(this.btnIdentifySound_Click);
            // 
            // btnLoadMelody
            // 
            resources.ApplyResources(this.btnLoadMelody, "btnLoadMelody");
            this.btnLoadMelody.Name = "btnLoadMelody";
            this.toolTip.SetToolTip(this.btnLoadMelody, resources.GetString("btnLoadMelody.ToolTip"));
            this.btnLoadMelody.UseVisualStyleBackColor = true;
            this.btnLoadMelody.Click += new System.EventHandler(this.btnLoadMelody_Click);
            // 
            // gbViewMelody
            // 
            resources.ApplyResources(this.gbViewMelody, "gbViewMelody");
            this.gbViewMelody.Controls.Add(this.lblFindLicense);
            this.gbViewMelody.Controls.Add(this.lblNotRegistered);
            this.gbViewMelody.Controls.Add(this.picRec);
            this.gbViewMelody.Controls.Add(this.picPlay);
            this.gbViewMelody.Controls.Add(this.btnPlayMelody);
            this.gbViewMelody.Controls.Add(this.picStop);
            this.gbViewMelody.Name = "gbViewMelody";
            this.gbViewMelody.TabStop = false;
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
            // picRec
            // 
            resources.ApplyResources(this.picRec, "picRec");
            this.picRec.Name = "picRec";
            this.picRec.TabStop = false;
            // 
            // picPlay
            // 
            resources.ApplyResources(this.picPlay, "picPlay");
            this.picPlay.Name = "picPlay";
            this.picPlay.TabStop = false;
            // 
            // btnPlayMelody
            // 
            resources.ApplyResources(this.btnPlayMelody, "btnPlayMelody");
            this.btnPlayMelody.Name = "btnPlayMelody";
            this.btnPlayMelody.UseVisualStyleBackColor = true;
            this.btnPlayMelody.Click += new System.EventHandler(this.btnPlayMelody_Click);
            // 
            // picStop
            // 
            resources.ApplyResources(this.picStop, "picStop");
            this.picStop.Name = "picStop";
            this.picStop.TabStop = false;
            // 
            // groupBox3
            // 
            resources.ApplyResources(this.groupBox3, "groupBox3");
            this.groupBox3.Controls.Add(this.picViewMelody);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.TabStop = false;
            // 
            // picViewMelody
            // 
            resources.ApplyResources(this.picViewMelody, "picViewMelody");
            this.picViewMelody.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picViewMelody.Name = "picViewMelody";
            this.picViewMelody.TabStop = false;
            this.picViewMelody.Paint += new System.Windows.Forms.PaintEventHandler(this.picViewMelody_Paint);
            this.picViewMelody.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picViewMelody_MouseDown);
            this.picViewMelody.MouseMove += new System.Windows.Forms.MouseEventHandler(this.picViewMelody_MouseMove);
            this.picViewMelody.MouseUp += new System.Windows.Forms.MouseEventHandler(this.picViewMelody_MouseUp);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnClient,
            this.toolStripSeparator5,
            this.btnExtensionWizard,
            this.btnEditMelodies,
            this.btnAudioAnalyzer,
            this.cmbInputDevice,
            this.toolStripSeparator2,
            this.btnFreeSinging,
            this.btnTranscribe,
            this.toolStripSeparator3,
            this.btnGuidedSinging,
            this.txtMelodyName,
            this.toolStripSeparator1,
            this.btnStopAll,
            this.btnOpenFile,
            this.btnSaveSings,
            this.toolStripSeparator4,
            this.lblNote});
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
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            resources.ApplyResources(this.toolStripSeparator5, "toolStripSeparator5");
            // 
            // btnExtensionWizard
            // 
            resources.ApplyResources(this.btnExtensionWizard, "btnExtensionWizard");
            this.btnExtensionWizard.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnExtensionWizard.Name = "btnExtensionWizard";
            this.btnExtensionWizard.Click += new System.EventHandler(this.btnExtensionWizard_Click);
            // 
            // btnEditMelodies
            // 
            resources.ApplyResources(this.btnEditMelodies, "btnEditMelodies");
            this.btnEditMelodies.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnEditMelodies.Name = "btnEditMelodies";
            this.btnEditMelodies.Click += new System.EventHandler(this.btnEditMelodies_Click);
            // 
            // btnAudioAnalyzer
            // 
            resources.ApplyResources(this.btnAudioAnalyzer, "btnAudioAnalyzer");
            this.btnAudioAnalyzer.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnAudioAnalyzer.Name = "btnAudioAnalyzer";
            this.btnAudioAnalyzer.Click += new System.EventHandler(this.btnAudioAnalyzer_Click);
            // 
            // cmbInputDevice
            // 
            this.cmbInputDevice.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.cmbInputDevice, "cmbInputDevice");
            this.cmbInputDevice.Name = "cmbInputDevice";
            this.cmbInputDevice.SelectedIndexChanged += new System.EventHandler(this.cmbInputDevice_SelectedIndexChanged);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            resources.ApplyResources(this.toolStripSeparator2, "toolStripSeparator2");
            // 
            // btnFreeSinging
            // 
            resources.ApplyResources(this.btnFreeSinging, "btnFreeSinging");
            this.btnFreeSinging.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnFreeSinging.Name = "btnFreeSinging";
            this.btnFreeSinging.Click += new System.EventHandler(this.btnFreeSinging_Click);
            // 
            // btnTranscribe
            // 
            resources.ApplyResources(this.btnTranscribe, "btnTranscribe");
            this.btnTranscribe.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnTranscribe.Name = "btnTranscribe";
            this.btnTranscribe.Click += new System.EventHandler(this.btnTranscribe_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            resources.ApplyResources(this.toolStripSeparator3, "toolStripSeparator3");
            // 
            // btnGuidedSinging
            // 
            resources.ApplyResources(this.btnGuidedSinging, "btnGuidedSinging");
            this.btnGuidedSinging.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnGuidedSinging.Name = "btnGuidedSinging";
            this.btnGuidedSinging.Click += new System.EventHandler(this.btnGuidedSinging_Click);
            // 
            // txtMelodyName
            // 
            resources.ApplyResources(this.txtMelodyName, "txtMelodyName");
            this.txtMelodyName.Name = "txtMelodyName";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            resources.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
            // 
            // btnStopAll
            // 
            resources.ApplyResources(this.btnStopAll, "btnStopAll");
            this.btnStopAll.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnStopAll.Name = "btnStopAll";
            this.btnStopAll.Click += new System.EventHandler(this.btnStopAll_Click);
            // 
            // btnOpenFile
            // 
            resources.ApplyResources(this.btnOpenFile, "btnOpenFile");
            this.btnOpenFile.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnOpenFile.Name = "btnOpenFile";
            this.btnOpenFile.Click += new System.EventHandler(this.replayToolStripMenuItem_Click);
            // 
            // btnSaveSings
            // 
            resources.ApplyResources(this.btnSaveSings, "btnSaveSings");
            this.btnSaveSings.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnSaveSings.Name = "btnSaveSings";
            this.btnSaveSings.Click += new System.EventHandler(this.btnSaveSings_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            resources.ApplyResources(this.toolStripSeparator4, "toolStripSeparator4");
            // 
            // lblNote
            // 
            resources.ApplyResources(this.lblNote, "lblNote");
            this.lblNote.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.lblNote.Name = "lblNote";
            this.lblNote.Click += new System.EventHandler(this.btnClient_Click);
            // 
            // timer
            // 
            this.timer.Interval = 5;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // frmPractice
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.gbMelody);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "frmPractice";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.gbMelody.ResumeLayout(false);
            this.gbMelody.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numTempo)).EndInit();
            this.gbViewMelody.ResumeLayout(false);
            this.gbViewMelody.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picRec)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picPlay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picStop)).EndInit();
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picViewMelody)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.GroupBox gbMelody;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ToolStripMenuItem voiceTypeWizardToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.Label lblNotRegistered;
        private System.Windows.Forms.Label lblFindLicense;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnFreeSinging;
        private System.Windows.Forms.ToolStripButton btnGuidedSinging;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.PictureBox picRec;
        private System.Windows.Forms.PictureBox picPlay;
        private System.Windows.Forms.PictureBox picStop;
        private System.Windows.Forms.Button btnLoadMelody;
        private System.Windows.Forms.Button btnPlayMelody;
        private System.Windows.Forms.GroupBox gbViewMelody;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.PictureBox picViewMelody;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.ToolStripComboBox cmbInputDevice;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripTextBox txtMelodyName;
        private System.Windows.Forms.NumericUpDown numTempo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton btnTranscribe;
        private System.Windows.Forms.ToolStripButton btnStopAll;
        private System.Windows.Forms.ToolStripButton btnSaveSings;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem noteGridToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem soundToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem waveShapeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pianoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sineWaveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem squareWaveToolStripMenuItem;
        private System.Windows.Forms.Button btnIdentifySound;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem intensityGraphToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton btnClient;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripLabel lblNote;
        private System.Windows.Forms.ToolStripMenuItem replayToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem compareToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton btnExtensionWizard;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripButton btnEditMelodies;
        private System.Windows.Forms.ToolStripButton btnOpenFile;
        private System.Windows.Forms.ToolStripButton btnAudioAnalyzer;
        private System.Windows.Forms.ComboBox cmbInitNote;
        private System.Windows.Forms.CheckBox chkModulateFirstNote;
        private System.Windows.Forms.ToolStripMenuItem saveLastPracticeToolStripMenuItem;
    }
}

