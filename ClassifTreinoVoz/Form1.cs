using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NAudio.Wave;
using AudioComparer;

using PSAMControlLibrary;
using System.IO;
using System.Drawing.Text;
using System.Diagnostics;

namespace ClassifTreinoVoz
{
    public partial class frmPractice : Form
    {


        public frmPractice()
        {
            InitializeComponent();
        }

        #region Initialize Font
        public void InitFont()
        {
            string font = Application.StartupPath + "\\Polihymnia.ttf";
            Polihymnia = LoadFont(new FileInfo(font), 20, FontStyle.Regular);

            PSAMControlLibrary.FontStyles.MusicFont = new Font(Polihymnia.FontFamily, 20);
            PSAMControlLibrary.FontStyles.GraceNoteFont = new Font(Polihymnia.FontFamily, 18);
            PSAMControlLibrary.FontStyles.StaffFont = new Font(Polihymnia.FontFamily, 23);
        }

        public static LoadedFont Polihymnia;
        public static LoadedFont LoadFont(FileInfo file, int fontSize, FontStyle fontStyle)
        {
            var fontCollection = new PrivateFontCollection();
            fontCollection.AddFontFile(file.FullName);
            if (fontCollection.Families.Length < 0)
            {
                throw new InvalidOperationException("No font familiy found when loading font");
            }

            var loadedFont = new LoadedFont();
            loadedFont.FontFamily = fontCollection.Families[0];
            loadedFont.Font = new Font(loadedFont.FontFamily, fontSize, fontStyle, GraphicsUnit.Pixel);
            return loadedFont;
        }
        public struct LoadedFont
        {
            public Font Font { get; set; }
            public FontFamily FontFamily { get; set; }
        }
        #endregion

        /// <summary>Melody viewer</summary>
        IncipitViewer melodyViewer = new IncipitViewer();
        private void Form1_Load(object sender, EventArgs e)
        {
            //initialize note combo
            initInitNoteCmb();


            txtMelodyName.Visible = false;

            SoftwareKey.CheckLicense(lblFindLicense.Text, lblNotRegistered.Text);

            //Initializes musical font
            InitFont();

            gbViewMelody.Controls.Add(melodyViewer);
            melodyViewer.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
            melodyViewer.Width = gbViewMelody.Width - 10 - btnPlayMelody.Width;
            melodyViewer.Top = btnPlayMelody.Top - (btnPlayMelody.Height>>2);
            melodyViewer.AddMusicalSymbol(new Clef(ClefType.GClef, 2));
            melodyViewer.PlayExternalMidiPlayer += new IncipitViewer.PlayExternalMidiPlayerDelegate(melodyViewer_PlayExternalMidiPlayer);
            //melodyViewer.MouseDown += new MouseEventHandler(melodyViewer_MouseDown);

            //MusicalSymbol ms = new PSAMControlLibrary.Barline();
            MusicalSymbol ms = new PSAMControlLibrary.TimeSignature(TimeSignatureType.Common, 4, 4);
            melodyViewer.AddMusicalSymbol(ms);

            //Initializes melody
            InitMelody();

            //List mics
            List<string> mics = SampleAudio.RealTime.GetMicrophones();
            foreach (string s in mics) cmbInputDevice.Items.Add(s);
            if (mics.Count <= 1) cmbInputDevice.Visible = false;
            if (mics.Count >= 1) cmbInputDevice.SelectedIndex = 0;
            //initialize guided singing
            initGuidedSinging();


            ////Show choose dialog
            //frmChooseActivity frmChoose = new frmChooseActivity();
            //frmChoose.ShowDialog();
        }


        private void Form1_Resize(object sender, EventArgs e)
        {
            picViewMelody.Invalidate();

        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void voiceTypeWizardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmIdentifyVoice frmiv = new frmIdentifyVoice();
            frmiv.ClientFolder = this.txtClientFolder;
            frmiv.ShowDialog();
        }
        private void btnAudioAnalyzer_Click(object sender, EventArgs e)
        {
            frmAudioComparer frmAudioComp = new frmAudioComparer();
            frmAudioComp.Show();
        }
        private void btnExtensionWizard_Click(object sender, EventArgs e)
        {
            voiceTypeWizardToolStripMenuItem_Click(sender, e);
        }
        private void btnEditMelodies_Click(object sender, EventArgs e)
        {
            frmEditMelody frmEditMelody = new frmEditMelody();

            frmEditMelody.txtNotes.Text = GetMelodyString();
            frmEditMelody.btnUpdate_Click(sender, e);

            frmEditMelody.ShowDialog();
        }

        #region Tests
        void teste()
        {
            List<string> notes = new List<string>();
            for (int k = -30; k < 30; k++)
            {
                double f = SoundSynthesis.Note.GetFrequency(k);
                notes.Add(SoundSynthesis.Note.GetNoteName(f) + " " + f.ToString());
            }

            SoundSynthesis ss = new SoundSynthesis();
            ss.Tempo = 1;

            //SoundSynthesis.Note n1 = new SoundSynthesis.Note(1, SoundSynthesis.Note.GetFrequency(0), new SoundSynthesis.WaveShapeSquare(0.8));
            SoundSynthesis.Note n1 = new SoundSynthesis.Note(1.5, SoundSynthesis.Note.GetFrequency(0), new SoundSynthesis.WaveShapeSine());
            SoundSynthesis.Note n2 = new SoundSynthesis.Note(1.5, SoundSynthesis.Note.GetFrequency(2), new SoundSynthesis.WaveShapeSine());
            SoundSynthesis.Note n3 = new SoundSynthesis.Note(1.5, SoundSynthesis.Note.GetFrequency(4), new SoundSynthesis.WaveShapeSine());
            SoundSynthesis.Note n4 = new SoundSynthesis.Note(1.5, SoundSynthesis.Note.GetFrequency(4), new SoundSynthesis.WaveShapeSine());
            SoundSynthesis.Note n5 = new SoundSynthesis.Note(1.5, SoundSynthesis.Note.GetFrequency(6), new SoundSynthesis.WaveShapeSine());
            SoundSynthesis.Note n6 = new SoundSynthesis.Note(1.5, SoundSynthesis.Note.GetFrequency(8), new SoundSynthesis.WaveShapeSine());
            SoundSynthesis.Note n7 = new SoundSynthesis.Note(1.5, SoundSynthesis.Note.GetFrequency(7), new SoundSynthesis.WaveShapeSine());
            SoundSynthesis.Note n8 = new SoundSynthesis.Note(1.5, SoundSynthesis.Note.GetFrequency(9), new SoundSynthesis.WaveShapeSine());
            SoundSynthesis.Note n9 = new SoundSynthesis.Note(1.5, SoundSynthesis.Note.GetFrequency(11), new SoundSynthesis.WaveShapeSine());
            SoundSynthesis.Note silence = new SoundSynthesis.Note(0.3, 0, new SoundSynthesis.WaveShapeSine());

            //ss.Notes.Add(n2); ss.Notes.Add(n2); ss.Notes.Add(n2); ss.Notes.Add(n2); ss.Notes.Add(n2);
            ss.Notes.Add(new List<SoundSynthesis.Note>());
            ss.Notes.Add(new List<SoundSynthesis.Note>());
            ss.Notes.Add(new List<SoundSynthesis.Note>());
            ss.Notes[1].Add(n1); ss.Notes[1].Add(n2); ss.Notes[1].Add(n3);
            ss.Notes[1].Add(n2); ss.Notes[1].Add(n1);

            ss.Notes[0].Add(n4); ss.Notes[0].Add(n5); ss.Notes[0].Add(n6);
            ss.Notes[0].Add(n5); ss.Notes[0].Add(n4);

            ss.Notes[2].Add(n7); ss.Notes[2].Add(n8); ss.Notes[2].Add(n9);
            ss.Notes[2].Add(n8); ss.Notes[2].Add(n7); 

            WaveOut wo = new WaveOut();
            wo.Init(ss.sampleSynthesizer);
            wo.Play();
        }
        void teste2()
        {
            SoundSynthesis ss2 = new SoundSynthesis();
            ss2.Notes.Add(new List<SoundSynthesis.Note>());

            SoundSynthesis.Note n0 = new SoundSynthesis.Note(1.75, SoundSynthesis.Note.GetFrequency(0), new SoundSynthesis.WaveShapeSine());
            SoundSynthesis.Note pause = new SoundSynthesis.Note(0.25, 0, new SoundSynthesis.WaveShapeSine());
            SoundSynthesis.Note n1 = new SoundSynthesis.Note(0.5, SoundSynthesis.Note.GetFrequency(0), new SoundSynthesis.WaveShapeSine());
            SoundSynthesis.Note n2 = new SoundSynthesis.Note(0.5, SoundSynthesis.Note.GetFrequency(2), new SoundSynthesis.WaveShapeSine());
            SoundSynthesis.Note n3 = new SoundSynthesis.Note(0.5, SoundSynthesis.Note.GetFrequency(4), new SoundSynthesis.WaveShapeSine());
            SoundSynthesis.Note n5 = new SoundSynthesis.Note(1.0, SoundSynthesis.Note.GetFrequency(0), new SoundSynthesis.WaveShapeSine());

            ss2.Notes.Add(new List<SoundSynthesis.Note>());
            ss2.Notes[0].Add(n0); ss2.Notes[0].Add(pause);
            ss2.Notes[0].Add(n1); ss2.Notes[0].Add(n2); ss2.Notes[0].Add(n3);
            ss2.Notes[0].Add(n2); ss2.Notes[0].Add(n5);

            WaveOut wo2 = new WaveOut();
            wo2.Init(ss2.sampleSynthesizer);

            //wo2.Play();

            //return;

            SoundSynthesis ss = new SoundSynthesis();
            ss.Notes.Add(new List<SoundSynthesis.Note>());

            ss.Tempo = 1f;
            //ss.Notes[0].Add(SoundSynthesis.Note.FromString("P 1"));
            ss.Notes[0].Add(SoundSynthesis.Note.FromString("A4 1.75", noteWaveShape));
            ss.Notes[0].Add(SoundSynthesis.Note.FromString("P 0.25", noteWaveShape));
            ss.Notes[0].Add(SoundSynthesis.Note.FromString("A4 0.5", noteWaveShape));
            ss.Notes[0].Add(SoundSynthesis.Note.FromString("B4 0.5", noteWaveShape));
            ss.Notes[0].Add(SoundSynthesis.Note.FromString("C#5 0.5", noteWaveShape));
            ss.Notes[0].Add(SoundSynthesis.Note.FromString("B4 0.5", noteWaveShape));
            ss.Notes[0].Add(SoundSynthesis.Note.FromString("A4 1", noteWaveShape));

            WaveOut wo = new WaveOut();
            wo.Init(ss2.sampleSynthesizer);
            wo.Play();
        }
        #endregion


        #region Melody - play, add

        SoundSynthesis ssMelody;
        WaveOut woPlayMelody;
        private void InitMelody()
        {
            ssMelody = new SoundSynthesis();

            woPlayMelody = new WaveOut();
            woPlayMelody.Init(ssMelody.sampleSynthesizer);
            woPlayMelody.PlaybackStopped +=new EventHandler<StoppedEventArgs>(woPlayMelody_PlaybackStopped);

            ssMelody.Notes.Add(new List<SoundSynthesis.Note>());
        }
        private void numTempo_ValueChanged(object sender, EventArgs e)
        {
            ssMelody.Tempo = (double)numTempo.Value;

        }


        private void addNote(string note)
        {
            //return melody to original
            bool modulateCheckedBefore = chkModulateFirstNote.Checked;
            chkModulateFirstNote.Checked = false;
            ModulateNotes();
            
            try
            {
                //Adds note to first track
                SoundSynthesis.Note ssNote = SoundSynthesis.Note.FromString(note, noteWaveShape);
                //cannot add pauses at first
                if (ssNote.Frequency == 0 && ssMelody.Notes[0].Count == 0) return;

                ssMelody.Notes[0].Add(ssNote);

                //Continue to add graphics to note
                double nHalfStepsAboveA4 = SoundSynthesis.Note.GetHalfStepsAboveA4(ssNote.Frequency);

                string sepDec = (1.1).ToString().Substring(1, 1);

                string[] noteParts = note.Split();
                noteParts[0] = noteParts[0].ToUpper();

                double duration = double.Parse(noteParts[1].Replace(".", sepDec).Replace(",", sepDec));

                int multiplier = 3 * 8;
                MusicalSymbolDuration msd = new MusicalSymbolDuration();
                int nDots = 0;
                int intDuration = (int)Math.Round(multiplier * duration);

                if (intDuration == multiplier * 4)
                {
                    msd = MusicalSymbolDuration.Whole;
                }
                else if (intDuration == multiplier * 3)
                {
                    msd = MusicalSymbolDuration.Half;
                    nDots = 1;
                }
                else if (intDuration == multiplier * 2)
                {
                    msd = MusicalSymbolDuration.Half;
                }
                else if (intDuration == multiplier * 1)
                {
                    msd = MusicalSymbolDuration.Quarter;
                }
                else if (intDuration == multiplier * 1.5)
                {
                    msd = MusicalSymbolDuration.Quarter;
                    nDots = 1;
                }
                else if (intDuration == multiplier * 1.75)
                {
                    msd = MusicalSymbolDuration.Quarter;
                    nDots = 2;
                }
                else if (intDuration == multiplier / 2)
                {
                    msd = MusicalSymbolDuration.Eighth;
                }
                else if (intDuration == multiplier * 0.75)
                {
                    msd = MusicalSymbolDuration.Eighth;
                    nDots = 1;
                }
                else if (intDuration == multiplier / 4)
                {
                    msd = MusicalSymbolDuration.Sixteenth;
                }
                else if (intDuration == multiplier * 0.375)
                {
                    msd = MusicalSymbolDuration.Sixteenth;
                    nDots = 1;
                }

                MusicalSymbol drawNote;
                if (noteParts[0] == "P")
                {
                    drawNote = new Rest(msd);
                    Rest rr = (Rest)drawNote;
                    rr.NumberOfDots = nDots;
                }
                else
                {
                    int noteAlter = 0;
                    if (noteParts[0].Substring(1, 1) == "#") noteAlter = 1;
                    if (noteParts[0].Substring(1, 1) == "B") noteAlter = -1;

                    int octave = int.Parse(noteParts[0].Substring(noteParts[0].Length - 1, 1));

                    drawNote = new Note(noteParts[0].Substring(0, 1), noteAlter, octave, msd, Math.Round(nHalfStepsAboveA4) >= 3 ? NoteStemDirection.Down : NoteStemDirection.Up,
                        NoteTieType.None, new List<NoteBeamType>() { NoteBeamType.Single });

                    Note nn = (Note)drawNote;
                    nn.NumberOfDots = nDots;

                    //lyrics!!!!!!!
                    if (noteParts.Length > 2)
                    {
                        nn.Lyrics = new List<LyricsType>() { LyricsType.Single };
                        nn.LyricTexts = new List<string>() { noteParts[2] };
                    }

                    if (noteParts[0].Substring(1, 1) == "N") nn.HasNatural = true;
                }

                melodyViewer.AddMusicalSymbol(drawNote);
                melodyViewer.Invalidate();
                picViewMelody.Invalidate();
            }
            catch
            {

            }

            chkModulateFirstNote.Checked = modulateCheckedBefore;
        }
        
        /// <summary>Playing melody?</summary>
        bool playingMelody = false;
        /// <summary>Stopwatch to play melody</summary>
        Stopwatch swPlayMelody = new Stopwatch();
        /// <summary>Total melody time</summary>
        double totalMelodyTime;

        private void btnPlayMelody_Click(object sender, EventArgs e)
        {
            if (ssMelody.Notes[0].Count <= 0) return;

            if (!playingMelody)
            {
                swPlayMelody.Start();
                playingMelody = true;
                totalMelodyTime = ssMelody.GetTrackPlayTime(0);

                //reset melody position
                ssMelody.sampleSynthesizer.Reset();
                woPlayMelody.Play();

                btnPlayMelody.Image = picStop.Image;
            }
            else
            {
                woPlayMelody.Stop();
            }
        }

        void woPlayMelody_PlaybackStopped(object sender, StoppedEventArgs e)
        {
            btnPlayMelody.Enabled = true;
            playingMelody = false;
            swPlayMelody.Reset();
            btnPlayMelody.Image = picPlay.Image;
        }
        void melodyViewer_PlayExternalMidiPlayer(IncipitViewer sender)
        {
            btnPlayMelody_Click(sender, new EventArgs());
        }
        
        private void btnDelMelody_Click(object sender, EventArgs e)
        {
            melodyViewer.ClearMusicalIncipit();
            melodyViewer.AddMusicalSymbol(new Clef(ClefType.GClef, 2));
            //MusicalSymbol ms = new PSAMControlLibrary.Barline();
            melodyViewer.AddMusicalSymbol(new TimeSignature(TimeSignatureType.Common, 4, 4));


            ssMelody.Notes[0].Clear();
            melodyViewer.Invalidate();
            picViewMelody.Invalidate();
        }

        private void chkModulateFirstNote_CheckedChanged(object sender, EventArgs e)
        {
            ModulateNotes();
        }
        private void ModulateNotes()
        {
            if (ssMelody == null) return;
            if (ssMelody.Notes[0].Count <= 0) return;
            
            SoundSynthesis.Note desiredFirstNote;
            if (chkModulateFirstNote.Checked) desiredFirstNote = SoundSynthesis.Note.FromString(cmbInitNote.Items[cmbInitNote.SelectedIndex].ToString() + " 1", noteWaveShape);
            else desiredFirstNote = SoundSynthesis.Note.FromString(ssMelody.Notes[0][0].buildString, noteWaveShape);
            ssMelody.ModulateSound(desiredFirstNote.Frequency);
            picViewMelody.Invalidate();
        }

        #region Load

        private string GetMelodyString()
        {
            string s = "";
            foreach (SoundSynthesis.Note n in ssMelody.Notes[0])
            {
                s += n.buildString + "\r\n";
            }
            return s;
        }

        private void PutMelodyString(string s)
        {
            string[] newNotes = s.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            btnDelMelody_Click(this, new EventArgs());
            foreach (string s2 in newNotes)
            {
                addNote(s2);
            }
        }


        private void btnLoadMelody_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.InitialDirectory = Application.StartupPath;
            ofd.Filter = "Melody|*.mld";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                PutMelodyString(File.ReadAllText(ofd.FileName));
            }
        }
        #endregion


        #endregion

        #region Guided singing, free singing

        GuidedSinging gs;
        private void cmbInputDevice_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (gs != null) gs.MicID = cmbInputDevice.SelectedIndex;
        }
        private void initGuidedSinging()
        {
            gs = new GuidedSinging(cmbInputDevice.SelectedIndex, guidedSingingStopFunc, ssMelody);
            ssMelody.Tempo = (double)numTempo.Value;


            //add some notes to melody
            if (ssMelody.Notes[0].Count == 0)
            {
                addNote("C4 2 La"); 
                addNote("P 0.5");
                addNote("C4 0.5 La");
                addNote("D4 0.5 La");
                addNote("E4 0.5 La");
                addNote("D4 0.5 La");
                addNote("C4 2 La");
            }
        }

        private void guidedSingingStopFunc()
        {
            btnGuidedSinging.Enabled = true;
            btnFreeSinging.Enabled = true;
            timer.Enabled = false;

            if (gs.FreeSinging) btnTranscribe.Enabled = true;

            //keeps visualization
            replayingSample = true;
            replayFreqSamples.Clear(); replayIntensSamples.Clear();
            replayPlayTime = (float)gs.trackPlayTime;
            replayFreqSamples.Add(gs.acqFreqSamples); replayIntensSamples.Add(gs.acqIntensSamples);
            //if (idxSing > 0 && idxSing % 1 == 0) btnSaveSings_Click(this, new EventArgs());
        }
        int idxSing = 0;
        private void btnGuidedSinging_Click(object sender, EventArgs e)
        {
            if (ssMelody.Notes[0].Count == 0) return;
            replayingSample = false;
            ClearGuidedSinging();

            gs.FreeSinging = false;
            gs.StartGuidedSinging("seq" + idxSing.ToString() + " " + txtMelodyName.Text); idxSing++;
            btnGuidedSinging.Enabled = false;
            btnFreeSinging.Enabled = false;
            timer.Enabled = true;
        }
        private void btnFreeSinging_Click(object sender, EventArgs e)
        {
            if (ssMelody.Notes[0].Count == 0) return;
            replayingSample = false;
            ClearGuidedSinging();

            gs.FreeSinging = true;
            gs.StartGuidedSinging("seq" + idxSing.ToString() + " " + txtMelodyName.Text); idxSing++;
            btnGuidedSinging.Enabled = false;
            btnFreeSinging.Enabled = false;
            timer.Enabled = true;
        }
        private void timer_Tick(object sender, EventArgs e)
        {
            if (gs != null && gs.isPlayingGuidedSinging)
            {
                gs.AcquireFreqSample();
                picViewMelody.Invalidate();
            }
            if (replayingSample) picViewMelody.Invalidate();
        }

        private void btnTranscribe_Click(object sender, EventArgs e)
        {
            List<string> notes = gs.TranscribeFreeSinging((double)numTempo.Value);
            if (notes.Count > 0)
            {
                btnDelMelody_Click(sender, e);
                foreach (string s in notes)
                {
                    addNote(s);
                }
            }
        }

        private void btnSaveSings_Click(object sender, EventArgs e)
        {
            //save singings
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "REC|*.wav";

            string UserFolder;
            if (Directory.Exists(txtClientFolder)) UserFolder = txtClientFolder;
            else UserFolder = Application.StartupPath + "\\" + txtClientFolder;

            if (!Directory.Exists(UserFolder)) Directory.CreateDirectory(UserFolder);

            string recFolder = getRecFolder();
            if (!Directory.Exists(recFolder)) Directory.CreateDirectory(recFolder);

            string recNameOnly = DateTime.Now.Hour.ToString() + "h" + DateTime.Now.Minute.ToString().PadLeft(2, '0') + "min" + DateTime.Now.Second.ToString().PadLeft(2, '0') + "s.wav";
            string recFileName = recFolder + "\\" + recNameOnly;
            sfd.InitialDirectory = recFolder;
            sfd.FileName = recNameOnly;

            if (sfd.ShowDialog() == DialogResult.OK) gs.SaveLastGuidedSinging(sfd.FileName);

            //gs.ClearData();
            //GC.Collect();

            //initGuidedSinging();
        }

        /// <summary>Clears guided singing before a new take</summary>
        private void ClearGuidedSinging()
        {
            gs.ClearData();
            GC.Collect();

            initGuidedSinging();
        }

        /// <summary>Retrieves folder to where audio recordings should be saved</summary>
        /// <returns></returns>
        private string getRecFolder()
        {
            string UserFolder;

            if (Directory.Exists(txtClientFolder)) UserFolder = txtClientFolder;
            else UserFolder = Application.StartupPath + "\\" + txtClientFolder;


            string dtString = DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString().PadLeft(2, '0') + "-" + DateTime.Now.Day.ToString().PadLeft(2, '0');

            return UserFolder + "\\" + dtString;
        }


        #endregion

        #region Graphics

        /// <summary>Draw note grid?</summary>
        private bool drawNoteGrid = true;

        private List<double> noteFrequencies;
        /// <summary>Draws grid of notes onto image</summary>
        /// <param name="g">Graphics element</param>
        /// <param name="W">Image Width</param>
        /// <param name="H">Image height</param>
        private void DrawNoteGrid(Graphics g, int W, int H, float offSetY, float scaleYPix)
        {
            if (!drawNoteGrid) return;

            if (noteFrequencies == null)
            {
                //reference notes - human voice limits
                SoundSynthesis.Note n0 = SoundSynthesis.Note.FromString("E2 1", noteWaveShape);
                SoundSynthesis.Note nf = SoundSynthesis.Note.FromString("E7 1", noteWaveShape);

                double nHalfTones0 = SoundSynthesis.Note.GetHalfStepsAboveA4(n0.Frequency);
                double nHalfTonesf = SoundSynthesis.Note.GetHalfStepsAboveA4(nf.Frequency);


                noteFrequencies = new List<double>();
                for (double k = nHalfTones0; k <= nHalfTonesf; k++) noteFrequencies.Add(SoundSynthesis.Note.GetFrequency(k));
            }
            
            float scaley = (float)(H / maxDrawFrequency);
            foreach (double f in noteFrequencies)
            {
                g.DrawLine(Pens.LightGreen, 0, H + (offSetY - scaley * (float)f) * scaleYPix, W, H + (offSetY - scaley * (float)f) * scaleYPix);

                //string strFreq = Math.Round(f, 2).ToString() + " Hz";
                //SizeF sString = g.MeasureString(strFreq, ff);
                //g.DrawString(strFreq, ff, Brushes.LightGreen, (float)(W - sString.Width), (float)(H + (offSetY - scaley * (float)f) * scaleYPix - sString.Height));
            }
        }
        private void noteGridToolStripMenuItem_Click(object sender, EventArgs e)
        {
            drawNoteGrid = !drawNoteGrid;
            picViewMelody.Invalidate();
        }

        /// <summary>Maximum frequency to draw</summary>
        float maxDrawFrequency = 2000;

        Font ff = new Font("Arial", 10, FontStyle.Bold);
        private void picViewMelody_Paint(object sender, PaintEventArgs e)
        {
            float WW = picViewMelody.Width;
            float HH = picViewMelody.Height;

            //Zoom
            float zoomX0 = 0, zoomY0 = 0, zoomScaleX = 1, zoomScaleY = 1;
            if (!clickSelect && (selP0.X != 0 || selP0.X != 0) && (selP0.X != selPf.X && selP0.X != selPf.Y))
            {
                zoomX0 = -Math.Min(selP0.X, selPf.X);
                zoomY0 = HH - Math.Max(selP0.Y, selPf.Y);
                zoomScaleX = (float)picViewMelody.Width / Math.Abs(selP0.X - selPf.X);
                zoomScaleY = (float)picViewMelody.Height / Math.Abs(selP0.Y - selPf.Y);
            }

            DrawNoteGrid(e.Graphics, picViewMelody.Width, picViewMelody.Height, zoomY0, zoomScaleY);

            if (ssMelody != null) ssMelody.DrawTrack(e.Graphics, 0, zoomX0, zoomY0, (int)(picViewMelody.Width), (int)(picViewMelody.Height), maxDrawFrequency, zoomScaleX, zoomScaleY);

            #region Plot replay sample
            if (replayingSample)
            {
                float scalex = (float)(WW / replayPlayTime);
                float scaley = (float)(HH / maxDrawFrequency);

                float time = (float)swReplaySample.Elapsed.TotalSeconds;
                e.Graphics.DrawLine(Pens.Red, zoomScaleX * (zoomX0 + time * scalex), 0, zoomScaleX * (zoomX0 + time * scalex), HH);

                if (replayFreqSamples != null)
                {
                    foreach (List<PointF> fSamples in replayFreqSamples)
                    {
                        List<PointF> plotFreqData = new List<PointF>();
                        foreach (PointF p in fSamples) plotFreqData.Add(new PointF(zoomScaleX * (zoomX0 + p.X * scalex), HH + zoomScaleY * (zoomY0 - p.Y * scaley)));
                        if (plotFreqData.Count > 1) e.Graphics.DrawLines(Pens.Blue, plotFreqData.ToArray());
                    }
                }

                if (replayIntensSamples != null && intensityGraphToolStripMenuItem.Checked)
                {
                    foreach (List<PointF> iSamples in replayIntensSamples)
                    {
                        List<PointF> plotIntensData = new List<PointF>();
                        foreach (PointF p in iSamples) plotIntensData.Add(new PointF(zoomScaleX * (zoomX0 + p.X * scalex), HH + zoomScaleY * (zoomY0 - p.Y * scaley)));
                        if (plotIntensData.Count > 1) e.Graphics.DrawLines(Pens.DarkOrange, plotIntensData.ToArray());
                    }
                }
            }
            #endregion

            #region Guided singing plot
            if (gs != null && gs.isPlayingGuidedSinging)
            {
                float scalex = (float)(WW / gs.trackPlayTime);
                float scaley = (float)(HH / maxDrawFrequency);

                //plot frequency
                List<PointF> plotFreqData = new List<PointF>();
                foreach (PointF p in gs.acqFreqSamples) plotFreqData.Add(new PointF(zoomScaleX * (zoomX0 + p.X * scalex), HH + zoomScaleY * (zoomY0 - p.Y * scaley)));
                if (plotFreqData.Count > 1) e.Graphics.DrawLines(Pens.Blue, plotFreqData.ToArray());

                //plot intensity
                if (intensityGraphToolStripMenuItem.Checked)
                {
                    List<PointF> plotIntensData = new List<PointF>();
                    foreach (PointF p in gs.acqIntensSamples) plotIntensData.Add(new PointF(zoomScaleX * (zoomX0 + p.X * scalex), HH + zoomScaleY * (zoomY0 - p.Y * scaley)));
                    if (plotIntensData.Count > 1) e.Graphics.DrawLines(Pens.DarkOrange, plotIntensData.ToArray());
                }

                e.Graphics.DrawLine(Pens.Red, zoomScaleX * (zoomX0 + gs.lastTime * scalex), 0, zoomScaleX * (zoomX0 + gs.lastTime * scalex), HH);

                e.Graphics.DrawString("F0:" + gs.lastFreq.ToString() + " Hz\r\nI:" + gs.lastIntens.ToString() + " dB", ff, Brushes.Black, 0, 0);

                //draw note candidates
                if (gs.FreeSinging)
                {
                    float xCand = zoomScaleX * (zoomX0 + 0.5f * (gs.curCandidate.tEnd + gs.curCandidate.tStart) * scalex);
                    float yCand = HH + zoomScaleY * (zoomY0 - gs.curCandidate.meanFreq * scaley);
                    SizeF ss = e.Graphics.MeasureString(gs.curCandidate.meanFreq.ToString() + "Hz", ff);

                    e.Graphics.DrawString(gs.curCandidate.meanFreq.ToString() + "Hz", ff, Brushes.Black, xCand - 0.5f * ss.Width, yCand - ss.Height);

                    foreach (GuidedSinging.CandidateNote cn in gs.FreeSingingNotes)
                    {
                        xCand = zoomScaleX * (zoomX0 + 0.5f * (cn.tEnd + cn.tStart) * scalex);
                        yCand = HH + zoomScaleY * (zoomY0 - cn.meanFreq * scaley);
                        ss = e.Graphics.MeasureString(cn.Note.ToString(), ff);
                        e.Graphics.DrawString(cn.Note.ToString(), ff, Brushes.Black, xCand - 0.5f * ss.Width, yCand - ss.Height);
                    }
                }
            }
            #endregion

            #region Draw Selection

            if (clickSelect && (selP0.X != selPf.X || selP0.X != selPf.X))
            {
                float x0 = Math.Min(selP0.X, selPf.X);
                float y0 = Math.Min(selP0.Y, selPf.Y);
                float ws = Math.Abs(selP0.X - selPf.X);
                float hs = Math.Abs(selP0.Y - selPf.Y);
                Color c = Color.FromArgb(50, 0, 255, 0);
                e.Graphics.FillRectangle(new SolidBrush(c), x0, y0, ws, hs);
                e.Graphics.DrawRectangle(Pens.Green, x0, y0, ws, hs);
            }
            if (ShowClickedPt)
            {
                Pen p = new Pen(Color.Red);
                p.DashPattern = new float[] { 5, 2, 1, 2 };
                e.Graphics.DrawLine(p, selP0.X, 0, selP0.X, picViewMelody.Height);
                e.Graphics.DrawLine(p, 0, selP0.Y, picViewMelody.Width, selP0.Y);

                float scaley = (float)(HH / maxDrawFrequency);

                string strFreq = Math.Round((picViewMelody.Height - selP0.Y) / scaley, 2).ToString() + " Hz";
                SizeF sString = e.Graphics.MeasureString(strFreq, ff);
                e.Graphics.DrawString(strFreq, ff, Brushes.Red, selP0.X, selP0.Y - sString.Height);

            }

            #endregion

            #region Jitter and shimmer, time
            int h0 = 40;
            if (replayingSample && (!clickSelect && (selP0.X != 0 || selP0.X != 0) && (selP0.X != selPf.X && selP0.X != selPf.Y)))
            {
                float t0 = replayPlayTime / picViewMelody.Width * selP0.X;
                float tf = replayPlayTime / picViewMelody.Width * selPf.X;

                e.Graphics.DrawString(Math.Round(tf - t0, 3).ToString() + " s", ff, Brushes.Black, 0, h0);

                if (tf < t0)
                {
                    float temp = t0; t0 = tf; tf = temp;
                }
                List<float> sampleFreqs = new List<float>();
                List<float> sampleIntens = new List<float>();
                for (int k = 0; k < replayFreqSamples[0].Count; k++)
                {
                    if (t0 <= replayFreqSamples[0][k].X && replayFreqSamples[0][k].X <= tf)
                    {
                        sampleFreqs.Add(replayFreqSamples[0][k].Y);
                        sampleIntens.Add(replayIntensSamples[0][k].Y);
                    }
                }
                float[] jitter = SampleAudio.RealTime.JitterShimmer.ComputeJitter(sampleFreqs.ToArray());
                float[] shimmer = SampleAudio.RealTime.JitterShimmer.ComputeShimmer(sampleIntens.ToArray());

                string jitString = "Jitta: " + Math.Round(jitter[0], 3).ToString() + " ms; ";
                jitString += "Jitt: " + Math.Round(jitter[1], 3).ToString() + "%; ";
                jitString += "Rap: " + Math.Round(jitter[2], 3).ToString() + "%; ";
                jitString += "ppq5: " + Math.Round(jitter[3], 3).ToString() + "%; ";

                Color jitColor = Color.Black;
                if (jitter[0] > 83.2 || jitter[1] > 1.04 || jitter[2] > 0.68 || jitter[3] > 0.84) jitColor = Color.Red;

                if (!float.IsNaN(jitter[1])) e.Graphics.DrawString(jitString, ff, new SolidBrush(jitColor), 0, h0+13);


                string shimmerString = "ShdB: " + Math.Round(shimmer[0], 3).ToString() + " dB; ";
                shimmerString += "Shim: " + Math.Round(shimmer[1], 3).ToString() + "%; ";
                //shimmerString += "apq3: " + Math.Round(shimmer[2], 3).ToString() + "%; ";
                //shimmerString += "apq5: " + Math.Round(shimmer[3], 3).ToString() + "%; ";
                //shimmerString += "apq11: " + Math.Round(shimmer[4], 3).ToString() + "%; ";
                Color shimmerColor = Color.Black;
                if (shimmer[0] > 0.35 || shimmer[1] > 3.81/* || shimmer[4] > 3.07*/) shimmerColor = Color.Red;
                if (!float.IsNaN(shimmer[1])) e.Graphics.DrawString(shimmerString, ff, new SolidBrush(shimmerColor), 0, h0+26);

            }
            #endregion
        }
        #endregion

        private void btnStopAll_Click(object sender, EventArgs e)
        {
            ssMelody.sampleSynthesizer.GoToEnd();
            if (replayingSample) woReplayFile.Stop();
        }

        #region Wave shape
        static SoundSynthesis.WaveShape wsPiano = new SoundSynthesis.WaveShapePiano();
        static SoundSynthesis.WaveShape wsSine = new SoundSynthesis.WaveShapeSine();
        static SoundSynthesis.WaveShape wsSquare = new SoundSynthesis.WaveShapeSquare(0.5);

        SoundSynthesis.WaveShape noteWaveShape = wsPiano;

        private void pianoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            noteWaveShape = wsPiano;
            RedoSoundWaveshape();
        }
        private void sineWaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            noteWaveShape = wsSine;
            RedoSoundWaveshape();
        }
        private void squareWaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            noteWaveShape = wsSquare;
            RedoSoundWaveshape();
        }
        private void RedoSoundWaveshape()
        {
            foreach (SoundSynthesis.Note n in ssMelody.Notes[0]) n.waveShape = noteWaveShape;
        }
        #endregion


        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox ab = new AboutBox();
            ab.ShowDialog();
        }


        #region Client management
        string txtClientFolder = Application.StartupPath;
        Bitmap defaultClientImg;
        frmClient frmC;
        private void btnClient_Click(object sender, EventArgs e)
        {
            if (defaultClientImg == null) defaultClientImg = (Bitmap)btnClient.Image;
            if (frmC == null || frmC.IsDisposed) frmC = new frmClient();

            frmC.ShowDialog();
            txtClientFolder = frmC.GetClientFolder();

            if (frmC.picClientPic.Image != null)
            {
                Bitmap bmp = new Bitmap(btnClient.Width, btnClient.Height);
                Graphics g = Graphics.FromImage(bmp);

                g.DrawImage(frmC.picClientPic.Image, 0, 0, bmp.Width, bmp.Height);

                btnClient.Image = bmp;
            }
            else btnClient.Image = defaultClientImg;

            lblNote.Text = frmC.txtImportantNotes.Text;
        }
        #endregion


        #region Replay client audio
        bool replayingSample = false;
        SampleAudio replaySample;
        float replayPlayTime;
        System.Diagnostics.Stopwatch swReplaySample = new Stopwatch();
        WaveOut woReplayFile;

        List<List<PointF>> replayFreqSamples = new List<List<PointF>>(), replayIntensSamples = new List<List<PointF>>();
        private void replayToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Rec|*.wav;*.mp3";

            if (Directory.Exists(txtClientFolder)) ofd.InitialDirectory = txtClientFolder;

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                if (replaySample != null) replaySample.Dispose();
                replaySample = new SampleAudio(ofd.FileName);

                //extract graphs
                replayFreqSamples.Clear(); replayIntensSamples.Clear();
                try
                {
                    if (File.Exists(ofd.FileName + "freq")) replayFreqSamples.Add(GuidedSinging.GetPlotFromString(File.ReadAllText(ofd.FileName + "freq")));
                }
                catch
                {
                }
                try
                {
                    if (File.Exists(ofd.FileName + "intens")) replayIntensSamples.Add(GuidedSinging.GetPlotFromString(File.ReadAllText(ofd.FileName + "intens")));
                }
                catch
                {
                }


                numTempo.Value = 1;
                chkModulateFirstNote.Checked = false;

                //extract melody from sample
                string s = "";
                foreach (SampleAudio.AudioAnnotation ann in replaySample.Annotations)
                {
                    string[] tryNote = ann.Text.Split(new string[] { ":", "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                    if (tryNote.Length >= 2) s += tryNote[1] + "\r\n";
                }
                PutMelodyString(s);
                replayPlayTime = (float)ssMelody.GetTrackPlayTime(0);
                SampleAudio.PlaySample ps = new SampleAudio.PlaySample(replaySample, 0, replaySample.time[replaySample.time.Length - 1]);
                //ps.PlayTimeFunc = DrawTime;
                //ps.ReplaySpeed = 0.01 * (double)repSpeedPercent;

                swReplaySample.Reset();
                swReplaySample.Start();

                woReplayFile = new WaveOut();
                woReplayFile.PlaybackStopped += new EventHandler<StoppedEventArgs>(wo_PlaybackStopped);
                woReplayFile.Init(ps);
                replayingSample = true;
                timer.Enabled = true;

                woReplayFile.Play();
                compareToolStripMenuItem.Enabled = true;
            }
        }

        void wo_PlaybackStopped(object sender, StoppedEventArgs e)
        {
            swReplaySample.Stop();
            //replayingSample = false;
            timer.Enabled = false;
        }

        private void compareToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Rec|*.wav;*.mp3";

            ofd.Multiselect = true;

            if (Directory.Exists(txtClientFolder)) ofd.InitialDirectory = txtClientFolder;

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                foreach (string file in ofd.FileNames)
                {
                    try
                    {
                        if (File.Exists(file + "freq")) replayFreqSamples.Add(GuidedSinging.GetPlotFromString(File.ReadAllText(ofd.FileName + "freq")));
                    }
                    catch
                    {
                    }
                    try
                    {
                        if (File.Exists(file + "intens")) replayIntensSamples.Add(GuidedSinging.GetPlotFromString(File.ReadAllText(ofd.FileName + "intens")));
                    }
                    catch
                    {
                    }
                }
                picViewMelody.Refresh();
            }
        }
        #endregion

        #region Selection
        bool clickSelect = false;
        PointF selP0 = new PointF();
        PointF selPf = new PointF();

        bool ShowClickedPt = false;
        private void picViewMelody_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left && selP0.X == 0 && selP0.Y == 0)
            {
                clickSelect = true;
                selP0.X = e.X;
                selP0.Y = e.Y;
            }
            else if (selP0.X != 0 || selP0.Y != 0)
            {
                selP0.X = 0;
                selP0.Y = 0;
            }
            selPf.X = e.X;
            selPf.Y = e.Y;
            picViewMelody.Invalidate();
        }
        private void picViewMelody_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                //clicked the same place
                ShowClickedPt = (selP0.X == e.X && selP0.Y == e.Y);

                clickSelect = false;
                picViewMelody.Invalidate();
            }
        }
        private void picViewMelody_MouseMove(object sender, MouseEventArgs e)
        {
            if (clickSelect)
            {
                selPf.X = e.X;
                selPf.Y = e.Y;
                picViewMelody.Invalidate();
            }
        }
        #endregion



        #region Identify frequency from sound

        private void btnIdentifySound_Click(object sender, EventArgs e)
        {
            if (!gs.IsIdentifyingFreq) gs.IdentifyFreq(cmbInputDevice.SelectedIndex, stopIdentFreq);
            else gs.StopIdentFreq();
        }
        private void stopIdentFreq()
        {
            if (!gs.IsIdentifyingFreq)
            {
                SoundSynthesis.Note n = new SoundSynthesis.Note(1, gs.identifyCurFreq, noteWaveShape);
                btnIdentifySound.Text = "";

                int noteId = (int)Math.Round(SoundSynthesis.Note.GetHalfStepsAboveA4(gs.identifyCurFreq));
                int idx = noteId - K0;
                idx = Math.Max(0, idx);
                idx = Math.Min(idx, cmbInitNote.Items.Count);

                cmbInitNote.SelectedIndex = idx;
            }
            else btnIdentifySound.Text = Math.Round(gs.identifyCurFreq).ToString();
        }

        int K0 = -36;
        private void initInitNoteCmb()
        {
            for (int k = K0; k < 32; k++)
            {
                double f = SoundSynthesis.Note.GetFrequency(k);
                cmbInitNote.Items.Add(SoundSynthesis.Note.GetNoteName(f));// + " " + f.ToString());
            }

            cmbInitNote.SelectedIndex = 17; 
        }

        private void cmbInitNote_SelectedIndexChanged(object sender, EventArgs e)
        {
            ModulateNotes();
        }
        #endregion







    }
}
