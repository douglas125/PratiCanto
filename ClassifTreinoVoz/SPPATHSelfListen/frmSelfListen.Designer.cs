namespace PratiCanto.SPPATHSelfListen
{
    partial class frmSelfListen
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSelfListen));
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
            this.gbEstimParameters = new System.Windows.Forms.GroupBox();
            this.picSpeed = new System.Windows.Forms.PictureBox();
            this.picFreq = new System.Windows.Forms.PictureBox();
            this.picIntens = new System.Windows.Forms.PictureBox();
            this.lblSpeed = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lblIntens = new System.Windows.Forms.Label();
            this.lblFreq = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.numSineFreq = new System.Windows.Forms.NumericUpDown();
            this.numSineAmp = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.numAudioDelay = new System.Windows.Forms.NumericUpDown();
            this.label9 = new System.Windows.Forms.Label();
            this.numFeedbackAmplif = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.chkAutoControlParams = new System.Windows.Forms.CheckBox();
            this.numTargetSpeed = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.numTargetFreq = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.numTargetIntensity = new System.Windows.Forms.NumericUpDown();
            this.chkChangeFreq = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.chkVoiceIntensity = new System.Windows.Forms.CheckBox();
            this.timerSample = new System.Windows.Forms.Timer(this.components);
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picStop)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picPlay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picRec)).BeginInit();
            this.gbEstimParameters.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picSpeed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picFreq)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picIntens)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numSineFreq)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSineAmp)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numAudioDelay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numFeedbackAmplif)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTargetSpeed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTargetFreq)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTargetIntensity)).BeginInit();
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
            this.toolStrip1.Size = new System.Drawing.Size(604, 39);
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
            this.cmbInputDevice.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
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
            this.txtAnnotation.Size = new System.Drawing.Size(101, 39);
            this.txtAnnotation.Text = "Notation";
            this.txtAnnotation.ToolTipText = "Phonetic notation";
            this.txtAnnotation.Visible = false;
            // 
            // picStop
            // 
            this.picStop.Image = ((System.Drawing.Image)(resources.GetObject("picStop.Image")));
            this.picStop.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.picStop.Location = new System.Drawing.Point(165, 220);
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
            this.picPlay.Location = new System.Drawing.Point(202, 220);
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
            this.picRec.Location = new System.Drawing.Point(127, 221);
            this.picRec.Name = "picRec";
            this.picRec.Size = new System.Drawing.Size(30, 30);
            this.picRec.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.picRec.TabIndex = 20;
            this.picRec.TabStop = false;
            this.picRec.Visible = false;
            // 
            // gbEstimParameters
            // 
            this.gbEstimParameters.Controls.Add(this.picSpeed);
            this.gbEstimParameters.Controls.Add(this.picFreq);
            this.gbEstimParameters.Controls.Add(this.picIntens);
            this.gbEstimParameters.Controls.Add(this.lblSpeed);
            this.gbEstimParameters.Controls.Add(this.label6);
            this.gbEstimParameters.Controls.Add(this.lblIntens);
            this.gbEstimParameters.Controls.Add(this.lblFreq);
            this.gbEstimParameters.Controls.Add(this.label5);
            this.gbEstimParameters.Controls.Add(this.label4);
            this.gbEstimParameters.Location = new System.Drawing.Point(8, 38);
            this.gbEstimParameters.Margin = new System.Windows.Forms.Padding(2);
            this.gbEstimParameters.Name = "gbEstimParameters";
            this.gbEstimParameters.Padding = new System.Windows.Forms.Padding(2);
            this.gbEstimParameters.Size = new System.Drawing.Size(218, 177);
            this.gbEstimParameters.TabIndex = 21;
            this.gbEstimParameters.TabStop = false;
            this.gbEstimParameters.Text = "Estimated voice parameters";
            // 
            // picSpeed
            // 
            this.picSpeed.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picSpeed.Location = new System.Drawing.Point(13, 112);
            this.picSpeed.Margin = new System.Windows.Forms.Padding(2);
            this.picSpeed.Name = "picSpeed";
            this.picSpeed.Size = new System.Drawing.Size(202, 14);
            this.picSpeed.TabIndex = 3;
            this.picSpeed.TabStop = false;
            // 
            // picFreq
            // 
            this.picFreq.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picFreq.Location = new System.Drawing.Point(13, 75);
            this.picFreq.Margin = new System.Windows.Forms.Padding(2);
            this.picFreq.Name = "picFreq";
            this.picFreq.Size = new System.Drawing.Size(202, 14);
            this.picFreq.TabIndex = 2;
            this.picFreq.TabStop = false;
            this.picFreq.Paint += new System.Windows.Forms.PaintEventHandler(this.picFreq_Paint);
            // 
            // picIntens
            // 
            this.picIntens.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picIntens.Location = new System.Drawing.Point(13, 40);
            this.picIntens.Margin = new System.Windows.Forms.Padding(2);
            this.picIntens.Name = "picIntens";
            this.picIntens.Size = new System.Drawing.Size(202, 14);
            this.picIntens.TabIndex = 1;
            this.picIntens.TabStop = false;
            this.picIntens.Paint += new System.Windows.Forms.PaintEventHandler(this.picIntens_Paint);
            // 
            // lblSpeed
            // 
            this.lblSpeed.AutoSize = true;
            this.lblSpeed.Location = new System.Drawing.Point(105, 97);
            this.lblSpeed.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblSpeed.Name = "lblSpeed";
            this.lblSpeed.Size = new System.Drawing.Size(13, 13);
            this.lblSpeed.TabIndex = 0;
            this.lblSpeed.Text = "0";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(11, 97);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(76, 13);
            this.label6.TabIndex = 0;
            this.label6.Text = "Speed (dBr/s):";
            // 
            // lblIntens
            // 
            this.lblIntens.AutoSize = true;
            this.lblIntens.Location = new System.Drawing.Point(104, 26);
            this.lblIntens.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblIntens.Name = "lblIntens";
            this.lblIntens.Size = new System.Drawing.Size(13, 13);
            this.lblIntens.TabIndex = 0;
            this.lblIntens.Text = "0";
            // 
            // lblFreq
            // 
            this.lblFreq.AutoSize = true;
            this.lblFreq.Location = new System.Drawing.Point(104, 60);
            this.lblFreq.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblFreq.Name = "lblFreq";
            this.lblFreq.Size = new System.Drawing.Size(13, 13);
            this.lblFreq.TabIndex = 0;
            this.lblFreq.Text = "0";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(10, 26);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(74, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "Intensity (dBr):";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(10, 60);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(82, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Frequency (Hz):";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.numTargetSpeed);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.checkBox1);
            this.groupBox1.Controls.Add(this.numTargetFreq);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.numTargetIntensity);
            this.groupBox1.Controls.Add(this.chkChangeFreq);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.chkVoiceIntensity);
            this.groupBox1.Location = new System.Drawing.Point(230, 38);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox1.Size = new System.Drawing.Size(357, 194);
            this.groupBox1.TabIndex = 21;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Objective";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox2.Controls.Add(this.numSineFreq);
            this.groupBox2.Controls.Add(this.numSineAmp);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.numAudioDelay);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.numFeedbackAmplif);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.chkAutoControlParams);
            this.groupBox2.Location = new System.Drawing.Point(6, 87);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox2.Size = new System.Drawing.Size(343, 102);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Control parameters";
            // 
            // numSineFreq
            // 
            this.numSineFreq.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numSineFreq.Location = new System.Drawing.Point(272, 75);
            this.numSineFreq.Margin = new System.Windows.Forms.Padding(2);
            this.numSineFreq.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.numSineFreq.Minimum = new decimal(new int[] {
            70,
            0,
            0,
            0});
            this.numSineFreq.Name = "numSineFreq";
            this.numSineFreq.Size = new System.Drawing.Size(57, 20);
            this.numSineFreq.TabIndex = 5;
            this.numSineFreq.Value = new decimal(new int[] {
            70,
            0,
            0,
            0});
            // 
            // numSineAmp
            // 
            this.numSineAmp.DecimalPlaces = 1;
            this.numSineAmp.Increment = new decimal(new int[] {
            2,
            0,
            0,
            65536});
            this.numSineAmp.Location = new System.Drawing.Point(211, 75);
            this.numSineAmp.Margin = new System.Windows.Forms.Padding(2);
            this.numSineAmp.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numSineAmp.Name = "numSineAmp";
            this.numSineAmp.Size = new System.Drawing.Size(57, 20);
            this.numSineAmp.TabIndex = 4;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(9, 77);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(188, 13);
            this.label8.TabIndex = 3;
            this.label8.Text = "Sine wave amplitude / frequency (Hz):";
            // 
            // numAudioDelay
            // 
            this.numAudioDelay.Location = new System.Drawing.Point(211, 34);
            this.numAudioDelay.Margin = new System.Windows.Forms.Padding(2);
            this.numAudioDelay.Maximum = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            this.numAudioDelay.Minimum = new decimal(new int[] {
            70,
            0,
            0,
            0});
            this.numAudioDelay.Name = "numAudioDelay";
            this.numAudioDelay.Size = new System.Drawing.Size(57, 20);
            this.numAudioDelay.TabIndex = 4;
            this.numAudioDelay.Value = new decimal(new int[] {
            70,
            0,
            0,
            0});
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(9, 35);
            this.label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(87, 13);
            this.label9.TabIndex = 3;
            this.label9.Text = "Audio delay (ms):";
            // 
            // numFeedbackAmplif
            // 
            this.numFeedbackAmplif.DecimalPlaces = 1;
            this.numFeedbackAmplif.Location = new System.Drawing.Point(211, 55);
            this.numFeedbackAmplif.Margin = new System.Windows.Forms.Padding(2);
            this.numFeedbackAmplif.Maximum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.numFeedbackAmplif.Name = "numFeedbackAmplif";
            this.numFeedbackAmplif.Size = new System.Drawing.Size(57, 20);
            this.numFeedbackAmplif.TabIndex = 4;
            this.numFeedbackAmplif.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(9, 56);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(160, 13);
            this.label7.TabIndex = 3;
            this.label7.Text = "Feedback intensity amplification:";
            // 
            // chkAutoControlParams
            // 
            this.chkAutoControlParams.AutoSize = true;
            this.chkAutoControlParams.Checked = true;
            this.chkAutoControlParams.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoControlParams.Location = new System.Drawing.Point(8, 16);
            this.chkAutoControlParams.Margin = new System.Windows.Forms.Padding(2);
            this.chkAutoControlParams.Name = "chkAutoControlParams";
            this.chkAutoControlParams.Size = new System.Drawing.Size(201, 17);
            this.chkAutoControlParams.TabIndex = 1;
            this.chkAutoControlParams.Text = "Automatic control to reach objectives";
            this.chkAutoControlParams.UseVisualStyleBackColor = true;
            // 
            // numTargetSpeed
            // 
            this.numTargetSpeed.Location = new System.Drawing.Point(291, 66);
            this.numTargetSpeed.Margin = new System.Windows.Forms.Padding(2);
            this.numTargetSpeed.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.numTargetSpeed.Minimum = new decimal(new int[] {
            70,
            0,
            0,
            0});
            this.numTargetSpeed.Name = "numTargetSpeed";
            this.numTargetSpeed.Size = new System.Drawing.Size(57, 20);
            this.numTargetSpeed.TabIndex = 5;
            this.numTargetSpeed.Value = new decimal(new int[] {
            70,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(212, 68);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(76, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Target (dBr/s):";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(6, 67);
            this.checkBox1.Margin = new System.Windows.Forms.Padding(2);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(133, 17);
            this.checkBox1.TabIndex = 3;
            this.checkBox1.Text = "Change speech speed";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // numTargetFreq
            // 
            this.numTargetFreq.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numTargetFreq.Location = new System.Drawing.Point(291, 45);
            this.numTargetFreq.Margin = new System.Windows.Forms.Padding(2);
            this.numTargetFreq.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.numTargetFreq.Minimum = new decimal(new int[] {
            70,
            0,
            0,
            0});
            this.numTargetFreq.Name = "numTargetFreq";
            this.numTargetFreq.Size = new System.Drawing.Size(57, 20);
            this.numTargetFreq.TabIndex = 2;
            this.numTargetFreq.Value = new decimal(new int[] {
            70,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(212, 47);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Target (Hz):";
            // 
            // numTargetIntensity
            // 
            this.numTargetIntensity.Location = new System.Drawing.Point(291, 25);
            this.numTargetIntensity.Margin = new System.Windows.Forms.Padding(2);
            this.numTargetIntensity.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.numTargetIntensity.Name = "numTargetIntensity";
            this.numTargetIntensity.Size = new System.Drawing.Size(57, 20);
            this.numTargetIntensity.TabIndex = 2;
            this.numTargetIntensity.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // chkChangeFreq
            // 
            this.chkChangeFreq.AutoSize = true;
            this.chkChangeFreq.Location = new System.Drawing.Point(6, 46);
            this.chkChangeFreq.Margin = new System.Windows.Forms.Padding(2);
            this.chkChangeFreq.Name = "chkChangeFreq";
            this.chkChangeFreq.Size = new System.Drawing.Size(142, 17);
            this.chkChangeFreq.TabIndex = 0;
            this.chkChangeFreq.Text = "Change voice frequency";
            this.chkChangeFreq.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(212, 26);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Target (dBr):";
            // 
            // chkVoiceIntensity
            // 
            this.chkVoiceIntensity.AutoSize = true;
            this.chkVoiceIntensity.Location = new System.Drawing.Point(6, 25);
            this.chkVoiceIntensity.Margin = new System.Windows.Forms.Padding(2);
            this.chkVoiceIntensity.Name = "chkVoiceIntensity";
            this.chkVoiceIntensity.Size = new System.Drawing.Size(133, 17);
            this.chkVoiceIntensity.TabIndex = 0;
            this.chkVoiceIntensity.Text = "Change voice intensity\r\n";
            this.chkVoiceIntensity.UseVisualStyleBackColor = true;
            this.chkVoiceIntensity.CheckedChanged += new System.EventHandler(this.chkVoiceIntensity_CheckedChanged);
            // 
            // timerSample
            // 
            this.timerSample.Enabled = true;
            this.timerSample.Interval = 20;
            this.timerSample.Tick += new System.EventHandler(this.timerSample_Tick);
            // 
            // frmSelfListen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(604, 277);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.gbEstimParameters);
            this.Controls.Add(this.picRec);
            this.Controls.Add(this.picStop);
            this.Controls.Add(this.picPlay);
            this.Controls.Add(this.toolStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "frmSelfListen";
            this.Text = "Self Listening";
            this.Load += new System.EventHandler(this.frmSelfListen_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picStop)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picPlay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picRec)).EndInit();
            this.gbEstimParameters.ResumeLayout(false);
            this.gbEstimParameters.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picSpeed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picFreq)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picIntens)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numSineFreq)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSineAmp)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numAudioDelay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numFeedbackAmplif)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTargetSpeed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTargetFreq)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTargetIntensity)).EndInit();
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
        private System.Windows.Forms.GroupBox gbEstimParameters;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown numTargetSpeed;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.NumericUpDown numTargetFreq;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numTargetIntensity;
        private System.Windows.Forms.CheckBox chkChangeFreq;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chkVoiceIntensity;
        private System.Windows.Forms.Timer timerSample;
        private System.Windows.Forms.Label lblSpeed;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblIntens;
        private System.Windows.Forms.Label lblFreq;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.NumericUpDown numSineFreq;
        private System.Windows.Forms.NumericUpDown numSineAmp;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.NumericUpDown numAudioDelay;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.NumericUpDown numFeedbackAmplif;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.CheckBox chkAutoControlParams;
        private System.Windows.Forms.PictureBox picSpeed;
        private System.Windows.Forms.PictureBox picFreq;
        private System.Windows.Forms.PictureBox picIntens;
    }
}