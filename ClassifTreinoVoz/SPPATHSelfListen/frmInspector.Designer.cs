namespace PratiCanto
{
    partial class frmInspector
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmInspector));
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.btnStopSelec = new System.Windows.Forms.Button();
            this.btnPlaySelec = new System.Windows.Forms.Button();
            this.txtVarPenalty = new System.Windows.Forms.TextBox();
            this.txtNFreqs = new System.Windows.Forms.TextBox();
            this.txtChangePenalty = new System.Windows.Forms.TextBox();
            this.txtDPenalty = new System.Windows.Forms.TextBox();
            this.lblSpecInfo = new System.Windows.Forms.Label();
            this.picControlBar = new System.Windows.Forms.PictureBox();
            this.GLPicIntens = new OpenTK.GLControl();
            this.glPicSpectre = new OpenTK.GLControl();
            this.label6 = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.lstSearchRes = new System.Windows.Forms.ListBox();
            this.btnPlayRegion = new System.Windows.Forms.Button();
            this.lblProgress = new System.Windows.Forms.Label();
            this.numMaxResultsPerRegion = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.lblFindInfo = new System.Windows.Forms.Label();
            this.txtDescrip = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.chkListSearch = new System.Windows.Forms.CheckedListBox();
            this.btnFindInterestRegions = new System.Windows.Forms.Button();
            this.btnAddSel = new System.Windows.Forms.Button();
            this.gbChooseFileOpen = new System.Windows.Forms.GroupBox();
            this.btnPlayOrigFile = new System.Windows.Forms.Button();
            this.btnStopOrigFile = new System.Windows.Forms.Button();
            this.btnOpenFileComment = new System.Windows.Forms.Button();
            this.picBigAudio = new System.Windows.Forms.PictureBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.txtAnnot = new System.Windows.Forms.ToolStripTextBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.bgComputeBigAudioSpec = new System.ComponentModel.BackgroundWorker();
            this.bgwFindRegion = new System.ComponentModel.BackgroundWorker();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picControlBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMaxResultsPerRegion)).BeginInit();
            this.gbChooseFileOpen.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBigAudio)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer
            // 
            resources.ApplyResources(this.splitContainer, "splitContainer");
            this.splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.btnStopSelec);
            this.splitContainer.Panel1.Controls.Add(this.btnPlaySelec);
            this.splitContainer.Panel1.Controls.Add(this.txtVarPenalty);
            this.splitContainer.Panel1.Controls.Add(this.txtNFreqs);
            this.splitContainer.Panel1.Controls.Add(this.txtChangePenalty);
            this.splitContainer.Panel1.Controls.Add(this.txtDPenalty);
            this.splitContainer.Panel1.Controls.Add(this.lblSpecInfo);
            this.splitContainer.Panel1.Controls.Add(this.picControlBar);
            this.splitContainer.Panel1.Controls.Add(this.GLPicIntens);
            this.splitContainer.Panel1.Controls.Add(this.glPicSpectre);
            this.splitContainer.Panel1.Controls.Add(this.label6);
            this.splitContainer.Panel1.Controls.Add(this.pictureBox2);
            this.splitContainer.Panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.splitContainer_Panel1_Paint);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.lstSearchRes);
            this.splitContainer.Panel2.Controls.Add(this.btnPlayRegion);
            this.splitContainer.Panel2.Controls.Add(this.lblProgress);
            this.splitContainer.Panel2.Controls.Add(this.numMaxResultsPerRegion);
            this.splitContainer.Panel2.Controls.Add(this.label2);
            this.splitContainer.Panel2.Controls.Add(this.lblFindInfo);
            this.splitContainer.Panel2.Controls.Add(this.txtDescrip);
            this.splitContainer.Panel2.Controls.Add(this.label1);
            this.splitContainer.Panel2.Controls.Add(this.chkListSearch);
            this.splitContainer.Panel2.Controls.Add(this.btnFindInterestRegions);
            this.splitContainer.Panel2.Controls.Add(this.btnAddSel);
            this.splitContainer.Panel2.Paint += new System.Windows.Forms.PaintEventHandler(this.splitContainer_Panel2_Paint);
            // 
            // btnStopSelec
            // 
            this.btnStopSelec.Cursor = System.Windows.Forms.Cursors.Hand;
            resources.ApplyResources(this.btnStopSelec, "btnStopSelec");
            this.btnStopSelec.Name = "btnStopSelec";
            this.btnStopSelec.UseVisualStyleBackColor = true;
            this.btnStopSelec.Click += new System.EventHandler(this.btnStopSelec_Click);
            // 
            // btnPlaySelec
            // 
            this.btnPlaySelec.Cursor = System.Windows.Forms.Cursors.Hand;
            resources.ApplyResources(this.btnPlaySelec, "btnPlaySelec");
            this.btnPlaySelec.Name = "btnPlaySelec";
            this.btnPlaySelec.UseVisualStyleBackColor = true;
            this.btnPlaySelec.Click += new System.EventHandler(this.btnPlaySelec_Click);
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
            // lstSearchRes
            // 
            resources.ApplyResources(this.lstSearchRes, "lstSearchRes");
            this.lstSearchRes.FormattingEnabled = true;
            this.lstSearchRes.Name = "lstSearchRes";
            this.lstSearchRes.SelectedIndexChanged += new System.EventHandler(this.lstSearchRes_SelectedIndexChanged);
            // 
            // btnPlayRegion
            // 
            this.btnPlayRegion.Cursor = System.Windows.Forms.Cursors.Hand;
            resources.ApplyResources(this.btnPlayRegion, "btnPlayRegion");
            this.btnPlayRegion.Name = "btnPlayRegion";
            this.btnPlayRegion.UseVisualStyleBackColor = true;
            this.btnPlayRegion.Click += new System.EventHandler(this.btnPlayRegion_Click);
            // 
            // lblProgress
            // 
            resources.ApplyResources(this.lblProgress, "lblProgress");
            this.lblProgress.BackColor = System.Drawing.Color.Transparent;
            this.lblProgress.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblProgress.Name = "lblProgress";
            // 
            // numMaxResultsPerRegion
            // 
            resources.ApplyResources(this.numMaxResultsPerRegion, "numMaxResultsPerRegion");
            this.numMaxResultsPerRegion.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
            this.numMaxResultsPerRegion.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numMaxResultsPerRegion.Name = "numMaxResultsPerRegion";
            this.numMaxResultsPerRegion.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.numMaxResultsPerRegion.ValueChanged += new System.EventHandler(this.numMaxResultsPerRegion_ValueChanged);
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Name = "label2";
            // 
            // lblFindInfo
            // 
            resources.ApplyResources(this.lblFindInfo, "lblFindInfo");
            this.lblFindInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblFindInfo.Name = "lblFindInfo";
            // 
            // txtDescrip
            // 
            resources.ApplyResources(this.txtDescrip, "txtDescrip");
            this.txtDescrip.Name = "txtDescrip";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Name = "label1";
            // 
            // chkListSearch
            // 
            resources.ApplyResources(this.chkListSearch, "chkListSearch");
            this.chkListSearch.CheckOnClick = true;
            this.chkListSearch.FormattingEnabled = true;
            this.chkListSearch.Name = "chkListSearch";
            this.chkListSearch.SelectedIndexChanged += new System.EventHandler(this.chkListSearch_SelectedIndexChanged);
            // 
            // btnFindInterestRegions
            // 
            this.btnFindInterestRegions.BackColor = System.Drawing.Color.Transparent;
            this.btnFindInterestRegions.Cursor = System.Windows.Forms.Cursors.Hand;
            resources.ApplyResources(this.btnFindInterestRegions, "btnFindInterestRegions");
            this.btnFindInterestRegions.Name = "btnFindInterestRegions";
            this.btnFindInterestRegions.UseVisualStyleBackColor = false;
            this.btnFindInterestRegions.Click += new System.EventHandler(this.btnFindInterestRegions_Click);
            // 
            // btnAddSel
            // 
            this.btnAddSel.BackColor = System.Drawing.Color.Transparent;
            this.btnAddSel.Cursor = System.Windows.Forms.Cursors.Hand;
            resources.ApplyResources(this.btnAddSel, "btnAddSel");
            this.btnAddSel.Name = "btnAddSel";
            this.btnAddSel.UseVisualStyleBackColor = false;
            this.btnAddSel.Click += new System.EventHandler(this.btnAddSel_Click);
            // 
            // gbChooseFileOpen
            // 
            resources.ApplyResources(this.gbChooseFileOpen, "gbChooseFileOpen");
            this.gbChooseFileOpen.Controls.Add(this.btnPlayOrigFile);
            this.gbChooseFileOpen.Controls.Add(this.btnStopOrigFile);
            this.gbChooseFileOpen.Controls.Add(this.btnOpenFileComment);
            this.gbChooseFileOpen.Controls.Add(this.picBigAudio);
            this.gbChooseFileOpen.Name = "gbChooseFileOpen";
            this.gbChooseFileOpen.TabStop = false;
            this.gbChooseFileOpen.Paint += new System.Windows.Forms.PaintEventHandler(this.gbChooseFileOpen_Paint);
            // 
            // btnPlayOrigFile
            // 
            resources.ApplyResources(this.btnPlayOrigFile, "btnPlayOrigFile");
            this.btnPlayOrigFile.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPlayOrigFile.Name = "btnPlayOrigFile";
            this.btnPlayOrigFile.UseVisualStyleBackColor = true;
            this.btnPlayOrigFile.Click += new System.EventHandler(this.btnPlayOrigFile_Click);
            // 
            // btnStopOrigFile
            // 
            resources.ApplyResources(this.btnStopOrigFile, "btnStopOrigFile");
            this.btnStopOrigFile.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnStopOrigFile.Name = "btnStopOrigFile";
            this.btnStopOrigFile.UseVisualStyleBackColor = true;
            this.btnStopOrigFile.Click += new System.EventHandler(this.btnStopOrigFile_Click);
            // 
            // btnOpenFileComment
            // 
            this.btnOpenFileComment.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnOpenFileComment.Image = global::PratiCanto.Properties.Resources.Openfolder_orange;
            resources.ApplyResources(this.btnOpenFileComment, "btnOpenFileComment");
            this.btnOpenFileComment.Name = "btnOpenFileComment";
            this.btnOpenFileComment.UseVisualStyleBackColor = true;
            this.btnOpenFileComment.Click += new System.EventHandler(this.btnOpenFileComment_Click);
            // 
            // picBigAudio
            // 
            resources.ApplyResources(this.picBigAudio, "picBigAudio");
            this.picBigAudio.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picBigAudio.Cursor = System.Windows.Forms.Cursors.Cross;
            this.picBigAudio.Name = "picBigAudio";
            this.picBigAudio.TabStop = false;
            this.picBigAudio.Paint += new System.Windows.Forms.PaintEventHandler(this.picBigAudio_Paint);
            this.picBigAudio.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picComposition_MouseDown);
            this.picBigAudio.MouseMove += new System.Windows.Forms.MouseEventHandler(this.picComposition_MouseMove);
            this.picBigAudio.MouseUp += new System.Windows.Forms.MouseEventHandler(this.picComposition_MouseUp);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.txtAnnot});
            resources.ApplyResources(this.toolStrip1, "toolStrip1");
            this.toolStrip1.Name = "toolStrip1";
            // 
            // txtAnnot
            // 
            this.txtAnnot.Name = "txtAnnot";
            resources.ApplyResources(this.txtAnnot, "txtAnnot");
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // bgComputeBigAudioSpec
            // 
            this.bgComputeBigAudioSpec.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgComputeBigAudioSpec_DoWork);
            // 
            // bgwFindRegion
            // 
            this.bgwFindRegion.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgwFindRegion_DoWork);
            // 
            // frmInspector
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.splitContainer);
            this.Controls.Add(this.gbChooseFileOpen);
            this.Name = "frmInspector";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmInspector_FormClosing);
            this.Load += new System.EventHandler(this.frmInspector_Load);
            this.Resize += new System.EventHandler(this.frmInspector_Resize);
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel1.PerformLayout();
            this.splitContainer.Panel2.ResumeLayout(false);
            this.splitContainer.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picControlBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMaxResultsPerRegion)).EndInit();
            this.gbChooseFileOpen.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picBigAudio)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox gbChooseFileOpen;
        private System.Windows.Forms.Button btnPlayOrigFile;
        private System.Windows.Forms.Button btnStopOrigFile;
        private System.Windows.Forms.Button btnOpenFileComment;
        private System.Windows.Forms.PictureBox picBigAudio;
        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.TextBox txtVarPenalty;
        private System.Windows.Forms.TextBox txtNFreqs;
        private System.Windows.Forms.TextBox txtChangePenalty;
        private System.Windows.Forms.TextBox txtDPenalty;
        private System.Windows.Forms.Label lblSpecInfo;
        private System.Windows.Forms.PictureBox picControlBar;
        private OpenTK.GLControl GLPicIntens;
        private OpenTK.GLControl glPicSpectre;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckedListBox chkListSearch;
        private System.Windows.Forms.Button btnFindInterestRegions;
        private System.Windows.Forms.Button btnAddSel;
        private System.Windows.Forms.TextBox txtDescrip;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripTextBox txtAnnot;
        private System.Windows.Forms.Button btnStopSelec;
        private System.Windows.Forms.Button btnPlaySelec;
        private System.Windows.Forms.Label lblFindInfo;
        private System.Windows.Forms.NumericUpDown numMaxResultsPerRegion;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblProgress;
        private System.Windows.Forms.Timer timer1;
        private System.ComponentModel.BackgroundWorker bgComputeBigAudioSpec;
        private System.Windows.Forms.Button btnPlayRegion;
        private System.Windows.Forms.ListBox lstSearchRes;
        private System.ComponentModel.BackgroundWorker bgwFindRegion;
    }
}