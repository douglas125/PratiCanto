namespace ClassifTreinoVoz
{
    partial class frmIdentifyVoice
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmIdentifyVoice));
            this.tabWiz = new System.Windows.Forms.TabControl();
            this.tabIntro = new System.Windows.Forms.TabPage();
            this.lblVoiceCats = new System.Windows.Forms.Label();
            this.btnNext0 = new System.Windows.Forms.Button();
            this.chkDisclaimer = new System.Windows.Forms.CheckBox();
            this.radioFemale = new System.Windows.Forms.RadioButton();
            this.radioMale = new System.Windows.Forms.RadioButton();
            this.txtSinger = new System.Windows.Forms.TextBox();
            this.txtEvaluator = new System.Windows.Forms.TextBox();
            this.lblSinger = new System.Windows.Forms.Label();
            this.lblGender = new System.Windows.Forms.Label();
            this.lblEvaluator = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tabTestParameters = new System.Windows.Forms.TabPage();
            this.btnNext1 = new System.Windows.Forms.Button();
            this.btnBack1 = new System.Windows.Forms.Button();
            this.gbParams = new System.Windows.Forms.GroupBox();
            this.label15 = new System.Windows.Forms.Label();
            this.btnLoadMelody = new System.Windows.Forms.Button();
            this.numBaseTempo = new System.Windows.Forms.NumericUpDown();
            this.label9 = new System.Windows.Forms.Label();
            this.btnPlayMelody = new System.Windows.Forms.Button();
            this.btnIdentify = new System.Windows.Forms.Button();
            this.picBaseMelody = new System.Windows.Forms.PictureBox();
            this.cmbInitNote = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnRefreshMics = new System.Windows.Forms.Button();
            this.cmbMics = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.tabIdentification = new System.Windows.Forms.TabPage();
            this.btnNext2 = new System.Windows.Forms.Button();
            this.btnBack2 = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.picVocalTest = new System.Windows.Forms.PictureBox();
            this.label11 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label12 = new System.Windows.Forms.Label();
            this.numNoteInc = new System.Windows.Forms.NumericUpDown();
            this.btnStopTest = new System.Windows.Forms.Button();
            this.btnPauseTest = new System.Windows.Forms.Button();
            this.btnStartTest = new System.Windows.Forms.Button();
            this.tabReport = new System.Windows.Forms.TabPage();
            this.lblUnsavedTest = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnFinish = new System.Windows.Forms.Button();
            this.btnBack4 = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtReport = new System.Windows.Forms.TextBox();
            this.picVoiceType = new System.Windows.Forms.PictureBox();
            this.lblVoiceType = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.lblExtension = new System.Windows.Forms.Label();
            this.testTimer = new System.Windows.Forms.Timer(this.components);
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.tabWiz.SuspendLayout();
            this.tabIntro.SuspendLayout();
            this.tabTestParameters.SuspendLayout();
            this.gbParams.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numBaseTempo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBaseMelody)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.tabIdentification.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picVocalTest)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numNoteInc)).BeginInit();
            this.tabReport.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picVoiceType)).BeginInit();
            this.SuspendLayout();
            // 
            // tabWiz
            // 
            resources.ApplyResources(this.tabWiz, "tabWiz");
            this.tabWiz.Controls.Add(this.tabIntro);
            this.tabWiz.Controls.Add(this.tabTestParameters);
            this.tabWiz.Controls.Add(this.tabIdentification);
            this.tabWiz.Controls.Add(this.tabReport);
            this.tabWiz.Name = "tabWiz";
            this.tabWiz.SelectedIndex = 0;
            this.tabWiz.SelectedIndexChanged += new System.EventHandler(this.tabWiz_SelectedIndexChanged);
            // 
            // tabIntro
            // 
            this.tabIntro.Controls.Add(this.lblVoiceCats);
            this.tabIntro.Controls.Add(this.btnNext0);
            this.tabIntro.Controls.Add(this.chkDisclaimer);
            this.tabIntro.Controls.Add(this.radioFemale);
            this.tabIntro.Controls.Add(this.radioMale);
            this.tabIntro.Controls.Add(this.txtSinger);
            this.tabIntro.Controls.Add(this.txtEvaluator);
            this.tabIntro.Controls.Add(this.lblSinger);
            this.tabIntro.Controls.Add(this.lblGender);
            this.tabIntro.Controls.Add(this.lblEvaluator);
            this.tabIntro.Controls.Add(this.label2);
            this.tabIntro.Controls.Add(this.label1);
            resources.ApplyResources(this.tabIntro, "tabIntro");
            this.tabIntro.Name = "tabIntro";
            this.tabIntro.UseVisualStyleBackColor = true;
            this.tabIntro.Paint += new System.Windows.Forms.PaintEventHandler(this.tabIntro_Paint);
            // 
            // lblVoiceCats
            // 
            this.lblVoiceCats.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            resources.ApplyResources(this.lblVoiceCats, "lblVoiceCats");
            this.lblVoiceCats.Name = "lblVoiceCats";
            // 
            // btnNext0
            // 
            resources.ApplyResources(this.btnNext0, "btnNext0");
            this.btnNext0.Name = "btnNext0";
            this.btnNext0.UseVisualStyleBackColor = true;
            this.btnNext0.Click += new System.EventHandler(this.btnNext0_Click);
            // 
            // chkDisclaimer
            // 
            resources.ApplyResources(this.chkDisclaimer, "chkDisclaimer");
            this.chkDisclaimer.Name = "chkDisclaimer";
            this.chkDisclaimer.UseVisualStyleBackColor = true;
            this.chkDisclaimer.CheckedChanged += new System.EventHandler(this.chkDisclaimer_CheckedChanged);
            // 
            // radioFemale
            // 
            resources.ApplyResources(this.radioFemale, "radioFemale");
            this.radioFemale.Name = "radioFemale";
            this.radioFemale.UseVisualStyleBackColor = true;
            // 
            // radioMale
            // 
            resources.ApplyResources(this.radioMale, "radioMale");
            this.radioMale.Checked = true;
            this.radioMale.Name = "radioMale";
            this.radioMale.TabStop = true;
            this.radioMale.UseVisualStyleBackColor = true;
            // 
            // txtSinger
            // 
            resources.ApplyResources(this.txtSinger, "txtSinger");
            this.txtSinger.Name = "txtSinger";
            // 
            // txtEvaluator
            // 
            resources.ApplyResources(this.txtEvaluator, "txtEvaluator");
            this.txtEvaluator.Name = "txtEvaluator";
            // 
            // lblSinger
            // 
            resources.ApplyResources(this.lblSinger, "lblSinger");
            this.lblSinger.Name = "lblSinger";
            // 
            // lblGender
            // 
            resources.ApplyResources(this.lblGender, "lblGender");
            this.lblGender.Name = "lblGender";
            // 
            // lblEvaluator
            // 
            resources.ApplyResources(this.lblEvaluator, "lblEvaluator");
            this.lblEvaluator.Name = "lblEvaluator";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label2.Name = "label2";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // tabTestParameters
            // 
            this.tabTestParameters.Controls.Add(this.btnNext1);
            this.tabTestParameters.Controls.Add(this.btnBack1);
            this.tabTestParameters.Controls.Add(this.gbParams);
            this.tabTestParameters.Controls.Add(this.groupBox1);
            resources.ApplyResources(this.tabTestParameters, "tabTestParameters");
            this.tabTestParameters.Name = "tabTestParameters";
            this.tabTestParameters.UseVisualStyleBackColor = true;
            this.tabTestParameters.Paint += new System.Windows.Forms.PaintEventHandler(this.tabTestParameters_Paint);
            // 
            // btnNext1
            // 
            resources.ApplyResources(this.btnNext1, "btnNext1");
            this.btnNext1.Name = "btnNext1";
            this.btnNext1.UseVisualStyleBackColor = true;
            this.btnNext1.Click += new System.EventHandler(this.btnNext1_Click);
            // 
            // btnBack1
            // 
            resources.ApplyResources(this.btnBack1, "btnBack1");
            this.btnBack1.Name = "btnBack1";
            this.btnBack1.UseVisualStyleBackColor = true;
            this.btnBack1.Click += new System.EventHandler(this.btnBack1_Click);
            // 
            // gbParams
            // 
            resources.ApplyResources(this.gbParams, "gbParams");
            this.gbParams.Controls.Add(this.label15);
            this.gbParams.Controls.Add(this.btnLoadMelody);
            this.gbParams.Controls.Add(this.numBaseTempo);
            this.gbParams.Controls.Add(this.label9);
            this.gbParams.Controls.Add(this.btnPlayMelody);
            this.gbParams.Controls.Add(this.btnIdentify);
            this.gbParams.Controls.Add(this.picBaseMelody);
            this.gbParams.Controls.Add(this.cmbInitNote);
            this.gbParams.Controls.Add(this.label10);
            this.gbParams.Controls.Add(this.label8);
            this.gbParams.Controls.Add(this.label7);
            this.gbParams.Name = "gbParams";
            this.gbParams.TabStop = false;
            this.gbParams.Paint += new System.Windows.Forms.PaintEventHandler(this.gbParams_Paint);
            // 
            // label15
            // 
            resources.ApplyResources(this.label15, "label15");
            this.label15.Name = "label15";
            // 
            // btnLoadMelody
            // 
            resources.ApplyResources(this.btnLoadMelody, "btnLoadMelody");
            this.btnLoadMelody.Name = "btnLoadMelody";
            this.toolTip.SetToolTip(this.btnLoadMelody, resources.GetString("btnLoadMelody.ToolTip"));
            this.btnLoadMelody.UseVisualStyleBackColor = true;
            this.btnLoadMelody.Click += new System.EventHandler(this.btnLoadMelody_Click);
            // 
            // numBaseTempo
            // 
            this.numBaseTempo.DecimalPlaces = 1;
            this.numBaseTempo.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            resources.ApplyResources(this.numBaseTempo, "numBaseTempo");
            this.numBaseTempo.Maximum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.numBaseTempo.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numBaseTempo.Name = "numBaseTempo";
            this.numBaseTempo.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numBaseTempo.ValueChanged += new System.EventHandler(this.numBaseTempo_ValueChanged);
            // 
            // label9
            // 
            resources.ApplyResources(this.label9, "label9");
            this.label9.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label9.Name = "label9";
            // 
            // btnPlayMelody
            // 
            resources.ApplyResources(this.btnPlayMelody, "btnPlayMelody");
            this.btnPlayMelody.Name = "btnPlayMelody";
            this.btnPlayMelody.UseVisualStyleBackColor = true;
            this.btnPlayMelody.Click += new System.EventHandler(this.btnPlayMelody_Click);
            // 
            // btnIdentify
            // 
            resources.ApplyResources(this.btnIdentify, "btnIdentify");
            this.btnIdentify.Name = "btnIdentify";
            this.btnIdentify.UseVisualStyleBackColor = true;
            this.btnIdentify.Click += new System.EventHandler(this.btnIdentify_Click);
            // 
            // picBaseMelody
            // 
            resources.ApplyResources(this.picBaseMelody, "picBaseMelody");
            this.picBaseMelody.BackColor = System.Drawing.Color.White;
            this.picBaseMelody.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picBaseMelody.Name = "picBaseMelody";
            this.picBaseMelody.TabStop = false;
            this.picBaseMelody.Paint += new System.Windows.Forms.PaintEventHandler(this.picBaseMelody_Paint);
            // 
            // cmbInitNote
            // 
            this.cmbInitNote.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbInitNote.FormattingEnabled = true;
            resources.ApplyResources(this.cmbInitNote, "cmbInitNote");
            this.cmbInitNote.Name = "cmbInitNote";
            this.cmbInitNote.SelectedIndexChanged += new System.EventHandler(this.cmbInitNote_SelectedIndexChanged);
            // 
            // label10
            // 
            resources.ApplyResources(this.label10, "label10");
            this.label10.Name = "label10";
            // 
            // label8
            // 
            resources.ApplyResources(this.label8, "label8");
            this.label8.Name = "label8";
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.Name = "label7";
            // 
            // groupBox1
            // 
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Controls.Add(this.btnRefreshMics);
            this.groupBox1.Controls.Add(this.cmbMics);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            this.groupBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.groupBox1_Paint);
            // 
            // btnRefreshMics
            // 
            resources.ApplyResources(this.btnRefreshMics, "btnRefreshMics");
            this.btnRefreshMics.Name = "btnRefreshMics";
            this.btnRefreshMics.UseVisualStyleBackColor = true;
            this.btnRefreshMics.Click += new System.EventHandler(this.btnRefreshMics_Click);
            // 
            // cmbMics
            // 
            resources.ApplyResources(this.cmbMics, "cmbMics");
            this.cmbMics.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMics.FormattingEnabled = true;
            this.cmbMics.Name = "cmbMics";
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            // 
            // tabIdentification
            // 
            this.tabIdentification.Controls.Add(this.btnNext2);
            this.tabIdentification.Controls.Add(this.btnBack2);
            this.tabIdentification.Controls.Add(this.groupBox4);
            this.tabIdentification.Controls.Add(this.groupBox3);
            resources.ApplyResources(this.tabIdentification, "tabIdentification");
            this.tabIdentification.Name = "tabIdentification";
            this.tabIdentification.UseVisualStyleBackColor = true;
            this.tabIdentification.Paint += new System.Windows.Forms.PaintEventHandler(this.tabIdentification_Paint);
            // 
            // btnNext2
            // 
            resources.ApplyResources(this.btnNext2, "btnNext2");
            this.btnNext2.Name = "btnNext2";
            this.btnNext2.UseVisualStyleBackColor = true;
            this.btnNext2.Click += new System.EventHandler(this.btnNext2_Click);
            // 
            // btnBack2
            // 
            resources.ApplyResources(this.btnBack2, "btnBack2");
            this.btnBack2.Name = "btnBack2";
            this.btnBack2.UseVisualStyleBackColor = true;
            this.btnBack2.Click += new System.EventHandler(this.btnBack2_Click);
            // 
            // groupBox4
            // 
            resources.ApplyResources(this.groupBox4, "groupBox4");
            this.groupBox4.Controls.Add(this.picVocalTest);
            this.groupBox4.Controls.Add(this.label11);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.TabStop = false;
            this.groupBox4.Paint += new System.Windows.Forms.PaintEventHandler(this.groupBox4_Paint);
            // 
            // picVocalTest
            // 
            resources.ApplyResources(this.picVocalTest, "picVocalTest");
            this.picVocalTest.BackColor = System.Drawing.Color.White;
            this.picVocalTest.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picVocalTest.Name = "picVocalTest";
            this.picVocalTest.TabStop = false;
            this.picVocalTest.Paint += new System.Windows.Forms.PaintEventHandler(this.picVocalTest_Paint);
            // 
            // label11
            // 
            resources.ApplyResources(this.label11, "label11");
            this.label11.Name = "label11";
            // 
            // groupBox3
            // 
            resources.ApplyResources(this.groupBox3, "groupBox3");
            this.groupBox3.Controls.Add(this.label12);
            this.groupBox3.Controls.Add(this.numNoteInc);
            this.groupBox3.Controls.Add(this.btnStopTest);
            this.groupBox3.Controls.Add(this.btnPauseTest);
            this.groupBox3.Controls.Add(this.btnStartTest);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.TabStop = false;
            this.groupBox3.Paint += new System.Windows.Forms.PaintEventHandler(this.groupBox3_Paint);
            // 
            // label12
            // 
            resources.ApplyResources(this.label12, "label12");
            this.label12.Name = "label12";
            // 
            // numNoteInc
            // 
            resources.ApplyResources(this.numNoteInc, "numNoteInc");
            this.numNoteInc.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numNoteInc.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numNoteInc.Name = "numNoteInc";
            this.numNoteInc.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // btnStopTest
            // 
            resources.ApplyResources(this.btnStopTest, "btnStopTest");
            this.btnStopTest.Name = "btnStopTest";
            this.btnStopTest.UseVisualStyleBackColor = true;
            this.btnStopTest.Click += new System.EventHandler(this.btnStopTest_Click);
            // 
            // btnPauseTest
            // 
            resources.ApplyResources(this.btnPauseTest, "btnPauseTest");
            this.btnPauseTest.Name = "btnPauseTest";
            this.btnPauseTest.UseVisualStyleBackColor = true;
            this.btnPauseTest.Click += new System.EventHandler(this.btnPauseTest_Click);
            // 
            // btnStartTest
            // 
            resources.ApplyResources(this.btnStartTest, "btnStartTest");
            this.btnStartTest.Name = "btnStartTest";
            this.btnStartTest.UseVisualStyleBackColor = true;
            this.btnStartTest.Click += new System.EventHandler(this.btnStartTest_Click);
            // 
            // tabReport
            // 
            this.tabReport.Controls.Add(this.lblUnsavedTest);
            this.tabReport.Controls.Add(this.btnSave);
            this.tabReport.Controls.Add(this.btnFinish);
            this.tabReport.Controls.Add(this.btnBack4);
            this.tabReport.Controls.Add(this.groupBox2);
            resources.ApplyResources(this.tabReport, "tabReport");
            this.tabReport.Name = "tabReport";
            this.tabReport.UseVisualStyleBackColor = true;
            this.tabReport.Paint += new System.Windows.Forms.PaintEventHandler(this.tabReport_Paint);
            // 
            // lblUnsavedTest
            // 
            resources.ApplyResources(this.lblUnsavedTest, "lblUnsavedTest");
            this.lblUnsavedTest.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblUnsavedTest.Name = "lblUnsavedTest";
            // 
            // btnSave
            // 
            resources.ApplyResources(this.btnSave, "btnSave");
            this.btnSave.Name = "btnSave";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnFinish
            // 
            resources.ApplyResources(this.btnFinish, "btnFinish");
            this.btnFinish.Name = "btnFinish";
            this.btnFinish.UseVisualStyleBackColor = true;
            this.btnFinish.Click += new System.EventHandler(this.btnFinish_Click);
            // 
            // btnBack4
            // 
            resources.ApplyResources(this.btnBack4, "btnBack4");
            this.btnBack4.Name = "btnBack4";
            this.btnBack4.UseVisualStyleBackColor = true;
            this.btnBack4.Click += new System.EventHandler(this.btnBack4_Click);
            // 
            // groupBox2
            // 
            resources.ApplyResources(this.groupBox2, "groupBox2");
            this.groupBox2.Controls.Add(this.txtReport);
            this.groupBox2.Controls.Add(this.picVoiceType);
            this.groupBox2.Controls.Add(this.lblVoiceType);
            this.groupBox2.Controls.Add(this.label14);
            this.groupBox2.Controls.Add(this.lblExtension);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.TabStop = false;
            this.groupBox2.Paint += new System.Windows.Forms.PaintEventHandler(this.groupBox2_Paint);
            // 
            // txtReport
            // 
            resources.ApplyResources(this.txtReport, "txtReport");
            this.txtReport.Name = "txtReport";
            // 
            // picVoiceType
            // 
            resources.ApplyResources(this.picVoiceType, "picVoiceType");
            this.picVoiceType.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picVoiceType.Name = "picVoiceType";
            this.picVoiceType.TabStop = false;
            // 
            // lblVoiceType
            // 
            resources.ApplyResources(this.lblVoiceType, "lblVoiceType");
            this.lblVoiceType.Name = "lblVoiceType";
            // 
            // label14
            // 
            resources.ApplyResources(this.label14, "label14");
            this.label14.Name = "label14";
            // 
            // lblExtension
            // 
            resources.ApplyResources(this.lblExtension, "lblExtension");
            this.lblExtension.Name = "lblExtension";
            // 
            // testTimer
            // 
            this.testTimer.Interval = 5;
            this.testTimer.Tick += new System.EventHandler(this.testTimer_Tick);
            // 
            // frmIdentifyVoice
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.tabWiz);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmIdentifyVoice";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmIdentifyVoice_FormClosing);
            this.Load += new System.EventHandler(this.frmIdentifyVoice_Load);
            this.tabWiz.ResumeLayout(false);
            this.tabIntro.ResumeLayout(false);
            this.tabIntro.PerformLayout();
            this.tabTestParameters.ResumeLayout(false);
            this.gbParams.ResumeLayout(false);
            this.gbParams.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numBaseTempo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBaseMelody)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabIdentification.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picVocalTest)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numNoteInc)).EndInit();
            this.tabReport.ResumeLayout(false);
            this.tabReport.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picVoiceType)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabWiz;
        private System.Windows.Forms.TabPage tabIntro;
        private System.Windows.Forms.TabPage tabTestParameters;
        private System.Windows.Forms.TabPage tabIdentification;
        private System.Windows.Forms.TabPage tabReport;
        private System.Windows.Forms.RadioButton radioFemale;
        private System.Windows.Forms.RadioButton radioMale;
        private System.Windows.Forms.TextBox txtEvaluator;
        private System.Windows.Forms.Label lblGender;
        private System.Windows.Forms.Label lblEvaluator;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chkDisclaimer;
        private System.Windows.Forms.TextBox txtSinger;
        private System.Windows.Forms.Label lblSinger;
        private System.Windows.Forms.Button btnNext0;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnRefreshMics;
        private System.Windows.Forms.ComboBox cmbMics;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox gbParams;
        private System.Windows.Forms.ComboBox cmbInitNote;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btnBack1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button btnPlayMelody;
        private System.Windows.Forms.Button btnIdentify;
        private System.Windows.Forms.PictureBox picBaseMelody;
        private System.Windows.Forms.Button btnNext1;
        private System.Windows.Forms.NumericUpDown numBaseTempo;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnStopTest;
        private System.Windows.Forms.Button btnPauseTest;
        private System.Windows.Forms.Button btnStartTest;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button btnNext2;
        private System.Windows.Forms.Button btnBack2;
        private System.Windows.Forms.PictureBox picVocalTest;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.NumericUpDown numNoteInc;
        private System.Windows.Forms.Timer testTimer;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label lblVoiceType;
        private System.Windows.Forms.Label lblExtension;
        private System.Windows.Forms.PictureBox picVoiceType;
        private System.Windows.Forms.Button btnFinish;
        private System.Windows.Forms.Button btnBack4;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnLoadMelody;
        private System.Windows.Forms.Label lblUnsavedTest;
        private System.Windows.Forms.Label lblVoiceCats;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox txtReport;
    }
}