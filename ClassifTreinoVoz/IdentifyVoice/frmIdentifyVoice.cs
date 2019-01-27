using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AudioComparer;
using NAudio.Wave;
using System.Diagnostics;
using System.IO;

namespace ClassifTreinoVoz
{
    public partial class frmIdentifyVoice : Form
    {
        /// <summary>Voice categories. Used for language localization</summary>
        string[] voiceCategories;

        /// <summary>Temporary, to test</summary>
        private void TEMP()
        {
            VoiceTestAnalysis vta = new VoiceTestAnalysis(130, 500, true, voiceCategories);
            picVoiceType.Image = vta.GetClassificationImage(picVoiceType.Width, "?", "Tenor", lblExtension.Text + " " + lblVoiceType.Text);

            BuildReportText();
        }

        public frmIdentifyVoice()
        {
            InitializeComponent();
        }

        /// <summary>Initial note in list. Halfsteps above A4</summary>
        int K0 = -31;
        private void frmIdentifyVoice_Load(object sender, EventArgs e)
        {
            DoButtonCosmetics();

            AudioComparer.SoftwareKey.CheckLicense("VoiceExtension", false);


            this.ClientFolder = PratiCantoForms.ClientFolder;

            btnRefreshMics_Click(this, e);

            for (int k = K0; k < 10; k++)
            {
                double f = SoundSynthesis.Note.GetFrequency(k);
                cmbInitNote.Items.Add(SoundSynthesis.Note.GetNoteName(f)) ;// + " " + f.ToString());
            }
            cmbInitNote.SelectedIndex = 10;

            CreateTestMelody();

            voiceCategories = lblVoiceCats.Text.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            //foreach (string cat in voiceCategories) cmbVoiceAnatomicInfo.Items.Add(cat);
            //cmbVoiceAnatomicInfo.SelectedIndex = 0;

            TEMP();
        }

        private void tabWiz_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!chkDisclaimer.Checked) tabWiz.SelectedIndex = 0;
        }

        #region Tab0 - Introduction
        private void chkDisclaimer_CheckedChanged(object sender, EventArgs e)
        {
            btnNext0.Enabled = chkDisclaimer.Checked;
        }
        private void btnNext0_Click(object sender, EventArgs e)
        {
            tabWiz.SelectedIndex = 1;
        }
        #endregion

        #region Tab1 - Params
        private void btnBack1_Click(object sender, EventArgs e)
        {
            tabWiz.SelectedIndex = 0;
        }
        private void btnNext1_Click(object sender, EventArgs e)
        {
            tabWiz.SelectedIndex = 2;
        }

        private void btnRefreshMics_Click(object sender, EventArgs e)
        {
            List<string> mics = SampleAudio.RealTime.GetMicrophones();
            cmbMics.Items.Clear();
            foreach (string s in mics) cmbMics.Items.Add(s);

            if (cmbMics.Items.Count > 0)
            {
                cmbMics.SelectedIndex = 0;
                btnNext1.Enabled = true;
                gbParams.Enabled = true;
            }
            else
            {
                btnNext1.Enabled = false;
                gbParams.Enabled = false;
            }

            
        }


        #region Identify voice frequency
        string prevBtnIdentText;
        private void btnIdentify_Click(object sender, EventArgs e)
        {
            if (gs == null) gs = new GuidedSinging(cmbMics.SelectedIndex, IdentFreqDataReceived, this.ssHearTestMelody);
            if (!gs.IsIdentifyingFreq)
            {
                gs.IdentifyFreq(cmbMics.SelectedIndex, IdentFreqDataReceived);

                prevBtnIdentText = btnIdentify.Text;
            }
            else
            {
                gs.StopIdentFreq();
                btnIdentify.Text = prevBtnIdentText;
            }
        }
        private void IdentFreqDataReceived()
        {
            if (gs.IsIdentifyingFreq)
            {
                btnIdentify.Text = Math.Round(gs.identifyCurFreq, 1).ToString();
            }
            else
            {
                btnIdentify.Text = prevBtnIdentText;

                int noteId = (int)Math.Round(SoundSynthesis.Note.GetHalfStepsAboveA4(gs.identifyCurFreq));
                int idx = noteId - K0;
                idx = Math.Max(0, idx);
                idx = Math.Min(idx, cmbInitNote.Items.Count);

                cmbInitNote.SelectedIndex = idx;
            }
            
        }
        
        #endregion

        #region Play test melody

        SoundSynthesis ssHearTestMelody;
        private void CreateTestMelody()
        {
            SoundSynthesis.Note a1 = new SoundSynthesis.Note(0.25, SoundSynthesis.Note.GetFrequency(0), noteWaveShape);
            SoundSynthesis.Note p1 = new SoundSynthesis.Note(0.75, 0, noteWaveShape);
            SoundSynthesis.Note p2 = new SoundSynthesis.Note(0.25, 0, noteWaveShape);
            SoundSynthesis.Note n0 = new SoundSynthesis.Note(1.75, SoundSynthesis.Note.GetFrequency(0), noteWaveShape);
            SoundSynthesis.Note n1 = new SoundSynthesis.Note(0.5, SoundSynthesis.Note.GetFrequency(0), noteWaveShape);
            SoundSynthesis.Note n2 = new SoundSynthesis.Note(0.5, SoundSynthesis.Note.GetFrequency(2), noteWaveShape);
            SoundSynthesis.Note n3 = new SoundSynthesis.Note(0.5, SoundSynthesis.Note.GetFrequency(4), noteWaveShape);
            SoundSynthesis.Note n5 = new SoundSynthesis.Note(1.0, SoundSynthesis.Note.GetFrequency(0), noteWaveShape);

            ssHearTestMelody = new SoundSynthesis();

            ssHearTestMelody.Notes.Add(new List<SoundSynthesis.Note>());
            ssHearTestMelody.Notes[0].Add(a1);
            ssHearTestMelody.Notes[0].Add(p1);
            ssHearTestMelody.Notes[0].Add(a1);
            ssHearTestMelody.Notes[0].Add(p1);

            ssHearTestMelody.Notes[0].Add(n0);
            ssHearTestMelody.Notes[0].Add(p2);
            ssHearTestMelody.Notes[0].Add(n1); ssHearTestMelody.Notes[0].Add(n2); ssHearTestMelody.Notes[0].Add(n3);
            ssHearTestMelody.Notes[0].Add(n2); 

            ssHearTestMelody.Notes[0].Add(n5); 
        }
        private void numBaseTempo_ValueChanged(object sender, EventArgs e)
        {
            ssHearTestMelody.Tempo = (double)numBaseTempo.Value;
        }
        WaveOut wo;
        private void btnPlayMelody_Click(object sender, EventArgs e)
        {
            ssHearTestMelody.Tempo = (double)numBaseTempo.Value;

            if (wo == null)
            {
                wo = new WaveOut();
                wo.Init(ssHearTestMelody.sampleSynthesizer);
            }
            ssHearTestMelody.sampleSynthesizer.Reset();
            wo.Play();
            wo.PlaybackStopped += new EventHandler<StoppedEventArgs>(wo_PlaybackStopped);
            btnPlayMelody.Enabled = false;
        }
        private void cmbInitNote_SelectedIndexChanged(object sender, EventArgs e)
        {
            double nHalfSteps = cmbInitNote.SelectedIndex + K0;
            if (ssHearTestMelody != null)
            {
                ssHearTestMelody.ModulateSound(SoundSynthesis.Note.GetFrequency(nHalfSteps));
                ssHearTestMelody.Tempo = (double)numBaseTempo.Value;
            }

            picBaseMelody.Invalidate();
        }
        void wo_PlaybackStopped(object sender, StoppedEventArgs e)
        {
            btnPlayMelody.Enabled = true;
            ssHearTestMelody.sampleSynthesizer.Reset();
        }
        
        private void picBaseMelody_Paint(object sender, PaintEventArgs e)
        {
            ssHearTestMelody.DrawTrack(e.Graphics, 0,0,0, picBaseMelody.Width, picBaseMelody.Height, 1000);
        }
        #endregion



        //Load custom melody
        SoundSynthesis.WaveShape noteWaveShape = new SoundSynthesis.WaveShapePiano();

        private void btnLoadMelody_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Melody|*.mld";
            ofd.InitialDirectory = Application.StartupPath;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                string s = System.IO.File.ReadAllText(ofd.FileName);
                ssHearTestMelody.Notes[0].Clear();
                string[] ss = s.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string s2 in ss)
                {
                    try
                    {
                        SoundSynthesis.Note n = SoundSynthesis.Note.FromString(s2, noteWaveShape);
                        ssHearTestMelody.Notes[0].Add(n);
                    }
                    catch
                    {
                    }
                }

                //No notes loaded, recreate base test melody
                if (ssHearTestMelody.Notes[0].Count == 0) CreateTestMelody();

                picBaseMelody.Invalidate();
            }
        }

 

        #endregion

        #region Tab2 - Vocal test

        private void btnBack2_Click(object sender, EventArgs e)
        {
            tabWiz.SelectedIndex = 1;
        }
        private void btnNext2_Click(object sender, EventArgs e)
        {
            tabWiz.SelectedIndex = 3;
        }
        private void picVocalTest_Paint(object sender, PaintEventArgs e)
        {
            float maxF = 1300;
            ssHearTestMelody.DrawTrack(e.Graphics, 0,0,0, picVocalTest.Width, picVocalTest.Height, maxF);

            if (gs != null && gs.isPlayingGuidedSinging)
            {
                float scalex = (float)(picVocalTest.Width / gs.trackPlayTime);
                float scaley = (float)(picVocalTest.Height / maxF);
                List<PointF> plotData = new List<PointF>();
                foreach (PointF p in gs.acqFreqSamples) plotData.Add(new PointF(p.X * scalex, picVocalTest.Height - p.Y * scaley));

                if (plotData.Count > 1) e.Graphics.DrawLines(Pens.Blue, plotData.ToArray());

                e.Graphics.DrawLine(Pens.Red, gs.lastTime * scalex, 0, gs.lastTime * scalex, picVocalTest.Height);

                e.Graphics.DrawString("F0:" + gs.lastFreq.ToString() + " I:" + gs.lastIntens.ToString() + "dB", new Font("Arial", 10, FontStyle.Bold), Brushes.Black, 0, 0);
            }
        }

        /// <summary>Current number of half steps above A4 (to compute frequencies)</summary>
        double curHalfSteps;
        /// <summary>Number of half steps to increment frequency</summary>
        int nHalfStepsIncrement;

        /// <summary>Increase frequency?</summary>
        bool incFreq = true;

        /// <summary>Maximum frequency achieved in halfsteps above A4</summary>
        double maxHalfSteps = 0;

        /// <summary>How many times sample was rejected?</summary>
        int nRejections;

        /// <summary>Class to guide singing</summary>
        GuidedSinging gs;

        /// <summary>Sequence Id</summary>
        int seqId = 1;

        private void btnStartTest_Click(object sender, EventArgs e)
        {
            gs = new GuidedSinging(cmbMics.SelectedIndex, woTest_PlaybackStopped, ssHearTestMelody);

            curHalfSteps = cmbInitNote.SelectedIndex + K0;
            nHalfStepsIncrement = (int)numNoteInc.Value;

            maxHalfSteps = curHalfSteps;
            incFreq = true;

            testTimer.Enabled = true;

            btnPauseTest.Enabled = true;
            btnStopTest.Enabled = true;
            btnStartTest.Enabled = false;

            gs.StartGuidedSinging(curHalfSteps, seqId.ToString() + " ");
            seqId++;
        }


        void woTest_PlaybackStopped()
        {
            if (gs.isPlayingGuidedSinging)
            {
                //verify if we accept this sample - at least 50% of points accepted
                if ((float)gs.nAcceptedPts / (float)gs.nAcqPts > 0.5f)
                {
                    if (incFreq)
                    {
                        maxHalfSteps = curHalfSteps;
                        curHalfSteps += nHalfStepsIncrement;
                    }
                    else curHalfSteps -= nHalfStepsIncrement;

                    nRejections = 0;
                }
                else
                {
                    //reject this sample
                    nRejections++;

                    if (nRejections > 1 && incFreq) //go back to initial frequency, set direction downwards the scale
                    {
                        curHalfSteps = cmbInitNote.SelectedIndex + K0;
                        incFreq = false;
                        nRejections = 0;
                    }
                    else if (nRejections > 1 && !incFreq) //we're done
                    {
                        SaveTestData();

                        btnStopTest_Click(this, new EventArgs());
                        return;
                    }
                }

                picVocalTest.Invalidate();

                gs.StartGuidedSinging(curHalfSteps, seqId.ToString() + " ");
                seqId++;
            }
        }
        private void testTimer_Tick(object sender, EventArgs e)
        {
            gs.AcquireFreqSample();
            picVocalTest.Invalidate();
        }

        private void btnStopTest_Click(object sender, EventArgs e)
        {
            btnStartTest.Enabled = true;
            btnPauseTest.Enabled = false;
            btnStopTest.Enabled = false;

            gs.Stop();
        }

        bool isPaused = false;
        private void btnPauseTest_Click(object sender, EventArgs e)
        {
            if (isPaused)
            {
                gs.Resume();
            }
            else
            {
                gs.Pause();
            }
            isPaused = !isPaused;
        }
        #endregion

        #region Tab3 - Report

        private void btnBack4_Click(object sender, EventArgs e)
        {
            tabWiz.SelectedIndex = 2;
        }
        private void btnFinish_Click(object sender, EventArgs e)
        {
            if (unsavedTest)
            {
                if (MessageBox.Show(lblUnsavedTest.Text, this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                {
                    btnSave_Click(sender, e);
                }
                else this.Close();
            }
            else this.Close();
        }
        #endregion

        #region Voice classification and analysis

        VoiceTestAnalysis vclass;
        private void SaveTestData()
        {
            //find lowest frequency achieved
            curHalfSteps += nHalfStepsIncrement;
            ssHearTestMelody.ModulateSound(SoundSynthesis.Note.GetFrequency(curHalfSteps));

            double minFreq;
            minFreq = ssHearTestMelody.Notes[0][0].Frequency[0];
            foreach (SoundSynthesis.Note n in ssHearTestMelody.Notes[0]) if (n.Frequency[0] > 0) minFreq = Math.Min(minFreq, n.Frequency[0]);

            //find highest frequency achieved
            curHalfSteps = maxHalfSteps;
            ssHearTestMelody.ModulateSound(SoundSynthesis.Note.GetFrequency(curHalfSteps));
            double maxFreq;
            maxFreq = ssHearTestMelody.Notes[0][0].Frequency[0];
            foreach (SoundSynthesis.Note n in ssHearTestMelody.Notes[0]) maxFreq = Math.Max(maxFreq, n.Frequency[0]);


            string s = SoundSynthesis.Note.GetNoteName(minFreq) + " - " + SoundSynthesis.Note.GetNoteName(maxFreq);
            vclass = new VoiceTestAnalysis(minFreq, maxFreq, radioMale.Checked, voiceCategories);

            lblVoiceType.Text = vclass.ClassifyVoice();
            picVoiceType.Image = vclass.GetClassificationImage(picVoiceType.Width, txtSinger.Text, lblVoiceType.Text, lblExtension.Text + " " + lblVoiceType.Text);

            BuildReportText();

            tabWiz.SelectedIndex = 3;
            unsavedTest = true;
        }

        #endregion

        bool unsavedTest = false;

        /// <summary>Client folder</summary>
        public string ClientFolder = "";
        private void btnSave_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog bfd = new FolderBrowserDialog();
            if (ClientFolder != "")
            {
                bfd.SelectedPath = ClientFolder;
                string dtString = DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString().PadLeft(2, '0') + "-" + DateTime.Now.Day.ToString().PadLeft(2, '0');
                string tryFolder = ClientFolder + "\\Extension" + dtString;
                if (!Directory.Exists(tryFolder))
                {
                    try
                    {
                        Directory.CreateDirectory(tryFolder);
                        bfd.SelectedPath = tryFolder;
                    }
                    catch
                    {
                    }
                }
            }
            if (bfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Bitmap bmp = (Bitmap)picVoiceType.Image;

                bmp.Save(bfd.SelectedPath + "\\voice" + txtSinger.Text + ".png", System.Drawing.Imaging.ImageFormat.Png);
                gs.SaveGuidedSingingToFolder(bfd.SelectedPath);
                unsavedTest = false;

                File.WriteAllText(bfd.SelectedPath + "\\report" + txtSinger.Text + ".txt", txtReport.Text);
            }
            
            btnFinish.Enabled = true;
        }


        #region Build report

        private void BuildReportText()
        {
            string report = "";
            report += DateTime.Now.ToString() + "\r\n";
            report += lblEvaluator.Text + txtEvaluator.Text + "\r\n";
            report += lblSinger.Text + txtSinger.Text + "\r\n";
            report += lblExtension.Text + lblVoiceType.Text + "\r\n";

            txtReport.Text = report;
        }


        #endregion

        private void frmIdentifyVoice_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (wo != null) wo.Dispose();
            if (gs != null && gs.woGuide != null) gs.woGuide.Dispose();
        }

        #region Cosmetics
        void DoButtonCosmetics()
        {
            btnStartTest.FlatAppearance.BorderSize = 0;
            btnStartTest.Cursor = Cursors.Hand;
            btnStartTest.FlatStyle = FlatStyle.Flat;
            btnStartTest.FlatAppearance.BorderSize = 0;
            btnStartTest.ForeColor = Color.White;
            btnStartTest.Image = PratiCanto.LayoutFuncs.MakeDegrade(Color.FromArgb(218, 30, 30), Color.FromArgb(113, 40, 40), btnStartTest.Width, btnStartTest.Height, true);

            foreach (TabPage tp in tabWiz.TabPages)
            {
                foreach (Control c in tp.Controls)
                {
                    if (c is Button)
                    {
                        Button b = (Button)c;
                        b.Cursor = Cursors.Hand;
                        b.FlatStyle = FlatStyle.Flat;
                        b.FlatAppearance.BorderSize = 0;
                        if (b == btnSave)
                        {
                            b.Image = PratiCanto.LayoutFuncs.MakeDegrade(Color.FromArgb(218, 30, 30), Color.FromArgb(113, 40, 40), b.Width, b.Height, true);
                            b.ForeColor = Color.White;
                        }
                        else b.Image = PratiCanto.LayoutFuncs.MakeDegrade(Color.White, Color.FromArgb(223, 223, 223), b.Width, b.Height, true);
                    }
                }
            }
        }

        private void tabIntro_Paint(object sender, PaintEventArgs e)
        {
            PratiCanto.LayoutFuncs.MakeDegrade(e.Graphics, Color.White, Color.FromArgb(236, 236, 236), tabIntro.Width, tabIntro.Height, true);
        }
        private void tabTestParameters_Paint(object sender, PaintEventArgs e)
        {
            PratiCanto.LayoutFuncs.MakeDegrade(e.Graphics, Color.White, Color.FromArgb(236, 236, 236), tabTestParameters.Width, tabTestParameters.Height, true);
        }
        private void tabIdentification_Paint(object sender, PaintEventArgs e)
        {
            PratiCanto.LayoutFuncs.MakeDegrade(e.Graphics, Color.White, Color.FromArgb(236, 236, 236), tabIdentification.Width, tabIdentification.Height, true);
        }
        private void tabReport_Paint(object sender, PaintEventArgs e)
        {
            PratiCanto.LayoutFuncs.MakeDegrade(e.Graphics, Color.White, Color.FromArgb(236, 236, 236), tabReport.Width, tabReport.Height, true);
        }
        private void groupBox4_Paint(object sender, PaintEventArgs e)
        {
            PratiCanto.LayoutFuncs.MakeDegrade(e.Graphics, Color.FromArgb(236, 236, 236), Color.White, groupBox4.Width, groupBox4.Height, true);
        }

        private void groupBox2_Paint(object sender, PaintEventArgs e)
        {
            PratiCanto.LayoutFuncs.DoGroupBoxLayout((GroupBox)sender, e.Graphics);
        }
        private void groupBox1_Paint(object sender, PaintEventArgs e)
        {
            PratiCanto.LayoutFuncs.DoGroupBoxLayout((GroupBox)sender, e.Graphics);
        }

        private void gbParams_Paint(object sender, PaintEventArgs e)
        {
            PratiCanto.LayoutFuncs.DoGroupBoxLayout((GroupBox)sender, e.Graphics);
        }
        private void groupBox3_Paint(object sender, PaintEventArgs e)
        {
            PratiCanto.LayoutFuncs.DoGroupBoxLayout((GroupBox)sender, e.Graphics);
        }
        #endregion





















    }
}
