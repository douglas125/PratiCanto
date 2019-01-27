namespace PratiCanto
{
    partial class frmVoiceCommand
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmVoiceCommand));
            this.btnStartTest = new System.Windows.Forms.Button();
            this.gbVoiceCmd = new System.Windows.Forms.GroupBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.btnOpen = new System.Windows.Forms.Button();
            this.lstCmds = new System.Windows.Forms.ListBox();
            this.cmbInputDevice = new System.Windows.Forms.ComboBox();
            this.gbActions = new System.Windows.Forms.GroupBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.radioButton4 = new System.Windows.Forms.RadioButton();
            this.radioButton3 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.comboBox3 = new System.Windows.Forms.ComboBox();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.cmbWakeUpCmd = new System.Windows.Forms.ComboBox();
            this.chkWakeUp = new System.Windows.Forms.CheckBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.gbMonitor = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.numMinIntens = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.chkUseVoiceInput = new System.Windows.Forms.CheckBox();
            this.gbVoiceCmd.SuspendLayout();
            this.gbActions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.gbMonitor.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numMinIntens)).BeginInit();
            this.SuspendLayout();
            // 
            // btnStartTest
            // 
            this.btnStartTest.BackColor = System.Drawing.Color.Transparent;
            this.btnStartTest.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnStartTest.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.btnStartTest.ForeColor = System.Drawing.Color.White;
            this.btnStartTest.Image = global::PratiCanto.Properties.Resources.startTest;
            this.btnStartTest.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnStartTest.Location = new System.Drawing.Point(10, 19);
            this.btnStartTest.Name = "btnStartTest";
            this.btnStartTest.Size = new System.Drawing.Size(237, 80);
            this.btnStartTest.TabIndex = 2;
            this.btnStartTest.Text = "         START / STOP \r\n        NEW COMMAND\r\n ";
            this.btnStartTest.UseVisualStyleBackColor = false;
            // 
            // gbVoiceCmd
            // 
            this.gbVoiceCmd.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbVoiceCmd.Controls.Add(this.btnSave);
            this.gbVoiceCmd.Controls.Add(this.textBox1);
            this.gbVoiceCmd.Controls.Add(this.label8);
            this.gbVoiceCmd.Controls.Add(this.btnOpen);
            this.gbVoiceCmd.Controls.Add(this.lstCmds);
            this.gbVoiceCmd.Controls.Add(this.cmbInputDevice);
            this.gbVoiceCmd.Controls.Add(this.btnStartTest);
            this.gbVoiceCmd.Location = new System.Drawing.Point(3, 188);
            this.gbVoiceCmd.Name = "gbVoiceCmd";
            this.gbVoiceCmd.Size = new System.Drawing.Size(314, 345);
            this.gbVoiceCmd.TabIndex = 3;
            this.gbVoiceCmd.TabStop = false;
            this.gbVoiceCmd.Text = "Voice Commands";
            this.gbVoiceCmd.Paint += new System.Windows.Forms.PaintEventHandler(this.gbVoiceCmd_Paint);
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.Transparent;
            this.btnSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSave.Image = global::PratiCanto.Properties.Resources.save;
            this.btnSave.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnSave.Location = new System.Drawing.Point(253, 87);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(55, 55);
            this.btnSave.TabIndex = 34;
            this.btnSave.UseVisualStyleBackColor = false;
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.Location = new System.Drawing.Point(65, 151);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(243, 29);
            this.textBox1.TabIndex = 33;
            this.textBox1.Text = "Command1";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(7, 158);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(52, 18);
            this.label8.TabIndex = 32;
            this.label8.Text = "Name:";
            // 
            // btnOpen
            // 
            this.btnOpen.BackColor = System.Drawing.Color.Transparent;
            this.btnOpen.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnOpen.Image = global::PratiCanto.Properties.Resources.open;
            this.btnOpen.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnOpen.Location = new System.Drawing.Point(253, 19);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(55, 55);
            this.btnOpen.TabIndex = 31;
            this.btnOpen.UseVisualStyleBackColor = false;
            // 
            // lstCmds
            // 
            this.lstCmds.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstCmds.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstCmds.FormattingEnabled = true;
            this.lstCmds.ItemHeight = 18;
            this.lstCmds.Location = new System.Drawing.Point(10, 184);
            this.lstCmds.Name = "lstCmds";
            this.lstCmds.Size = new System.Drawing.Size(298, 148);
            this.lstCmds.TabIndex = 30;
            // 
            // cmbInputDevice
            // 
            this.cmbInputDevice.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbInputDevice.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F);
            this.cmbInputDevice.FormattingEnabled = true;
            this.cmbInputDevice.Location = new System.Drawing.Point(10, 105);
            this.cmbInputDevice.Name = "cmbInputDevice";
            this.cmbInputDevice.Size = new System.Drawing.Size(237, 37);
            this.cmbInputDevice.TabIndex = 29;
            // 
            // gbActions
            // 
            this.gbActions.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbActions.Controls.Add(this.pictureBox1);
            this.gbActions.Controls.Add(this.numericUpDown1);
            this.gbActions.Controls.Add(this.radioButton4);
            this.gbActions.Controls.Add(this.radioButton3);
            this.gbActions.Controls.Add(this.radioButton2);
            this.gbActions.Controls.Add(this.radioButton1);
            this.gbActions.Controls.Add(this.checkBox1);
            this.gbActions.Controls.Add(this.comboBox3);
            this.gbActions.Controls.Add(this.comboBox2);
            this.gbActions.Controls.Add(this.comboBox1);
            this.gbActions.Controls.Add(this.label12);
            this.gbActions.Controls.Add(this.label11);
            this.gbActions.Controls.Add(this.label10);
            this.gbActions.Controls.Add(this.label9);
            this.gbActions.Controls.Add(this.cmbWakeUpCmd);
            this.gbActions.Controls.Add(this.chkWakeUp);
            this.gbActions.Location = new System.Drawing.Point(3, 6);
            this.gbActions.Name = "gbActions";
            this.gbActions.Size = new System.Drawing.Size(435, 527);
            this.gbActions.TabIndex = 4;
            this.gbActions.TabStop = false;
            this.gbActions.Text = "Actions";
            this.gbActions.Paint += new System.Windows.Forms.PaintEventHandler(this.gbActions_Paint);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Location = new System.Drawing.Point(169, 317);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(186, 114);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 35;
            this.pictureBox1.TabStop = false;
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.numericUpDown1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F);
            this.numericUpDown1.Location = new System.Drawing.Point(308, 483);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericUpDown1.Minimum = new decimal(new int[] {
            20,
            0,
            0,
            -2147483648});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(69, 31);
            this.numericUpDown1.TabIndex = 34;
            this.numericUpDown1.Value = new decimal(new int[] {
            4,
            0,
            0,
            0});
            // 
            // radioButton4
            // 
            this.radioButton4.AutoSize = true;
            this.radioButton4.BackColor = System.Drawing.Color.Transparent;
            this.radioButton4.Checked = true;
            this.radioButton4.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButton4.Location = new System.Drawing.Point(30, 201);
            this.radioButton4.Name = "radioButton4";
            this.radioButton4.Size = new System.Drawing.Size(98, 22);
            this.radioButton4.TabIndex = 33;
            this.radioButton4.TabStop = true;
            this.radioButton4.Text = "Do nothing";
            this.radioButton4.UseVisualStyleBackColor = false;
            // 
            // radioButton3
            // 
            this.radioButton3.AutoSize = true;
            this.radioButton3.BackColor = System.Drawing.Color.Transparent;
            this.radioButton3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButton3.Location = new System.Drawing.Point(30, 328);
            this.radioButton3.Name = "radioButton3";
            this.radioButton3.Size = new System.Drawing.Size(85, 22);
            this.radioButton3.TabIndex = 33;
            this.radioButton3.Text = "Left-click";
            this.radioButton3.UseVisualStyleBackColor = false;
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.BackColor = System.Drawing.Color.Transparent;
            this.radioButton2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButton2.Location = new System.Drawing.Point(30, 283);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(113, 22);
            this.radioButton2.TabIndex = 33;
            this.radioButton2.Text = "Move mouse";
            this.radioButton2.UseVisualStyleBackColor = false;
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.BackColor = System.Drawing.Color.Transparent;
            this.radioButton1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButton1.Location = new System.Drawing.Point(30, 242);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(92, 22);
            this.radioButton1.TabIndex = 33;
            this.radioButton1.Text = "Press key";
            this.radioButton1.UseVisualStyleBackColor = false;
            // 
            // checkBox1
            // 
            this.checkBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkBox1.AutoSize = true;
            this.checkBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBox1.Location = new System.Drawing.Point(27, 488);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(254, 24);
            this.checkBox1.TabIndex = 32;
            this.checkBox1.Text = "Auto-compute required similarity";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // comboBox3
            // 
            this.comboBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F);
            this.comboBox3.FormattingEnabled = true;
            this.comboBox3.Location = new System.Drawing.Point(169, 274);
            this.comboBox3.Name = "comboBox3";
            this.comboBox3.Size = new System.Drawing.Size(260, 37);
            this.comboBox3.TabIndex = 31;
            // 
            // comboBox2
            // 
            this.comboBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F);
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Location = new System.Drawing.Point(169, 233);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(260, 37);
            this.comboBox2.TabIndex = 31;
            // 
            // comboBox1
            // 
            this.comboBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F);
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(216, 82);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(213, 37);
            this.comboBox1.TabIndex = 31;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.BackColor = System.Drawing.Color.Transparent;
            this.label12.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(216, 131);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(60, 22);
            this.label12.TabIndex = 30;
            this.label12.Text = "Trigger";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.BackColor = System.Drawing.Color.Transparent;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(12, 131);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(187, 20);
            this.label11.TabIndex = 30;
            this.label11.Text = "Identified command type:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.Color.Transparent;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(12, 169);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(241, 20);
            this.label10.TabIndex = 30;
            this.label10.Text = "When this command is identified:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(12, 93);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(156, 20);
            this.label9.TabIndex = 30;
            this.label9.Text = "Configure command:";
            // 
            // cmbWakeUpCmd
            // 
            this.cmbWakeUpCmd.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbWakeUpCmd.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbWakeUpCmd.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F);
            this.cmbWakeUpCmd.FormattingEnabled = true;
            this.cmbWakeUpCmd.Location = new System.Drawing.Point(216, 19);
            this.cmbWakeUpCmd.Name = "cmbWakeUpCmd";
            this.cmbWakeUpCmd.Size = new System.Drawing.Size(213, 37);
            this.cmbWakeUpCmd.TabIndex = 29;
            // 
            // chkWakeUp
            // 
            this.chkWakeUp.AutoSize = true;
            this.chkWakeUp.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkWakeUp.Location = new System.Drawing.Point(15, 28);
            this.chkWakeUp.Name = "chkWakeUp";
            this.chkWakeUp.Size = new System.Drawing.Size(195, 24);
            this.chkWakeUp.TabIndex = 5;
            this.chkWakeUp.Text = "Use wake-up command";
            this.chkWakeUp.UseVisualStyleBackColor = true;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(12, 12);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.gbMonitor);
            this.splitContainer1.Panel1.Controls.Add(this.gbVoiceCmd);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.gbActions);
            this.splitContainer1.Size = new System.Drawing.Size(765, 536);
            this.splitContainer1.SplitterDistance = 320;
            this.splitContainer1.TabIndex = 5;
            // 
            // gbMonitor
            // 
            this.gbMonitor.Controls.Add(this.label7);
            this.gbMonitor.Controls.Add(this.label6);
            this.gbMonitor.Controls.Add(this.label5);
            this.gbMonitor.Controls.Add(this.label4);
            this.gbMonitor.Controls.Add(this.numMinIntens);
            this.gbMonitor.Controls.Add(this.label2);
            this.gbMonitor.Controls.Add(this.label3);
            this.gbMonitor.Controls.Add(this.label1);
            this.gbMonitor.Controls.Add(this.chkUseVoiceInput);
            this.gbMonitor.Location = new System.Drawing.Point(4, 6);
            this.gbMonitor.Name = "gbMonitor";
            this.gbMonitor.Size = new System.Drawing.Size(310, 176);
            this.gbMonitor.TabIndex = 4;
            this.gbMonitor.TabStop = false;
            this.gbMonitor.Text = "Command Panel";
            this.gbMonitor.Paint += new System.Windows.Forms.PaintEventHandler(this.gbMonitor_Paint);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(91, 46);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(19, 20);
            this.label7.TabIndex = 10;
            this.label7.Text = "?";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(3, 46);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(82, 18);
            this.label6.TabIndex = 10;
            this.label6.Text = "Command:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(194, 145);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(18, 20);
            this.label5.TabIndex = 9;
            this.label5.Text = "0";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(194, 120);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(18, 20);
            this.label4.TabIndex = 9;
            this.label4.Text = "0";
            // 
            // numMinIntens
            // 
            this.numMinIntens.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F);
            this.numMinIntens.Location = new System.Drawing.Point(194, 86);
            this.numMinIntens.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numMinIntens.Minimum = new decimal(new int[] {
            20,
            0,
            0,
            -2147483648});
            this.numMinIntens.Name = "numMinIntens";
            this.numMinIntens.Size = new System.Drawing.Size(69, 31);
            this.numMinIntens.TabIndex = 8;
            this.numMinIntens.Value = new decimal(new int[] {
            4,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(6, 147);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(155, 18);
            this.label2.TabIndex = 7;
            this.label2.Text = "Current intensity (dBr):";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(6, 93);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(167, 18);
            this.label3.TabIndex = 7;
            this.label3.Text = "Intensity threshold (dBr):";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(6, 120);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(167, 18);
            this.label1.TabIndex = 7;
            this.label1.Text = "Intensity threshold (dBr):";
            // 
            // chkUseVoiceInput
            // 
            this.chkUseVoiceInput.AutoSize = true;
            this.chkUseVoiceInput.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkUseVoiceInput.Location = new System.Drawing.Point(6, 19);
            this.chkUseVoiceInput.Name = "chkUseVoiceInput";
            this.chkUseVoiceInput.Size = new System.Drawing.Size(206, 24);
            this.chkUseVoiceInput.TabIndex = 6;
            this.chkUseVoiceInput.Text = "Use voice to control input";
            this.chkUseVoiceInput.UseVisualStyleBackColor = true;
            // 
            // frmVoiceCommand
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(789, 560);
            this.Controls.Add(this.splitContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmVoiceCommand";
            this.Text = "Voice Command";
            this.Load += new System.EventHandler(this.frmVoiceCommand_Load);
            this.gbVoiceCmd.ResumeLayout(false);
            this.gbVoiceCmd.PerformLayout();
            this.gbActions.ResumeLayout(false);
            this.gbActions.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.gbMonitor.ResumeLayout(false);
            this.gbMonitor.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numMinIntens)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnStartTest;
        private System.Windows.Forms.GroupBox gbVoiceCmd;
        private System.Windows.Forms.GroupBox gbActions;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ComboBox cmbInputDevice;
        private System.Windows.Forms.ComboBox cmbWakeUpCmd;
        private System.Windows.Forms.CheckBox chkWakeUp;
        private System.Windows.Forms.GroupBox gbMonitor;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chkUseVoiceInput;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown numMinIntens;
        private System.Windows.Forms.ListBox lstCmds;
        private System.Windows.Forms.Button btnOpen;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.RadioButton radioButton4;
        private System.Windows.Forms.RadioButton radioButton3;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.ComboBox comboBox3;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}