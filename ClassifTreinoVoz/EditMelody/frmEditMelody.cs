using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PSAMControlLibrary;
using NAudio.Wave;
using System.IO;
using NAudio.Midi;

namespace ClassifTreinoVoz
{
    public partial class frmEditMelody : Form
    {
        public frmEditMelody()
        {
            InitializeComponent();

            ssMelody = new SoundSynthesis();
            ssMelody.Notes.Add(new List<SoundSynthesis.Note>());
        }

        IncipitViewer melodyViewer = new IncipitViewer();
        private void frmEditMelody_Load(object sender, EventArgs e)
        {
            //AudioComparer.SoftwareKey.CheckLicense("MelodyCreator", false);

            
            this.Height = gbNotes.Top + gbNotes.Height + 50;
            label1.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Bottom;
            gbNotes.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;

            cmbKeyboardOctave.SelectedIndex = 3;

            gbViewMelody.Controls.Add(melodyViewer);
            //melodyViewer.DrawOnParentControl = true;


            melodyViewer.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
            melodyViewer.Width = gbViewMelody.Width - 10 - btnPlayMelody.Width;
            melodyViewer.Top = btnPlayMelody.Top - (btnPlayMelody.Height >> 2);

                        
            //melodyViewer.AddMusicalSymbol(new Clef(ClefType.GClef, 2));
            //melodyViewer.PlayExternalMidiPlayer += new IncipitViewer.PlayExternalMidiPlayerDelegate(melodyViewer_PlayExternalMidiPlayer);
            melodyViewer.MouseDown += new MouseEventHandler(melodyViewer_MouseDown);

            //MusicalSymbol key = new PSAMControlLibrary.Key(0);
            //melodyViewer.AddMusicalSymbol(key);

            MusicalSymbol ms = new PSAMControlLibrary.Direction();
            //melodyViewer.AddMusicalSymbol(ms);

            woPlayMelody = new WaveOut();
            woPlayMelody.Init(ssMelody.sampleSynthesizer);
            
            woPlayMelody.PlaybackStopped += new EventHandler<StoppedEventArgs>(woPlayMelody_PlaybackStopped);
        }

        /// <summary>Stores melody</summary>
        SoundSynthesis ssMelody;

        /// <summary>Briefly play notes when it is added</summary>
        SoundSynthesis ssBriefPlay = new SoundSynthesis();
        WaveOut woPlayBrief;
        SoundSynthesis.WaveShape noteWaveShape = new SoundSynthesis.WaveShapePiano();

        private void btnAddNote_Click(object sender, EventArgs e)
        {
            if (playingMelody) return;
 
            if (ssMelody.Notes[0].Count == 0 && txtAddNote.Text.ToUpper().StartsWith("P")) return; //cannot start with pause

            try
            {
                string note = txtAddNote.Text;

                //notes in Portuguese
                if (note.ToUpper().StartsWith("DÓ") || note.ToUpper().StartsWith("DO"))
                    note = "C" + note.Substring(2, note.Length - 2);
                else if (note.ToUpper().StartsWith("RÉ") || note.ToUpper().StartsWith("RE"))
                    note = "D" + note.Substring(2, note.Length - 2);
                else if (note.ToUpper().StartsWith("MI"))
                    note = "E" + note.Substring(2, note.Length - 2);
                else if (note.ToUpper().StartsWith("FÁ") || note.ToUpper().StartsWith("FA"))
                    note = "F" + note.Substring(2, note.Length - 2);
                else if (note.ToUpper().StartsWith("SOL"))
                    note = "G" + note.Substring(3, note.Length - 3);
                else if (note.ToUpper().StartsWith("LÁ") || note.ToUpper().StartsWith("LA"))
                    note = "A" + note.Substring(2, note.Length - 2);
                else if (note.ToUpper().StartsWith("SI"))
                    note = "B" + note.Substring(2, note.Length - 2);


                //Adds note to first track
                SoundSynthesis.Note ssNote = SoundSynthesis.Note.FromString(note, noteWaveShape);
                ssMelody.Notes[0].Add(ssNote);

                //briefly play note
                if (ssBriefPlay.Notes.Count == 0)
                {
                    ssBriefPlay.Notes.Add(new List<SoundSynthesis.Note>());
                    woPlayBrief = new WaveOut();
                }
                if (woPlayBrief != null && woPlayBrief.PlaybackState != PlaybackState.Playing && (sender == btnAddNote || sender == melodyViewer))
                {
                    ssBriefPlay.sampleSynthesizer.Reset();
                    ssBriefPlay.Notes[0].Clear();

                    ssBriefPlay.Notes[0].Add(new SoundSynthesis.Note(0.15, ssNote.Frequency[0], noteWaveShape));
                    woPlayBrief.Init(ssBriefPlay.sampleSynthesizer);
                    woPlayBrief.Play();
                }


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

                if (sender == btnAddNote || sender == melodyViewer) txtNotes.Text = GetMelodyString();

            }
            catch
            {
                MessageBox.Show(lblWrongNoteFormat.Text + "\r\n" + txtAddNote.Text, "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtAddNote.Text = "C4 0.5 La";
            }


        }

        //Add notes by clicking
        void melodyViewer_MouseDown(object sender, MouseEventArgs e)
        {
            //add notes by clicking

            //y=50 C4,y=47 D4, y=44 E4, y=41 F4, y=38 G4, y=35 A4, y=32 B4, y=29 C5, y=20 F5, y=5 D6, y=2 E6

            int yMod = (int)Math.Round((e.Y - 2.0) / 3.0);
            string noteStr = "";
            switch (yMod)
            {
                case 0: noteStr = "E6"; break;
                case 1: noteStr = "D6"; break;
                case 2: noteStr = "C6"; break;
                case 3: noteStr = "B5"; break;
                case 4: noteStr = "A5"; break;
                case 5: noteStr = "G5"; break;
                case 6: noteStr = "F5"; break;
                case 7: noteStr = "E5"; break;
                case 8: noteStr = "D5"; break;
                case 9: noteStr = "C5"; break;
                case 10: noteStr = "B4"; break;
                case 11: noteStr = "A4"; break;
                case 12: noteStr = "G4"; break;
                case 13: noteStr = "F4"; break;
                case 14: noteStr = "E4"; break;
                case 15: noteStr = "D4"; break;
                case 16: noteStr = "C4"; break;
                case 17: noteStr = "B3"; break;
                case 18: noteStr = "A3"; break;
                case 19: noteStr = "G3"; break;
                case 20: noteStr = "F3"; break;
                case 21: noteStr = "E3"; break;
                case 22: noteStr = "D3"; break;
                case 23: noteStr = "C3"; break;

                default: noteStr = ""; break;
            }

            //23 -> c3

            if (noteStr != "")
            {
                try
                {
                    string txt = txtAddNote.Text;
                    string[] s = txt.Split();
                    for (int k = 1; k < s.Length; k++) noteStr += " " + s[k];
                    txtAddNote.Text = noteStr;
                    btnAddNote_Click(sender, e);
                }
                catch
                {
                }
            }
        }

        #region Edit/save/load

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
                txtAddNote.Text = s2;
                btnAddNote_Click(this, new EventArgs());
            }
        }

        private void btnSaveMelody_Click(object sender, EventArgs e)
        {
            if (ssMelody.Notes[0].Count <= 0) return;

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.InitialDirectory = Application.StartupPath;
            sfd.Filter = "Melody|*.mld";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                using (StreamWriter sw = new StreamWriter(sfd.FileName)) sw.Write(GetMelodyString());
            }
        }

        private void btnLoadMelody_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.InitialDirectory = Application.StartupPath;
            ofd.Filter = "Melody|*.mld;*.mid";

            if (playingMelody) btnPlayMelody_Click(sender, e);

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                ssMelody.Notes.Clear();
                ssMelody.Notes.Add(new List<SoundSynthesis.Note>());

                if (ofd.FileName.ToLower().EndsWith(".mid"))
                {
                    PratiCanto.EditMelody.frmImportMidi frmImport = new PratiCanto.EditMelody.frmImportMidi(ofd.FileName, ssMelody);
                    frmImport.ShowDialog();

                    numTempo.Value = (decimal)ssMelody.Tempo;

                    string melody = GetMelodyString();
                    txtNotes.Text = melody;
                    PutMelodyString(melody);
                }
                else
                {
                    PutMelodyString(File.ReadAllText(ofd.FileName));
                    txtNotes.Text = GetMelodyString();
                }
            }

        }



        private void btnDelMelody_Click(object sender, EventArgs e)
        {
            melodyViewer.ClearMusicalIncipit();
            melodyViewer.AddMusicalSymbol(new Clef(ClefType.GClef, 2));
            //MusicalSymbol ms = new PSAMControlLibrary.Barline();
            melodyViewer.AddMusicalSymbol(new Direction());


            ssMelody.Notes[0].Clear();
            //ssMelody.Notes.Add(new List<SoundSynthesis.Note>());

            melodyViewer.Invalidate();
        }
        //stop playback if closing form
        private void frmEditMelody_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (playingMelody) btnPlayMelody_Click(sender, e);

            if (woPlayBrief != null) woPlayBrief.Dispose();
            if (woPlayMelody != null) woPlayMelody.Dispose();
        }

        public void btnUpdate_Click(object sender, EventArgs e)
        {
            PutMelodyString(txtNotes.Text);
        }

        #endregion

        #region Play/stop
        /// <summary>Playing melody?</summary>
        bool playingMelody = false;
        WaveOut woPlayMelody;
        private void btnPlayMelody_Click(object sender, EventArgs e)
        {
            if (ssMelody.Notes[0].Count <= 0) return;
            if (sv != null) sv.ApplyPhonemes(ssMelody.Notes[0]);
            if (!playingMelody)
            {
                txtNotes.Focus();

                playingMelody = true;
                txtNotes.Text = GetMelodyString();

                //reset melody position
                ssMelody.sampleSynthesizer.Reset();
                woPlayMelody.Play();

                btnPlayMelody.Image = picStop.Image;
            }
            else
            {
                woPlayMelody.Stop();
                frmPractice.ResetToBlack(melodyViewer);

                txtNotes.Focus();
                try
                {
                    txtNotes.SelectionStart = posNote0;
                    txtNotes.SelectionLength = posNoteF - posNote0;
                }
                catch { }
            }
        }

        void woPlayMelody_PlaybackStopped(object sender, StoppedEventArgs e)
        {
            playingMelody = false;
            btnPlayMelody.Image = picPlay.Image;
            frmPractice.ResetToBlack(melodyViewer);
        }
        private void numTempo_ValueChanged(object sender, EventArgs e)
        {
            ssMelody.Tempo = (double)numTempo.Value;
        }
        #endregion

        /// <summary>Note position in textbox, to select</summary>
        int posNote0, posNoteF;
        private void timer_Tick(object sender, EventArgs e)
        {
            frmPractice.ColorNotes(ssMelody, melodyViewer);
            if (playingMelody && ssMelody != null && melodyViewer != null && ssMelody.sampleSynthesizer != null && ssMelody.sampleSynthesizer.curNote.Count > 0)
            {
                int curNote = ssMelody.sampleSynthesizer.curNote[0];
                try
                {
                    int nNewLines = 0;
                    int posNewLine = 0;
                    int posPrevNewLine = 0;
                    while (nNewLines <= curNote)
                    {
                        posPrevNewLine = posNewLine;
                        posNewLine = txtNotes.Text.IndexOf("\r\n", posNewLine + 2);
                        if (posNewLine < 0) posNewLine = txtNotes.Text.Length;
                        nNewLines++;
                    }
                    posNote0 = posPrevNewLine;
                    posNoteF = posNewLine;
                        txtNotes.SelectionStart = posPrevNewLine;
                        txtNotes.SelectionLength = posNewLine - posPrevNewLine;
                }
                catch { }
            }
        }


        #region Virtual piano

        /// <summary>Piano Font</summary>
        private Font pianoFont = new Font("Arial", 8, FontStyle.Bold);

        /// <summary>Retrieves clicked note</summary>
        /// <param name="x">Mouse x coordinate</param>
        /// <param name="y">Mouse y coordinate</param>
        /// <param name="W">Image width</param>
        /// <param name="H">Image height</param>
        /// <returns></returns>
        private string GetClickedNote(int x, int y, int W, int H, int initialOctave)
        {
            //draw 3 octaves?
            int keyW = (int)(H * 0.185);
            //# and bemol
            int alterH = (int)(H * 0.573);
            int alterW = (int)(H * 0.127);

            int keyPos = x / keyW;

            //where is click?
            string[] noteNames = new string[] { "C", "D", "E", "F", "G", "A", "B" };
            int insideNoteX = x - keyW * keyPos;
            string selNote;

            if (y < alterH && insideNoteX < (alterW >> 1)) selNote = noteNames[(keyPos % 7) - 1] + "#" + (initialOctave + keyPos / 7).ToString();
            else if (y < alterH && insideNoteX > keyW - (alterW >> 1)) selNote = noteNames[keyPos % 7] + "#" + (initialOctave + keyPos / 7).ToString();
            else selNote = noteNames[keyPos % 7] + (initialOctave + keyPos / 7).ToString();

            return selNote;
        }

        /// <summary>Piano drawing</summary>
        /// <param name="g">Graphics element</param>
        /// <param name="W">Width of image</param>
        /// <param name="H">Height of image</param>
        /// <param name="initialOctave">Initial octave</param>
        private void DrawPiano(Graphics g, int W, int H, int initialOctave)
        {
            //draw 3 octaves?
            int keyW = (int)(H * 0.185);
            //# and bemol
            int alterH = (int)(H * 0.573);
            int alterW = (int)(H * 0.127);

            //start filling all white
            g.FillRectangle(Brushes.White, 0, 0, W, H);

            //draw keys
            Pen blackPen = new Pen(Color.Black, 2);

            string[] noteNames = new string[] { "C", "D", "E", "F", "G", "A", "B" };

            //number of keys
            int nKeys = (int)Math.Ceiling((double)W / (double)(keyW));
            for (int k = 0; k < nKeys; k++)
            {
                string nName = noteNames[k % 7] + (initialOctave + (k / 7)).ToString();
                if (nName == virtualPianoNote) g.FillRectangle(Brushes.LightBlue, k * keyW, 0, keyW, H);
                g.DrawRectangle(blackPen, k * keyW, 0, keyW, H);


                if (k % 7 != 0 && k % 7 != 3)
                {
                    string nNameMod = noteNames[(k-1) % 7] + "#" + (initialOctave + ((k-1) / 7)).ToString();
                    Brush b;
                    if (nNameMod == virtualPianoNote) 
                        b = Brushes.LightBlue;
                    else b = Brushes.Black;
                    
                    g.FillRectangle(b, k * keyW - (alterW >> 1), 0, alterW, alterH);
                }
            }

            //note names
            for (int k = 0; k < nKeys; k++)
            {
                string nName = noteNames[k % 7] + (initialOctave + (k / 7)).ToString();
                SizeF s = g.MeasureString(nName, pianoFont);
                g.DrawString(nName, pianoFont, Brushes.Black, (k + 1) * keyW - (keyW >> 1) - (s.Width * 0.5f), H - s.Height * 2);
            }
        }
        private void picPiano_Paint(object sender, PaintEventArgs e)
        {
            DrawPiano(e.Graphics, picPiano.Width, picPiano.Height, cmbKeyboardOctave.SelectedIndex);
        }

        #region Insertion of notes with keyboard
        System.Diagnostics.Stopwatch swKeys = new System.Diagnostics.Stopwatch();
        bool keyDown = false;
        string virtualPianoNote = "";
        private void frmEditMelody_KeyDown(object sender, KeyEventArgs e)
        {
            if (!txtKeyboardText.Focused) return;
            if (keyDown) return;
            
            keyDown = true;
            if (!txtAddNote.Focused && !txtNotes.Focused && !numTempo.Focused)
            {
                string note;

                #region Keyboard to notes
                switch (e.KeyCode.ToString())
                {
                    case "A":
                        note = "C";
                        break;
                    case "S":
                        note = "D";
                        break;
                    case "W":
                        note = "C#";
                        break;
                    case "E":
                        note = "D#";
                        break;
                    case "D":
                        note = "E";
                        break;
                    case "F":
                        note = "F";
                        break;
                    case "G":
                        note = "G";
                        break;
                    case "H":
                        note = "A";
                        break;
                    case "T":
                        note = "F#";
                        break;
                    case "Y":
                        note = "G#";
                        break;
                    case "U":
                        note = "A#";
                        break;
                    case "J":
                        note = "B";
                        break;
                    default:
                        return;
                }
                #endregion

                virtualPianoNote = note + (cmbKeyboardOctave.SelectedIndex + 1).ToString();
                BriefPlayPianoNote(virtualPianoNote);

                swKeys.Restart();
                picPiano.Invalidate();
            }
        }

        private void BriefPlayPianoNote(string noteToPlay)
        {
            if (noteToPlay == "") return;
            string note = noteToPlay + " " + (1.0 / 8.0).ToString();
            if (ssBriefPlay.Notes.Count == 0)
            {
                ssBriefPlay.Notes.Add(new List<SoundSynthesis.Note>());
                woPlayBrief = new WaveOut();
            }

            ssBriefPlay.sampleSynthesizer.Reset();
            ssBriefPlay.Notes[0].Clear();
            SoundSynthesis.Note ssNote = SoundSynthesis.Note.FromString(note, noteWaveShape);

            ssBriefPlay.Notes[0].Add(new SoundSynthesis.Note(0.15, ssNote.Frequency[0], noteWaveShape));
            woPlayBrief.Init(ssBriefPlay.sampleSynthesizer);
            woPlayBrief.Play();
        }

        private void frmEditMelody_KeyUp(object sender, KeyEventArgs e)
        {
            keyDown = false;
            if (!txtAddNote.Focused && !txtNotes.Focused && !numTempo.Focused)
                AddVirtualPianoNote();
        }
        #endregion

        #region Insertion of notes with mouse
        private void picPiano_MouseDown(object sender, MouseEventArgs e)
        {
            virtualPianoNote = GetClickedNote(e.X, e.Y, picPiano.Width, picPiano.Height, cmbKeyboardOctave.SelectedIndex);
            BriefPlayPianoNote(virtualPianoNote);
            //mouseClicked = true;
            swKeys.Restart();
            picPiano.Invalidate();
            txtKeyboardText.Focus();
        }
        private void picPiano_MouseUp(object sender, MouseEventArgs e)
        {
            //mouseClicked = false;
            AddVirtualPianoNote();
        }

        private void AddVirtualPianoNote()
        {
            if (virtualPianoNote == "") return;
            swKeys.Stop();

            double duration = swKeys.Elapsed.TotalSeconds / (double)numTempo.Value;
            duration = Math.Round(duration * 4) * 0.25;
            if (duration == 0) duration = 0.25;

            string note = virtualPianoNote + " " + duration.ToString();
            txtAddNote.Text = note;

            virtualPianoNote = "";

            btnAddNote_Click(this, new EventArgs());
            picPiano.Invalidate();

            txtNotes.Text = GetMelodyString();
        }

        #endregion
        private void picPiano_Resize(object sender, EventArgs e)
        {
            picPiano.Invalidate();
        }
        private void cmbKeyboardOctave_SelectedIndexChanged(object sender, EventArgs e)
        {
            picPiano.Invalidate();
        }
        private void txtKeyboardText_TextChanged(object sender, EventArgs e)
        {
            if (txtKeyboardText.Text.Length > 1) txtKeyboardText.Text = txtKeyboardText.Text.Substring(0, 1);
        }
        #endregion

        private void btnEditWaveShapes_Click(object sender, EventArgs e)
        {
            (new frmEditWaveForm()).ShowDialog();
        }

        #region Virtual voice
        SyntheticVoice sv;
        private void btnLoadVoice_Click(object sender, EventArgs e)
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
            DoCosmetics(sender, e.Graphics);
        }

        private void DoCosmetics(object sender, Graphics g)
        {
            PratiCanto.LayoutFuncs.DoGroupBoxLayout((GroupBox)sender, g);
        }

        private void groupBox2_Paint(object sender, PaintEventArgs e)
        {
            DoCosmetics(sender, e.Graphics);
        }
        private void gbNotes_Paint(object sender, PaintEventArgs e)
        {
            DoCosmetics(sender, e.Graphics);
        }

        #endregion

        private void txtNotes_Leave(object sender, EventArgs e)
        {
            if (!playingMelody) btnUpdate_Click(sender, e);
        }





    }
}
