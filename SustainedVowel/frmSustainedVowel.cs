using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AudioComparer;

namespace SustainedVowel
{
    public partial class frmSustainedVowel : Form
    {
        public frmSustainedVowel()
        {
            InitializeComponent();   
        }

        SampleAudioGL saGL;
        SampleAudio.RealTime rt;
        bool recording = false;
        SampleAudio curSample;

        private void btnRecStop_Click(object sender, EventArgs e)
        {

            if (!recording)
            {
                btnRecStop.Text = "Recording...";
                rt = new SampleAudio.RealTime();
                //rt.OnDataReceived = SoundReceived;
                rt.StartRecording(cmbInputDevice.SelectedIndex);

                //btnRec.Image = picStop.Image;

                recording = true;

            }
            else
            {
                btnRecStop.Text = "Stopped";
                recording = false;
                rt.StopRecording();

                if (curSample != null) curSample.Dispose();
                curSample = new SampleAudio(rt);

                saGL.SetSampleAudio(curSample);
                int L = curSample.time.Length;
                saGL.SelectRegion(curSample.time[L / 4], curSample.time[3 * L / 4]);

                ComputeRegionInfo();
            }
        }

        private void frmSustainedVowel_Load(object sender, EventArgs e)
        {
            //List mics
            List<string> mics = SampleAudio.RealTime.GetMicrophones();
            foreach (string s in mics) cmbInputDevice.Items.Add(s);
            if (mics.Count <= 1) cmbInputDevice.Visible = false;
            if (mics.Count >= 1) cmbInputDevice.SelectedIndex = 0;

            //init GL
            saGL = new SampleAudioGL(GLPicIntens, glPicSpectre, picControlBar, lblSpecInfo, txtAnnotation);
            saGL.IncreaseSpectreHeight();
        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
            curSample.Play(0, curSample.time[curSample.time.Length - 1]);
        }



        #region Region information


        private void btnComputeRegionInfo_Click(object sender, EventArgs e)
        {
            ComputeRegionInfo();
        }

        /// <summary>List of fundamental frequency and formants</summary>
        List<List<float>> FN = new List<List<float>>();

        private void ComputeRegionInfo()
        {
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

            string freqInfo = "";
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

            string jitString = "";
            if (medianF0 > 0)
            {
                jitString += "Jitta: " + Math.Round(jitter[0], 3).ToString() + " ms; ";
                jitString += "Jitt: " + Math.Round(jitter[1], 3).ToString() + "%; ";
                jitString += "Rap: " + Math.Round(jitter[2], 3).ToString() + "%; ";
                jitString += "ppq5: " + Math.Round(jitter[3], 3).ToString() + "%; ";
                jitString += " (Ref: 83.2ms, 1.04%, 0.68%, 0.84%)";
            }

            //Color jitColor = Color.Black;
            //if (jitter[0] > 83.2 || jitter[1] > 1.04 || jitter[2] > 0.68 || jitter[3] > 0.84) jitColor = Color.Red;
            //if (!float.IsNaN(jitter[1])) e.Graphics.DrawString(jitString, ff, new SolidBrush(jitColor), 0, h0); h0 += 13;


            string shimmerString = "ShdB: " + Math.Round(shimmer[0], 3).ToString() + " dB; ";
            shimmerString += "Shim: " + Math.Round(shimmer[1], 3).ToString() + "%; ";
            shimmerString += " (Ref: 0.35dB, 3.81%)";

            ////shimmerString += "apq3: " + Math.Round(shimmer[2], 3).ToString() + "%; ";
            ////shimmerString += "apq5: " + Math.Round(shimmer[3], 3).ToString() + "%; ";
            ////shimmerString += "apq11: " + Math.Round(shimmer[4], 3).ToString() + "%; ";
            //Color shimmerColor = Color.Black;
            //if (shimmer[0] > 0.35 || shimmer[1] > 3.81/* || shimmer[4] > 3.07*/) shimmerColor = Color.Red;
            //if (!float.IsNaN(shimmer[1])) e.Graphics.DrawString(shimmerString, ff, new SolidBrush(shimmerColor), 0, h0); h0 += 13;

            //Retrieve formant information
            string formantInfo = "Formant medians (Hz): ";
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
            if (medianF0 > 0) txtHarmonicNoise = "Harmonic-to-noise: " + Math.Round(htn, 4).ToString() + "dB; Harmonic-to-total: " + Math.Round(100.0 * htt, 4).ToString() + "%";

            //Display information
            cmbNumList.SelectedIndex = 0;

            string infoTxt = freqInfo;
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
        static float hnrMin = 9.56f * 0.5f;

        /// <summary>Amplify graph by how much?</summary>
        static float shrinkCenterReg = 10;


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
                float jitShimVal = Math.Max(jitEvol[k] / jitMax, shimEvol[k] / shimMax);
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
        #endregion

        private void btnAnalyze_Click(object sender, EventArgs e)
        {
            ComputeRegionInfo();
        }

        private void picVoiceQualityEvol_Paint(object sender, PaintEventArgs e)
        {
            if (jitterEvol.Count == 0) return;
            DrawVoiceEvolution(e.Graphics, jitterEvol, shimmEvol, hnrEvol, picVoiceQualityEvol.Width, picVoiceQualityEvol.Height);
        }
        private void cmbNumList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (FN.Count == 0 || cmbNumList.SelectedIndex < 0) return;

            lstNumInfo.Items.Clear();
            foreach (float f in FN[cmbNumList.SelectedIndex]) lstNumInfo.Items.Add(Math.Round(f, 2).ToString());
        }
        #endregion

        private void frmSustainedVowel_Resize(object sender, EventArgs e)
        {
            picVoiceQualityEvol.Invalidate();
        }

     






    }
}
