namespace SustainedVowel
{
    partial class frmSustainedVowel
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSustainedVowel));
            this.btnRecStop = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblSpecInfo = new System.Windows.Forms.Label();
            this.glPicSpectre = new OpenTK.GLControl();
            this.picControlBar = new System.Windows.Forms.PictureBox();
            this.GLPicIntens = new OpenTK.GLControl();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnAnalyze = new System.Windows.Forms.Button();
            this.numEvolPts = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.gbStar = new System.Windows.Forms.GroupBox();
            this.numAmplif = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.picVoiceQualityEvol = new System.Windows.Forms.PictureBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.lstNumInfo = new System.Windows.Forms.ListBox();
            this.cmbNumList = new System.Windows.Forms.ComboBox();
            this.gbDetails = new System.Windows.Forms.GroupBox();
            this.txtRegInfoDetails = new System.Windows.Forms.TextBox();
            this.cmbInputDevice = new System.Windows.Forms.ComboBox();
            this.btnPlay = new System.Windows.Forms.Button();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.txtAnnotation = new System.Windows.Forms.ToolStripTextBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picControlBar)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numEvolPts)).BeginInit();
            this.gbStar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numAmplif)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picVoiceQualityEvol)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.gbDetails.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnRecStop
            // 
            this.btnRecStop.Location = new System.Drawing.Point(16, 87);
            this.btnRecStop.Margin = new System.Windows.Forms.Padding(4);
            this.btnRecStop.Name = "btnRecStop";
            this.btnRecStop.Size = new System.Drawing.Size(200, 53);
            this.btnRecStop.TabIndex = 0;
            this.btnRecStop.Text = "Record / stop";
            this.btnRecStop.UseVisualStyleBackColor = true;
            this.btnRecStop.Click += new System.EventHandler(this.btnRecStop_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox1.Controls.Add(this.lblSpecInfo);
            this.groupBox1.Controls.Add(this.glPicSpectre);
            this.groupBox1.Controls.Add(this.picControlBar);
            this.groupBox1.Controls.Add(this.GLPicIntens);
            this.groupBox1.Location = new System.Drawing.Point(16, 148);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(728, 514);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Frequency/intensity";
            // 
            // lblSpecInfo
            // 
            this.lblSpecInfo.AutoSize = true;
            this.lblSpecInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblSpecInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.lblSpecInfo.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblSpecInfo.Location = new System.Drawing.Point(9, 231);
            this.lblSpecInfo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSpecInfo.Name = "lblSpecInfo";
            this.lblSpecInfo.Size = new System.Drawing.Size(24, 27);
            this.lblSpecInfo.TabIndex = 18;
            this.lblSpecInfo.Text = "  ";
            // 
            // glPicSpectre
            // 
            this.glPicSpectre.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.glPicSpectre.BackColor = System.Drawing.Color.Black;
            this.glPicSpectre.Cursor = System.Windows.Forms.Cursors.Cross;
            this.glPicSpectre.Location = new System.Drawing.Point(9, 231);
            this.glPicSpectre.Margin = new System.Windows.Forms.Padding(5);
            this.glPicSpectre.Name = "glPicSpectre";
            this.glPicSpectre.Size = new System.Drawing.Size(709, 274);
            this.glPicSpectre.TabIndex = 16;
            this.glPicSpectre.VSync = false;
            // 
            // picControlBar
            // 
            this.picControlBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.picControlBar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picControlBar.Cursor = System.Windows.Forms.Cursors.Cross;
            this.picControlBar.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.picControlBar.Location = new System.Drawing.Point(9, 30);
            this.picControlBar.Margin = new System.Windows.Forms.Padding(4);
            this.picControlBar.Name = "picControlBar";
            this.picControlBar.Size = new System.Drawing.Size(709, 45);
            this.picControlBar.TabIndex = 14;
            this.picControlBar.TabStop = false;
            // 
            // GLPicIntens
            // 
            this.GLPicIntens.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GLPicIntens.BackColor = System.Drawing.Color.Black;
            this.GLPicIntens.Cursor = System.Windows.Forms.Cursors.Cross;
            this.GLPicIntens.Location = new System.Drawing.Point(9, 76);
            this.GLPicIntens.Margin = new System.Windows.Forms.Padding(5);
            this.GLPicIntens.Name = "GLPicIntens";
            this.GLPicIntens.Size = new System.Drawing.Size(709, 145);
            this.GLPicIntens.TabIndex = 15;
            this.GLPicIntens.VSync = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.btnAnalyze);
            this.groupBox2.Controls.Add(this.numEvolPts);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.gbStar);
            this.groupBox2.Controls.Add(this.groupBox3);
            this.groupBox2.Controls.Add(this.gbDetails);
            this.groupBox2.Location = new System.Drawing.Point(752, 148);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox2.Size = new System.Drawing.Size(747, 514);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Analysis";
            // 
            // btnAnalyze
            // 
            this.btnAnalyze.Location = new System.Drawing.Point(313, 20);
            this.btnAnalyze.Margin = new System.Windows.Forms.Padding(4);
            this.btnAnalyze.Name = "btnAnalyze";
            this.btnAnalyze.Size = new System.Drawing.Size(241, 41);
            this.btnAnalyze.TabIndex = 8;
            this.btnAnalyze.Text = "Analyze";
            this.btnAnalyze.UseVisualStyleBackColor = true;
            this.btnAnalyze.Click += new System.EventHandler(this.btnAnalyze_Click);
            // 
            // numEvolPts
            // 
            this.numEvolPts.Location = new System.Drawing.Point(197, 27);
            this.numEvolPts.Margin = new System.Windows.Forms.Padding(4);
            this.numEvolPts.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numEvolPts.Name = "numEvolPts";
            this.numEvolPts.Size = new System.Drawing.Size(76, 22);
            this.numEvolPts.TabIndex = 7;
            this.numEvolPts.Value = new decimal(new int[] {
            14,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label1.Location = new System.Drawing.Point(13, 30);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(112, 17);
            this.label1.TabIndex = 6;
            this.label1.Text = "Evolution points:";
            // 
            // gbStar
            // 
            this.gbStar.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbStar.Controls.Add(this.numAmplif);
            this.gbStar.Controls.Add(this.label2);
            this.gbStar.Controls.Add(this.picVoiceQualityEvol);
            this.gbStar.Location = new System.Drawing.Point(169, 208);
            this.gbStar.Margin = new System.Windows.Forms.Padding(4);
            this.gbStar.Name = "gbStar";
            this.gbStar.Padding = new System.Windows.Forms.Padding(4);
            this.gbStar.Size = new System.Drawing.Size(569, 298);
            this.gbStar.TabIndex = 5;
            this.gbStar.TabStop = false;
            this.gbStar.Text = "Voice quality evolution";
            // 
            // numAmplif
            // 
            this.numAmplif.Location = new System.Drawing.Point(171, 22);
            this.numAmplif.Margin = new System.Windows.Forms.Padding(4);
            this.numAmplif.Minimum = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.numAmplif.Name = "numAmplif";
            this.numAmplif.Size = new System.Drawing.Size(76, 22);
            this.numAmplif.TabIndex = 6;
            this.numAmplif.Value = new decimal(new int[] {
            12,
            0,
            0,
            0});
            this.numAmplif.ValueChanged += new System.EventHandler(this.numAmplif_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label2.Location = new System.Drawing.Point(9, 25);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(111, 17);
            this.label2.TabIndex = 1;
            this.label2.Text = "Center distance:";
            // 
            // picVoiceQualityEvol
            // 
            this.picVoiceQualityEvol.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.picVoiceQualityEvol.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picVoiceQualityEvol.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.picVoiceQualityEvol.Location = new System.Drawing.Point(8, 54);
            this.picVoiceQualityEvol.Margin = new System.Windows.Forms.Padding(4);
            this.picVoiceQualityEvol.Name = "picVoiceQualityEvol";
            this.picVoiceQualityEvol.Size = new System.Drawing.Size(553, 236);
            this.picVoiceQualityEvol.TabIndex = 0;
            this.picVoiceQualityEvol.TabStop = false;
            this.picVoiceQualityEvol.Paint += new System.Windows.Forms.PaintEventHandler(this.picVoiceQualityEvol_Paint);
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox3.Controls.Add(this.lstNumInfo);
            this.groupBox3.Controls.Add(this.cmbNumList);
            this.groupBox3.Location = new System.Drawing.Point(8, 55);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox3.Size = new System.Drawing.Size(145, 475);
            this.groupBox3.TabIndex = 4;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Frequencies (Hz)";
            // 
            // lstNumInfo
            // 
            this.lstNumInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstNumInfo.FormattingEnabled = true;
            this.lstNumInfo.ItemHeight = 16;
            this.lstNumInfo.Location = new System.Drawing.Point(9, 58);
            this.lstNumInfo.Margin = new System.Windows.Forms.Padding(4);
            this.lstNumInfo.Name = "lstNumInfo";
            this.lstNumInfo.Size = new System.Drawing.Size(127, 388);
            this.lstNumInfo.TabIndex = 1;
            // 
            // cmbNumList
            // 
            this.cmbNumList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbNumList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbNumList.FormattingEnabled = true;
            this.cmbNumList.Items.AddRange(new object[] {
            "Fundamental",
            "Formant 1",
            "Formant 2",
            "Formant 3",
            "Formant 4",
            "Formant 5",
            "Formant 6"});
            this.cmbNumList.Location = new System.Drawing.Point(9, 25);
            this.cmbNumList.Margin = new System.Windows.Forms.Padding(4);
            this.cmbNumList.Name = "cmbNumList";
            this.cmbNumList.Size = new System.Drawing.Size(127, 24);
            this.cmbNumList.TabIndex = 0;
            this.cmbNumList.SelectedIndexChanged += new System.EventHandler(this.cmbNumList_SelectedIndexChanged);
            // 
            // gbDetails
            // 
            this.gbDetails.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbDetails.Controls.Add(this.txtRegInfoDetails);
            this.gbDetails.Location = new System.Drawing.Point(161, 55);
            this.gbDetails.Margin = new System.Windows.Forms.Padding(4);
            this.gbDetails.Name = "gbDetails";
            this.gbDetails.Padding = new System.Windows.Forms.Padding(4);
            this.gbDetails.Size = new System.Drawing.Size(577, 145);
            this.gbDetails.TabIndex = 3;
            this.gbDetails.TabStop = false;
            this.gbDetails.Text = "Details";
            // 
            // txtRegInfoDetails
            // 
            this.txtRegInfoDetails.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtRegInfoDetails.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.txtRegInfoDetails.Location = new System.Drawing.Point(8, 26);
            this.txtRegInfoDetails.Margin = new System.Windows.Forms.Padding(4);
            this.txtRegInfoDetails.Multiline = true;
            this.txtRegInfoDetails.Name = "txtRegInfoDetails";
            this.txtRegInfoDetails.ReadOnly = true;
            this.txtRegInfoDetails.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtRegInfoDetails.Size = new System.Drawing.Size(560, 111);
            this.txtRegInfoDetails.TabIndex = 0;
            this.txtRegInfoDetails.Text = "Details";
            // 
            // cmbInputDevice
            // 
            this.cmbInputDevice.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbInputDevice.FormattingEnabled = true;
            this.cmbInputDevice.Location = new System.Drawing.Point(16, 54);
            this.cmbInputDevice.Margin = new System.Windows.Forms.Padding(4);
            this.cmbInputDevice.Name = "cmbInputDevice";
            this.cmbInputDevice.Size = new System.Drawing.Size(837, 24);
            this.cmbInputDevice.TabIndex = 3;
            // 
            // btnPlay
            // 
            this.btnPlay.Location = new System.Drawing.Point(249, 87);
            this.btnPlay.Margin = new System.Windows.Forms.Padding(4);
            this.btnPlay.Name = "btnPlay";
            this.btnPlay.Size = new System.Drawing.Size(171, 53);
            this.btnPlay.TabIndex = 4;
            this.btnPlay.Text = "Play";
            this.btnPlay.UseVisualStyleBackColor = true;
            this.btnPlay.Click += new System.EventHandler(this.btnPlay_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.txtAnnotation});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1515, 39);
            this.toolStrip1.TabIndex = 5;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // txtAnnotation
            // 
            this.txtAnnotation.Font = new System.Drawing.Font("Segoe UI", 14.25F);
            this.txtAnnotation.Name = "txtAnnotation";
            this.txtAnnotation.Size = new System.Drawing.Size(199, 39);
            this.txtAnnotation.Text = "Notation";
            this.txtAnnotation.ToolTipText = "Phonetic notation";
            // 
            // frmSustainedVowel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1515, 690);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.btnPlay);
            this.Controls.Add(this.cmbInputDevice);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnRecStop);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frmSustainedVowel";
            this.Text = "Sustained Vowel Analysis";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmSustainedVowel_Load);
            this.Resize += new System.EventHandler(this.frmSustainedVowel_Resize);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picControlBar)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numEvolPts)).EndInit();
            this.gbStar.ResumeLayout(false);
            this.gbStar.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numAmplif)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picVoiceQualityEvol)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.gbDetails.ResumeLayout(false);
            this.gbDetails.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnRecStop;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox cmbInputDevice;
        private System.Windows.Forms.Button btnPlay;
        private OpenTK.GLControl glPicSpectre;
        private System.Windows.Forms.PictureBox picControlBar;
        private OpenTK.GLControl GLPicIntens;
        private System.Windows.Forms.Label lblSpecInfo;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripTextBox txtAnnotation;
        private System.Windows.Forms.GroupBox gbDetails;
        private System.Windows.Forms.TextBox txtRegInfoDetails;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ListBox lstNumInfo;
        private System.Windows.Forms.ComboBox cmbNumList;
        private System.Windows.Forms.GroupBox gbStar;
        private System.Windows.Forms.NumericUpDown numAmplif;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox picVoiceQualityEvol;
        private System.Windows.Forms.NumericUpDown numEvolPts;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnAnalyze;
    }
}

