using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NAudio.Wave;
using System.Drawing;
using System.IO;

namespace ClassifTreinoVoz
{
    /// <summary>Sound synthesis</summary>
    public class SoundSynthesis
    {
        /// <summary>Constructor</summary>
        public SoundSynthesis()
        {
            sampleSynthesizer = new SynthSample(this);
        }
        /// <summary>Synthesizer for this audio</summary>
        public SynthSample sampleSynthesizer;

        /// <summary>Base metronome tempo in seconds</summary>
        public double Tempo = 1;

        /// <summary>Tracks of musical notes in this synthesis</summary>
        public List<List<Note>> Notes = new List<List<Note>>();

        /// <summary>Musical notes</summary>
        public class Note
        {
            /// <summary>Creates a note from a string - noteModifier duration. Pause is P. Eg: A4 1, B#4 0.25; P 0.5</summary>
            /// <param name="note">String to create note from</param>
            /// <param name="ws">Desired wave shape</param>
            public static Note FromString(string note, WaveShape ws)
            {
                string sepDec = (1.1).ToString().Substring(1, 1);

                string[] noteParts = note.Split();
                noteParts[0] = noteParts[0].ToUpper();

                double duration = double.Parse(noteParts[1].Replace(".", sepDec).Replace(",", sepDec));
                double f=0;
                double halfTonesAboveA4 = 0;
                if (noteParts[0] != "P")
                {
                    int octave = int.Parse(noteParts[0].Substring(noteParts[0].Length - 1, 1));
                    char name = noteParts[0].Substring(0, 1).ToCharArray()[0];
                    string modifier = noteParts[0].Substring(1, 1);

                    double LocalHalfTones = 0;
                    switch (name)
                    {
                        case 'A':
                            LocalHalfTones = 0;
                            break;
                        case 'B':
                            LocalHalfTones = 2;
                            break;
                        case 'C':
                            LocalHalfTones = 3; octave--;
                            break;
                        case 'D':
                            LocalHalfTones = 5; octave--;
                            break;
                        case 'E':
                            LocalHalfTones = 7; octave--;
                            break;
                        case 'F':
                            LocalHalfTones = 8; octave--;
                            break;
                        case 'G':
                            LocalHalfTones = 10; octave--;
                            break;
                        default:
                            throw new Exception("Symbol not found");
                    }

                    halfTonesAboveA4 = 12 * (octave - 4) + LocalHalfTones;
                    if (modifier == "#") halfTonesAboveA4++;
                    if (modifier == "B") halfTonesAboveA4--;

                    f = GetFrequency(halfTonesAboveA4);
                }
                Note newNote = new Note(duration, f, ws);
                if (noteParts.Length >= 3)
                {
                    if (noteParts[2].ToLower() == "major")
                    {
                        newNote.Frequency.Add(GetFrequency(halfTonesAboveA4 + 4)); 
                        newNote.Frequency.Add(GetFrequency(halfTonesAboveA4 + 7));
                    }
                    else if (noteParts[2].ToLower() == "minor")
                    {
                        newNote.Frequency.Add(GetFrequency(halfTonesAboveA4 + 3));
                        newNote.Frequency.Add(GetFrequency(halfTonesAboveA4 + 7));
                    }
                }

                //string TEMP = newNote.ToString();
                newNote.buildString = note;
                return newNote;
            }

            /// <summary>New musical note</summary>
            /// <param name="duration">Duration in tempos. 2=minim, 4=semibreve, 1=crotchet, 0.5=quaver, 0.25 = semiquaver</param>
            /// <param name="frequency">Frequency</param>
            /// <param name="ws">Wave shape</param>
            public Note(double duration, double frequency, WaveShape ws)
            {
                this.Duration = duration;
                this.Frequency.Add(frequency);
                this.waveShape = ws;

                this.Period = 1.0 / frequency;
            }

            /// <summary>Note has been modulated in frequency change?</summary>
            internal bool modulated = false;

            /// <summary>String used to construct this note when using FromString</summary>
            public string buildString = "";

            /// <summary>Note duration in tempos. 2=minim, 4=semibreve, 1=crotchet, 0.5=quaver, 0.25 = semiquaver</summary>
            public double Duration;

            /// <summary>Note frequencies in Hz</summary>
            public List<double> Frequency = new List<double>();

            /// <summary>Period. inverse of frequency</summary>
            private double Period;

            /// <summary>Shape of this wave - associated with timber</summary>
            public WaveShape waveShape;

            /// <summary>Note volume, to allow song dynamics</summary>
            public float Volume = 1;

            /// <summary>Retrieves note intensity at time T (for playback)</summary>
            /// <param name="time">Desired time</param>
            /// <param name="phase">Accumulated phase</param>
            public double GetNoteIntensity(double time, double phase)
            {
                double sumIntens = 0;
                double attenFunc = waveShape.GetWaveAmplitude(time, Duration);
                for (int kk = 0; kk < Frequency.Count; kk++)
                {
                    double normTime = time * Frequency[kk] - (int)(time * Frequency[kk]);

                    //time between 0 and 1
                    double time01 = normTime + phase - (int)(normTime + phase);

                    //Compute new waveshape if this is waveshapespeech
                    if (waveShape is SyntheticVoice.WaveShapeSpeech)
                    {
                        SyntheticVoice.WaveShapeSpeech wsp = (SyntheticVoice.WaveShapeSpeech)waveShape;
                        wsp.PlayFrequency = (float)Frequency[kk];
                    }

                    sumIntens += waveShape.GetWaveIntensity(normTime + phase) * attenFunc;
                }
                if (Frequency.Count == 0) return 0;
                return sumIntens * Volume;// (float)Frequency.Count;
            }



            /// <summary>String representation</summary>
            /// <returns></returns>
            public override string ToString()
            {
                return GetNoteName(Frequency[0]) + " " + Math.Round(Duration,2).ToString();
            }

            #region Static functions

            /// <summary>Base of tempered scale, twelfth root of two</summary>
            private static double TemperedBase = Math.Pow(2, 1.0 / 12.0);

            /// <summary>Retrieves frequency of notes</summary>
            /// <param name="n">Number of half steps above A4. Negative if lower</param>
            /// <returns></returns>
            public static double GetFrequency(double n)
            {
                return 440 * Math.Pow(TemperedBase, n);
            }

            /// <summary>Computes number of half steps above/below a4</summary>
            /// <param name="f">Frequency to compute number of half steps</param>
            /// <returns></returns>
            public static double GetHalfStepsAboveA4(double f)
            {
                return Math.Log(f / 440, TemperedBase);
            }

            /// <summary>Retrieve note name from its frequency</summary>
            /// <param name="f">Frequency</param>
            public static string GetNoteName(double f)
            {
                if (f == 0) return "P";

                int n = (int)Math.Round(GetHalfStepsAboveA4(f));

                //steps above C5
                n = n - 3;

                //quotient/remainder of division by 12
                int k = n / 12;
                int r = n - k * 12;
                if (r < 0)
                {
                    k--;
                    r += 12;
                }

                //example - A4 -> k=0, r=0
                //A3 -> k=-1, r=0
                //B4 -> k=0, r=2
                string ans = "";

                switch (r)
                {
                    case 9: ans += "A"; break;
                    case 10: ans += "A#"; break;
                    case 11: ans += "B"; break;
                    case 0: ans += "C"; break;
                    case 1: ans += "C#"; break;
                    case 2: ans += "D"; break;
                    case 3: ans += "D#"; break;
                    case 4: ans += "E"; break;
                    case 5: ans += "F"; break;
                    case 6: ans += "F#"; break;
                    case 7: ans += "G"; break;
                    case 8: ans += "G#"; break;
                }

                ans += (5 + k).ToString();
                if (ans.Length == 1)
                {
                }
                return ans;
            }
            #endregion
        }

        /// <summary>Retrieves track play time in seconds</summary>
        /// <param name="track">Track index, Notes[track[</param>
        public double GetTrackPlayTime(int track)
        {
            double sum = 0;
            foreach (Note n in Notes[track]) sum += n.Duration;

            return sum * this.Tempo;
        }

        #region Graphic generation

        /// <summary>Draws track</summary>
        /// <param name="g">Graphic element to draw to</param>
        /// <param name="trackId">Track id: Notes[trackId]</param>
        /// <param name="offSetX">Offset in X (pixels)</param>
        /// <param name="offSetY">Offset in Y (pixels)</param>
        /// <param name="W">Image width</param>
        /// <param name="H">Image height</param>
        /// <param name="maxF">Frequency at the top of image</param>
        public void DrawTrack(Graphics g, int trackId, float offSetX, float offSetY, int W, int H, float maxF)
        {
            DrawTrack(g, trackId, offSetX, offSetY, W, H, maxF, 1, 1);
        }

        /// <summary>Draws track. Returns time-frequency graph</summary>
        /// <param name="g">Graphic element to draw to</param>
        /// <param name="trackId">Track id: Notes[trackId]</param>
        /// <param name="offSetX">Offset in X (pixels)</param>
        /// <param name="offSetY">Offset in Y (pixels)</param>
        /// <param name="W">Image width</param>
        /// <param name="H">Image height</param>
        /// <param name="maxF">Frequency at the top of image</param>
        /// <param name="scaleXPix">X pixel scale for zoom</param>
        /// <param name="scaleYPix">Y pixel scale for zoom</param>
        public void DrawTrack(Graphics g, int trackId, float offSetX, float offSetY, int W, int H, float maxF, float scaleXPix, float scaleYPix)
        {
            //nothing to draw
            if (Notes[trackId].Count == 0) return;

            List<PointF> trackPts = new List<PointF>();

            //float minF = (float)Notes[trackId][0].Frequency;
            //float maxF = (float)Notes[trackId][0].Frequency;

            float totalTime = 0;
            for (int k = 0; k < Notes[trackId].Count; k++)
            {
                PointF pt = new PointF(totalTime, (float)Notes[trackId][k].Frequency[0]);
                trackPts.Add(pt);
                totalTime += (float)Notes[trackId][k].Duration;

                pt = new PointF(totalTime, (float)Notes[trackId][k].Frequency[0]);
                trackPts.Add(pt);
                if (Notes[trackId][k].Frequency[0] > 0)
                {
                    //minF = Math.Min(minF, (float)Notes[trackId][k].Frequency);
                    //maxF = Math.Max(maxF, (float)Notes[trackId][k].Frequency);
                }
            }

            float scaleX = W / totalTime;
            float scaleY = H / (maxF - 0); //minF);
            for (int k = 0; k < trackPts.Count; k++)
            {
                trackPts[k] = new PointF((trackPts[k].X * scaleX + offSetX) * scaleXPix, H + (offSetY - trackPts[k].Y * scaleY) * scaleYPix);
            }

            Pen p = new Pen(Color.DarkGray, 7);
            g.DrawLines(p, trackPts.ToArray());

            Font f = new Font("Arial", 10, FontStyle.Bold);
            g.DrawString(Notes[trackId][0].ToString().Split()[0], f, Brushes.Black, trackPts[0].X, trackPts[0].Y - 17);
        }

        /// <summary>Retrieves frequency to play at time from Notes[trackId]</summary>
        /// <param name="time">Time at which to recover frequency</param>
        /// <param name="trackId">Track to analyze</param>
        public double GetPlayFrequency(double time, int trackId)
        {
            double totalTime = 0;
            int idx = 0;
            while (idx < Notes[trackId].Count && totalTime + Notes[trackId][idx].Duration * Tempo < time)
            {
                totalTime += Notes[trackId][idx].Duration * Tempo;
                idx++;
            }
            if (idx >= Notes[trackId].Count) return 0;
            else return Notes[trackId][idx].Frequency[0];
        }

        #endregion

        #region Wave shapes
        /// <summary>Shape of the wave to be played</summary>
        public abstract class WaveShape
        {
            /// <summary>Retrieves intensity of wave at a given normalized time from 0 to 1</summary>
            /// <param name="time">Time [0,1) at which to retrieve wave intensity</param>
            public abstract double GetWaveIntensity(double time);

            /// <summary>Retrieves amplitude multiplier of wave during its reproduction. 1 for constant intensity, 1->less to reduce </summary>
            /// <param name="time">Time of note, from 0 to Duration (note duration)</param>
            /// <param name="duration">Note duration in seconds</param>
            public abstract double GetWaveAmplitude(double time, double duration);
        }

        /// <summary>Produces uniform white noise</summary>
        public class WaveShapeWhiteNoise : WaveShape
        {
            Random rnd = new Random();

            /// <summary>Amplitude of white noise. Always 1</summary>
            public override double GetWaveAmplitude(double time, double duration)
            {
                return 1;
            }

            /// <summary>White noise wave intensity. Random</summary>
            public override double GetWaveIntensity(double time)
            {
                return (rnd.NextDouble() - 0.5) * 0.5;
            }
        }

        /// <summary>Produces uniform pink noise</summary>
        public class WaveShapePinkNoise : WaveShape
        {
            Random rnd = new Random();

            /// <summary>Amplitude of white noise. Always 1</summary>
            public override double GetWaveAmplitude(double time, double duration)
            {
                return 1;
            }

            /// <summary>Pink noise filter terms</summary>
            double b0, b1, b2, b3, b4, b5, b6;

            /// <summary>White noise wave intensity. Random</summary>
            public override double GetWaveIntensity(double time)
            {
                double white = (rnd.NextDouble() - 0.5) * 0.5;
                b0 = 0.99886 * b0 + white * 0.0555179;
                b1 = 0.99332 * b1 + white * 0.0750759;
                b2 = 0.96900 * b2 + white * 0.1538520;
                b3 = 0.86650 * b3 + white * 0.3104856;
                b4 = 0.55000 * b4 + white * 0.5329522;
                b5 = -0.7616 * b5 - white * 0.0168980;
                b6 = white * 0.115926;
                return b0 + b1 + b2 + b3 + b4 + b5 + b6 + white * 0.5362;
            }
        }

        /// <summary>Square wave shape</summary>
        public class WaveShapeSquare : WaveShape
        {
            /// <summary>On/off ratio</summary>
            double onOffRat;

            /// <summary>Generates a square wave shape with given onoffratio</summary>
            /// <param name="OnOffRatio">Ratio of on/off times. For example, half square -> on-off ratio = 0.5</param>
            public WaveShapeSquare(double OnOffRatio)
            {
                onOffRat = OnOffRatio;
            }
            public override double GetWaveIntensity(double time)
            {
                time = time - (int)time;
                return (time < onOffRat) ? 0.5 : -0.5;
            }
            public override double GetWaveAmplitude(double time, double duration)
            {
                return 0.5;
            }
        }

        /// <summary>Square wave shape</summary>
        public class WaveShapeSine : WaveShape
        {
            public override double GetWaveIntensity(double time)
            {
                return Math.Sin(2 * Math.PI * time);
            }
            public override double GetWaveAmplitude(double time, double duration)
            {
                return 1;
            }
        }


        /// <summary>Piano wave shape</summary>
        public class WaveShapePiano : WaveShape
        {
            public override double GetWaveIntensity(double time)
            {
                return 0.8 * Math.Sin(2 * Math.PI * time) + 0.1 * Math.Sin(4 * Math.PI * time) + 0.05 * Math.Sin(6 * Math.PI * time);
            }
            public override double GetWaveAmplitude(double time, double duration)
            {
                double constMult = 3;
                return Math.Exp(-constMult * time);
            }
        }

        /// <summary>Custom wave shape, from file</summary>
        public class WaveShapeCustom : WaveShape
        {
            /// <summary>Is amplitude relative or absolute?</summary>
            public bool isAmplitudeRelative;
            /// <summary>Wave intensity data</summary>
            public List<float> waveIntens;
            /// <summary>Wave intensity times</summary>
            public List<float> waveIntensTime;
            /// <summary>Wave amplitude</summary>
            public List<float> waveAmp;
            /// <summary>Wave amplitude time</summary>
            public List<float> waveAmpTime;

            /// <summary>Creates custom wave shape</summary>
            /// <param name="isAmpRel">Is amplitude absolute or relative to note duration?</param>
            /// <param name="wIntens">Wave intensity graph</param>
            /// <param name="wIntensTime">Wave intensity times (ascending order)</param>
            /// <param name="wAmp">Wave amplitude</param>
            /// <param name="wAmpTime">Wave amplitude times (ascending order)</param>
            public WaveShapeCustom(bool isAmpRel, List<float> wIntens, List<float> wIntensTime, List<float> wAmp, List<float> wAmpTime)
            {
                if (wIntens.Count != wIntensTime.Count) throw new Exception("wIntens and wIntensTime must have same dimension");
                if (wAmp.Count != wAmpTime.Count) throw new Exception("wAmp and wAmpTime must have same dimension");
                this.isAmplitudeRelative = isAmpRel;
                this.waveIntens = wIntens;
                this.waveIntensTime = wIntensTime;
                this.waveAmp = wAmp;
                this.waveAmpTime = wAmpTime;
            }
            /// <summary>Custom wave intensity</summary>
            /// <param name="time">Local time 0-1</param>
            /// <returns></returns>
            public override double GetWaveIntensity(double time)
            {
                time = time - (int)time;
                return AudioComparer.LinearPredictor.LinInterp(this.waveIntensTime, this.waveIntens, (float)time);
            }
            /// <summary>Wave amplitude</summary>
            /// <param name="time">Time</param>
            /// <param name="duration">Note duration</param>
            /// <returns></returns>
            public override double GetWaveAmplitude(double time, double duration)
            {
                float localt = (float)time;
                if (isAmplitudeRelative) localt = (float)(time / duration);

                return AudioComparer.LinearPredictor.LinInterp(this.waveAmpTime, this.waveAmp, localt);
            }

            /// <summary>Save this custom wave shape to a file</summary>
            /// <param name="File">File to write</param>
            public void WriteToFile(string File)
            {
                using (StreamWriter sw = new StreamWriter(File))
                {
                    if (this.isAmplitudeRelative) sw.WriteLine("AmpRel");
                    else sw.WriteLine("AmpAbs");
                    sw.WriteLine(waveIntensTime.Count.ToString());
                    for (int k = 0; k < waveIntensTime.Count; k++)
                        sw.WriteLine(waveIntensTime[k].ToString() + " " + waveIntens[k].ToString());

                    sw.WriteLine(waveAmpTime.Count.ToString());
                    for (int k = 0; k < waveAmpTime.Count; k++)
                        sw.WriteLine(waveAmpTime[k].ToString() + " " + waveAmp[k].ToString());
                }
            }

            /// <summary>Read custom wave shape from file</summary>
            /// <param name="File">File to read</param>
            /// <returns></returns>
            public static WaveShapeCustom FromFile(string File)
            {
                bool isAmpRel = false;
                List<float> wIntens = new List<float>();
                List<float> wIntensTime = new List<float>();
                List<float> wAmp = new List<float>();
                List<float> wAmpTime = new List<float>();

                using (StreamReader sr = new StreamReader(File))
                {
                    string sepDec = (1.1).ToString().Substring(1, 1);
                    string s = sr.ReadLine();
                    if (s.ToLower() == "amprel") isAmpRel = true;

                    int nIntens = int.Parse(sr.ReadLine());
                    for (int k = 0; k < nIntens; k++) 
                    {
                        string[] ss = sr.ReadLine().Replace(".", sepDec).Replace(",", sepDec).Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                        wIntensTime.Add(float.Parse(ss[0]));
                        wIntens.Add(float.Parse(ss[1]));
                    }

                    int nAmp = int.Parse(sr.ReadLine());
                    for (int k = 0; k < nAmp; k++)
                    {
                        string[] ss = sr.ReadLine().Replace(".", sepDec).Replace(",", sepDec).Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                        wAmpTime.Add(float.Parse(ss[0]));
                        wAmp.Add(float.Parse(ss[1]));
                    }
                }

                return new WaveShapeCustom(isAmpRel, wIntens, wIntensTime, wAmp, wAmpTime);
            }
        }



        #endregion
        
        #region Synthesize audio

        /// <summary>Synthesize wave</summary>
        public class SynthSample : WaveProvider32
        {
            /// <summary>current sample to fetch</summary>
            int sample;

            /// <summary>Current playtime</summary>
            double t;

            SoundSynthesis ss;

            /// <summary>Creates a new synthesize player</summary>
            public SynthSample(SoundSynthesis soundSynth)
            {
                ss = soundSynth;
            }

            /// <summary>Goes straight to end</summary>
            public void GoToEnd()
            {
                sample = int.MaxValue;
            }

            /// <summary>Current note being played</summary>
            public List<int> curNote = new List<int>();
            /// <summary>Duration of notes already played - set these two together</summary>
            public List<double> durPlayedNotes = new List<double>();

            /// <summary>Played all tracks?</summary>
            public bool allDone = false;

            //public List<float> tempPlayed = new List<float>();

            /// <summary>Tiny difference in time - adjust to avoid bumps in sound</summary>
            List<double> phase = new List<double>();

            /// <summary>Plays audio sample</summary>
            /// <param name="buffer">Source buffer</param>
            /// <param name="offset">Offset index</param>
            /// <param name="count">Number of samples to read</param>
            /// <returns></returns>
            public override int Read(float[] buffer, int offset, int count)
            {
                //populates counters
                if (phase.Count == 0)
                {
                    foreach (List<Note> lstN in ss.Notes)
                    {
                        phase.Add(0);
                        curNote.Add(0);
                        durPlayedNotes.Add(0);
                    }
                }

                int sampleRate = WaveFormat.SampleRate;

                List<float> b2 = new List<float>();

                for (int n = 0; n < count; n++)
                {
                    t = (double)sample / (double)sampleRate;

                    float intens = 0;
                    allDone = true;
                    for (int track = 0; track < ss.Notes.Count; track++)
                    {
                        while (curNote[track] < ss.Notes[track].Count && durPlayedNotes[track] + ss.Tempo * ss.Notes[track][curNote[track]].Duration < t)
                        {
                            durPlayedNotes[track] += ss.Tempo * ss.Notes[track][curNote[track]].Duration;
                            phase[track] += ss.Tempo * ss.Notes[track][curNote[track]].Duration * ss.Notes[track][curNote[track]].Frequency[0];
                            curNote[track]++;
                        }

                        if (curNote[track] < ss.Notes[track].Count)
                        {
                            allDone = false;
                            float curNoteIntens = (float)ss.Notes[track][curNote[track]].GetNoteIntensity(t - durPlayedNotes[track], phase[track]);

                            float overlapTime = SyntheticVoice.Config.VoiceOverlapTime;
                            if (ss.Notes[track][curNote[track]].waveShape is SyntheticVoice.WaveShapeSpeech &&
                                t - durPlayedNotes[track] < overlapTime && curNote[track] > 0
                                && ss.Notes[track][curNote[track] - 1].waveShape is SyntheticVoice.WaveShapeSpeech)
                            {
                                float prevNoteIntens = (float)ss.Notes[track][curNote[track] - 1].GetNoteIntensity(t - durPlayedNotes[track] - ss.Notes[track][curNote[track] - 1].Duration, phase[track]);
                                float w = (float)(t - durPlayedNotes[track]) / overlapTime;
                                intens += prevNoteIntens * (1 - w) + curNoteIntens * w;
                            }
                            else intens += curNoteIntens;//(float)(0.1 * Math.Sin((2 * Math.PI * 1000 * t)));
                        }
                    }
                    if (allDone)
                    {
                        ////start again from zero next time
                        //Reset();
                        return 0;
                    }

                    //tempPlayed.Add(intens);


                    b2.Add(intens);
                    buffer[n + offset] = intens;

                    sample++;
                    //if (sample > sampleRate) sample = 0;
                }

                //if (sw.ElapsedMilliseconds > 5000) return 0;

                return count;
            }

            /// <summary>Resets parameters</summary>
            public void Reset()
            {
                t = 0;
                sample = 0;
                curNote = new List<int>();
                durPlayedNotes = new List<double>();
                phase = new List<double>();
            }
        }
        #endregion

        #region Melody modification

        /// <summary>Increases/decreases all frequencies so that frequency of first note of first track Notes[0][0] has selected frequency</summary>
        /// <param name="frequency">Frequency to set Notes[0][0] to</param>
        public void ModulateSound(double frequency)
        {
            double n0 = Note.GetHalfStepsAboveA4(Notes[0][0].Frequency[0]);
            double deltaN = Note.GetHalfStepsAboveA4(frequency) - n0;

            foreach (List<Note> lstN in Notes)
            {
                foreach (Note n in lstN)
                {
                    n.modulated = false;
                }
            }

            foreach (List<Note> lstN in Notes)
            {
                foreach (Note n in lstN)
                {
                    if (!n.modulated)
                    {
                        n.modulated = true;
                        for (int kk = 0; kk < n.Frequency.Count; kk++)
                        {
                            double nHalfSteps = Note.GetHalfStepsAboveA4(n.Frequency[kk]);
                            //double finit = n.Frequency[kk];

                            n.Frequency[kk] = Note.GetFrequency(nHalfSteps + deltaN);
                        }
                    }
                }
            }

        }

        #endregion


    }
}
