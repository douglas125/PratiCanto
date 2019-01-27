using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NAudio.Wave;

namespace ClassifTreinoVoz
{
    public partial class frmEditWaveForm : Form
    {
        public frmEditWaveForm()
        {
            InitializeComponent();
        }
        #region Initializations
        private void frmEditWaveForm_Load(object sender, EventArgs e)
        {
            //initializes graphic data
            initGraph();

            cmbWaveDuration.Items.Add((0.25).ToString());
            cmbWaveDuration.Items.Add((0.5).ToString());
            cmbWaveDuration.Items.Add((1).ToString());
            cmbWaveDuration.Items.Add((2).ToString());
            cmbWaveDuration.Items.Add((4).ToString());
            cmbWaveDuration.Items.Add((8).ToString());
            cmbWaveDuration.SelectedIndex = 2;

            initNoteCmb();
        }

        int K0 = -36;
        private void initNoteCmb()
        {
            for (int k = K0; k < 32; k++)
            {
                double f = SoundSynthesis.Note.GetFrequency(k);
                cmbWaveNote.Items.Add(SoundSynthesis.Note.GetNoteName(f));// + " " + f.ToString());
            }

            cmbWaveNote.SelectedIndex = 17;
        }
        #endregion

        /// <summary>Waveform parameters</summary>
        public List<float> wIntens, wIntensTime, wAmp, wAmpTime;

        int N = 800;
        private void initGraph()
        {
            wIntens = new List<float>();
            wIntensTime = new List<float>();
            wAmp = new List<float>();
            wAmpTime = new List<float>();
            
            for (int k = 0; k < N; k++)
            {
                float t = (float)k / (float)N;
                wIntensTime.Add(t);
                wAmpTime.Add(t);
                wIntens.Add((float)Math.Sin(2.0f * Math.PI * t));
                wAmp.Add((float)Math.Exp(-5*t));
            }
        }
        private void UpdateWAmpTime(float maxTime)
        {
            int nn = wAmpTime.Count;
            wAmpTime.Clear();
            for (int k = 0; k < nn; k++)
            {
                float t = maxTime * (float)k / (float)nn;
                wAmpTime.Add(t);
            }
        }

        #region Drawings
        private void picWaveShape_Paint(object sender, PaintEventArgs e)
        {
            if (wIntensTime != null)
            {
                e.Graphics.DrawLine(Pens.Blue, 0, picWaveShape.Height >> 1, picWaveShape.Width, picWaveShape.Height >> 1);

                Pen p = new Pen(Color.Black);
                p.Width=3;
                AudioComparer.SampleAudio.PlotToBmp(e.Graphics, wIntensTime.ToArray(), wIntens.ToArray(), 0, 1, -1, 1, p, picWaveShape.Width, picWaveShape.Height, 0, wIntensTime.Count);
            }
        }

        private void picWaveAmpTime_Paint(object sender, PaintEventArgs e)
        {
            if (wAmpTime != null)
            {
                e.Graphics.DrawLine(Pens.Blue, 0, picWaveAmpTime.Height, picWaveAmpTime.Width, picWaveAmpTime.Height);

                float maxT = wAmpTime[wAmpTime.Count - 1];

                Pen p = new Pen(Color.Black);
                p.Width = 3;
                AudioComparer.SampleAudio.PlotToBmp(e.Graphics, wAmpTime.ToArray(), wAmp.ToArray(), 0, maxT, 0, 1, p, picWaveAmpTime.Width, picWaveAmpTime.Height, 0, wIntensTime.Count);
            }

        }
        #endregion


        #region Curve editing
        bool clickWaveShape = false;
        private void picWaveShape_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left) clickWaveShape = true;
        }
        private void picWaveShape_MouseUp(object sender, MouseEventArgs e)
        {
            clickWaveShape = false;
        }

        private void picWaveShape_MouseMove(object sender, MouseEventArgs e)
        {
            if (clickWaveShape)
            {
                UpdateCurve(wIntensTime, wIntens, e.X, e.Y, picWaveShape.Width, picWaveShape.Height, -1, 1);
                picWaveShape.Invalidate();
            }
        }

        bool clickWaveAmp = false;
        private void picWaveAmpTime_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left) clickWaveAmp = true;
        }

        private void picWaveAmpTime_MouseUp(object sender, MouseEventArgs e)
        {
            clickWaveAmp = false;
        }

        private void picWaveAmpTime_MouseMove(object sender, MouseEventArgs e)
        {
            if (clickWaveAmp)
            {
                UpdateCurve(wAmpTime, wAmp, e.X, e.Y, picWaveAmpTime.Width, picWaveAmpTime.Height, 0, 1);
                picWaveAmpTime.Invalidate();
            }
        }

        private static void UpdateCurve(List<float> x, List<float> y, int XClick, int YClick, int Width, int Height, float y0, float yf)
        {
            int t0 = (int)(x.Count * (XClick - 1) / Width);
            int tf = (int)(x.Count * (XClick + 1) / Width);
            if (t0 < 0) t0 = 0; if (tf < 0) tf = 0;
            if (t0 > x.Count - 1) t0 = x.Count - 1;
            if (tf > x.Count - 1) tf = x.Count - 1;

            float yval = (float)(Height - YClick) / (float)Height * (yf - y0) + y0;
            if (yval < y0) yval = y0;
            if (yval > yf) yval = yf;
            for (int k = t0; k <= tf; k++) y[k] = yval;
        }
        private void frmEditWaveForm_Resize(object sender, EventArgs e)
        {
            picWaveShape.Invalidate();
            picWaveAmpTime.Invalidate();
        }

        #endregion


        #region Play notes
        SoundSynthesis sMelody;
        WaveOut woPlayMelody;
        bool isPlaying = false;
        private void btnPlay_Click(object sender, EventArgs e)
        {
            if (isPlaying) return;

            float maxTime = radioRel.Checked ? 1 : (float)numMaxTime.Value;
            UpdateWAmpTime(maxTime);

            //adjust intensity mean
            float yAvg = wIntens.Sum()/(float)wIntens.Count;
            for (int k = 0; k < wIntens.Count; k++) wIntens[k] -= yAvg;
            picWaveShape.Invalidate();

            if (sMelody == null)
            {
                sMelody = new SoundSynthesis();
                sMelody.Notes.Add(new List<SoundSynthesis.Note>());


                woPlayMelody = new WaveOut();
                woPlayMelody.Init(sMelody.sampleSynthesizer);
                woPlayMelody.PlaybackStopped += new EventHandler<StoppedEventArgs>(woPlayMelody_PlaybackStopped);
            }
            isPlaying = true;

            sMelody.Notes[0].Clear();
            string noteStr = cmbWaveNote.Items[cmbWaveNote.SelectedIndex].ToString() + " " + cmbWaveDuration.Items[cmbWaveDuration.SelectedIndex].ToString();

            SoundSynthesis.WaveShapeCustom wsCustom = new SoundSynthesis.WaveShapeCustom(radioRel.Checked, wIntens, wIntensTime, wAmp, wAmpTime);

            sMelody.Notes[0].Add(SoundSynthesis.Note.FromString(noteStr, wsCustom));
            sMelody.sampleSynthesizer.Reset();

            woPlayMelody.Play();
        }

        void woPlayMelody_PlaybackStopped(object sender, StoppedEventArgs e)
        {
            isPlaying = false;
        }
        #endregion

        #region Enable/disable time edition
        private void radioRel_CheckedChanged(object sender, EventArgs e)
        {
            float maxTime = radioRel.Checked ? 1 : (float)numMaxTime.Value;
            numMaxTime.Enabled = !radioRel.Checked;
            UpdateWAmpTime(maxTime);
        }
        private void numMaxTime_ValueChanged(object sender, EventArgs e)
        {
            UpdateWAmpTime((float)numMaxTime.Value);
        }
        private void radioAbs_CheckedChanged(object sender, EventArgs e)
        {

        }
        #endregion

        private void btnOpenFile_Click(object sender, EventArgs e)
        {
            string folder = Application.StartupPath;
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Wave Shape|*.wshape";

            ofd.InitialDirectory = folder;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                SoundSynthesis.WaveShapeCustom wsc = SoundSynthesis.WaveShapeCustom.FromFile(ofd.FileName);

                radioRel.Checked = wsc.isAmplitudeRelative;
                radioAbs.Checked = !wsc.isAmplitudeRelative;
                wIntens = wsc.waveIntens;
                wIntensTime = wsc.waveIntensTime;
                wAmp = wsc.waveAmp;
                wAmpTime = wsc.waveAmpTime;

                picWaveAmpTime.Invalidate();
                picWaveShape.Invalidate();
            }
        }

        private void btnSaveSings_Click(object sender, EventArgs e)
        {
            string folder = Application.StartupPath;
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Wave Shape|*.wshape";

            sfd.InitialDirectory = folder;
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                SoundSynthesis.WaveShapeCustom wsCustom = new SoundSynthesis.WaveShapeCustom(radioRel.Checked, wIntens, wIntensTime, wAmp, wAmpTime);
                wsCustom.WriteToFile(sfd.FileName);
            }
        }


        #region Cosmetics
        private void gbWaveShape_Paint(object sender, PaintEventArgs e)
        {
            PratiCanto.LayoutFuncs.DoGroupBoxLayout((GroupBox)sender, e.Graphics);
        }
        private void gbWaveAmplitude_Paint(object sender, PaintEventArgs e)
        {
            PratiCanto.LayoutFuncs.DoGroupBoxLayout((GroupBox)sender, e.Graphics );

        }
        #endregion




    }
}
