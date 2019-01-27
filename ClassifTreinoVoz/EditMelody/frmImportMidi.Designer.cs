namespace PratiCanto.EditMelody
{
    partial class frmImportMidi
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmImportMidi));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtInfoMidi = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.numAllVols = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.numVolume = new System.Windows.Forms.NumericUpDown();
            this.numTempo = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.btnStopReplay = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnReplayMelody = new System.Windows.Forms.Button();
            this.btnReplayAll = new System.Windows.Forms.Button();
            this.cmbTrack = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numAllVols)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numVolume)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTempo)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Controls.Add(this.txtInfoMidi);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            this.toolTip.SetToolTip(this.groupBox1, resources.GetString("groupBox1.ToolTip"));
            this.groupBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.groupBox1_Paint);
            // 
            // txtInfoMidi
            // 
            resources.ApplyResources(this.txtInfoMidi, "txtInfoMidi");
            this.txtInfoMidi.Name = "txtInfoMidi";
            this.txtInfoMidi.ReadOnly = true;
            this.toolTip.SetToolTip(this.txtInfoMidi, resources.GetString("txtInfoMidi.ToolTip"));
            // 
            // groupBox2
            // 
            resources.ApplyResources(this.groupBox2, "groupBox2");
            this.groupBox2.Controls.Add(this.numAllVols);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.numVolume);
            this.groupBox2.Controls.Add(this.numTempo);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.btnStopReplay);
            this.groupBox2.Controls.Add(this.btnOk);
            this.groupBox2.Controls.Add(this.btnReplayMelody);
            this.groupBox2.Controls.Add(this.btnReplayAll);
            this.groupBox2.Controls.Add(this.cmbTrack);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.TabStop = false;
            this.toolTip.SetToolTip(this.groupBox2, resources.GetString("groupBox2.ToolTip"));
            this.groupBox2.Paint += new System.Windows.Forms.PaintEventHandler(this.groupBox2_Paint);
            // 
            // numAllVols
            // 
            resources.ApplyResources(this.numAllVols, "numAllVols");
            this.numAllVols.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numAllVols.Name = "numAllVols";
            this.toolTip.SetToolTip(this.numAllVols, resources.GetString("numAllVols.ToolTip"));
            this.numAllVols.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numAllVols.ValueChanged += new System.EventHandler(this.numAllVols_ValueChanged);
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Name = "label4";
            this.toolTip.SetToolTip(this.label4, resources.GetString("label4.ToolTip"));
            // 
            // numVolume
            // 
            resources.ApplyResources(this.numVolume, "numVolume");
            this.numVolume.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numVolume.Name = "numVolume";
            this.toolTip.SetToolTip(this.numVolume, resources.GetString("numVolume.ToolTip"));
            this.numVolume.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numVolume.ValueChanged += new System.EventHandler(this.numVolume_ValueChanged);
            // 
            // numTempo
            // 
            resources.ApplyResources(this.numTempo, "numTempo");
            this.numTempo.DecimalPlaces = 2;
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
            this.toolTip.SetToolTip(this.numTempo, resources.GetString("numTempo.ToolTip"));
            this.numTempo.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numTempo.ValueChanged += new System.EventHandler(this.numTempo_ValueChanged);
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Name = "label2";
            this.toolTip.SetToolTip(this.label2, resources.GetString("label2.ToolTip"));
            // 
            // btnStopReplay
            // 
            resources.ApplyResources(this.btnStopReplay, "btnStopReplay");
            this.btnStopReplay.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnStopReplay.Name = "btnStopReplay";
            this.toolTip.SetToolTip(this.btnStopReplay, resources.GetString("btnStopReplay.ToolTip"));
            this.btnStopReplay.UseVisualStyleBackColor = true;
            this.btnStopReplay.Click += new System.EventHandler(this.btnStopReplay_Click);
            // 
            // btnOk
            // 
            resources.ApplyResources(this.btnOk, "btnOk");
            this.btnOk.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnOk.ForeColor = System.Drawing.Color.White;
            this.btnOk.Name = "btnOk";
            this.toolTip.SetToolTip(this.btnOk, resources.GetString("btnOk.ToolTip"));
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnReplayMelody
            // 
            resources.ApplyResources(this.btnReplayMelody, "btnReplayMelody");
            this.btnReplayMelody.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnReplayMelody.Name = "btnReplayMelody";
            this.toolTip.SetToolTip(this.btnReplayMelody, resources.GetString("btnReplayMelody.ToolTip"));
            this.btnReplayMelody.UseVisualStyleBackColor = true;
            this.btnReplayMelody.Click += new System.EventHandler(this.btnReplayMelody_Click);
            // 
            // btnReplayAll
            // 
            resources.ApplyResources(this.btnReplayAll, "btnReplayAll");
            this.btnReplayAll.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnReplayAll.Name = "btnReplayAll";
            this.toolTip.SetToolTip(this.btnReplayAll, resources.GetString("btnReplayAll.ToolTip"));
            this.btnReplayAll.UseVisualStyleBackColor = true;
            this.btnReplayAll.Click += new System.EventHandler(this.btnReplayAll_Click);
            // 
            // cmbTrack
            // 
            resources.ApplyResources(this.cmbTrack, "cmbTrack");
            this.cmbTrack.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTrack.FormattingEnabled = true;
            this.cmbTrack.Name = "cmbTrack";
            this.toolTip.SetToolTip(this.cmbTrack, resources.GetString("cmbTrack.ToolTip"));
            this.cmbTrack.SelectedIndexChanged += new System.EventHandler(this.cmbTrack_SelectedIndexChanged);
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Name = "label3";
            this.toolTip.SetToolTip(this.label3, resources.GetString("label3.ToolTip"));
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Name = "label1";
            this.toolTip.SetToolTip(this.label1, resources.GetString("label1.ToolTip"));
            // 
            // frmImportMidi
            // 
            this.AcceptButton = this.btnOk;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "frmImportMidi";
            this.toolTip.SetToolTip(this, resources.GetString("$this.ToolTip"));
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmImportMidi_FormClosing);
            this.Load += new System.EventHandler(this.frmImportMidi_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numAllVols)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numVolume)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTempo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtInfoMidi;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnReplayMelody;
        private System.Windows.Forms.Button btnReplayAll;
        private System.Windows.Forms.ComboBox cmbTrack;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnStopReplay;
        private System.Windows.Forms.NumericUpDown numTempo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numVolume;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.NumericUpDown numAllVols;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnOk;
    }
}