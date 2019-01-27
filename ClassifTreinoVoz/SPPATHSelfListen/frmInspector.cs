using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AudioComparer;
using System.IO;
using ClassifTreinoVoz;

namespace PratiCanto
{
    public partial class frmInspector : Form
    {
        public frmInspector()
        {
            InitializeComponent();
        }

        SampleAudioGL saGL;
        private void frmInspector_Load(object sender, EventArgs e)
        {
            //check license
            AudioComparer.SoftwareKey.CheckLicense("Inspector", false);
            
            //do cosmetics
            AdjButtons();
            
            //init GL
            saGL = new SampleAudioGL(GLPicIntens, glPicSpectre, picControlBar, lblSpecInfo, txtAnnot);
            saGL.IncreaseSpectreHeight();

            bgwFindRegion.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bgwFindRegion_RunWorkerCompleted);
            delegSetMsg = SetMsg;
        }


        #region Select part of big audio

        /// <summary>Selecting part of big audio?</summary>
        bool clickSel = false;
        Point p0 = new Point(), pf = new Point();
        private void picComposition_MouseDown(object sender, MouseEventArgs e)
        {
            clickSel = true;
            p0 = new Point(e.X, e.Y);
        }
        private void picComposition_MouseMove(object sender, MouseEventArgs e)
        {
            if (clickSel)
            {
                pf = new Point(e.X, e.Y);
                picBigAudio.Invalidate();
            }
        }

        private void picComposition_MouseUp(object sender, MouseEventArgs e)
        {
            clickSel = false;
            pf = new Point(e.X, e.Y);
            if (p0.X == pf.X && p0.Y == pf.Y)
            {
                p0 = new Point();
                pf = new Point();
            }
            
            if (p0.X < 0) p0 = new Point(0, 0);
            if (pf.X < 0) pf = new Point(0, 0);
            if (p0.X >= picBigAudio.Width) p0 = new Point(picBigAudio.Width - 1, 0);
            if (pf.X >= picBigAudio.Width) pf = new Point(picBigAudio.Width - 1, 0);
            picBigAudio.Invalidate();

            PickDetail();
        }
        #endregion

        #region Visual feedback on big audio
        Font f = new Font("Arial", 9, FontStyle.Bold);
        private void picBigAudio_Paint(object sender, PaintEventArgs e)
        {
            if (p0.X != 0 || pf.X != 0)
            {
                e.Graphics.FillRectangle(Brushes.LightGreen, Math.Min(p0.X, pf.X), 0, Math.Abs(p0.X - pf.X), picBigAudio.Height);
                if (saBigAudio != null)
                {
                    float t0 = Math.Min(p0.X, pf.X) / (float)picBigAudio.Width * saBigAudio.time[saBigAudio.time.Length - 1];
                    float tf = Math.Max(p0.X, pf.X) / (float)picBigAudio.Width * saBigAudio.time[saBigAudio.time.Length - 1];
                    e.Graphics.DrawString(Math.Round(t0, 2).ToString() + " s", f, Brushes.Black, Math.Min(p0.X, pf.X), 0);
                    e.Graphics.DrawString(Math.Round(tf, 2).ToString() + " s", f, Brushes.Black, Math.Max(p0.X, pf.X), 14);
                }
            }

            //draw progress if playing file
            if (saBigAudio != null) //(isPlayingFile)
            {
                float audioPosition = (float)curTime / saBigAudio.time[saBigAudio.time.Length - 1] * (picBigAudio.Width);
                e.Graphics.DrawLine(new Pen(Color.Red, 1), audioPosition, -1, audioPosition, picBigAudio.Height);
                e.Graphics.DrawString(Math.Round(curTime, 1).ToString() + "s ", f, Brushes.Black, 3, 3);
            }

            //show feedback about phonetic searches
            if (saBigAudio != null) DrawSearchResults(e.Graphics, picBigAudio.Width, picBigAudio.Height, saBigAudio.time[saBigAudio.time.Length - 1]);
        }
        #endregion

        #region Open, play, stop BigFile

        SampleAudio saBigAudio;
        private void btnOpenFileComment_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Rec|*.wav;*.mp3";

            if (Directory.Exists(PratiCantoForms.ClientFolder)) ofd.InitialDirectory = PratiCantoForms.ClientFolder;

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                //dont know audio spectre anymore
                bigAudioSpecToSearch = null;
                for (int k = 0; k < searchResults.Count; k++) searchResults[k] = null;

                saBigAudio = new SampleAudio(ofd.FileName);
                
                bgComputeBigAudioSpec.RunWorkerAsync();
                btnPlayOrigFile_Click(sender, e);
            }
        }

        private void btnPlayOrigFile_Click(object sender, EventArgs e)
        {
            if (saBigAudio == null) return;

            float t0 = Math.Min(p0.X, pf.X) / (float)picBigAudio.Width * saBigAudio.time[saBigAudio.time.Length - 1];
            float tf = Math.Max(p0.X, pf.X) / (float)picBigAudio.Width * saBigAudio.time[saBigAudio.time.Length - 1];
            if (p0.X == 0 && pf.X == 0) tf = saBigAudio.time[saBigAudio.time.Length - 1];

            SampleAudio.PlaySample ps = saBigAudio.Play(t0, tf);
            if (ps!=null) ps.PlayTimeFunc = DrawTime;
        }

        private void btnStopOrigFile_Click(object sender, EventArgs e)
        {
            if (saBigAudio!=null) saBigAudio.Stop();
        }

        System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
        double lastUpdt;
        double curTime;
        private void DrawTime(double time)
        {
            //return;
            if (!sw.IsRunning) sw.Start();
            if (sw.Elapsed.TotalSeconds - lastUpdt > 0.04)
            {
                curTime = time;
                picBigAudio.Invalidate();
                lastUpdt = sw.Elapsed.TotalSeconds;
                //if (saGL != null && wo != null) saGL.DrawCurPbTime((float)time);
            }
        }
        #endregion

        #region Manage selection of bigFile - audio details, play, stop

        SampleAudio saDetail;

        /// <summary>Shows detail of big file upon selection</summary>
        private void PickDetail()
        {
            if (saBigAudio == null) return;
            float t0 = Math.Min(p0.X, pf.X) / (float)picBigAudio.Width * saBigAudio.time[saBigAudio.time.Length - 1];
            float tf = Math.Max(p0.X, pf.X) / (float)picBigAudio.Width * saBigAudio.time[saBigAudio.time.Length - 1];

            if (saDetail != null) saDetail.Stop();

            saDetail = PickSample(saBigAudio, t0, tf);
            if (saDetail != null) saDetail.RecomputeSpectre();
            //saDetail.ComputeBaseFreqAndFormants(0, saDetail.time[saDetail.time.Length - 1], txtDPenalty.Text, txtChangePenalty.Text, txtNFreqs.Text, txtVarPenalty.Text, true);


            saGL.SetSampleAudio(saDetail);
        }

        private static SampleAudio PickSample(SampleAudio original, float t0, float tf)
        {
            float sampleTime = original.time[1];
            int idx0 = (int)(t0 / sampleTime);
            int idxf = (int)(tf / sampleTime);
            if (idxf - idx0 <= 0) return null;

            List<float> detailTime = new List<float>();
            List<float> detailData = new List<float>();
            double curTime = 0;
            for (int k = idx0; k < idxf; k++)
            {
                curTime = original.time[k - idx0];
                detailTime.Add((float)curTime);
                detailData.Add(original.audioData[k]);
            }

            SampleAudio newSample = new SampleAudio(detailTime.ToArray(), detailData.ToArray(), original.waveFormat);

            return newSample;
        }

        private void frmInspector_Resize(object sender, EventArgs e)
        {
            if (saGL != null)
            {
                saGL.ResetDrawingRanges();
                saGL.UpdateDrawing();
            }
        }

        SampleAudio.PlaySample ps;
        //NAudio.Wave.WaveOut wo;
        private void btnPlaySelec_Click(object sender, EventArgs e)
        {
            if (saDetail != null)
            {
                if (saGL.p0SelX != saGL.pfSelX)
                    ps = saDetail.Play((double)saGL.p0SelX, (double)saGL.pfSelX);
                else ps = saDetail.Play();
                //ps.PlayTimeFunc = DrawTime2;
                //saGL.DrawCurrentPlaybackTime = true;
            }
        }


        //System.Diagnostics.Stopwatch sw2 = new System.Diagnostics.Stopwatch();
        //double lastUpdt2 = 0;
        ///// <summary>Draws time when playing. Receives total audio time (in original file)</summary>
        ///// <param name="time">Time to draw</param>
        //private void DrawTime2(double time)
        //{
        //    if (!sw2.IsRunning) sw2.Start();
        //    if (sw2.Elapsed.TotalSeconds - lastUpdt2 > 0.005)
        //    {
        //        lastUpdt = sw2.Elapsed.TotalSeconds;
        //        // if (saGL != null && ps != null) saGL.DrawCurPbTime((float)time, false);
        //    }
        //    else
        //    {
        //    }
        //}


        private void btnStopSelec_Click(object sender, EventArgs e)
        {
            if (saDetail != null) saDetail.Stop();
        }
        #endregion

        #region Find regions of interest

        void DrawSearchResults(Graphics g, int w, int h, float audioLen)
        {

            //find max/min similarities over all checked items

            float simMax = float.MinValue;
            float simMin = float.MaxValue;
            for (int i = 0; i < chkListSearch.Items.Count; i++)
            {
                if (chkListSearch.GetItemChecked(i) && searchResults[i] != null)
                {
                    foreach (PhonemeRecognition.SpectrogramFoundRegion sfr in searchResults[i])
                    {
                        simMax = Math.Max(simMax, sfr.Difference);
                        simMin = Math.Min(simMin, sfr.Difference);
                    }
                }
            }

            int MAXRESULTSPERLINE = (int)numMaxResultsPerRegion.Value;
            g.FillRectangle(Brushes.Black, 0, h * 0.667f, w, h * 0.333f);

            for (int i = 0; i < chkListSearch.Items.Count; i++)
            {
                if (chkListSearch.GetItemChecked(i) && searchResults[i] != null)
                {
                    for (int k = 0; k < Math.Min(MAXRESULTSPERLINE, searchResults[i].Count); k++)
                    {
                        PhonemeRecognition.SpectrogramFoundRegion sfr = searchResults[i][k];
                        
                        //float[] c = BuildVisualization.FcnGetColor(sfr.Difference, simMin, simMax);
                        float monoDif = 1.0f - (sfr.Difference - simMin) / (simMax - simMin);
                        float[] c = new float[] { monoDif, monoDif, monoDif };

                        Color c2 = Color.FromArgb((int)(255.0 * c[0]), (int)(255.0 * c[1]), (int)(255.0 * c[2]));


                        float p0x = (float)sfr.t0 / audioLen * (float)w;
                        float pfx = (float)sfr.tf / audioLen * (float)w;
                        g.FillRectangle(new SolidBrush(c2), p0x, h * 0.68f, pfx - p0x, h * 0.333f);
                        //g.DrawRectangle(Pens.Black, p0x, h * 0.667f, pfx - p0x, h * 0.333f);

                    }
                }
            }
        }

        List<SampleAudio> audiosToSearch = new List<SampleAudio>();
        List<List<PhonemeRecognition.SpectrogramFoundRegion>> searchResults = new List<List<PhonemeRecognition.SpectrogramFoundRegion>>();

        private void btnAddSel_Click(object sender, EventArgs e)
        {
            if (saGL.p0SelX == saGL.pfSelX) return;

            audiosToSearch.Add(PickSample(saDetail, saGL.p0SelX, saGL.pfSelX));
            chkListSearch.Items.Add(txtDescrip.Text);
            chkListSearch.SetItemChecked(chkListSearch.Items.Count - 1, true);

            searchResults.Add(null);
        }

        float progress = 0;
        private void SetProgress(float prog)
        {
            progress = prog;
        }

        //compute big audio spectre in background
        bool isComputingBigAudioSpectre = false;
        void ComputebigAudioSpectre()
        {
            //compute big audio spectre
            if (bigAudioSpecToSearch == null && !isComputingBigAudioSpectre)
            {
                isComputingBigAudioSpectre = true;

                bigAudioSpecToSearch = AudioIdentifRecog.ExtractFeatures(saBigAudio.audioData, 0, saBigAudio.audioData.Length,
                    saBigAudio.waveFormat.SampleRate, SampleAudio.FFTSamples, out bigAudioTStep, false);


                PhonemeRecognition.SpectrogramNormalize(bigAudioSpecToSearch);

                isComputingBigAudioSpectre = false;
            }
        }
        private void bgComputeBigAudioSpec_DoWork(object sender, DoWorkEventArgs e)
        {
            ComputebigAudioSpectre();
        }


        List<float[]> bigAudioSpecToSearch; double bigAudioTStep;
        private void btnFindInterestRegions_Click(object sender, EventArgs e)
        {
            //FindInterestRegions();
            bgwFindRegion.RunWorkerAsync();
            btnFindInterestRegions.Enabled = false;
        }
        private void bgwFindRegion_DoWork(object sender, DoWorkEventArgs e)
        {
            FindInterestRegions();
        }
        void bgwFindRegion_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            btnFindInterestRegions.Enabled = true;

            StructureSearchResults();

            //btnFindInterestRegions.Text = origText;

            msgToSet = origText;
            if (btnFindInterestRegions.InvokeRequired)
            {
                this.Invoke(delegSetMsg);
            }
            else SetMsg();


            lblProgress.Visible = false; timer1.Enabled = false;
            picBigAudio.Invalidate();
        }

        string origText;
        string msgToSet;
        delegate void delSetMsg();
        delSetMsg delegSetMsg;
        private void SetMsg()
        {
            btnFindInterestRegions.Text = msgToSet;
            btnFindInterestRegions.Refresh();
        }

        private void FindInterestRegions()
        {
            //lblProgress.Visible = true; timer1.Enabled = true;

            origText = btnFindInterestRegions.Text;
            string[] msgs = lblFindInfo.Text.ToUpper().Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);

            msgToSet = msgs[0];
            if (btnFindInterestRegions.InvokeRequired)
            {
                this.Invoke(delegSetMsg);
            }
            else SetMsg();
            //btnFindInterestRegions.Text = msgs[0];
            //btnFindInterestRegions.Refresh();

            if (saBigAudio == null) return;

            //AudioIdentifRecog.setProgress = SetProgress;
            msgToSet = msgs[1];
            if (btnFindInterestRegions.InvokeRequired)
            {
                this.Invoke(delegSetMsg);
            }
            else SetMsg();
            //btnFindInterestRegions.Text = msgs[1];
            //btnFindInterestRegions.Refresh();
            ComputebigAudioSpectre();
            while (isComputingBigAudioSpectre)
            {
                //cant go on without this
            }

            ////compute spectre for each region
            //for (int i = 0; i < chkListSearch.Items.Count; i++)
            //{
            //    if (chkListSearch.GetItemChecked(i) && searchResults[i] == null)
            //    {
            //        btnFindInterestRegions.Text = msgs[2].Replace("REGNUMBER", (i + 1).ToString());
            //        btnFindInterestRegions.Refresh();




            //        double tStep = 0;
            //        List<float[]> specToLookFor = AudioIdentifRecog.ExtractFeatures(audiosToSearch[i].audioData, 0, audiosToSearch[i].audioData.Length, audiosToSearch[i].waveFormat.SampleRate, SampleAudio.FFTSamples, out tStep, false);
            //        PhonemeRecognition.SpectrogramNormalize(specToLookFor);

            //        List<List<float[]>> lstSpecToLookFor = new List<List<float[]>>();
            //        lstSpecToLookFor.Add(specToLookFor);

            //        List<PhonemeRecognition.SpectrogramFoundRegion> sfr = PhonemeRecognition.SpectrogramSearch(null, saBigAudio.time[1], 0, saBigAudio.time[saBigAudio.time.Length - 1],
            //            lstSpecToLookFor, bigAudioSpecToSearch, ref bigAudioTStep);

            //        searchResults[i] = sfr;
            //    }

            //}

            //compute spectre for each region
            for (int i = 0; i < chkListSearch.Items.Count; i++)
            {
                if (chkListSearch.GetItemChecked(i) && searchResults[i]==null)
                {
                    //btnFindInterestRegions.Text = msgs[2].Replace("REGNUMBER", (i + 1).ToString());
                    //btnFindInterestRegions.Refresh();
                    msgToSet = msgs[2].Replace("REGNUMBER", (i + 1).ToString());
                    if (btnFindInterestRegions.InvokeRequired)
                    {
                        this.Invoke(delegSetMsg);
                    }
                    else SetMsg();


                    double tStep = 0;
                    List<float[]> specToLookFor = AudioIdentifRecog.ExtractFeatures(audiosToSearch[i].audioData, 0, audiosToSearch[i].audioData.Length, audiosToSearch[i].waveFormat.SampleRate, SampleAudio.FFTSamples, out tStep, false);
                    PhonemeRecognition.SpectrogramNormalize(specToLookFor);

                    List<List<float[]>> lstSpecToLookFor = new List<List<float[]>>();
                    lstSpecToLookFor.Add(specToLookFor);

                    double sampleTime = 1.0 / (double)saGL.sampleAudio.waveFormat.SampleRate;

                    List<PhonemeRecognition.SpectrogramFoundRegion> sfr = PhonemeRecognition.SpectrogramSearch(null, sampleTime, 0, saBigAudio.time[saBigAudio.time.Length - 1],
                        lstSpecToLookFor, bigAudioSpecToSearch, ref bigAudioTStep);

                    searchResults[i] = sfr;

                    
                }
                
            }


        }

        /// <summary>Regions to display in list</summary>
        List<PhonemeRecognition.SpectrogramFoundRegion> lstRegionsDisplayList = new List<PhonemeRecognition.SpectrogramFoundRegion>();

        /// <summary>Structures search results to show </summary>
        private void StructureSearchResults()
        {
            lstSearchRes.Items.Clear();
            lstRegionsDisplayList.Clear();

            int MAXRESULTSPERLINE = (int)numMaxResultsPerRegion.Value;

            for (int i = 0; i < chkListSearch.Items.Count; i++)
            {
                if (chkListSearch.GetItemChecked(i) && searchResults[i] != null)
                {
                    for (int k = 0; k < Math.Min(MAXRESULTSPERLINE, searchResults[i].Count); k++)
                    {
                        PhonemeRecognition.SpectrogramFoundRegion sfr = searchResults[i][k];
                        sfr.Label = chkListSearch.Items[i].ToString();
                        lstRegionsDisplayList.Add(sfr);
                    }
                }
            }

            lstRegionsDisplayList = lstRegionsDisplayList.OrderBy(i => i.Difference).ToList<PhonemeRecognition.SpectrogramFoundRegion>();

            //remove time overlaps
            for (int i = 0; i < lstRegionsDisplayList.Count; i++)
            {
                bool hasOverlap = false;

                //check if lstRegionsDisplayList[i] has overlap with any region from 0 to i-1
                for (int j = 0; j < i; j++)
                {
                    if ((lstRegionsDisplayList[j].t0 <= lstRegionsDisplayList[i].t0 && lstRegionsDisplayList[i].t0 <= lstRegionsDisplayList[j].tf) ||
                        (lstRegionsDisplayList[j].t0 <= lstRegionsDisplayList[i].tf && lstRegionsDisplayList[i].tf <= lstRegionsDisplayList[j].tf))
                    {
                        hasOverlap = true;
                        break;
                    }
                }

                if (hasOverlap)
                {
                    lstRegionsDisplayList.RemoveAt(i);
                    i--;
                }
            }

            foreach (PhonemeRecognition.SpectrogramFoundRegion sfr in lstRegionsDisplayList)
            {
                lstSearchRes.Items.Add(Math.Round(sfr.Difference, 1).ToString().PadLeft(10, ' ') + " " + sfr.Label);
            }
        }

        private void chkListSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            picBigAudio.Invalidate();
        }


        private void numMaxResultsPerRegion_ValueChanged(object sender, EventArgs e)
        {
            StructureSearchResults();
            picBigAudio.Invalidate();
        }

        private void lstSearchRes_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstSearchRes.SelectedIndex >= 0)
            {
                PhonemeRecognition.SpectrogramFoundRegion sfr = lstRegionsDisplayList[lstSearchRes.SelectedIndex];

                int x0 = (int)(sfr.t0 / saBigAudio.time[saBigAudio.time.Length - 1] * picBigAudio.Width);
                int xf = (int)(sfr.tf / saBigAudio.time[saBigAudio.time.Length - 1] * picBigAudio.Width);

                p0 = new Point(x0, 0);
                pf = new Point(xf, 0);
                picBigAudio.Invalidate();

                btnStopOrigFile_Click(sender, e);

                if (saBigAudio != null)
                {
                    SampleAudio.PlaySample ps = saBigAudio.Play(sfr.t0, sfr.tf);
                    if (ps != null) ps.PlayTimeFunc = DrawTime;
                }
            }
        }
        #endregion


        #region Cosmetics

        private void AdjButtons()
        {

            LayoutFuncs.AdjustButtonGraphic(btnOpenFileComment, false);
            LayoutFuncs.AdjustButtonGraphic(btnPlayOrigFile, false);
            LayoutFuncs.AdjustButtonGraphic(btnPlayRegion, false);
            //LayoutFuncs.AdjustButtonGraphic(btnPauseOrigFile, false);
            LayoutFuncs.AdjustButtonGraphic(btnStopOrigFile, false);
            LayoutFuncs.AdjustButtonGraphic(btnPlaySelec, false);
            LayoutFuncs.AdjustButtonGraphic(btnStopSelec, false);
            LayoutFuncs.AdjustButtonGraphic(btnAddSel, false);
            LayoutFuncs.AdjustButtonGraphic(btnFindInterestRegions, true);
            btnFindInterestRegions.ForeColor = Color.White;

        }


        private void gbChooseFileOpen_Paint(object sender, PaintEventArgs e)
        {
            PratiCanto.LayoutFuncs.DoGroupBoxLayout((GroupBox)sender, e.Graphics);
        }
        private void splitContainer_Panel1_Paint(object sender, PaintEventArgs e)
        {

            PratiCanto.LayoutFuncs.MakeDegrade(e.Graphics, Color.White, Color.FromArgb(230, 230, 230), splitContainer.Panel1.Width, splitContainer.Panel1.Height, true);
        }

        private void splitContainer_Panel2_Paint(object sender, PaintEventArgs e)
        {
            PratiCanto.LayoutFuncs.MakeDegrade(e.Graphics, Color.White, Color.FromArgb(230, 230, 230), splitContainer.Panel2.Width, splitContainer.Panel2.Height, true);
        }
        #endregion

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblProgress.Text = Math.Round(100.0 * progress, 2).ToString() + "%";
            Application.DoEvents();
        }

        private void btnPlayRegion_Click(object sender, EventArgs e)
        {
            if (chkListSearch.SelectedIndex >= 0)
            {
                audiosToSearch[chkListSearch.SelectedIndex].Play();
            }
        }

        private void frmInspector_FormClosing(object sender, FormClosingEventArgs e)
        {
            btnStopOrigFile_Click(sender, e);
            btnStopSelec_Click(sender, e);
        }
























    }
}
