namespace VocalGames
{
    partial class frmFreqBird
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
            this.gl2D = new OpenTK.GLControl();
            this.bgWAnim = new System.ComponentModel.BackgroundWorker();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblPts = new System.Windows.Forms.Label();
            this.lblLifes = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // gl2D
            // 
            this.gl2D.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gl2D.BackColor = System.Drawing.Color.Black;
            this.gl2D.Location = new System.Drawing.Point(0, 51);
            this.gl2D.Name = "gl2D";
            this.gl2D.Size = new System.Drawing.Size(528, 227);
            this.gl2D.TabIndex = 0;
            this.gl2D.VSync = false;
            // 
            // bgWAnim
            // 
            this.bgWAnim.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgWAnim_DoWork);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(2, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(124, 39);
            this.label1.TabIndex = 1;
            this.label1.Text = "Points:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(299, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(101, 39);
            this.label2.TabIndex = 1;
            this.label2.Text = "Lifes:";
            // 
            // lblPts
            // 
            this.lblPts.AutoSize = true;
            this.lblPts.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPts.Location = new System.Drawing.Point(123, 9);
            this.lblPts.Name = "lblPts";
            this.lblPts.Size = new System.Drawing.Size(36, 39);
            this.lblPts.TabIndex = 1;
            this.lblPts.Text = "0";
            // 
            // lblLifes
            // 
            this.lblLifes.AutoSize = true;
            this.lblLifes.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLifes.Location = new System.Drawing.Point(406, 9);
            this.lblLifes.Name = "lblLifes";
            this.lblLifes.Size = new System.Drawing.Size(36, 39);
            this.lblLifes.TabIndex = 1;
            this.lblLifes.Text = "5";
            // 
            // frmFreqBird
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(527, 276);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblLifes);
            this.Controls.Add(this.lblPts);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.gl2D);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "frmFreqBird";
            this.Text = "frmPitchGame";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmFreqBird_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private OpenTK.GLControl gl2D;
        private System.ComponentModel.BackgroundWorker bgWAnim;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblPts;
        private System.Windows.Forms.Label lblLifes;
    }
}

