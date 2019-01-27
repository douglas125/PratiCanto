namespace ClassifTreinoVoz
{
    partial class frmEditMelody
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmEditMelody));
            this.gbViewMelody = new System.Windows.Forms.GroupBox();
            this.numTempo = new System.Windows.Forms.NumericUpDown();
            this.picPlay = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnPlayMelody = new System.Windows.Forms.Button();
            this.picStop = new System.Windows.Forms.PictureBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnExtensionWizard = new System.Windows.Forms.ToolStripButton();
            this.btnSaveSings = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.txtAddNote = new System.Windows.Forms.ToolStripTextBox();
            this.btnAddNote = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnDelMelody = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.btnEditWaveShapes = new System.Windows.Forms.ToolStripButton();
            this.label1 = new System.Windows.Forms.Label();
            this.gbNotes = new System.Windows.Forms.GroupBox();
            this.btnLoadVoice = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.txtNotes = new System.Windows.Forms.TextBox();
            this.lblWrongNoteFormat = new System.Windows.Forms.Label();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cmbKeyboardOctave = new System.Windows.Forms.ComboBox();
            this.txtKeyboardText = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.picPiano = new System.Windows.Forms.PictureBox();
            this.gbViewMelody.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numTempo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picPlay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picStop)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.gbNotes.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picPiano)).BeginInit();
            this.SuspendLayout();
            // 
            // gbViewMelody
            // 
            resources.ApplyResources(this.gbViewMelody, "gbViewMelody");
            this.gbViewMelody.Controls.Add(this.numTempo);
            this.gbViewMelody.Controls.Add(this.picPlay);
            this.gbViewMelody.Controls.Add(this.label2);
            this.gbViewMelody.Controls.Add(this.btnPlayMelody);
            this.gbViewMelody.Controls.Add(this.picStop);
            this.gbViewMelody.Name = "gbViewMelody";
            this.gbViewMelody.TabStop = false;
            this.gbViewMelody.Paint += new System.Windows.Forms.PaintEventHandler(this.gbViewMelody_Paint);
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
            15,
            0,
            0,
            65536});
            this.numTempo.ValueChanged += new System.EventHandler(this.numTempo_ValueChanged);
            // 
            // picPlay
            // 
            resources.ApplyResources(this.picPlay, "picPlay");
            this.picPlay.Name = "picPlay";
            this.picPlay.TabStop = false;
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
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
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnExtensionWizard,
            this.btnSaveSings,
            this.toolStripSeparator1,
            this.toolStripLabel1,
            this.txtAddNote,
            this.btnAddNote,
            this.toolStripSeparator2,
            this.btnDelMelody,
            this.toolStripSeparator3,
            this.btnEditWaveShapes});
            resources.ApplyResources(this.toolStrip1, "toolStrip1");
            this.toolStrip1.Name = "toolStrip1";
            // 
            // btnExtensionWizard
            // 
            resources.ApplyResources(this.btnExtensionWizard, "btnExtensionWizard");
            this.btnExtensionWizard.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnExtensionWizard.Name = "btnExtensionWizard";
            this.btnExtensionWizard.Click += new System.EventHandler(this.btnLoadMelody_Click);
            // 
            // btnSaveSings
            // 
            resources.ApplyResources(this.btnSaveSings, "btnSaveSings");
            this.btnSaveSings.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnSaveSings.Name = "btnSaveSings";
            this.btnSaveSings.Click += new System.EventHandler(this.btnSaveMelody_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            resources.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
            // 
            // toolStripLabel1
            // 
            resources.ApplyResources(this.toolStripLabel1, "toolStripLabel1");
            this.toolStripLabel1.Name = "toolStripLabel1";
            // 
            // txtAddNote
            // 
            resources.ApplyResources(this.txtAddNote, "txtAddNote");
            this.txtAddNote.Name = "txtAddNote";
            // 
            // btnAddNote
            // 
            resources.ApplyResources(this.btnAddNote, "btnAddNote");
            this.btnAddNote.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnAddNote.Name = "btnAddNote";
            this.btnAddNote.Click += new System.EventHandler(this.btnAddNote_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            resources.ApplyResources(this.toolStripSeparator2, "toolStripSeparator2");
            // 
            // btnDelMelody
            // 
            resources.ApplyResources(this.btnDelMelody, "btnDelMelody");
            this.btnDelMelody.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnDelMelody.Name = "btnDelMelody";
            this.btnDelMelody.Click += new System.EventHandler(this.btnDelMelody_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            resources.ApplyResources(this.toolStripSeparator3, "toolStripSeparator3");
            // 
            // btnEditWaveShapes
            // 
            resources.ApplyResources(this.btnEditWaveShapes, "btnEditWaveShapes");
            this.btnEditWaveShapes.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnEditWaveShapes.Name = "btnEditWaveShapes";
            this.btnEditWaveShapes.Click += new System.EventHandler(this.btnEditWaveShapes_Click);
            // 
            // label1
            // 
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // gbNotes
            // 
            resources.ApplyResources(this.gbNotes, "gbNotes");
            this.gbNotes.Controls.Add(this.btnLoadVoice);
            this.gbNotes.Controls.Add(this.btnUpdate);
            this.gbNotes.Controls.Add(this.txtNotes);
            this.gbNotes.Name = "gbNotes";
            this.gbNotes.TabStop = false;
            this.gbNotes.Paint += new System.Windows.Forms.PaintEventHandler(this.gbNotes_Paint);
            // 
            // btnLoadVoice
            // 
            resources.ApplyResources(this.btnLoadVoice, "btnLoadVoice");
            this.btnLoadVoice.Name = "btnLoadVoice";
            this.btnLoadVoice.UseVisualStyleBackColor = true;
            this.btnLoadVoice.Click += new System.EventHandler(this.btnLoadVoice_Click);
            // 
            // btnUpdate
            // 
            resources.ApplyResources(this.btnUpdate, "btnUpdate");
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // txtNotes
            // 
            resources.ApplyResources(this.txtNotes, "txtNotes");
            this.txtNotes.Name = "txtNotes";
            this.txtNotes.Leave += new System.EventHandler(this.txtNotes_Leave);
            // 
            // lblWrongNoteFormat
            // 
            resources.ApplyResources(this.lblWrongNoteFormat, "lblWrongNoteFormat");
            this.lblWrongNoteFormat.Name = "lblWrongNoteFormat";
            // 
            // timer
            // 
            this.timer.Enabled = true;
            this.timer.Interval = 10;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // groupBox2
            // 
            resources.ApplyResources(this.groupBox2, "groupBox2");
            this.groupBox2.Controls.Add(this.cmbKeyboardOctave);
            this.groupBox2.Controls.Add(this.txtKeyboardText);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.picPiano);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.TabStop = false;
            this.groupBox2.Paint += new System.Windows.Forms.PaintEventHandler(this.groupBox2_Paint);
            // 
            // cmbKeyboardOctave
            // 
            this.cmbKeyboardOctave.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbKeyboardOctave.FormattingEnabled = true;
            this.cmbKeyboardOctave.Items.AddRange(new object[] {
            resources.GetString("cmbKeyboardOctave.Items"),
            resources.GetString("cmbKeyboardOctave.Items1"),
            resources.GetString("cmbKeyboardOctave.Items2"),
            resources.GetString("cmbKeyboardOctave.Items3"),
            resources.GetString("cmbKeyboardOctave.Items4"),
            resources.GetString("cmbKeyboardOctave.Items5"),
            resources.GetString("cmbKeyboardOctave.Items6")});
            resources.ApplyResources(this.cmbKeyboardOctave, "cmbKeyboardOctave");
            this.cmbKeyboardOctave.Name = "cmbKeyboardOctave";
            this.cmbKeyboardOctave.SelectedIndexChanged += new System.EventHandler(this.cmbKeyboardOctave_SelectedIndexChanged);
            // 
            // txtKeyboardText
            // 
            resources.ApplyResources(this.txtKeyboardText, "txtKeyboardText");
            this.txtKeyboardText.Name = "txtKeyboardText";
            this.txtKeyboardText.TextChanged += new System.EventHandler(this.txtKeyboardText_TextChanged);
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // picPiano
            // 
            resources.ApplyResources(this.picPiano, "picPiano");
            this.picPiano.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picPiano.Name = "picPiano";
            this.picPiano.TabStop = false;
            this.picPiano.Paint += new System.Windows.Forms.PaintEventHandler(this.picPiano_Paint);
            this.picPiano.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picPiano_MouseDown);
            this.picPiano.MouseUp += new System.Windows.Forms.MouseEventHandler(this.picPiano_MouseUp);
            this.picPiano.Resize += new System.EventHandler(this.picPiano_Resize);
            // 
            // frmEditMelody
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.lblWrongNoteFormat);
            this.Controls.Add(this.gbNotes);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.gbViewMelody);
            this.KeyPreview = true;
            this.Name = "frmEditMelody";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmEditMelody_FormClosing);
            this.Load += new System.EventHandler(this.frmEditMelody_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmEditMelody_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.frmEditMelody_KeyUp);
            this.gbViewMelody.ResumeLayout(false);
            this.gbViewMelody.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numTempo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picPlay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picStop)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.gbNotes.ResumeLayout(false);
            this.gbNotes.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picPiano)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox gbViewMelody;
        private System.Windows.Forms.PictureBox picPlay;
        private System.Windows.Forms.Button btnPlayMelody;
        private System.Windows.Forms.PictureBox picStop;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnExtensionWizard;
        private System.Windows.Forms.ToolStripButton btnSaveSings;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripTextBox txtAddNote;
        private System.Windows.Forms.ToolStripButton btnAddNote;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox gbNotes;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton btnDelMelody;
        public System.Windows.Forms.TextBox txtNotes;
        private System.Windows.Forms.Label lblWrongNoteFormat;
        private System.Windows.Forms.NumericUpDown numTempo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.PictureBox picPiano;
        private System.Windows.Forms.ComboBox cmbKeyboardOctave;
        private System.Windows.Forms.TextBox txtKeyboardText;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton btnEditWaveShapes;
        private System.Windows.Forms.Button btnLoadVoice;
    }
}