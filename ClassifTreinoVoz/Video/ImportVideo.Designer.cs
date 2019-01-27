namespace PratiCanto.Video
{
    partial class ImportVideo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ImportVideo));
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblOpenVideo = new System.Windows.Forms.Label();
            this.lblExtractAudio = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 109);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(315, 52);
            this.label1.TabIndex = 0;
            this.label1.Text = "PratiCanto uses FFmpeg under the LGPLv2.1 https://ffmpeg.org/\r\n\r\nIf video does no" +
                "t load, try installing a Codec Pack, such as\r\nk-lite_codec_pack\r\n";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox1.Controls.Add(this.lblOpenVideo);
            this.groupBox1.Controls.Add(this.lblExtractAudio);
            this.groupBox1.Location = new System.Drawing.Point(15, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(339, 84);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Please wait for the following steps";
            // 
            // lblOpenVideo
            // 
            this.lblOpenVideo.AutoSize = true;
            this.lblOpenVideo.Location = new System.Drawing.Point(16, 60);
            this.lblOpenVideo.Name = "lblOpenVideo";
            this.lblOpenVideo.Size = new System.Drawing.Size(76, 13);
            this.lblOpenVideo.TabIndex = 0;
            this.lblOpenVideo.Text = "Opening video";
            // 
            // lblExtractAudio
            // 
            this.lblExtractAudio.AutoSize = true;
            this.lblExtractAudio.Location = new System.Drawing.Point(16, 32);
            this.lblExtractAudio.Name = "lblExtractAudio";
            this.lblExtractAudio.Size = new System.Drawing.Size(135, 13);
            this.lblExtractAudio.TabIndex = 0;
            this.lblExtractAudio.Text = "Extracting audio from video";
            // 
            // ImportVideo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(366, 177);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ImportVideo";
            this.Text = "Import Video";
            this.Load += new System.EventHandler(this.ImportVideo_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        public System.Windows.Forms.Label lblOpenVideo;
        public System.Windows.Forms.Label lblExtractAudio;
    }
}