namespace AudioComparer
{
    partial class frmAnalyzePart
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAnalyzePart));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnDelAnnot = new System.Windows.Forms.Button();
            this.cmbAnnot = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tabAnalyze = new System.Windows.Forms.TabControl();
            this.tabRegionInfo = new System.Windows.Forms.TabPage();
            this.numEvolPts = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.gbStar = new System.Windows.Forms.GroupBox();
            this.numAmplif = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.picVoiceQualityEvol = new System.Windows.Forms.PictureBox();
            this.gbDetails = new System.Windows.Forms.GroupBox();
            this.txtRegInfoDetails = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.lstNumInfo = new System.Windows.Forms.ListBox();
            this.cmbNumList = new System.Windows.Forms.ComboBox();
            this.btnComputeRegionInfo = new System.Windows.Forms.Button();
            this.tabFreqFilter = new System.Windows.Forms.TabPage();
            this.btnSaveSeq = new System.Windows.Forms.Button();
            this.btnPlaySeq = new System.Windows.Forms.Button();
            this.numMaxFreq = new System.Windows.Forms.NumericUpDown();
            this.numMinFreq = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnComputeFiltered = new System.Windows.Forms.Button();
            this.btnClearFreqs = new System.Windows.Forms.Button();
            this.btnSelectFreqs = new System.Windows.Forms.Button();
            this.picSpecFilter = new System.Windows.Forms.PictureBox();
            this.tabGroup = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lstDurations = new System.Windows.Forms.ListBox();
            this.numTries = new System.Windows.Forms.NumericUpDown();
            this.lblComputeTime = new System.Windows.Forms.Label();
            this.lblMeanSqError = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.numQtdClusters = new System.Windows.Forms.NumericUpDown();
            this.label10 = new System.Windows.Forms.Label();
            this.gRegions = new System.Windows.Forms.GroupBox();
            this.lstSimRegions = new System.Windows.Forms.ListBox();
            this.btnGroupSimilarRegions = new System.Windows.Forms.Button();
            this.numSmallestRegionTime = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.tabSelfSim = new System.Windows.Forms.TabPage();
            this.gbSimAn = new System.Windows.Forms.GroupBox();
            this.picSelfSim = new System.Windows.Forms.PictureBox();
            this.btnSpeechSpeed = new System.Windows.Forms.Button();
            this.btnSelfSim = new System.Windows.Forms.Button();
            this.tabPhoneticSearch = new System.Windows.Forms.TabPage();
            this.cmbAnnotPhonSearch = new System.Windows.Forms.ComboBox();
            this.lblAvgStdDevPhonSearch = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.btnIncludeAnnot = new System.Windows.Forms.Button();
            this.numSimRegThresh = new System.Windows.Forms.NumericUpDown();
            this.label12 = new System.Windows.Forms.Label();
            this.txtSimilarAnnotate = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.btnAnnotatedSearch = new System.Windows.Forms.Button();
            this.btnSearchSimilar = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.lstPhoneticSearchRegions = new System.Windows.Forms.ListBox();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.groupBox1.SuspendLayout();
            this.tabAnalyze.SuspendLayout();
            this.tabRegionInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numEvolPts)).BeginInit();
            this.gbStar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numAmplif)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picVoiceQualityEvol)).BeginInit();
            this.gbDetails.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.tabFreqFilter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numMaxFreq)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMinFreq)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picSpecFilter)).BeginInit();
            this.tabGroup.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numTries)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numQtdClusters)).BeginInit();
            this.gRegions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numSmallestRegionTime)).BeginInit();
            this.tabSelfSim.SuspendLayout();
            this.gbSimAn.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picSelfSim)).BeginInit();
            this.tabPhoneticSearch.SuspendLayout();
            this.groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numSimRegThresh)).BeginInit();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Controls.Add(this.btnDelAnnot);
            this.groupBox1.Controls.Add(this.cmbAnnot);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            this.toolTip.SetToolTip(this.groupBox1, resources.GetString("groupBox1.ToolTip"));
            this.groupBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.groupBox1_Paint);
            // 
            // btnDelAnnot
            // 
            resources.ApplyResources(this.btnDelAnnot, "btnDelAnnot");
            this.btnDelAnnot.Name = "btnDelAnnot";
            this.toolTip.SetToolTip(this.btnDelAnnot, resources.GetString("btnDelAnnot.ToolTip"));
            this.btnDelAnnot.UseVisualStyleBackColor = true;
            this.btnDelAnnot.Click += new System.EventHandler(this.btnDelAnnot_Click);
            // 
            // cmbAnnot
            // 
            resources.ApplyResources(this.cmbAnnot, "cmbAnnot");
            this.cmbAnnot.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAnnot.FormattingEnabled = true;
            this.cmbAnnot.Name = "cmbAnnot";
            this.toolTip.SetToolTip(this.cmbAnnot, resources.GetString("cmbAnnot.ToolTip"));
            this.cmbAnnot.SelectedIndexChanged += new System.EventHandler(this.cmbAnnot_SelectedIndexChanged);
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            this.toolTip.SetToolTip(this.label3, resources.GetString("label3.ToolTip"));
            // 
            // tabAnalyze
            // 
            resources.ApplyResources(this.tabAnalyze, "tabAnalyze");
            this.tabAnalyze.Controls.Add(this.tabRegionInfo);
            this.tabAnalyze.Controls.Add(this.tabFreqFilter);
            this.tabAnalyze.Controls.Add(this.tabGroup);
            this.tabAnalyze.Controls.Add(this.tabSelfSim);
            this.tabAnalyze.Controls.Add(this.tabPhoneticSearch);
            this.tabAnalyze.Name = "tabAnalyze";
            this.tabAnalyze.SelectedIndex = 0;
            this.toolTip.SetToolTip(this.tabAnalyze, resources.GetString("tabAnalyze.ToolTip"));
            // 
            // tabRegionInfo
            // 
            resources.ApplyResources(this.tabRegionInfo, "tabRegionInfo");
            this.tabRegionInfo.Controls.Add(this.numEvolPts);
            this.tabRegionInfo.Controls.Add(this.label1);
            this.tabRegionInfo.Controls.Add(this.gbStar);
            this.tabRegionInfo.Controls.Add(this.gbDetails);
            this.tabRegionInfo.Controls.Add(this.groupBox3);
            this.tabRegionInfo.Controls.Add(this.btnComputeRegionInfo);
            this.tabRegionInfo.Name = "tabRegionInfo";
            this.toolTip.SetToolTip(this.tabRegionInfo, resources.GetString("tabRegionInfo.ToolTip"));
            this.tabRegionInfo.UseVisualStyleBackColor = true;
            // 
            // numEvolPts
            // 
            resources.ApplyResources(this.numEvolPts, "numEvolPts");
            this.numEvolPts.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numEvolPts.Name = "numEvolPts";
            this.toolTip.SetToolTip(this.numEvolPts, resources.GetString("numEvolPts.ToolTip"));
            this.numEvolPts.Value = new decimal(new int[] {
            14,
            0,
            0,
            0});
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            this.toolTip.SetToolTip(this.label1, resources.GetString("label1.ToolTip"));
            // 
            // gbStar
            // 
            resources.ApplyResources(this.gbStar, "gbStar");
            this.gbStar.Controls.Add(this.numAmplif);
            this.gbStar.Controls.Add(this.label2);
            this.gbStar.Controls.Add(this.picVoiceQualityEvol);
            this.gbStar.Name = "gbStar";
            this.gbStar.TabStop = false;
            this.toolTip.SetToolTip(this.gbStar, resources.GetString("gbStar.ToolTip"));
            this.gbStar.Paint += new System.Windows.Forms.PaintEventHandler(this.groupBox1_Paint);
            // 
            // numAmplif
            // 
            resources.ApplyResources(this.numAmplif, "numAmplif");
            this.numAmplif.Minimum = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.numAmplif.Name = "numAmplif";
            this.toolTip.SetToolTip(this.numAmplif, resources.GetString("numAmplif.ToolTip"));
            this.numAmplif.Value = new decimal(new int[] {
            12,
            0,
            0,
            0});
            this.numAmplif.ValueChanged += new System.EventHandler(this.numAmplif_ValueChanged);
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            this.toolTip.SetToolTip(this.label2, resources.GetString("label2.ToolTip"));
            // 
            // picVoiceQualityEvol
            // 
            resources.ApplyResources(this.picVoiceQualityEvol, "picVoiceQualityEvol");
            this.picVoiceQualityEvol.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picVoiceQualityEvol.Name = "picVoiceQualityEvol";
            this.picVoiceQualityEvol.TabStop = false;
            this.toolTip.SetToolTip(this.picVoiceQualityEvol, resources.GetString("picVoiceQualityEvol.ToolTip"));
            this.picVoiceQualityEvol.Paint += new System.Windows.Forms.PaintEventHandler(this.picVoiceQualityEvol_Paint);
            // 
            // gbDetails
            // 
            resources.ApplyResources(this.gbDetails, "gbDetails");
            this.gbDetails.Controls.Add(this.txtRegInfoDetails);
            this.gbDetails.Name = "gbDetails";
            this.gbDetails.TabStop = false;
            this.toolTip.SetToolTip(this.gbDetails, resources.GetString("gbDetails.ToolTip"));
            this.gbDetails.Paint += new System.Windows.Forms.PaintEventHandler(this.groupBox1_Paint);
            // 
            // txtRegInfoDetails
            // 
            resources.ApplyResources(this.txtRegInfoDetails, "txtRegInfoDetails");
            this.txtRegInfoDetails.Name = "txtRegInfoDetails";
            this.txtRegInfoDetails.ReadOnly = true;
            this.toolTip.SetToolTip(this.txtRegInfoDetails, resources.GetString("txtRegInfoDetails.ToolTip"));
            // 
            // groupBox3
            // 
            resources.ApplyResources(this.groupBox3, "groupBox3");
            this.groupBox3.Controls.Add(this.lstNumInfo);
            this.groupBox3.Controls.Add(this.cmbNumList);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.TabStop = false;
            this.toolTip.SetToolTip(this.groupBox3, resources.GetString("groupBox3.ToolTip"));
            this.groupBox3.Paint += new System.Windows.Forms.PaintEventHandler(this.groupBox1_Paint);
            // 
            // lstNumInfo
            // 
            resources.ApplyResources(this.lstNumInfo, "lstNumInfo");
            this.lstNumInfo.FormattingEnabled = true;
            this.lstNumInfo.Name = "lstNumInfo";
            this.toolTip.SetToolTip(this.lstNumInfo, resources.GetString("lstNumInfo.ToolTip"));
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
            this.toolTip.SetToolTip(this.cmbNumList, resources.GetString("cmbNumList.ToolTip"));
            this.cmbNumList.SelectedIndexChanged += new System.EventHandler(this.cmbNumList_SelectedIndexChanged);
            // 
            // btnComputeRegionInfo
            // 
            resources.ApplyResources(this.btnComputeRegionInfo, "btnComputeRegionInfo");
            this.btnComputeRegionInfo.Name = "btnComputeRegionInfo";
            this.toolTip.SetToolTip(this.btnComputeRegionInfo, resources.GetString("btnComputeRegionInfo.ToolTip"));
            this.btnComputeRegionInfo.UseVisualStyleBackColor = true;
            this.btnComputeRegionInfo.Click += new System.EventHandler(this.btnComputeRegionInfo_Click);
            // 
            // tabFreqFilter
            // 
            resources.ApplyResources(this.tabFreqFilter, "tabFreqFilter");
            this.tabFreqFilter.Controls.Add(this.btnSaveSeq);
            this.tabFreqFilter.Controls.Add(this.btnPlaySeq);
            this.tabFreqFilter.Controls.Add(this.numMaxFreq);
            this.tabFreqFilter.Controls.Add(this.numMinFreq);
            this.tabFreqFilter.Controls.Add(this.label5);
            this.tabFreqFilter.Controls.Add(this.label4);
            this.tabFreqFilter.Controls.Add(this.btnComputeFiltered);
            this.tabFreqFilter.Controls.Add(this.btnClearFreqs);
            this.tabFreqFilter.Controls.Add(this.btnSelectFreqs);
            this.tabFreqFilter.Controls.Add(this.picSpecFilter);
            this.tabFreqFilter.Name = "tabFreqFilter";
            this.toolTip.SetToolTip(this.tabFreqFilter, resources.GetString("tabFreqFilter.ToolTip"));
            this.tabFreqFilter.UseVisualStyleBackColor = true;
            // 
            // btnSaveSeq
            // 
            resources.ApplyResources(this.btnSaveSeq, "btnSaveSeq");
            this.btnSaveSeq.Name = "btnSaveSeq";
            this.toolTip.SetToolTip(this.btnSaveSeq, resources.GetString("btnSaveSeq.ToolTip"));
            this.btnSaveSeq.UseVisualStyleBackColor = true;
            this.btnSaveSeq.Click += new System.EventHandler(this.btnSaveSeq_Click);
            // 
            // btnPlaySeq
            // 
            resources.ApplyResources(this.btnPlaySeq, "btnPlaySeq");
            this.btnPlaySeq.Name = "btnPlaySeq";
            this.toolTip.SetToolTip(this.btnPlaySeq, resources.GetString("btnPlaySeq.ToolTip"));
            this.btnPlaySeq.UseVisualStyleBackColor = true;
            this.btnPlaySeq.Click += new System.EventHandler(this.btnPlaySeq_Click);
            // 
            // numMaxFreq
            // 
            resources.ApplyResources(this.numMaxFreq, "numMaxFreq");
            this.numMaxFreq.DecimalPlaces = 3;
            this.numMaxFreq.Name = "numMaxFreq";
            this.toolTip.SetToolTip(this.numMaxFreq, resources.GetString("numMaxFreq.ToolTip"));
            this.numMaxFreq.ValueChanged += new System.EventHandler(this.numMaxFreq_ValueChanged);
            // 
            // numMinFreq
            // 
            resources.ApplyResources(this.numMinFreq, "numMinFreq");
            this.numMinFreq.DecimalPlaces = 3;
            this.numMinFreq.Name = "numMinFreq";
            this.toolTip.SetToolTip(this.numMinFreq, resources.GetString("numMinFreq.ToolTip"));
            this.numMinFreq.ValueChanged += new System.EventHandler(this.numMinFreq_ValueChanged);
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            this.toolTip.SetToolTip(this.label5, resources.GetString("label5.ToolTip"));
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            this.toolTip.SetToolTip(this.label4, resources.GetString("label4.ToolTip"));
            // 
            // btnComputeFiltered
            // 
            resources.ApplyResources(this.btnComputeFiltered, "btnComputeFiltered");
            this.btnComputeFiltered.Name = "btnComputeFiltered";
            this.toolTip.SetToolTip(this.btnComputeFiltered, resources.GetString("btnComputeFiltered.ToolTip"));
            this.btnComputeFiltered.UseVisualStyleBackColor = true;
            this.btnComputeFiltered.Click += new System.EventHandler(this.btnComputeFiltered_Click);
            // 
            // btnClearFreqs
            // 
            resources.ApplyResources(this.btnClearFreqs, "btnClearFreqs");
            this.btnClearFreqs.Name = "btnClearFreqs";
            this.toolTip.SetToolTip(this.btnClearFreqs, resources.GetString("btnClearFreqs.ToolTip"));
            this.btnClearFreqs.UseVisualStyleBackColor = true;
            this.btnClearFreqs.Click += new System.EventHandler(this.btnClearFreqs_Click);
            // 
            // btnSelectFreqs
            // 
            resources.ApplyResources(this.btnSelectFreqs, "btnSelectFreqs");
            this.btnSelectFreqs.Name = "btnSelectFreqs";
            this.toolTip.SetToolTip(this.btnSelectFreqs, resources.GetString("btnSelectFreqs.ToolTip"));
            this.btnSelectFreqs.UseVisualStyleBackColor = true;
            this.btnSelectFreqs.Click += new System.EventHandler(this.btnSelectFreqs_Click);
            // 
            // picSpecFilter
            // 
            resources.ApplyResources(this.picSpecFilter, "picSpecFilter");
            this.picSpecFilter.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picSpecFilter.Cursor = System.Windows.Forms.Cursors.Cross;
            this.picSpecFilter.Name = "picSpecFilter";
            this.picSpecFilter.TabStop = false;
            this.toolTip.SetToolTip(this.picSpecFilter, resources.GetString("picSpecFilter.ToolTip"));
            this.picSpecFilter.Paint += new System.Windows.Forms.PaintEventHandler(this.picSpecFilter_Paint);
            this.picSpecFilter.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picSpecFilter_MouseDown);
            this.picSpecFilter.MouseMove += new System.Windows.Forms.MouseEventHandler(this.picSpecFilter_MouseMove);
            this.picSpecFilter.MouseUp += new System.Windows.Forms.MouseEventHandler(this.picSpecFilter_MouseUp);
            // 
            // tabGroup
            // 
            resources.ApplyResources(this.tabGroup, "tabGroup");
            this.tabGroup.Controls.Add(this.groupBox2);
            this.tabGroup.Controls.Add(this.numTries);
            this.tabGroup.Controls.Add(this.lblComputeTime);
            this.tabGroup.Controls.Add(this.lblMeanSqError);
            this.tabGroup.Controls.Add(this.label9);
            this.tabGroup.Controls.Add(this.label8);
            this.tabGroup.Controls.Add(this.label6);
            this.tabGroup.Controls.Add(this.numQtdClusters);
            this.tabGroup.Controls.Add(this.label10);
            this.tabGroup.Controls.Add(this.gRegions);
            this.tabGroup.Controls.Add(this.btnGroupSimilarRegions);
            this.tabGroup.Controls.Add(this.numSmallestRegionTime);
            this.tabGroup.Controls.Add(this.label7);
            this.tabGroup.Name = "tabGroup";
            this.toolTip.SetToolTip(this.tabGroup, resources.GetString("tabGroup.ToolTip"));
            this.tabGroup.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            resources.ApplyResources(this.groupBox2, "groupBox2");
            this.groupBox2.Controls.Add(this.lstDurations);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.TabStop = false;
            this.toolTip.SetToolTip(this.groupBox2, resources.GetString("groupBox2.ToolTip"));
            // 
            // lstDurations
            // 
            resources.ApplyResources(this.lstDurations, "lstDurations");
            this.lstDurations.FormattingEnabled = true;
            this.lstDurations.Name = "lstDurations";
            this.toolTip.SetToolTip(this.lstDurations, resources.GetString("lstDurations.ToolTip"));
            // 
            // numTries
            // 
            resources.ApplyResources(this.numTries, "numTries");
            this.numTries.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numTries.Name = "numTries";
            this.toolTip.SetToolTip(this.numTries, resources.GetString("numTries.ToolTip"));
            this.numTries.Value = new decimal(new int[] {
            8,
            0,
            0,
            0});
            // 
            // lblComputeTime
            // 
            resources.ApplyResources(this.lblComputeTime, "lblComputeTime");
            this.lblComputeTime.Name = "lblComputeTime";
            this.toolTip.SetToolTip(this.lblComputeTime, resources.GetString("lblComputeTime.ToolTip"));
            // 
            // lblMeanSqError
            // 
            resources.ApplyResources(this.lblMeanSqError, "lblMeanSqError");
            this.lblMeanSqError.Name = "lblMeanSqError";
            this.toolTip.SetToolTip(this.lblMeanSqError, resources.GetString("lblMeanSqError.ToolTip"));
            // 
            // label9
            // 
            resources.ApplyResources(this.label9, "label9");
            this.label9.Name = "label9";
            this.toolTip.SetToolTip(this.label9, resources.GetString("label9.ToolTip"));
            // 
            // label8
            // 
            resources.ApplyResources(this.label8, "label8");
            this.label8.Name = "label8";
            this.toolTip.SetToolTip(this.label8, resources.GetString("label8.ToolTip"));
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            this.toolTip.SetToolTip(this.label6, resources.GetString("label6.ToolTip"));
            // 
            // numQtdClusters
            // 
            resources.ApplyResources(this.numQtdClusters, "numQtdClusters");
            this.numQtdClusters.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.numQtdClusters.Name = "numQtdClusters";
            this.toolTip.SetToolTip(this.numQtdClusters, resources.GetString("numQtdClusters.ToolTip"));
            this.numQtdClusters.Value = new decimal(new int[] {
            7,
            0,
            0,
            0});
            // 
            // label10
            // 
            resources.ApplyResources(this.label10, "label10");
            this.label10.Name = "label10";
            this.toolTip.SetToolTip(this.label10, resources.GetString("label10.ToolTip"));
            // 
            // gRegions
            // 
            resources.ApplyResources(this.gRegions, "gRegions");
            this.gRegions.Controls.Add(this.lstSimRegions);
            this.gRegions.Name = "gRegions";
            this.gRegions.TabStop = false;
            this.toolTip.SetToolTip(this.gRegions, resources.GetString("gRegions.ToolTip"));
            // 
            // lstSimRegions
            // 
            resources.ApplyResources(this.lstSimRegions, "lstSimRegions");
            this.lstSimRegions.FormattingEnabled = true;
            this.lstSimRegions.Name = "lstSimRegions";
            this.toolTip.SetToolTip(this.lstSimRegions, resources.GetString("lstSimRegions.ToolTip"));
            this.lstSimRegions.SelectedIndexChanged += new System.EventHandler(this.lstSimRegions_SelectedIndexChanged);
            this.lstSimRegions.DoubleClick += new System.EventHandler(this.lstSimRegions_DoubleClick);
            this.lstSimRegions.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.lstSimRegions_KeyPress);
            // 
            // btnGroupSimilarRegions
            // 
            resources.ApplyResources(this.btnGroupSimilarRegions, "btnGroupSimilarRegions");
            this.btnGroupSimilarRegions.Name = "btnGroupSimilarRegions";
            this.toolTip.SetToolTip(this.btnGroupSimilarRegions, resources.GetString("btnGroupSimilarRegions.ToolTip"));
            this.btnGroupSimilarRegions.UseVisualStyleBackColor = true;
            this.btnGroupSimilarRegions.Click += new System.EventHandler(this.btnGroupSimilarRegions_Click);
            // 
            // numSmallestRegionTime
            // 
            resources.ApplyResources(this.numSmallestRegionTime, "numSmallestRegionTime");
            this.numSmallestRegionTime.DecimalPlaces = 3;
            this.numSmallestRegionTime.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.numSmallestRegionTime.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numSmallestRegionTime.Name = "numSmallestRegionTime";
            this.toolTip.SetToolTip(this.numSmallestRegionTime, resources.GetString("numSmallestRegionTime.ToolTip"));
            this.numSmallestRegionTime.Value = new decimal(new int[] {
            6,
            0,
            0,
            131072});
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.Name = "label7";
            this.toolTip.SetToolTip(this.label7, resources.GetString("label7.ToolTip"));
            // 
            // tabSelfSim
            // 
            resources.ApplyResources(this.tabSelfSim, "tabSelfSim");
            this.tabSelfSim.Controls.Add(this.gbSimAn);
            this.tabSelfSim.Controls.Add(this.btnSpeechSpeed);
            this.tabSelfSim.Controls.Add(this.btnSelfSim);
            this.tabSelfSim.Name = "tabSelfSim";
            this.toolTip.SetToolTip(this.tabSelfSim, resources.GetString("tabSelfSim.ToolTip"));
            this.tabSelfSim.UseVisualStyleBackColor = true;
            // 
            // gbSimAn
            // 
            resources.ApplyResources(this.gbSimAn, "gbSimAn");
            this.gbSimAn.Controls.Add(this.picSelfSim);
            this.gbSimAn.Name = "gbSimAn";
            this.gbSimAn.TabStop = false;
            this.toolTip.SetToolTip(this.gbSimAn, resources.GetString("gbSimAn.ToolTip"));
            // 
            // picSelfSim
            // 
            resources.ApplyResources(this.picSelfSim, "picSelfSim");
            this.picSelfSim.Name = "picSelfSim";
            this.picSelfSim.TabStop = false;
            this.toolTip.SetToolTip(this.picSelfSim, resources.GetString("picSelfSim.ToolTip"));
            this.picSelfSim.MouseMove += new System.Windows.Forms.MouseEventHandler(this.picSelfSim_MouseMove);
            // 
            // btnSpeechSpeed
            // 
            resources.ApplyResources(this.btnSpeechSpeed, "btnSpeechSpeed");
            this.btnSpeechSpeed.Name = "btnSpeechSpeed";
            this.toolTip.SetToolTip(this.btnSpeechSpeed, resources.GetString("btnSpeechSpeed.ToolTip"));
            this.btnSpeechSpeed.UseVisualStyleBackColor = true;
            this.btnSpeechSpeed.Click += new System.EventHandler(this.btnSpeechSpeed_Click);
            // 
            // btnSelfSim
            // 
            resources.ApplyResources(this.btnSelfSim, "btnSelfSim");
            this.btnSelfSim.Name = "btnSelfSim";
            this.toolTip.SetToolTip(this.btnSelfSim, resources.GetString("btnSelfSim.ToolTip"));
            this.btnSelfSim.UseVisualStyleBackColor = true;
            this.btnSelfSim.Click += new System.EventHandler(this.btnSelfSim_Click);
            // 
            // tabPhoneticSearch
            // 
            resources.ApplyResources(this.tabPhoneticSearch, "tabPhoneticSearch");
            this.tabPhoneticSearch.Controls.Add(this.cmbAnnotPhonSearch);
            this.tabPhoneticSearch.Controls.Add(this.lblAvgStdDevPhonSearch);
            this.tabPhoneticSearch.Controls.Add(this.label14);
            this.tabPhoneticSearch.Controls.Add(this.label13);
            this.tabPhoneticSearch.Controls.Add(this.groupBox5);
            this.tabPhoneticSearch.Controls.Add(this.btnAnnotatedSearch);
            this.tabPhoneticSearch.Controls.Add(this.btnSearchSimilar);
            this.tabPhoneticSearch.Controls.Add(this.groupBox4);
            this.tabPhoneticSearch.Name = "tabPhoneticSearch";
            this.toolTip.SetToolTip(this.tabPhoneticSearch, resources.GetString("tabPhoneticSearch.ToolTip"));
            this.tabPhoneticSearch.UseVisualStyleBackColor = true;
            // 
            // cmbAnnotPhonSearch
            // 
            resources.ApplyResources(this.cmbAnnotPhonSearch, "cmbAnnotPhonSearch");
            this.cmbAnnotPhonSearch.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAnnotPhonSearch.FormattingEnabled = true;
            this.cmbAnnotPhonSearch.Name = "cmbAnnotPhonSearch";
            this.toolTip.SetToolTip(this.cmbAnnotPhonSearch, resources.GetString("cmbAnnotPhonSearch.ToolTip"));
            this.cmbAnnotPhonSearch.SelectedIndexChanged += new System.EventHandler(this.cmbAnnotPhonSearch_SelectedIndexChanged);
            // 
            // lblAvgStdDevPhonSearch
            // 
            resources.ApplyResources(this.lblAvgStdDevPhonSearch, "lblAvgStdDevPhonSearch");
            this.lblAvgStdDevPhonSearch.Name = "lblAvgStdDevPhonSearch";
            this.toolTip.SetToolTip(this.lblAvgStdDevPhonSearch, resources.GetString("lblAvgStdDevPhonSearch.ToolTip"));
            // 
            // label14
            // 
            resources.ApplyResources(this.label14, "label14");
            this.label14.Name = "label14";
            this.toolTip.SetToolTip(this.label14, resources.GetString("label14.ToolTip"));
            // 
            // label13
            // 
            resources.ApplyResources(this.label13, "label13");
            this.label13.Name = "label13";
            this.toolTip.SetToolTip(this.label13, resources.GetString("label13.ToolTip"));
            // 
            // groupBox5
            // 
            resources.ApplyResources(this.groupBox5, "groupBox5");
            this.groupBox5.Controls.Add(this.btnIncludeAnnot);
            this.groupBox5.Controls.Add(this.numSimRegThresh);
            this.groupBox5.Controls.Add(this.label12);
            this.groupBox5.Controls.Add(this.txtSimilarAnnotate);
            this.groupBox5.Controls.Add(this.label11);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.TabStop = false;
            this.toolTip.SetToolTip(this.groupBox5, resources.GetString("groupBox5.ToolTip"));
            // 
            // btnIncludeAnnot
            // 
            resources.ApplyResources(this.btnIncludeAnnot, "btnIncludeAnnot");
            this.btnIncludeAnnot.Name = "btnIncludeAnnot";
            this.toolTip.SetToolTip(this.btnIncludeAnnot, resources.GetString("btnIncludeAnnot.ToolTip"));
            this.btnIncludeAnnot.UseVisualStyleBackColor = true;
            this.btnIncludeAnnot.Click += new System.EventHandler(this.btnIncludeAnnot_Click);
            // 
            // numSimRegThresh
            // 
            resources.ApplyResources(this.numSimRegThresh, "numSimRegThresh");
            this.numSimRegThresh.DecimalPlaces = 2;
            this.numSimRegThresh.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numSimRegThresh.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.numSimRegThresh.Name = "numSimRegThresh";
            this.toolTip.SetToolTip(this.numSimRegThresh, resources.GetString("numSimRegThresh.ToolTip"));
            this.numSimRegThresh.Value = new decimal(new int[] {
            4,
            0,
            0,
            0});
            // 
            // label12
            // 
            resources.ApplyResources(this.label12, "label12");
            this.label12.Name = "label12";
            this.toolTip.SetToolTip(this.label12, resources.GetString("label12.ToolTip"));
            // 
            // txtSimilarAnnotate
            // 
            resources.ApplyResources(this.txtSimilarAnnotate, "txtSimilarAnnotate");
            this.txtSimilarAnnotate.Name = "txtSimilarAnnotate";
            this.toolTip.SetToolTip(this.txtSimilarAnnotate, resources.GetString("txtSimilarAnnotate.ToolTip"));
            // 
            // label11
            // 
            resources.ApplyResources(this.label11, "label11");
            this.label11.Name = "label11";
            this.toolTip.SetToolTip(this.label11, resources.GetString("label11.ToolTip"));
            // 
            // btnAnnotatedSearch
            // 
            resources.ApplyResources(this.btnAnnotatedSearch, "btnAnnotatedSearch");
            this.btnAnnotatedSearch.Name = "btnAnnotatedSearch";
            this.toolTip.SetToolTip(this.btnAnnotatedSearch, resources.GetString("btnAnnotatedSearch.ToolTip"));
            this.btnAnnotatedSearch.UseVisualStyleBackColor = true;
            this.btnAnnotatedSearch.Click += new System.EventHandler(this.btnAnnotatedSearch_Click);
            // 
            // btnSearchSimilar
            // 
            resources.ApplyResources(this.btnSearchSimilar, "btnSearchSimilar");
            this.btnSearchSimilar.Name = "btnSearchSimilar";
            this.toolTip.SetToolTip(this.btnSearchSimilar, resources.GetString("btnSearchSimilar.ToolTip"));
            this.btnSearchSimilar.UseVisualStyleBackColor = true;
            this.btnSearchSimilar.Click += new System.EventHandler(this.btnSearchSimilar_Click);
            // 
            // groupBox4
            // 
            resources.ApplyResources(this.groupBox4, "groupBox4");
            this.groupBox4.Controls.Add(this.lstPhoneticSearchRegions);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.TabStop = false;
            this.toolTip.SetToolTip(this.groupBox4, resources.GetString("groupBox4.ToolTip"));
            // 
            // lstPhoneticSearchRegions
            // 
            resources.ApplyResources(this.lstPhoneticSearchRegions, "lstPhoneticSearchRegions");
            this.lstPhoneticSearchRegions.FormattingEnabled = true;
            this.lstPhoneticSearchRegions.Name = "lstPhoneticSearchRegions";
            this.toolTip.SetToolTip(this.lstPhoneticSearchRegions, resources.GetString("lstPhoneticSearchRegions.ToolTip"));
            this.lstPhoneticSearchRegions.SelectedIndexChanged += new System.EventHandler(this.lstPhoneticSearchRegions_SelectedIndexChanged);
            this.lstPhoneticSearchRegions.DoubleClick += new System.EventHandler(this.lstPhoneticSearchRegions_DoubleClick);
            this.lstPhoneticSearchRegions.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.lstPhoneticSearchRegions_KeyPress);
            // 
            // frmAnalyzePart
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabAnalyze);
            this.Controls.Add(this.groupBox1);
            this.Name = "frmAnalyzePart";
            this.toolTip.SetToolTip(this, resources.GetString("$this.ToolTip"));
            this.TopMost = true;
            this.Load += new System.EventHandler(this.frmAnalyzePart_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabAnalyze.ResumeLayout(false);
            this.tabRegionInfo.ResumeLayout(false);
            this.tabRegionInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numEvolPts)).EndInit();
            this.gbStar.ResumeLayout(false);
            this.gbStar.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numAmplif)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picVoiceQualityEvol)).EndInit();
            this.gbDetails.ResumeLayout(false);
            this.gbDetails.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.tabFreqFilter.ResumeLayout(false);
            this.tabFreqFilter.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numMaxFreq)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMinFreq)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picSpecFilter)).EndInit();
            this.tabGroup.ResumeLayout(false);
            this.tabGroup.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numTries)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numQtdClusters)).EndInit();
            this.gRegions.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numSmallestRegionTime)).EndInit();
            this.tabSelfSim.ResumeLayout(false);
            this.gbSimAn.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picSelfSim)).EndInit();
            this.tabPhoneticSearch.ResumeLayout(false);
            this.tabPhoneticSearch.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numSimRegThresh)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cmbAnnot;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TabControl tabAnalyze;
        private System.Windows.Forms.TabPage tabFreqFilter;
        private System.Windows.Forms.Button btnDelAnnot;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnComputeFiltered;
        private System.Windows.Forms.Button btnClearFreqs;
        private System.Windows.Forms.Button btnSelectFreqs;
        private System.Windows.Forms.PictureBox picSpecFilter;
        private System.Windows.Forms.NumericUpDown numMaxFreq;
        private System.Windows.Forms.NumericUpDown numMinFreq;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnSaveSeq;
        private System.Windows.Forms.Button btnPlaySeq;
        private System.Windows.Forms.TabPage tabGroup;
        private System.Windows.Forms.NumericUpDown numSmallestRegionTime;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnGroupSimilarRegions;
        private System.Windows.Forms.GroupBox gRegions;
        private System.Windows.Forms.ListBox lstSimRegions;
        private System.Windows.Forms.NumericUpDown numQtdClusters;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.NumericUpDown numTries;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblMeanSqError;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lblComputeTime;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ListBox lstDurations;
        private System.Windows.Forms.TabPage tabRegionInfo;
        private System.Windows.Forms.Button btnComputeRegionInfo;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ListBox lstNumInfo;
        private System.Windows.Forms.ComboBox cmbNumList;
        private System.Windows.Forms.GroupBox gbDetails;
        private System.Windows.Forms.TextBox txtRegInfoDetails;
        private System.Windows.Forms.GroupBox gbStar;
        private System.Windows.Forms.NumericUpDown numEvolPts;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox picVoiceQualityEvol;
        private System.Windows.Forms.NumericUpDown numAmplif;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TabPage tabSelfSim;
        private System.Windows.Forms.Button btnSelfSim;
        private System.Windows.Forms.GroupBox gbSimAn;
        private System.Windows.Forms.PictureBox picSelfSim;
        private System.Windows.Forms.Button btnSpeechSpeed;
        private System.Windows.Forms.TabPage tabPhoneticSearch;
        private System.Windows.Forms.Button btnSearchSimilar;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.ListBox lstPhoneticSearchRegions;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Button btnIncludeAnnot;
        private System.Windows.Forms.NumericUpDown numSimRegThresh;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtSimilarAnnotate;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button btnAnnotatedSearch;
        private System.Windows.Forms.ComboBox cmbAnnotPhonSearch;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label lblAvgStdDevPhonSearch;
        private System.Windows.Forms.Label label14;

    }
}