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
using NAudio.Midi;

namespace PratiCanto.EditMelody
{
    public partial class frmImportMidi : Form
    {
        public frmImportMidi(string filename, SoundSynthesis ssMelody)
        {
            InitializeComponent();

            this.ssMelody = ssMelody;
            DescribeMIDI(filename);
        }

        /// <summary>Stores melody</summary>
        SoundSynthesis ssMelody;

        /// <summary>Stores melody single staff</summary>
        SoundSynthesis ssMelodyOneStaff = new SoundSynthesis();

        /// <summary>Play melody</summary>
        WaveOut woPlayMelody;
        /// <summary>Play melody</summary>
        WaveOut woPlayMelodySingleStaff;

        /// <summary>Wave shape</summary>
        SoundSynthesis.WaveShapePiano noteWaveShape = new SoundSynthesis.WaveShapePiano();

        /// <summary>Replaying melody?</summary>
        bool playingMelody = false;

        private void frmImportMidi_Load(object sender, EventArgs e)
        {
            //cosmetics
            AdjButtons();

            numTempo.Value = (decimal)ssMelody.Tempo;

            ssMelodyOneStaff.Notes.Add(new List<SoundSynthesis.Note>());

            woPlayMelody = new WaveOut();
            woPlayMelody.Init(ssMelody.sampleSynthesizer);

            woPlayMelodySingleStaff = new WaveOut();
            woPlayMelodySingleStaff.Init(ssMelodyOneStaff.sampleSynthesizer);

            woPlayMelody.PlaybackStopped += new EventHandler<StoppedEventArgs>(woPlayMelody_PlaybackStopped);
            woPlayMelodySingleStaff.PlaybackStopped += new EventHandler<StoppedEventArgs>(woPlayMelody_PlaybackStopped);

            //populate information
            string[] infoMidi = mf.ToString().Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            string sBuild = "";
            foreach (string s in infoMidi)
            {
                if (!s.Contains("NoteOn") && !s.Contains("NoteOff") && !s.Contains("SetTempo")) 
                    sBuild += s + "\r\n";
            }
            txtInfoMidi.Text = sBuild;

            for (int k = 0; k < ssMelody.Notes.Count; k++) cmbTrack.Items.Add(k);
            if (cmbTrack.Items.Count > 0) cmbTrack.SelectedIndex = 0;
        }


        void woPlayMelody_PlaybackStopped(object sender, StoppedEventArgs e)
        {
            playingMelody = false;
            //btnPlayMelody.Image = picPlay.Image;
            //frmPractice.ResetToBlack(melodyViewer);
        }


        #region MIDI Files
        /// <summary>Stores midi file</summary>
        MidiFile mf;
        public string DescribeMIDI(string fileName)
        {
            mf = new MidiFile(fileName, false);

            ssMelody.Notes.Clear();

            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("Format {0}, Tracks {1}, Delta Ticks Per Quarter Note {2}\r\n",
                mf.FileFormat, mf.Tracks, mf.DeltaTicksPerQuarterNote);
            var timeSignature = mf.Events[0].OfType<TimeSignatureEvent>().FirstOrDefault();
            for (int n = 0; n < mf.Tracks; n++)
            {
                ssMelody.Notes.Add(new List<SoundSynthesis.Note>());
                double curStartTempo = -1;
                double curEndTempo = 0;

                foreach (MidiEvent midiEvent in mf.Events[n])
                {
                    if (!MidiEvent.IsNoteOff(midiEvent))
                    {
                        if (midiEvent.CommandCode != MidiCommandCode.NoteOn && midiEvent.CommandCode != MidiCommandCode.NoteOff)
                        {
                            string sAppend = ToMBT(midiEvent.AbsoluteTime, mf.DeltaTicksPerQuarterNote, timeSignature).ToString() + " " + midiEvent.ToString() + "\r\n";
                            if (sAppend.IndexOf("NoteOn") < 0) sb.AppendFormat(sAppend);
                        }

                        if (midiEvent.CommandCode == MidiCommandCode.NoteOn)
                        {
                            //1:3:0 960 NoteOn Ch: 1 G5 Vel:70 Len: 1440

                            NoteOnEvent nono = (NoteOnEvent)midiEvent;

                            //50 -> d4
                            //double freq = SoundSynthesis.Note.GetFrequency(nono.NoteNumber - 57);
                            double freq = SoundSynthesis.Note.GetFrequency(nono.NoteNumber - 69);
                            double dur = (double)nono.NoteLength / (double)mf.DeltaTicksPerQuarterNote;
                            double startTempo = (double)nono.AbsoluteTime / (double)mf.DeltaTicksPerQuarterNote;

                            if (curStartTempo == startTempo)
                            {
                                ssMelody.Notes[n][ssMelody.Notes[n].Count - 1].Frequency.Add(freq);
                            }
                            else
                            {
                                if (startTempo - curEndTempo > 0)
                                {
                                    SoundSynthesis.Note pause = new SoundSynthesis.Note(startTempo - curEndTempo, 0, this.noteWaveShape);
                                    ssMelody.Notes[n].Add(pause);
                                }

                                if (startTempo - curEndTempo >= 0)
                                {
                                    SoundSynthesis.Note nn = new SoundSynthesis.Note(dur, freq, this.noteWaveShape);
                                    ssMelody.Notes[n].Add(nn);

                                    curStartTempo = startTempo;
                                    curEndTempo = startTempo + dur;
                                }
                                else
                                {
                                }

                            }
                        }
                    }
                }
            }
            AdjustSSMelodyZero();

            return sb.ToString();
        }

        /// <summary>Prevents initial melody note from being a pause, for modulation reasons</summary>
        private void AdjustSSMelodyZero()
        {
            while (ssMelody.Notes[0].Count == 0 && ssMelody.Notes.Count > 1) ssMelody.Notes.RemoveAt(0);
            if (ssMelody.Notes[0][0].Frequency[0] == 0)
            {
                double freq = 0;
                foreach (SoundSynthesis.Note nn in ssMelody.Notes[0])
                {
                    freq = nn.Frequency[0];
                    if (freq != 0) break;
                }

                ssMelody.Notes[0][0].Duration -= 1e-3f;
                float vol0 = ssMelody.Notes[0][0].Volume;
                ssMelody.Notes[0].Insert(0, new SoundSynthesis.Note(1e-3f, freq, this.noteWaveShape));
                ssMelody.Notes[0][0].Volume = vol0;
            }
        }

        private string ToMBT(long eventTime, int ticksPerQuarterNote, TimeSignatureEvent timeSignature)
        {
            int beatsPerBar = timeSignature == null ? 4 : timeSignature.Numerator;
            int ticksPerBar = timeSignature == null ? ticksPerQuarterNote * 4 : (timeSignature.Numerator * ticksPerQuarterNote * 4) / (1 << timeSignature.Denominator);
            int ticksPerBeat = ticksPerBar / beatsPerBar;
            long bar = 1 + (eventTime / ticksPerBar);
            long beat = 1 + ((eventTime % ticksPerBar) / ticksPerBeat);
            long tick = eventTime % ticksPerBeat;
            return String.Format("{0}:{1}:{2}", bar, beat, tick);
        }

        /// <summary>
        /// Find the number of beats per measure
        /// (for now assume just one TimeSignature per MIDI track)
        /// </summary>
        private int FindBeatsPerMeasure(IEnumerable<MidiEvent> midiEvents)
        {
            int beatsPerMeasure = 4;
            foreach (MidiEvent midiEvent in midiEvents)
            {
                TimeSignatureEvent tse = midiEvent as TimeSignatureEvent;
                if (tse != null)
                {
                    beatsPerMeasure = tse.Numerator;
                }
            }
            return beatsPerMeasure;
        }
        #endregion

        #region Replay
        private void btnStopReplay_Click(object sender, EventArgs e)
        {
            if (woPlayMelody.PlaybackState == PlaybackState.Playing) woPlayMelody.Stop();
            if (woPlayMelodySingleStaff.PlaybackState == PlaybackState.Playing) woPlayMelodySingleStaff.Stop();
        }
        private void btnReplayAll_Click(object sender, EventArgs e)
        {
            if (playingMelody) return;

            ssMelody.sampleSynthesizer.Reset();
            woPlayMelody.Play();
            playingMelody = true;
        }
        
        private void btnReplayMelody_Click(object sender, EventArgs e)
        {
            if (playingMelody) return;

            ssMelodyOneStaff.sampleSynthesizer.Reset();
            woPlayMelodySingleStaff.Play();
            playingMelody = true;
        }
        private void frmImportMidi_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (playingMelody) btnStopReplay_Click(sender, e);

            //set desired melody track
            List<SoundSynthesis.Note> melTrack = ssMelody.Notes[cmbTrack.SelectedIndex];
            ssMelody.Notes.RemoveAt(cmbTrack.SelectedIndex);
            ssMelody.Notes.Insert(0, melTrack);

            AdjustSSMelodyZero();
        }

        private void cmbTrack_SelectedIndexChanged(object sender, EventArgs e)
        {
            ssMelodyOneStaff.Notes[0].Clear();
            foreach (SoundSynthesis.Note n in ssMelody.Notes[cmbTrack.SelectedIndex]) ssMelodyOneStaff.Notes[0].Add(n);
            if (ssMelody.Notes[cmbTrack.SelectedIndex].Count > 0) numVolume.Value = (decimal)(100*ssMelody.Notes[cmbTrack.SelectedIndex][0].Volume);
        }

        private void numVolume_ValueChanged(object sender, EventArgs e)
        {
            float vol = 0.01f*(float)numVolume.Value;
            foreach (SoundSynthesis.Note n in ssMelody.Notes[cmbTrack.SelectedIndex])
            {
                n.Volume = vol;
            }
        }

        private void numTempo_ValueChanged(object sender, EventArgs e)
        {
            ssMelodyOneStaff.Tempo = (double)numTempo.Value;
            ssMelody.Tempo = (double)numTempo.Value;
        }
        #endregion

        #region Cosmetics
        private void groupBox1_Paint(object sender, PaintEventArgs e)
        {
            PratiCanto.LayoutFuncs.DoGroupBoxLayout((GroupBox)sender, e.Graphics);
        }
        private void groupBox2_Paint(object sender, PaintEventArgs e)
        {
            PratiCanto.LayoutFuncs.DoGroupBoxLayout((GroupBox)sender, e.Graphics);

        }
        private void AdjButtons()
        {
            btnReplayAll.Image = PratiCanto.LayoutFuncs.MakeDegrade(Color.White, Color.FromArgb(223, 223, 223), btnReplayAll.Width, btnReplayAll.Height, true);
            btnReplayMelody.Image = PratiCanto.LayoutFuncs.MakeDegrade(Color.White, Color.FromArgb(223, 223, 223), btnReplayMelody.Width, btnReplayMelody.Height, true);
            btnStopReplay.Image = PratiCanto.LayoutFuncs.MakeDegrade(Color.White, Color.FromArgb(223, 223, 223), btnStopReplay.Width, btnStopReplay.Height, true);

            btnOk.Image = PratiCanto.LayoutFuncs.MakeDegrade(Color.FromArgb(218, 30, 30), Color.FromArgb(113, 40, 40), btnOk.Width, btnOk.Height, false);
        }
        #endregion



        private void numAllVols_ValueChanged(object sender, EventArgs e)
        {
            foreach (List<SoundSynthesis.Note> melodyLine in ssMelody.Notes)
            {
                float vol = 0.01f * (float)numAllVols.Value;
                foreach (SoundSynthesis.Note n in melodyLine)
                {
                    n.Volume = vol;
                }
            }

        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            this.Close();
        }










    }
}
