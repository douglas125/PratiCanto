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
    public partial class frmEloquence : Form
    {
        public frmEloquence()
        {
            InitializeComponent();
        }
        private void btnClient_Click(object sender, EventArgs e)
        {
            PratiCantoForms.ShowClientForm();
        }

        private void frmEloquence_Load(object sender, EventArgs e)
        {
            delUpdateFrameRate = UpdateFrameRate;
            
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
            //only starts if reference is loaded
            if (dicSearchItems.Count == 0) return;


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
                rt.OnDataReceived = SoundReceived;
                rt.StartRecording(cmbInputDevice.SelectedIndex);

                btnRec.Image = picStop.Image;

                recording = true;
                bWIdentify.RunWorkerAsync();
            }
            else
            {
                recording = false;
                rt.StopRecording();

                //saGL.ResetRealTimeDrawing();

                if (curSample != null) curSample.Dispose();
                curSample = new SampleAudio(rt);
                //saGL.SetSampleAudio(curSample);
                btnRec.Image = picRec.Image;


            }
        }
        
        private void SoundReceived()
        {
            //saGL.DrawRealTime(rt);
        }
        #endregion



        #region References
        SampleAudio refAudioPhonSearch;
        private void btnOpenFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Rec|*.wav;*.mp3";
            ofd.Multiselect = true;

            if (Directory.Exists(PratiCantoForms.ClientFolder)) ofd.InitialDirectory = PratiCantoForms.ClientFolder;

            dicSearchItems.Clear();
            dicIntraMeanStdDev.Clear();

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                foreach (string file in ofd.FileNames)
                {
                    refAudioPhonSearch = new SampleAudio(file);

                    //cmbAnnotPhonSearch.Items.Clear();

                    foreach (SampleAudio.AudioAnnotation ann in refAudioPhonSearch.Annotations)
                    {
                        List<List<float[]>> annSamples;

                        List<float[]> curAnnSample = refAudioPhonSearch.ExtractFeatures(ann.startTime, ann.stopTime, false);
                        PhonemeRecognition.SpectrogramNormalize(curAnnSample);

                        if (dicSearchItems.TryGetValue(ann.Text, out annSamples)) annSamples.Add(curAnnSample);
                        else
                        {
                            annSamples = new List<List<float[]>>();
                            annSamples.Add(curAnnSample);
                            dicSearchItems.Add(ann.Text, annSamples);
                        }
                    }

                    //if (cmbAnnotPhonSearch.Items.Count > 0) cmbAnnotPhonSearch.SelectedIndex = 0;
                }

                lstAnnotations.Items.Clear();
                foreach (KeyValuePair<string, List<List<float[]>>> kvp in dicSearchItems)
                {
                    dicIntraMeanStdDev.Add(kvp.Key, PhonemeRecognition.SpectrogramComputeIntraGroupSqDist(kvp.Value));
                    lstAnnotations.Items.Add(kvp.Key);
                }
            }
        }
        #endregion

        #region Real time extraction of features and classification

        private delegate void voidDel();

        voidDel delUpdateFrameRate;
        string foundClasses = "";
        private void UpdateFrameRate()
        {
            lblFPS.Text = frameRate.ToString();
            lblIdentWord.Text = foundClasses;
            //this.Text = foundClasses.Replace("\r\n", "") + " " + frameRate.ToString();
        }

        private SortedDictionary<string, List<List<float[]>>> dicSearchItems = new SortedDictionary<string,List<List<float[]>>>();
        /// <summary>Intra mean distance and standard deviation</summary>
        private SortedDictionary<string, float[]> dicIntraMeanStdDev = new SortedDictionary<string,float[]>();

        System.Diagnostics.Stopwatch swIdentify = new System.Diagnostics.Stopwatch();
        int nIdentifs = 0;
        double frameRate = 0;

        /// <summary>Time window to analyze, in seconds. Gets reduced if frame rate drops too low</summary>
        double windowTimeToAnalyze = 0.9;
        private void bWIdentify_DoWork(object sender, DoWorkEventArgs e)
        {
            nIdentifs = 0;
            swIdentify.Reset();

            double requiredFrameRate = 1.0 / windowTimeToAnalyze;

            while (recording)
            {
                float tol = (float)numTolerance.Value;
                int idxf = rt.audioReceived.Count - 1;
                double sampleTime = 1.0 / (double)rt.waveSource.WaveFormat.SampleRate;
                int idx0 = idxf - (int)(windowTimeToAnalyze / sampleTime);
                if (idx0 >= 0)
                {
                    if (!swIdentify.IsRunning) swIdentify.Start();
                    nIdentifs++;
                    foundClasses = "";

                    float[] lastData = new float[idxf - idx0];
                    for (int k = 0; k < idxf - idx0; k++) lastData[k] = rt.audioReceived[k + idx0];

                    double tStep = 0;
                    List<float[]> specToBeSearchd = AudioIdentifRecog.ExtractFeatures(lastData, 0, idxf - idx0, rt.waveSource.WaveFormat.SampleRate, SampleAudio.FFTSamples, out tStep, false);
                    PhonemeRecognition.SpectrogramNormalize(specToBeSearchd);

                    foreach (KeyValuePair<string, List<List<float[]>>> kvp in dicSearchItems)
                    {
                        string text = kvp.Key;
                        List<List<float[]>> searchSamples = kvp.Value;
                        List<PhonemeRecognition.SpectrogramFoundRegion> sfr = PhonemeRecognition.SpectrogramSearch(null, sampleTime, 0, (float)((idxf - idx0 - 1) * sampleTime), searchSamples, specToBeSearchd, ref tStep);
                        
                        //found - difference lower than estimated threshold
                        float evalNum = (sfr[0].Difference - dicIntraMeanStdDev[text][0]) / dicIntraMeanStdDev[text][0];

                        foundClasses += Math.Round(evalNum,2).ToString();
                        if (evalNum < tol) foundClasses += "***" + text;
                        else foundClasses += text;
                    }

                }

                if (nIdentifs > 0)
                {
                    frameRate = (double)nIdentifs / swIdentify.Elapsed.TotalSeconds;
                    //try to improve frame rate
                    if (frameRate < requiredFrameRate) windowTimeToAnalyze *= 0.95;
                    try
                    {
                        this.Invoke(delUpdateFrameRate);
                    }
                    catch { }
                }
            }
        }
        #endregion



    }
}
