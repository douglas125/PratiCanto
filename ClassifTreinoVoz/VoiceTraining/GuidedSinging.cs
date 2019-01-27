using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AudioComparer;
using NAudio.Wave;
using System.Diagnostics;
using System.Drawing;

namespace ClassifTreinoVoz
{
    /// <summary>Guided singing class. Audio sythesis, recording and analyses</summary>
    public class GuidedSinging
    {
        /// <summary>Microphone to use</summary>
        public int MicID;

        /// <summary>Playing guided singing?</summary>
        public bool isPlayingGuidedSinging = false;

        /// <summary>Delegate of function to be called when playback stops</summary>
        public delegate void StopFunc();

        /// <summary>Function to be called when playback stops. delegate void StopFunc();</summary>
        public StopFunc stopFunc;

        /// <summary>Constructor</summary>
        /// <param name="micID">Microphone to use</param>
        /// <param name="stopFunc">Function to call when playback stops</param>
        /// <param name="ssmelody">Melody to use in the guided singing</param>
        public GuidedSinging(int micID, StopFunc stopFunc, SoundSynthesis ssmelody)
        {
            this.MicID = micID;
            this.stopFunc = stopFunc;
            this.ssMelody = ssmelody;
        }

        /// <summary>Sound synthesizer for desired melody</summary>
        public SoundSynthesis ssMelody;

        /// <summary>Current recording</summary>
        public SampleAudio.RealTime rtSingRecord = new SampleAudio.RealTime();

        /// <summary>Track play time</summary>
        public double trackPlayTime;


        /// <summary>Index in rtSingRecord of start of each recording sequence</summary>
        List<int> sequenceStartIdxs;
        /// <summary>Names of sequences</summary>
        List<string> sequenceNames;


        /// <summary>Number of points acquired in this stance</summary>
        public int nAcqPts;
        /// <summary>Number of points accepted in this stance</summary>
        public int nAcceptedPts;

        /// <summary>Wave player for test</summary>
        public WaveOut woGuide;


        /// <summary>Stopwatch to measure time</summary>
        System.Diagnostics.Stopwatch swGuidedWatch = new Stopwatch();

        #region Start/stop/pause singing

        /// <summary>Start guided singing of melody, without modulation</summary>
        /// <param name="seqName">Name of this sequence</param>
        public void StartGuidedSinging(string seqName)
        {
            if (ssMelody.Notes[0].Count == 0) return;
            StartGuidedSinging(SoundSynthesis.Note.GetHalfStepsAboveA4(ssMelody.Notes[0][0].Frequency[0]), seqName);
        }

        /// <summary>Clear data from recordings</summary>
        public void ClearData()
        {
            if (rt != null)
            {
                rt.audioReceived.Clear();
                rt.waveSource.Dispose();
            }
            if (rtSingRecord != null && rtSingRecord.audioReceived != null)
            {
                rtSingRecord.audioReceived.Clear();
                rtSingRecord.waveSource.Dispose();
            }
            

        }

        /// <summary>Start guided singing of melody</summary>
        /// <param name="modulationHalfSteps">Frequency of first melody note in halfsteps above A4</param>
        /// <param name="seqName">Name of this sequence</param>
        public void StartGuidedSinging(double modulationHalfSteps, string seqName)
        {
            if (ssMelody.Notes[0].Count == 0) return;
            //initializations
            if (!isPlayingGuidedSinging)
            {
                rtSingRecord = new SampleAudio.RealTime();
                rtSingRecord.StartRecording(this.MicID);
                sequenceStartIdxs = new List<int>();
                sequenceNames = new List<string>();

                isPlayingGuidedSinging = true;

                woGuide = new WaveOut();
                woGuide.Init(ssMelody.sampleSynthesizer);
                woGuide.PlaybackStopped += new EventHandler<StoppedEventArgs>(woTest_PlaybackStopped);
                
                isPaused = false;

                lstAcqFreqSamples.Clear();
                lstAcqIntensSamples.Clear();
                lstAnnMelodies.Clear();
            }

            ssMelody.ModulateSound(SoundSynthesis.Note.GetFrequency(modulationHalfSteps));
            ssMelody.sampleSynthesizer.Reset();

            //total play time of melody
            trackPlayTime = ssMelody.GetTrackPlayTime(0);

            ssMelody.sampleSynthesizer.Reset();

            //clear acquired points used for display, starts watch
            acqFreqSamples.Clear();
            acqIntensSamples.Clear();
            nAcceptedPts = 0;
            nAcqPts = 0;

            //store start index of this playback
            sequenceStartIdxs.Add(rtSingRecord.time.Count - 1);
            sequenceNames.Add(seqName + ssMelody.Notes[0][0].ToString().Split()[0]);

            if (isPaused) Resume();

            //will not resume if it was not playing anything
            if (woGuide.PlaybackState != PlaybackState.Playing)
            {
                woGuide.Play(); int kkk = 1;
                while (woGuide.PlaybackState != PlaybackState.Playing && kkk < 100000000)
                { kkk++; }
            }
            swGuidedWatch.Restart();


        }

        /// <summary>Retrieves % of playtime already played</summary>
        public double GetCurrentPlayTimePercent()
        {
            return swGuidedWatch.Elapsed.TotalSeconds / trackPlayTime;
        }

        //void woTest_PlaybackStopped(object sender, StoppedEventArgs e)
        //{
        //    ssMelody.sampleSynthesizer.Reset();
        //    woGuide.Play();
        //}


        void woTest_PlaybackStopped(object sender, StoppedEventArgs e)
        {
            //pauses playback
            Pause();

            //record frequency and intensity curves
            lstAnnMelodies.Add(GetAnnotationsFromMelody(ssMelody, 0));
            lstAcqFreqSamples.Add(copyList(acqFreqSamples));
            lstAcqIntensSamples.Add(copyList(acqIntensSamples));

            //calls user stopfunc -> may want to analyze data, save, etc
            stopFunc();
        }
        List<PointF> copyList(List<PointF> orig)
        {
            List<PointF> ans = new List<PointF>();
            foreach (PointF p in orig) ans.Add(p);
            return ans;
        }

        /// <summary>Stops guided singing</summary>
        public void Stop()
        {
            //sw.Reset();
            //testing = false;
            woGuide.Stop();
            rtSingRecord.StopRecording();
            isPlayingGuidedSinging = false;
            //testTimer.Enabled = false;
        }

        /// <summary>Is guided singing paused?</summary>
        public bool isPaused = false;

        /// <summary>Pause guided singing</summary>
        public void Pause()
        {
            if (!isPaused)
            {
                swGuidedWatch.Stop();
                rtSingRecord.PauseRecording();
                woGuide.Pause();
                isPaused = true;
            }
        }

        /// <summary>Resume guided singing</summary>
        public void Resume()
        {
            if (isPaused)
            {
                rtSingRecord.ResumeRecording();
                woGuide.Resume();
                int kkk = 1;
                while (woGuide.PlaybackState != PlaybackState.Playing && kkk < 100000000)
                { kkk++; }

                swGuidedWatch.Start();
                isPaused = false;
            }
        }

        #endregion

        #region Acquire sample of frequency/intensity

        /// <summary>Free singing? Set to true to accept any F0</summary>
        public bool FreeSinging = false;

        /// <summary>Intensity to consider as silence</summary>
        public double CutIntensity = 2;

        /// <summary>Frequency samples</summary>
        public List<PointF> acqFreqSamples = new List<PointF>();
        /// <summary>Intensity samples</summary>
        public List<PointF> acqIntensSamples = new List<PointF>();

        /// <summary>Melody strings</summary>
        public List<List<SampleAudio.AudioAnnotation>> lstAnnMelodies = new List<List<SampleAudio.AudioAnnotation>>();
        /// <summary>All acquired frequency samples</summary>
        public List<List<PointF>> lstAcqFreqSamples = new List<List<PointF>>();
        /// <summary>All acquired intensity samples</summary>
        public List<List<PointF>> lstAcqIntensSamples = new List<List<PointF>>();

        /// <summary>Last acquired frequency</summary>
        public float lastFreq = 0;
        /// <summary>Last acquired intensity</summary>
        public float lastIntens = 0;
        /// <summary>Last acquired time</summary>
        public float lastTime = 0;

        /// <summary>Acquire frequency/intensity sample</summary>
        public void AcquireFreqSample()
        {
            AcquireFreqSample(rtSingRecord.audioReceived.Count - 1);
        }

        /// <summary>Acquire frequency/intensity sample</summary>
        /// <param name="id">Get sample from what ID in rtSingRecord.audioReceived? </param>
        public void AcquireFreqSample(int id)
        {
            //retrieve relevant sound data
            int nElems = SampleAudio.FFTSamples;//2 * (int)(1.1 / (rtSingRecord.deltaT * 50));

            //avoid error
            if (id > rtSingRecord.audioReceived.Count - 1) id = rtSingRecord.audioReceived.Count - 1;

            if (rtSingRecord.audioReceived.Count > nElems)
            {
                float[] latestData = new float[nElems];

                for (int k = 0; k < nElems; k++) if (id - k >= 0) latestData[k] = rtSingRecord.audioReceived[id - k];

                float expectedf = (float)ssMelody.GetPlayFrequency((float)swGuidedWatch.Elapsed.TotalSeconds, 0);

                //current F0
                float curf = 0;
                float curI = 0;

                float stepFreq = (float)rtSingRecord.waveSource.WaveFormat.SampleRate / (float)SampleAudio.FFTSamples;
                
                List<LinearPredictor.Formant> formants;

                try
                {
                    float hnf;
                    curf = LinearPredictor.ComputeReinforcedFrequencies(latestData, stepFreq, latestData.Length - 1, 70, 1250, out formants, null, out hnf);
                    curI = (float)SampleAudio.RealTime.ComputeIntensity(latestData, rtSingRecord.deltaT, latestData.Length - 1, 70); //50);

                    //if (FreeSinging)
                    //{
                    //    //curf = (float)SampleAudio.RealTime.ComputeBaseFrequency(latestData, rtSingRecord.deltaT, latestData.Length - 1, 50);



                    //    curI = (float)SampleAudio.RealTime.ComputeIntensity(latestData, rtSingRecord.deltaT, latestData.Length - 1, 50);
                    //}
                    //else
                    //{
                    //    //curf = (float)SampleAudio.RealTime.ComputeBaseFrequency(latestData, rtSingRecord.deltaT, latestData.Length - 1, expectedf * 0.6f); //50);

                    //    curI = (float)SampleAudio.RealTime.ComputeIntensity(latestData, rtSingRecord.deltaT, latestData.Length - 1, expectedf * 0.6f); //50);
                    //}
                }
                catch 
                { 
                }

                //if (curf > 1500) curf = 1500;
                
                lastFreq = curf;
                lastIntens = curI;
                lastTime = (float)swGuidedWatch.Elapsed.TotalSeconds;

                //take into consideration microphone and processing delay
                float curt = (float)swGuidedWatch.Elapsed.TotalSeconds - 2.5f * nElems * (float)rtSingRecord.deltaT;

                float curIntens = (float)SampleAudio.RealTime.ComputeIntensity(latestData, rtSingRecord.deltaT, latestData.Length - 1, 50);//50);


                nAcqPts++;
                if (curIntens > CutIntensity && (Math.Abs(expectedf - curf) < 0.2f * expectedf || FreeSinging))
                {
                    ////compute 1st order filter
                    //if (acqFreqSamples.Count > 0)
                    //{
                    //    float deltaT = curt - acqFreqSamples[acqFreqSamples.Count - 1].X;
                    //    float a = deltaT / (0.005f + deltaT);
                    //    curf = (1.0f - a) * acqFreqSamples[acqFreqSamples.Count - 1].Y + a * curf;
                    //}

                    //acqFreqSamples.Add(new PointF(storeTime, curf));
                    acqFreqSamples.Add(new PointF(curt, curf));
                    acqIntensSamples.Add(new PointF(curt, curIntens));
                    nAcceptedPts++;


                }
                //very low intensity
                if (curIntens <= CutIntensity)
                {
                    acqFreqSamples.Add(new PointF(curt, 0));
                    acqIntensSamples.Add(new PointF(curt, curIntens));
                }
                //analyze free singing if in free singing mode
                if (FreeSinging) AnalyzeFreeSinging();
            }
        }

        #region Analyze free singing

        /// <summary>Tolerance in free singing to couple frequencies together</summary>
        public float FreeSingFreqTol = 0.2f;
        /// <summary>Smallest time duration in seconds to stick notes together in seconds</summary>
        public float FreeSingSmallestTimeDuration = 0.2f;

        /// <summary>Last free singing point which was analyzed</summary>
        int lastAnalyzedPt = 0;

        /// <summary>Candidate note for free singing identification</summary>
        public class CandidateNote
        {
            /// <summary>number of samples in this candidate</summary>
            public int nSamples = 0;

            /// <summary>average frequency in Hz</summary>
            public float meanFreq = 0;

            /// <summary>Start time of this candidate</summary>
            public float tStart = 0;

            /// <summary>End time of this candidate</summary>
            public float tEnd = 0;

            /// <summary>Identified note</summary>
            public SoundSynthesis.Note Note;
        }
        /// <summary>Current candidate</summary>
        public CandidateNote curCandidate = new CandidateNote();

        /// <summary>Sung notes</summary>
        public List<CandidateNote> FreeSingingNotes = new List<CandidateNote>();

        /// <summary>If in free singing mode, try to identify what the person is singing</summary>
        private void AnalyzeFreeSinging()
        {
            if (acqFreqSamples.Count < lastAnalyzedPt)
            {
                //restarted the guided singing - go back to beginning
                lastAnalyzedPt = 0;
                curCandidate = new CandidateNote();
                FreeSingingNotes.Clear();
            }

            lastAnalyzedPt = acqFreqSamples.Count - 1;
            PointF pt = acqFreqSamples[acqFreqSamples.Count-1];

            bool includeCurSample = Math.Abs(pt.Y - curCandidate.meanFreq) <= FreeSingFreqTol * curCandidate.meanFreq;

            if (curCandidate.nSamples == 0 || !includeCurSample)
            {
                if (curCandidate.tEnd - curCandidate.tStart > FreeSingSmallestTimeDuration)
                {
                    curCandidate.Note = new SoundSynthesis.Note(curCandidate.tEnd - curCandidate.tStart, curCandidate.meanFreq, new SoundSynthesis.WaveShapeSine());
                    FreeSingingNotes.Add(curCandidate);
                }
                //starting new candidate
                curCandidate = new CandidateNote();
                curCandidate.tStart = pt.X;
                curCandidate.tEnd = pt.X;
                curCandidate.nSamples = 1;
                curCandidate.meanFreq = pt.Y;
            }
            else if (includeCurSample)
            {
                curCandidate.nSamples++;
                curCandidate.meanFreq = (pt.Y + (float)(curCandidate.nSamples - 1) * curCandidate.meanFreq) / (float)curCandidate.nSamples;
                curCandidate.tEnd = pt.X;
            }

        }

        /// <summary>Transcribes free singing into a sequence of notes</summary>
        /// <param name="tempo">Tempo of the recording (s)</param>
        public List<string> TranscribeFreeSinging(double tempo)
        {
            List<string> notes = new List<string>();

            foreach (CandidateNote cn in FreeSingingNotes)
            {
                //adjust note duration into multiples of 0.125
                cn.Note.Duration = 0.25 * Math.Round(cn.Note.Duration * 4 / tempo);

                notes.Add(cn.Note.ToString());
            }

            return notes;
        }

        #endregion


        #endregion

        #region Save files

        /// <summary>Saves guided singing to target folder</summary>
        /// <param name="folder">Folder to save to</param>
        public void SaveGuidedSingingToFolder(string folder)
        {
            if (sequenceStartIdxs == null) return;
            
            sequenceStartIdxs[0] = 0;
            for (int i = 0; i < sequenceStartIdxs.Count; i++)
            {
                int idx0 = sequenceStartIdxs[i];
                int idxf;
                if (i == sequenceStartIdxs.Count - 1) idxf = rtSingRecord.time.Count - 1;
                else idxf = sequenceStartIdxs[i + 1];

                SampleAudio sa = new SampleAudio(rtSingRecord, idx0, idxf);
                sa.Annotations = lstAnnMelodies[i];
                string file = folder + "\\" + sequenceNames[i] + ".wav";
                sa.SavePart(file, sa.time[0], sa.time[sa.time.Length - 1]);
                sa.Dispose();

                //saves frequency and intensity graphs
                try
                {
                    System.IO.File.WriteAllText(file + "freq", GetPlotString(lstAcqFreqSamples[i]));
                    System.IO.File.WriteAllText(file + "intens", GetPlotString(lstAcqIntensSamples[i]));
                }
                catch
                {
                }
            }
        }

        /// <summary>Saves all guided singing to file</summary>
        /// <param name="file">File to save</param>
        public void SaveLastGuidedSinging(string file)
        {
            if (sequenceStartIdxs == null) return;

            if (sequenceStartIdxs.Count != 1)
            {
                //DEBUG
            }

            int idx0 = sequenceStartIdxs[sequenceStartIdxs.Count - 1];
            int idxf = rtSingRecord.time.Count - 1;


            SampleAudio sa = new SampleAudio(rtSingRecord, idx0, idxf);
            sa.Annotations = GetAnnotationsFromMelody(this.ssMelody, 0);
            sa.SavePart(file, sa.time[0], sa.time[sa.time.Length - 1]);

            try
            {
                System.IO.File.WriteAllText(file + "freq", GetPlotString(acqFreqSamples));
                System.IO.File.WriteAllText(file + "intens", GetPlotString(acqIntensSamples));
            }
            catch
            {
            }
        }

        /// <summary>Retrieves a string to save graph data</summary>
        /// <param name="data">Data to save</param>
        private string GetPlotString(List<PointF> data)
        {
            string s = "";
            for (int i = 0; i < data.Count; i++)
            {
                if (i != 0) s += "\r\n";
                s += data[i].X.ToString() + "\t" + data[i].Y.ToString();
            }
            return s;
        }

        /// <summary>Retrieves plot data string</summary>
        /// <param name="s">String to read</param>
        /// <returns></returns>
        public static List<PointF> GetPlotFromString(string s)
        {
            List<PointF> lst = new List<PointF>();
            string sepdec = (1.1).ToString().Substring(1, 1);
            s = s.Replace(".", sepdec).Replace(",", sepdec);

            string[] sSplit = s.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string ss in sSplit)
            {
                string[] ss2 = ss.Split(new string[] { "\t", " " }, StringSplitOptions.RemoveEmptyEntries);
                PointF pt = new PointF(float.Parse(ss2[0]), float.Parse(ss2[1]));
                lst.Add(pt);
            }

            return lst;
        }

        /// <summary>Retrieves annotations from melody</summary>
        /// <param name="melody">Source melody</param>
        /// <param name="track">Melody track (usually 0)</param>
        /// <returns></returns>
        public static List<SampleAudio.AudioAnnotation> GetAnnotationsFromMelody(SoundSynthesis melody, int track)
        {
            List<SampleAudio.AudioAnnotation> anns = new List<SampleAudio.AudioAnnotation>();

            //build annotations from melody
            double singTime = 0;
            for (int k = 0; k < melody.Notes[track].Count; k++)
            {
                double noteDur = melody.Notes[track][k].Duration * melody.Tempo;
                string note = melody.Notes[track][k].ToString().Split()[0];
                SampleAudio.AudioAnnotation an = new SampleAudio.AudioAnnotation("M:" + note+" "+noteDur.ToString(), singTime, singTime + noteDur);

                //add duration in seconds to current time
                singTime += noteDur;

                anns.Add(an);
            }

            return anns;
        }


        #endregion

        #region Identify voice frequency
        /// <summary>Currently identified frequency</summary>
        public double identifyCurFreq;
        /// <summary>Identifying frequency?</summary>
        public bool IsIdentifyingFreq = false;
        /// <summary>Maximum time to take identifying frequency</summary>
        public double MAXTIMETOIDENTFREQ = 5.0;

        SampleAudio.RealTime rt;
        System.Diagnostics.Stopwatch swIdent = new System.Diagnostics.Stopwatch();

        /// <summary>Attempts to identify frequency over last second</summary>
        List<float> identTimes, identFreqs;
        /// <summary>Callback function</summary>
        StopFunc stopFuncIdentVoiceFreq;

        /// <summary>Identify sing frequency from mic</summary>
        /// <param name="micID">Mic ID to use</param>
        /// <param name="stopFunc">Function to call when data is received/stop recording</param>
        public void IdentifyFreq(int micID, StopFunc stopFuncIdentVoiceFreq)
        {
            this.stopFuncIdentVoiceFreq = stopFuncIdentVoiceFreq;
            if (!IsIdentifyingFreq)
            {
                IsIdentifyingFreq = true;
                swIdent.Start();
                rt = new SampleAudio.RealTime();
                rt.OnDataReceived = IdentFreqDataReceived;
                rt.StartRecording(micID);

                identTimes = new List<float>();
                identFreqs = new List<float>();
            }
            else
            {
                StopIdentFreq();
            }
        }
        private void IdentFreqDataReceived()
        {
            if (!IsIdentifyingFreq) return;

            float stepFreq = (float)rtSingRecord.freqAmost / (float)SampleAudio.FFTSamples;
            List<LinearPredictor.Formant> formants;

            float hnr;
            float f0 = LinearPredictor.ComputeReinforcedFrequencies(rt.audioReceived.ToArray(), stepFreq, rt.audioReceived.Count - 1, 70, 1250, out formants, null, out hnr);

            //float f0 = SampleAudio.RealTime.ComputeBaseFrequency(rt.audioReceived.ToArray(), rt.deltaT, rt.audioReceived.Count - 1, 60);
            identifyCurFreq = f0;
            stopFuncIdentVoiceFreq();

            //ignores outlier frequencies
            if (70 <= f0 && f0 <= 800)
            {
                float curTime = (float)swIdent.Elapsed.TotalSeconds;
                identTimes.Add(curTime);
                identFreqs.Add(f0);
                while (curTime - identTimes[0] > 1)
                {
                    identTimes.RemoveAt(0);
                    identFreqs.RemoveAt(0);
                }

                float fStdDev;
                float fMean = SampleAudio.RealTime.getMean(identFreqs.ToArray(), out fStdDev);

                //found frequency
                if (curTime > 1 && fStdDev < 10 && fMean > 50 && fMean < 800) StopIdentFreq();

                //taking too long
                if (swIdent.Elapsed.TotalSeconds > MAXTIMETOIDENTFREQ) StopIdentFreq();
            }
        }
        /// <summary>Stop identifying frequency</summary>
        public void StopIdentFreq()
        {
            rt.StopRecording();
            IsIdentifyingFreq = false;
            stopFuncIdentVoiceFreq();
            swIdent.Reset();

            float fStdDev;
            float fMean = SampleAudio.RealTime.getMean(identFreqs.ToArray(), out fStdDev);

            identifyCurFreq = fMean;

            int noteId = (int)Math.Round(SoundSynthesis.Note.GetHalfStepsAboveA4(fMean));
        }
        #endregion
    }
}
