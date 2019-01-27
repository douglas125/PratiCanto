using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AudioComparer;
using OpenCLTemplate.MachineLearning;
using System.Threading.Tasks;
using System.Threading;

namespace ClassifTreinoVoz
{
    /// <summary>Implements tools for phoneme recognition</summary>
    public class PhonemeRecognition
    {
        #region SVM search

        /// <summary>SVMs for multiclass classification</summary>
        private List<PhonemeSVM> pSVMs = new List<PhonemeSVM>();

        /// <summary>SVM for phoneme recognition</summary>
        private class PhonemeSVM
        {
            /// <summary>Constructor</summary>
            /// <param name="phoneme">Phoneme string</param>
            /// <param name="tset">Training set</param>
            /// <param name="idCat">Phoneme id, to allow local specific training set</param>
            public PhonemeSVM(string phoneme, TrainingSet tset, int idCat)
            {
                this.Phoneme = phoneme;
                svm = new SVM();
                svm.TrainingSet = new TrainingSet();

                foreach (TrainingUnit tu in tset.trainingArray) svm.TrainingSet.addTrainingUnit(new TrainingUnit(tu.xVector, tu.y == idCat ? 1 : -1));

                svm.Train();
                svm.RemoveNonSupportVectors();
            }
            
            /// <summary>SVM classifier</summary>
            public SVM svm;

            /// <summary>Phoneme being recognized</summary>
            public string Phoneme;
        }

        /// <summary>Categories</summary>
        public List<string> categories = new List<string>();

        /// <summary>Training set</summary>
        TrainingSet tSet = new TrainingSet();

        /// <summary>Constructor. Receives audio data from files and annotations</summary>
        /// <param name="lstSa">List of audio files with annotations</param>
        public PhonemeRecognition(List<SampleAudio> lstSa)
        {
            foreach (SampleAudio sa in lstSa) RetrieveFeats(sa);

            for (int i = 0; i < categories.Count; i++)
            {
                PhonemeSVM pSVM = new PhonemeSVM(categories[i], tSet, i);
                pSVMs.Add(pSVM);
            }

        }
        /// <summary>Retrieve features from sample audio</summary>
        /// <param name="sa">Sample audio</param>
        private void RetrieveFeats(SampleAudio sa)
        {
            foreach (SampleAudio.AudioAnnotation ann in sa.Annotations)
            {
                string annString = ann.Text.Replace("\r\n", "");
                if (ann.Text.StartsWith("[2]"))
                {
                    double timeStep;
                    List<float[]> feats = AudioIdentifRecog.ExtractFeatures(sa.audioData, (int)(ann.startTime / sa.time[1]), (int)(ann.stopTime / sa.time[1]), sa.waveFormat.SampleRate, SampleAudio.FFTSamples, out timeStep, true);

                    int idCat = categories.IndexOf(annString);
                    if (idCat < 0)
                    {
                        categories.Add(annString);
                        idCat = categories.Count - 1;
                    }

                    foreach (float[] featVec in feats) tSet.addTrainingUnit(new TrainingUnit(featVec, idCat));
                }
            }

        }

        /// <summary>Retrieve phoneme from spectre</summary>
        /// <param name="vec">Spectre vector</param>
        /// <param name="maxVal">Maximum value found</param>
        public float Classify(float[] vec, out float maxVal)
        {
            List<float> classifications = new List<float>();
            float maxClassifVal = float.NegativeInfinity;
            int idClass = -1;
            for (int k=0;k<pSVMs.Count;k++)
            {
                PhonemeSVM  psvm = pSVMs[k];
                float v = psvm.svm.ClassificationValue(new TrainingUnit(vec, 0));
                classifications.Add(v);

                if (v > maxClassifVal)
                {
                    idClass = k;
                    maxClassifVal = v;
                }
            }

            //float cat = msvm.Classify(new TrainingUnit(vec, 0), out maxVal);
            maxVal = maxClassifVal;
            return idClass;// cat;
        }

        /// <summary>Attempts to automatically create audio annotations</summary>
        /// <param name="sa">Sample audio</param>
        /// <param name="t0">Initial time</param>
        /// <param name="tf">Final time</param>
        /// <param name="minTime">Minimum time length to annotate</param>
        /// <returns></returns>
        public List<SampleAudio.AudioAnnotation> AutoAnnotate(SampleAudio sa, float t0, float tf, float minTime)
        {
            int idx0 = (int)(t0 / sa.time[1]);
            int idxf = (int)(tf / sa.time[1]);

            double timeStep;
            List<float[]> feats = AudioIdentifRecog.ExtractFeatures(sa.audioData, idx0, idxf, sa.waveFormat.SampleRate, SampleAudio.FFTSamples, out timeStep, true);


            List<float> cats = new List<float>();
            float m;
            for (int k = 0; k < feats.Count; k++) cats.Add(Classify(feats[k], out m));
            LinearPredictor.MedianFilterFundamentalFrequencies(cats, 0, cats.Count - 1, 2 * (int)(timeStep / minTime));

            int[] intCats = new int[cats.Count];
            for (int k = 0; k < cats.Count; k++) intCats[k] = (int)Math.Round(cats[k]);

            List<int[]> groups = AudioIdentifRecog.GroupRegions(0, timeStep, intCats);

            List<SampleAudio.AudioAnnotation> lstAutoAnn = new List<SampleAudio.AudioAnnotation>();
            int curCateg = -2;
            int prevCateg;
            foreach (int[] ii in groups)
            {
                double tfLocal = t0 + ii[1] * timeStep;
                double t0Local = ii[0] * timeStep + t0;
                if (tfLocal - t0Local > minTime)
                {
                    prevCateg = curCateg;
                    curCateg = ii[2];

                    //prevent emmpty breaks
                    if (prevCateg == curCateg) lstAutoAnn[lstAutoAnn.Count - 1].stopTime = tfLocal;
                    else lstAutoAnn.Add(new SampleAudio.AudioAnnotation(categories[ii[2]], t0Local, tfLocal));
                }
            }

            return lstAutoAnn;
        }

        /// <summary>FFT holder</summary>
        static AudioComparer.LomontFFT fft = new AudioComparer.LomontFFT();

        /// <summary>Estimates harmonics and nonHarmonics intensities (NOT LOG). Returns [0] harmonics, [1] non harmonics</summary>
        /// <param name="fft">Local fourier transform</param>
        /// <param name="f0">Fundamental frequency</param>
        /// <param name="stepFreq">Step frequency in FFT</param>
        /// <param name="fftInLog">FFT in log? If yes, take 10^(fft[k]/20)</param>
        public static float[] HarmonicNoiseIntensities(float[] fft, float f0, float stepFreq, bool fftInLog)
        {
            double harmonics = 0;
            double nonHarmonics = 0;
            for (int y = 0; y < fft.Length; y++)
            {
                float curFreq = y * stepFreq;
                if (curFreq > 60) //attempt to remove low frequency noise
                {
                    float closestHarmonic = (float)Math.Round(curFreq / f0) * f0;

                    //double intens = Math.Exp(fft[y]);
                    double intens;
                    if (fftInLog) intens = Math.Pow(10.0, fft[y] * 0.05);
                    else intens = fft[y];
                    //double intens = fft[y];
                    if (Math.Abs(curFreq - closestHarmonic) <= 0.22f * f0)
                    {
                        harmonics += intens;
                        //float wHarm = 1.0f - Math.Abs(curFreq - closestHarmonic) / (0.22f * f0);
                        //harmonics += (intens * wHarm);
                        //nonHarmonics += (intens * (1.0f - wHarm));
                    }
                    else nonHarmonics += (intens);
                }

            }
            return new float[] { (float)harmonics, (float)nonHarmonics };
        }

        #endregion

        #region Phonetic search

        /// <summary>Retrieves all classification scores of a vector</summary>
        /// <param name="vec">Vector</param>
        /// <returns></returns>
        public float[] AllClassificationScores(float[] vec)
        {
            float[] classifications = new float[pSVMs.Count];

            for (int k = 0; k < pSVMs.Count; k++)
            {
                PhonemeSVM psvm = pSVMs[k];
                float v = psvm.svm.ClassificationValue(new TrainingUnit(vec, 0));
                classifications[k] = v;
            }

            return classifications;
        }

        /// <summary>Attempts to find phonemes in an audio</summary>
        /// <param name="phonemes">Sequence of phonemes to look for</param>
        /// <param name="sa">Sample audio</param>
        /// <param name="t0">Where to start search</param>
        /// <param name="tf">Where to stop search</param>
        /// <param name="searchTimeWindow">Maximum duration of text in seconds</param>
        /// <returns></returns>
        public List<SampleAudio.AudioAnnotation> PhoneticSearch(string phonemes, SampleAudio sa, float t0, float tf, float searchTimeWindow)
        {
            //TODO: FInish this

            int[] phonemesIdx = new int[phonemes.Length];
            for (int k = 0; k < phonemes.Length; k++)
            {
                PhonemeSVM psvm = pSVMs.Where(p => p.Phoneme.Substring(3, 1) == phonemes.Substring(k, 1)).FirstOrDefault();
                if (psvm == null) throw new Exception("Attempting to identify unknown phoneme");
                phonemesIdx[k] = pSVMs.IndexOf(psvm);
            }

            //identify phoneme probabilities
            int idx0 = (int)(t0 / sa.time[1]);
            int idxf = (int)(tf / sa.time[1]);

            double timeStep;
            List<float[]> feats = AudioIdentifRecog.ExtractFeatures(sa.audioData, idx0, idxf, sa.waveFormat.SampleRate, SampleAudio.FFTSamples, out timeStep, true);

            //retrieves all classification scores
            List<float[]> cats = new List<float[]>();
            for (int k = 0; k < feats.Count; k++) cats.Add(AllClassificationScores(feats[k]));

            List<PhoneticUnit> subSeqs = DTWPhoneticSubseq(phonemesIdx, cats);

            SampleAudio.AudioAnnotation ann = new SampleAudio.AudioAnnotation(phonemes, t0 + timeStep * subSeqs[0].startId, t0 + timeStep * subSeqs[0].endId);
            List<SampleAudio.AudioAnnotation> lstann = new List<SampleAudio.AudioAnnotation>();
            lstann.Add(ann);

            return lstann;
        }

        /// <summary>Find best alignment of subsequence X inside Y</summary>
        /// <param name="X">Subsequence X</param>
        /// <param name="Y">Sequence Y</param>
        /// <returns></returns>
        private List<PhoneticUnit> DTWPhoneticSubseq(int[] X, List<float[]> Y)
        {
            int N = X.Length;
            int M = Y.Count;
            float[,] D = new float[N, M];

            //initialization
            D[0, 0] = phoneticDist(X[0], Y[0]);
            for (int k = 1; k < N; k++) D[k, 0] = D[k - 1, 0] + phoneticDist(X[k], Y[0]);
            for (int k = 1; k < M; k++) D[0, k] = phoneticDist(X[0], Y[k]);

            for (int i = 1; i < N; i++)
            {
                for (int j = 1; j < M; j++)
                {
                    D[i, j] = Math.Min(D[i - 1, j - 1], Math.Min(D[i - 1, j], D[i, j - 1])) + phoneticDist(X[i], Y[j]);
                }
            }

            //find local minima
            List<int> minima = new List<int>();
            for (int j = 1; j < M - 1; j++)
            {
                if (D[N - 1, j - 1] >= D[N - 1, j] && D[N - 1, j] <= D[N - 1, j + 1]) minima.Add(j);
            }

            //backtrack
            List<PhoneticUnit> regions = new List<PhoneticUnit>();
            for (int k = 0; k < minima.Count; k++)
            {
                int i = N - 1;
                int j = minima[k];
                while (i > 0 && j > 0)
                {
                    if (D[i - 1, j - 1] < D[i - 1, j] && D[i - 1, j - 1] < D[i, j - 1])
                    {
                        i--;
                        j--;
                    }
                    else if (D[i - 1, j] < D[i - 1, j - 1] && D[i - 1, j] < D[i, j - 1]) i--;
                    else j--;
                }

                regions.Add(new PhoneticUnit(j, minima[k], D[N - 1, minima[k]]));
            }
            regions = regions.OrderBy(r=>r.score).ToList<PhoneticUnit>();

            return regions;
        }
        private class PhoneticUnit
        {
            public PhoneticUnit(int id0, int idf, float score)
            {
                this.startId = id0;
                this.endId = idf;
                this.score = score;
            }
            public int startId;
            public int endId;
            public float score;

            public override string ToString()
            {
                return startId.ToString() + " to " + endId.ToString() + " score: " + score.ToString();
            }
        }


        private float phoneticDist(int x, float[] y)
        {
            //return (float)Math.Exp(-y[x]);
            float similarity = 0.01f * (float)Math.Exp(-y[x]);
            //return y[x] > 0 ? 1 : 0;
            float max = y[0];
            int idMax = 0;
            for (int k = 0; k < y.Length; k++) 
                if (max < y[k])
                {
                    max = y[k];
                    idMax = k;
                }


            return (x == idMax) ? 0 + similarity : 1 + similarity;
        }

        /// <summary>Find best alignment of subsequence X inside Y</summary>
        /// <param name="X">Subsequence X</param>
        /// <param name="Y">Sequence Y</param>
        /// <returns></returns>
        private int DTWSubseq(int[] X, int[] Y)
        {
            int N = X.Length;
            int M = Y.Length;
            int[,] D = new int[N, M];

            //initialization
            D[0, 0] = dist(X[0], Y[0]);
            for (int k = 1; k < N; k++) D[k, 0] = D[k - 1, 0] + dist(X[k], Y[0]);
            for (int k = 1; k < M; k++) D[0, k] = dist(X[0], Y[k]);

            for (int i = 1; i < N; i++)
            {
                for (int j = 1; j < M; j++)
                {
                    D[i, j] = Math.Min(D[i - 1, j - 1], Math.Min(D[i - 1, j], D[i, j - 1])) + dist(X[i], Y[j]);
                }
            }

            //DEBUG
            string s = "";
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < M; j++)
                {
                    s += D[i, j].ToString() + "\t";
                }
                s += "\r\n";
            }
            //textBox1.Text = s;


            return 0;
        }

        private int dist(int x, int y)
        {
            return x == y ? 0 : 1;
        }

        #endregion

        #region Spectrogram region search

        /// <summary>Region found in a spectrogram search</summary>
        public class SpectrogramFoundRegion
        {
            /// <summary>Constructor. Found region in spectrogram search</summary>
            /// <param name="tt0">Initial time</param>
            /// <param name="ttf">Final time</param>
            /// <param name="ddif">Difference to reference</param>
            public SpectrogramFoundRegion(double tt0, double ttf, float ddif)
            {
                t0 = tt0;
                tf = ttf;
                Difference = ddif;
            }

            /// <summary>Label of region</summary>
            public string Label;

            /// <summary>Region start time</summary>
            public double t0;
            /// <summary>Region end time</summary>
            public double tf;
            /// <summary>Difference to reference region</summary>
            public float Difference;

            public override string ToString()
            {
                return Math.Round(t0, 3).ToString() + " to " + Math.Round(tf, 3).ToString() + "s  dif=" + Difference.ToString();
            }
        }

        /// <summary>Searches spectrogram for a given sub-spectrogram</summary>
        /// <param name="sa">Audio to search</param>
        /// <param name="saSampleTime">Audio sample time</param>
        /// <param name="t0">Initial time</param>
        /// <param name="tf">Final time</param>
        /// <param name="lstSpecToSearch">Sample spectrograms of the same thing to search. 
        /// Extract with ExtractFeatures function. All samples specToSearch[k] should have same Count. This function uses
        /// smallest count</param>
        /// <param name="specToBeSearchd">Spectrogram to be searched. Use null to force computation</param>
        /// <param name="tStep">Time step between spectrograms in samples. Computed if spectrogram is null</param>
        public static List<SpectrogramFoundRegion> SpectrogramSearch(SampleAudio sa, double saSampleTime, float t0, float tf, List<List<float[]>> lstSpecToSearch, List<float[]> specToBeSearchd, ref double tStep)
        {
            int idx0 = (int)(t0 / saSampleTime);
            int idxf = (int)(tf / saSampleTime);

            if (specToBeSearchd == null || tStep <= 0)
            {
                specToBeSearchd = AudioIdentifRecog.ExtractFeatures(sa.audioData, idx0, idxf, sa.waveFormat.SampleRate, SampleAudio.FFTSamples, out tStep, false);
                PhonemeRecognition.SpectrogramNormalize(specToBeSearchd);
            }
            //smallest value of lstSpecToSearch[k].Count
            int minSpecsCount = int.MaxValue;
            for (int k = 0; k < lstSpecToSearch.Count; k++) minSpecsCount = Math.Min(minSpecsCount, lstSpecToSearch[k].Count);

            //compute duration of spectre to search. Assume it comes from recording with the same parameters
            double specDuration = minSpecsCount * tStep;

            float[] difs = new float[specToBeSearchd.Count - minSpecsCount];
            int FFTKeepPts = specToBeSearchd[0].Length;

            float invNSpecs = 1.0f / (float)minSpecsCount;


            Parallel.For(0, specToBeSearchd.Count - minSpecsCount, i =>
            //for (int i = 0; i < specToBeSearchd.Count - specToSearch.Count; i++)
            {

                float minDif = float.MaxValue;
                foreach (List<float[]> specToSearch in lstSpecToSearch)
                {
                    float curDif = 0;
                    for (int j = 0; j < minSpecsCount; j++)
                    {
                        for (int k = 0; k < FFTKeepPts; k++)
                        {
                            if (!float.IsNaN(specToSearch[j][k]) && !float.IsNaN(specToBeSearchd[i + j][k]))
                                curDif += (specToSearch[j][k] - specToBeSearchd[i + j][k]) * (specToSearch[j][k] - specToBeSearchd[i + j][k]);
                        }
                    }
                    minDif = Math.Min(minDif, curDif);
                }
                difs[i] = minDif * invNSpecs;
            });

            int[] idMatch = new int[difs.Length];
            for (int k = 0; k < idMatch.Length; k++) idMatch[k] = k;
            Array.Sort(difs, idMatch);
            List<SpectrogramFoundRegion> lstFoundRegion = new List<SpectrogramFoundRegion>();
            for (int i = 0; i < difs.Length; i++)
            {
                if (!float.IsNaN(difs[i]))
                {
                    double t0Reg = t0 + tStep * idMatch[i];
                    double tfReg = t0Reg + specDuration;
                    bool isInFoundRegion = false;
                    foreach (SpectrogramFoundRegion sfr in lstFoundRegion)
                    {
                        if ((t0Reg <= sfr.t0 && sfr.t0 <= tfReg) || (t0Reg <= sfr.tf && sfr.tf <= tfReg))
                        {
                            isInFoundRegion = true;
                            break;
                        }
                    }

                    if (!isInFoundRegion)
                    {
                        lstFoundRegion.Add(new SpectrogramFoundRegion(t0Reg, tfReg, difs[i]));
                    }
                }
                else
                {
                }
            }

            return lstFoundRegion;
        }

        /// <summary>Computes inner mean and variance of a spectrogram list to serve as threshold reference. Needs at least 3 samples - or else return 5 mean 1 stddev</summary>
        /// <param name="lstSpecToSearch">Spectrograms</param>
        /// <returns></returns>
        public static float[] SpectrogramComputeIntraGroupSqDist(List<List<float[]>> lstSpecToSearch)
        {

            //require at least 3 samples - 
            if (lstSpecToSearch.Count < 3) return new float[] { 5, 1 };

            //smallest value of lstSpecToSearch[k].Count
            int minSpecsCount = int.MaxValue;
            for (int k = 0; k < lstSpecToSearch.Count; k++) minSpecsCount = Math.Min(minSpecsCount, lstSpecToSearch[k].Count);
            int FFTKeepPts = lstSpecToSearch[0][0].Length;
            float invNSpecs = 1.0f / (float)minSpecsCount;

            List<float> difs = new List<float>();
            
            for (int i = 0; i < lstSpecToSearch.Count; i++)
            {
                float minDif = float.MaxValue;
                for (int j = 0; j < lstSpecToSearch.Count; j++)
                {
                    if (j != i)
                    {
                        float dif = 0;
                        for (int k = 0; k < minSpecsCount; k++) for (int p = 0; p < FFTKeepPts; p++)
                                dif += (lstSpecToSearch[i][k][p] - lstSpecToSearch[j][k][p]) * (lstSpecToSearch[i][k][p] - lstSpecToSearch[j][k][p]);

                        minDif = Math.Min(minDif, dif);
                    }
                }

                difs.Add(minDif * invNSpecs);

            }

            //Compute the Average      
            float avg = difs.Average();
            //Perform the Sum of (value-avg)_2_2      
            float sum = difs.Sum(d => (d - avg) * (d - avg));
            //Put it all together      
            float stddev = (float)Math.Sqrt(sum / (difs.Count - 1));

            return new float[] { avg, stddev };
        }

        /// <summary>Normalize set of spectrogram samples</summary>
        /// <param name="samples">List of samples to normalize</param>
        public void SpectrogramNormalize(List<List<float[]>> lstSamples)
        {
            foreach (List<float[]> s in lstSamples) SpectrogramNormalize(s);
        }

        /// <summary>Feedback on progress</summary>
        /// <param name="prog">Progress value</param>
        public delegate void SetProgressFunc(float prog);
        /// <summary>Feedback on progress</summary>
        public static SetProgressFunc setProgress;

        /// <summary>Normalize set of spectrogram samples</summary>
        /// <param name="samples">Samples to normalize</param>
        public static void SpectrogramNormalize(List<float[]> samples)
        {
            List<float> vals = new List<float>();
            for (int k = 2; k < samples.Count - 2; k++)
            {

                for (int p = 1; p < samples[k].Length - 1; p++)
                {
                    vals.Clear();
                    for (int r = -2; r <= 2; r++) 
                        for (int s = -1; s <= 1; s++) 
                            vals.Add(samples[k + r][p + s]);
                    
                    //0 1 2 3 4 5 6 7 8
                    vals.Sort();

                    samples[k][p] = vals[vals.Count >> 1];
                }
            }

            for (int k = 0; k < samples.Count; k++)
            {
                float minInt = float.MaxValue;
                float maxInt = float.MinValue;
                foreach (float ff in samples[k])
                {
                    minInt = Math.Min(minInt, ff);
                    maxInt = Math.Max(maxInt, ff);
                }
                float scale = 1.0f / (maxInt - minInt);
                for (int p = 0; p < samples[k].Length; p++) samples[k][p] = (samples[k][p] - minInt) * scale;
            }
            
            ////float minInt = float.MaxValue;
            ////float maxInt = float.MinValue;
            //List<float> vals = new List<float>();

            //for (int i = 0; i < samples.Count; i++)
            //{
            //    for (int j = 0; j < samples[i].Length; j++)
            //    {
            //        vals.Add(samples[i][j]);
            //    }
            //}

            ////Compute the Average      
            //float avg = vals.Average();
            ////Perform the Sum of (value-avg)_2_2      
            //float sum = vals.Sum(d => (d - avg) * (d - avg));
            ////Put it all together      
            //float stddev = (float)Math.Sqrt(sum / (vals.Count - 1));

            //float invNorm = 1.0f / (3.0f * stddev);
            //for (int i = 0; i < samples.Count; i++)
            //{
            //    for (int j = 0; j < samples[i].Length; j++)
            //    {
            //        samples[i][j] = (samples[i][j] - avg) * invNorm;
            //    }
            //}
        }


        #endregion
    }
}
