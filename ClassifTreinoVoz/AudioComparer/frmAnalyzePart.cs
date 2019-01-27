using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NAudio.Wave;
using ClassifTreinoVoz;
using System.IO;

namespace AudioComparer
{
    /// <summary>Detailed analysis of part of audio</summary>
    public partial class frmAnalyzePart : Form
    {
        /// <summary>OpenGL controls where draws are being made</summary>
        SampleAudioGL saGL;

        /// <summary>Analyze part of audio</summary>
        public frmAnalyzePart(SampleAudioGL saGL)
        {
            this.saGL = saGL;

            InitializeComponent();
        }

        private void frmAnalyzePart_Load(object sender, EventArgs e)
        {
            //maybe some other version
            tabAnalyze.TabPages.Remove(tabSelfSim);
            tabAnalyze.TabPages.Remove(tabPhoneticSearch);

            
            this.Height = tabAnalyze.Top + tabAnalyze.Height + 50;
            tabAnalyze.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom;

            shrinkCenterReg = (float)numAmplif.Value;
            refreshAnnotationSelection();
        }

        private void refreshAnnotationSelection()
        {
            cmbAnnot.Items.Clear();

            foreach (SampleAudio.AudioAnnotation ann in saGL.sampleAudio.Annotations) cmbAnnot.Items.Add(ann.Text);
            //if (cmbAnnot.Items.Count > 0) cmbAnnot.SelectedIndex = 0;

            //numStart.Minimum = (decimal)saGL.sampleAudio.time[0];
            //numStart.Maximum = (decimal)saGL.sampleAudio.time[saGL.sampleAudio.time.Length - 1];
            //numEnd.Minimum = (decimal)saGL.sampleAudio.time[0];
            //numEnd.Maximum = (decimal)saGL.sampleAudio.time[saGL.sampleAudio.time.Length - 1];
        }
        private void btnDelAnnot_Click(object sender, EventArgs e)
        {
            if (cmbAnnot.SelectedIndex >= 0)
            {
                saGL.sampleAudio.Annotations.RemoveAt(cmbAnnot.SelectedIndex);
                refreshAnnotationSelection();
            }
        }

        #region Select and zoom to region
        private void btnZoomToRegion_Click(object sender, EventArgs e)
        {
            //saGL.p0SelX = (float)numStart.Value;
            //saGL.pfSelX = (float)numEnd.Value;
            //saGL.ZoomIn();
        }
        private void btnZoomOut_Click(object sender, EventArgs e)
        {
            saGL.ZoomOut();
        }
        private void cmbAnnot_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbAnnot.Items.Count == 0) return;
            int annotID = cmbAnnot.SelectedIndex;

            saGL.SelectRegion((float)saGL.sampleAudio.Annotations[annotID].startTime, (float)saGL.sampleAudio.Annotations[annotID].stopTime);
            //numStart.Value = Math.Max(numStart.Minimum, Math.Min(numStart.Maximum, (decimal)saGL.sampleAudio.Annotations[annotID].startTime));
            //numEnd.Value = Math.Max(numEnd.Minimum, Math.Min(numEnd.Maximum, (decimal)saGL.sampleAudio.Annotations[annotID].stopTime));
        }
        private void numStart_ValueChanged(object sender, EventArgs e)
        {
            drawGLSelection();
        }
        private void numEnd_ValueChanged(object sender, EventArgs e)
        {
            drawGLSelection();
        }
        private void drawGLSelection()
        {
            if (cmbAnnot.SelectedIndex < 0) return;
            //saGL.SelectRegion((float)numStart.Value, (float)numEnd.Value);
        }
        #endregion


        #region Formant analysis

        float[] cLS;
        //float[] cLplq;
        float[] freqs;
        float[] formants;
        float[] fIntens;

        float[] burgCs;
        float[] burgFs;

        private void btnFormantsLS_Click(object sender, EventArgs e)
        {
            //OpenCLTemplate.CLCalc.DisableCL();

            System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
            sw.Start();
            formants = LinearPredictor.EstimateFormantsLS(saGL.sampleAudio.audioData, saGL.sampleAudio.time[1], 60, 2000, saGL.pfSelX, out fIntens);
            for (int k = 0; k < formants.Length; k++)
            {
                //formants[k] = (float)Math.Log(formants[k]);
                if (k % 2 == 1) fIntens[k] = -fIntens[k];
            }


            sw.Stop();

            //acquisition time step
            float deltaX = saGL.sampleAudio.time[1];

            //lower frequency requires longer distance
            int idxMax = (int)(1.0f / (70 * deltaX));
            int idxMin = (int)(1.0f / (1200 * deltaX));

            //int anIdx = (int)((float)numEnd.Value / deltaX);
            int anIdx = (int)((float)saGL.pfSelX / deltaX);

            int step = 1;
            LinearPredictor lp = new LinearPredictor(saGL.sampleAudio.audioData, idxMin, idxMax, step, 2 * (idxMax - idxMin), anIdx);

            //samples for burg
            double[] burgSamples = new double[2 * idxMax];
            for (int k = 0; k < burgSamples.Length; k++) burgSamples[k] = saGL.sampleAudio.audioData[anIdx + k - burgSamples.Length];
            double[] burgCoefs = LinearPredictor.BurgAlgorithm(idxMax, burgSamples);
            burgCs = new float[burgCoefs.Length]; burgFs = new float[burgCoefs.Length];
            for (int k = 0; k < burgCoefs.Length; k++)
            {
                burgFs[k] = (float)(1.0f / (float)(k * deltaX));
                burgCs[k] = (float)burgCoefs[k] * 0.03f;
            }
            burgFs[0] = 100;


            //float f0 = SampleAudio.RealTime.ComputeBaseFrequency(saGL.sampleAudio.audioData, saGL.sampleAudio.time[1], anIdx, 70);


            float[] x0 = new float[lp.nFuncs];
            for (int k = 0; k < lp.nFuncs; k++) x0[k] = 0.1f;
            //cLS = lp.LinearPredict(x0, 2.01f, 2.01f, 1);
            cLS = lp.LinearPredictLS();
            //cLplq = lp.LinearPredict(x0, 2.0f, 1.05f, 1);

            float maxFreq = 1000;

            freqs = new float[cLS.Length];
            for (int k = 0; k < cLS.Length; k++)
            {
                freqs[k] = 1.0f / (float)((k * step + idxMin) * deltaX);
                //if (freqs[k] < f0)
                {
                    cLS[k] = 0;
                    //cLplq[k] = 0;
                }
            }

            //Clip frequencies
            List<float> lstFreq = new List<float>();
            List<float> lstCLS = new List<float>();
            for (int k = 0; k < cLS.Length; k++)
            {
                if (freqs[k] < maxFreq)
                {
                    lstFreq.Add((float)(freqs[k]));
                    lstCLS.Add(cLS[k]);
                }
            }

            freqs = lstFreq.ToArray();
            cLS = lstCLS.ToArray();
            //freqs = reverse(freqs);
            //cLS = reverse(cLS);
            //cLplq = reverse(cLplq);

            //picLinPred.Refresh();
        }

        private float[] reverse(float[] v)
        {
            float[] ans = new float[v.Length];
            for (int k = 0; k < ans.Length; k++) ans[k] = v[ans.Length - 1 - k];
            return ans;
        }

        #endregion

        float[] floatCeps;
        float[] freq;
        private void btnCepstrum_Click(object sender, EventArgs e)
        {
            //acquisition time step
            float deltaX = saGL.sampleAudio.time[1];

            //index to analyze
            //int anIdx = (int)((float)numEnd.Value / deltaX);
            int anIdx = (int)((float)saGL.pfSelX / deltaX);

            double[] fft = SampleAudio.computeAudioFFT(saGL.sampleAudio.audioData, anIdx, SampleAudio.FFTSamples);

            float[] cepstrum = new float[fft.Length];
            for (int k = 0; k < fft.Length; k++) cepstrum[k] = (float)fft[k];


            float stepFreq = (float)saGL.sampleAudio.waveFormat.SampleRate / (float)SampleAudio.FFTSamples;
            int idMax = SampleAudio.FFTSamples >> 1;//(int)(SampleAudio.maxKeepFreq / stepFreq);

            freq = new float[idMax];
            for (int k = 0; k < idMax; k++) freq[k] = k * stepFreq;

            double[] dblCeps = SampleAudio.computeAudioFFT(cepstrum, SampleAudio.FFTSamples - 1, idMax);

            floatCeps = new float[dblCeps.Length];
            for (int k = 0; k < dblCeps.Length; k++) floatCeps[k] = (float)dblCeps[k];

            //floatCeps = cepstrum;
            //picLinPred.Invalidate();
        }

        #region Frequency filter
        float[] localFFT;
        float[] freqsFFT;

        private void btnSelectFreqs_Click(object sender, EventArgs e)
        {
            float deltaT = saGL.sampleAudio.time[1];
            int anIdx = (int)((float)saGL.pfSelX / deltaT);

            float stepFreq = (float)saGL.sampleAudio.waveFormat.SampleRate / (float)SampleAudio.FFTSamples;
            int idMax = (int)(SampleAudio.maxKeepFreq / stepFreq);


            double[] refFFT = SampleAudio.computeAudioFFT(saGL.sampleAudio.audioData, anIdx, SampleAudio.FFTSamples);

            localFFT = new float[refFFT.Length >> 1];
            freqsFFT = new float[refFFT.Length >> 1];
            for (int k = 0; k < localFFT.Length; k++)
            {
                freqsFFT[k] = k * stepFreq;
                localFFT[k] = (float)refFFT[k]; // (float)Math.Pow(10, refFFT[k] * 0.05) * 0.2f;
            }

            numMinFreq.Maximum = (decimal)freqsFFT[freqsFFT.Length - 1];
            numMaxFreq.Maximum = (decimal)freqsFFT[freqsFFT.Length - 1];
            numMinFreq.Value = (decimal)freqsFFT[0];
            numMaxFreq.Value = (decimal)(freqsFFT[freqsFFT.Length - 1] * 0.5f);


            picSpecFilter.Invalidate();
        }

        Font font = new Font("Arial", 8, FontStyle.Bold);
        private void picSpecFilter_Paint(object sender, PaintEventArgs e)
        {
            if (localFFT == null) return;

            float maxY = localFFT[0];
            float minY = localFFT[0];
            foreach (float f in localFFT)
            {
                maxY = Math.Max(f, maxY);
                minY = Math.Min(f, minY);
            }
            SampleAudio.PlotToBmp(e.Graphics, freqsFFT, localFFT, (float)numMinFreq.Value, (float)numMaxFreq.Value, minY, maxY, Pens.Brown, picSpecFilter.Width, picSpecFilter.Height, 0, freqsFFT.Length - 1);


            //draw selections
            Brush b = new SolidBrush(Color.FromArgb(50, 0, 255, 0));
            if (clickSpec)
            {
                if (ptf.X > pt0.X) e.Graphics.FillRectangle(b, pt0.X, 0, ptf.X - pt0.X, picSpecFilter.Height);
                else e.Graphics.FillRectangle(b, ptf.X, 0, pt0.X - ptf.X, picSpecFilter.Height);
            }
            else
            {
                float freq = ((float)numMaxFreq.Value - (float)numMinFreq.Value) * (float)ptf.X / (float)picSpecFilter.Width + (float)numMinFreq.Value;
                e.Graphics.DrawString(freq.ToString() + "Hz", font, Brushes.Black, ptf.X, ptf.Y);
            }

            float scaley = (float)picSpecFilter.Width / ((float)numMaxFreq.Value - (float)numMinFreq.Value);
            foreach (FFTFreqFilter.freqRegion fr in lstFRegions)
            {
                float x0 = (fr.f0 - (float)numMinFreq.Value) * scaley;
                float xf = (fr.ff - (float)numMinFreq.Value) * scaley;
                e.Graphics.FillRectangle(b, x0, 0, xf - x0, picSpecFilter.Height);
                e.Graphics.DrawString(Math.Round(fr.f0, 2).ToString() + "Hz\r\n" + Math.Round(fr.ff, 2).ToString() + "Hz", font, Brushes.Black, x0, 0);
            }
        }

        private void numMinFreq_ValueChanged(object sender, EventArgs e)
        {
            picSpecFilter.Invalidate();
            if (numMinFreq.Value > numMaxFreq.Value) numMaxFreq.Value = numMinFreq.Value;
        }

        private void numMaxFreq_ValueChanged(object sender, EventArgs e)
        {
            picSpecFilter.Invalidate();
            if (numMinFreq.Value > numMaxFreq.Value) numMinFreq.Value = numMaxFreq.Value;
        }

        bool clickSpec = false;
        private void btnClearFreqs_Click(object sender, EventArgs e)
        {
            lstFRegions.Clear();
            picSpecFilter.Invalidate();
        }

        List<FFTFreqFilter.freqRegion> lstFRegions = new List<FFTFreqFilter.freqRegion>();
        PointF pt0 = new PointF();
        PointF ptf = new PointF();

        private void picSpecFilter_MouseDown(object sender, MouseEventArgs e)
        {
            pt0.X = e.X;
            pt0.Y = e.Y;
            ptf.X = e.X;
            ptf.Y = e.Y;
            clickSpec = true;
        }

        private void picSpecFilter_MouseUp(object sender, MouseEventArgs e)
        {
            if (clickSpec)
            {
                ptf.X = e.X;
                ptf.Y = e.Y;
                float scalex = ((float)numMaxFreq.Value - (float)numMinFreq.Value) / (float)picSpecFilter.Width;

                float f0 = scalex * pt0.X + (float)numMinFreq.Value;
                float fF = scalex * ptf.X + (float)numMinFreq.Value;

                if (ptf.X != pt0.X)
                {
                    if (f0 < 0) f0 = 0;
                    if (fF < 0) fF = 0;

                    if (fF >= f0) lstFRegions.Add(new FFTFreqFilter.freqRegion(f0, fF));
                    else lstFRegions.Add(new FFTFreqFilter.freqRegion(fF, f0));
                }

                clickSpec = false;
            }
        }

        private void picSpecFilter_MouseMove(object sender, MouseEventArgs e)
        {
            ptf.X = e.X;
            ptf.Y = e.Y;
            picSpecFilter.Invalidate();
        }

        SampleAudio audioFiltered;
        WaveOut woFilter;
        bool playingFiltered;

        private void btnComputeFiltered_Click(object sender, EventArgs e)
        {
            int idx0 = (int)((float)saGL.p0SelX / saGL.sampleAudio.time[1]);
            int idxF = (int)((float)saGL.pfSelX / saGL.sampleAudio.time[1]);

            float[] newAudio = FFTFreqFilter.FFTFilter(saGL.sampleAudio.audioData, idx0, idxF, saGL.sampleAudio.waveFormat.SampleRate, SampleAudio.FFTSamples, lstFRegions);

            float[] t = new float[newAudio.Length];
            for (int k = 0; k < t.Length; k++) t[k] = saGL.sampleAudio.time[k];

            ////TEMP - for visualization
            //freqsFFT = t;
            //localFFT = newAudio;

            //numMinFreq.Maximum = (decimal)freqsFFT[freqsFFT.Length - 1];
            //numMaxFreq.Maximum = (decimal)freqsFFT[freqsFFT.Length - 1];
            //numMinFreq.Value = (decimal)freqsFFT[0];
            //numMaxFreq.Value = (decimal)(freqsFFT[freqsFFT.Length - 1]);

            //picSpecFilter.Invalidate();

            audioFiltered = new SampleAudio(t, newAudio, saGL.sampleAudio.waveFormat);

            btnPlaySeq.Enabled = true;
            btnSaveSeq.Enabled = true;

            btnPlaySeq_Click(sender, e);
        }


        private void btnPlaySeq_Click(object sender, EventArgs e)
        {
            if (!playingFiltered)
            {
                SampleAudio.PlaySample ps = new SampleAudio.PlaySample(audioFiltered, 0, audioFiltered.time[audioFiltered.time.Length - 1]);

                woFilter = new WaveOut();
                woFilter.PlaybackStopped += new EventHandler<StoppedEventArgs>(woFilter_PlaybackStopped);
                woFilter.Init(ps);

                woFilter.Play();
                playingFiltered = true;
            }
            else
            {
                woFilter.Stop();
                playingFiltered = false;
            }

        }

        void woFilter_PlaybackStopped(object sender, StoppedEventArgs e)
        {
            //throw new NotImplementedException();
            playingFiltered = false;
        }


        private void btnSaveSeq_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();

            sfd.Filter = "REC|*.wav";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                audioFiltered.SavePart(sfd.FileName, 0, audioFiltered.time[audioFiltered.time.Length - 1]);
            }
        }
        #endregion

        #region Group similar regions

        /// <summary>Similar regions</summary>
        List<float[]> simRegions;


        private void btnGroupSimilarRegions_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
            sw.Start();

            //delta time in spectres
            float deltaT = saGL.sampleAudio.baseFreqsTimes[1];

            int idx0 = (int)((float)saGL.p0SelX / deltaT);
            int idxF = (int)((float)saGL.pfSelX / deltaT);

            float bestErr = float.MaxValue;
            for (int curTry = 0; curTry < (int)numTries.Value; curTry++)
            {
                lblComputeTime.Text = curTry.ToString() + "/" + numTries.Value.ToString();
                lblComputeTime.Refresh();

                float err;
                List<float[]> localsimRegions = AudioIdentifRecog.kMeansCluster(saGL.sampleAudio.audioSpectre, idx0, idxF, (int)numQtdClusters.Value, deltaT, (float)numSmallestRegionTime.Value, out err);
                localsimRegions = localsimRegions.OrderBy(r => r[2]).ToList();

                if (err < bestErr)
                {
                    simRegions = localsimRegions;
                    bestErr = err;
                }
            }
            sw.Stop();

            lblMeanSqError.Text = bestErr.ToString();
            //show if used OpenCL
            lblComputeTime.Text = ((OpenCLTemplate.CLCalc.CLAcceleration == OpenCLTemplate.CLCalc.CLAccelerationType.UsingCL) ? "CL" : "") + sw.Elapsed.ToString();

            //show regions
            lstSimRegions.Items.Clear();
            foreach (float[] f in simRegions) lstSimRegions.Items.Add(f[2].ToString().PadLeft(3, '0') + ":" + Math.Round(f[0], 3).ToString() + "s - " + Math.Round(f[1], 3).ToString() + "s");

            //show region durations
            lstDurations.Items.Clear();
            SortedDictionary<int, float> dicDurations = new SortedDictionary<int, float>();
            foreach (float[] f in simRegions)
            {
                int idx = (int)Math.Round(f[2]);
                float value;
                if (dicDurations.TryGetValue(idx, out value)) dicDurations[idx] += f[1] - f[0];
                else dicDurations.Add(idx, f[1] - f[0]);
            }
            foreach (KeyValuePair<int, float> p in dicDurations)
            {
                lstDurations.Items.Add(p.Key.ToString().PadLeft(3, '0') + ": " + Math.Round(p.Value, 3).ToString() + "s");
            }

            ////Mean grouping
            //float meanDif, stdDevDif;
            //simRegions = AudioIdentifRecog.ComputeSimilarSpectreRegions(saGL.sampleAudio.audioSpectre, idx0, idxF, deltaT, (float)numSmallestRegionTime.Value, (float)numMeanAbsDifThresh.Value, out meanDif, out stdDevDif);

            ////suggest new value
            //numMeanAbsDifThresh.Value = (decimal)(meanDif + 2 * stdDevDif);
            //lblMeanAbsDif.Text = meanDif.ToString();
            //lblStdDevAbsDif.Text = stdDevDif.ToString();

        }
        private void lstSimRegions_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstSimRegions.SelectedIndex < 0) return;
            saGL.SelectRegion(simRegions[lstSimRegions.SelectedIndex][0], simRegions[lstSimRegions.SelectedIndex][1]);
        }
        private void lstSimRegions_DoubleClick(object sender, EventArgs e)
        {
            PlayGroupRegion();
        }
        private void lstSimRegions_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13) PlayGroupRegion();
        }
        private void PlayGroupRegion()
        {
            if (lstSimRegions.SelectedIndex < 0) return;
            saGL.sampleAudio.Play(simRegions[lstSimRegions.SelectedIndex][0], simRegions[lstSimRegions.SelectedIndex][1]);
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
            //delta time in spectres
            float deltaT = saGL.sampleAudio.baseFreqsTimes[1];

            int idx0 = (int)(saGL.p0SelX / deltaT);
            int idxF = (int)(saGL.pfSelX / deltaT);

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

        List<float> jitterEvol = new List<float>();
        List<float> shimmEvol = new List<float>();
        List<float> hnrEvol = new List<float>();
        static Font fVoiceEvol = new Font("Arial", 12, FontStyle.Bold);


        #region Voice evolution drawing functions
        static float jitMax = 83.2f;
        static float shimMax = 0.35f;
        static float hnrMin = 9.56f*0.5f;

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
            g.FillRectangle(Brushes.LightGreen, 0, 2*invScaleY, invScaleX, H);
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
            g.DrawLine(pline, W, 0, W-15, 5);


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
                g.DrawString((k + 1).ToString(), evolFont, Brushes.Black, evol[k].X +11, evol[k].Y - 0.5f * size.Height);
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
                evol[k]=pt;
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


        #endregion

        #region Compute self similarity

        float minTime = 0.2f;
        float deltaTime;
        float p0SelfSim, pfSelfSim;
        bool hasSimilarityImg = false;
        string origText;
        private void btnSelfSim_Click(object sender, EventArgs e)
        {
            deltaTime = saGL.sampleAudio.baseFreqsTimes[1] * 2;

            int W, H;
            float[] selfSims = SelfSimilarity.ComputeSelfSimilarity(saGL.sampleAudio, saGL.p0SelX, saGL.pfSelX, minTime, 3.0f, deltaTime, out W, out H);
            p0SelfSim = saGL.p0SelX;
            pfSelfSim = saGL.pfSelX;

            Bitmap bmp = BuildVisualization.buildBmp(W, H, selfSims);
            hasSimilarityImg = true; origText = gbSimAn.Text;

            picSelfSim.Image = bmp;

        }
        private void picSelfSim_MouseMove(object sender, MouseEventArgs e)
        {
            if (hasSimilarityImg)
            {
                float time = (float)e.X/(float)picSelfSim.Width * (pfSelfSim-p0SelfSim)+p0SelfSim;
                float delta = (float)e.Y * deltaTime + minTime;
                gbSimAn.Text = origText + " " + time.ToString() + "," + (minTime + delta).ToString();
                gbSimAn.Refresh();
            }
        }
        
        private void btnSpeechSpeed_Click(object sender, EventArgs e)
        {
            float[] selfSim1 = SelfSimilarity.ComputeVariationRate(saGL.sampleAudio, saGL.p0SelX, saGL.pfSelX, 1.1f);
            float[] selfSim2 = SelfSimilarity.ComputeVariationRate(saGL.sampleAudio, saGL.p0SelX, saGL.pfSelX, 0.05f);

            List<float> selfSim3 = new List<float>();
            foreach(float f in selfSim2) selfSim3.Add(f);

            int wSize = (int)(0.6f / saGL.sampleAudio.baseFreqsTimes[1]);
            LinearPredictor.MedianFilterFundamentalFrequencies(selfSim3, 0, selfSim3.Count, wSize);


            Bitmap bmp = new Bitmap(selfSim1.Length,100);
            float[] x = new float[selfSim1.Length];
            for (int k=0;k<selfSim1.Length;k++) x[k]=k;

            SampleAudio.PlotToBmp(Graphics.FromImage(bmp), x, selfSim1, 0, selfSim1.Length, 0, 4, Pens.Red, bmp.Width, bmp.Height, 0, selfSim1.Length - 1);
            SampleAudio.PlotToBmp(Graphics.FromImage(bmp), x, selfSim2, 0, selfSim2.Length, 0, 4, Pens.Blue, bmp.Width, bmp.Height, 0, selfSim2.Length - 1);
            SampleAudio.PlotToBmp(Graphics.FromImage(bmp), x, selfSim3.ToArray(), 0, selfSim2.Length, 0, 4, Pens.Orange, bmp.Width, bmp.Height, 0, selfSim2.Length - 1);

            picSelfSim.Image = bmp;
        }
        #endregion

        #region Phonetic search
        List<PhonemeRecognition.SpectrogramFoundRegion> sfr;
        private void btnSearchSimilar_Click(object sender, EventArgs e)
        {
            List<float[]> search = saGL.sampleAudio.ExtractFeatures(saGL.p0SelX, saGL.pfSelX, false);

            PhonemeRecognition.SpectrogramNormalize(search);

            List<List<float[]>> lstSearch = new List<List<float[]>>() { search };

            double tStep = 0;
            double sampleTime = 1.0 / (double)saGL.sampleAudio.waveFormat.SampleRate;
            sfr = PhonemeRecognition.SpectrogramSearch(saGL.sampleAudio, sampleTime, 0, saGL.sampleAudio.time[saGL.sampleAudio.time.Length - 1], lstSearch, null, ref tStep);


            lstPhoneticSearchRegions.Items.Clear();
            foreach (PhonemeRecognition.SpectrogramFoundRegion s in sfr)
            {
                lstPhoneticSearchRegions.Items.Add(s.ToString());
            //    curSample.Annotations.Add(new SampleAudio.AudioAnnotation(Math.Round(s.Difference, 2).ToString(), s.t0, s.tf));
            }
            //picControlBar.Invalidate();
        }
        private void lstPhoneticSearchRegions_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstPhoneticSearchRegions.SelectedIndex < 0) return;
            numSimRegThresh.Value = (decimal)sfr[lstPhoneticSearchRegions.SelectedIndex].Difference;
            saGL.SelectRegion((float)sfr[lstPhoneticSearchRegions.SelectedIndex].t0, (float)sfr[lstPhoneticSearchRegions.SelectedIndex].tf);
        }
        private void lstPhoneticSearchRegions_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                if (lstPhoneticSearchRegions.SelectedIndex < 0) return;
                saGL.sampleAudio.Play(sfr[lstPhoneticSearchRegions.SelectedIndex].t0, sfr[lstPhoneticSearchRegions.SelectedIndex].tf);
            }
        }
        private void lstPhoneticSearchRegions_DoubleClick(object sender, EventArgs e)
        {
            if (lstPhoneticSearchRegions.SelectedIndex < 0) return;
            saGL.sampleAudio.Play(sfr[lstPhoneticSearchRegions.SelectedIndex].t0, sfr[lstPhoneticSearchRegions.SelectedIndex].tf);
        }
        private void btnIncludeAnnot_Click(object sender, EventArgs e)
        {
            if (sfr == null) return;
            foreach (PhonemeRecognition.SpectrogramFoundRegion s in sfr)
            {
                if (s.Difference <= (float)numSimRegThresh.Value)
                {
                    saGL.sampleAudio.Annotations.Add(new SampleAudio.AudioAnnotation(txtSimilarAnnotate.Text, s.t0, s.tf));
                    saGL.UpdateDrawing();
                }
            }
        }

        SampleAudio refAudioPhonSearch;
        private void btnAnnotatedSearch_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Rec|*.wav;*.mp3";

            if (Directory.Exists(PratiCantoForms.ClientFolder)) ofd.InitialDirectory = PratiCantoForms.ClientFolder;

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                refAudioPhonSearch = new SampleAudio(ofd.FileName);

                cmbAnnotPhonSearch.Items.Clear();

                List<string> annots = new List<string>();
                foreach (SampleAudio.AudioAnnotation ann in refAudioPhonSearch.Annotations)
                {
                    if (!annots.Contains(ann.Text))
                    {
                        annots.Add(ann.Text);
                        cmbAnnotPhonSearch.Items.Add(ann.Text);
                    }
                }

                if (cmbAnnotPhonSearch.Items.Count > 0) cmbAnnotPhonSearch.SelectedIndex = 0;
            }
        }
        private void cmbAnnotPhonSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbAnnotPhonSearch.Items.Count <= 0) return;

            List<List<float[]>> searchSamples = new List<List<float[]>>();
            string annString = cmbAnnotPhonSearch.Items[cmbAnnotPhonSearch.SelectedIndex].ToString();
            foreach (SampleAudio.AudioAnnotation ann in refAudioPhonSearch.Annotations)
            {
                if (ann.Text == annString)
                {
                    List<float[]> search = refAudioPhonSearch.ExtractFeatures(ann.startTime, ann.stopTime, false);

                    PhonemeRecognition.SpectrogramNormalize(search);
                    searchSamples.Add(search);
                }
            }

            float[] avgStddev = PhonemeRecognition.SpectrogramComputeIntraGroupSqDist(searchSamples);
            lblAvgStdDevPhonSearch.Text = avgStddev[0].ToString() + " (" + avgStddev[1].ToString() + ")";

            double tStep = 0;
            double sampleTime = 1.0 / (double)saGL.sampleAudio.waveFormat.SampleRate;

            sfr = PhonemeRecognition.SpectrogramSearch(saGL.sampleAudio, sampleTime, 0, saGL.sampleAudio.time[saGL.sampleAudio.time.Length - 1], searchSamples, null, ref tStep);


            lstPhoneticSearchRegions.Items.Clear();
            foreach (PhonemeRecognition.SpectrogramFoundRegion s in sfr)
            {
                lstPhoneticSearchRegions.Items.Add(s.ToString());
            }
        }

        
        #endregion


        #region cosmetics
        private void groupBox1_Paint(object sender, PaintEventArgs e)
        {
            PratiCanto.LayoutFuncs.DoGroupBoxLayout((GroupBox)sender, e.Graphics);

        }
        #endregion





    }
}
