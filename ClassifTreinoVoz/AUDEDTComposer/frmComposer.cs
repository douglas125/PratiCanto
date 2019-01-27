using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ClassifTreinoVoz;
using AudioComparer;
using System.IO;
using NAudio.Wave;

namespace PratiCanto
{
    public partial class frmComposer : Form
    {
        public frmComposer()
        {
            InitializeComponent();
        }
        private void btnClient_Click(object sender, EventArgs e)
        {
            PratiCantoForms.ShowClientForm();
        }

        private void frmSelfListen_Load(object sender, EventArgs e)
        {
            AudioComparer.SoftwareKey.CheckLicense("Composer", false);
            
            //adjust button cosmetics
            AdjustButtons();
            
            //List mics
            List<string> mics = SampleAudio.RealTime.GetMicrophones();
            foreach (string s in mics) cmbInputDevice.Items.Add(s);
            if (mics.Count <= 1) cmbInputDevice.Visible = false;
            if (mics.Count >= 1) cmbInputDevice.SelectedIndex = 0;
        }
        public void SetClient()
        {
            if (PratiCantoForms.frmC == null || PratiCantoForms.frmC.IsDisposed) return;
            if (PratiCantoForms.frmC.picClientPic.Image != null)
            {
                Bitmap bmp = new Bitmap(btnClient.Width, btnClient.Height);
                Graphics g = Graphics.FromImage(bmp);

                g.DrawImage(PratiCantoForms.frmC.picClientPic.Image, 0, 0, bmp.Width, bmp.Height);

                btnClient.Image = bmp;
            }
            else btnClient.Image = PratiCantoForms.DefaultClientImg;

            lblNote.Text = PratiCantoForms.frmC.txtImportantNotes.Text;
        }


        #region Record and manage recordings

        List<AudioComment> lstAudioComments = new List<AudioComment>();
        class AudioComment : IComparable<AudioComment>
        {
            public int CompareTo(AudioComment other)
            {
                return this.initTime.CompareTo(other.initTime);
            }

            public AudioComment(string Title, double t0, SampleAudio sa)
            {
                this.title = Title;
                this.initTime = t0;
                this.saComment = sa;
            }

            /// <summary>Title of comment</summary>
            public string title;
            /// <summary>Initial time</summary>
            public double initTime;
            /// <summary>Audio comment</summary>
            public SampleAudio saComment;
        }

        //SampleAudioGL saGL;
        SampleAudio.RealTime rt;
        bool recording = false;
        SampleAudio curSample;
        /// <summary>Time when record of comment started</summary>
        double startRecTime = 0;

        /// <summary>Was playing when started comment?</summary>
        bool wasPlaying = false;
        private void btnRec_Click(object sender, EventArgs e)
        {
            if (!recording)
            {
                wasPlaying = !chkVoiceOver.Checked && isPlayingFile && (!isPausedFile);

                //pause if playing file
                if (!chkVoiceOver.Checked) btnPauseOrigFile_Click(sender, e);


                startRecTime = curTime;

                rt = new SampleAudio.RealTime();
                rt.OnDataReceived = SoundReceived;
                rt.StartRecording(cmbInputDevice.SelectedIndex);

                btnRec.Image = picStop.Image;
                btnStartAddComment.ForeColor = Color.Blue;

                recording = true;
            }
            else
            {
                recording = false;
                btnStartAddComment.ForeColor = Color.White;

                //btnStopOrigFile_Click(this, e);

                rt.StopRecording();

                curSample = new SampleAudio(rt);
                btnRec.Image = picRec.Image;

                //compensate for mic delay
                lstAudioComments.Add(new AudioComment(txtDescrip.Text, startRecTime-0.22, curSample));
                

                lstAudioComments.Sort();
                lstComments.Items.Clear();
                foreach (AudioComment ac in lstAudioComments) lstComments.Items.Add(ac.title);


                txtDescrip.Text = "C" + (1 + lstAudioComments.Count).ToString();
                picComposition.Invalidate();

                ComposeCommentAudio();
                
                curTime += curSample.time[curSample.time.Length - 1];

                if (wasPlaying) btnPlayOrigFile_Click(sender, e);
            }
        }

        private void btnDeleteComment_Click(object sender, EventArgs e)
        {
            if (lstComments.SelectedIndex >= 0)
            {
                lstAudioComments.RemoveAt(lstComments.SelectedIndex);
                lstComments.Items.Clear();
                foreach (AudioComment ac in lstAudioComments)
                {
                    lstComments.Items.Add(ac.title);
                }
                ComposeCommentAudio();
            }
        }

        private void numCommentTime_ValueChanged(object sender, EventArgs e)
        {
            if (lstComments.SelectedIndex >= 0)
            {
                lstAudioComments[lstComments.SelectedIndex].initTime = (double)numCommentTime.Value;
                ComposeCommentAudio();
                picComposition.Invalidate();
            }
        }



        private void btnPlaySelCmt_Click(object sender, EventArgs e)
        {
            int idComment = lstComments.SelectedIndex;
            if (idComment >= 0)
            {
                btnPauseOrigFile_Click(sender, e);
                float tf = lstAudioComments[idComment].saComment.time[lstAudioComments[idComment].saComment.time.Length - 1];
                lstAudioComments[idComment].saComment.Play(0, tf);
            }
        }

        private void lstComments_SelectedIndexChanged(object sender, EventArgs e)
        {
            int idComment = lstComments.SelectedIndex;
            if (idComment >= 0)
            {
                txtDescrip.Text = lstAudioComments[idComment].title;
                numCommentTime.Maximum = (decimal)saComposed.time[saComposed.time.Length - 1];
                numCommentTime.Value = (decimal)lstAudioComments[idComment].initTime;
                lblDuration.Text = Math.Round(lstAudioComments[idComment].saComment.time[lstAudioComments[idComment].saComment.time.Length - 1], 2).ToString();
            }
        }

        private void SoundReceived()
        {
            //saGL.DrawRealTime(rt);
        }

        void ComposeCommentAudio()
        {
            saComposed.Dispose();

            List<double> insTimes = new List<double>();
            List<SampleAudio> audiosIns = new List<SampleAudio>();
            foreach(AudioComment ac in lstAudioComments)
            {
                insTimes.Add(ac.initTime);
                audiosIns.Add(ac.saComment);
            }

            float voiceOverIntens = 0, audiointens = 0;
            if (chkVoiceOver.Checked)
            {
                voiceOverIntens = (float)numVolComment.Value * 0.01f;
                if (voiceOverIntens == 0) voiceOverIntens = 0.001f;
                audiointens = (float)numVolAudio.Value * 0.01f;
            }
            saComposed.ComposeAudioData(saOriginal, audiosIns, insTimes, voiceOverIntens, audiointens);
        }
        #endregion


        #region Load audio file, play, pause and stop
        /// <summary>Audio file after edit</summary>
        SampleAudio saComposed;
        /// <summary>Original audio file</summary>
        SampleAudio saOriginal;
        private void btnOpenFile_Click(object sender, EventArgs e)
        {
            btnStopOrigFile_Click(sender, e);
            
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Rec|*.wav;*.mp3";

            if (Directory.Exists(PratiCantoForms.ClientFolder)) ofd.InitialDirectory = PratiCantoForms.ClientFolder;

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                saComposed = new SampleAudio(ofd.FileName);
                saOriginal = new SampleAudio(ofd.FileName);

                btnPlayOrigFile_Click(sender, e);
            }
        }


        WaveOut wo;
        SampleAudio.PlaySample ps;
        /// <summary>Playing composed file?</summary>
        bool isPlayingFile = false;
        /// <summary>Paused playing composed file?</summary>
        bool isPausedFile = false;

        private void btnPlayOrigFile_Click(object sender, EventArgs e)
        {
            if (!isPlayingFile)
            {
                ps = new SampleAudio.PlaySample(saComposed, 0, saComposed.time[saComposed.time.Length - 1]);
                ps.PlayTimeFunc = DrawTime;
                
                wo = new WaveOut();
                wo.PlaybackStopped += new EventHandler<StoppedEventArgs>(wo_PlaybackStopped);
                wo.Init(ps);

                wo.Play();
                isPlayingFile = true;
            }
            if (isPausedFile)
            {
                double localTime = curTime;
                wo.Stop();
                ps.SetTime(curTime);
                wo.Dispose();
                wo = new WaveOut();
                wo.PlaybackStopped += new EventHandler<StoppedEventArgs>(wo_PlaybackStopped);
                wo.Init(ps);

                wo.Play();
                isPlayingFile = true;
                isPausedFile = false;
            }
        }
        private void btnPauseOrigFile_Click(object sender, EventArgs e)
        {
            isPausedFile = true;
            if (wo != null) wo.Pause();
        }

        private void btnStopOrigFile_Click(object sender, EventArgs e)
        {
            if (wo!=null) wo.Stop();
        }

        void wo_PlaybackStopped(object sender, StoppedEventArgs e)
        {
            curTime = 0;
            isPlayingFile = false;
            isPausedFile = false;
            if (wo != null) wo.Dispose();
            picComposition.Invalidate();
        }

        System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
        double lastUpdt;
        double curTime;
        private void DrawTime(double time)
        {
            curTime = time;
            //return;
            if (!sw.IsRunning) sw.Start();
            if (sw.Elapsed.TotalSeconds - lastUpdt > 0.04)
            {
                picComposition.Invalidate();
                lastUpdt = sw.Elapsed.TotalSeconds;
                //if (saGL != null && wo != null) saGL.DrawCurPbTime((float)time);
            }
        }

        private void picComposition_MouseClick(object sender, MouseEventArgs e)
        {
            ps.SetTime ( (float)e.X / picComposition.Width * saComposed.time[saComposed.time.Length - 1]);
        }

        #endregion

        #region Visual feedback of play and comment position
        Font f = new Font("Arial", 9, FontStyle.Bold);
        private void picComposition_Paint(object sender, PaintEventArgs e)
        {
            //draw where comments are
            foreach (AudioComment ac in lstAudioComments)
            {
                float t00 = (float)ac.initTime / saComposed.time[saComposed.time.Length - 1] * (picComposition.Width);
                float tff = (float)(ac.initTime + ac.saComment.time[ac.saComment.time.Length - 1]) / saComposed.time[saComposed.time.Length - 1] * (picComposition.Width);
                e.Graphics.FillRectangle(Brushes.Blue, t00, 20, tff - t00, picComposition.Height - 22);
            }

            //draw progress if playing file
            if (isPlayingFile)
            {
                float audioPosition = (float)curTime / saComposed.time[saComposed.time.Length - 1] * (picComposition.Width);
                e.Graphics.DrawLine(new Pen(Color.Red, 3), audioPosition, -1, audioPosition, picComposition.Height);
                e.Graphics.DrawString(Math.Round(curTime, 1).ToString() + "s ", f, Brushes.Black, 3, 3);
            }


        }
        #endregion


        #region Save .WAV and .MP3

        private void btnSaveWithCmt_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Rec|*.wav";

            if (Directory.Exists(PratiCantoForms.ClientFolder)) sfd.InitialDirectory = PratiCantoForms.ClientFolder;
            try
            {
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    if (isPausedFile) saComposed.SavePart(sfd.FileName, 0, curTime);
                    else saComposed.SavePart(sfd.FileName, 0, saComposed.time[saComposed.time.Length - 1]);

                    using (var reader = new MediaFoundationReader(sfd.FileName))
                    {
                        MediaFoundationEncoder.EncodeToMp3(reader, sfd.FileName + ".mp3");
                    }
                }
            }
            catch
            {
                MessageBox.Show("File error!");
            }
        }

        #endregion


        #region Cosmetics

        private void AdjustButtons()
        {
            btnStartAddComment.FlatStyle = FlatStyle.Flat;
            btnStartAddComment.FlatAppearance.BorderSize = 0;


            LayoutFuncs.AdjustButtonGraphic(btnOpenFileComment, false);
            LayoutFuncs.AdjustButtonGraphic(btnPlayOrigFile, false);
            LayoutFuncs.AdjustButtonGraphic(btnPauseOrigFile, false);
            LayoutFuncs.AdjustButtonGraphic(btnStopOrigFile, false);
            LayoutFuncs.AdjustButtonGraphic(btnDeleteComment, false);

            LayoutFuncs.AdjustButtonGraphic(btnPlaySelCmt, false);
            LayoutFuncs.AdjustButtonGraphic(btnSaveWithCmt, true);
             

        }


        private void gbChooseFileOpen_Paint(object sender, PaintEventArgs e)
        {
            PratiCanto.LayoutFuncs.DoGroupBoxLayout((GroupBox)sender, e.Graphics);
        }
        private void gbAudioCom_Paint(object sender, PaintEventArgs e)
        {
            PratiCanto.LayoutFuncs.DoGroupBoxLayout((GroupBox)sender, e.Graphics);
        }

        #endregion


        #region voiceover or not?
        private void chkVoiceOver_CheckedChanged(object sender, EventArgs e)
        {
            numVolComment.Enabled = chkVoiceOver.Checked;
            numVolAudio.Enabled = chkVoiceOver.Checked;

            if (chkVoiceOver.Checked)
            {
                //was not using voiceover. Need to compensate bringing comments
                double allDuration = 0;
                for (int k = 0; k < lstAudioComments.Count; k++)
                {
                    lstAudioComments[k].initTime -= allDuration;
                    allDuration += lstAudioComments[k].saComment.time[lstAudioComments[k].saComment.time.Length - 1];
                }
            }
            else
            {
                //was using voiceover. need to compensate delaying comments
                double allDuration = 0;
                for (int k = 0; k < lstAudioComments.Count; k++)
                {
                    lstAudioComments[k].initTime += allDuration;
                    allDuration += lstAudioComments[k].saComment.time[lstAudioComments[k].saComment.time.Length - 1];
                }
            }

            ComposeCommentAudio();

        }
        private void numVolComment_ValueChanged(object sender, EventArgs e)
        {
            ComposeCommentAudio();
        }

        private void numVolAudio_ValueChanged(object sender, EventArgs e)
        {
            ComposeCommentAudio();
        }
        #endregion

        private void frmComposer_FormClosing(object sender, FormClosingEventArgs e)
        {
            btnStopOrigFile_Click(sender, e);
        }








    }
}
