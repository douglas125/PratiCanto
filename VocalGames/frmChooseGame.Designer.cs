namespace VocalGames
{
    partial class frmChooseGame
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmChooseGame));
            this.btnFreqBird = new System.Windows.Forms.Button();
            this.lblFreqBirdDesc = new System.Windows.Forms.Label();
            this.cmbInputDevice = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnFreqBird
            // 
            this.btnFreqBird.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFreqBird.Location = new System.Drawing.Point(12, 59);
            this.btnFreqBird.Name = "btnFreqBird";
            this.btnFreqBird.Size = new System.Drawing.Size(198, 91);
            this.btnFreqBird.TabIndex = 0;
            this.btnFreqBird.Text = "Frequency bird";
            this.btnFreqBird.UseVisualStyleBackColor = true;
            this.btnFreqBird.Click += new System.EventHandler(this.btnFreqBird_Click);
            // 
            // lblFreqBirdDesc
            // 
            this.lblFreqBirdDesc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblFreqBirdDesc.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFreqBirdDesc.Location = new System.Drawing.Point(229, 60);
            this.lblFreqBirdDesc.Name = "lblFreqBirdDesc";
            this.lblFreqBirdDesc.Size = new System.Drawing.Size(317, 90);
            this.lblFreqBirdDesc.TabIndex = 1;
            this.lblFreqBirdDesc.Text = "Control the flight of the bird by raising or lowering the pitch of your voice. Sc" +
                "ore points eating corn. Try not to die by avoiding the poison.";
            // 
            // cmbInputDevice
            // 
            this.cmbInputDevice.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbInputDevice.FormattingEnabled = true;
            this.cmbInputDevice.Location = new System.Drawing.Point(229, 21);
            this.cmbInputDevice.Name = "cmbInputDevice";
            this.cmbInputDevice.Size = new System.Drawing.Size(317, 21);
            this.cmbInputDevice.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(58, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(152, 21);
            this.label1.TabIndex = 3;
            this.label1.Text = "Microphone:";
            // 
            // frmChooseGame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(587, 261);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmbInputDevice);
            this.Controls.Add(this.lblFreqBirdDesc);
            this.Controls.Add(this.btnFreqBird);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmChooseGame";
            this.Text = "Vocal Games";
            this.Load += new System.EventHandler(this.frmChooseGame_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnFreqBird;
        private System.Windows.Forms.Label lblFreqBirdDesc;
        private System.Windows.Forms.ComboBox cmbInputDevice;
        private System.Windows.Forms.Label label1;
    }
}

