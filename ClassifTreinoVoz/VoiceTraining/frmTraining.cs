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
        public static void InitFont()
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
            SoftwareKey.CheckLicense("VocalTrainer", false);

            //do some tweaking to btnSing
            btnSing.FlatStyle = FlatStyle.Flat;
            btnSing.FlatAppearance.BorderSize = 0;

            Bitmap imgMicrophone = (Bitmap)btnSing.Image.Clone();
            Bitmap imgDest = PratiCanto.LayoutFuncs.MakeDegrade(Color.FromArgb(228, 100, 100), Color.FromArgb(173, 110, 110), btnSing.Width, btnSing.Height, true);
            Graphics g = Graphics.FromImage(imgDest);
            g.DrawImage(imgMicrophone, 5, 5, btnSing.Width-10, btnSing.Height-10);
            btnSing.Image = imgDest;


            //set client if already chosen
            SetClient();
            
            //make replay multiple files invisible
            btnReplayMultiple.Visible = false;
            
            //initialize note combo
            initInitNoteCmb();


            txtMelodyName.Visible = false;


            //Initializes musical font
            //InitFont();

            gbViewMelody.Controls.Add(melodyViewer);
            melodyViewer.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
            melodyViewer.Width = gbViewMelody.Width - 10 - btnPlayMelody.Width;
            melodyViewer.Top = btnPlayMelody.Top - (btnPlayMelody.Height>>2);
            melodyViewer.AddMusicalSymbol(new Clef(ClefType.GClef, 2));
            melodyViewer.PlayExternalMidiPlayer += new IncipitViewer.PlayExternalMidiPlayerDelegate(melodyViewer_PlayExternalMidiPlayer);
            //melodyViewer.MouseDown += new MouseEventHandler(melodyViewer_MouseDown);

            //MusicalSymbol ms = new PSAMControlLibrary.Barline();
            //MusicalSymbol ms = new PSAMControlLibrary.TimeSignature(TimeSignatureType.Common, 4, 4);
            MusicalSymbol ms = new PSAMControlLibrary.Direction();
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
            frmiv.ShowDialog();
        }
        private void btnAudioAnalyzer_Click(object sender, EventArgs e)
        {
            PratiCantoForms.ShowFormAnalyzer();
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
        //void teste()
        //{
        //    List<string> notes = new List<string>();
        //    for (int k = -30; k < 30; k++)
        //    {
        //        double f = SoundSynthesis.Note.GetFrequency(k);
        //        notes.Add(SoundSynthesis.Note.GetNoteName(f) + " " + f.ToString());
        //    }

        //    SoundSynthesis ss = new SoundSynthesis();
        //    ss.Tempo = 1;

        //    //SoundSynthesis.Note n1 = new SoundSynthesis.Note(1, SoundSynthesis.Note.GetFrequency(0), new SoundSynthesis.WaveShapeSquare(0.8));
        //    SoundSynthesis.Note n1 = new SoundSynthesis.Note(1.5, SoundSynthesis.Note.GetFrequency(0), new SoundSynthesis.WaveShapeSine());
        //    SoundSynthesis.Note n2 = new SoundSynthesis.Note(1.5, SoundSynthesis.Note.GetFrequency(2), new SoundSynthesis.WaveShapeSine());
        //    SoundSynthesis.Note n3 = new SoundSynthesis.Note(1.5, SoundSynthesis.Note.GetFrequency(4), new SoundSynthesis.WaveShapeSine());
        //    SoundSynthesis.Note n4 = new SoundSynthesis.Note(1.5, SoundSynthesis.Note.GetFrequency(4), new SoundSynthesis.WaveShapeSine());
        //    SoundSynthesis.Note n5 = new SoundSynthesis.Note(1.5, SoundSynthesis.Note.GetFrequency(6), new SoundSynthesis.WaveShapeSine());
        //    SoundSynthesis.Note n6 = new SoundSynthesis.Note(1.5, SoundSynthesis.Note.GetFrequency(8), new SoundSynthesis.WaveShapeSine());
        //    SoundSynthesis.Note n7 = new SoundSynthesis.Note(1.5, SoundSynthesis.Note.GetFrequency(7), new SoundSynthesis.WaveShapeSine());
        //    SoundSynthesis.Note n8 = new SoundSynthesis.Note(1.5, SoundSynthesis.Note.GetFrequency(9), new SoundSynthesis.WaveShapeSine());
        //    SoundSynthesis.Note n9 = new SoundSynthesis.Note(1.5, SoundSynthesis.Note.GetFrequency(11), new SoundSynthesis.WaveShapeSine());
        //    SoundSynthesis.Note silence = new SoundSynthesis.Note(0.3, 0, new SoundSynthesis.WaveShapeSine());

        //    //ss.Notes.Add(n2); ss.Notes.Add(n2); ss.Notes.Add(n2); ss.Notes.Add(n2); ss.Notes.Add(n2);
        //    ss.Notes.Add(new List<SoundSynthesis.Note>());
        //    ss.Notes.Add(new List<SoundSynthesis.Note>());
        //    ss.Notes.Add(new List<SoundSynthesis.Note>());
        //    ss.Notes[1].Add(n1); ss.Notes[1].Add(n2); ss.Notes[1].Add(n3);
        //    ss.Notes[1].Add(n2); ss.Notes[1].Add(n1);

        //    ss.Notes[0].Add(n4); ss.Notes[0].Add(n5); ss.Notes[0].Add(n6);
        //    ss.Notes[0].Add(n5); ss.Notes[0].Add(n4);

        //    ss.Notes[2].Add(n7); ss.Notes[2].Add(n8); ss.Notes[2].Add(n9);
        //    ss.Notes[2].Add(n8); ss.Notes[2].Add(n7); 

        //    WaveOut wo = new WaveOut();
        //    wo.Init(ss.sampleSynthesizer);
        //    wo.Play();
        //}
        //void teste2()
        //{
        //    SoundSynthesis ss2 = new SoundSynthesis();
        //    ss2.Notes.Add(new List<SoundSynthesis.Note>());

        //    SoundSynthesis.Note n0 = new SoundSynthesis.Note(1.75, SoundSynthesis.Note.GetFrequency(0), new SoundSynthesis.WaveShapeSine());
        //    SoundSynthesis.Note pause = new SoundSynthesis.Note(0.25, 0, new SoundSynthesis.WaveShapeSine());
        //    SoundSynthesis.Note n1 = new SoundSynthesis.Note(0.5, SoundSynthesis.Note.GetFrequency(0), new SoundSynthesis.WaveShapeSine());
        //    SoundSynthesis.Note n2 = new SoundSynthesis.Note(0.5, SoundSynthesis.Note.GetFrequency(2), new SoundSynthesis.WaveShapeSine());
        //    SoundSynthesis.Note n3 = new SoundSynthesis.Note(0.5, SoundSynthesis.Note.GetFrequency(4), new SoundSynthesis.WaveShapeSine());
        //    SoundSynthesis.Note n5 = new SoundSynthesis.Note(1.0, SoundSynthesis.Note.GetFrequency(0), new SoundSynthesis.WaveShapeSine());

        //    ss2.Notes.Add(new List<SoundSynthesis.Note>());
        //    ss2.Notes[0].Add(n0); ss2.Notes[0].Add(pause);
        //    ss2.Notes[0].Add(n1); ss2.Notes[0].Add(n2); ss2.Notes[0].Add(n3);
        //    ss2.Notes[0].Add(n2); ss2.Notes[0].Add(n5);

        //    WaveOut wo2 = new WaveOut();
        //    wo2.Init(ss2.sampleSynthesizer);

        //    //wo2.Play();

        //    //return;

        //    SoundSynthesis ss = new SoundSynthesis();
        //    ss.Notes.Add(new List<SoundSynthesis.Note>());

        //    ss.Tempo = 1f;
        //    //ss.Notes[0].Add(SoundSynthesis.Note.FromString("P 1"));
        //    ss.Notes[0].Add(SoundSynthesis.Note.FromString("A4 1.75", noteWaveShape));
        //    ss.Notes[0].Add(SoundSynthesis.Note.FromString("P 0.25", noteWaveShape));
        //    ss.Notes[0].Add(SoundSynthesis.Note.FromString("A4 0.5", noteWaveShape));
        //    ss.Notes[0].Add(SoundSynthesis.Note.FromString("B4 0.5", noteWaveShape));
        //    ss.Notes[0].Add(SoundSynthesis.Note.FromString("C#5 0.5", noteWaveShape));
        //    ss.Notes[0].Add(SoundSynthesis.Note.FromString("B4 0.5", noteWaveShape));
        //    ss.Notes[0].Add(SoundSynthesis.Note.FromString("A4 1", noteWaveShape));

        //    WaveOut wo = new WaveOut();
        //    wo.Init(ss2.sampleSynthesizer);
        //    wo.Play();
        //}
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
                if (ssNote.Frequency[0] == 0 && ssMelody.Notes[0].Count == 0) return;

                ssMelody.Notes[0].Add(ssNote);

                //Continue to add graphics to note
                double nHalfStepsAboveA4 = SoundSynthesis.Note.GetHalfStepsAboveA4(ssNote.Frequency[0]);

                string sepDec = (1.1).ToString().Substring(1, 1);

                string[] noteParts = note.Split();
                noteParts[0] = noteParts[0].ToUpper();

                double duration = double.Parse(noteParts[1].Replace(".", sepDec).Replace(",", sepDec));

                int multiplier = 3 * 8;
                MusicalSymbolDuration msd = new MusicalSymbolDuration();
                int nDots = 0;
                int intDuration = (int)Math.Round(multiplier * duration);

                int difTol = 2;


                if (Math.Abs(intDuration - multiplier * 4) <= difTol)
                {
                    msd = MusicalSymbolDuration.Whole;
                }
                else if (Math.Abs(intDuration - multiplier * 3) <= difTol)
                {
                    msd = MusicalSymbolDuration.Half;
                    nDots = 1;
                }
                else if (Math.Abs(intDuration - multiplier * 2) <= difTol)
                {
                    msd = MusicalSymbolDuration.Half;
                }
                else if (Math.Abs(intDuration - multiplier * 1) <= difTol)
                {
                    msd = MusicalSymbolDuration.Quarter;
                }
                else if (Math.Abs(intDuration - multiplier * 1.5) <= difTol)
                {
                    msd = MusicalSymbolDuration.Quarter;
                    nDots = 1;
                }
                else if (Math.Abs(intDuration - multiplier * 1.75) <= difTol)
                {
                    msd = MusicalSymbolDuration.Quarter;
                    nDots = 2;
                }
                else if (Math.Abs(intDuration - multiplier * 0.5) <= difTol)
                {
                    msd = MusicalSymbolDuration.Eighth;
                }
                else if (Math.Abs(intDuration - multiplier * 0.75) <= difTol)
                {
                    msd = MusicalSymbolDuration.Eighth;
                    nDots = 1;
                }
                else if (Math.Abs(intDuration - multiplier * 0.25) <= difTol)
                {
                    msd = MusicalSymbolDuration.Sixteenth;
                }
                else if (Math.Abs(intDuration - multiplier * 0.375) <= difTol)
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
            if (sv != null) sv.ApplyPhonemes(ssMelody.Notes[0]);

            if (!playingMelody)
            {
                swPlayMelody.Start();
                playingMelody = true;
                totalMelodyTime = ssMelody.GetTrackPlayTime(0);

                //reset melody position
                ssMelody.sampleSynthesizer.Reset();
                woPlayMelody.Play();
                if (playbackSample != null) PlayPlayback(0, playbackSample.time[playbackSample.time.Length - 1]);

                timer.Enabled = true;

                btnPlayMelody.Image = picStop.Image;
            }
            else
            {
                woPlayMelody.Stop();
                StopPlayback();
            }
        }

        void woPlayMelody_PlaybackStopped(object sender, StoppedEventArgs e)
        {
            btnPlayMelody.Enabled = true;
            
            playingMelody = false;
            swPlayMelody.Reset();
            btnPlayMelody.Image = picPlay.Image;
            timer.Enabled = false;

            ResetToBlack(melodyViewer);
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
            //melodyViewer.AddMusicalSymbol(new TimeSignature(TimeSignatureType.Common, 4, 4));
            melodyViewer.AddMusicalSymbol(new Direction());


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
            ssMelody.ModulateSound(desiredFirstNote.Frequency[0]);
            picViewMelody.Invalidate();
        }

        #region Load

        private string GetMelodyString()
        {
            string s = "";
            foreach (SoundSynthesis.Note n in ssMelody.Notes[0])
            {
                if (n.buildString != "") s += n.buildString + "\r\n";
                else s += n.ToString() + "\r\n";
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
            ofd.Filter = "Melody|*.mld;*.mid";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                ssMelody.Notes.Clear();
                ssMelody.Notes.Add(new List<SoundSynthesis.Note>());

                if (ofd.FileName.ToLower().EndsWith(".mid"))
                {
                    PratiCanto.EditMelody.frmImportMidi frmImport = new PratiCanto.EditMelody.frmImportMidi(ofd.FileName, ssMelody);
                    frmImport.ShowDialog();

                    //adjust tone
                    string melodyTone = ssMelody.Notes[0][0].ToString().Split()[0].ToUpper();
                    float vol0 = ssMelody.Notes[0][0].Volume;

                    numTempo.Value = (decimal)ssMelody.Tempo;

                    string melody = GetMelodyString();
                    PutMelodyString(melody);

                    for (int k = 0; k < cmbInitNote.Items.Count; k++)
                    {
                        if (melodyTone == cmbInitNote.Items[k].ToString().ToUpper())
                        {
                            cmbInitNote.SelectedIndex = k;
                            break;
                        }
                    }

                    foreach (SoundSynthesis.Note n in ssMelody.Notes[0]) n.Volume = vol0;
                }
                else
                {

                    PutMelodyString(File.ReadAllText(ofd.FileName));
                }
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
                addNote("C4 0.25 pa");
                addNote("P 0.75");
                addNote("C4 0.25 pa");
                addNote("P 0.75");
                addNote("C4 1.75 a"); 
                addNote("P 0.25");
                addNote("C4 0.5 a");
                addNote("D4 0.5 i");
                addNote("E4 0.5 a");
                addNote("D4 0.5 i");
                addNote("C4 2 a");
            }
        }

        private void guidedSingingStopFunc()
        {
            btnGuidedSinging.Enabled = true;
            btnFreeSinging.Enabled = true;
            btnSing.Enabled = true;
            btnPlayMelody.Enabled = true;
            btnDiscardLatest.Enabled = true;
            btnReplayCurSample.Enabled = true;

            StopPlayback();

            timer.Enabled = false;

            if (gs.FreeSinging) btnTranscribe.Enabled = true;

            //keeps visualization
            replayingSample = true;
            replayFreqSamples.Clear(); replayIntensSamples.Clear();
            replayPlayTime = (float)gs.trackPlayTime;
            replayFreqSamples.Add(gs.acqFreqSamples); replayIntensSamples.Add(gs.acqIntensSamples);
            //if (idxSing > 0 && idxSing % 1 == 0) btnSaveSings_Click(this, new EventArgs());

            //Set melody notes to black again
            ResetToBlack(melodyViewer);

            if (replaySample != null) replaySample.Dispose();
            replaySample = new SampleAudio(gs.rtSingRecord);

            //redraw
            picViewMelody.Invalidate();
        }
        int idxSing = 0;
        private void btnGuidedSinging_Click(object sender, EventArgs e)
        {
            StartSinging(false);
        }
        private void btnFreeSinging_Click(object sender, EventArgs e)
        {
            StartSinging(true);
        }

        private void StartSinging(bool freeSinging)
        {
            if (ssMelody != null) replayPlayTime = (float)ssMelody.GetTrackPlayTime(0);

            curTimeBarOffset = 0;
            if (playingMelody) return;
            if (ssMelody.Notes[0].Count == 0) return;
            if (sv != null && ssMelody != null) sv.ApplyPhonemes(ssMelody.Notes[0]);

            replayingSample = false;
            ClearGuidedSinging();
            
            gs.FreeSinging = freeSinging;

            //playback
            if (playbackSample != null) PlayPlayback(0, playbackSample.time[playbackSample.time.Length - 1]);

            //Play video
            if (dxplayer != null)
            {
                dxplayer.Stop();
                dxplayer.Start();
            }

            gs.StartGuidedSinging("seq" + idxSing.ToString() + " " + txtMelodyName.Text); idxSing++;
            btnGuidedSinging.Enabled = false;
            btnReplayCurSample.Enabled = false;
            btnFreeSinging.Enabled = false;
            btnSing.Enabled = false;
            btnPlayMelody.Enabled = false;
            btnDiscardLatest.Enabled = false;
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

            ColorNotes(ssMelody,melodyViewer);
        }

        /// <summary>Color current notes in a melody viewer</summary>
        /// <param name="ssMelody">Melody</param>
        /// <param name="melodyViewer">Melody viewer</param>
        /// <param name="offSet">Note offset</param>
        public static void ColorNotes(SoundSynthesis ssMelody, IncipitViewer melodyViewer)
        {
            if (ssMelody != null && melodyViewer != null && ssMelody.sampleSynthesizer != null && ssMelody.sampleSynthesizer.curNote.Count > 0)
            {
                int curNote = ssMelody.sampleSynthesizer.curNote[0];
                if (curNote + 2 < melodyViewer.CountIncipitElements)
                {
                    melodyViewer.IncipitElement(curNote + 2).MusicalCharacterColor = Color.Red;
                    if (curNote > 0) melodyViewer.IncipitElement(curNote + 2 - 1).MusicalCharacterColor = Color.Black;

                    melodyViewer.Refresh();
                }
                else melodyViewer.IncipitElement(melodyViewer.CountIncipitElements - 1).MusicalCharacterColor = Color.Black;
            }
        }
        /// <summary>Resets all characters to black</summary>
        /// <param name="melodyViewer">Melody viewer</param>
        public static void ResetToBlack(IncipitViewer melodyViewer)
        {
            for (int k = 0; k < melodyViewer.CountIncipitElements; k++) melodyViewer.IncipitElement(k).MusicalCharacterColor = Color.Black;
            melodyViewer.Refresh();
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

            if (Directory.Exists(PratiCantoForms.ClientFolder)) UserFolder = PratiCantoForms.ClientFolder;
            else UserFolder = Application.StartupPath + "\\" + PratiCantoForms.ClientFolder;

            if (!Directory.Exists(UserFolder)) Directory.CreateDirectory(UserFolder);

            string recFolder = PratiCantoForms.getRecFolder();
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

                double nHalfTones0 = SoundSynthesis.Note.GetHalfStepsAboveA4(n0.Frequency[0]);
                double nHalfTonesf = SoundSynthesis.Note.GetHalfStepsAboveA4(nf.Frequency[0]);


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
            int lineWidth = 5;
            float WW = picViewMelody.Width;
            float HH = picViewMelody.Height;

            //Zoom
            float zoomX0 = 0, zoomY0 = 0, zoomScaleX = 1, zoomScaleY = 1;
            if (!clickSelect && (selP0.X != 0 || selP0.Y != 0) && (selP0.X != selPf.X && selP0.X != selPf.Y))
            {
                zoomX0 = -Math.Min(selP0.X, selPf.X);
                zoomY0 = HH - Math.Max(selP0.Y, selPf.Y);
                zoomScaleX = (float)picViewMelody.Width / Math.Abs(selP0.X - selPf.X);
                zoomScaleY = (float)picViewMelody.Height / Math.Abs(selP0.Y - selPf.Y);

                if (selP0.X == selPf.X) zoomScaleX = picViewMelody.Width;
                if (selP0.Y == selPf.Y) zoomScaleY = picViewMelody.Height;
            }

            if (replayPlayTime == 0)
            {
                replayPlayTime = 1;
                if (ssMelody != null) replayPlayTime = (float)ssMelody.GetTrackPlayTime(0);
                if (replayPlayTime == 0) replayPlayTime = 1;
            }

            float scalex = (float)(WW / replayPlayTime);
            float scaley = (float)(HH / maxDrawFrequency);

            DrawNoteGrid(e.Graphics, picViewMelody.Width, picViewMelody.Height, zoomY0, zoomScaleY);

            if (ssMelody != null)
            {
                ssMelody.DrawTrack(e.Graphics, 0, zoomX0, zoomY0, (int)(picViewMelody.Width), (int)(picViewMelody.Height), maxDrawFrequency, zoomScaleX, zoomScaleY);
                //plot intensity reference
                PlotIntensityRef(e.Graphics, zoomScaleX, zoomX0, scalex, zoomScaleY, zoomY0, scaley, HH);
            }

            #region Plot replay sample
            float time = (float)swReplaySample.Elapsed.TotalSeconds;
            if (replayingSample)
            {

                e.Graphics.DrawLine(Pens.Red, zoomScaleX * (zoomX0 + (curTimeBarOffset + time) * scalex), 0, zoomScaleX * (zoomX0 + (curTimeBarOffset + time) * scalex), HH);

                if (replayFreqSamples != null)
                {
                    Pen pen = Pens.Blue;
                    foreach (List<PointF> fSamples in replayFreqSamples)
                    {
                        List<PointF> plotFreqData = new List<PointF>();
                        foreach (PointF p in fSamples) plotFreqData.Add(new PointF(zoomScaleX * (zoomX0 + p.X * scalex), HH + zoomScaleY * (zoomY0 - p.Y * scaley)));
                        if (plotFreqData.Count > 1) e.Graphics.DrawLines(pen, plotFreqData.ToArray());
                        pen = Pens.Red;
                    }
                }

                if (replayIntensSamples != null && intensityGraphToolStripMenuItem.Checked)
                {
                    Pen pen = Pens.Orange;
                    foreach (List<PointF> iSamples in replayIntensSamples)
                    {
                        List<PointF> plotIntensData = new List<PointF>();
                        foreach (PointF p in iSamples)
                        {
                            float intens = p.Y * intensAmplification;
                            plotIntensData.Add(new PointF(zoomScaleX * (zoomX0 + p.X * scalex), HH + zoomScaleY * (zoomY0 - intens * scaley)));
                        }
                        if (plotIntensData.Count > 1) e.Graphics.DrawLines(pen, plotIntensData.ToArray());
                        pen = Pens.DarkGreen;
                    }
                }
            }
            #endregion

            #region Guided singing plot
            if (gs != null && gs.isPlayingGuidedSinging)
            {
                //float scalex = (float)(WW / gs.trackPlayTime);
                //float scaley = (float)(HH / maxDrawFrequency);

                //plot frequency
                List<PointF> plotFreqData = new List<PointF>();
                foreach (PointF p in gs.acqFreqSamples)
                {
                    float xCoordLocal = zoomScaleX * (zoomX0 + p.X * scalex);
                    plotFreqData.Add(new PointF(xCoordLocal, HH + zoomScaleY * (zoomY0 - p.Y * scaley)));
                }
                if (plotFreqData.Count > 1) e.Graphics.DrawLines(new Pen(Color.Blue,lineWidth), plotFreqData.ToArray());

                //plot intensity
                if (intensityGraphToolStripMenuItem.Checked)
                {
                    List<PointF> plotIntensData = new List<PointF>();
                    foreach (PointF p in gs.acqIntensSamples)
                    {
                        float intens = p.Y * intensAmplification;
                        plotIntensData.Add(new PointF(zoomScaleX * (zoomX0 + p.X * scalex), HH + zoomScaleY * (zoomY0 - intens * scaley)));
                    }
                    if (plotIntensData.Count > 1) e.Graphics.DrawLines(new Pen(Color.DarkOrange, lineWidth), plotIntensData.ToArray());
                }

                e.Graphics.DrawLine(Pens.Red, zoomScaleX * (zoomX0 + gs.lastTime * scalex), 0, zoomScaleX * (zoomX0 + gs.lastTime * scalex), HH);
                e.Graphics.DrawString("F0:" + gs.lastFreq.ToString() + " Hz\r\nI:" + gs.lastIntens.ToString() + " dB", ff, Brushes.Black, 0, 0);

                //draw note candidates
                if (gs.FreeSinging && chkShowTimesNotes.Checked)
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

                //float scaley = (float)(HH / maxDrawFrequency);

                string strFreq = Math.Round((picViewMelody.Height - selP0.Y) / scaley, 2).ToString() + " Hz";

                float intensLocal = (HH - (float)clickCoords.Y) / (intensAmplification * scaley);
                if (intensLocal < 130) strFreq += " " + Math.Round(intensLocal, 1) + " dBr";

                SizeF sString = e.Graphics.MeasureString(strFreq, ff);
                e.Graphics.DrawString(strFreq, ff, Brushes.Red, selP0.X, selP0.Y - sString.Height);

            }

            #endregion

            #region Jitter and shimmer, time, mean/median F0, errors
            int h0 = 40;
            if (chkShowTimesNotes.Checked && replayingSample && (!clickSelect && (selP0.X != 0 || selP0.X != 0) && (selP0.X != selPf.X && selP0.X != selPf.Y)))
            {
                //float scalex = (float)(WW / replayPlayTime);

                float t0 = replayPlayTime / picViewMelody.Width * selP0.X;
                float tf = replayPlayTime / picViewMelody.Width * selPf.X;

                e.Graphics.DrawString(Math.Round(tf - t0, 3).ToString() + " s", ff, Brushes.Black, 0, h0); h0 += 13;

                if (tf < t0)
                {
                    float temp = t0; t0 = tf; tf = temp;
                }
                List<float> refFreqs = new List<float>();
                List<float> sampleFreqs = new List<float>();
                List<float> sampleIntens = new List<float>();
                List<PointF> plotErrors = new List<PointF>();

                for (int k = 0; k < replayFreqSamples[0].Count; k++)
                {
                    if (t0 <= replayFreqSamples[0][k].X && replayFreqSamples[0][k].X <= tf)
                    {
                        sampleFreqs.Add(replayFreqSamples[0][k].Y);
                        sampleIntens.Add(replayIntensSamples[0][k].Y);
                        refFreqs.Add((float)ssMelody.GetPlayFrequency(replayFreqSamples[0][k].X, 0));
                        plotErrors.Add(new PointF(zoomScaleX * (zoomX0 + replayFreqSamples[0][k].X * scalex), 0));
                    }
                }
                
                //frequency error
                List<float> freqErr = new List<float>();
                List<float> freqErrAbs = new List<float>();


                float prevNote = 0;
                float entryErrors = 0;
                float allZero = 0;
                float errFac = 0.01f;
                for (int k = 0; k < refFreqs.Count; k++)
                {
                    if (sampleFreqs[k] != 0 && refFreqs[k] != 0)
                    {
                        prevNote = (float)SoundSynthesis.Note.GetHalfStepsAboveA4(sampleFreqs[k]);

                        float val = prevNote - (float)SoundSynthesis.Note.GetHalfStepsAboveA4(refFreqs[k]);
                        freqErr.Add(val);
                        freqErrAbs.Add(Math.Abs(val));
                        if (errorGraphToolStripMenuItem.Checked) plotErrors[k] = new PointF(plotErrors[k].X, Math.Min(HH * 0.17f, Math.Abs(val) * HH * errFac));
                    }
                    else if (sampleFreqs[k] == 0 && refFreqs[k] != 0)
                    {
                        if (prevNote == 0) entryErrors++; //did not start singing
                        else //some consonant kicked in. assume previous frequency
                        {
                            float val = prevNote - (float)SoundSynthesis.Note.GetHalfStepsAboveA4(refFreqs[k]);
                            freqErr.Add(val);
                            freqErrAbs.Add(Math.Abs(val));
                            if (errorGraphToolStripMenuItem.Checked) plotErrors[k] = new PointF(plotErrors[k].X, Math.Min(HH * 0.17f, Math.Abs(val) * HH * errFac));
                        }
                    }
                    else if (sampleFreqs[k] != 0 && refFreqs[k] == 0)
                    {
                        //sung when should not
                        entryErrors += 1;
                    }
                    else //both zero
                    {
                        allZero++;
                        prevNote = 0; //clear previous frequency
                    }
                }
                entryErrors /= ((float)refFreqs.Count - allZero);
                if (freqErr.Count == 0)
                {
                    freqErr.Add(0);
                    freqErrAbs.Add(0);
                }

                float stdDevFreqErr, stdDevFreqErrAbs;
                float avgFreqErr = SampleAudio.RealTime.getMean(freqErr.ToArray(), out stdDevFreqErr);
                float avgFreqErrAbs =SampleAudio.RealTime.getMean(freqErrAbs.ToArray(), out stdDevFreqErrAbs);
                freqErr.Sort();
                freqErrAbs.Sort();
                if (entryErrors != 1)
                {
                    string s = "TempoErr: " + Math.Round(100 * entryErrors, 1) + "% ";
                    s += "Errors in halftones: ";
                    //s += "F0AbsAvgErr: " + Math.Round(avgFreqErrAbs, 3) + " ";
                    s += "F0AbsMedianErr: " + Math.Round(freqErrAbs[freqErrAbs.Count >> 1], 3) + " ";
                    s += "F0AvgErr: " + Math.Round(avgFreqErr, 3) + " ";
                    s += "F0StdDevErr: " + Math.Round(stdDevFreqErr, 3) + " ";
                    e.Graphics.DrawString(s, ff, Brushes.Black, 0, h0); h0 += 13;
                }
                //plot errors
                if (errorGraphToolStripMenuItem.Checked)
                {
                    Pen pErr = new Pen(Color.FromArgb(200, 255, 50, 50), 3);
                    e.Graphics.DrawLines(pErr, plotErrors.ToArray());
                }

                //Mean/median frequency
                List<float> meanMedianFreq = new List<float>();
                foreach (float val in sampleFreqs) if (val > 0) meanMedianFreq.Add(val);
                if (meanMedianFreq.Count > 0)
                {
                    float meanF0 = meanMedianFreq.Sum() / (float)meanMedianFreq.Count;
                    meanMedianFreq.Sort();
                    float medianF0 = meanMedianFreq[meanMedianFreq.Count >> 1];
                    e.Graphics.DrawString("F0avg: " + Math.Round(meanF0, 1).ToString() + "Hz  F0median: " + Math.Round(medianF0, 1).ToString() + "Hz", ff, Brushes.Black, 0, h0); h0 += 13;
                }

                //jitter and shimmer
                float[] jitter = SampleAudio.RealTime.JitterShimmer.ComputeJitter(sampleFreqs.ToArray());
                float[] shimmer = SampleAudio.RealTime.JitterShimmer.ComputeShimmer(sampleIntens.ToArray());

                string jitString = "Jitta: " + Math.Round(jitter[0], 3).ToString() + " ms; ";
                jitString += "Jitt: " + Math.Round(jitter[1], 3).ToString() + "%; ";
                jitString += "Rap: " + Math.Round(jitter[2], 3).ToString() + "%; ";
                jitString += "ppq5: " + Math.Round(jitter[3], 3).ToString() + "%; ";

                Color jitColor = Color.Black;
                if (jitter[0] > 83.2 || jitter[1] > 1.04 || jitter[2] > 0.68 || jitter[3] > 0.84) jitColor = Color.Red;

                if (!float.IsNaN(jitter[1])) e.Graphics.DrawString(jitString, ff, new SolidBrush(jitColor), 0, h0); h0 += 13;


                string shimmerString = "ShdB: " + Math.Round(shimmer[0], 3).ToString() + " dB; ";
                shimmerString += "Shim: " + Math.Round(shimmer[1], 3).ToString() + "%; ";
                //shimmerString += "apq3: " + Math.Round(shimmer[2], 3).ToString() + "%; ";
                //shimmerString += "apq5: " + Math.Round(shimmer[3], 3).ToString() + "%; ";
                //shimmerString += "apq11: " + Math.Round(shimmer[4], 3).ToString() + "%; ";
                Color shimmerColor = Color.Black;
                if (shimmer[0] > 0.35 || shimmer[1] > 3.81/* || shimmer[4] > 3.07*/) shimmerColor = Color.Red;
                if (!float.IsNaN(shimmer[1])) e.Graphics.DrawString(shimmerString, ff, new SolidBrush(shimmerColor), 0, h0); h0 += 13;

            }
            #endregion

            #region Lyrics
            if (showLyricsToolStripMenuItem.Checked && (zoomScaleX == 1 || (gs != null && gs.woGuide != null && gs.woGuide.PlaybackState == PlaybackState.Playing)))
            {
                drawLyrics(e.Graphics, gs.lastTime, (float)gs.trackPlayTime, WW, HH);
            }
            #endregion
        }
        #endregion

        private void btnStopAll_Click(object sender, EventArgs e)
        {
            ssMelody.sampleSynthesizer.GoToEnd();
            StopPlayback();
            if (replayingSample && woReplayFile != null) woReplayFile.Stop();
        }

        #region Wave shape
        static SoundSynthesis.WaveShape wsPiano = new SoundSynthesis.WaveShapePiano();
        static SoundSynthesis.WaveShape wsSine = new SoundSynthesis.WaveShapeSine();
        static SoundSynthesis.WaveShape wsWhiteNoise = new SoundSynthesis.WaveShapeWhiteNoise();
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
        private void whiteNoiseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            noteWaveShape = wsWhiteNoise;
            RedoSoundWaveshape();
        }
        private void RedoSoundWaveshape()
        {
            sv = null;
            foreach (SoundSynthesis.Note n in ssMelody.Notes[0]) n.waveShape = noteWaveShape;
        }

        private void shapeFromfileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string folder = Application.StartupPath;
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Wave Shape|*.wshape";

            ofd.InitialDirectory = folder;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    this.noteWaveShape = SoundSynthesis.WaveShapeCustom.FromFile(ofd.FileName);
                }
                catch
                {
                    noteWaveShape = new SoundSynthesis.WaveShapePiano();
                }
                RedoSoundWaveshape();
            }
        }
        #endregion


        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox ab = new AboutBox();
            ab.ShowDialog();
        }


        #region Client management
        
        private void btnClient_Click(object sender, EventArgs e)
        {
            PratiCantoForms.ShowClientForm();

            //SetClient();
        }
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
            ofd.Multiselect = true;
            ofd.Filter = "Rec|*.wav;*.mp3";

            if (Directory.Exists(PratiCantoForms.ClientFolder)) ofd.InitialDirectory = PratiCantoForms.ClientFolder;

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                ReplayFile(ofd.FileName);

                btnReplayMultiple.DropDownItems.Clear();
                if (ofd.FileNames.Length > 1)
                {
                    btnReplayMultiple.Visible = true;
                    foreach (string file in ofd.FileNames)
                    {
                        FileInfo fi = new FileInfo(file);
                        ToolStripMenuItem m = new ToolStripMenuItem(fi.Name);
                        m.Click += new EventHandler(m_Click);
                        m.Tag = fi.FullName;
                        btnReplayMultiple.DropDownItems.Add(m);
                    }
                }
                else btnReplayMultiple.Visible = false;
            }
        }
        /// <summary>Play file from menu</summary>
        void m_Click(object sender, EventArgs e)
        {
            try
            {
                if (woReplayFile.PlaybackState == PlaybackState.Playing) return;
                ToolStripMenuItem m = (ToolStripMenuItem)sender;
                string fileToReplay = (string)m.Tag;
                ReplayFile(fileToReplay);

            }
            catch { }
        }

        /// <summary>Replay Voice Training file</summary>
        /// <param name="repFile">Voice training file</param>
        private void ReplayFile(string repFile)
        {

            if (replaySample != null) replaySample.Dispose();
            replaySample = new SampleAudio(repFile);

            //extract graphs
            replayFreqSamples.Clear(); replayIntensSamples.Clear();
            bool loadedIntensF0 = true;
            try
            {
                if (File.Exists(repFile + "freq")) replayFreqSamples.Add(GuidedSinging.GetPlotFromString(File.ReadAllText(repFile + "freq")));
                else loadedIntensF0 = false;
            }
            catch
            {
                loadedIntensF0 = false;
            }
            try
            {
                if (File.Exists(repFile + "intens")) replayIntensSamples.Add(GuidedSinging.GetPlotFromString(File.ReadAllText(repFile + "intens")));
                else loadedIntensF0 = false;
            }
            catch
            {
                loadedIntensF0 = false;
            }

            if (!loadedIntensF0)
            {
                replaySample.ComputeBaseFreqAndFormants(0, replaySample.time[replaySample.time.Length - 1], "0", "3e-3", "6", "0", false);
                List<PointF> fSamples = new List<PointF>();
                List<PointF> fIntens = new List<PointF>();
                for (int k = 0; k < replaySample.baseFreqsTimes.Count; k++)
                {
                    if (!float.IsInfinity(replaySample.baseFreqsF0[k]) && !float.IsInfinity(replaySample.baseFreqsIntensity[k]) && !float.IsNaN(replaySample.baseFreqsF0[k]) && !float.IsNaN(replaySample.baseFreqsIntensity[k]))
                    {
                        fSamples.Add(new PointF(replaySample.baseFreqsTimes[k], replaySample.baseFreqsF0[k]));
                        fIntens.Add(new PointF(replaySample.baseFreqsTimes[k], replaySample.baseFreqsIntensity[k]));
                    }
                }
                replayFreqSamples.Add(fSamples);
                replayIntensSamples.Add(fIntens);
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
            if (s != "") PutMelodyString(s);

            ReplayCurSample(0, replaySample.time[replaySample.time.Length - 1]);

            compareToolStripMenuItem.Enabled = true;
        }


        void wo_PlaybackStopped(object sender, StoppedEventArgs e)
        {
            StopPlayback();
            ResetToBlack(melodyViewer);
            swReplaySample.Stop();
            //replayingSample = false;
            timer.Enabled = false;
        }

        private void compareToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Rec|*.wav;*.mp3";

            ofd.Multiselect = true;

            if (Directory.Exists(PratiCantoForms.ClientFolder)) ofd.InitialDirectory = PratiCantoForms.ClientFolder;

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
        PointF clickCoords = new PointF();

        bool ShowClickedPt = false;
        private void picViewMelody_MouseDown(object sender, MouseEventArgs e)
        {
            if (dynamicPtSelId >= 0 && selP0.X == 0 && selP0.Y == 0)
            {
                //start moving dynamics point
                clickMoveDynamic = true;
                return;
            }

            if (showLyricsToolStripMenuItem.Checked) StartSecondLyricLine(gs.lastTime, (float)gs.trackPlayTime, e.Y);
            
            if (showLyricsToolStripMenuItem.Checked && selectedLyric != null && (gs != null && (gs.woGuide == null || (gs.woGuide != null && gs.woGuide.PlaybackState != PlaybackState.Playing))))
            {
                clickMoveLyrics = true;
                moveLyricX = e.X;
                moveLyricPos0 = selectedLyric.StartTimePerc;
            }
            else if (selectedLyric == null)
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
            }
            picViewMelody.Invalidate();
        }
        private void picViewMelody_MouseUp(object sender, MouseEventArgs e)
        {
            if (clickMoveDynamic)
            {
                clickMoveDynamic = false;
                adjustIntensTrainingVec();
            }
            
            clickMoveLyrics = false;

            if (e.Button == System.Windows.Forms.MouseButtons.Left && selectedLyric == null)
            {

                //clicked the same place
                ShowClickedPt = (selP0.X == e.X && selP0.Y == e.Y);
                clickCoords = new PointF(e.X, e.Y);
                clickSelect = false;
                picViewMelody.Invalidate();
            }
        }
        private void picViewMelody_MouseMove(object sender, MouseEventArgs e)
        {
            if (clickMoveDynamic)
            {
                moveDynPt(e.X, e.Y);
                return;
            }

            //select point in dynamics training
            selectDynamicPt(e.X, e.Y);

            if (showLyricsToolStripMenuItem.Checked)
            {
                SelLyric(e.X, e.Y, picViewMelody.Width);
            }

            if (selectedLyric == null && clickSelect)
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
            chkModulateFirstNote.Checked = true;
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

        private void btnDiscardLatest_Click(object sender, EventArgs e)
        {
            btnStopAll_Click(sender, e);
            if (ssMelody.Notes[0].Count == 0) return;
            replayingSample = false;
            ClearGuidedSinging();

            gs.FreeSinging = true;
            picViewMelody.Invalidate();
        }

        #region Replay region
        private void btnReplayCurSample_Click(object sender, EventArgs e)
        {
            float t0 = 0;
            float tf = replayPlayTime;
            if (replayingSample && (!clickSelect && (selP0.X != 0 || selP0.X != 0) && (selP0.X != selPf.X && selP0.X != selPf.Y)))
            {
                t0 = replayPlayTime / picViewMelody.Width * selP0.X;
                tf = replayPlayTime / picViewMelody.Width * selPf.X;
            }
            ReplayCurSample(t0, tf);
        }

        /// <summary>Offset in time bar</summary>
        private float curTimeBarOffset = 0;

        private void ReplayCurSample(float t0, float tf)
        {
            if (woReplayFile != null && woReplayFile.PlaybackState == PlaybackState.Playing) return;
            if (replaySample != null)
            {
                replayPlayTime = (float)ssMelody.GetTrackPlayTime(0);
                if (replayPlayTime == 0) replayPlayTime = 1;

                SampleAudio.PlaySample ps = new SampleAudio.PlaySample(replaySample, t0, tf);
                //ps.PlayTimeFunc = DrawTime;
                //ps.ReplaySpeed = 0.01 * (double)repSpeedPercent;

                swReplaySample.Reset();
                swReplaySample.Start();

                woReplayFile = new WaveOut();
                woReplayFile.PlaybackStopped += new EventHandler<StoppedEventArgs>(wo_PlaybackStopped);
                woReplayFile.Init(ps);
                replayingSample = true;
                timer.Enabled = true;

                curTimeBarOffset = t0;
                woReplayFile.Play();
                PlayPlayback(t0, tf);
            }
        }

        #endregion

        #region Intensity selection
        float intensAmplification = 1;
        private void toolStripMenuItemIntens0_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem tsmi = (ToolStripMenuItem)sender;

            toolStripMenuItemIntens0.Checked = false;
            toolStripMenuItemIntens1.Checked = false;
            toolStripMenuItemIntens2.Checked = false;
            toolStripMenuItemIntens3.Checked = false;

            tsmi.Checked = true;

            intensAmplification = float.Parse(tsmi.Text);

            picViewMelody.Invalidate();
        }

        #endregion

        private void frmPractice_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (woPlayMelody != null) woPlayMelody.Dispose();
            if (woReplayFile != null) woReplayFile.Dispose();
            if (gs != null && gs.woGuide != null) gs.woGuide.Dispose();
            if (dxplayer != null) dxplayer.Dispose();
        }


        #region Lyrics

        /// <summary>Song lyrics</summary>
        List<LyricLine> Lyrics = new List<LyricLine>();

        /// <summary>Encapsulates each line of lyrics</summary>
        private class LyricLine
        {
            /// <summary>Creates a new lyric line</summary>
            /// <param name="text">Text to display</param>
            /// <param name="f">Font, to measure string</param>
            public LyricLine(string text, Font f)
            {
                SetString(text, f);
            }

            /// <summary>Sets text and font of this line</summary>
            /// <param name="text">New text</param>
            /// <param name="f">New font</param>
            public void SetString(string text, Font f)
            {
                this.Text = text.Trim();
                drawSize = MeasureString(text, f);
            }

            /// <summary>Lyric text</summary>
            public string Text;

            /// <summary>Draw size</summary>
            public SizeF drawSize;

            /// <summary>Start time, in % of total music duration</summary>
            public float StartTimePerc = 0;

            public override string ToString()
            {
                return Text;
            }

            #region Static methods

            /// <summary>Dummy bitmap to measure string</summary>
            static Bitmap dumBmp = new Bitmap(1, 1);
            /// <summary>Dummy Graphic to measure string</summary>
            static Graphics g;

            /// <summary>Measure string size</summary>
            public static SizeF MeasureString(string s, Font font)
            {
                SizeF result;
                if (g == null) g = Graphics.FromImage(dumBmp);

                result = g.MeasureString(s, font);

                return result;
            }

            #endregion

        }

        /// <summary>Selected lyric</summary>
        LyricLine selectedLyric = null;
        bool clickMoveLyrics = false;
        float moveLyricX = 0;
        /// <summary>Initial start time percentage</summary>
        float moveLyricPos0 = 0;

        /// <summary>Is mouse in position to select a lyric line? Move lyric if clicked</summary>
        private void SelLyric(int X, int Y, int WW)
        {
            if ((selP0.X != 0 || selP0.Y != 0) && (selP0.X != selPf.X && selP0.X != selPf.Y))
            {
                selectedLyric = null;
                return;
            }

            if (gs != null && gs.woGuide != null && gs.woGuide.PlaybackState == PlaybackState.Playing)
            {
                selectedLyric = null;
                return;
            }

            bool needInvalidate = false;
            if (clickMoveLyrics && selectedLyric != null)
            {
                selectedLyric.StartTimePerc = moveLyricPos0 + (float)(X - moveLyricX) / (float)WW;

                int idMove = Lyrics.IndexOf(selectedLyric);
                for (int k = idMove; k < Lyrics.Count; k++) if (Lyrics[k].StartTimePerc < selectedLyric.StartTimePerc) Lyrics[k].StartTimePerc = selectedLyric.StartTimePerc;

                needInvalidate = true;
            }
            else
            {
                float curY = 0;
                LyricLine prevSel = selectedLyric;
                selectedLyric = null;
                for (int k = 0; k < Lyrics.Count; k++)
                {
                    LyricLine L = Lyrics[k];
                    float xDraw = L.StartTimePerc * WW;
                    if (xDraw <= X && X <= xDraw + L.drawSize.Width && curY <= Y && Y <= curY + L.drawSize.Height)
                    {
                        selectedLyric = L;
                        break;
                    }

                    curY += L.drawSize.Height;
                }
                if (prevSel != selectedLyric) needInvalidate = true;
            }

            if (needInvalidate) picViewMelody.Invalidate();
        }

        Font lyrFont = new Font("Arial", 25, FontStyle.Regular);
        Font lyrFontSmall = new Font("Arial", 10, FontStyle.Regular);
        private void fontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FontDialog fd = new FontDialog();
            fd.Font = lyrFont;
            if (fd.ShowDialog() == DialogResult.OK)
            {
                lyrFont = fd.Font;
                picViewMelody.Invalidate();
            }
        }
        private void globalViewFontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FontDialog fd = new FontDialog();
            fd.Font = lyrFontSmall;
            if (fd.ShowDialog() == DialogResult.OK)
            {
                lyrFontSmall = fd.Font;
                RecomputeTextSizes();
                picViewMelody.Invalidate();
            }
        }

        /// <summary>Index of subtitle string to draw during practice</summary>
        int[] idDraw = new int[] { -1, -1 };

        /// <summary>Sets second lyric line to start at the time of click</summary>
        private void StartSecondLyricLine(float curTime, float totalTime, int YClickPos)
        {
            if (gs != null && gs.woGuide != null && gs.woGuide.PlaybackState == PlaybackState.Playing && idDraw[1] >= 0 && idDraw[1] < Lyrics.Count)
            {
                if (Lyrics.Count > 0)
                {
                    SizeF bigSize = LyricLine.MeasureString(Lyrics[0].Text, lyrFont);
                    if (YClickPos < 2 * bigSize.Height)
                        Lyrics[idDraw[1]].StartTimePerc = curTime / totalTime;
                }
            }
        }

        /// <summary>Draw lyrics</summary>
        /// <param name="g">Graphic element</param>
        /// <param name="isPlayingGuidedSinging">Playing guided singing? If yes, draw everything. If not, only current and next lines</param>
        /// <param name="curTime">Current time</param>
        /// <param name="totalTime">Total playback time</param>
        /// <param name="WW">Image width</param>
        private void drawLyrics(Graphics g, float curTime, float totalTime, float WW, float HH)
        {

            if (gs != null && gs.woGuide != null && gs.woGuide.PlaybackState == PlaybackState.Playing)
            {
                if (Lyrics.Count == 0) return;

                float tRel = curTime / totalTime;
                idDraw = new int[] { -1, -1 };

                if (tRel < Lyrics[0].StartTimePerc) idDraw[1] = 0;
                else if (tRel > Lyrics[Lyrics.Count - 1].StartTimePerc) idDraw[0] = Lyrics.Count - 1;
                else
                {
                    for (int k = 0; k < Lyrics.Count - 1; k++)
                    {
                        if (Lyrics[k].StartTimePerc <= tRel && Lyrics[k + 1].StartTimePerc > tRel)
                        {
                            idDraw[0] = k;
                            idDraw[1] = k + 1;
                            break;
                        }
                    }
                }

                for (int kk = 0; kk < idDraw.Length; kk++)
                {
                    int k = idDraw[kk];
                    if (k >= 0 && k < Lyrics.Count)
                    {
                        LyricLine L = Lyrics[k];
                        SizeF bigSize = LyricLine.MeasureString(L.Text, lyrFont);
                        float curY = bigSize.Height * kk;
                        g.FillRectangle(Brushes.White, 0, curY, bigSize.Width, bigSize.Height);
                        g.DrawRectangle(Pens.Black, 0, curY, bigSize.Width, bigSize.Height);
                        g.DrawString(L.Text, lyrFont, Brushes.Black, 0, curY);
                    }
                }
            }
            else
            {
                float curY = 0;
                foreach (LyricLine L in Lyrics)
                {
                    float xDraw = L.StartTimePerc * WW;

                    if (selectedLyric != null && L == selectedLyric)
                    {
                        g.FillRectangle(Brushes.Yellow, xDraw, curY, L.drawSize.Width, L.drawSize.Height);
                        g.DrawLine(Pens.Yellow, xDraw, curY + L.drawSize.Height, xDraw, HH);
                    }
                    else g.FillRectangle(Brushes.White, xDraw, curY, L.drawSize.Width, L.drawSize.Height);

                    g.DrawRectangle(Pens.Black, xDraw, curY, L.drawSize.Width, L.drawSize.Height);

                    g.DrawString(L.Text, lyrFontSmall, Brushes.Black, xDraw, curY);

                    curY += L.drawSize.Height;
                }
            }
        }

        /// <summary>Recomputes text sizes, when changing font</summary>
        private void RecomputeTextSizes()
        {
            foreach (LyricLine L in Lyrics) L.drawSize = LyricLine.MeasureString(L.Text, lyrFontSmall);
        }

        private void ImportLyrics(string text)
        {
            Lyrics.Clear();
            string[] ss = text.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string s in ss)
            {
                string[] sSplit = s.Split(new string[] { "[", "]" }, StringSplitOptions.RemoveEmptyEntries);
                int timeInMs = 0;
                if (sSplit.Length == 2 && int.TryParse(sSplit[0], out timeInMs))
                {
                    LyricLine LL = new LyricLine(sSplit[1], lyrFontSmall);
                    LL.StartTimePerc = 0.0001f * (float)timeInMs;
                    Lyrics.Add(LL);
                }
                else Lyrics.Add(new LyricLine(s, lyrFontSmall));
            }

            if (Lyrics.Count > 0 && !showLyricsToolStripMenuItem.Checked)
            {
                showLyricsToolStripMenuItem.Checked = true;
                picViewMelody.Invalidate();
            }
        }

        private string ExportLyrics()
        {
            string s = "";

            foreach (LyricLine L in Lyrics)
            {
                s += "[" + Math.Round(L.StartTimePerc * 10000).ToString() + "]" + L.Text + "\r\n";
            }

            return s;
        }

        /// <summary>Show menu for edition</summary>
        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string strLyric = ExportLyrics();
            PratiCanto.VoiceTraining.frmEdit frmEdt = new PratiCanto.VoiceTraining.frmEdit();
            frmEdt.txtLyric.Text = strLyric;
            frmEdt.ShowDialog();
            
            if (frmEdt.txtLyric.Text.Trim() != "")
            {
                ImportLyrics(frmEdt.txtLyric.Text);
                picViewMelody.Invalidate();
            }
        }
        private void showLyricsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            picViewMelody.Invalidate();
            if (!showLyricsToolStripMenuItem.Checked) selectedLyric = null;
        }
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Text|*.lyr";
            ofd.InitialDirectory = Application.StartupPath;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                ImportLyrics(File.ReadAllText(ofd.FileName));
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Text|*.lyr";
            sfd.InitialDirectory = Application.StartupPath;
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                File.WriteAllText(sfd.FileName, ExportLyrics());
            }

        }

        #endregion



        #region Load/clear playback audio and video
        SampleAudio playbackSample;
        private void loadAudioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Rec|*.wav;*.mp3";

            if (Directory.Exists(PratiCantoForms.ClientFolder)) ofd.InitialDirectory = PratiCantoForms.ClientFolder;

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                playbackSample = new SampleAudio(ofd.FileName);
            }
        }
        private void clearAudioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (playbackSample != null) playbackSample.Dispose();
            playbackSample = null;
            closeVideo();
        }
        private void PlayPlayback(float t0, float tf)
        {
            if (playbackSample != null) playbackSample.Play(t0, tf);
            if (dxplayer != null)
            {
                dxplayer.Stop();
                dxplayer.SetTime(t0);
                dxplayer.Start();
            }
        }
        private void StopPlayback()
        {
            if (playbackSample != null) playbackSample.Stop();
            if (dxplayer != null)
            {
                dxplayer.Stop();
                dxplayer.SetTime(0);
            }
        }
        private void menuVol10_Click(object sender, EventArgs e)
        {
            string vol = ((ToolStripMenuItem)sender).Text.Replace("%", "");
            float fvol = 0.01f * float.Parse(vol);
            if (playbackSample != null) playbackSample.SetVolume(fvol);
        }


        DxPlay.DxPlay dxplayer;
        private void openVideoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Video|*.mp4;*.avi;*.mov;*.wma;*.aiff;*.au;*.flv;*.3gp";

            if (Directory.Exists(PratiCantoForms.ClientFolder)) ofd.InitialDirectory = PratiCantoForms.ClientFolder;

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                closeVideo();

                string outaudioFileName;
                dxplayer = PratiCanto.Video.ImportVideoClass.ImportVideo(ofd.FileName, panVideo, out outaudioFileName);

                if (dxplayer != null)
                {
                    playbackSample = new SampleAudio(outaudioFileName);

                    try
                    {
                        //doesn't need audio anymore
                        File.Delete(outaudioFileName);
                    }
                    catch
                    {
                    }
                    panContainer.Visible = true;
                }
                else MessageBox.Show("Can't open video");

                if (File.Exists(outaudioFileName))
                {
                    //doesn't need audio anymore
                    File.Delete(outaudioFileName);
                }
            }
        }

        private void closeVideo()
        {
            if (dxplayer != null)
            {
                dxplayer.Dispose();
                dxplayer = null;
            }
            panContainer.Visible = false;
        }
        bool clickVideo = false;
        private void panContainer_MouseDown(object sender, MouseEventArgs e)
        {
            clickVideo = true;
        }

        private void panContainer_MouseUp(object sender, MouseEventArgs e)
        {
            if (clickVideo)
            {
                dxplayer.SetWindowSize(panVideo);

                clickVideo = false;
            }
        }

        private void panContainer_MouseMove(object sender, MouseEventArgs e)
        {
            if (clickVideo)
            {
                int left = picViewMelody.Width - panContainer.Width + e.X;
                int width = panContainer.Width - e.X;
                int height = e.Y;
                if (width > 50 && height > 50)
                {
                    panContainer.Left = left;
                    panContainer.Width = width;
                    panContainer.Height = height;


                }
            }
        }
        #endregion


        #region Dynamics / intensity training
        List<PointF> intensTrainingRef = new List<PointF>();
        private void showReferenceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (showReferenceToolStripMenuItem.Checked)
            {

                if (ssMelody != null) replayPlayTime = (float)ssMelody.GetTrackPlayTime(0);
                if (replayPlayTime == 0) replayPlayTime = 1;

                intensityGraphToolStripMenuItem.Checked = true;
                toolStripMenuItemIntens0_Click(toolStripMenuItemIntens1, e);

                if (intensTrainingRef.Count == 0)
                {
                    int NN = 10;
                    for (int i = 0; i <= NN; i++)
                    {
                        float tRef = (float)i * (1.0f / (float)NN) * replayPlayTime;
                        intensTrainingRef.Add(new PointF(tRef, 25));
                    }
                }

            }
            picViewMelody.Invalidate();
        }

        /// <summary>Plots intensity reference</summary>
        private void PlotIntensityRef(Graphics g, float zoomScaleX, float zoomX0, float scalex, float zoomScaleY, float zoomY0, float scaley, float HH)
        {
            if (!showReferenceToolStripMenuItem.Checked) return;

            Pen pen = new Pen(Color.DarkOrange, 3);
            PointF[] scaledIntensTRef = ComputeScaledPts(intensTrainingRef, zoomScaleX, zoomX0, scalex, zoomScaleY, zoomY0, scaley, HH, intensAmplification);

            g.DrawLines(pen, scaledIntensTRef);
            for (int k = 0; k < intensTrainingRef.Count; k++)
            {
                PointF pp = scaledIntensTRef[k];
                Brush bb = Brushes.DarkOrange;
                if (dynamicPtSelId == k) bb = Brushes.Yellow;
                g.FillRectangle(bb, pp.X - dynamicPtSelIdWindow, pp.Y - dynamicPtSelIdWindow, dynamicPtSelIdWindow << 1, dynamicPtSelIdWindow << 1);
            }
        }

        /// <summary>Compute scaled points</summary>
        private static PointF[] ComputeScaledPts(List<PointF> ptOrig, float zoomScaleX, float zoomX0, float scalex, float zoomScaleY, float zoomY0, float scaley, float HH, float intensAmp)
        {
            PointF[] ans = new PointF[ptOrig.Count];

            for (int k = 0; k < ptOrig.Count; k++)
            {
                PointF p = ptOrig[k];
                float intens = p.Y * intensAmp;
                ans[k] = new PointF(zoomScaleX * (zoomX0 + p.X * scalex), HH + zoomScaleY * (zoomY0 - intens * scaley));
            }

            return ans;
        }

        /// <summary>Moving dynamic point?</summary>
        bool clickMoveDynamic = false;

        /// <summary>Selected point in dynamic training</summary>
        int dynamicPtSelId = -1;
        int dynamicPtSelIdWindow = 5;
        /// <summary>Select point in dynamic training</summary>
        void selectDynamicPt(int X, int Y)
        {
            int prevSel = dynamicPtSelId;
            dynamicPtSelId = -1;
            if (!showReferenceToolStripMenuItem.Checked) return;

            float HH = picViewMelody.Height;
            float WW = picViewMelody.Width;

            float scalex = (float)(WW / replayPlayTime);
            float scaley = (float)(HH / maxDrawFrequency);

            PointF[] scaledIntensTRef = ComputeScaledPts(intensTrainingRef, 1, 0, scalex, 1, 0, scaley, HH, intensAmplification);
            for (int k = 0; k < intensTrainingRef.Count; k++)
            {
                PointF pp = scaledIntensTRef[k];
                if (pp.X - dynamicPtSelIdWindow <= X && X <= pp.X + dynamicPtSelIdWindow && pp.Y - dynamicPtSelIdWindow <= Y && Y <= pp.Y + dynamicPtSelIdWindow)
                {
                    dynamicPtSelId = k;
                    break;
                }
            }
            //redraw if selection changed
            if (prevSel != dynamicPtSelId) picViewMelody.Invalidate();
        }

        void moveDynPt(int x, int y)
        {
            if (dynamicPtSelId < 0 || !clickMoveDynamic) return;

            float HH = picViewMelody.Height;
            float WW = picViewMelody.Width;

            float scalex = (float)(WW / replayPlayTime);
            float scaley = (float)(HH / maxDrawFrequency);

            intensTrainingRef[dynamicPtSelId] = new PointF((float)x / scalex, (HH - y) / (intensAmplification * scaley));
            picViewMelody.Invalidate();
        }

        /// <summary>Make sure x is in ascending order in the intensity training vector</summary>
        void adjustIntensTrainingVec()
        {
            intensTrainingRef = intensTrainingRef.OrderBy(p => p.X).ToList<PointF>();
            picViewMelody.Invalidate();
        }

        /// <summary>Export intensity training reference</summary>
        /// <returns></returns>
        string ExportIntensTrainingRef()
        {
            string s = "";
            foreach (PointF pf in intensTrainingRef)
                s += pf.X.ToString() + " " + pf.Y.ToString() + "\r\n";

            return s;
        }

        /// <summary>Import intensity training reference</summary>
        /// <param name="s">Reference to import</param>
        void ImportIntensTrainingRef(string s)
        {
            intensTrainingRef.Clear();
            string[] ss = s.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string s2 in ss)
            {
                try
                {
                    string[] xy = s2.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                    float x = float.Parse(xy[0]);
                    float y = float.Parse(xy[1]);
                    intensTrainingRef.Add(new PointF(x, y));
                }
                catch
                {
                }
            }
            picViewMelody.Invalidate();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string strDyn = ExportIntensTrainingRef();
            PratiCanto.VoiceTraining.frmEdit frmEdt = new PratiCanto.VoiceTraining.frmEdit();
            frmEdt.Text = editToolStripMenuItem.Text;
            frmEdt.txtLyric.Text = strDyn;
            frmEdt.ShowDialog();

            if (frmEdt.txtLyric.Text.Trim() != "")
                ImportIntensTrainingRef(frmEdt.txtLyric.Text.Trim());

        }

        private void saveToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Dynamic|*.dyn";
            sfd.InitialDirectory = Application.StartupPath;
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                File.WriteAllText(sfd.FileName, ExportIntensTrainingRef());
            }
        }

        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Dynamic|*.dyn";
            ofd.InitialDirectory = Application.StartupPath;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                ImportIntensTrainingRef(File.ReadAllText(ofd.FileName));
            }

        }
        #endregion

        #region Voice file
        SyntheticVoice sv;
        private void fromVoiceFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Annotated audio|*.wav";
            ofd.Multiselect = true;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                List<AudioComparer.SampleAudio> lstSa = new List<AudioComparer.SampleAudio>();
                foreach (string f in ofd.FileNames)
                {
                    try
                    {
                        lstSa.Add(new AudioComparer.SampleAudio(f));
                    }
                    catch
                    {
                        MessageBox.Show("Error in " + f);
                    }
                }

                sv = new SyntheticVoice(lstSa);

                foreach (AudioComparer.SampleAudio sa in lstSa) sa.Dispose();
                lstSa.Clear();
            }
        }
        #endregion


        #region Cosmetics
        private void gbViewMelody_Paint(object sender, PaintEventArgs e)
        {
            PratiCanto.LayoutFuncs.MakeDegrade(e.Graphics, Color.FromArgb(230, 230, 230), Color.White, gbViewMelody.Width, gbViewMelody.Height, true);
        }

        private void gbMelody_Paint(object sender, PaintEventArgs e)
        {
            PratiCanto.LayoutFuncs.MakeDegrade(e.Graphics, Color.White, Color.FromArgb(230, 230, 230), gbViewMelody.Width, gbViewMelody.Height, true);
        }
        private void panMelody_Paint(object sender, PaintEventArgs e)
        {
            PratiCanto.LayoutFuncs.MakeDegrade(e.Graphics, Color.White, Color.FromArgb(230, 230, 230), panMelody.Width, panMelody.Height, true);
        }
        private void chkShowTimesNotes_CheckedChanged(object sender, EventArgs e)
        {
            picViewMelody.Invalidate();
        }
        private void groupBox3_Paint(object sender, PaintEventArgs e)
        {
            PratiCanto.LayoutFuncs.DoGroupBoxLayout((GroupBox)sender, e.Graphics);
        }
        #endregion




 










    }
}
