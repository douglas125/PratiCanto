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

namespace PratiCanto
{
    public partial class frmIntonation : Form
    {
        public frmIntonation()
        {
            InitializeComponent();
        }
        private void btnClient_Click(object sender, EventArgs e)
        {
            PratiCantoForms.ShowClientForm();
        }

        private void frmSelfListen_Load(object sender, EventArgs e)
        {
            AudioComparer.SoftwareKey.CheckLicense("Intonation", false);
            
            AdjButtons();
            
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


        #region Record
        //SampleAudioGL saGL;
        SampleAudio.RealTime rt;
        bool recording = false;
        SampleAudio curSample;
        string recFileName;
        private void btnRec_Click(object sender, EventArgs e)
        {

            if (!recording)
            {
                //StopAllReplay();


                string UserFolder;
                if (Directory.Exists(PratiCantoForms.ClientFolder)) UserFolder = PratiCantoForms.ClientFolder;
                else UserFolder = Application.StartupPath + "\\" + PratiCantoForms.ClientFolder;

                if (!Directory.Exists(UserFolder)) Directory.CreateDirectory(UserFolder);

                string recFolder = PratiCantoForms.getRecFolder();
                if (!Directory.Exists(recFolder)) Directory.CreateDirectory(recFolder);

                recFileName = recFolder + "\\Intonation" + DateTime.Now.Hour.ToString() + "h" + DateTime.Now.Minute.ToString().PadLeft(2, '0') + "min" + DateTime.Now.Second.ToString().PadLeft(2, '0') + "s.wav";
                //saGL.ViewFormants = false;
                rt = new SampleAudio.RealTime();
                rt.OnDataReceived = SoundReceived;
                rt.StartRecording(cmbInputDevice.SelectedIndex);

                btnRec.Image = picStop.Image;

                recording = true;

                if (recTime.Count > 0)
                {
                    oldTimes.Add(recTime);
                    oldFreqs.Add(recFreq);
                    oldAudios.Add(curSample);
                }
                recTime = new List<float>();
                recFreq = new List<float>(); 
            }
            else
            {
                recording = false;
                rt.StopRecording();
                
                //saGL.ResetRealTimeDrawing();

                curSample = new SampleAudio(rt);
                //saGL.SetSampleAudio(curSample);
                btnRec.Image = picRec.Image;

                //attempt to correct frequencies
                curSample.ComputeBaseFreqAndFormants(0, curSample.time[curSample.time.Length - 1], "0", "3e-3", "6", "0", true);
                List<PointF> fSamples = new List<PointF>();
                List<PointF> fIntens = new List<PointF>();

                RetrieveBaseFreqs(curSample, (float)numMinIntens.Value, out recTime, out recFreq);

                picIntonation.Invalidate();
                RemakeCmbReplay();
            }
        }
        
        private void SoundReceived()
        {
            //saGL.DrawRealTime(rt);
        }
        #endregion

        private void btnStartStop_Click(object sender, EventArgs e)
        {

        }

        #region Real time recording
        List<float> recTime = new List<float>();
        List<float> recFreq = new List<float>();
        float curf, curI;
        float prevf;

        List<List<float>> oldTimes = new List<List<float>>();
        List<List<float>> oldFreqs = new List<List<float>>();
        List<SampleAudio> oldAudios = new List<SampleAudio>();

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

                    recTime.Add(rt.time[rt.time.Count - 1]);
                    if (curI > (float)numMinIntens.Value)
                    {
                        //float curfHT = 0;
                        //if (curf > 0) curfHT = 30 + (float)SoundSynthesis.Note.GetHalfStepsAboveA4(curf);
                        if (curf != 0) prevf = curf;
                        if (curf == 0) curf = prevf;

                        recFreq.Add(curf);
                    }
                    else recFreq.Add(0);
                }
                catch
                {
                }
                picIntonation.Invalidate();
       
            }

        }
        #endregion

        #region Manage visual feedback
        int penSize = 6;
        Font ff = new Font("Arial", 10, FontStyle.Bold);
        private void picIntonation_Paint(object sender, PaintEventArgs e)
        {
            if (recTime != null && recTime.Count > 2)
            {
                SampleAudio.PlotToBmp(e.Graphics, recTime.ToArray(), recFreq.ToArray(), 0, (double)numMaxTime.Value, (double)numMinFreq.Value, (double)numMaxFreq.Value, new Pen(Color.Red, penSize), picIntonation.Width, picIntonation.Height, 0, recTime.Count - 1);

                e.Graphics.DrawString("f0:" + curf.ToString() + " intens:" + curI.ToString(), ff, Brushes.Black, 0, 30);
            }

            for (int k = 0; k < oldTimes.Count; k++)
            {
                Color c = Color.Blue;
                if (cmbReplay.SelectedIndex - 1 == k) c = Color.Orange;
                SampleAudio.PlotToBmp(e.Graphics, oldTimes[k].ToArray(), oldFreqs[k].ToArray(), 0, (double)numMaxTime.Value, (double)numMinFreq.Value, (double)numMaxFreq.Value, new Pen(c, penSize), picIntonation.Width, picIntonation.Height, 0, oldTimes[k].Count - 1);
            }

            if (clickPt.X != 0 || clickPt.Y != 0)
            {
                e.Graphics.FillRectangle(Brushes.Black, clickPt.X - 1, clickPt.Y - 1, 3, 3);
                double t = Math.Round((double)numMaxTime.Value * (double)clickPt.X / (double)picIntonation.Width, 1);
                double F = Math.Round((double)numMinFreq.Value + (double)(numMaxFreq.Value - numMinFreq.Value) * (double)(picIntonation.Height - clickPt.Y) / (double)picIntonation.Height, 1);
                e.Graphics.DrawString("t = " + t.ToString() + "s; " + "f = " + F.ToString() + " Hz", ff, Brushes.Black, clickPt.X+5, clickPt.Y);
            }

            if (totalTime != 0)
            {
                float xProg = (float)(picIntonation.Width * curTime / (double)numMaxTime.Value);
                e.Graphics.DrawLine(new Pen(Color.Red, 3), xProg, 0, xProg, picIntonation.Height);
            }
        }

        private void numMaxFreq_ValueChanged(object sender, EventArgs e)
        {
            picIntonation.Invalidate();
        }

        private void numMinFreq_ValueChanged(object sender, EventArgs e)
        {
            picIntonation.Invalidate();
        }

        private void txtDelCurves_Click(object sender, EventArgs e)
        {
            StopAllReplay();
            
            recTime.Clear();
            recFreq.Clear();

            oldTimes.Clear();
            oldFreqs.Clear();

            if (curSample != null) curSample.Dispose();
            curSample = null;

            foreach (SampleAudio saud in oldAudios) saud.Dispose();
            oldAudios.Clear();

            picIntonation.Invalidate();

            RemakeCmbReplay();
        }

        private void numMaxTime_ValueChanged(object sender, EventArgs e)
        {
            picIntonation.Invalidate();
        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
            if (curSample != null) curSample.Play(curSample.time[0], curSample.time[curSample.time.Length - 1]);
        }

        private void cmbReplay_SelectedIndexChanged(object sender, EventArgs e)
        {
            picIntonation.Invalidate();
            cmbReplayTogether.SelectedIndex = cmbReplay.SelectedIndex;
        }
        #endregion

        #region Replay audio

        /// <summary>Remakes replay combobox with current and previous audios</summary>
        void RemakeCmbReplay()
        {
            cmbReplay.Items.Clear();
            cmbReplayTogether.Items.Clear();

            if (curSample == null) return;
            cmbReplay.Items.Add(0);
            cmbReplayTogether.Items.Add(0);
            for (int k = 0; k < oldAudios.Count; k++)
            {
                cmbReplay.Items.Add(k + 1);
                cmbReplayTogether.Items.Add(k + 1);
            }

            if (cmbReplay.Items.Count > 0) cmbReplay.SelectedIndex = 0;
        }

        private void btnReplayPrevAudio_Click(object sender, EventArgs e)
        {
            if (cmbReplay.SelectedIndex >= 0)
            {
                AudioComparer.SampleAudio.PlaySample ps;
                if (cmbReplay.SelectedIndex == 0)
                {
                    ps = curSample.Play();
                    totalTime = curSample.time[curSample.time.Length - 1];
                }
                else
                {
                    ps = oldAudios[cmbReplay.SelectedIndex - 1].Play();
                    totalTime = oldAudios[cmbReplay.SelectedIndex - 1].time[oldAudios[cmbReplay.SelectedIndex - 1].time.Length - 1];
                }
                if (ps != null) ps.PlayTimeFunc = DrawTime;

                if (cmbReplayTogether.SelectedIndex != cmbReplay.SelectedIndex)
                {
                    if (cmbReplayTogether.SelectedIndex == 0) curSample.Play();
                    else oldAudios[cmbReplayTogether.SelectedIndex - 1].Play();
                }
            }
            
        }

        System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
        double lastUpdt;
        double curTime;
        double totalTime;
        private void DrawTime(double time)
        {
            //return;
            if (!sw.IsRunning) sw.Start();
            if (sw.Elapsed.TotalSeconds - lastUpdt > 0.01)
            {
                curTime = time;
                picIntonation.Invalidate();
                lastUpdt = sw.Elapsed.TotalSeconds;
                //if (saGL != null && wo != null) saGL.DrawCurPbTime((float)time);
            }
        }

        private void btnStopOrigFile_Click(object sender, EventArgs e)
        {
            StopAllReplay();
        }

        private void StopAllReplay()
        {
            if (curSample != null) curSample.Stop();
            foreach (SampleAudio sa in oldAudios) sa.Stop();
        }

        private void btnOpenFileInton_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Rec|*.wav;*.mp3";

            if (Directory.Exists(PratiCantoForms.ClientFolder)) ofd.InitialDirectory = PratiCantoForms.ClientFolder;

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                SampleAudio newSample = new SampleAudio(ofd.FileName);
                newSample.ComputeBaseFreqAndFormants(0, newSample.time[newSample.time.Length - 1], "0", "3e-3", "6", "0", true);
                List<float> recT, recF;
                RetrieveBaseFreqs(newSample, (float)numMinIntens.Value, out recT, out recF);

                if (curSample == null)
                {
                    curSample = newSample;
                    recTime = recT;
                    recFreq = recF;
                    numMaxTime.Value = (decimal)Math.Ceiling(newSample.time[newSample.time.Length - 1]);
                }
                else
                {
                    oldAudios.Add(newSample);
                    oldFreqs.Add(recF);
                    oldTimes.Add(recT);
                }

                RemakeCmbReplay();
            }

        }

        private static void RetrieveBaseFreqs(SampleAudio sa, float minIntens, out List<float> recT, out List<float> recF)
        {
            recT = new List<float>();
            recF = new List<float>();
            for (int k = 0; k < sa.baseFreqsTimes.Count; k++)
            {
                if (!float.IsInfinity(sa.baseFreqsF0[k]) && !float.IsInfinity(sa.baseFreqsIntensity[k]) && !float.IsNaN(sa.baseFreqsF0[k]) && !float.IsNaN(sa.baseFreqsIntensity[k]))
                {
                    if (sa.baseFreqsIntensity[k] > minIntens)
                    {
                        recT.Add(sa.baseFreqsTimes[k]);
                        recF.Add(sa.baseFreqsF0[k]);
                    }
                }
            }
        }
        private void btnSaveAll_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Rec|*.wav";

            string recFolder = PratiCantoForms.getRecFolder();
            if (!Directory.Exists(recFolder)) Directory.CreateDirectory(recFolder);

            recFileName = "Inton" + DateTime.Now.Hour.ToString() + "h" + DateTime.Now.Minute.ToString().PadLeft(2, '0') + "min" + DateTime.Now.Second.ToString().PadLeft(2, '0') + "s.wav";
            sfd.FileName = recFileName;
            sfd.InitialDirectory = recFolder;

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                curSample.SaveAll(sfd.FileName);
                FileInfo fi = new FileInfo(sfd.FileName);
                int nn=1;
                foreach (SampleAudio saud in oldAudios)
                {
                    string fname = fi.DirectoryName + "\\" + fi.Name.Replace(".wav", "") + nn.ToString() + fi.Extension;
                    saud.SaveAll(fname);
                    nn++;
                }
            }
        }

        private Point clickPt = new Point();
        private void picIntonation_MouseDown(object sender, MouseEventArgs e)
        {
            clickPt = new Point(e.X, e.Y);
            double F = Math.Round((double)numMinFreq.Value + (double)(numMaxFreq.Value - numMinFreq.Value) * (double)(picIntonation.Height - clickPt.Y) / (double)picIntonation.Height, 1);

            System.Console.Beep((int)F, 250);
            picIntonation.Invalidate();
        }
        #endregion

        #region Cosmetics
        private void gbIntonAnalysis_Paint(object sender, PaintEventArgs e)
        {
            PratiCanto.LayoutFuncs.DoGroupBoxLayout((GroupBox)sender, e.Graphics);
        }

        private void AdjButtons()
        {
            btnStartTest.FlatStyle = FlatStyle.Flat;
            btnStartTest.FlatAppearance.BorderSize = 0;

            btnDelCurves.FlatStyle = FlatStyle.Flat;
            btnDelCurves.FlatAppearance.BorderSize = 0;
            btnDelCurves.Image = PratiCanto.LayoutFuncs.MakeDegradeWithImageGray(btnDelCurves.Image, btnDelCurves.Width, btnDelCurves.Height, true);

            btnOpenFileInton.FlatStyle = FlatStyle.Flat;
            btnOpenFileInton.FlatAppearance.BorderSize = 0;
            btnOpenFileInton.Image = PratiCanto.LayoutFuncs.MakeDegradeWithImageGray(btnOpenFileInton.Image, btnOpenFileInton.Width, btnOpenFileInton.Height, true);

            btnReplayPrevAudio.FlatStyle = FlatStyle.Flat;
            btnReplayPrevAudio.FlatAppearance.BorderSize = 0;
            btnReplayPrevAudio.Image = PratiCanto.LayoutFuncs.MakeDegradeWithImageGray(btnReplayPrevAudio.Image, btnReplayPrevAudio.Width, btnReplayPrevAudio.Height, true);

            btnStopOrigFile.FlatStyle = FlatStyle.Flat;
            btnStopOrigFile.FlatAppearance.BorderSize = 0;
            btnStopOrigFile.Image = PratiCanto.LayoutFuncs.MakeDegradeWithImageGray(btnStopOrigFile.Image, btnStopOrigFile.Width, btnStopOrigFile.Height, true);

            btnSaveAll.FlatStyle = FlatStyle.Flat;
            btnSaveAll.FlatAppearance.BorderSize = 0;
            btnSaveAll.Image = PratiCanto.LayoutFuncs.MakeDegradeWithImageRed(btnSaveAll.Image, btnSaveAll.Width, btnSaveAll.Height, false);
        }

        

        #endregion

        private void frmIntonation_FormClosing(object sender, FormClosingEventArgs e)
        {
            StopAllReplay();
        }














    }
}
