using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioComparer
{
    public class SelfSimilarity
    {
        #region Self similarity
        //Base: https://www.researchgate.net/publication/281647772_Automatic_classification_of_speech_dysfluencies_in_continuous_speech_based_on_similarity_measures_and_morphological_image_processing_tools

        /// <summary>Computes self similarity of a sound. Returns negative values to help build visualization</summary>
        /// <param name="sa">Sample audio</param>
        /// <param name="t0">Start time</param>
        /// <param name="tf">Final time</param>
        /// <param name="minTime">Minimum time window to consider</param>
        /// <param name="maxTime">Maximum time window to consider</param>
        /// <param name="windowTimeStep">Maximum time window to consider</param>
        /// <param name="H">Height of answer, corresponding to different window sizes</param>
        /// <param name="W">Width of answer, corresponding to different times</param>
        public static float[] ComputeSelfSimilarity(SampleAudio sa, float t0, float tf, float minTime, float maxTime, float windowTimeStep, out int W, out int H)
        {
            if (sa.audioSpectre == null) sa.RecomputeSpectre();
            int NElems = sa.audioSpectre[0].Length;

            float tstep = sa.baseFreqsTimes[1];
            int idx0 = (int)(t0 / tstep);
            int idxf = (int)(tf / tstep);

            //number of spectres
            int NSpecs = idxf - idx0 + 1;


            int nMinTime = (int)(minTime / tstep);
            int nMaxTime = (int)(maxTime / tstep);
            int nTimeStep = (int)(windowTimeStep / tstep);
            if (nTimeStep < 1) nTimeStep = 1;

            //compute pairwise difference - difs[i,j] = || spectre[i+idx0] - spectre[i+idx0-j] ||
            float[,] difs = new float[NSpecs+nMaxTime, nMaxTime];

            Parallel.For(0, NSpecs + nMaxTime, i =>
            //for (int i = 0; i < NSpecs; i++)
            {
                for (int j = 0; j < nMaxTime; j++)
                {
                    int ii = i;
                    if (ii + idx0 > sa.audioSpectre.Count - 1) ii = sa.audioSpectre.Count - 1 - idx0;

                    int jj = ii + idx0 - j;
                    if (jj >= 0) difs[i, j] = ComputeAbsDiff(sa.audioSpectre[ii + idx0], sa.audioSpectre[jj], NElems);
                    else difs[i, j] = ComputeAbsDiff(sa.audioSpectre[ii + idx0], sa.audioSpectre[0], NElems);
                }
            });

            W = NSpecs;
            H = (int)Math.Ceiling((double)(nMaxTime - nMinTime) / (double)nTimeStep);
            float[] selfDifs = new float[W * H]; //selfDifs[j + W*i] = j-th spectre, i-th windowSize

            int n = 0;

            //window sizes
            for (int windowSize = nMinTime; windowSize < nMaxTime; windowSize += nTimeStep)
            {
                float[] difInt = new float[NSpecs + nMaxTime];
                difInt[0] = difs[0, windowSize];
                for (int k = 1; k < NSpecs + nMaxTime; k++) difInt[k] = difInt[k - 1] + difs[k, windowSize];

                //float[] selfDifference = new float[NSpecs];
                
                //loop through times
                for (int t = 0; t < NSpecs; t++)
                {
                    selfDifs[t + W * n] = (difInt[t + windowSize] - difInt[t]) / (float)windowSize;

                    selfDifs[t + W * n] = -(float)Math.Log(1 + selfDifs[t + W * n]);
                }

                n++;
            }

            return selfDifs;
        }



        #endregion

        #region Speech speed

        /// <summary>Computes a variation rate curve</summary>
        /// <param name="sa">Sample audio</param>
        /// <param name="t0">Initial time (seconds)</param>
        /// <param name="tf">Final time (seconds)</param>
        /// <param name="wSize">Window size (seconds)</param>
        /// <returns></returns>
        public static float[] ComputeVariationRate(SampleAudio sa, float t0, float tf, float wSize)
        {
            float tstep = sa.baseFreqsTimes[1];
            int idx0 = (int)(t0 / tstep);
            int idxf = (int)(tf / tstep);
            //int iw = (int)(wSize / tstep);

            float[] varCurve = new float[idxf - idx0 + 1];
            
            for (int k = 0; k < idxf - idx0 + 1; k++)
            {
                varCurve[k] = SpectreVariationRate(sa, t0 + k * tstep - wSize, t0 + k * tstep);
            }

            return varCurve;
        }

        /// <summary>Computes spectre variation rate over a window</summary>
        /// <param name="sa">Sample audio</param>
        /// <param name="t0">Initial time</param>
        /// <param name="tf">Final time</param>
        public static float SpectreVariationRate(SampleAudio sa, float t0, float tf)
        {
            float tstep = sa.baseFreqsTimes[1];
            int idx0 = (int)(t0 / tstep); if (idx0 < 0) idx0 = 0;
            int idxf = (int)(tf / tstep); 
            if (idx0 == idxf) return 0;

            if (sa.audioSpectre == null) sa.RecomputeSpectre();
            int NElems = sa.audioSpectre[0].Length; 


            float specDifs = 0.0f;
            //integrates
            for (int k = 1; k < idxf - idx0 + 1; k++)
            {
                specDifs += ComputeAbsDiff(sa.audioSpectre[k + idx0], sa.audioSpectre[k + idx0 - 1], NElems);
            }
            return specDifs / (float)(idxf - idx0);
        }

        #endregion


        /// <summary>Computes absolute difference between two vectors</summary>
        /// <param name="x">Vector 1</param>
        /// <param name="y">Vector 2</param>
        /// <param name="N">Number of elements in vectors</param>
        /// <returns></returns>
        private static float ComputeAbsDiff(float[] x, float[] y, int N)
        {
            float dif = 0;

            for (int k = 0; k < N; k++) dif += Math.Abs(x[k] - y[k]);

            return dif / (float)N;
        }

        /// <summary>Computes quadratic difference between two vectors</summary>
        /// <param name="x">Vector 1</param>
        /// <param name="y">Vector 2</param>
        /// <param name="N">Number of elements in vectors</param>
        /// <returns></returns>
        private static float ComputeQuadDiff(float[] x, float[] y, int N)
        {
            float dif = 0;

            for (int k = 0; k < N; k++) dif += (x[k] - y[k]) * (x[k] - y[k]);

            return (float)Math.Sqrt(dif) / (float)N;
        }
    }
}
