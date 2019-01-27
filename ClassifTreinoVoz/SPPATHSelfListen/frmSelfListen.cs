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

namespace PratiCanto.SPPATHSelfListen
{
    public partial class frmSelfListen : Form
    {
        public frmSelfListen()
        {
            InitializeComponent();
        }
        private void btnClient_Click(object sender, EventArgs e)
        {
            PratiCantoForms.ShowClientForm();
        }

        float sampleTime = 1.0f / 44100.0f;
        /// <summary>Total replay time to preallocate</summary>
        float timeToAllocate = 60 * 5;

        private void frmSelfListen_Load(object sender, EventArgs e)
        {
            //List mics
            List<string> mics = SampleAudio.RealTime.GetMicrophones();
            foreach (string s in mics) cmbInputDevice.Items.Add(s);
            if (mics.Count <= 1) cmbInputDevice.Visible = false;
            if (mics.Count >= 1) cmbInputDevice.SelectedIndex = 0;

            //allocate desired total time
            int nAllocate = (int)(timeToAllocate / sampleTime);
            float[] vTime = new float[nAllocate];
            float[] vIntens = new float[nAllocate];
            for (int k = 0; k < nAllocate; k++) vTime[k] = k * sampleTime;

            //feedback audio
            saFeedback = new SampleAudio(vTime, vIntens, new WaveFormat(44100, 1));
            
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


        #region Record and feedback sound
        //SampleAudioGL saGL;
        SampleAudio.RealTime rt;
        bool recording = false;
        //SampleAudio curSample;
        string recFileName;
        private void btnRec_Click(object sender, EventArgs e)
        {

            if (!recording)
            {
                string UserFolder;
                if (Directory.Exists(PratiCantoForms.ClientFolder)) UserFolder = PratiCantoForms.ClientFolder;
                else UserFolder = Application.StartupPath + "\\" + PratiCantoForms.ClientFolder;

                if (!Directory.Exists(UserFolder)) Directory.CreateDirectory(UserFolder);

                string recFolder = PratiCantoForms.getRecFolder();
                if (!Directory.Exists(recFolder)) Directory.CreateDirectory(recFolder);

                recFileName = recFolder + "\\SustainedVowel" + DateTime.Now.Hour.ToString() + "h" + DateTime.Now.Minute.ToString().PadLeft(2, '0') + "min" + DateTime.Now.Second.ToString().PadLeft(2, '0') + "s.wav";
                //saGL.ViewFormants = false;
                rt = new SampleAudio.RealTime();
                rt.AudioBufferSizeInMilliseconds = 20;
                
                rt.OnDataReceived = SoundReceived;
                rt.StartRecording(cmbInputDevice.SelectedIndex);
                int bufferSize = rt.waveSource.BufferMilliseconds;

                resetVoiceStatistics();
                btnRec.Image = picStop.Image;

                //feedback audio
                lastIdx = 0;
                PlayStopFeedback();

                recording = true;

            }
            else
            {
                //feedback audio
                PlayStopFeedback();

                recording = false;
                rt.StopRecording();

                //saGL.ResetRealTimeDrawing();

                //if (curSample != null) curSample.Dispose();
                //curSample = new SampleAudio(rt);

                //saGL.SetSampleAudio(curSample);
                btnRec.Image = picRec.Image;


            }
        }

        SampleAudio saFeedback;
        /// <summary>Last index copied from realtime audio</summary>
        int lastIdx = 0;
        int curIdx, minDelay;

        double prevFreq = 0;
        double curPhase = 0;

        private void SoundReceived()
        {
            if (lastIdx == 0)
            {
                curIdx = (int)(ps.t / sampleTime);
                minDelay = rt.audioReceived.Count;
            }

            //computes reference frequency
            double curFreq = (double)numSineFreq.Value;
            if (prevFreq == 0) prevFreq = curFreq;
            if (prevFreq != curFreq)
            {
                curPhase += 2.0 * Math.PI * (prevFreq - curFreq) * (lastIdx / 44100.0);
                prevFreq = curFreq;
            }


            for (int k = lastIdx; k < rt.audioReceived.Count; k++)
            {
                //sets delay
                int desiredDelay = (int)(0.001f * ((float)(numAudioDelay.Value)) / sampleTime);
                if (desiredDelay > curIdx + minDelay) desiredDelay -= curIdx + minDelay;
                else desiredDelay = 0;

                float refFreqAdd = (float)(Math.Sin(curPhase + 2.0 * Math.PI * curFreq * (k / 44100.0)) * 2000 * (double)numSineAmp.Value);
                //float refFreqAdd = (float)(Math.Sin(2.0 * Math.PI * (double)numSineFreq.Value * (sw.Elapsed.TotalSeconds)) * 2000 * (double)numSineAmp.Value);

                //adjusts feedback intensity
                saFeedback.audioData[curIdx + k + minDelay + desiredDelay] = refFreqAdd + rt.audioReceived[k] * (float)numFeedbackAmplif.Value;
            }
            //rt.OnDataReceived 
            //saFeedback.audioData[

            lastIdx = rt.audioReceived.Count - 1;

            //saGL.DrawRealTime(rt);
        }
        #endregion


        #region Replay audio
        bool replaying = false;
        SampleAudio.PlaySample ps;
        WaveOut wo;
        /// <summary>Starts/stops feedback replay</summary>
        void PlayStopFeedback()
        {
            if (saFeedback == null) return;

            if (!replaying)
            {
                //Play
                //saGL.DrawCurrentPlaybackTime = true;

                //double t0Play = saGL.p0SelX;
                //double tfPlay = saGL.pfSelX;
                //if (t0Play == tfPlay)
                //{
                float t0Play = 0;
                float tfPlay = saFeedback.time[saFeedback.time.Length - 1];
                //}

                ps = new SampleAudio.PlaySample(saFeedback, t0Play, tfPlay);
                ps.ReplaySpeed = 1;
                //ps.PlayTimeFunc = DrawTime;

                wo = new WaveOut();
                wo.DesiredLatency = 65;

                wo.PlaybackStopped += new EventHandler<StoppedEventArgs>(wo_PlaybackStopped);
                wo.Init(ps);

                wo.Play();

                //btnPlay.Image = picStop.Image;
                replaying = true;
            }
            else
            {
                wo.Stop();
                wo_PlaybackStopped(this, new StoppedEventArgs());
            }

        }

        void wo_PlaybackStopped(object sender, StoppedEventArgs e)
        {
            //if (saGL != null) saGL.DrawCurrentPlaybackTime = false;
            //progPlay.Value = 100;
            //progPlay.Visible = false;
            //btnPlay.Image = picPlay.Image;
            replaying = false;

            if (wo != null) wo.Dispose();
        }

        #endregion

        #region Voice parameters control

        /// <summary>Current f0</summary>
        float lastf = 0, curf = 0;
        /// <summary>Current intensity</summary>
        float lastI = 0, curI = 0;

        /// <summary>Extreme values</summary>
        float minFreq, maxFreq, minIntens, maxIntens; // minSpeed, maxSpeed;
        /// <summary>Average values</summary>
        double avgFreq, avgIntens; //, avgSpeed;
        /// <summary>Number of samples</summary>
        int nSamples = 0;

        void resetVoiceStatistics()
        {
            minFreq = float.MaxValue;
            minIntens = float.MaxValue;
            //minSpeed = float.MaxValue;

            maxFreq = float.MinValue;
            maxIntens = float.MinValue;
            //maxSpeed = float.MinValue;

            avgFreq = 0; avgIntens = 0; //avgSpeed = 0;
            nSamples = 0;

            lastf = 0; lastI = 0;
        }

        private void timerSample_Tick(object sender, EventArgs e)
        {
            if (!recording) return;

//retrieve relevant sound data
            int nElems = SampleAudio.FFTSamples;//2 * (int)(1.1 / (rtSingRecord.deltaT * 50));

            //avoid error
            int id = rt.audioReceived.Count - 1;

            if (rt.audioReceived.Count > nElems)
            {
                float[] latestData = new float[nElems];

                for (int k = 0; k < nElems; k++) if (id - k >= 0) latestData[k] = rt.audioReceived[id - k];

                float stepFreq = (float)rt.waveSource.WaveFormat.SampleRate / (float)SampleAudio.FFTSamples;

                List<LinearPredictor.Formant> formants;

                try
                {
                    float hnf;
                    double[] refFFT = null;
                    curf = LinearPredictor.ComputeReinforcedFrequencies(latestData, stepFreq, latestData.Length - 1, 70, 1250, out formants, refFFT, out hnf);
                    curI = (float)SampleAudio.RealTime.ComputeIntensity(latestData, rt.deltaT, latestData.Length - 1, 10); //50);
                }
                catch
                {
                }

                if (curf > 0)
                {
                    float w = 0.99f;
                    if (lastf == 0 && lastI == 0) w = 0;
                    curf = w * lastf + (1-w) * curf;
                    curI = w * lastI + (1-w) * curI;
                    lastf = curf;
                    lastI = curI;

                    minFreq = Math.Min(curf, minFreq);
                    if (minFreq < 70) minFreq = 70;
                    maxFreq = Math.Max(curf, maxFreq);
                    minIntens = Math.Min(curI, minIntens);
                    maxIntens = Math.Max(curI, maxIntens);

                    if (curf > 0)
                    {
                        nSamples++;
                        avgFreq = (avgFreq * (nSamples - 1) + curf) / (double)nSamples;
                        avgIntens = (avgIntens * (nSamples - 1) + curI) / (double)nSamples;
                    }
                }
                

                lblFreq.Text = Math.Round(curf, 1).ToString();
                lblIntens.Text = Math.Round(curI, 1).ToString();

                picIntens.Invalidate();
                picFreq.Invalidate();
            }

            //Automatic control
            if (chkAutoControlParams.Checked)
            {
                if (chkVoiceIntensity.Checked)
                {
                    float errorIntens = curI - (float)numTargetIntensity.Value;

                    float curFeedbackAmp = (float)numFeedbackAmplif.Value;

                    //deadzone at 2dBr, ignore if amplitude is too low
                    if (Math.Abs(errorIntens) > 2 && errorIntens > -10)
                    {
                        try
                        {
                            if (errorIntens > 0) numFeedbackAmplif.Value = (decimal)(curFeedbackAmp * 1.005f);
                            else numFeedbackAmplif.Value = (decimal)(curFeedbackAmp * 0.995f);
                        }
                        catch { }
                    }
                    if (numFeedbackAmplif.Value < (decimal)0.1) numFeedbackAmplif.Value = (decimal)0.1;
                }
                //else
                //{
                //    if (numFeedbackAmplif.Value != 1) numFeedbackAmplif.Value = 1;
                //}

                if (chkChangeFreq.Checked)
                {
                    float targetFreq = (float)numTargetFreq.Value;
                    float errorFreq = curf - targetFreq;
                     
                    float curRefSineFreq = (float)numSineFreq.Value;

                    if (numSineAmp.Value == 0) numSineAmp.Value = 1;
                    if (curf > 70)
                    {
                        curRefSineFreq = curf + Math.Max(Math.Min(9, targetFreq - curf), -9);

                        if ((decimal)curRefSineFreq > numSineFreq.Minimum && (decimal)curRefSineFreq < numSineFreq.Maximum)
                            numSineFreq.Value = (decimal)curRefSineFreq;
                    }
                }
                //else
                //{
                //    if (numSineAmp.Value != 0) numSineAmp.Value = 0;
                //}
            }
        }

        Font f = new Font("Arial", 8, FontStyle.Bold);
        private void drawGauge(Graphics g, float min, float max, float val, double avgval, bool drawTarget, float targetVal, int W, int H)
        {
            if (min == float.MinValue || max == float.MaxValue || min == max) return;
            g.FillRectangle(Brushes.White, 0, 0, W, H);
            float wFill = (val - min) / (max - min) * W;
            g.FillRectangle(Brushes.Green, 0, 0, wFill, H);

            float wAvg = (float)(avgval - min) / (max - min) * W;
            g.FillRectangle(Brushes.LightBlue, wAvg - 3, 0, 6, W);

            if (drawTarget)
            {
                float wTarget = (targetVal - min) / (max - min) * W;
                g.FillRectangle(Brushes.Red, wTarget - 3, 0, 6, W);
            }

            g.DrawString(Math.Round(min, 2).ToString(), f, Brushes.Black, 0, 0);
            SizeF s = g.MeasureString(Math.Round(max, 2).ToString(), f);
            g.DrawString(Math.Round(max, 2).ToString(), f, Brushes.Black, W - s.Width, 0);
        }

        private void picIntens_Paint(object sender, PaintEventArgs e)
        {
            drawGauge(e.Graphics, minIntens, maxIntens, curI, avgIntens, chkVoiceIntensity.Checked, (float)numTargetIntensity.Value, picIntens.Width, picIntens.Height);

        }

        private void picFreq_Paint(object sender, PaintEventArgs e)
        {
            drawGauge(e.Graphics, minFreq, maxFreq, curf,avgFreq,chkChangeFreq.Checked,(float)numTargetFreq.Value, picIntens.Width, picIntens.Height);
        }

        private void chkVoiceIntensity_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (recording) numTargetIntensity.Value = (decimal)(avgIntens - 6);
            }
            catch { }
        }

        #endregion








    }
}
