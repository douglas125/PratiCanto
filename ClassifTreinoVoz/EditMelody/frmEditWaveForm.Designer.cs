namespace ClassifTreinoVoz
{
    partial class frmEditWaveForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmEditWaveForm));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnOpenFile = new System.Windows.Forms.ToolStripButton();
            this.btnSaveSings = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnPlay = new System.Windows.Forms.ToolStripButton();
            this.cmbWaveNote = new System.Windows.Forms.ToolStripComboBox();
            this.cmbWaveDuration = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.gbWaveShape = new System.Windows.Forms.GroupBox();
            this.picWaveShape = new System.Windows.Forms.PictureBox();
            this.gbWaveAmplitude = new System.Windows.Forms.GroupBox();
            this.picWaveAmpTime = new System.Windows.Forms.PictureBox();
            this.numMaxTime = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.radioAbs = new System.Windows.Forms.RadioButton();
            this.radioRel = new System.Windows.Forms.RadioButton();
            this.toolStrip1.SuspendLayout();
            this.gbWaveShape.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picWaveShape)).BeginInit();
            this.gbWaveAmplitude.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picWaveAmpTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMaxTime)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnOpenFile,
            this.btnSaveSings,
            this.toolStripSeparator1,
            this.btnPlay,
            this.cmbWaveNote,
            this.cmbWaveDuration,
            this.toolStripSeparator2});
            resources.ApplyResources(this.toolStrip1, "toolStrip1");
            this.toolStrip1.Name = "toolStrip1";
            // 
            // btnOpenFile
            // 
            resources.ApplyResources(this.btnOpenFile, "btnOpenFile");
            this.btnOpenFile.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnOpenFile.Name = "btnOpenFile";
            this.btnOpenFile.Click += new System.EventHandler(this.btnOpenFile_Click);
            // 
            // btnSaveSings
            // 
            resources.ApplyResources(this.btnSaveSings, "btnSaveSings");
            this.btnSaveSings.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnSaveSings.Name = "btnSaveSings";
            this.btnSaveSings.Click += new System.EventHandler(this.btnSaveSings_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            resources.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
            // 
            // btnPlay
            // 
            resources.ApplyResources(this.btnPlay, "btnPlay");
            this.btnPlay.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnPlay.Name = "btnPlay";
            this.btnPlay.Click += new System.EventHandler(this.btnPlay_Click);
            // 
            // cmbWaveNote
            // 
            this.cmbWaveNote.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbWaveNote.Name = "cmbWaveNote";
            resources.ApplyResources(this.cmbWaveNote, "cmbWaveNote");
            // 
            // cmbWaveDuration
            // 
            this.cmbWaveDuration.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbWaveDuration.Name = "cmbWaveDuration";
            resources.ApplyResources(this.cmbWaveDuration, "cmbWaveDuration");
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            resources.ApplyResources(this.toolStripSeparator2, "toolStripSeparator2");
            // 
            // gbWaveShape
            // 
            resources.ApplyResources(this.gbWaveShape, "gbWaveShape");
            this.gbWaveShape.Controls.Add(this.picWaveShape);
            this.gbWaveShape.Name = "gbWaveShape";
            this.gbWaveShape.TabStop = false;
            this.gbWaveShape.Paint += new System.Windows.Forms.PaintEventHandler(this.gbWaveShape_Paint);
            // 
            // picWaveShape
            // 
            resources.ApplyResources(this.picWaveShape, "picWaveShape");
            this.picWaveShape.BackColor = System.Drawing.Color.White;
            this.picWaveShape.Name = "picWaveShape";
            this.picWaveShape.TabStop = false;
            this.picWaveShape.Paint += new System.Windows.Forms.PaintEventHandler(this.picWaveShape_Paint);
            this.picWaveShape.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picWaveShape_MouseDown);
            this.picWaveShape.MouseMove += new System.Windows.Forms.MouseEventHandler(this.picWaveShape_MouseMove);
            this.picWaveShape.MouseUp += new System.Windows.Forms.MouseEventHandler(this.picWaveShape_MouseUp);
            // 
            // gbWaveAmplitude
            // 
            resources.ApplyResources(this.gbWaveAmplitude, "gbWaveAmplitude");
            this.gbWaveAmplitude.Controls.Add(this.picWaveAmpTime);
            this.gbWaveAmplitude.Controls.Add(this.numMaxTime);
            this.gbWaveAmplitude.Controls.Add(this.label1);
            this.gbWaveAmplitude.Controls.Add(this.radioAbs);
            this.gbWaveAmplitude.Controls.Add(this.radioRel);
            this.gbWaveAmplitude.Name = "gbWaveAmplitude";
            this.gbWaveAmplitude.TabStop = false;
            this.gbWaveAmplitude.Paint += new System.Windows.Forms.PaintEventHandler(this.gbWaveAmplitude_Paint);
            // 
            // picWaveAmpTime
            // 
            resources.ApplyResources(this.picWaveAmpTime, "picWaveAmpTime");
            this.picWaveAmpTime.BackColor = System.Drawing.Color.White;
            this.picWaveAmpTime.Name = "picWaveAmpTime";
            this.picWaveAmpTime.TabStop = false;
            this.picWaveAmpTime.Paint += new System.Windows.Forms.PaintEventHandler(this.picWaveAmpTime_Paint);
            this.picWaveAmpTime.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picWaveAmpTime_MouseDown);
            this.picWaveAmpTime.MouseMove += new System.Windows.Forms.MouseEventHandler(this.picWaveAmpTime_MouseMove);
            this.picWaveAmpTime.MouseUp += new System.Windows.Forms.MouseEventHandler(this.picWaveAmpTime_MouseUp);
            // 
            // numMaxTime
            // 
            this.numMaxTime.DecimalPlaces = 2;
            resources.ApplyResources(this.numMaxTime, "numMaxTime");
            this.numMaxTime.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numMaxTime.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numMaxTime.Name = "numMaxTime";
            this.numMaxTime.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numMaxTime.ValueChanged += new System.EventHandler(this.numMaxTime_ValueChanged);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // radioAbs
            // 
            resources.ApplyResources(this.radioAbs, "radioAbs");
            this.radioAbs.Name = "radioAbs";
            this.radioAbs.UseVisualStyleBackColor = true;
            this.radioAbs.CheckedChanged += new System.EventHandler(this.radioAbs_CheckedChanged);
            // 
            // radioRel
            // 
            resources.ApplyResources(this.radioRel, "radioRel");
            this.radioRel.Checked = true;
            this.radioRel.Name = "radioRel";
            this.radioRel.TabStop = true;
            this.radioRel.UseVisualStyleBackColor = true;
            this.radioRel.CheckedChanged += new System.EventHandler(this.radioRel_CheckedChanged);
            // 
            // frmEditWaveForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.gbWaveAmplitude);
            this.Controls.Add(this.gbWaveShape);
            this.Controls.Add(this.toolStrip1);
            this.Name = "frmEditWaveForm";
            this.Load += new System.EventHandler(this.frmEditWaveForm_Load);
            this.Resize += new System.EventHandler(this.frmEditWaveForm_Resize);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.gbWaveShape.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picWaveShape)).EndInit();
            this.gbWaveAmplitude.ResumeLayout(false);
            this.gbWaveAmplitude.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picWaveAmpTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMaxTime)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnOpenFile;
        private System.Windows.Forms.ToolStripButton btnSaveSings;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnPlay;
        private System.Windows.Forms.ToolStripComboBox cmbWaveNote;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.GroupBox gbWaveShape;
        private System.Windows.Forms.GroupBox gbWaveAmplitude;
        private System.Windows.Forms.NumericUpDown numMaxTime;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton radioAbs;
        private System.Windows.Forms.RadioButton radioRel;
        private System.Windows.Forms.PictureBox picWaveShape;
        private System.Windows.Forms.PictureBox picWaveAmpTime;
        private System.Windows.Forms.ToolStripComboBox cmbWaveDuration;
    }
}