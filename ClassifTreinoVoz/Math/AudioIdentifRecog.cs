using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace AudioComparer
{
    /// <summary>Audio identification and recognition</summary>
    public class AudioIdentifRecog
    {
        /// <summary>Compute kmeans similarity in spectre regions, attempting to identify contiguous regions. Returns regions from 0 to N, -1 if region not assigned. Index 3 is group index</summary>
        /// <param name="spectre">Audio spectre</param>
        /// <param name="idx0">Initial spectre index</param>
        /// <param name="idxf">Final spectre index</param>
        /// <param name="nCenters">Number of groups to identify</param>
        /// <param name="timeStep">Acquisition time interval</param>
        /// <param name="smallestRegionTime">Smallest region to consider as region</param>
        /// <param name="totalError">Total clustering error</param>
        public static List<float[]> kMeansCluster(List<float[]> spectre, int idx0, int idxf, int nCenters, float timeStep, float smallestRegionTime, out float totalError)
        {
            totalError = 0;
            if (idxf >= spectre.Count) idxf = spectre.Count - 1;

            List<float[]> regionsToCluster = new List<float[]>();
            for (int k = idx0; k <= idxf; k++)
            {
                //float[] intens = new float[spectre[k].Length];
                //for (int p = 0; p < intens.Length; p++) intens[p] = (float)Math.Exp(spectre[k][p]);
                regionsToCluster.Add(spectre[k]);
            }
            if (regionsToCluster.Count == 0) return null;

            CLKmeansTracking.CLKmeans kmeans = new CLKmeansTracking.CLKmeans(regionsToCluster);
            int[] assignedRegions; float[] centerErrors;

            kmeans.CLClusterize(nCenters, out assignedRegions, out centerErrors);
            for (int k = 0; k < centerErrors.Length; k++)
            {
                centerErrors[k] /= regionsToCluster[0].Length;
                totalError += centerErrors[k];
            }

            float err = 0;
            foreach (float f in centerErrors) err += f;

            List<int[]> intRegions = GroupRegions(smallestRegionTime, timeStep, assignedRegions);
            List<float[]> regions = new List<float[]>();
            foreach (int[] rGroup in intRegions) regions.Add(new float[] { (idx0 + rGroup[0]) * timeStep, (idx0 + rGroup[1]) * timeStep, rGroup[2] });

            return regions;
        }

        /// <summary>Compute sequential similarity in spectre regions, attempting to identify contiguous regions. Returns regions from 0 to N, -1 if region not assigned</summary>
        /// <param name="spectre">Audio spectre</param>
        /// <param name="idx0">Initial spectre index</param>
        /// <param name="idxf">Final spectre index</param>
        /// <param name="similarityThreshold">Threshold to consider different, in mean absolute difference</param>
        /// <param name="timeStep">Acquisition time interval</param>
        /// <param name="smallestRegionTime">Smallest region to consider as region</param>
        public static List<float[]> ComputeSimilarSpectreRegions(List<float[]> spectre, int idx0, int idxf, float timeStep, float smallestRegionTime, float similarityThreshold, out float meanDif, out float stdDevDif)
        {
            if (idxf >= spectre.Count) idxf = spectre.Count - 1;

            //assign region to each DFT
            int[] region = new int[idxf - idx0 + 1];

            int curRegion = 0;
            int baseIdx = idx0;

            //current mean
            float[] difs = new float[idxf-idx0+1];

            float[] curMean = new float[spectre[baseIdx].Length];
            for (int kk = 0; kk < curMean.Length; kk++) curMean[kk] = spectre[baseIdx][kk];
            int nDFTsInInterval = 1;

            for (int k = idx0 + 1; k <= idxf; k++)
            {
                float curDif = ComputeMeanAbsDifference(spectre[baseIdx], spectre[k]);
                difs[k-idx0]=curDif;
                if (curDif > similarityThreshold)
                {
                    curRegion++;
                    baseIdx = k;
                    nDFTsInInterval = 1;
                    for (int kk = 0; kk < curMean.Length; kk++) curMean[kk] = spectre[baseIdx][kk];
                }
                else
                {
                    nDFTsInInterval++;
                    float w = 1.0f / nDFTsInInterval;
                    for (int kk = 0; kk < curMean.Length; kk++) curMean[kk] = (1.0f - w) * curMean[kk] + w * spectre[k][kk];
                }
                region[k - idx0] = curRegion;
            }

            meanDif = SampleAudio.RealTime.getMean(difs, out stdDevDif);

            List<int[]> intRegions = GroupRegions(smallestRegionTime, timeStep, region);

            List<float[]> regions = new List<float[]>();
            foreach (int[] rGroup in intRegions) regions.Add(new float[] { (idx0 + rGroup[0]) * timeStep, (idx0 + rGroup[1]) * timeStep });

            ////minimum number of indexes to consider as region
            //int minIdxTimeInterval = (int)Math.Round(smallestRegionTime / timeStep);
            //int nIdxsInInterval = 1;
            ////index where this interval begins
            //int idBeginInterval = idx0;
            //for (int k = idx0 + 1; k <= idxf; k++)
            //{
            //    if (region[k - idx0] == region[k - 1 - idx0] && k < idxf) nIdxsInInterval++;
            //    else
            //    {
            //        if (nIdxsInInterval < minIdxTimeInterval)
            //        {
            //            for (int j = idBeginInterval; j < k; j++) region[j - idBeginInterval] = -1;
            //        }
            //        else regions.Add(new float[] { idBeginInterval * timeStep, (k - 1) * timeStep });

            //        //reset number of indexes in interval
            //        nIdxsInInterval = 1;
            //        idBeginInterval = k;
            //    }
            //}


            return regions;
        }

        /// <summary>Group regions with same category. Last index contains category</summary>
        /// <param name="smallestRegionTime">Smallest allowed region time</param>
        /// <param name="timeStep">Time step</param>
        /// <param name="region">Region categories</param>
        public static List<int[]> GroupRegions(float smallestRegionTime, double timeStep, int[] region)
        {
            List<int[]> regions = new List<int[]>();

            //minimum number of indexes to consider as region
            int minIdxTimeInterval = (int)Math.Round(smallestRegionTime / timeStep);
            int nIdxsInInterval = 1;
            //index where this interval begins
            int idBeginInterval = 0;
            for (int k = 1; k < region.Length; k++)
            {
                if (region[k] == region[k - 1] && k < region.Length - 1) nIdxsInInterval++;
                else
                {
                    if (nIdxsInInterval < minIdxTimeInterval)
                    {
                        for (int j = idBeginInterval; j < k; j++) region[j - idBeginInterval] = -1;
                    }
                    else regions.Add(new int[] { idBeginInterval, k - 1, region[k - 1] });

                    //reset number of indexes in interval
                    nIdxsInInterval = 1;
                    idBeginInterval = k;
                }
            }

            return regions;
        }

        /// <summary>Compute mean absolute difference between two vectors</summary>
        public static float ComputeMeanAbsDifference(float[] v1, float[] v2)
        {
            float ans = 0;
            int n = v1.Length;
            for (int k = 0; k < n; k++) ans += Math.Abs(v1[k] - v2[k]);

            return ans / (float)n;
        }

        /// <summary>Feedback on progress</summary>
        /// <param name="prog">Progress value</param>
        public delegate void SetProgressFunc(float prog);
        /// <summary>Feedback on progress</summary>
        public static SetProgressFunc setProgress;

        /// <summary>Extracts features for phoneme recognition. Input should be a uniform region</summary>
        /// <param name="audio">Audio to filter</param>
        /// <param name="idx0">Initial audio[] index</param>
        /// <param name="idxf">Final audio[] index</param>
        /// <param name="SampleRate">Sampling rate</param>
        /// <param name="nFFTSamples">Number of samples to use in FFT</param>
        /// <param name="scaleIntesities">Scale intensities to normalized value?</param>
        public static List<float[]> ExtractFeatures(float[] audio, int idx0, int idxf, int SampleRate, int nFFTSamples, out double timeStep, bool scaleIntesities)
        {
            float stepFreq = SampleRate / (float)nFFTSamples;
            int numFreqKeep = nFFTSamples >> 2;

            //overlap
            int step = nFFTSamples >> 3; // nFFTSamples >> 2;

            timeStep = (double)step / (double)SampleRate;

            int nFFTs = (idxf - idx0) / step;

            //need at least 1 FFT
            if (nFFTs == 0)
                nFFTs++;

            //recompute final index
            idxf = step * nFFTs + idx0;

            List<float[]> fftIntens = new List<float[]>();
            for (int k = 0; k < nFFTs; k++)
            {
                fftIntens.Add(null);
            }

            int maxKSoFar = 0;
         
            Parallel.For(0, nFFTs, k =>
            //for (int k = 0; k < nFFTs; k++)
            {
                int curId = idx0 + step * k;


                double[] LocalFFT = AudioComparer.SampleAudio.computeAudioFFT(audio, curId, numFreqKeep);
                float[] localfftfloat = new float[numFreqKeep];
                for (int p = 0; p < numFreqKeep; p++) localfftfloat[p] = (float)LocalFFT[p];

                //normalize local FFT
                if (scaleIntesities)
                {
                    float minInt = float.MaxValue;
                    float maxInt = float.MinValue;
                    foreach (float ff in localfftfloat)
                    {
                        minInt = Math.Min(minInt, ff);
                        maxInt = Math.Max(maxInt, ff);
                    }
                    float scale = 1.0f / (maxInt - minInt);
                    for (int p = 0; p < localfftfloat.Length; p++) localfftfloat[p] = (localfftfloat[p] - minInt) * scale;
                }
                fftIntens[k] = localfftfloat;

                //progress feedback
                if (k > maxKSoFar && setProgress !=null)
                {
                    setProgress((float)k / (float)nFFTs);
                }
            });
            return fftIntens;
        }

        /// <summary>Extracts features for phoneme recognition. Input should be a uniform region</summary>
        /// <param name="audio">Audio to filter</param>
        /// <param name="idx0">Initial audio[] index</param>
        /// <param name="idxf">Final audio[] index</param>
        /// <param name="SampleRate">Sampling rate</param>
        /// <param name="nFFTSamples">Number of samples to use in FFT</param>
        public static void ExtractFeaturesOLD(float[] audio, int idx0, int idxf, int SampleRate, int nFFTSamples)
        {
            float stepFreq = SampleRate / (float)nFFTSamples;
            int numFreqKeep = nFFTSamples >> 2;

            //overlap
            int step = nFFTSamples >> 2;

            int nFFTs = (idxf - idx0) / step;

            //need at least 1 FFT
            if (nFFTs == 0)
                nFFTs++;

            //recompute final index
            idxf = step * nFFTs + idx0;

            List<float[]> fftIntens = new List<float[]>();
            List<List<AudioComparer.LinearPredictor.Formant>> lstReinfFreqs = new List<List<AudioComparer.LinearPredictor.Formant>>();
            List<float> lstF0 = new List<float>();
            for (int k = 0; k < nFFTs; k++)
            {
                lstReinfFreqs.Add(null);
                fftIntens.Add(null);
                lstF0.Add(0);
            }

            for (int k = 0; k < nFFTs; k++)
            {
                int curId = idx0 + step * k;

                double[] LocalFFT = AudioComparer.SampleAudio.computeAudioFFT(audio, curId, numFreqKeep);
                float[] localfftfloat = new float[numFreqKeep];
                for (int p = 0; p < numFreqKeep; p++) localfftfloat[p] = (float)LocalFFT[p];

                fftIntens[k] = localfftfloat;

                List<AudioComparer.LinearPredictor.Formant> reinfFreqs;

                float hnr;
                lstF0[k] = AudioComparer.LinearPredictor.ComputeReinforcedFrequencies(audio, stepFreq, curId, 60, 1250, out reinfFreqs, LocalFFT, out hnr);
                //lstReinfFreqs[k] = reinfFreqs;
                lstReinfFreqs[k] = reinfFreqs.OrderBy(ff => -ff.Intensity).ToList<AudioComparer.LinearPredictor.Formant>();
            }

            //compute median f0 and use this value. It should be robust
            List<float> f0Sort = new List<float>();
            foreach (float f in lstF0) f0Sort.Add(f);
            f0Sort.Sort();
            float f0Median = f0Sort[f0Sort.Count >> 1];

            //retrieves harmonics and subharmonics
            List<List<float>> harmonicFeatures = new List<List<float>>();
            for (int k = 0; k < nFFTs; k++) harmonicFeatures.Add(null);

            float f0ByStep = f0Median / stepFreq;

            for (int k = 0; k < nFFTs; k++)
            {
                for (int p = 0; p < numFreqKeep; p++) fftIntens[k][p] = (float)Math.Exp(fftIntens[k][p]);
                //normalize spectre
                float sum = 0;
                for (int p = 0; p < numFreqKeep; p++) sum += fftIntens[k][p];
                sum = 1.0f / sum;
                for (int p = 0; p < numFreqKeep; p++) fftIntens[k][p] *= sum;

                //harmonics list
                List<float> harmonicIntens = new List<float>();

                if (f0Median > 0)
                {

                    for (int harmonicId = 1; harmonicId < 30; harmonicId++)
                    {
                        int id0 = (int)(f0ByStep * (harmonicId - 0.25f));
                        int idf = (int)Math.Ceiling(f0ByStep * (harmonicId + 0.25f));
                        if (idf >= numFreqKeep) break;

                        float hIntens = 0;
                        for (int p = id0; p <= idf; p++)
                        {
                            hIntens += fftIntens[k][p];
                            fftIntens[k][p] = 0; //remove from spectre
                        }

                        harmonicIntens.Add(hIntens);
                    }

                }
                harmonicFeatures[k] = harmonicIntens;
            }

            //TODO: is this vocalized?
            //TODO: robust jitter and shimmer estimation

            //TODO: if this is vocalized, split features into vocalized and non-vocalized parts
            //normalize sum to 1
            //vocalized - extract harmonics, ~ 20 to 30 features - region average
            //after extraction - ~ 20 to 30 features, - region average

            //TODO: estimate harmonic to noise ratio
        }
    }
}
