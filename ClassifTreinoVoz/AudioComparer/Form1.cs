using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using NAudio.Wave;
using System.IO;
using ClassifTreinoVoz;
using System.Drawing.Imaging;
using OpenTK.Graphics.OpenGL;
using TensorFlow;

namespace AudioComparer
{
    //TODO:

    //modificar sampling frequency
    //alertas automaticos
    //identificar saturacao no audio automaticamente
    //ibm voice

    //tocar escala
    //identificar quebra
    //exibir enquanto grava OK
    //mostrar os dois: referecia e produzido
    //pinch to zoom



    //Tutorial PRAAT
    //rotular trechos do audio
    //nome do arquivo OK

    //Trial por tempo limitado
    //Armazenar informações relacionadas aos clientes
    //seleção e recorte de trechos OK
    //barra de seleção de trechos OK
    //comparacao de audio lado a lado OK
    //comparacao no mesmo audio

    //exibir/esconder OK?


    //tempo de coleta: pacientes precisam de atualizacao - configuracoes
    //tipos de capturas?
    //tipo de trabalho a ser feito?





    public partial class frmAudioComparer : Form
    {
        /// <summary>Constructor</summary>
        public frmAudioComparer()
        {
            InitializeComponent();
        }

        #region Check licence



        #endregion

        SampleAudio curSample;
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!isVoiceAnalysisLicensed) return;

            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Rec|*.wav;*.mp3";

            if (Directory.Exists(PratiCantoForms.ClientFolder)) ofd.InitialDirectory = PratiCantoForms.ClientFolder;

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                closeVideo();
                OpenAudioFile(ofd.FileName);
                this.Text = ofd.FileName;
            }
        }

        private void OpenAudioFile(string filename)
        {
            //stop playback
            if (replaying && wo != null && wo.PlaybackState == PlaybackState.Playing) btnPlay_Click(this, new EventArgs());

            if (curSample != null) curSample.Dispose();
            curSample = new SampleAudio(filename);

            setGLDrawToCurSample();
        }

        #region Data drawing and scaling

        private void btnZoomIn_Click(object sender, EventArgs e)
        {
            saGL.ZoomIn();
            if (sample2 != null) saGLCompare.ZoomIn();
        }
  

        private void btnZoomOut_Click(object sender, EventArgs e)
        {
            saGL.ZoomOut();
            if (sample2 != null) saGLCompare.ZoomOut();
        }


        #endregion


        #region Control bar


        private void picControlBar_Resize(object sender, EventArgs e)
        {
            picControlBar.Refresh();
        }


        #endregion


        #region Client management
        /// <summary>Display client information</summary>
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

        private void btnClient_Click(object sender, EventArgs e)
        {
            PratiCantoForms.ShowClientForm();

            //SetClient();
        }
        #endregion
        /// <summary>Is this module licensed?</summary>
        bool isVoiceAnalysisLicensed;
        private void frmAudioComparer_Load(object sender, EventArgs e)
        {
            isVoiceAnalysisLicensed = false; // SoftwareKey.CheckLicense("VoiceAnalysis", false);

            //disables features that require licensing
            // gBoxAudio.Enabled = isVoiceAnalysisLicensed;
            btnClient.Enabled = isVoiceAnalysisLicensed;
            btnClient.Visible = isVoiceAnalysisLicensed;
            btnSaveRecord.Enabled = isVoiceAnalysisLicensed;
            fileToolStripMenuItem.Enabled = isVoiceAnalysisLicensed;
            viewToolStripMenuItem.Enabled = isVoiceAnalysisLicensed;
            editToolStripMenuItem.Enabled = isVoiceAnalysisLicensed;
            filterToolStripMenuItem.Enabled = isVoiceAnalysisLicensed;

            btnComputeF0formants.Enabled = isVoiceAnalysisLicensed;
            btnComputeF0formants.Visible = isVoiceAnalysisLicensed;

            btnAnnotate.Visible = isVoiceAnalysisLicensed;
            txtAnnotation.Visible = isVoiceAnalysisLicensed;

            SampleAudioGL.InfoBarItems = lblFloatingAnnotMenuItems.Text;

            //set client if already chosen
            SetClient();
            
            //phonetic symbols
            PutPhoneticSymbols();

            //List mics
            List<string> mics = SampleAudio.RealTime.GetMicrophones();
            foreach (string s in mics) cmbInputDevice.Items.Add(s);
            if (mics.Count <= 1) cmbInputDevice.Visible = false;
            if (mics.Count >= 1) cmbInputDevice.SelectedIndex = 0;

            //load deep learning model if available
            loadModelToolStripMenuItem_Click(sender, e);

            initGL();
        }

        #region Phonetic symbols

        private void PutPhoneticSymbols()
        {
            string phoneticConsonants = "p b t d k g|tʃ dʒ|f v θ ð s z ʃ ʒ h m n ŋ|l r|j w ʔ";
            string phoneticVowels = "ɪ e æ ɒ ʌ ʊ|iː eɪ aɪ ɔɪ|uː əʊ aʊ ɪə eə ɑː ɔː ʊə ɜː|ə i u|n̩ l̩ ˈ";
            string phonetic = phoneticConsonants + "|" + phoneticVowels;


            foreach (ToolStripMenuItem m in btnPhoneticSymbols.DropDownItems)
            {
                string[] s = m.Text.Split(' ');
                foreach (string ss in s)
                {
                    ToolStripMenuItem ts = new ToolStripMenuItem(ss);
                    m.DropDownItems.Add(ts);
                    ts.Click += new EventHandler(ts_Click);
                }

            }
        }

        void ts_Click(object sender, EventArgs e)
        {
            if (!txtAnnotation.Text.StartsWith("[2]")) txtAnnotation.Text = "[2]";
            txtAnnotation.Text += ((ToolStripMenuItem)sender).Text;
        }


        #endregion

        /// <summary>Sine wave example</summary>
        public class SineWaveProvider32 : WaveProvider32
        {
            int sample;
            /// <summary>Constructor</summary>
            public SineWaveProvider32()
            {
                Frequency = 1000;
                Amplitude = 0.25f; // let's not hurt our ears            
            }
            /// <summary>Frequency</summary>
            public float Frequency { get; set; }
            /// <summary>Amplitude</summary>
            public float Amplitude { get; set; }

            /// <summary>Reads audio data</summary>
            public override int Read(float[] buffer, int offset, int sampleCount)
            {
                List<float> b2 = new List<float>();

                int sampleRate = WaveFormat.SampleRate;
                for (int n = 0; n < sampleCount; n++)
                {
                    float ii = (float)(Amplitude * Math.Sin((2 * Math.PI * sample * Frequency) / sampleRate));
                    buffer[n + offset] = ii;

                    b2.Add(ii);

                    sample++;
                    if (sample >= sampleRate) sample = 0;
                }
                return sampleCount;
            }
        }

        #region Record audio
        bool recording = false;
        SampleAudio.RealTime rt;

        string recFileName = "";
        private void btnRec_Click(object sender, EventArgs e)
        {
            if (!recording)
            {
                closeVideo();

                string UserFolder;
                if (Directory.Exists(PratiCantoForms.ClientFolder)) UserFolder = PratiCantoForms.ClientFolder;
                else UserFolder = Application.StartupPath + "\\" + PratiCantoForms.ClientFolder;

                if (!Directory.Exists(UserFolder)) Directory.CreateDirectory(UserFolder);

                string recFolder = PratiCantoForms.getRecFolder();
                if (!Directory.Exists(recFolder)) Directory.CreateDirectory(recFolder);

                recFileName = recFolder + "\\" + DateTime.Now.Hour.ToString() + "h" + DateTime.Now.Minute.ToString().PadLeft(2, '0') + "min" + DateTime.Now.Second.ToString().PadLeft(2, '0') + "s.wav";

                rt = new SampleAudio.RealTime
                {
                    OnDataReceived = SoundReceived
                };
                rt.StartRecording(cmbInputDevice.SelectedIndex);

                btnRec.Image = picStop.Image;

                recording = true;

            }
            else
            {
                recording = false;
                rt.StopRecording();

                saGL.ResetRealTimeDrawing();

                if (curSample != null) curSample.Dispose();
                curSample = new SampleAudio(rt);
                saGL.SetSampleAudio(curSample);

                btnRec.Image = picRec.Image;

            }
        }

        private void SoundReceived()
        {
            saGL.DrawRealTime(rt);
        }
        #endregion

        #region Replay audio
        bool replaying = false;
        private WaveOut wo, wo2;
        private void btnPlay_Click(object sender, EventArgs e)
        {
            int repSpeedPercent;

            if (!int.TryParse(txtReplaySpeed.Text, out repSpeedPercent))
            {
                repSpeedPercent = 100;
                txtReplaySpeed.Text = "100";
            }
            else
            {
                if (repSpeedPercent < 10 || repSpeedPercent > 500)
                {
                    repSpeedPercent = 100;
                    txtReplaySpeed.Text = "100";
                }
            }

            if (curSample == null) return;

            if (!replaying)
            {
                //Play video
                if (dxplayer != null)
                {
                    dxplayer.Stop();
                    dxplayer.Start();
                }

                //Play
                saGL.DrawCurrentPlaybackTime = true;

                double t0Play = saGL.p0SelX;
                double tfPlay = saGL.pfSelX;
                if (t0Play == tfPlay)
                {
                    t0Play = 0;
                    tfPlay = curSample.time[curSample.time.Length - 1];
                }

                SampleAudio.PlaySample ps = new SampleAudio.PlaySample(curSample, t0Play, tfPlay);
                ps.PlayTimeFunc = DrawTime;
                ps.ReplaySpeed = 0.01 * (double)repSpeedPercent;

                if (wo != null) wo.Dispose();
                wo = new WaveOut();
                wo.PlaybackStopped += new EventHandler<StoppedEventArgs>(wo_PlaybackStopped);
                wo.Init(ps);

                if (saGL.IsSelected || sample2 == null || (sample2 != null && !saGLCompare.IsSelected))
                {
                    playingOnlyCompareAudio = false;
                    wo.Play();
                }
                else playingOnlyCompareAudio = true;

                //comparison audio selected
                if (sample2 != null && saGLCompare.p0SelX != saGLCompare.pfSelX)
                {
                    SampleAudio.PlaySample ps2 = new SampleAudio.PlaySample(sample2, saGLCompare.p0SelX, saGLCompare.pfSelX);
                    ps2.ReplaySpeed = 0.01 * (double)repSpeedPercent;
                    ps2.PlayTimeFunc = DrawTimeComp;
                    saGLCompare.DrawCurrentPlaybackTime = true;

                    if (wo2 != null) wo2.Dispose();
                    wo2 = new WaveOut();
                    wo2.Init(ps2);
                    wo2.Play();
                    wo2.PlaybackStopped += new EventHandler<StoppedEventArgs>(wo2_PlaybackStopped);
                }

                //progPlay.Visible = true;
                btnPlay.Image = picStop.Image;
                replaying = true;
                //lblAudioPos.Visible = true;
            }
            else
            {
                wo.Stop();
                wo_PlaybackStopped(this, new StoppedEventArgs());

                if (playingOnlyCompareAudio)
                {
                    wo2.Stop();
                    wo2_PlaybackStopped(this, new StoppedEventArgs());
                }
            }

        }

        bool playingOnlyCompareAudio = false;
        void wo2_PlaybackStopped(object sender, StoppedEventArgs e)
        {
            if (playingOnlyCompareAudio)
            {
                if (saGL != null) saGL.DrawCurrentPlaybackTime = false;
                if (saGLCompare != null) saGLCompare.DrawCurrentPlaybackTime = false;
                btnPlay.Image = picPlay.Image;
                replaying = false;

                if (dxplayer != null) dxplayer.Stop();
                if (wo != null) wo.Dispose();
                if (wo2 != null) wo2.Dispose();
            }
        }
        void wo_PlaybackStopped(object sender, StoppedEventArgs e)
        {
            if (saGL!=null) saGL.DrawCurrentPlaybackTime = false;
            if (saGLCompare != null) saGLCompare.DrawCurrentPlaybackTime = false;
            //progPlay.Value = 100;
            //progPlay.Visible = false;
            btnPlay.Image = picPlay.Image;
            replaying = false;

            if (dxplayer != null) dxplayer.Stop();
            if (wo != null) wo.Dispose();
            if (wo2 != null) wo2.Dispose();
        }

        System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
        double lastUpdt = 0;
        /// <summary>Draws time when playing. Receives total audio time (in original file)</summary>
        /// <param name="time">Time to draw</param>
        private void DrawTime(double time)
        {
            if (!sw.IsRunning) sw.Start();
            if (sw.Elapsed.TotalSeconds - lastUpdt > 0.02)
            {
                if (dxplayer != null) dxplayer.SetTime(getVideoTime(time));

                lastUpdt = sw.Elapsed.TotalSeconds;
                if (saGL!=null && wo!=null) saGL.DrawCurPbTime((float)time, true);
            }
            else
            {
            }
        }
        System.Diagnostics.Stopwatch swComp = new System.Diagnostics.Stopwatch();
        double lastUpdtComp = 0;
        /// <summary>Draws time when playing. Receives total audio time (in original file)</summary>
        /// <param name="time">Time to draw</param>
        private void DrawTimeComp(double time)
        {
            if (!swComp.IsRunning) swComp.Start();
            if (swComp.Elapsed.TotalSeconds - lastUpdtComp > 0.02)
            {
                lastUpdtComp = swComp.Elapsed.TotalSeconds;
                if (saGLCompare != null && wo2 != null) saGLCompare.DrawCurPbTime((float)time, true);
            }
            else
            {
            }
        }


 

        #endregion

        private void txtReplaySpeed_Leave(object sender, EventArgs e)
        {
            //return;

            ////TODO: remove, this is temporary
            //double stepTime = curSample.time[1];
            //double time = double.Parse(txtReplaySpeed.Text);

            //int idx = (int)(time / stepTime);

            //double f = SampleAudio.RealTime.ComputeBaseFrequency(curSample.audioData, curSample.time[1], idx, 50);


        }

        #region Filter audio
        private void menuAvg1ms_Click(object sender, EventArgs e)
        {
            curSample.FilterMean(0.0001);
            curSample.audioSpectre = null;
            saGL.UpdateDrawing();
        }
        private void menuAvg10ms_Click(object sender, EventArgs e)
        {
            curSample.FilterMean(0.0005);
            curSample.audioSpectre = null;
            saGL.UpdateDrawing();
        }
        private void menuAvg25ms_Click(object sender, EventArgs e)
        {
            curSample.FilterMean(0.001);
            curSample.audioSpectre = null;
            saGL.UpdateDrawing();
        }
        private void menuAvg50ms_Click(object sender, EventArgs e)
        {
            curSample.FilterMean(0.005);
            curSample.audioSpectre = null;
            saGL.UpdateDrawing();
        }
        
        private void msToolStripMenuItem_Click(object sender, EventArgs e)
        {
            curSample.FilterMedian(0.0001);
            curSample.audioSpectre = null;
            saGL.UpdateDrawing();
        }

        private void msToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            curSample.FilterMedian(0.0005);
            curSample.audioSpectre = null;
            saGL.UpdateDrawing();
        }

        private void msToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            curSample.FilterMedian(0.001);
            curSample.audioSpectre = null;
            saGL.UpdateDrawing();
        }

        private void msToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            curSample.FilterMedian(0.0015);
            curSample.audioSpectre = null;

            saGL.UpdateDrawing();
            
        }

        private void medianToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            curSample.MedianFilterSpectrogram(3, 1, true);
            //curSample.MedianFilterSpectrogram(4, 1, true);

            //curSample.MedianFilterSpectrogram(4, 2, false);

            saGL.UpdateDrawing();
        }

        #endregion

        #region View Menu
        private void fundamentalFrequencyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saGL.ViewFundFreq = fundamentalFrequencyToolStripMenuItem.Checked;
            glPicSpectre.Invalidate();
            if (sample2 != null)
            {
                saGLCompare.ViewFundFreq = fundamentalFrequencyToolStripMenuItem.Checked;
                GLPicSpec2.Invalidate();
            }
            
        }
        private void intensityToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saGL.ViewFundIntensity = intensityToolStripMenuItem.Checked;
            glPicSpectre.Invalidate();
            if (sample2 != null)
            {
                saGLCompare.ViewFundIntensity = intensityToolStripMenuItem.Checked;
                GLPicSpec2.Invalidate();
            }

        }
        private void formantsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saGL.ViewFormants = formantsToolStripMenuItem.Checked;
            glPicSpectre.Invalidate();
            if (sample2 != null)
            {
                saGLCompare.ViewFormants = formantsToolStripMenuItem.Checked;
                GLPicSpec2.Invalidate();
            }
        }
        private void harmonictonoiseRatioToolStripMenuItem_Click(object sender, EventArgs e)
        {

            saGL.ViewHNR = harmonictonoiseRatioToolStripMenuItem.Checked;
            glPicSpectre.Invalidate();
            if (sample2 != null)
            {
                saGLCompare.ViewHNR = harmonictonoiseRatioToolStripMenuItem.Checked;
                GLPicSpec2.Invalidate();
            }
        }


        bool showSpectreInColor = false;
        bool showSpectreMonochromatic = true;
        private void monochromaticToolStripMenuItem_Click(object sender, EventArgs e)
        {
            showSpectreMonochromatic = monochromaticToolStripMenuItem.Checked;
            if (showSpectreMonochromatic)
            {
                showSpectreInColor = false;
                inColorToolStripMenuItem.Checked = false;
            }

            saGL.ViewColorSpectre = showSpectreInColor;
            saGL.ViewBWSpectre = showSpectreMonochromatic;
            saGL.UpdateDrawing();

            if (sample2 != null)
            {
                saGLCompare.ViewColorSpectre = showSpectreInColor;
                saGLCompare.ViewBWSpectre = showSpectreMonochromatic;
                saGLCompare.UpdateDrawing();
            }

        }
        private void inColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            showSpectreInColor = inColorToolStripMenuItem.Checked;
            if (showSpectreInColor)
            {
                showSpectreMonochromatic = false;
                monochromaticToolStripMenuItem.Checked = false;
            }

            saGL.ViewColorSpectre = showSpectreInColor;
            saGL.ViewBWSpectre = showSpectreMonochromatic;
            saGL.UpdateDrawing();

            if (sample2 != null)
            {
                saGLCompare.ViewColorSpectre = showSpectreInColor;
                saGLCompare.ViewBWSpectre = showSpectreMonochromatic;
                saGLCompare.UpdateDrawing();
            }
        }


        private void increaseSpectreHeightToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saGL.IncreaseSpectreHeight();
            saGLCompare.IncreaseSpectreHeight();
        }
        private void reduceSpectreHeightToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saGL.ResetSpectreHeight();
            saGLCompare.ResetSpectreHeight();
        }
        private void highlightHarmonicsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SampleAudioGL.highlightHarmonics = highlightHarmonicsToolStripMenuItem.Checked;
            if (saGL != null) saGL.UpdateDrawing();
            if (saGLCompare != null) saGLCompare.UpdateDrawing();
        }


        #region number of formants to show
        private void toolStripMenuItem10_Click(object sender, EventArgs e)
        {
            saGL.MAXFORMANTS = 2;
            saGLCompare.MAXFORMANTS = 2;
            CheckNumFormants((ToolStripMenuItem)sender);

        }
        private void toolStripMenuItem11_Click(object sender, EventArgs e)
        {
            saGL.MAXFORMANTS = 3;
            saGLCompare.MAXFORMANTS = 3;
            CheckNumFormants((ToolStripMenuItem)sender);

        }
        private void toolStripMenuItem12_Click(object sender, EventArgs e)
        {
            saGL.MAXFORMANTS = 4;
            saGLCompare.MAXFORMANTS = 4;
            CheckNumFormants((ToolStripMenuItem)sender);

        }

        private void toolStripMenuItem13_Click(object sender, EventArgs e)
        {
            saGL.MAXFORMANTS = 5;
            saGLCompare.MAXFORMANTS = 5;
            CheckNumFormants((ToolStripMenuItem)sender);
        }

        private void toolStripMenuItem14_Click(object sender, EventArgs e)
        {
            saGL.MAXFORMANTS = 6;
            saGLCompare.MAXFORMANTS = 6;
            CheckNumFormants((ToolStripMenuItem)sender);
        }

        private void CheckNumFormants(ToolStripMenuItem t)
        {
            toolStripMenuItem10.Checked = false;
            toolStripMenuItem11.Checked = false;
            toolStripMenuItem12.Checked = false;
            toolStripMenuItem13.Checked = false;
            toolStripMenuItem14.Checked = false;
            t.Checked = true;
            saGL.UpdateDrawing();
            saGLCompare.UpdateDrawing();
        }
        #endregion

        //amplification of fundamental frequency
        private void toolStripMenuItemF0Amp4_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem tsmi = (ToolStripMenuItem)sender;

            toolStripMenuItemF0Amp1.Checked = false;
            toolStripMenuItemF0Amp2.Checked = false;
            toolStripMenuItemF0Amp3.Checked = false;
            toolStripMenuItemF0Amp4.Checked = false;

            tsmi.Checked = true;

            float ampFac = float.Parse(tsmi.Text);

            saGL.F0Amplification = ampFac;
            saGL.UpdateDrawing();

            if (saGLCompare != null)
            {
                saGLCompare.F0Amplification = ampFac;
                saGLCompare.UpdateDrawing();
            }
        }

        //maximum intensity level (dBr)
        private void toolStripMenuItemRIntens0_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem tsmi = (ToolStripMenuItem)sender;

            toolStripMenuItemRIntens0.Checked = false;
            toolStripMenuItemRIntens1.Checked = false;
            toolStripMenuItemRIntens2.Checked = false;
            toolStripMenuItemRIntens3.Checked = false;

            tsmi.Checked = true;

            float ampFac = float.Parse(tsmi.Text);

            saGL.IntensRange = ampFac;
            saGL.UpdateDrawing();

            if (saGLCompare != null)
            {
                saGLCompare.IntensRange = ampFac;
                saGLCompare.UpdateDrawing();
            }
        }

        #endregion

        #region Compare two audio files

        SampleAudio sample2;
        private void compareToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (Directory.Exists(PratiCantoForms.ClientFolder)) ofd.InitialDirectory = PratiCantoForms.ClientFolder;
            ofd.Filter = "Rec|*.wav;*.mp3";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                //stop playback
                if (replaying && wo2 != null && wo2.PlaybackState == PlaybackState.Playing) btnPlay_Click(sender, e);

                if (sample2 != null) sample2.Dispose();
                sample2 = new SampleAudio(ofd.FileName);
                saGLCompare.SetSampleAudio(sample2);

                picControlBarCompare.Top = picControlBar.Top;
                GLPicIntens2.Top = GLPicIntens.Top; GLPicIntens2.Height = GLPicIntens.Height;
                GLPicSpec2.Top = glPicSpectre.Top; GLPicSpec2.Height = glPicSpectre.Height;

                int w = this.Width / 2 - 10;
                picControlBar.Width = w;
                GLPicIntens.Width = w;
                glPicSpectre.Width = w;

                picControlBarCompare.Visible = true;
                GLPicSpec2.Visible = true;
                GLPicIntens2.Visible = true;
                lblSpecInfoCompare.Visible = true;

                
                picControlBarCompare.Width = w;
                GLPicIntens2.Width = w;
                GLPicSpec2.Width = w;
                GLPicIntens2.Left = w+10;
                GLPicSpec2.Left = w+10;
                picControlBarCompare.Left = w + 10;
                lblSpecInfoCompare.Top = GLPicSpec2.Top; lblSpecInfoCompare.Left = GLPicSpec2.Left;

                
                saGL.ResetDrawingRanges();
                saGLCompare.ResetDrawingRanges();
            }
        }



        private void stopComparingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (sample2 != null)
            {
                //stop playback
                if (replaying && wo2 != null && wo2.PlaybackState == PlaybackState.Playing) btnPlay_Click(sender, e);

                sample2.Dispose();
                sample2 = null;
            }
            picControlBarCompare.Visible = false;
            GLPicSpec2.Visible = false;
            GLPicIntens2.Visible = false;
            lblSpecInfoCompare.Visible = false;

            picControlBar.Width = gBoxAudio.Width - 10;
            GLPicIntens.Width = gBoxAudio.Width - 10;
            glPicSpectre.Width = gBoxAudio.Width - 10;

            saGL.ResetDrawingRanges();
        }

 

        #endregion


        #region Save and edit audio
        private void deleteSelectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (curSample == null) return;

            curSample.Delete(saGL.p0SelX, saGL.pfSelX);
            curSample.RecomputeAnnotationsDelPart(saGL.p0SelX, saGL.pfSelX);

            if (dxplayer != null) cutTimes.Add(new float[] { saGL.p0SelX, saGL.pfSelX });

            curSample.audioSpectre = null;
            curSample.RecomputeSpectre();

            //deselects
            saGL.p0SelX = 0;
            saGL.pfSelX = 0;

            //redraw
            saGL.ResetDrawingRanges();
            saGL.UpdateDrawing();

            if (sample2 != null && saGLCompare.p0SelX != saGLCompare.pfSelX)
            {
                sample2.Delete(saGLCompare.p0SelX, saGLCompare.pfSelX);
                sample2.RecomputeAnnotationsDelPart(saGLCompare.p0SelX, saGLCompare.pfSelX);

                sample2.audioSpectre = null;
                sample2.RecomputeSpectre();
                
                //deselects
                saGLCompare.p0SelX = 0;
                saGLCompare.pfSelX = 0;

                //redraw
                saGLCompare.ResetDrawingRanges();
                saGLCompare.UpdateDrawing();
            }
        }
        private void increaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (curSample == null) return;
            curSample.ChangeVolume(saGL.p0SelX, saGL.pfSelX, 2.0f);
            curSample.audioSpectre = null;
            saGL.UpdateDrawing();
        }
        private void decreaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (curSample == null) return;
            curSample.ChangeVolume(saGL.p0SelX, saGL.pfSelX, 0.5f);
            curSample.audioSpectre = null;
            saGL.UpdateDrawing();
        }

        private void baseFrequencyAndFormantsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (curSample == null) return;

            curSample.ComputeBaseFreqAndFormants(saGL.p0SelX, saGL.pfSelX, txtDPenalty.Text, txtChangePenalty.Text,txtNFreqs.Text,txtVarPenalty.Text, true);
            saGL.UpdateDrawing();

            if (sample2 == null) return;
            sample2.ComputeBaseFreqAndFormants(saGLCompare.p0SelX, saGLCompare.pfSelX, txtDPenalty.Text, txtChangePenalty.Text, txtNFreqs.Text, txtVarPenalty.Text, true);
            saGLCompare.UpdateDrawing();
        }

        private void saveSelectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!saGL.IsSelected) return;

            SaveFileDialog sfd = new SaveFileDialog();

            if (recFileName != "")
            {
                FileInfo fi = new FileInfo(recFileName);
                sfd.FileName = fi.Name;
                sfd.InitialDirectory = fi.DirectoryName;
            }

            sfd.Filter = "WAV|*.wav";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                curSample.SavePart(sfd.FileName, saGL.p0SelX, saGL.pfSelX);
            }
        }
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (curSample == null) return;
            SaveFileDialog sfd = new SaveFileDialog();

            if (recFileName != "")
            {
                FileInfo fi = new FileInfo(recFileName);
                sfd.FileName = fi.Name;
                sfd.InitialDirectory = fi.DirectoryName;
            }

            sfd.Filter = "REC|*.wav";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                if (!isVoiceAnalysisLicensed) return;
                curSample.SavePart(sfd.FileName, 0, curSample.time[curSample.time.Length - 1]);
                this.Text = sfd.FileName;
            }
        }
        #endregion

        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            (new ClassifTreinoVoz.AboutBox()).ShowDialog();
        }

        #region OpenGL drawings
        SampleAudioGL saGL, saGLCompare;
        private void initGL()
        {
            saGL = new SampleAudioGL(GLPicIntens, glPicSpectre, picControlBar, lblSpecInfo, txtAnnotation);
            GLPicIntens2.Height = GLPicIntens.Height; GLPicIntens2.Width = GLPicIntens.Width;
            GLPicSpec2.Height = glPicSpectre.Height; GLPicSpec2.Width = glPicSpectre.Width;
            
            saGLCompare = new SampleAudioGL(GLPicIntens2, GLPicSpec2, picControlBarCompare, lblSpecInfoCompare, txtAnnotation);
            GLPicIntens2.Visible = false;
            GLPicSpec2.Visible = false;

            GLPicIntens.Refresh();
            glPicSpectre.Refresh();
        }

        /// <summary>Updates data in OpenGL</summary>
        private void updateGL()
        {
            
        }

        private void setGLDrawToCurSample()
        {
            saGL.SetSampleAudio(curSample);

            updateGL();
        }

        private void GLPicIntens_Load(object sender, EventArgs e)
        {
            //SampleAudioGL.InitGL();
            //SampleAudioGL.SetupViewport((OpenTK.GLControl)sender, new float[] { 0, 1, 0, 1 });
        }
        private void glPicSpectre_Load(object sender, EventArgs e)
        {
            //SampleAudioGL.InitGL();
            //SampleAudioGL.SetupViewport((OpenTK.GLControl)sender, new float[] { 0, 1, 0, 1 });
        }
        
        private void frmAudioComparer_Resize(object sender, EventArgs e)
        {
            if (saGL != null) saGL.SetupViewPorts();
        }
        #endregion

        #region Audio annotations
        private string GetSubtitleFileName(string file)
        {
            FileInfo fi = new FileInfo(file);
            return fi.DirectoryName + "\\" + fi.Name + ".srt";
        }
        private void btnAnnotate_Click(object sender, EventArgs e)
        {
            if (saGL.IsSelected)
            {
                curSample.Annotations.Add(new SampleAudio.AudioAnnotation(txtAnnotation.Text, saGL.p0SelX, saGL.pfSelX));
                picControlBar.Invalidate();

                //remake annotation graphics
                saGL.DrawAnnotations();
                saGL.UpdateDrawing();
                //attempt to save annotations
                if (curSample.AudioFile != "")
                {
                    try
                    {
                        curSample.SaveAnnotations();
                    }
                    catch
                    {
                    }
                }
            }
        }
        #endregion

        #region Region analysis
        frmAnalyzePart frmAnP;
        private void regionAnalysisToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (curSample == null) return;

            if (frmAnP == null || frmAnP.IsDisposed) frmAnP = new frmAnalyzePart(saGL);
            //frmAnP.MdiParent = this.MdiParent;
            frmAnP.Show();

            //frmAnP.numEnd.Value = (decimal)saGL.pfSelX;
            //frmAnP.numStart.Value = (decimal)saGL.p0SelX;
        }


        #endregion

        private void frmAudioComparer_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (recording) btnRec_Click(sender, e);

            if (wo != null) wo.Dispose();
            if (wo2 != null) wo2.Dispose();
            if (dxplayer != null) dxplayer.Dispose();
        }

        private void btnVoiceTraining_Click(object sender, EventArgs e)
        {
            ClassifTreinoVoz.PratiCantoForms.ShowFormPractice();
        }

        private void audioAnnotationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (curSample == null) return;
            curSample.Annotations.Clear();
            picControlBar.Invalidate();
        }

        private void btnCleanAll_Click(object sender, EventArgs e)
        {
            float min = curSample.audioData[0];
            float max = curSample.audioData[0];
            for (int k = 0; k < curSample.audioData.Length; k++)
            {
                min = Math.Min(min, curSample.audioData[k]);
                max = Math.Max(max, curSample.audioData[k]);
                curSample.audioData[k] = 0;
            }
            curSample.audioData[0] = min;
            for (int k = 1; k < curSample.audioData.Length; k++) curSample.audioData[k] = max;

            curSample.audioSpectre = null;

            setGLDrawToCurSample();
        }

        private void exportWaveformToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //return;

            frmEditWaveForm frmEWF = new frmEditWaveForm();
            frmEWF.Show();

            int idx0 = (int)(saGL.p0SelX / curSample.time[1]);
            int idxf = (int)(saGL.pfSelX / curSample.time[1]) + 1;

            frmEWF.wIntens.Clear();
            frmEWF.wIntensTime.Clear();
            frmEWF.wAmpTime.Clear();
            frmEWF.wAmp.Clear();

            float invTotTime = 1.0f / (curSample.time[idxf] - curSample.time[idx0]);

            float maxIntens = float.MinValue;
            for (int k = idx0; k <= idxf; k++)
            {
                float intens = curSample.audioData[k];
                float localTime = (curSample.time[k] - curSample.time[idx0]) * invTotTime;
                frmEWF.wIntensTime.Add(localTime);
                frmEWF.wIntens.Add(intens);
                maxIntens = Math.Max(maxIntens, Math.Abs(intens));

                frmEWF.wAmpTime.Add(localTime);
                frmEWF.wAmp.Add(1);
            }

            maxIntens = 1.0f/maxIntens;
            for (int k = 0; k < frmEWF.wIntens.Count; k++) frmEWF.wIntens[k] *= maxIntens;
        }


        #region Open video
        DxPlay.DxPlay dxplayer;
        
        /// <summary>Used to sync video time when parts of audio are cut</summary>
        List<float[]> cutTimes = new List<float[]>();
        double getVideoTime(double time)
        {
            double videoTime = time;
            for (int k = cutTimes.Count - 1; k >= 0; k--)
            {
                if (time > cutTimes[k][0]) 
                    videoTime += cutTimes[k][1] - cutTimes[k][0];
            }
            return videoTime;
        }

        private void openVideoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Video|*.mp4;*.avi;*.mov;*.wma;*.aiff;*.au;*.flv;*.3gp";

            if (Directory.Exists(PratiCantoForms.ClientFolder)) ofd.InitialDirectory = PratiCantoForms.ClientFolder;

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                closeVideo();

                string outaudioFileName;
                dxplayer = PratiCanto.Video.ImportVideoClass.ImportVideo(ofd.FileName, panVideo, out outaudioFileName);

                if (dxplayer != null)
                {
                    OpenAudioFile(outaudioFileName);
                    
                    
                    try
                    {
                        //doesn't need audio anymore
                        File.Delete(outaudioFileName);
                    }
                    catch
                    {
                    }
                    panContainer.Visible = true;
                }
                else MessageBox.Show("Can't open video");

                if (File.Exists(outaudioFileName))
                {
                    //doesn't need audio anymore
                    File.Delete(outaudioFileName);
                }
            }
        }

        private void closeVideo()
        {
            if (dxplayer != null)
            {
                dxplayer.Dispose();
                dxplayer = null;
            }
            cutTimes.Clear();
            panContainer.Visible = false;
        }

        bool clickVideo = false;
        private void panVideo_MouseDown(object sender, MouseEventArgs e)
        {
        }
        private void panVideo_MouseUp(object sender, MouseEventArgs e)
        {
        }

        private void panVideo_MouseMove(object sender, MouseEventArgs e)
        {

        }
        private void panContainer_MouseDown(object sender, MouseEventArgs e)
        {
            clickVideo = true;
        }

        private void panContainer_MouseUp(object sender, MouseEventArgs e)
        {
            if (clickVideo)
            {
                dxplayer.SetWindowSize(panVideo);

                clickVideo = false;
            }
        }

        private void panContainer_MouseMove(object sender, MouseEventArgs e)
        {
            if (clickVideo)
            {
                int left = glPicSpectre.Width - panContainer.Width + e.X;
                int width = panContainer.Width - e.X;
                int height = e.Y;
                if (width > 50 && height > 50)
                {
                    panContainer.Left = left;
                    panContainer.Width = width;
                    panContainer.Height = height;

                    
                }
            }
        }
        #endregion


        #region Save to image
        private void saveToImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.FileName = "Screenshot.JPG";
            sfd.Filter = "Jpeg|*.JPG";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                //SCREENSHOT
                int Worig = glPicSpectre.Width;
                int Horig = glPicSpectre.Height;
                //glPicSpectre.Width = 1920 * 4;
                //glPicSpectre.Height = 1080 * 4;

                //saGL.UpdateDrawing();
                //glPicSpectre.MakeCurrent();
                //Application.DoEvents();

                Bitmap bmp = new Bitmap(glPicSpectre.Width, glPicSpectre.Height);

                System.Drawing.Imaging.BitmapData data = bmp.LockBits(new Rectangle(0, 0, glPicSpectre.Width, glPicSpectre.Height), System.Drawing.Imaging.ImageLockMode.WriteOnly,
                    System.Drawing.Imaging.PixelFormat.Format24bppRgb);
                GL.ReadPixels(0, 0, glPicSpectre.Width, glPicSpectre.Height, OpenTK.Graphics.OpenGL.PixelFormat.Bgr, PixelType.UnsignedByte, data.Scan0);
                GL.Finish();
                bmp.UnlockBits(data);
                bmp.RotateFlip(RotateFlipType.RotateNoneFlipY);



                System.Drawing.Imaging.Encoder Encoder = System.Drawing.Imaging.Encoder.Quality;
                System.Drawing.Imaging.EncoderParameters myEncoderParameters = new System.Drawing.Imaging.EncoderParameters(1);
                System.Drawing.Imaging.EncoderParameter myEncoderParameter = new System.Drawing.Imaging.EncoderParameter(Encoder, 97L);
                myEncoderParameters.Param[0] = myEncoderParameter;
                System.Drawing.Imaging.ImageCodecInfo jgpEncoder = GetEncoder(System.Drawing.Imaging.ImageFormat.Jpeg);
                bmp.Save(sfd.FileName, jgpEncoder, myEncoderParameters);

                glPicSpectre.Width = Worig;
                glPicSpectre.Height = Horig;

                bmp.Dispose();
            }



        }
        private System.Drawing.Imaging.ImageCodecInfo GetEncoder(System.Drawing.Imaging.ImageFormat format)
        {

            System.Drawing.Imaging.ImageCodecInfo[] codecs = System.Drawing.Imaging.ImageCodecInfo.GetImageDecoders();

            foreach (System.Drawing.Imaging.ImageCodecInfo codec in codecs)
            {
                if (codec.FormatID == format.Guid)
                {
                    return codec;
                }
            }
            return null;
        }

        #endregion


        #region Learning
        PhonemeRecognition pr;
        private void learnFromThisFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pr = new PhonemeRecognition(new List<SampleAudio>() { curSample });
        }

        private void classifySelectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (pr == null || pr.categories.Count <= 1) return;

            int[] lstCat = new int[pr.categories.Count];

            List<float[]> feats = curSample.ExtractFeatures(saGL.p0SelX, saGL.pfSelX,true);
            foreach (float[] ff in feats)
            {
                float m;
                int cat = (int)pr.Classify(ff, out m);
                lstCat[cat]++;
            }

            int bestCat = 0;
            int bestCatCount = lstCat[0];
            for (int k = 1; k < lstCat.Length; k++)
            {
                if (lstCat[k] > bestCatCount)
                {
                    bestCatCount = lstCat[k];
                    bestCat = k;
                }
            }

            string strCat = pr.categories[bestCat];

            curSample.Annotations.Add(new SampleAudio.AudioAnnotation(strCat, saGL.p0SelX, saGL.pfSelX));
            picControlBar.Invalidate();
        }

        private void learnFromFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.SelectedPath = Application.StartupPath;

            if (fbd.ShowDialog() == DialogResult.OK)
            {
                DirectoryInfo d = new DirectoryInfo(fbd.SelectedPath);
                FileInfo[] lstFiles = d.GetFiles("*.wav");
                List<SampleAudio> lstsa = new List<SampleAudio>();
                foreach (FileInfo fi in lstFiles)
                {
                    SampleAudio saload = new SampleAudio(fi.FullName);
                    lstsa.Add(saload);
                }
                pr = new PhonemeRecognition(lstsa);
            }
        }
        private void autoAnnotateSelectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (pr == null) return;
            List<SampleAudio.AudioAnnotation> autoAnn = pr.AutoAnnotate(curSample, saGL.p0SelX, saGL.pfSelX, 0.010f);

            curSample.Annotations.AddRange(autoAnn);
            picControlBar.Invalidate();
        }


        private void phoneticSearchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            List<SampleAudio.AudioAnnotation> phoneticSearchAnn = pr.PhoneticSearch(txtAnnotation.Text, this.curSample, saGL.p0SelX, saGL.pfSelX, 4);

            curSample.Annotations.AddRange(phoneticSearchAnn);
            picControlBar.Invalidate();
        }
        #endregion



        private void convertWAVToMP3ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Rec|*.wav;*.mp3";

            if (Directory.Exists(PratiCantoForms.ClientFolder)) ofd.InitialDirectory = PratiCantoForms.ClientFolder;

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                string destFile = ofd.FileName.Replace(".wav", ".mp3");

                using (var reader = new MediaFoundationReader(ofd.FileName))
                {
                    MediaFoundationEncoder.EncodeToMp3(reader, destFile);
                }
            }
        }

        private void insertAtStartOfSelectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (curSample == null) return;

            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Rec|*.wav;*.mp3";

            if (Directory.Exists(PratiCantoForms.ClientFolder)) ofd.InitialDirectory = PratiCantoForms.ClientFolder;

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                SampleAudio audioAdd = new SampleAudio(ofd.FileName);
                List<SampleAudio> lstAudioAdd = new List<SampleAudio>();
                List<double> lstInsTime = new List<double>();
                lstAudioAdd.Add(audioAdd);
                lstInsTime.Add(saGL.p0SelX);

                curSample.ComposeAudioData(curSample, lstAudioAdd, lstInsTime, 0, 0);
                curSample.RecomputeSpectre();

                setGLDrawToCurSample();
            }
        }


        #region Deep learning models
        TFGraph graph = null;
        string[] model_classes = null;
        private const float CUTOFF_CONFIDENCE = 0.99f;

        private void loadModelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                string modelFile = Application.StartupPath + "\\DLModels\\attRNN.pb";
                graph = new TFGraph();
                // Load the serialized GraphDef from a file.
                byte[] model = File.ReadAllBytes(modelFile);
                model_classes = File.ReadAllText(modelFile + ".classes").Split(new string[] { "\r\n", "\n" },
                    StringSplitOptions.RemoveEmptyEntries);

                graph.Import(model, "");

                spotkeywordsInAudioToolStripMenuItem.Enabled = true;
                spotKeywordInSelectionToolStripMenuItem.Enabled = true;

                loadModelToolStripMenuItem.Visible = false;
            }
            catch
            {
                //silently fail, user does not need to know this is happening
            }
        }

        /// <summary>Runs the tensrflow model in an audio sample.
        /// Returns selected class.</summary>
        /// <param name="audiosample">Sample to use</param>
        /// <param name="confidence">Output confidence</param>
        /// <returns></returns>
        private int RunModel(List<double> audiosample, out float confidence)
        {
            float[,] x = new float[1, audiosample.Count];

            for (int k = 0; k < audiosample.Count; k++) x[0, k] = (float)audiosample[k];

            confidence = 0;
            int maxIdx = -1;
            using (var session = new TFSession(graph))
            {
                TFTensor tensor = new TFTensor(x);
                var runner = session.GetRunner();

                runner.AddInput(graph["input"][0], tensor).Fetch(graph["output/Softmax"][0]);
                var output = runner.Run();

                var result = output[0];
                float[,] val = (float[,])result.GetValue(jagged: false);

                for (int k = 0; k < val.GetLength(1); k++)
                {
                    if (val[0, k] > confidence)
                    {
                        maxIdx = k;
                        confidence = val[0, k];
                    }
                }
            }
            return maxIdx;
        }

        private void SpotKeywordInSelectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // no graph
            if (graph == null) return;
            // no selection or too short
            if (saGL.pfSelX - saGL.p0SelX < 0.7)
            {
                //add annotation
                txtAnnotation.Text = "?";
                btnAnnotate_Click(sender, e);
                return;
            }

            //resample to 16kHz
            List<double> dblx = curSample.Resample(saGL.p0SelX, saGL.pfSelX, 16000);

            int sel_class_id = RunModel(dblx, out float conf);
            if (sel_class_id >= 0)
            {
                string sel_class = model_classes[sel_class_id];

                //add annotation
                txtAnnotation.Text = sel_class + ": " + Math.Round(conf, 2).ToString();
                btnAnnotate_Click(sender, e);
            }
        }

        private void SpotkeywordsInAudioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (curSample == null) return;
            //resample all audio to 16kHz
            List<double> dblx = curSample.Resample(0, curSample.time[curSample.time.Length - 1], 16000);

            //Find a nice cutoff point for intensity
            List<float> lstIntens = new List<float>();
            foreach (double d in dblx) lstIntens.Add( (float)Math.Abs(d) );
            lstIntens.Sort();
            float cutoff = lstIntens[(int)(0.15 * lstIntens.Count)];
            for (int k = 0; k < lstIntens.Count; k++)
            {
                if (lstIntens[k] < cutoff )
                {
                    lstIntens.RemoveAt(k);
                    k--;
                }
            }
            if (lstIntens.Count < 5) return;

            float audioavg = SampleAudio.RealTime.getMean(lstIntens.ToArray(), out float audiostd);
            
            //steps of 0.1 s -> skip 1600
            int step = 1600;

            //spans a 1s window each time
            int duration = 16000;

            List<int> detected_classes = new List<int>();
            List<float> detected_confidences = new List<float>();

            for (int k = 0; k < dblx.Count - duration; k += step)
            {
                float tt = (float)k / 16000;

                List<double> analyze_sample = new List<double>();
                float cur_avg = 0;
                for (int i = k; i < k + duration; i++)
                {
                    analyze_sample.Add(dblx[i]);
                    cur_avg += (float)Math.Abs(dblx[i]);
                }
                cur_avg /= (float)duration;

                if (cur_avg > audioavg - 0.25 * audiostd)
                {
                    //display feedback
                    saGL.SelectRegion(tt, tt + 1.0f);
                    picControlBar.Invalidate();
                    Application.DoEvents();

                    int c = RunModel(analyze_sample, out float confidence);

                    detected_classes.Add(c);
                    detected_confidences.Add(confidence);

                    if (confidence > CUTOFF_CONFIDENCE)
                    {
                        // skips q steps if confidence is high
                        for (int q = 0; q < 3; q++)
                        {
                            k += step;
                            detected_classes.Add(c);
                            detected_confidences.Add(confidence);
                        }
                    }
                }
                else
                {
                    //discarded by intensity
                    detected_classes.Add(0);
                    detected_confidences.Add(0);
                }
            }
            saGL.SelectRegion(0, 0);


            //max suppression
            float thresh = CUTOFF_CONFIDENCE;
            for (int k = 0; k < detected_classes.Count; k++)
            {
                if (detected_confidences[k] >= thresh)
                {
                    //start tracking
                    int track_id = detected_classes[k];

                    int t0 = k;
                    int search_idx = k + 1;
                    while (search_idx < detected_classes.Count &&
                        detected_classes[search_idx] == track_id &&
                        detected_confidences[search_idx] >= thresh)
                    {
                        search_idx++;
                    }

                    int tf = search_idx;

                    // dont update with unknown
                    if (detected_classes[k] > 0)
                    {
                        string sel_class = model_classes[detected_classes[k]];
                        txtAnnotation.Text = sel_class; // + ": " + Math.Round(conf, 2).ToString();

                        // in theory, sound interval is
                        // [tf, t0 + duration]
                        float f_t0 = (float)tf * step / 16000f - 0.38f;
                        float f_tf = ((float)t0 * step + duration) / 16000f + 0.23f;

                        if (f_tf - f_t0 < 0.1) f_tf = f_t0 + 0.1f;
                        saGL.SelectRegion(f_t0, f_tf);

                        btnAnnotate_Click(sender, e);
                    }

                    // update k
                    k = search_idx;
                }
            }
        }
        #endregion


    }
}
