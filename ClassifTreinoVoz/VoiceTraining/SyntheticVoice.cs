using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClassifTreinoVoz
{
    public class SyntheticVoice
    {
        /// <summary>Configuration</summary>
        public static class Config
        {
            /// <summary>Overlap time between waveshapespeech </summary>
            public static float VoiceOverlapTime = 0.1f;
        }

        /// <summary>Speech wave shape - periodic wave shape is a function of the frequency, adds aperiodic part</summary>
        public class WaveShapeSpeech : SoundSynthesis.WaveShape
        {
            /// <summary>Creates vowel wave sample from audio sample, from t0 to tf, extrapolating the sample into other frequencies</summary>
            /// <param name="sa">Sample audio</param>
            /// <param name="t0">Initial time in audio</param>
            /// <param name="tf">Final time in audio</param>
            /// <returns></returns>
            public static WaveShapeSpeech ExtrapolatePeriodicSample(AudioComparer.SampleAudio sa, float t0, float tf)
            {
                int idx0 = (int)(t0 / sa.time[1]);
                int idxf = (int)(tf / sa.time[1]) + 1;
                sa.ComputeBaseFreqAndFormants(t0, tf, "0", "3e-3", "6", "0", true);

                float stepFreq = (float)sa.waveFormat.SampleRate / (float)AudioComparer.SampleAudio.FFTSamples;

                float[] ampPerFreq = new float[sa.audioSpectre[0].Length];
                for (int k = 0; k < ampPerFreq.Length; k++)
                {
                    if (k * stepFreq > 50.0f)
                    {
                        for (int i = 0; i < sa.audioSpectre.Count; i++) ampPerFreq[k] = Math.Max((float)Math.Pow(10, 0.05 * sa.audioSpectre[i][k]), ampPerFreq[k]);
                    }
                }

                float sampleTime = sa.time[1];
                //total time in seconds
                float totalTime = 11;

                int nSamples = (int)(totalTime / sampleTime);
                float[] freqs = new float[nSamples];
                float[] wave = new float[nSamples];
                for (int k = 0; k < nSamples; k++)
                {
                    double time = (double)k / 44100.0;
                    double freq = 50 + 30 * time;// +60 * time;

                    freqs[k] = (float)freq;
                    //wave[k] += (float)(20 * Math.Sin(2.0 * Math.PI * freq * time));

                    //harmonics
                    for (int i = 1; i < (int)((ampPerFreq.Length - 2) * stepFreq / freq); i++)
                    {
                        double curHarmonicFreq = freq * i;

                        int idAmp = (int)(curHarmonicFreq / stepFreq);
                        float w = (float)curHarmonicFreq / stepFreq - idAmp;

                        float A = (1 - w) * ampPerFreq[idAmp] + w * ampPerFreq[idAmp + 1];

                        wave[k] += (float)(2000 * A * Math.Sin(2.0 * Math.PI * i * freq * time));
                        //wave[k] += (float)(20 * Math.Sin(2.0 * Math.PI * i * freq * time));
                    }
                }

                return new WaveShapeSpeech(wave, null, freqs, sampleTime);
            }

            /// <summary>Creates vowel wave sample from audio sample, from t0 to tf</summary>
            /// <param name="sa">Sample audio</param>
            /// <param name="t0">Initial time in audio</param>
            /// <param name="tf">Final time in audio</param>
            /// <returns></returns>
            public static WaveShapeSpeech FromPeriodicSample(AudioComparer.SampleAudio sa, float t0, float tf)
            {
                int idx0 = (int)(t0 / sa.time[1]);
                int idxf = (int)(tf / sa.time[1]) + 1;
                sa.ComputeBaseFreqAndFormants(t0, tf, "0", "3e-3", "6", "0", true);

                int idx0Freq = (int)(t0 / sa.baseFreqsTimes[1]);
                int idxfFreq = (int)(tf / sa.baseFreqsTimes[1]);

                float[] vowelSample = new float[idxf - idx0 + 1];
                float[] vowelFreqs = new float[idxfFreq - idx0Freq + 1];

                for (int k = 0; k < idxf - idx0 + 1; k++) vowelSample[k] = sa.audioData[idx0 + k];
                for (int k = 0; k < idxfFreq - idx0Freq + 1; k++) vowelFreqs[k] = sa.baseFreqsF0[idx0Freq + k];

                WaveShapeSpeech wsv = new WaveShapeSpeech(vowelSample, null, vowelFreqs, sa.time[1]);
                return wsv;
            }

            /// <summary>Creates consonant wave sample from audio sample, from t0 to tf</summary>
            /// <param name="sa">Sample audio</param>
            /// <param name="t0">Initial time in audio</param>
            /// <param name="tf">Final time in audio</param>
            /// <returns></returns>
            public static WaveShapeSpeech FromAperiodicSample(AudioComparer.SampleAudio sa, float t0, float tf)
            {
                int idx0 = (int)(t0 / sa.time[1]);
                int idxf = (int)(tf / sa.time[1]) + 1;

                float[] consSample = new float[idxf - idx0 + 1];

                for (int k = 0; k < idxf - idx0 + 1; k++) consSample[k] = sa.audioData[idx0 + k];

                WaveShapeSpeech wsv = new WaveShapeSpeech(null, consSample, null, sa.time[1]);
                return wsv;
            }

            /// <summary>Creates a new custom wave shape for speech. vSample and vFrequencies dont have to be same length - 
            /// frequencies will be sought at a corresponding percentage of vector length</summary>
            /// <param name="periodicSample">Vowel - periodic sound sample</param>
            /// <param name="aperiodicSample">Consonant - aperiodic sound sample</param>
            /// <param name="periodicFrequencies">Corresponding vowel frequencies</param>
            /// <param name="sampleTime">Sample time of signal</param>
            public WaveShapeSpeech(float[] periodicSample, float[] aperSample, float[] periodicFrequencies, float sampleTime)
            {
                this.periodicSample = periodicSample;
                this.periodicFrequencies = periodicFrequencies;
                this.audioSampleTime = sampleTime;

                this.aperiodicSample = aperSample;
                if (aperiodicSample != null)
                {
                    float max = float.MinValue;
                    foreach (float f in aperiodicSample) max = Math.Max(max, f);
                    max = 0.4f / max;
                    for (int k = 0; k < aperiodicSample.Length; k++) aperiodicSample[k] *= max;
                }

                if (periodicSample != null)
                {
                    waveShapeForFreq.Add(0); waveShapeForFreq.Add(0);
                }

            }

            /// <summary>Sample time</summary>
            private float audioSampleTime;
            /// <summary>Vowel sample at 4410kHz</summary>
            public float[] periodicSample;
            /// <summary>Consonant sample at 4410kHz</summary>
            private float[] aperiodicSample;
            /// <summary>Corresponding vowel frequencies</summary>
            private float[] periodicFrequencies;

            /// <summary>Current vowel frequency</summary>
            float curplayfreq = 0;

            /// <summary>Wave shape for current frequency</summary>
            List<float> waveShapeForFreq = new List<float>();

            ///// <summary>Note play time in seconds</summary>
            //float _noteplaytime = 1;

            ///// <summary>Total note play time in seconds. Useful for aperiodic part</summary>
            //public float PlayTimeNote
            //{
            //    set
            //    {
            //        _noteplaytime = value;
            //    }
            //}

            /// <summary>Sets play frequency, to adjust what wave shape will be used</summary>
            public float PlayFrequency
            {
                set
                {
                    if (value == curplayfreq) return;
                    if (periodicSample == null) return;

                    curplayfreq = value;

                    //find start point corresponding to the frequency
                    int idFreq = 0;
                    for (int k = 1; k < periodicFrequencies.Length; k++)
                    {
                        if (periodicFrequencies[k - 1] <= curplayfreq && curplayfreq < periodicFrequencies[k])
                        {
                            idFreq = k - 1;
                            break;
                        }
                    }

                    //cut appropriate wave shape from sample
                    int idSample = (int)((double)idFreq * (double)periodicSample.Length / (double)periodicFrequencies.Length);
                    if (idSample < 1) idSample = 1;

                    //need nSamples = sound Period / sampleTime
                    int nSamples = (int)(1.0f / (curplayfreq * audioSampleTime));

                    //select an appropriate wave shape
                    waveShapeForFreq.Clear();
                    //waveShapeForFreq.Add(0);

                    //upcrossing
                    int idCut0 = 0;
                    for (int k = idSample; k < nSamples + idSample; k++)
                    {
                        if (periodicSample[k - 1] <= 0 && periodicSample[k] > 0)
                        {
                            idCut0 = k;
                            break;
                        }
                    }

                    for (int k = idCut0; k < idCut0 + nSamples - 1; k++) waveShapeForFreq.Add(periodicSample[k]);
                    //waveShapeForFreq.Add(0);

                    float invwaveShapemaxIntens = 1.0f / waveShapeForFreq.Max(i => Math.Abs(i));
                    for (int k = 0; k < waveShapeForFreq.Count; k++) waveShapeForFreq[k] *= invwaveShapemaxIntens;
                }
            }

            /// <summary>Aperiodic id of return. Gets back to zero after passing through all aperiodic signal</summary>
            int localAperiodicId = 0;

            /// <summary>Wave intensity from vowel - wave shape computed for a given frequency</summary>
            /// <param name="time">Local time 0-1</param>
            /// <returns></returns>
            public override double GetWaveIntensity(double time)
            {
                float ans = 0;

                //aperiodic part
                if (aperiodicSample != null)
                {
                    ans += aperiodicSample[localAperiodicId];
                    localAperiodicId++;
                    if (localAperiodicId == aperiodicSample.Length) localAperiodicId = 0;

                }

                //periodic part
                if (periodicSample != null)
                {
                    time = time - (int)time;
                    int id = (int)(time * (waveShapeForFreq.Count - 1));

                    //float w = (float)time * (waveShapeForFreq.Count - 1) - id;
                    //return waveShapeForFreq[id] * (1.0f - w) + waveShapeForFreq[id + 1];

                    //parabolic interp attempt
                    float x0 = 0, x1 = 1, x2 = 2;
                    float y0, y1, y2;
                    if (id <= 0) y0 = waveShapeForFreq[waveShapeForFreq.Count - 1]; else y0 = waveShapeForFreq[id - 1];
                    if (id >= waveShapeForFreq.Count - 1) y2 = waveShapeForFreq[0]; else y2 = waveShapeForFreq[id + 1];
                    y1 = waveShapeForFreq[id];

                    float xx = (float)time * (waveShapeForFreq.Count - 1) - id;

                    ans += ParabolicInterp(xx, x0, x1, x2, y0, y1, y2);
                }

                return ans;
            }

            public override double GetWaveAmplitude(double time, double duration)
            {
                return 1;
                //double constMult = 3;
                //return Math.Exp(-constMult * time);
            }

            private static float ParabolicInterp(float xx, float x0, float x1, float x2, float y0, float y1, float y2)
            {
                float invdenom = 1.0f / ((x0 - x1) * (x0 - x2) * (x1 - x2));
                float a = (x0 * (y2 - y1) + x1 * (y0 - y2) + x2 * (y1 - y0)) * invdenom;
                float b = (x0 * x0 * (y1 - y2) + x2 * x2 * (y0 - y1) + x1 * x1 * (y2 - y0)) * invdenom;
                float c = (x0 * x0 * (x1 * y2 - x2 * y1) + x1 * x1 * (x2 * y0 - x0 * y2) + x2 * x2 * (y1 * x0 - y0 * x1)) * invdenom;

                return a * a * xx + b * xx + c;
            }
        }

        /// <summary>Reproducible phonemes</summary>
        SortedDictionary<string, WaveShapeSpeech> _dicPhonemes;

        /// <summary>Builds synthetic voice from an annotated sample audio. Annotations greater than 5s
        /// are assumed to be vowels; the others are assumed to be consonants.</summary>
        /// <param name="sa">Sample audio</param>
        public SyntheticVoice(List<AudioComparer.SampleAudio> lstSa)
        {
            _dicPhonemes = new SortedDictionary<string, WaveShapeSpeech>();

            foreach (AudioComparer.SampleAudio sa in lstSa)
            {
                foreach (AudioComparer.SampleAudio.AudioAnnotation ann in sa.Annotations)
                {
                    string s = ann.Text;
                    if (s.StartsWith("[2]"))
                    {
                        s = s.Replace("[2]", "").Replace("\r\n", "");
                        WaveShapeSpeech wsp;

                        if (ann.stopTime - ann.startTime > 3.5) wsp = WaveShapeSpeech.FromPeriodicSample(sa, (float)ann.startTime, (float)ann.stopTime);
                        //if (ann.stopTime - ann.startTime > 3.5) wsp = WaveShapeSpeech.ExtrapolatePeriodicSample(sa, (float)ann.startTime, (float)ann.stopTime);
                        else wsp = WaveShapeSpeech.FromAperiodicSample(sa, (float)ann.startTime, (float)ann.stopTime);

                        if (!_dicPhonemes.ContainsKey(s)) _dicPhonemes.Add(s, wsp);
                    }
                }
            }
        }

        /// <summary>Constructor</summary>
        /// <param name="phonemes">Phonemes of desired voice</param>
        public SyntheticVoice(SortedDictionary<string, WaveShapeSpeech> phonemes)
        {
            this._dicPhonemes = phonemes;
        }

        /// <summary>Apply phonemes</summary>
        /// <param name="lstN"></param>
        public void ApplyPhonemes(List<SoundSynthesis.Note> lstN)
        {
            foreach (SoundSynthesis.Note n in lstN)
            {
                string[] s = n.buildString.Split();
                string phonemeCandidate = s[s.Length - 1];

                WaveShapeSpeech wsp;
                if (_dicPhonemes.TryGetValue(phonemeCandidate, out wsp)) n.waveShape = wsp;
            }
        }
    }
}
