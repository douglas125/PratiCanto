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

namespace PratiCanto.SPPATHSustainedVowel
{
    public partial class frmSustVowel : Form
    {
        public frmSustVowel()
        {
            InitializeComponent();
        }

        private void frmSustVowel_Load(object sender, EventArgs e)
        {
            #region Cosmetics
            btnAnalyze.FlatStyle = FlatStyle.Flat;
            btnAnalyze.FlatAppearance.BorderSize = 0;
            btnAnalyze.Image = PratiCanto.LayoutFuncs.MakeDegrade(Color.White, Color.FromArgb(223, 223, 223), btnAnalyze.Width, btnAnalyze.Height, true);
            PratiCanto.LayoutFuncs.AnimateEnterLeaveBorder(btnAnalyze);

            btnClient.FlatStyle = FlatStyle.Flat;
            btnClient.FlatAppearance.BorderSize = 0;

            btnStartTest.FlatStyle = FlatStyle.Flat;
            btnStartTest.FlatAppearance.BorderSize = 0;

            btnReplay.FlatStyle = FlatStyle.Flat;
            btnReplay.FlatAppearance.BorderSize = 0;
            
            //PratiCanto.LayoutFuncs.AnimateEnterLeaveBgColor(btnReplay);
            btnSave.FlatStyle = FlatStyle.Flat;
            btnSave.FlatAppearance.BorderSize = 0;
            btnOpen.FlatStyle = FlatStyle.Flat;
            btnOpen.FlatAppearance.BorderSize = 0;

            #endregion

            AudioComparer.SoftwareKey.CheckLicense("SustainedVowel", false);


            //add dummy jitter/shimmer/hne
            jitterEvol.Add(0); jitterEvol.Add(0);
            shimmEvol.Add(0); shimmEvol.Add(0);
            hnrEvol.Add(100); hnrEvol.Add(100);


            //List mics
            List<string> mics = SampleAudio.RealTime.GetMicrophones();
            foreach (string s in mics) cmbInputDevice.Items.Add(s);
            if (mics.Count <= 1) cmbInputDevice.Visible = false;
            if (mics.Count >= 1) cmbInputDevice.SelectedIndex = 0;

            //init GL
            saGL = new SampleAudioGL(GLPicIntens, glPicSpectre, picControlBar, lblSpecInfo, txtAnnotation);
            saGL.IncreaseSpectreHeight();

            shrinkCenterReg = (float)numAmplif.Value;
        }


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

        #region Record
        SampleAudioGL saGL;
        SampleAudio.RealTime rt;
        bool recording = false;
        SampleAudio curSample;
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
                saGL.ViewFormants = false;
                rt = new SampleAudio.RealTime();
                rt.OnDataReceived = SoundReceived;
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

                AnalyzeCurSample();

            }
        }

        private void AnalyzeCurSample()
        {
            //compute formants and everything
            curSample.ComputeBaseFreqAndFormants(0, curSample.time[curSample.time.Length - 1], txtDPenalty.Text, txtChangePenalty.Text, txtNFreqs.Text, txtVarPenalty.Text, true);

            //find threshold intensity
            float cutIntens = 0.5f * curSample.baseFreqsIntensity.Max();
            float p0sel = 0, pfsel = 0;
            for (int k = 0; k < curSample.baseFreqsTimes.Count; k++)
            {
                if (curSample.baseFreqsIntensity[k] > cutIntens && curSample.baseFreqsF0[k] > 0)
                {
                    p0sel = curSample.baseFreqsTimes[k];
                    break;
                }
            }
            for (int k = curSample.baseFreqsTimes.Count - 1; k >= 0; k--)
            {
                if (curSample.baseFreqsIntensity[k] > cutIntens && curSample.baseFreqsF0[k] > 0)
                {
                    pfsel = curSample.baseFreqsTimes[k];
                    break;
                }
            }
            saGL.SelectRegion(p0sel, pfsel);

            saGL.ViewFormants = true;
            saGL.UpdateDrawing();

            ComputeRegionInfo();
        }

        private void SoundReceived()
        {
            saGL.DrawRealTime(rt);
        }
        #endregion

        #region Replay audio
        bool replaying = false;
        WaveOut wo;
        private void btnPlay_Click(object sender, EventArgs e)
        {
            if (curSample == null) return;

            if (!replaying)
            {
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
                ps.ReplaySpeed = 1;
                ps.PlayTimeFunc = DrawTime;

                wo = new WaveOut();
                wo.PlaybackStopped += new EventHandler<StoppedEventArgs>(wo_PlaybackStopped);
                wo.Init(ps);

                wo.Play();

                btnPlay.Image = picStop.Image;
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
            if (saGL != null) saGL.DrawCurrentPlaybackTime = false;
            //progPlay.Value = 100;
            //progPlay.Visible = false;
            btnPlay.Image = picPlay.Image;
            replaying = false;

            if (wo != null) wo.Dispose();
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
                lastUpdt = sw.Elapsed.TotalSeconds;
                if (saGL != null && wo != null) saGL.DrawCurPbTime((float)time, true);
            }
            else
            {
            }
        }

        
        #endregion


        #region Region information


        private void btnComputeRegionInfo_Click(object sender, EventArgs e)
        {
            ComputeRegionInfo();
        }

        /// <summary>List of fundamental frequency and formants</summary>
        List<List<float>> FN = new List<List<float>>();

        private void ComputeRegionInfo()
        {
            if (saGL.sampleAudio == null) return;
            //delta time in spectres
            float deltaT = saGL.sampleAudio.baseFreqsTimes[1];

            int idx0 = (int)(saGL.p0SelX / deltaT);
            int idxF = (int)(saGL.pfSelX / deltaT);

            if (idxF > saGL.sampleAudio.baseFormants.Count - 2) idxF = saGL.sampleAudio.baseFormants.Count - 2;

            //force new computation
            for (int k = idx0; k <= idxF; k++) saGL.sampleAudio.baseFormants[k] = new List<LinearPredictor.Formant>();
            saGL.sampleAudio.ComputeBaseFreqAndFormants(idx0 * deltaT, idxF * deltaT, "0", "3e-3", "6", "0", false);

            List<float> localSampleFreqs = new List<float>();
            List<float> localSampleIntens = new List<float>();
            List<float> localSampleHNR = new List<float>();
            for (int k = idx0; k <= idxF; k++)
            {
                localSampleFreqs.Add(saGL.sampleAudio.baseFreqsF0[k]);
                localSampleIntens.Add(saGL.sampleAudio.baseFreqsIntensity[k]);
                localSampleHNR.Add(saGL.sampleAudio.baseHNR[k]);
            }

            FN.Add(localSampleFreqs);

            //Mean/median frequency
            List<float> meanMedianFreq = new List<float>();
            foreach (float val in localSampleFreqs) meanMedianFreq.Add(val);

            string freqInfo = "\r\nFundamental frequency\r\n";
            float medianF0 = 0;
            if (meanMedianFreq.Count > 0)
            {
                meanMedianFreq.Sort();
                medianF0 = meanMedianFreq[meanMedianFreq.Count >> 1];
                if (medianF0 > 0)
                {
                    //removes from jitter computation frequencies that deviate too much from median
                    for (int k = 0; k < localSampleFreqs.Count; k++)
                    {
                        //According to Prof. Savio, jitter fluctuation is absolute in Hz, order of 10Hz. Assume 20Hz for safety
                        if (Math.Abs(localSampleFreqs[k] - medianF0) > 20)
                        {
                            localSampleFreqs.RemoveAt(k);
                            k--;
                        }
                    }

                    float meanF0 = localSampleFreqs.Sum() / (float)localSampleFreqs.Count;
                    freqInfo += "F0Mean: " + meanF0.ToString() + " Hz; " + "F0Median: " + medianF0.ToString() + " Hz";
                }
            }

            //jitter and shimmer
            float[] jitter = SampleAudio.RealTime.JitterShimmer.ComputeJitter(localSampleFreqs.ToArray());
            float[] shimmer = SampleAudio.RealTime.JitterShimmer.ComputeShimmer(localSampleIntens.ToArray());

            //compute jitter/shimmer/HNR sequence
            int nPts = (int)numEvolPts.Value;
            jitterEvol = new List<float>();
            shimmEvol = new List<float>();
            hnrEvol = new List<float>();
            for (int k = 1; k <= nPts; k++)
            {
                float kk0 = (float)(k - 1) / (float)nPts;
                float kkF = (float)k / (float)nPts;

                //jitter has some deletions
                int idJit0 = (int)(kk0 * localSampleFreqs.Count);
                int idJitF = (int)(kkF * localSampleFreqs.Count);

                if (idJitF - idJit0 <= 1)
                    break; //cannot compute evolution with so few samples

                //shimmer and hnr share
                int idShimHnr0 = (int)(kk0 * localSampleIntens.Count);
                int idShimHnrF = (int)(kkF * localSampleIntens.Count);

                List<float> partialIntensLst = new List<float>();
                List<float> partialFreqLst = new List<float>();
                List<float> partialHNRLst = new List<float>();
                for (int i = idJit0; i < idJitF; i++) partialFreqLst.Add(localSampleFreqs[i]);
                for (int i = idShimHnr0; i < idShimHnrF; i++)
                {
                    partialIntensLst.Add(localSampleIntens[i]);
                    partialHNRLst.Add(localSampleHNR[i]);
                }

                float[] jitterPart = SampleAudio.RealTime.JitterShimmer.ComputeJitter(partialFreqLst.ToArray());
                float[] shimPart = SampleAudio.RealTime.JitterShimmer.ComputeShimmer(partialIntensLst.ToArray());
                float hnrPart = partialHNRLst.Sum() / (float)partialHNRLst.Count;

                jitterEvol.Add(jitterPart[0]);
                shimmEvol.Add(shimPart[0]);
                hnrEvol.Add(hnrPart);
            }
            picVoiceQualityEvol.Invalidate();
            picVoiceParams.Invalidate();

            string jitString = "\r\nJitter / shimmer\r\n";
            if (medianF0 > 0)
            {
                jitString += "Jitta: " + Math.Round(jitter[0], 3).ToString() + " ms; ";
                jitString += "Jitt: " + Math.Round(jitter[1], 3).ToString() + "%; ";
                jitString += "Rap: " + Math.Round(jitter[2], 3).ToString() + "%; ";
                jitString += "ppq5: " + Math.Round(jitter[3], 3).ToString() + "%; ";
                jitString += " (Ref: < 83.2ms, < 1.04%, < 0.68%, < 0.84%)";
            }

            //Color jitColor = Color.Black;
            //if (jitter[0] > 83.2 || jitter[1] > 1.04 || jitter[2] > 0.68 || jitter[3] > 0.84) jitColor = Color.Red;
            //if (!float.IsNaN(jitter[1])) e.Graphics.DrawString(jitString, ff, new SolidBrush(jitColor), 0, h0); h0 += 13;


            string shimmerString = "ShdB: " + Math.Round(shimmer[0], 3).ToString() + " dB; ";
            shimmerString += "Shim: " + Math.Round(shimmer[1], 3).ToString() + "%; ";
            shimmerString += " (Ref: < 0.35dB, 3.81%)";

            ////shimmerString += "apq3: " + Math.Round(shimmer[2], 3).ToString() + "%; ";
            ////shimmerString += "apq5: " + Math.Round(shimmer[3], 3).ToString() + "%; ";
            ////shimmerString += "apq11: " + Math.Round(shimmer[4], 3).ToString() + "%; ";
            //Color shimmerColor = Color.Black;
            //if (shimmer[0] > 0.35 || shimmer[1] > 3.81/* || shimmer[4] > 3.07*/) shimmerColor = Color.Red;
            //if (!float.IsNaN(shimmer[1])) e.Graphics.DrawString(shimmerString, ff, new SolidBrush(shimmerColor), 0, h0); h0 += 13;

            //Retrieve formant information
            string formantInfo = "\r\nFormants\r\nFormant medians (Hz): ";
            saGL.sampleAudio.ComputeBaseFreqAndFormants(idx0 * deltaT, idxF * deltaT, "0", "3e-3", "6", "0", true);

            //currently computing 6 Formants
            for (int iF = 1; iF <= 6; iF++)
            {
                List<float> formantList = new List<float>();
                List<float> lstToSort = new List<float>();
                for (int k = idx0; k <= idxF; k++)
                {
                    float f = 0;
                    if (saGL.sampleAudio.baseFormants[k].Count > iF) f = (float)saGL.sampleAudio.baseFormants[k][iF].Frequency;
                    formantList.Add(f);
                    lstToSort.Add(f);
                }
                FN.Add(formantList);

                lstToSort.Sort();
                float medianFormant = lstToSort[lstToSort.Count >> 1];
                formantInfo += " " + iF.ToString() + ":" + Math.Round(medianFormant, 1).ToString();
            }

            //Harmonic to noise ratio
            float stepFreq = (float)saGL.sampleAudio.waveFormat.SampleRate / (float)SampleAudio.FFTSamples;
            float harmonicIntens = 0, nonHarmonicIntens = 0;
            for (int k = idx0; k <= idxF; k++)
            {
                float[] harmonicNoise = ClassifTreinoVoz.PhonemeRecognition.HarmonicNoiseIntensities(saGL.sampleAudio.audioSpectre[k], saGL.sampleAudio.baseFreqsF0[k], stepFreq, true);
                harmonicIntens += harmonicNoise[0];
                nonHarmonicIntens += harmonicNoise[1];
            }

            /*Revista Brasileira de Otorrinolaringologia
            Print version ISSN 0034-7299
            Rev. Bras. Otorrinolaringol. vol.72 no.5 São Paulo Sept./Oct. 2006
            http://dx.doi.org/10.1590/S0034-72992006000500013 

            Standardization of acoustic measures for normal voice patterns
            Ana Clara Naufel de FelippeI; Maria Helena Marotti Martelletti GrilloII; Thaís Helena GrechiIII*/

            double htn = (20.0 * Math.Log(harmonicIntens / nonHarmonicIntens, 10));
            double htt = (harmonicIntens / (harmonicIntens + nonHarmonicIntens));

            string txtHarmonicNoise = "";
            if (medianF0 > 0) txtHarmonicNoise = "Harmonic-to-noise: " + Math.Round(htn, 4).ToString() + "dB (ref > 9.56 dB):"; //; Harmonic-to-total: " + Math.Round(100.0 * htt, 4).ToString() + "%";

            //Display information
            cmbNumList.SelectedIndex = 0;

            //Phonation time

            /*Reference:
             https://hub.hku.hk/bitstream/10722/55510/1/ft.pdf
             * Li, T. C. [李紫菊]. (2007). Effect of visual feedback on maximum
phonation time. (Thesis). University of Hong Kong, Pokfulam,
Hong Kong SAR.

             */
            double phonTime = saGL.pfSelX - saGL.p0SelX;
            string phonTimeInfo = "\r\nPhonation Time: " + Math.Round(phonTime, 1) + " s (ref: > 9.23s)\r\n";


            string infoTxt = freqInfo;
            infoTxt += phonTimeInfo;
            infoTxt += "\r\n" + txtHarmonicNoise;
            infoTxt += "\r\n" + jitString;
            infoTxt += "\r\n" + shimmerString;
            infoTxt += "\r\n" + formantInfo;
            txtRegInfoDetails.Text = infoTxt;

            
        }



        List<float> jitterEvol = new List<float>();
        List<float> shimmEvol = new List<float>();
        List<float> hnrEvol = new List<float>();
        static Font fVoiceEvol = new Font("Arial", 12, FontStyle.Bold);


        #region Voice evolution drawing functions
        static float jitMax = 83.2f;
        static float shimMax = 0.35f;
        static float hnrMin = 9.56f; // * 0.5f;
        static float minMPT = 9.23f;

        /// <summary>Amplify graph by how much?</summary>
        static float shrinkCenterReg = 10;

        /// <summary>Draw jitter/shimmer/hnr - compared to reference values</summary>
        /// <param name="g">Graphics element</param>
        /// <param name="W">Width</param>
        /// <param name="H">Height</param>
        /// <param name="jitterEvol">Measured jitter</param>
        /// <param name="shimmerEvol">Measured shimmer</param>
        /// <param name="hnrEvol">Measured HNR</param>
        static void DrawJitterShimmerHNR(Graphics g, int W, int H, List<float> jitterEvol, List<float> shimmerEvol, List<float> hnrEvol, float phonationTime)
        {
            int curh = 10;
            //int N = 6;
            int wStart = 100; // 50 + W / N;// 90;
            int wEnd = W - 20; // (N - 1) * W / N; // W - 20;

            g.FillRectangle(new SolidBrush(Color.FromArgb(240,240,240)), 0, 0, W, H);


            DrawBar(g, "Jitter", wStart, wEnd, jitterEvol, jitMax, ref curh, Color.FromArgb(0, 128, 0), Color.OrangeRed, Color.FromArgb(0, 128, 0), Color.Red);
            DrawBar(g, "Shimmer", wStart, wEnd, shimmerEvol, shimMax, ref curh, Color.FromArgb(0, 128, 0), Color.OrangeRed, Color.FromArgb(0, 128, 0), Color.Red);
            DrawBar(g, "HNR", wStart, wEnd, hnrEvol, hnrMin, ref curh, Color.OrangeRed, Color.FromArgb(0, 128, 0), Color.Red, Color.FromArgb(0, 128, 0));

            List<float> phonTime = new List<float>();
            phonTime.Add(phonationTime);phonTime.Add(phonationTime);
            DrawBar(g, "PhonTime", wStart, wEnd, phonTime, minMPT, ref curh, Color.OrangeRed, Color.LightGreen, Color.Red, Color.Green);
        }
        private static void DrawBar(Graphics g, string param, int wStart, int wEnd, List<float> evol, float refValue, 
            ref int curh, Color cLow, Color cHigh, Color paramLow, Color paramHigh)
        {
            //Jitter - normal at 0.5
            //Compute the Average
            float avg = evol.Average();
            //Perform the Sum of (value-avg)^2
            float sum = evol.Sum(d => (d - avg) * (d - avg));
            //Put it all together
            float stdDev = (float)Math.Sqrt(sum / (evol.Count - 1));

            Color c = paramLow;
            if (avg - stdDev > refValue && avg + stdDev > refValue) c = paramHigh;
            else if (avg - stdDev <= refValue && avg + stdDev > refValue) c = Color.Yellow;
            float wParamMin = (avg - stdDev) / refValue * 0.5f * (wEnd - wStart) + wStart;
            float wParamMax = (avg + stdDev) / refValue * 0.5f * (wEnd - wStart) + wStart;

            wParamMin = Math.Min(Math.Max(wParamMin, wStart), wEnd);
            wParamMax = Math.Min(Math.Max(wParamMax, wStart), wEnd);
            if (wParamMax == wParamMin) wParamMax = wParamMin + 5;

            SizeF s = g.MeasureString(param, fVoiceEvol);
            g.FillEllipse(new SolidBrush(cLow), wStart - (s.Height * 0.5f), curh, s.Height, s.Height);
            g.FillEllipse(new SolidBrush(cHigh), wEnd - (s.Height * 0.5f), curh, s.Height, s.Height);
            g.FillRectangle(new SolidBrush(cLow), wStart, curh, wEnd - wStart, s.Height);
            g.FillRectangle(new SolidBrush(cHigh), 0.5f * (wEnd + wStart), curh, 0.5f * (wEnd - wStart), s.Height);
            g.FillRectangle(new SolidBrush(c), wParamMin, curh + 2, wParamMax - wParamMin, s.Height - 4);
            g.DrawRectangle(Pens.Black, wParamMin, curh + 2, wParamMax - wParamMin, s.Height - 4);

            g.DrawString(param, fVoiceEvol, new SolidBrush(c), 5, curh);
            curh += (int)s.Height + 10;
        }

        /// <summary>Draws voice evolution</summary>
        /// <param name="g">Graphics element</param>
        /// <param name="jitEvol">Evolution of jitter</param>
        /// <param name="shimEvol">Evolution of shimmer</param>
        /// <param name="hnrEvol">Evolution of HNR</param>
        /// <param name="W">Image width</param>
        /// <param name="H">Image height</param>
        private static void DrawVoiceEvolution(Graphics g, List<float> jitEvol, List<float> shimEvol, List<float> hnrEvol, int W, int H)
        {
            string strhnrMax = "HNR = " + hnrMin.ToString();
            string strshimJitMax = "Shimmer/Jitter " + shimMax.ToString() + " dB/" + jitMax.ToString() + " ms";

            //goes to 3x maximum jitter/shimmer and HNR
            float invScaleX = 3.0f / shrinkCenterReg * (float)W;
            float invScaleY = 3.0f / shrinkCenterReg * (float)H;

            //yellow region
            g.FillRectangle(Brushes.Yellow, 0, invScaleY, 2 * invScaleX, H);

            //green region
            g.FillRectangle(Brushes.LightGreen, 0, 2 * invScaleY, invScaleX, H);
            g.DrawString("Normal", new Font("Arial", 20, FontStyle.Bold), Brushes.DarkGreen, 0, 2 * invScaleY + 0.4f * (H - 2 * invScaleY));

            //axes
            Pen pline = new Pen(Brushes.Black, 5);
            g.DrawLine(pline, 0, 0, W, 0);
            g.DrawLine(pline, 0, 0, 0, H);

            //names
            g.DrawString(strhnrMax, fVoiceEvol, Brushes.Black, 0, invScaleY);
            g.DrawLine(Pens.Black, 0, invScaleY, W, invScaleY);
            g.DrawLine(pline, 0, H, 5, H - 15);

            g.DrawString(strshimJitMax, fVoiceEvol, Brushes.Black, invScaleX, 0);
            g.DrawLine(Pens.Black, invScaleX, 0, invScaleX, H);
            g.DrawLine(pline, W, 0, W - 15, 5);


            Font evolFont = new Font("Arial", 8, FontStyle.Bold);
            //plot evolution
            PointF[] evol = new PointF[jitEvol.Count];
            for (int k = 0; k < jitEvol.Count; k++)
            {
                float jitShimVal;
                if (!float.IsNaN(jitEvol[k])) jitShimVal = Math.Max(jitEvol[k] / jitMax, shimEvol[k] / shimMax);
                else jitShimVal = shimEvol[k] / shimMax;


                float hnrVal = hnrEvol[k] / hnrMin;
                if (hnrVal < 0) hnrVal = 0;
                evol[k] = new PointF(jitShimVal * invScaleX, hnrVal * invScaleY);

                Brush bb = Brushes.DarkGreen;
                if (jitShimVal > 2 || hnrVal < 1) bb = Brushes.Red;
                else if (jitShimVal > 1 || hnrVal < 2) bb = Brushes.Yellow;

                g.FillRectangle(Brushes.Black, evol[k].X - 7, evol[k].Y - 7, 15, 15);
                g.FillEllipse(bb, evol[k].X - 5, evol[k].Y - 5, 11, 11);


                SizeF size = g.MeasureString((k + 1).ToString(), evolFont);
                g.DrawString((k + 1).ToString(), evolFont, Brushes.Black, evol[k].X + 11, evol[k].Y - 0.5f * size.Height);
            }

            g.DrawLines(Pens.Black, evol);

        }

        /// <summary>Draws voice evolution</summary>
        /// <param name="g">Graphics element</param>
        /// <param name="jitEvol">Evolution of jitter</param>
        /// <param name="shimEvol">Evolution of shimmer</param>
        /// <param name="hnrEvol">Evolution of HNR</param>
        /// <param name="W">Image width</param>
        /// <param name="H">Image height</param>
        private static void DrawVoiceEvolution2(Graphics g, List<float> jitEvol, List<float> shimEvol, List<float> hnrEvol, int W, int H)
        {
            string strhnrMax = "HNR " + hnrMin.ToString();
            string strshimMax = "Shimmer " + shimMax.ToString();
            string strjitMax = "Jitter " + jitMax.ToString();

            PointF center = getXYCoord(ptTriangCoord(0, 0, float.PositiveInfinity), W, H);
            PointF ptHnr = getXYCoord(ptTriangCoord(0, 0, hnrMin), W, H);
            PointF ptShim = getXYCoord(ptTriangCoord(0, shimMax, float.PositiveInfinity), W, H);
            PointF ptJit = getXYCoord(ptTriangCoord(jitMax, 0, float.PositiveInfinity), W, H);

            //red region
            //g.FillRectangle(Brushes.OrangeRed, 0, 0, W, H);

            //yellow region
            PointF ptHnrGreen = getXYCoord(ptTriangCoord(0, 0, 0.5f * hnrMin), W, H);
            PointF ptShimGreen = getXYCoord(ptTriangCoord(0, 2.0f * shimMax, float.PositiveInfinity), W, H);
            PointF ptJitGreen = getXYCoord(ptTriangCoord(2.0f * jitMax, 0, float.PositiveInfinity), W, H);
            float radius = ptHnrGreen.X - center.X;
            g.FillEllipse(Brushes.Yellow, center.X - radius, center.Y - radius * H / W, 2 * radius, 2 * radius * H / W);

            //green region
            radius = ptHnr.X - center.X;
            g.FillEllipse(Brushes.LightGreen, center.X - radius, center.Y - radius * H / W, 2 * radius, 2 * radius * H / W);




            g.DrawString(strhnrMax, fVoiceEvol, Brushes.Black, ptHnr);
            g.DrawString(strshimMax, fVoiceEvol, Brushes.Black, ptShim);
            g.DrawString(strjitMax, fVoiceEvol, Brushes.Black, ptJit);
            int ptSize = 3;
            g.FillEllipse(Brushes.Black, center.X - ptSize, center.Y - ptSize, 2 * ptSize, 2 * ptSize);
            g.FillEllipse(Brushes.Black, ptHnr.X - ptSize, ptHnr.Y - ptSize, 2 * ptSize, 2 * ptSize);
            g.FillEllipse(Brushes.Black, ptShim.X - ptSize, ptShim.Y - ptSize, 2 * ptSize, 2 * ptSize);
            g.FillEllipse(Brushes.Black, ptJit.X - ptSize, ptJit.Y - ptSize, 2 * ptSize, 2 * ptSize);

            //plot evolution
            PointF[] evol = new PointF[jitEvol.Count];
            for (int k = 0; k < jitEvol.Count; k++)
            {
                float[] triCoord = ptTriangCoord(jitEvol[k], shimEvol[k], hnrEvol[k]);
                PointF pt = getXYCoord(triCoord, W, H);
                g.FillRectangle(Brushes.Black, pt.X - ptSize, pt.Y - ptSize, 2 * ptSize, 2 * ptSize);
                evol[k] = pt;
            }
            g.DrawLines(Pens.Black, evol);
        }

        private static float[] ptTriangCoord(float j, float s, float hnr)
        {
            //hnr -> 0deg
            //shim -> 120 deg
            //jit -> 240 deg

            j /= jitMax * shrinkCenterReg;
            s /= shimMax * shrinkCenterReg;

            if (hnr < 0.4f * hnrMin / shrinkCenterReg) hnr = 0.4f * hnrMin / shrinkCenterReg;
            hnr = (hnrMin / hnr) / shrinkCenterReg;
            float rho = Math.Max(j, Math.Max(s, hnr));
            float theta = 0;
            if (hnr >= j && s >= j) theta = (0 * hnr + 120 * s) / (hnr + s);
            else if (s >= hnr && j >= hnr) theta = (240 * j + 120 * s) / (j + s);
            else theta = (360 * hnr + 240 * j) / (hnr + j);

            if (rho == 0) theta = 0;
            return new float[] { rho, (float)(theta * Math.PI / 180.0f) };
        }

        /// <summary>Retrieve XY from polar coords {rho, theta}</summary>
        private static PointF getXYCoord(float[] polar, int W, int H)
        {
            float xCoord = (float)(W >> 1) + polar[0] * (float)Math.Cos(polar[1]) * W;
            float yCoord = (float)(H >> 1) + polar[0] * (float)Math.Sin(polar[1]) * H;
            return new PointF(xCoord, yCoord);
        }

        private void numAmplif_ValueChanged(object sender, EventArgs e)
        {
            shrinkCenterReg = (float)numAmplif.Value;
            picVoiceQualityEvol.Invalidate();
        }
        private void btnAnalyze_Click(object sender, EventArgs e)
        {
            ComputeRegionInfo();
        }
        private void numEvolPts_ValueChanged(object sender, EventArgs e)
        {
            ComputeRegionInfo();
        }
        private void cmbNumList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (FN.Count == 0 || cmbNumList.SelectedIndex < 0) return;

            lstNumInfo.Items.Clear();
            foreach (float f in FN[cmbNumList.SelectedIndex]) lstNumInfo.Items.Add(Math.Round(f, 2).ToString());
        }

        private void picVoiceQualityEvol_Paint(object sender, PaintEventArgs e)
        {
            if (jitterEvol.Count == 0) return;
            DrawVoiceEvolution(e.Graphics, jitterEvol, shimmEvol, hnrEvol, picVoiceQualityEvol.Width, picVoiceQualityEvol.Height);
        }
        private void picVoiceParams_Paint(object sender, PaintEventArgs e)
        {
            if (jitterEvol.Count == 0) return;
            DrawJitterShimmerHNR(e.Graphics, picVoiceParams.Width, picVoiceParams.Height, jitterEvol, shimmEvol, hnrEvol, (float)(saGL.pfSelX-saGL.p0SelX));
        }
        #endregion

        private void btnSaveRecord_Click(object sender, EventArgs e)
        {
            if (curSample == null) return;
            SaveFileDialog sfd = new SaveFileDialog();

            if (Directory.Exists(PratiCantoForms.ClientFolder)) sfd.InitialDirectory = PratiCantoForms.ClientFolder;

            if (recFileName != "")
            {
                FileInfo fi = new FileInfo(recFileName);
                sfd.FileName = fi.Name;
                sfd.InitialDirectory = fi.DirectoryName;
            }
            
            sfd.Filter = "REC|*.wav";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                curSample.SavePart(sfd.FileName, 0, curSample.time[curSample.time.Length - 1]);
            }
        }


        #endregion

        private void btnOpenFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Rec|*.wav;*.mp3";

            if (Directory.Exists(PratiCantoForms.ClientFolder)) ofd.InitialDirectory = PratiCantoForms.ClientFolder;

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                OpenAudioFile(ofd.FileName);
                AnalyzeCurSample();
            }
        }

        private void OpenAudioFile(string filename)
        {
            //stop playback
            if (replaying && wo != null && wo.PlaybackState == PlaybackState.Playing) btnPlay_Click(this, new EventArgs());

            if (curSample != null) curSample.Dispose();
            curSample = new SampleAudio(filename);

            saGL.SetSampleAudio(curSample);
        }

        #region Cosmetics
        private void panSustVowelCfg_Paint(object sender, PaintEventArgs e)
        {
            PratiCanto.LayoutFuncs.MakeDegrade(e.Graphics, Color.FromArgb(230, 230, 230), Color.White, panSustVowelCfg.Width, panSustVowelCfg.Height, true);
        }

        private void panSummary_Paint(object sender, PaintEventArgs e)
        {
            PratiCanto.LayoutFuncs.MakeDegrade(e.Graphics, Color.FromArgb(230, 230, 230), Color.White, panSummary.Width, panSummary.Height, true);
        }


        bool isSummarySelected = true;
        Font fBut = new Font("Arial", 10, FontStyle.Bold);
        private void btnSummary_Paint(object sender, PaintEventArgs e)
        {
            if (isSummarySelected) PratiCanto.LayoutFuncs.MakeDegrade(e.Graphics, Color.FromArgb(218, 30, 30), Color.FromArgb(113, 40, 40), btnSummary.Width, btnSummary.Height, true);
            else PratiCanto.LayoutFuncs.MakeDegrade(e.Graphics, Color.FromArgb(230, 230, 230), Color.White, btnSummary.Width, btnSummary.Height, true);

            SizeF s = e.Graphics.MeasureString(btnSummary.Text, fBut);
            Brush b = isSummarySelected ? Brushes.White : Brushes.Black;
            e.Graphics.DrawString(btnSummary.Text, fBut, b, (btnSummary.Width >> 1) - (s.Width * 0.5f), (btnSummary.Height >> 1) - (s.Height * 0.5f));
        }
        private void btnDetails_Paint(object sender, PaintEventArgs e)
        {
            if (!isSummarySelected) PratiCanto.LayoutFuncs.MakeDegrade(e.Graphics, Color.FromArgb(218, 30, 30), Color.FromArgb(113, 40, 40), btnDetails.Width, btnDetails.Height, true);
            else PratiCanto.LayoutFuncs.MakeDegrade(e.Graphics, Color.FromArgb(230, 230, 230), Color.White, btnDetails.Width, btnDetails.Height, true);

            SizeF s = e.Graphics.MeasureString(btnDetails.Text, fBut);
            Brush b = !isSummarySelected ? Brushes.White : Brushes.Black;
            e.Graphics.DrawString(btnDetails.Text, fBut, b, (btnDetails.Width >> 1) - (s.Width * 0.5f), (btnDetails.Height >> 1) - (s.Height * 0.5f));
        }
        private void btnSummary_Click(object sender, EventArgs e)
        {
            isSummarySelected = true;
            panSummary.Visible = true;
            panDetails.Visible = false;
            panVoiceEvol.Visible = true;
            btnSummary.Invalidate();
            btnDetails.Invalidate();
        }

        private void btnDetails_Click(object sender, EventArgs e)
        {
            isSummarySelected = false;
            panSummary.Visible = false;
            panDetails.Visible = true;
            panVoiceEvol.Visible = false;
            btnSummary.Invalidate();
            btnDetails.Invalidate();
        }
        private void panDetFreqs_Paint(object sender, PaintEventArgs e)
        {
            PratiCanto.LayoutFuncs.MakeDegrade(e.Graphics, Color.FromArgb(236, 236, 236), Color.White, panDetFreqs.Width, panDetFreqs.Height, true);
        }

        private void panDetNumDetails_Paint(object sender, PaintEventArgs e)
        {
            PratiCanto.LayoutFuncs.MakeDegrade(e.Graphics, Color.FromArgb(236, 236, 236), Color.White, panDetNumDetails.Width, panDetNumDetails.Height, true);
        }
        private void panDetails_Paint(object sender, PaintEventArgs e)
        {
            PratiCanto.LayoutFuncs.MakeDegrade(e.Graphics, Color.White, Color.FromArgb(236, 236, 236), panDetails.Width, panDetails.Height, true);
        }
        private void panVoiceEvol_Paint(object sender, PaintEventArgs e)
        {
            PratiCanto.LayoutFuncs.MakeDegrade(e.Graphics, Color.White, Color.FromArgb(236, 236, 236), panVoiceEvol.Width, panVoiceEvol.Height, true);
        }
        private void frmSustVowel_Resize(object sender, EventArgs e)
        {
            picVoiceParams.Invalidate();
            if (saGL != null)
            {
                saGL.ResetDrawingRanges();
                saGL.UpdateDrawing();
            }
            foreach (Control c in this.Controls)
            {
                if (c is Panel) ((Panel)c).Invalidate();
            }

        }
        private void splitContainer1_Panel1_Resize(object sender, EventArgs e)
        {
            saGL.ResetDrawingRanges();
            saGL.UpdateDrawing();
        }
        #endregion

        private void frmSustVowel_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (recording) btnRec_Click(sender, e);
            if (replaying) btnPlay_Click(sender, e);
        }













    }
}
