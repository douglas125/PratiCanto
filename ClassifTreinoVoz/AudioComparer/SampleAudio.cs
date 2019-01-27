using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NAudio.Wave;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.ComponentModel;

namespace AudioComparer
{
    /// <summary>Sample audio analyzer</summary>
    public partial class SampleAudio
    {
        #region Configuration parameters

        /// <summary>Maximum number of FFTs</summary>
        public static int MAXFFTCurvePts = 3000;

        /// <summary>Number of samples for FFT</summary>
        public static int FFTSamples = 1024 * 2; //1024 * 8

        /// <summary>do not compute all ffts; jump this number of points</summary>
        static int NumPtsToSkipInFFT = 128; //FFTSamples / 64;

        /// <summary>Height of bitmaps in pixels</summary>
        public static int BITMAPHEIGHTS = 800;

        /// <summary>Maximum frequency to keep, in Hz</summary>
        public static double maxKeepFreq = 10000;

        #endregion

        /// <summary>File containing this audio, currently open</summary>
        public string AudioFile;

        /// <summary>Format of this data</summary>
        public WaveFormat waveFormat;

        /// <summary>Formants</summary>
        public List<List<LinearPredictor.Formant>> baseFormants;

        /// <summary>Fundamental frequencies</summary>
        public List<float> baseFreqsF0;
        /// <summary>Times of sound associated with the base frequencies</summary>
        public List<float> baseFreqsTimes;
        /// <summary>Sound intensity associated with base frequencies</summary>
        public List<float> baseFreqsIntensity;
        /// <summary>Local harmonic to noise ratio</summary>
        public List<float> baseHNR;

        /// <summary>Silence parameters of this audio</summary>
        public RealTime.Silence silenceParameters;

        #region Audio data, time and interpolation
        /// <summary>Audio data</summary>
        public float[] audioData;

        /// <summary>Audio spectre</summary>
        public List<float[]> audioSpectre;

        /// <summary>Associated times</summary>
        public float[] time;

        /// <summary>Retrieves intensity at a given time</summary>
        /// <param name="t">Time to retrieve</param>
        public double GetIntensAtTime(double t)
        {
            double w = t / time[1];
            int idx = (int)w;
            w = w - idx;
            if (idx >= 0 && idx < audioData.Length - 2) return mix(audioData[idx], audioData[idx + 1], w);
            else return 0;
        }
        private double mix(double x, double y, double t)
        {
            return x + (y - x) * t;
        }
        #endregion

        #region Constructors

        /// <summary>Creates a sample audio to analyze from file</summary>
        /// <param name="filename">Name of input file</param>
        public SampleAudio(string filename)
        {
            fromFile(filename);
        }

        WaveStream ConvertTo441KHz(string inFile)
        {
            int outRate = 44100;
            //var inFile = @"test.mp3";
            var outFile = System.Windows.Forms.Application.StartupPath + "resampleTemp.wav";

            if (System.IO.File.Exists(outFile)) System.IO.File.Delete(outFile);

            using (var reader = new WaveFileReader(inFile))//new Mp3FileReader(inFile))
            {
                var outFormat = new WaveFormat(outRate, reader.WaveFormat.Channels);
                using (var resampler = new MediaFoundationResampler(reader, outFormat))
                {
                    // resampler.ResamplerQuality = 60;
                    WaveFileWriter.CreateWaveFile(outFile, resampler);
                }
            }

            return new WaveFileReader(outFile);
        }

        void fromFile(string fileName)
        {
            AudioFile = fileName;
            WaveStream str = null;

            if (fileName.EndsWith(".wav", true, null))
            {
                str = new WaveFileReader(fileName);

                if (str.WaveFormat.SampleRate != 44100) str = ConvertTo441KHz(fileName);
            }
            else if (fileName.EndsWith(".mp3", true, null)) str = new Mp3FileReader(fileName);
            if (str == null) return;

            byte[] audioRawData = new byte[str.Length];
            str.Read(audioRawData, 0, (int)str.Length);

            waveFormat = str.WaveFormat;

            str.Close();
            str.Dispose();

            audioData = GetAudioIntensityData(audioRawData, out this.time);

            //attempt to retrieve annotations
            try
            {
                System.IO.FileInfo fi = new System.IO.FileInfo(fileName);
                string fiName = System.IO.Path.GetFileNameWithoutExtension(fileName);
                string srtFile = fi.DirectoryName + "\\" + fiName + ".srt";

                if (System.IO.File.Exists(srtFile)) this.Annotations = SampleAudio.AudioAnnotation.ReadFromFile(srtFile);
            }
            catch
            {
            }
        }

        /// <summary>Attempts to save annotations to .SRT file associated with this audiofile</summary>
        public void SaveAnnotations()
        {
            if (this.AudioFile == null || this.AudioFile == "") return;
            System.IO.FileInfo fi = new System.IO.FileInfo(this.AudioFile);
            string fiName = System.IO.Path.GetFileNameWithoutExtension(this.AudioFile);
            string srtFile = fi.DirectoryName + "\\" + fiName + ".srt";

            SampleAudio.AudioAnnotation.SaveToFile(this.Annotations, srtFile);
        }

        /// <summary>Creates audio data from realtime capture</summary>
        /// <param name="rt">Real time capture</param>
        public SampleAudio(SampleAudio.RealTime rt)
        {
            this.audioData = rt.audioReceived.ToArray();
            this.waveFormat = rt.waveSource.WaveFormat;
            this.time = rt.time.ToArray();
        }

        /// <summary>Creates audio from raw data</summary>
        /// <param name="time">Time data</param>
        /// <param name="audioData">Audio data</param>
        /// <param name="waveFormat">Waveformat</param>
        public SampleAudio(float[] time, float[] audioData, WaveFormat waveFormat)
        {
            this.audioData = audioData;
            this.waveFormat = waveFormat;
            this.time = time;
        }

        /// <summary>Creates audio data from realtime capture</summary>
        /// <param name="rt">Real time capture</param>
        /// <param name="idx0">Start index</param>
        /// <param name="idxf">Stop index</param>
        public SampleAudio(SampleAudio.RealTime rt, int idx0, int idxf)
        {
            if (idx0 < 0) idx0 = 0;
            if (idxf >= rt.audioReceived.Count) idxf = rt.audioReceived.Count - 1;

            //skip raw data
            audioData = new float[idxf - idx0 + 1];
            time = new float[idxf - idx0 + 1];
            waveFormat = rt.waveSource.WaveFormat;

            for (int k = idx0; k <= idxf; k++)
            {
                time[k - idx0] = (k - idx0) * (rt.time[1]);
                audioData[k - idx0] = rt.audioReceived[k];
            }
        }

        /// <summary>Composes a new audio using a base and inserting into it new audios. Assumes all sample times correspond to that of first audio</summary>
        /// <param name="baseAudio">Base audio</param>
        /// <param name="insertions">Audios to insert</param>
        /// <param name="insertTimes">Times to insert</param>
        public SampleAudio(SampleAudio baseAudio, List<SampleAudio> insertions, List<double> insertTimes)
        {
            ComposeAudioData(baseAudio, insertions, insertTimes, 0, 0);
        }

        /// <summary>Remakes audio data combining other audios</summary>
        /// <param name="baseAudio">Base audio</param>
        /// <param name="insertions">Audios to insert</param>
        /// <param name="insertTimes">Times to insert</param>
        /// <param name="voiceOverMultiplier">Voice over intensity multiplier. Set to ZERO to split original audio</param>
        /// <param name="origAudioMultiplier">Original audio intensity multiplier. Only used if voiceover is not zero </param>
        public void ComposeAudioData(SampleAudio baseAudio, List<SampleAudio> insertions, List<double> insertTimes, float voiceOverMultiplier, float origAudioMultiplier)
        {
            if (insertions.Count != insertTimes.Count) throw new Exception("Insert times must match insertions Count");

            List<float> newAudioData = new List<float>();
            List<float> newTimes = new List<float>();

            double sampleTime = 1.0 / (double)baseAudio.waveFormat.SampleRate;

            //copy original data
            foreach (float f in baseAudio.audioData) newAudioData.Add(f);

            double prevInsertTimes = 0;
            for (int k = 0; k < insertions.Count; k++)
            {
                int insertIndex = (int)((insertTimes[k] + prevInsertTimes) / sampleTime);
                if (insertIndex < 0) insertIndex = 0;
                //prevInsertTimes += insertions[k].time.Length * sampleTime;

                if (voiceOverMultiplier == 0) newAudioData.InsertRange(insertIndex, insertions[k].audioData);
                else
                {
                    for (int p = 0; p < insertions[k].audioData.Length; p++)
                    {
                        int id = p + insertIndex;
                        if (id >= newAudioData.Count) break;

                        newAudioData[id] = insertions[k].audioData[p] * voiceOverMultiplier + newAudioData[id] * origAudioMultiplier;
                    }
                }
            }

            //add times
            double currentTime = 0;
            foreach (float f in newAudioData)
            {
                newTimes.Add((float)currentTime);
                currentTime += sampleTime;
            }

            audioData = newAudioData.ToArray();
            time = newTimes.ToArray();
        }

        /// <summary>Dispose elements</summary>
        public void Dispose()
        {
            audioData = null;
            if (audioSpectre != null) audioSpectre.Clear();
        }

        #endregion

        #region Conversion of raw data to float intensity

        /// <summary>Retrieves intensity/time parameters for this audio</summary>
        /// <param name="audioRawData">Raw audio data</param>
        /// <param name="time">Time values</param>
        /// <returns></returns>
        private float[] GetAudioIntensityData(byte[] audioRawData, out float[] time)
        {
            List<float> intensData = new List<float>();
            List<float> lsttime = new List<float>();

            int curSample = 0;
            float sampleTime = 1.0f / (float)waveFormat.SampleRate;

            int sampleSize = waveFormat.BitsPerSample / 8;
            
            //read only one channel for the time being
            for (int k = 0; k < audioRawData.Length; k += sampleSize * waveFormat.Channels)
            {
                lsttime.Add(curSample * sampleTime); curSample++;
                float sample = 0;

                if (sampleSize == 2)
                {
                    Int16 val = BitConverter.ToInt16(audioRawData, k);
                    sample = val;
                }
                else if (sampleSize == 4)
                {
                    Int32 val = BitConverter.ToInt32(audioRawData, k);
                    sample = val;
                }
                else throw new Exception("Samplesize != 2 and != 4");

                intensData.Add(sample);
            }
            time = lsttime.ToArray();
            return intensData.ToArray();
        }

        #endregion

        #region Fourier Transform

        /// <summary>Recompute spectre</summary>
        public void RecomputeSpectre()
        {
            float[] f;
            List<float[]> curSampleSpectre = GetAudioFreqData(audioData, out f);
        }

        /// <summary>Retrieves intensity/frequency parameters</summary>
        /// <param name="audioReceived">Audio received</param>
        /// <param name="freq">Frequency associated with spectres</param>
        public List<float[]> GetAudioFreqData(float[] audioReceived, out float[] freq)
        {
            this.baseFreqsF0 = new List<float>();
            this.baseHNR = new List<float>();
            this.baseFreqsTimes = new List<float>();
            this.baseFreqsIntensity = new List<float>();
            this.baseFormants = new List<List<LinearPredictor.Formant>>();
            this.audioSpectre = GetAudioFreqData(audioReceived, out freq, this.waveFormat, baseFreqsTimes, baseFreqsF0, baseHNR, baseFreqsIntensity, baseFormants);

            //int idSilence = 0;
            //int nSilenceSamples = 50;
            //for (int i = 1; i < audioSpectre.Count; i++)
            //{
            //    if (audioSpectre[i][0] == audioSpectre[i - 1][0])
            //    {
            //        idSilence = i;
            //    }
            //    else break;
            //}
            //idSilence *= 2; //first idSilence are equal; until 2*idsilence very correlated

            ////consider first 0.3s as silence
            //int idMax = (int)(0.3 / this.time[1]);
            //float[] silenceIntensData = new float[idMax];
            //for (int k = 0; k < idMax; k++) silenceIntensData[k] = this.audioData[k];

            //silenceParameters = new RealTime.Silence(audioSpectre, nSilenceSamples, idSilence, silenceIntensData);

            ////Subtract silence?
            //for (int k = 0; k < this.audioSpectre.Count; k++)
            //{
            //    silenceParameters.SubtractSilenceSpectrum(audioSpectre[k]);
            //}
            ////recompute silence parameters
            //silenceParameters = new RealTime.Silence(audioSpectre, nSilenceSamples, idSilence, silenceIntensData);

            return audioSpectre;
        }

        /// <summary>Retrieves intensity/frequency parameters</summary>
        /// <param name="audioReceived">Audio received</param>
        /// <param name="freq">Frequency associated with spectres</param>
        /// <param name="waveFormat">Wave format</param>
        /// <param name="timeBaseFreqs">Time where F0 were obtained</param>
        /// <param name="baseFreqs">F0 base freqs</param>
        /// <param name="audioIntensities">Intensities at timeBaseFreqs</param>
        /// <returns></returns>
        public static List<float[]> GetAudioFreqData(float[] audioReceived, out float[] freq, WaveFormat waveFormat, List<float> timeBaseFreqs, List<float> baseFreqs, List<float> baseHNR, List<float> audioIntensities, List<List<LinearPredictor.Formant>> baseFormants)
        {

            //total number of graphs
            int nFFTs = audioReceived.Length / NumPtsToSkipInFFT;

            //limit maximum number of FFTs
            if (nFFTs > MAXFFTCurvePts) nFFTs = MAXFFTCurvePts;
            int localNumPtsSkip = audioReceived.Length / nFFTs;

            List<float[]> intensData = new List<float[]>();
            freq = null;

            for (int k = 0; k < nFFTs; k++)
                intensData.Add(null);

            //****************
            //keep data up to maxKeepFreq (=idMax * stepFreq) 
            float stepFreq = (float)waveFormat.SampleRate / (float)FFTSamples;
            int idMax = (int)(maxKeepFreq / stepFreq);

            freq = new float[idMax];
            for (int k = 0; k < idMax; k++) freq[k] = k * stepFreq;
            //****************

            //Populate vectors
            if (timeBaseFreqs != null)
            {
                for (int k = 0; k < nFFTs; k++)
                {
                    timeBaseFreqs.Add(0);
                    baseFreqs.Add(0);
                    baseHNR.Add(0);
                    audioIntensities.Add(0);
                    baseFormants.Add(new List<LinearPredictor.Formant>());
                }
            }

            //System.Diagnostics.Stopwatch swFFT = new System.Diagnostics.Stopwatch();
            System.Diagnostics.Stopwatch swTot = new System.Diagnostics.Stopwatch();
            //System.Diagnostics.Stopwatch swf0 = new System.Diagnostics.Stopwatch();


            swTot.Start();
            

            //for (int k = 0; k < nFFTs; k++)
            Parallel.For(0, nFFTs, kk =>
            {
                int k = (int)kk;
                //swFFT.Start();
                double[] tempFFT = computeAudioFFT(audioReceived, k * localNumPtsSkip, idMax);

                intensData[k] = new float[tempFFT.Length];
                for (int dd = 0; dd < intensData[k].Length; dd++) intensData[k][dd] = (float)tempFFT[dd];

                //swFFT.Stop();

                if (timeBaseFreqs != null)
                {
                    timeBaseFreqs[k] = (float)k * (float)localNumPtsSkip / (float)waveFormat.SampleRate;
                    //swf0.Start();
                    //baseFreqs[k] = SampleAudio.RealTime.ComputeBaseFrequency(audioReceived, 1.0f / (float)waveFormat.SampleRate, k * NumPtsToSkipInFFT, 63);//SampleAudio.RealTime.ComputeBaseFrequency(intensData[k], stepFreq)[0];

                    ////don't need to recompute FFT
                    //List<LinearPredictor.Formant> formants = new List<LinearPredictor.Formant>();
                    //baseFreqs[k] = LinearPredictor.ComputeFormants(audioReceived, stepFreq, k * NumPtsToSkipInFFT, 68, 1250, out formants, tempFFT);
                    //baseFormants[k] = formants;

                    //swf0.Stop();
                    //audioIntensities[k] = SampleAudio.RealTime.ComputeIntensity(audioReceived, 1.0f / (float)waveFormat.SampleRate, k * NumPtsToSkipInFFT, 63);
                    //baseFreqs[k] = SampleAudio.RealTime.ComputeBaseFrequency(audioReceived, 1.0 / (double)waveFormat.SampleRate, k * skip);//SampleAudio.RealTime.ComputeBaseFrequency(intensData[k], stepFreq)[0];
                    //baseFreqs[k] = SampleAudio.RealTime.ComputeBaseFrequency(intensData[k], stepFreq)[0];//SampleAudio.RealTime.ComputeBaseFrequency(intensData[k], stepFreq)[0];
                }
            });
            swTot.Stop();
            //double meanTime = swFFT.Elapsed.TotalSeconds / (double)nFFTs;

            return intensData;
        }

        /// <summary>Computes base frequency and formants</summary>
        /// <param name="t0">Initial time</param>
        /// <param name="tf">Final time</param>
        public void ComputeBaseFreqAndFormants(float t0, float tf, string txtdistPenalty, string txtChangeFreqPenalty, string nFreqsCals, string txtVarPenalty, bool useMedianFilter)
        {
            if (audioSpectre == null) RecomputeSpectre();

            float tstep = baseFreqsTimes[1];
            int idx0 = (int)(t0 / tstep);
            int idxf = (int)(tf / tstep);
            float stepFreq = (float)waveFormat.SampleRate / (float)FFTSamples;
            int idMax = (int)(maxKeepFreq / stepFreq);

            if (idx0 < 0) idx0 = 0;
            if (idxf > audioSpectre.Count - 1) idxf = audioSpectre.Count - 1;
            if (idx0 > audioSpectre.Count - 1) idx0 = audioSpectre.Count - 1;

            for (int k = idx0; k <= idxf; k++)
            //Parallel.For(0, idxf - idx0 + 1, kk =>
            {
                //int k = (int)kk + idx0;
                if (baseFormants[k].Count == 0)
                {

                    //swFFT.Start();
                    float[] fft = this.audioSpectre[k];
                    double[] refFft = new double[fft.Length];
                    for (int q = 0; q < fft.Length; q++) refFft[q] = fft[q];

                    //swf0.Start();
                    //baseFreqs[k] = SampleAudio.RealTime.ComputeBaseFrequency(audioReceived, 1.0f / (float)waveFormat.SampleRate, k * NumPtsToSkipInFFT, 63);//SampleAudio.RealTime.ComputeBaseFrequency(intensData[k], stepFreq)[0];

                    //don't need to recompute FFT
                    List<LinearPredictor.Formant> formants = new List<LinearPredictor.Formant>();
                    float hnr;
                    this.baseFreqsF0[k] = LinearPredictor.ComputeReinforcedFrequencies(audioData, stepFreq, k, 68, 1250, out formants, refFft, out hnr);
                    this.baseHNR[k] = hnr;

                    this.baseFormants[k] = formants;

                    this.baseFreqsIntensity[k] = SampleAudio.RealTime.ComputeIntensity(audioData, 1.0f / (float)waveFormat.SampleRate, (int)(baseFreqsTimes[k] / time[1]), 20);
                }

            }//);

            if (useMedianFilter)
            {
                //smooth fundamental frequencies
                LinearPredictor.MedianFilterFundamentalFrequencies(baseFreqsF0, idx0, idxf, (int)(this.waveFormat.SampleRate * 0.0004f));

                //smooth intensities
                LinearPredictor.MedianFilterFundamentalFrequencies(baseFreqsIntensity, idx0, idxf, (int)(this.waveFormat.SampleRate * 0.0004f));

                //smooth HNR
                LinearPredictor.MedianFilterFundamentalFrequencies(baseHNR, idx0, idxf, (int)(this.waveFormat.SampleRate * 0.0004f));
            }

            for (int k = idx0; k <= idxf; k++) this.baseFormants[k][0].Frequency = baseFreqsF0[k];

            LinearPredictor.TrackFormants(this.baseFormants, idx0, idxf, txtdistPenalty, txtChangeFreqPenalty, nFreqsCals, txtVarPenalty);
        }


        /// <summary>FFT structure holder</summary>
        static LomontFFT fft = new LomontFFT();

        /// <summary>Computes audio FFT at a given index</summary>
        /// <param name="audioReceived">Audio data</param>
        /// <param name="idx">Desired index</param>
        /// <param name="numFreqKeep">Number of frequencies to keep</param>
        /// <returns></returns>
        public static double[] computeAudioFFT(float[] audioReceived, int idx, int numFreqKeep)
        {
            //double[] x = new double[FFTSamples];
            double[] y = new double[FFTSamples];

            if (idx < FFTSamples-1) idx = FFTSamples-1;
            if (idx >= audioReceived.Length) idx = audioReceived.Length - 1;
            int initIdx = idx - FFTSamples + 1;
            if (initIdx < 0) initIdx = 0;
            
            for (int i = idx; i >= initIdx; i--) y[i - idx + FFTSamples - 1] = audioReceived[i];


            double[] y2 = new double[y.Length * 2];
            for (int k = 0; k < y.Length; k++)
            {
                double windowFcn = (double)k / (double)(FFTSamples - 1);
                double hanning = 0.5 * (1 - Math.Cos(2 * Math.PI * windowFcn));

                y2[k << 1] = (float)y[k];
                y2[k << 1] *= (float)hanning;
            }

            //System.Diagnostics.Stopwatch sw1 = new System.Diagnostics.Stopwatch();
            //sw1.Start();
            //fft.FFT(y2, true);

            //y2 = OpenCLTemplate.FourierTransform.CLFFTfloat.FFT4(y2);

            fft.RealFFT(y2, true);
            //y2[1] = 0;

            //sw1.Stop();

            //fft.RealFFT(y2TEMP, true);
            

            //double freqAmost = waveFormat.SampleRate;
            //double stepFreq = (double)freqAmost / (double)FFTSamples;
            double[] yAns = new double[numFreqKeep];

            //double maxAmpl = waveFormat.BitsPerSample == 16 ? (32767.0 * 32767.0) : (32767.0 * 32767.0 * 32767.0 * 32767.0);  //Max power used for 16 bit audio
            double invMaxAmpl = 1.0/32767.0; //= waveFormat.BitsPerSample == 16 ? (32767.0) : (32767.0 * 32767.0);  //Max power used for 16 bit audio
            for (int k = 0; k < numFreqKeep; k++)
            {
                //x[k] = k * stepFreq; // *(double)13.15396;// k* freqStep;

                double amp = Math.Sqrt(y2[k << 1] * y2[k << 1] + y2[1 + (k << 1)] * y2[1 + (k << 1)]);

                if (amp > 0) yAns[k] = 20 * Math.Log10(amp * invMaxAmpl);//(double)audioReceived[k];
                //yAns[k] = amp / maxAmpl;
            }


            //freq = x;
            return yAns;
        }

        #endregion

        #region Real time analyses and recording

        /// <summary>Real time analyses and recording</summary>
        public class RealTime
        {
            /// <summary>Maximum number of samples to keep</summary>
            public long MAXSAMPLES = long.MaxValue;

            /// <summary>Wave source</summary>
            public WaveIn waveSource = null;
            /// <summary>Sampling frequency in Hz</summary>
            public int freqAmost = 44100;

            /// <summary>Audio received</summary>
            public List<float> audioReceived;

            ///// <summary>Stores latest audio received for FFT purposes</summary>
            //public List<float> latestAudio;

            ///// <summary>Raw audio data in bytes</summary>
            //public List<byte> audioRaw;

            /// <summary>Times of acquisition</summary>
            public List<float> time;

            /// <summary>Acquisition interval</summary>
            public float curT, deltaT;

            /// <summary>Delegate to callback when data is received</summary>
            public delegate void DataReceived();

            /// <summary>This delegate is called whenever data is received</summary>
            public DataReceived OnDataReceived;

            #region Recording

            /// <summary>Is recording paused?</summary>
            public bool isPaused = false;

            /// <summary>Returns a list of available input devices</summary>
            /// <returns></returns>
            public static List<string> GetMicrophones()
            {
                List<string> devices = new List<string>();
                int waveInDevices = WaveIn.DeviceCount;
                for (int waveInDevice = 0; waveInDevice < waveInDevices; waveInDevice++)
                {
                    WaveInCapabilities deviceInfo = WaveIn.GetCapabilities(waveInDevice);
                    devices.Add(deviceInfo.ProductName);
                }
                return devices;
            }

            /// <summary>Naudio Wavein buffer size. Use zero to set to default</summary>
            public int AudioBufferSizeInMilliseconds = 0;

            /// <summary>Start recording</summary>
            /// <param name="Device">Microphone to use</param>
            public void StartRecording(int Device)
            {

                //int waveInDevices = WaveIn.DeviceCount;
                //for (int waveInDevice = 0; waveInDevice < waveInDevices; waveInDevice++)
                //{
                //    WaveInCapabilities deviceInfo = WaveIn.GetCapabilities(waveInDevice);
                //    Console.WriteLine("Device {0}: {1}, {2} channels", waveInDevice, deviceInfo.ProductName, deviceInfo.Channels);
                //}


                audioReceived = new List<float>();
                //audioRaw = new List<byte>();
                time = new List<float>();

                waveSource = new WaveIn();
                waveSource.WaveFormat = new WaveFormat(freqAmost, 1);
                waveSource.DeviceNumber = Device;
                if (AudioBufferSizeInMilliseconds > 0) waveSource.BufferMilliseconds = AudioBufferSizeInMilliseconds;

                waveSource.DataAvailable += new EventHandler<WaveInEventArgs>(waveSource_DataAvailable);
                //waveSource.RecordingStopped += new EventHandler<StoppedEventArgs>(waveSource_RecordingStopped);

                //waveFile = new WaveFileWriter(@"C:\Temp\Test0001.wav", waveSource.WaveFormat);
                deltaT = 1.0f / (float)waveSource.WaveFormat.SampleRate;
                curT = 0;

                ////latest audio storage
                //latestAudio = new List<float>();
                //for (int k = 0; k < FFTSamples; k++) latestAudio.Add(0);

                waveSource.StartRecording();
            }


            /// <summary>Stop recording</summary>
            public void StopRecording()
            {
                waveSource.StopRecording();
            }

            /// <summary>Pause recording without stopping data acquisition from mic</summary>
            public void PauseRecording()
            {
                isPaused = true;
            }

            /// <summary>Resume recording</summary>
            public void ResumeRecording()
            {
                isPaused = false;
            }

            //void waveSource_RecordingStopped(object sender, StoppedEventArgs e)
            //{
            //}

            void waveSource_DataAvailable(object sender, WaveInEventArgs e)
            {
                //allow Rec Pausing
                if (isPaused) return;

                int sampleSize = waveSource.WaveFormat.BitsPerSample / 8; //2 bytes per sample

                int n = 0;
                for (int k = 0; k < e.BytesRecorded; k += sampleSize)
                {
                    Int16 val = BitConverter.ToInt16(e.Buffer, k);

                    //latestAudio.Add(val);
                    audioReceived.Add(val);
                    time.Add(curT);
                    curT += deltaT;

                    n++;
                }
                if (OnDataReceived != null) OnDataReceived();

                //Limits amount of data received
                if (audioReceived.Count > this.MAXSAMPLES)
                {
                    while (audioReceived.Count > this.MAXSAMPLES)
                    {
                        audioReceived.RemoveAt(0);
                        time.RemoveAt(0);
                    }
                    float t0 = time[0];
                    for (int k = 0; k < time.Count; k++) time[k] -= t0;
                }
            }



            #endregion

            #region Identify silence parameters

            /// <summary>Compute silence parameters</summary>
            public class Silence
            {
                /// <summary>Average spectrum of silence</summary>
                public float[] avg;

                /// <summary>Standard deviation of spectrum of silence</summary>
                public float[] stdDev;

                /// <summary>Frequencies in spectrum (Hz)</summary>
                public float[] freq;

                /// <summary>Average RMS value of silence</summary>
                public float silenceIntensAvg;
                /// <summary>Standard deviation RMS value of silence</summary>
                public float silenceIntensStddev;

                /// <summary>Constructor. Computes parameters</summary>
                /// <param name="silenceData">Array containing capture of silence</param>
                /// <param name="waveFormat">Waveformat used</param>
                /// <param name="idFreq0">Id of initial frequency</param>
                /// <param name="silenceIntensData">Silence intensity data</param>
                public Silence(float[] silenceData, WaveFormat waveFormat, int idFreq0, float[] silenceIntensData)
                {
                    //compute spectrum of silence
                    List<float[]> freqData = GetAudioFreqData(silenceData, out freq, waveFormat, null,null, null, null, null);

                    ComputeSilenceSpectrumAvgStdDev(freqData, freqData.Count, idFreq0);
                }

                /// <summary>Constructor</summary>
                /// <param name="freqData">Frequency data</param>
                /// <param name="nFreqsToUse">How many first components to use to determine silence</param>
                /// <param name="idFreq0">Id of initial frequency</param>
                /// <param name="silenceIntensData">Silence intensity data</param>
                public Silence(List<float[]> freqData, int nFreqsToUse, int idFreq0, float[] silenceIntensData)
                {
                    ComputeSilenceSpectrumAvgStdDev(freqData, nFreqsToUse, idFreq0);

                    float[] rms = new float[silenceIntensData.Length];
                    for (int k = 0; k < rms.Length; k++) rms[k] = silenceIntensData[k] * silenceIntensData[k];

                    silenceIntensAvg = getMean(rms, out silenceIntensStddev);
                }

                #region Statistics
                /// <summary>Computes average and stddev of spectrum based on nFreqsToUse first spectrums</summary>
                /// <param name="freqData">Frequency data</param>
                /// <param name="nFreqsToUse">Number of first spectrums to use</param>
                /// <param name="idFreq0">Id of initial frequency</param>
                private void ComputeSilenceSpectrumAvgStdDev(List<float[]> freqData, int nFreqsToUse, int idFreq0)
                {
                    int nCurves = nFreqsToUse;
                    int nPts = freqData[0].Length;
                    avg = new float[nPts];
                    stdDev = new float[nPts];


                    for (int k = 0; k < nPts; k++)
                    {
                        //average
                        float invTot = 1.0f / (float)nCurves;
                        for (int i = 0; i < nCurves; i++) avg[k] += freqData[i + idFreq0][k];
                        avg[k] *= invTot;

                        //stdDev
                        invTot = 1.0f / ((float)nCurves - 1);
                        for (int i = 0; i < nCurves; i++) stdDev[k] += (avg[k] - freqData[i + idFreq0][k]) * (avg[k] - freqData[i + idFreq0][k]);
                        stdDev[k] = (float)Math.Sqrt(stdDev[k] * invTot);
                    }
                }



                #endregion


                /// <summary>Subtracts average silence spectrum from data. Silence becomes 0 db</summary>
                /// <param name="audioSpectrum">Audio spectrum to subtract silence spectrum from</param>
                public void SubtractSilenceSpectrum(float[] audioSpectrum)
                {
                    int n = audioSpectrum.Length;
                    for (int k = 0; k < n; k++) audioSpectrum[k] -= avg[k];
                }

                /// <summary>Checks if audioIntensData[idx] is silence, according to this criterion</summary>
                /// <param name="audioIntensData">Audio intensity data</param>
                /// <param name="idx">Index to check</param>
                /// <param name="nAvg">Check nAvg points to define average</param>
                public bool IsSilence(float[] audioIntensData, int idx, int nAvg)
                {
                    int idMin = idx - nAvg;
                    int idMax = idx;
                    if (idMin < 0) idMin = 0;

                    double m = 0;
                    for (int k = idMin; k <= idMax; k++) m += audioIntensData[k] * audioIntensData[k];

                    m /= (double)(idMax - idMin);

                    return (m < silenceIntensAvg + 3 * silenceIntensStddev);
                }


            }

            /// <summary>Retrieves mean and standard deviation of a vector</summary>
            /// <param name="vec">Vector</param>
            /// <param name="stdDev">Standard deviation</param>
            /// <returns></returns>
            public static float getMean(float[] vec, out float stdDev)
            {
                int nPts = vec.Length;

                float avg = 0;

                //average
                float invTot = 1.0f / (float)nPts;
                for (int i = 0; i < nPts; i++) avg += vec[i];
                avg *= invTot;

                //stdDev
                stdDev = 0;
                invTot = 1.0f / ((float)nPts - 1);
                for (int i = 0; i < nPts; i++) stdDev += (avg - vec[i]) * (avg - vec[i]);
                stdDev = (float)Math.Sqrt(stdDev * invTot);

                return avg;
            }

            #endregion

            #region Retrieve current base frequency and sound intensity

            /// <summary>Computes intensity of audio data</summary>
            /// <param name="audioData">Audio data</param>
            /// <param name="timeStep">Audio acquisition time step</param>
            /// <param name="idx">Index at which to computa base frequency</param>
            /// <param name="minFreq">Lowest frequency of interest (Hz)</param>
            public static float ComputeIntensity(float[] audioData, double timeStep, int idx, double minFreq)
            {
                int windowSize = (int)(1.2 / (timeStep * minFreq));

                int minId = idx - windowSize;
                if (minId < 0) minId = 0;

                //sum of squares
                double ss = 0;
                for (int k = minId; k < idx; k++)
                {
                    ss += audioData[k] * audioData[k];
                }
                double maxAmpl = 32767.0; //= waveFormat.BitsPerSample == 16 ? (32767.0) : (32767.0 * 32767.0);  //Max power used for 16 bit audio
                if (idx != minId) ss /= (double)(idx - minId);
                ss = 20 * Math.Log10(ss / maxAmpl);

                return (float)ss;
            }




            /// <summary>Computes base frequency of audio data</summary>
            /// <param name="audioData">Audio data</param>
            /// <param name="timeStep">Audio acquisition time step</param>
            /// <param name="idx">Index at which to computa base frequency</param>
            /// <param name="minFreq">Lowest frequency of interest (Hz)</param>
            public static float ComputeBaseFrequencyOLD(float[] audioData, double timeStep, int idx, double minFreq)
            {
                System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
                sw.Start();

                int windowSize = (int)(1.1 / (timeStep * minFreq)); //(int)(1.1 / (timeStep * minFreq));

                double curDx = 0;
                double DxInt = 0;

                List<double> dnxs = new List<double>();

                for (int d = 1; d < windowSize; d++)
                {
                    curDx = Dx(d, audioData, windowSize, idx);
                    DxInt += curDx;

                    double DNx = curDx / (DxInt * (double)d);

                    dnxs.Add(DNx);
                }

                int dMin = 0;
                double valDmin = dnxs[0];

                List<double> localMin = new List<double>();
                List<int> idlocalMin = new List<int>();


                //List<double> TEMPfreqs = new List<double>();


                int founddMin = 0;
                int noFoundGuess = 0;

                for (int k = 1; k < windowSize - 2; k++)
                {
                    if (dnxs[k] < dnxs[k - 1] && dnxs[k] < dnxs[k + 1])
                    {
                        if (localMin.Count == 0 || (localMin.Count > 0 && dnxs[k] < localMin[0] * 0.8)) //clearly an outlier if dnxs[k]>localMin[0], disregard
                        {
                            localMin.Add(dnxs[k]);
                            idlocalMin.Add(k);
                            //TEMPfreqs.Add(1 / (k * timeStep));

                            //keep searching even if greater than minimum found: has to be significantly lower
                            if (dnxs[k] < valDmin * 2.5)
                            {
                                valDmin = dnxs[k];

                                //if next candidate is exactly half of current and no good candidate is found, this is best guess
                                if (Math.Abs(2 * dMin - k) <= 2 && noFoundGuess == 0)
                                    noFoundGuess = dMin;

                                dMin = k;
                            }
                            else
                            {
                                if (founddMin == 0) founddMin = dMin;
                                //break;
                            }
                        }
                    }
                }
                if (founddMin == 0) founddMin = noFoundGuess;

                if (founddMin == 0 && idlocalMin.Count > 0) founddMin = idlocalMin[0];
                sw.Stop();
                if (founddMin == 0) return 0;
                return (float)(1.0f / (founddMin * timeStep));
            }

            /// <summary>Computes squared difference of signal</summary>
            /// <param name="d">Index</param>
            /// <param name="x">Vector</param>
            /// <param name="windowSize">window size</param>
            /// <param name="idx">Index in vector</param>
            /// <returns></returns>
            private static double Dx(int d, float[] x, int windowSize, int idx)
            {
                if (idx >= x.Length) idx = x.Length - 1;
                int idMin = idx - windowSize;
                if (idMin - d < 0) idMin = d;
                double sum = 0;
                for (int k = idMin; k < windowSize + idMin; k++)
                {
                    double temp = x[k] - x[k - d];
                    sum += temp * temp;
                }

                return sum;
            }

            /// <summary>Computes base frequency of audio data</summary>
            /// <param name="audioData">Audio data</param>
            /// <param name="timeStep">Audio acquisition time step</param>
            /// <param name="idx">Index at which to computa base frequency</param>
            public static double ComputeBaseFrequencyOLD(double[] audioData, double timeStep, int idx)
            {
                double[] autoCorr = ComputeAutocorrelation(audioData, idx);

                List<double> localMax = new List<double>();
                List<int> localMaxIdx = new List<int>();

                //List<double> TEMPMaxfreq = new List<double>();

                //fundamental frequency in humans: 100Hz to 300Hz
                //int kMin = (int)(1.0 / (300 * timeStep) - 2);
                int kMax = (int)(1.0 / (80 * timeStep) + 2);

                //for (int k = 1; k < autoCorr.Length - 1; k++)
                for (int k = 1; k < kMax; k++)
                {
                    if (autoCorr[k - 1] < autoCorr[k] && autoCorr[k] > autoCorr[k + 1])
                    {
                        double tempFreq = 1 / (k * timeStep);

                        localMax.Add(autoCorr[k]);
                        localMaxIdx.Add(k);
                        //TEMPMaxfreq.Add(tempFreq);
                    }
                    //if (autoCorr[k] < 0 && localMaxIdx.Count > 0) break;
                }
                if (localMax.Count == 0)
                {
                    localMaxIdx.Add(0);
                    localMax.Add(0);
                }

                int idMax = localMaxIdx[0];
                double valMax = localMax[0];
                for (int k = 1; k < localMaxIdx.Count - 1; k++)
                {
                    if (localMax[k - 1] < localMax[k] && localMax[k] > localMax[k + 1] && localMaxIdx[k] > 10)
                    {

                        //double integerDiff = (double)localMaxIdx[k] / (double)idMax;
                        //integerDiff = Math.Abs(integerDiff - Math.Round(integerDiff));
                        //try to prevent identification of harmonics
                        //if (integerDiff < 0.1)


                        //accept lower correlation up to a certain limit
                        if (localMax[k] > valMax - 0.2)
                        {
                            idMax = localMaxIdx[k];
                            valMax = localMax[k];
                        }
                        break;
                    }
                }

                if (localMaxIdx.Count > 0) return 1 / (idMax * timeStep);
                else return 0;
            }



            #endregion

            #region Autocorrelation

            /// <summary>Computes autocorrelation of audio at a given index</summary>
            /// <param name="audioReceived">Received audio data</param>
            /// <param name="idx">Index to compute autocorrelation at</param>
            public static double[] ComputeAutocorrelation(double[] audioReceived, int idx)
            {
                int autoCorrSamples = FFTSamples;


                double[] ans = new double[autoCorrSamples];
                double[] y = new double[2 * autoCorrSamples];

                if (idx < autoCorrSamples) idx = autoCorrSamples;
                int initIdx = idx - autoCorrSamples + 1;

                for (int i = idx; i >= initIdx; i--) y[(i - idx + autoCorrSamples - 1) << 1] = audioReceived[i];

                fft.FFT(y, true);

                for (int i = 0; i < autoCorrSamples; i++)
                {
                    y[i << 1] = y[i << 1] * y[i << 1] + y[(i << 1) + 1] * y[(i << 1) + 1];
                    y[(i << 1) + 1] = 0;
                }

                fft.FFT(y, false);

                double invY0 = 1 / y[0];
                for (int i = 0; i < autoCorrSamples; i++) ans[i] = y[i << 1] * invY0;

                return ans;
            }

            #endregion

            #region Jitter and shimmer
            /*Jitter and Shimmer Measurements for Speaker Recognition
              Mireia Farrús, Javier Hernando, Pascual Ejarque
              TALP Research Center, Department of Signal Theory and Communications
              Universitat Politècnica de Catalunya, Barcelona, Spain
              {mfarrus,javier,pascual}@gps.tsc.upc.edu*/

            /// <summary>Class to compute voice jitter and shimmer</summary>
            public static class JitterShimmer
            {
                //http://www.sciencedirect.com/science/article/pii/S2212017313002788
                //threshold to detect pathologies

                /// <summary>Computes variations of jitter. Answer: [0] (ms) - jitta pathology at 83.2 microseconds, [1](%) - jitt pathology at 1.04%, [2](%) - rap pathology at 0.68%, [3](%) ppq5 0.84</summary>
                /// <param name="v">Frequency vector, Hz. </param>
                /// <returns></returns>
                public static float[] ComputeJitter(float[] v)
                {
                    float[] ans = new float[4];

                    int N = v.Length;

                    float[] invV = new float[N];
                    for (int k = 0; k < N; k++) invV[k] = 1.0f / v[k];


                    float avg = 0;
                    for (int k = 0; k < N; k++) avg += invV[k];
                    avg /= (float)N;

                    float sumJitta = 0;
                    for (int k = 0; k < N - 1; k++) sumJitta += Math.Abs(invV[k + 1] - invV[k]);
                    sumJitta /= (float)(N - 1);

                    float sumRap = 0;
                    for (int k = 1; k < N - 1; k++) sumRap += Math.Abs(invV[k] - 0.33333333f * (invV[k - 1] + invV[k] + invV[k + 1]));
                    sumRap /= (float)(N - 2);

                    float sumppq5 = 0;
                    for (int k = 2; k < N - 2; k++) sumppq5 += Math.Abs(invV[k] - 0.2f * (invV[k - 2] + invV[k - 1] + invV[k] + invV[k + 1] + invV[k + 2]));
                    sumppq5 /= (float)(N - 4);


                    ans[0] = sumJitta * 1e6f;
                    ans[1] = 100.0f * sumJitta / avg;
                    ans[2] = 100.0f * sumRap / avg;
                    ans[3] = 100.0f * sumppq5 / avg;

                    return ans;
                }


                /// <summary>Computes variations of shimmer. [0] (dB) ShdB pathology at 0.35 dB, [1] % Shim pathology at 3.81%, [2] % apq3 [3] % apq5 [4] % apq11 3.07%</summary>
                /// <param name="v">Log-amplitude, dB</param>
                /// <returns></returns>
                public static float[] ComputeShimmer(float[] v)
                {
                    float[] ans = new float[5];
                    int N = v.Length;

                    float[] expV = new float[N];
                    for (int k = 0; k < N; k++) expV[k] = (float)Math.Pow(10, v[k] * 0.05f);

                    float avg = 0;
                    for (int k = 0; k < N; k++) avg += expV[k];
                    avg /= (float)N;

                    float sumShdb = 0;
                    for (int k = 0; k < N - 1; k++) sumShdb += Math.Abs(20.0f * (float)Math.Log10(expV[k + 1] / expV[k]));
                    sumShdb /= (float)(N - 1);


                    float sumShim = 0;
                    for (int k = 0; k < N - 1; k++) sumShim += Math.Abs(expV[k + 1] - expV[k]);
                    sumShim /= (float)(N - 1);


                    float sumapq3 = 0;
                    for (int k = 1; k < N - 1; k++) sumapq3 += Math.Abs(expV[k] - 0.33333333f * (expV[k - 1] + expV[k] + expV[k + 1]));
                    sumapq3 /= (float)(N - 2);

                    float sumapq5 = 0;
                    for (int k = 2; k < N - 2; k++) sumapq5 += Math.Abs(expV[k] - 0.2f * (expV[k - 2] + expV[k - 1] + expV[k] + expV[k + 1] + expV[k + 2]));
                    sumapq5 /= (float)(N - 4);


                    float sumapq11 = 0;
                    for (int k = 5; k < N - 5; k++) sumapq11 += Math.Abs(expV[k] - 0.0909090909f * (expV[k - 5] + expV[k - 4] + expV[k - 3] + expV[k - 2] + expV[k - 1] + expV[k] + expV[k + 5] + expV[k + 4] + expV[k + 3] + expV[k + 2] + expV[k + 1]));
                    sumapq11 /= (float)(N - 10);

                    ans[0] = sumShdb;
                    ans[1] = 100 * sumShim / avg;
                    ans[2] = 100 * sumapq3 / avg;
                    ans[3] = 100 * sumapq5 / avg;
                    ans[4] = 100 * sumapq11 / avg;

                    return ans;
                }


            }

            #endregion

            #region Save

            /// <summary>Saves part of this to a stream</summary>
            /// <param name="t0">Initial time</param>
            /// <param name="tf">Final time</param>
            /// <param name="stream">Stream to save to</param>
            public void SavePart(int id0, int idf, System.IO.Stream stream)
            {
                if (id0 < 0) id0 = 0;

                if (idf >= audioReceived.Count - 1) idf = audioReceived.Count - 1;
                
                using (WaveFileWriter wfw = new WaveFileWriter(stream, new WaveFormat(waveSource.WaveFormat.SampleRate, 16, 1)))
                {
                    //float[] samples = new float[idf - id0 + 1];
                    for (int i = id0; i <= idf; i++) wfw.WriteSample((float)(0.000033f * this.audioReceived[i]));// samples[i - id0] = (float)(0.0005f * this.audioData[i]);

                    //wfw.WriteSamples(samples, 0, samples.Length);
                }
            }

            #endregion
        }

        #endregion

        #region Visualization bitmap - frequencies, bitmap for audio graph - OBSOLETE mainly used to generate scale and color functions

        /// <summary>Returns Y scaling for intensity (graph goes from -YScale to YScale)</summary>
        public double GetBMPIntensityYScale
        {
            get
            {
                double xmax = double.MinValue;
                for (int k = 0; k < audioData.Length; k++) xmax = Math.Max(xmax, Math.Abs(audioData[k]));
                return xmax;
            }
        }

        /// <summary>Retrieves a bitmap that represents the intensity of the audio</summary>
        public Bitmap GetBMPIntensity()
        {
            //total number of points
            int nFFTs = audioData.Length / NumPtsToSkipInFFT;

            Bitmap bmp = new Bitmap(nFFTs, BITMAPHEIGHTS);

            Graphics g = Graphics.FromImage(bmp);

            DrawIntensity(g, nFFTs, BITMAPHEIGHTS);

            return bmp;
        }

        /// <summary>Draws intensity graph</summary>
        /// <param name="g">Graphics element to draw to</param>
        /// <param name="H">Width</param>
        /// <param name="W">Height</param>
        public void DrawIntensity(Graphics g, int W, int H)
        {
            double ymax = GetBMPIntensityYScale;

            Pen p = new Pen(Color.Blue, 1);
            PlotToBmp(g, time, audioData, 0, time[time.Length - 1], -ymax, ymax, p, W, H);
        }
        /// <summary>Draws intensity graph</summary>
        /// <param name="g">Graphics element to draw to</param>
        /// <param name="H">Width</param>
        /// <param name="W">Height</param>
        /// <param name="idx0">Initial index</param>
        /// <param name="idxF">Final index</param>
        public void DrawIntensity(Graphics g, int W, int H, int idx0, int idxF)
        {
            double ymax = GetBMPIntensityYScale;

            Pen p = new Pen(Color.Blue, 1);
            PlotToBmp(g, time, audioData, 0, time[time.Length - 1], -ymax, ymax, p, W, H, idx0, idxF);
        }

        /// <summary>Plot fundamental frequency</summary>
        /// <param name="g">Graphic object to plot to</param>
        /// <param name="W">Image width</param>
        /// <param name="H">Image height</param>
        /// <param name="SpectrePercentMax">% of maximum keep frequency to plot [0 to 1]</param>
        public void PlotF0(Graphics g, int W, int H, float SpectrePercentMax)
        {
            PlotToBmp(g, this.baseFreqsTimes.ToArray(), this.baseFreqsF0.ToArray(), 0, this.baseFreqsTimes[baseFreqsTimes.Count - 1], 0, maxKeepFreq * SpectrePercentMax, Pens.Blue, W, H);
        }

        /// <summary>Intensity plot minimum and maximum</summary>
        public double IntensityPlotYMin, IntensityPlotYMax;

        /// <summary>Plot power of audio</summary>
        /// <param name="g">Graphic object to plot to</param>
        /// <param name="W">Image width</param>
        /// <param name="H">Image height</param>
        public void PlotPower(Graphics g, int W, int H)
        {
            IntensityPlotYMin = float.MaxValue;//baseFreqsIntensity[1];
            IntensityPlotYMax = float.MinValue;
            for (int k = 0; k < baseFreqsIntensity.Count; k++)
            {
                if (!float.IsInfinity(baseFreqsIntensity[k]))
                {
                    IntensityPlotYMin = Math.Min(baseFreqsIntensity[k], IntensityPlotYMin);
                    IntensityPlotYMax = Math.Max(baseFreqsIntensity[k], IntensityPlotYMax);
                }
                else baseFreqsIntensity[k] = 0;
            }
            IntensityPlotYMax *= 1.4f;
            baseFreqsIntensity[0] = (float)IntensityPlotYMin;
            PlotToBmp(g, this.baseFreqsTimes.ToArray(), this.baseFreqsIntensity.ToArray(), 0, this.baseFreqsTimes[baseFreqsTimes.Count - 1], IntensityPlotYMin, IntensityPlotYMax, Pens.Purple, W, H);
        }

        /// <summary>Plots a curve into a graphic element</summary>
        /// <param name="g">Graphic element</param>
        /// <param name="x">x points to plot</param>
        /// <param name="y">y points to plot</param>
        /// <param name="xmin">Minimum value of x</param>
        /// <param name="xmax">Maximum value of x</param>
        /// <param name="ymin">Minimum value of y, bottom of image</param>
        /// <param name="ymax">Maximum value of y, top of image</param>
        /// <param name="p">Pen to use</param>
        /// <param name="W">Image width</param>
        /// <param name="H">Image height</param>
        private static void PlotToBmp(Graphics g, float[] x, float[] y, double xmin, double xmax, double ymin, double ymax, Pen p, int W, int H)
        {
            PlotToBmp(g, x, y, xmin, xmax, ymin, ymax, p, W, H, 0, x.Length - 1);
        }

        /// <summary>Plots a curve into a graphic element</summary>
        /// <param name="g">Graphic element</param>
        /// <param name="x">x points to plot</param>
        /// <param name="y">y points to plot</param>
        /// <param name="xmin">Minimum value of x</param>
        /// <param name="xmax">Maximum value of x</param>
        /// <param name="ymin">Minimum value of y, bottom of image</param>
        /// <param name="ymax">Maximum value of y, top of image</param>
        /// <param name="p">Pen to use</param>
        /// <param name="W">Image width</param>
        /// <param name="H">Image height</param>
        /// <param name="idx0">Initial index of x to plot</param>
        /// <param name="idxF">Final index of x to plot</param>
        public static void PlotToBmp(Graphics g, float[] x, float[] y, double xmin, double xmax, double ymin, double ymax, Pen p, int W, int H, int idx0, int idxF)
        {
            if (idx0 < 0) idx0 = 0;
            if (idxF >= x.Length) idxF = x.Length - 1;

            int skip = 0;
            //int PlotMaxPts = 20 * MAXFFTCurvePts;
            //if (idxF - idx0 + 1 > PlotMaxPts)
            //{
            //    skip = (idxF - idx0 + 1) / PlotMaxPts;
            //}

            PointF[] pts = new PointF[(idxF - idx0 + 1) / (1 + skip)];
            idxF = pts.Length + idx0 - 1;
            double xScale = 1.0 / (xmax - xmin);
            double yScale = 1.0 / (ymax - ymin);
            for (int k = idx0; k <= idxF; k++)
            {
                pts[k - idx0].X = (float)((x[k*(1+skip)] - xmin) * xScale * (float)W);
                pts[k - idx0].Y = H - (float)((y[k*(1+skip)] - ymin) * yScale * (float)H);
            }

            g.DrawLines(p, pts);
        }

        /// <summary>Retrieves a bitmap that represents the spectre of the audio</summary>
        /// <param name="useColor">Use color or black and white?</param>
        /// <param name="percentGraph">Percentage of maximum frequency to go to</param>
        public Bitmap GetBMPSpectre(bool useColor, float percentGraph)
        {
            if (this.audioSpectre == null) RecomputeSpectre();
            return buildBmp(this.audioSpectre, useColor, percentGraph);
        }

        /// <summary>Build bitmap representation for frequency map</summary>
        /// <param name="vals">Intensity values</param>
        /// <param name="useColor">Use color or black and white?</param>
        /// <param name="percentGraph">Percentage of maximum frequency to go to</param>
        /// <returns></returns>
        private static Bitmap buildBmp(List<float[]> vals, bool useColor, float percentGraph)
        {
            int W = vals.Count;
            int H = (int)(vals[0].Length * percentGraph);
            if (H <= 0) H = 0;

            Bitmap Bmp = new Bitmap(W, H);

            double min = double.MaxValue;
            double max = double.MinValue;
            double mean = 0;
            double stdDev = 0;

            for (int k = 0; k < vals.Count; k++)
            {
                for (int j = 0; j < vals[k].Length; j++)
                {
                    mean += vals[k][j];
                    min = Math.Min(min, vals[k][j]);
                    max = Math.Max(max, vals[k][j]);
                }
            }
            mean /= ((double)vals.Count*(double)vals[0].Length);
            for (int k = 0; k < vals.Count; k++)
            {
                for (int j = 0; j < vals[k].Length; j++)
                {
                    stdDev += (vals[k][j] - mean) * (vals[k][j] - mean);
                }
            }
            stdDev = Math.Sqrt(stdDev / ((double)vals.Count * (double)vals[0].Length - 1));
            min = mean - 3 * stdDev;
            //max = mean + 3 * stdDev;

            ProcessUsingLockbits(Bmp, min, max, vals, useColor);

            return Bmp;
        }

        /// <summary>Add markers to a graph</summary>
        /// <param name="g">Graph to add graph to</param>
        /// <param name="dimension">Dimention in use: height or width</param>
        /// <param name="nMarks">Number of marks to add</param>
        /// <param name="min">Minimum value in graph</param>
        /// <param name="max">Maximum value in graph</param>
        /// <param name="offsetX">X offset in pixels</param>
        /// <param name="unit">Unit string</param>
        public static void AddMarkers(Graphics g, int dimension, int nMarks, double min, double max, string unit, int offsetX)
        {
            double stepfreq = (max - min) / nMarks;
            double curVal = min;

            double step = (double)dimension / nMarks;
            int curY = dimension - 1;

            for (int k = 0; k < nMarks; k++)
            {
                Pen p = new Pen(Brushes.Red, 3);
                g.DrawLine(p, offsetX, curY, offsetX + 10, curY);
                g.DrawString(Math.Round(curVal).ToString() + unit, new Font("Arial", 10), Brushes.Red, offsetX + 12, curY - 7);

                curY -= (int)step;
                curVal += stepfreq;
            }
        }


        private static void ProcessUsingLockbits(Bitmap processedBitmap, double min, double max, List<float[]> vals, bool useColor)
        {
            int W = processedBitmap.Width;
            int H = processedBitmap.Height;
            BitmapData bitmapData = processedBitmap.LockBits(new Rectangle(0, 0, processedBitmap.Width, processedBitmap.Height), ImageLockMode.ReadWrite, processedBitmap.PixelFormat);

            int bytesPerPixel = Bitmap.GetPixelFormatSize(processedBitmap.PixelFormat) / 8;
            int byteCount = bitmapData.Stride * processedBitmap.Height;
            byte[] pixels = new byte[byteCount];
            IntPtr ptrFirstPixel = bitmapData.Scan0;
            Marshal.Copy(ptrFirstPixel, pixels, 0, pixels.Length);
            int heightInPixels = bitmapData.Height;
            int widthInBytes = bitmapData.Width * bytesPerPixel;

            int xx, yy;
            yy = 0;
            for (int y = 0; y < heightInPixels; y++)
            {
                int currentLine = y * bitmapData.Stride;
                xx = 0;
                for (int x = 0; x < widthInBytes; x = x + bytesPerPixel)
                {
                    //int oldBlue = pixels[currentLine + x];
                    //int oldGreen = pixels[currentLine + x + 1];
                    //int oldRed = pixels[currentLine + x + 2];

                    float[] rgb = useColor ? FcnGetColor((float)vals[xx][H - 1 - yy], (float)min, (float)max) : FcnGetBWColor((float)vals[xx][H - 1 - yy], (float)min, (float)max);


                    // calculate new pixel value
                    pixels[currentLine + x] = (byte)(rgb[2] * 255.0f);
                    pixels[currentLine + x + 1] = (byte)(rgb[1] * 255.0f);
                    pixels[currentLine + x + 2] = (byte)(rgb[0] * 255.0f);
                    pixels[currentLine + x + 3] = 255;// (byte)(rgb[0] * 255.0f);

                    xx++;
                }
                yy++;
            }

            // copy modified bytes back
            Marshal.Copy(pixels, 0, ptrFirstPixel, pixels.Length);
            processedBitmap.UnlockBits(bitmapData);
        }

        /// <summary>Retrieves black and white color from intensity map</summary>
        public static float[] FcnGetBWColor(float t, float hMin, float hMax)
        {
            if (t < hMin) t = hMin;
            if (t > hMax) t = hMax;

            float zRel = 1.0f - (t - hMin) / (hMax - hMin);

            return new float[] { zRel, zRel, zRel, 1.0f };
        }

        /// <summary>Retrieves color from intensity map</summary>
        public static float[] FcnGetColor(float t, float hMin, float hMax)
        {
            if (t < hMin) t = hMin;
            if (t > hMax) t = hMax;

            float invMaxH = 1.0f / (hMax - hMin);
            float zRel = (t - hMin) * invMaxH;

            float cR = 0, cG = 0, cB = 0;


            if (0 <= zRel && zRel < 0.2f)
            {
                cB = 1.0f;
                cG = zRel * 5.0f;
            }
            else if (0.2f <= zRel && zRel < 0.4f)
            {
                cG = 1.0f;
                cB = 1.0f - (zRel - 0.2f) * 5.0f;
            }
            else if (0.4f <= zRel && zRel < 0.6f)
            {
                cG = 1.0f;
                cR = (zRel - 0.4f) * 5.0f;
            }
            else if (0.6f <= zRel && zRel < 0.8f)
            {
                cR = 1.0f;
                cG = 1.0f - (zRel - 0.6f) * 5.0f;
            }
            else
            {
                cR = 1.0f - (zRel - 0.8f) * 5.0f;
                //cG = (zRel - 0.8f) * 5.0f;
                //cB = cG;
            }

            return new float[] { cR, cG, cB, 1.0f };
        }

        #endregion

        #region Play audio

        /// <summary>Waveout to simple audio replay</summary>
        WaveOut wo;
        /// <summary>Play sampler for this audio</summary>
        SampleAudio.PlaySample ps;

        /// <summary>Simple audio play - all audio</summary>
        public PlaySample Play()
        {
            return Play(time[0], time[time.Length - 1]);
        }

        /// <summary>Simple audio play sound from t0 to tf. Returns playSample to handle events</summary>
        /// <param name="t0">Initial time</param>
        /// <param name="tf">End time</param>
        public PlaySample Play(double t0, double tf)
        {
            //do not replay simultaneously
            if (wo != null) return null;

            t0 = Math.Max(t0, time[0]);
            tf = Math.Min(tf, time[time.Length - 1]);

            ps = new SampleAudio.PlaySample(this, t0, tf);
            ps.ReplaySpeed = 1;

            wo = new WaveOut();
            wo.PlaybackStopped += new EventHandler<StoppedEventArgs>(wo_PlaybackStopped);
            wo.Init(ps);
            wo.Play();

            return ps;

        }
        /// <summary>Adjust volume of this play</summary>
        /// <param name="vol">Desired volume. 1.0f = 100%</param>
        public void SetVolume(float vol)
        {
            if (ps != null) ps.intensMultiplier = vol;
        }

        /// <summary>Stop all playback</summary>
        public void Stop()
        {
            if (wo != null) wo.Stop();
        }
        void wo_PlaybackStopped(object sender, StoppedEventArgs e)
        {
            wo.Dispose();
            wo = null;
        }

        /// <summary>Plays recorded samples</summary>
        public class PlaySample : WaveProvider32
        {
            /// <summary>Multiply intensity by this factor, to increase or decrease volume</summary>
            public float intensMultiplier = 1.0f;

            int sample;

            SampleAudio sa;
            double t0, tf;

            /// <summary>Replay speed</summary>
            public double ReplaySpeed = 1.0;

            /// <summary>Current playtime</summary>
            public double t;

            /// <summary>Set playtime</summary>
            /// <param name="tSet">Time to set</param>
            public void SetTime(double tSet)
            {
                sample = (int)(tSet * (double)WaveFormat.SampleRate * ReplaySpeed);
            }

            /// <summary>Callback delegate for actions on replay time t</summary>
            /// <param name="time">Current play time</param>
            public delegate void PlayingAtTime(double time);

            /// <summary>Delegate to call</summary>
            public PlayingAtTime PlayTimeFunc;

            /// <summary>Creates a new sample player</summary>
            /// <param name="sa">Audio to play</param>
            /// <param name="t0">Start time</param>
            /// <param name="tf">Stop time</param>
            public PlaySample(SampleAudio sa, double t0, double tf)
            {
                this.t = 0;
                this.sa = sa;
                this.t0 = t0;
                this.tf = tf;
            }

            /// <summary>Plays sample</summary>
            public override int Read(float[] buffer, int offset, int count)
            {
                int sampleRate = WaveFormat.SampleRate;

                if (t + t0 > tf) return 0;
                List<float> b2 = new List<float>();

                for (int n = 0; n < count; n++)
                {
                    t = (double)sample / (double)sampleRate * ReplaySpeed;
                    if (PlayTimeFunc != null) PlayTimeFunc(t + t0);


                    float intens;
                    if (t + t0 > tf) intens = 0; //stop at exact time
                    else intens = intensMultiplier * (float)(0.0001f * sa.GetIntensAtTime(t + t0));//(float)(0.1 * Math.Sin((2 * Math.PI * 1000 * t)));

                    b2.Add(intens);
                    buffer[n + offset] = intens;

                    sample++;
                    //if (sample > sampleRate) sample = 0;
                }

                //if (sw.ElapsedMilliseconds > 5000) return 0;

                return count;
            }
        }

        #endregion

        #region Annotations and Save to file

        /// <summary>Recompute annotations position when a part of the audio is deleted</summary>
        /// <param name="t0">Initial delete time</param>
        /// <param name="tf">Final delete time</param>
        public void RecomputeAnnotationsDelPart(float t0, float tf)
        {
            for (int k = 0; k < Annotations.Count; k++)
            {
                //entire deletion before comment
                if (Annotations[k].startTime > tf && Annotations[k].stopTime > tf)
                {
                    Annotations[k].startTime -= tf - t0;
                    Annotations[k].stopTime -= tf - t0;
                }
                else if (Annotations[k].startTime <= tf && tf <= Annotations[k].stopTime && t0 <= Annotations[k].startTime)
                {
                    Annotations[k].startTime = t0; //t0 will be the new tf
                    Annotations[k].stopTime -= tf - t0;
                }
                else if (Annotations[k].startTime <= t0 && tf <= Annotations[k].stopTime)
                {
                    Annotations[k].stopTime -= tf - t0;
                }
                else if (Annotations[k].startTime <= t0 && t0 <= Annotations[k].stopTime && Annotations[k].startTime <= tf)
                {
                    Annotations[k].stopTime = t0;
                }
                else if (t0 <= Annotations[k].startTime && Annotations[k].stopTime <= tf)
                {
                    Annotations.RemoveAt(k);
                    k--;
                }
            }
        }

        /// <summary>Audio annotations</summary>
        public List<AudioAnnotation> Annotations = new List<AudioAnnotation>();
        /// <summary>Audio annotations</summary>
        public class AudioAnnotation
        {
            /// <summary>Audio annotation</summary>
            /// <param name="Text">Annotation text</param>
            /// <param name="startTime">Annotation start time, in seconds</param>
            /// <param name="stopTime">Annotation stop time, in seconds</param>
            public AudioAnnotation(string Text, double startTime, double stopTime)
            {
                if (startTime > stopTime) throw new Exception("stopTime must be greater than startTime");
                this.Text = Text;
                this.startTime = startTime;
                this.stopTime = stopTime;
            }

            /// <summary>Annotation text</summary>
            public string Text;
            /// <summary>Annotation start time, in seconds</summary>
            public double startTime;
            /// <summary>Annotation stop time, in seconds</summary>
            public double stopTime;

            /// <summary>Font if going to annotate inside spectrogram</summary>
            public Font annFont = new Font("Arial", 44);
            /// <summary>Show in spectrogram?</summary>
            public bool ShowInSpectrogram = true;
            /// <summary>Annotation start frequency, in Hz</summary>
            public double startFreq = 200;
            /// <summary>Annotation stop time, in Hz</summary>
            public double stopFreq = 1200;

            /// <summary>Converts a time to a string representation hh:mm:ss,MMM -> MMM milliseconds</summary>
            /// <param name="time">Time to convert</param>
            /// <returns></returns>
            private static string time2string(double time)
            {
                int hours = (int)(time / 3600.0); time -= (double)hours * 3600.0;
                int mins = (int)(time / 60.0); time -= (double)mins*60.0;
                int secs = (int)time; time -= secs;
                int millisecs = (int)(1000.0 * time);

                return hours.ToString().PadLeft(2, '0') + ":" + mins.ToString().PadLeft(2, '0') + ":" + secs.ToString().PadLeft(2, '0') + "," + millisecs.ToString().PadLeft(3, '0');
            }

            /// <summary>Converts a string representation hh:mm:ss,MMM -> MMM milliseconds into time (s)</summary>
            /// <param name="s">String to convert</param>
            private static double string2time(string s)
            {
                string[] ss = s.Split(new string[] { ":", "," }, StringSplitOptions.RemoveEmptyEntries);

                double h = double.Parse(ss[0]);
                double m = double.Parse(ss[1]);
                double sec = double.Parse(ss[2]);
                double ms = double.Parse(ss[3]);

                return 3600.0 * h + 60.0 * m + sec + 0.001 * ms;
            }

            /// <summary>String representation</summary>
            public override string ToString()
            {
                return time2string(startTime) + " --> " + time2string(stopTime) + "\r\n" + Text;
            }

            /// <summary>Saves annotations to file</summary>
            /// <param name="annotations">Annotations</param>
            /// <param name="file">File name</param>
            public static void SaveToFile(List<AudioAnnotation> annotations, string file)
            {
                int i = 1;
                string s = "";
                foreach (AudioAnnotation ann in annotations)
                {
                    if (i > 1) s += "\r\n\r\n";

                    s += i.ToString() + "\r\n"; i++;
                    s += time2string(ann.startTime) + " --> " + time2string(ann.stopTime) + "\r\n";

                    TypeConverter converter = TypeDescriptor.GetConverter(typeof(Font));
                    string fontString = converter.ConvertToString(ann.annFont);
                    s += ann.Text + "|||" + Math.Round(ann.startFreq) + "|||" + Math.Round(ann.stopFreq) + "|||" + fontString + "|||" + ann.ShowInSpectrogram.ToString() + "|||";
                }

                System.IO.File.WriteAllText(file, s, Encoding.UTF8);
            }

            /// <summary>Reads annotations from file</summary>
            /// <param name="File">File to read</param>
            public static List<AudioAnnotation> ReadFromFile(string File)
            {
                string[] s = System.IO.File.ReadAllText(File, Encoding.UTF8).Split(new string[] { "\r\n" }, StringSplitOptions.None);
                List<AudioAnnotation> lstAnn = new List<AudioAnnotation>();

                int subtitleId = 1;
                for (int k = 0; k < s.Length; k++)
                {
                    int subTitleNumber = int.Parse(s[k]);
                    if (subTitleNumber != subtitleId) throw new Exception("Error in subtitle file: line " + k.ToString());
                    k++; subtitleId++;

                    double t0, tf;
                    string[] startStopTime = s[k].Split(new string[] { " ", "-->" }, StringSplitOptions.RemoveEmptyEntries);
                    t0 = string2time(startStopTime[0]);
                    tf = string2time(startStopTime[1]);
                    k++;

                    string txt = "";
                    while (k < s.Length && s[k].Trim() != "")
                    {
                        txt += s[k] + "\r\n";
                        k++;
                    }
                    while (k + 1 < s.Length && s[k + 1].Trim() == "") k++;

                    //retrieve start frequency, stop frequency and font if available
                    double f0 = 0, ff = 0;
                    Font fAnn = null;
                    bool showInSpectrogram = false;
                    TypeConverter converter = TypeDescriptor.GetConverter(typeof(Font));

                    string[] dataTXT = txt.Split(new string[] { "|||" }, StringSplitOptions.RemoveEmptyEntries);
                    if (dataTXT.Length > 1)
                    {
                        try
                        {
                            txt = dataTXT[0];

                            if (dataTXT.Length > 1) f0 = double.Parse(dataTXT[1]);
                            if (dataTXT.Length > 2) ff = double.Parse(dataTXT[2]);
                            if (dataTXT.Length > 3)
                            {
                                fAnn = (Font)converter.ConvertFromString(dataTXT[3]);
                            }
                            if (dataTXT.Length > 4) showInSpectrogram = (dataTXT[4] == (true).ToString());
                        }
                        catch
                        {
                        }
                    }

                    AudioAnnotation newAnn = new AudioAnnotation(txt, t0, tf);
                    if (f0 > 0) newAnn.startFreq = f0;
                    if (ff > 0) newAnn.stopFreq = ff;
                    if (fAnn != null) newAnn.annFont = fAnn;
                    newAnn.ShowInSpectrogram = showInSpectrogram;
                    lstAnn.Add(newAnn);
                }

                return lstAnn;
            }
        }

        /// <summary>Save audio to a separate file</summary>
        /// <param name="fileName">File to save to</param>
        public void SaveAll(string filename)
        {
            SavePart(filename, time[0], time[time.Length - 1]);
        }

        /// <summary>Save part of audio to a separate file</summary>
        /// <param name="fileName">File to save to</param>
        /// <param name="t0">Initial time</param>
        /// <param name="tf">Final time</param>
        public void SavePart(string fileName, double t0, double tf)
        {
            int id0 = (int)(t0 / time[1]);
            int idf = (int)(tf / time[1]);
            if (id0 < 0) id0 = 0;
            if (idf >= audioData.Length - 1) idf = audioData.Length - 1;

            using (WaveFileWriter wfw = new WaveFileWriter(fileName, new WaveFormat(this.waveFormat.SampleRate, 16, 1)))
            {
                //float[] samples = new float[idf - id0 + 1];
                for (int i = id0; i <= idf; i++) wfw.WriteSample((float)(0.00003f * this.audioData[i]));// samples[i - id0] = (float)(0.0005f * this.audioData[i]);

                //wfw.WriteSamples(samples, 0, samples.Length);
            }

            this.AudioFile = fileName;
            try
            {
                SaveAnnotations();
            }
            catch { }
        }

        #endregion

        #region Filtering and denoising

        /// <summary>Applies mean filter to audio - neighbor mean</summary>
        /// <param name="timeWindow">Time window to filter</param>
        public void FilterMean(double timeWindow)
        {
            //integral of signal
            float[] audioIntegral = new float[audioData.Length];
            audioIntegral[0]=audioData[0];
            for (int k = 1; k < audioIntegral.Length; k++) audioIntegral[k] = audioIntegral[k - 1] + audioData[k];


            int windowSize = (int)(0.5*timeWindow / this.time[1]);
            if (windowSize < 1) windowSize = 1;

            float w = 1.0f / (2f * windowSize + 1f);
            for (int k = windowSize; k < audioData.Length - windowSize; k++)
            {
                audioData[k] = w * (audioIntegral[k + windowSize] - audioIntegral[k - windowSize]);
            }
        }

        /// <summary>Applies median filter to audio</summary>
        public void FilterMedian(double timeWindow)
        {
            int windowSize = (int)(0.5*timeWindow / this.time[1]);
            if (windowSize < 1) windowSize = 1;

            double w = 1.0 / (2 * windowSize + 1);
            List<float> vals = new List<float>();
            for (int k = windowSize; k < audioData.Length - windowSize; k++)
            {
                vals.Clear();
                for (int i = -windowSize; i <= windowSize; i++) vals.Add(audioData[k+i]);
                vals.Sort();
                audioData[k] = vals[windowSize];
            }
        }

        /// <summary>Apply median/average filter to spectrogram</summary>
        /// <param name="wSizeT">Window size in time direction</param>
        /// <param name="wSizeF">Window size in frequency direction</param>
        public void MedianFilterSpectrogram(int wSizeT, int wSizeF, bool median)
        {
            if (audioSpectre == null) RecomputeSpectre();

            int W = audioSpectre.Count;
            int H = audioSpectre[0].Length;

            for (int i = wSizeF; i < H - wSizeF - 1; i++)
            {
                for (int j = wSizeT; j < W - wSizeT - 1; j++)
                {
                    List<float> vals = new List<float>();
                    for (int ii = -wSizeF; ii <= wSizeF; ii++)
                        for (int jj = -wSizeT; jj <= wSizeT; jj++) vals.Add(audioSpectre[j + jj][i + ii]);

                    if (median)
                    {
                        vals.Sort();
                        audioSpectre[j][i] = vals[vals.Count >> 1];
                    }
                    else audioSpectre[j][i] = vals.Average();
                }
            }
        }

        #endregion

        #region Audio edit - delete, change volume

        /// <summary>Deletes audio from t0 to tf</summary>
        /// <param name="t0">Start time to delete (s)</param>
        /// <param name="tf">End time to delete (s)</param>
        public void Delete(double t0, double tf)
        {
            float tstep = time[1];
            int idx0 = (int)(t0 / tstep);
            int idxf = (int)(tf / tstep);

            List<float> lstAudioData = new List<float>();
            for (int k = 0; k < audioData.Length; k++) if (!(idx0 <= k && k <= idxf)) lstAudioData.Add(audioData[k]);

            time = new float[lstAudioData.Count];
            for (int k = 0; k < time.Length; k++) time[k] = k * tstep;

            audioData = lstAudioData.ToArray();
        }

        /// <summary>Changes audio volume</summary>
        /// <param name="t0">Start time to change (s)</param>
        /// <param name="tf">End time to change (s)</param>
        /// <param name="multiplier">Intensity multiplier</param>
        public void ChangeVolume(double t0, double tf, float multiplier)
        {
            double tstep = time[1];
            int idx0 = (int)(t0 / tstep);
            int idxf = (int)(tf / tstep);

            for (int k = idx0; k <= idxf; k++)
            {
                audioData[k] *= multiplier;
            }
        }

        #endregion

        /// <summary>Feature extraction for machine learning</summary>
        /// <param name="t0">Initial time</param>
        /// <param name="tf">Final time</param>
        /// <param name="scaleIntesities">Autoscale intensities?</param>
        public List<float[]> ExtractFeatures(double t0, double tf, bool scaleIntensities)
        {
            double tstep = time[1];
            int idx0 = (int)(t0 / tstep);
            int idxf = (int)(tf / tstep);
            double timeStep;
            return AudioIdentifRecog.ExtractFeatures(this.audioData, idx0, idxf, this.waveFormat.SampleRate, SampleAudio.FFTSamples, out timeStep, scaleIntensities);
        }
    }
}
