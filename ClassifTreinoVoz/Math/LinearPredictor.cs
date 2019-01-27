using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenCLTemplate.LinearAlgebra;
using OpenCLTemplate;
using System.Drawing;

namespace AudioComparer
{
    /// <summary>Implements GPU Auto-regressive identification</summary>
    public class LinearPredictor
    {

        /*Quote: BURG Original
> Does anyone have C code for the Levinson-Durbin recursion (a.k.a. autocorrelation
> method) to derive the linear prediction coefficients for a set of data?

Sure:

// find the lpc coefficents and reflection coefficients from the
//   autocorrelation function

float Sacf2lpc(float *acf, int nacf, float *lpc, float *ref) {
  static float *tmp;
  static int ntmp = 0;
  double e, ci;
  int i, j;

  if(ntmp != nacf) {
    if(ntmp != 0) Sfree(tmp);
    tmp = SvectorFloat(nacf);
    ntmp = nacf;
  }

  // find lpc coefficients
  e = acf[0];
  lpc[0] = 1.0;
  for(i = 1; i <= nacf; i++) {
    ci = 0.0;
    for(j = 1; j < i; j++) ci += lpc[j] * acf[i-j];
    ref[i] = ci = (acf[i] - ci) / e;
    lpc[i] = ci;
    for(j = 1; j < i; j++) tmp[j] = lpc[j] - ci * lpc[i-j];
    for(j = 1; j < i; j++) lpc[j] = tmp[j];
    e = (1 - ci * ci) * e;
  }
  return(e); 
         
         void BurgAlgorithm( vector<double> &coeffs, const vector<double> &x )
{
// GET SIZE FROM INPUT VECTORS
size_t N = x.size() - 1;
size_t m = coeffs.size();
// INITIALIZE Ak
vector<double> Ak( m + 1, 0.0 );
Ak[ 0 ] = 1.0;
// INITIALIZE f and b
vector<double> f( x );
vector<double> b( x );
// INITIALIZE Dk
double Dk = 0.0;
for ( size_t j = 0; j <= N; j++ )
{
Dk += 2.0 * f[ j ] * f[ j ];
10
}
Dk -= f[ 0 ] * f[ 0 ] + b[ N ] * b[ N ];
// BURG RECURSION
for ( size_t k = 0; k < m; k++ )
{
// COMPUTE MU
double mu = 0.0;
for ( size_t n = 0; n <= N - k - 1; n++ )
{
mu += f[ n + k + 1 ] * b[ n ];
}
mu *= -2.0 / Dk;
// UPDATE Ak
for ( size_t n = 0; n <= ( k + 1 ) / 2; n++ )
{
double t1 = Ak[ n ] + mu * Ak[ k + 1 - n ];
double t2 = Ak[ k + 1 - n ] + mu * Ak[ n ];
Ak[ n ] = t1;
Ak[ k + 1 - n ] = t2;
}
// UPDATE f and b
for ( size_t n = 0; n <= N - k - 1; n++ )
{
double t1 = f[ n + k + 1 ] + mu * b[ n ];
double t2 = b[ n ] + mu * f[ n + k + 1 ];
f[ n + k + 1 ] = t1;
b[ n ] = t2;
}
// UPDATE Dk
Dk = ( 1.0 - mu * mu ) * Dk - f[ k + 1 ] * f[ k + 1 ] - b[ N - k - 1 ] * b[ N - k - 1 ];
}
// ASSIGN COEFFICIENTS
coeffs.assign( ++Ak.begin(), Ak.end() );
}*/

        /// <summary>Burg algorithm for autocorrelation UNDER TEST</summary>
        /// <param name="mCoefs"></param>
        /// <param name="x"></param>
        /// <returns></returns>
        public static double[] BurgAlgorithm(int mCoefs, double[] x)
        {
            double[] coefs = new double[mCoefs];

            // GET SIZE FROM INPUT VECTORS
            int N = x.Length - 1;
            //int mCoefs = coeffs.Length;

            // INITIALIZE Ak
            double[] Ak = new double[mCoefs + 1];
            Ak[0] = 1.0;
            // INITIALIZE f and b
            double[] f = new double[N + 1];
            double[] b = new double[N + 1];
            for (int kk = 0; kk <= N; kk++)
            {
                f[kk] = x[kk];
                b[kk] = x[kk];
            }
            // INITIALIZE Dk
            double Dk = 0.0;
            for (int j = 0; j <= N; j++)
            {
                Dk += 2.0 * f[j] * f[j];
            }
            Dk -= f[0] * f[0] + b[N] * b[N];
            // BURG RECURSION
            for (int k = 0; k < mCoefs; k++)
            {
                // COMPUTE MU
                double mu = 0.0;
                for (int n = 0; n <= N - k - 1; n++)
                {
                    mu += f[n + k + 1] * b[n];
                }
                mu *= -2.0 / Dk;
                // UPDATE Ak
                for (int n = 0; n <= (k + 1) / 2; n++)
                {
                    double t1 = Ak[n] + mu * Ak[k + 1 - n];
                    double t2 = Ak[k + 1 - n] + mu * Ak[n];
                    Ak[n] = t1;
                    Ak[k + 1 - n] = t2;
                }
                // UPDATE f and b
                for (int n = 0; n <= N - k - 1; n++)
                {
                    double t1 = f[n + k + 1] + mu * b[n];
                    double t2 = b[n] + mu * f[n + k + 1];
                    f[n + k + 1] = t1;
                    b[n] = t2;
                }
                // UPDATE Dk
                Dk = (1.0 - mu * mu) * Dk - f[k + 1] * f[k + 1] - b[N - k - 1] * b[N - k - 1];
            }
            for (int kk = 0; kk < mCoefs; kk++) coefs[kk] = Ak[kk + 1];
            return coefs;
        }

        static LinearPredictor()
        {
            if (CLCalc.CLAcceleration == CLCalc.CLAccelerationType.Unknown)
            {
                CLCalc.InitCL();
            }
        }

        /// <summary>Fit matrix</summary>
        float[,] M;

        /// <summary>Independent variable</summary>
        float[] y;

        /// <summary>Number of functions and points</summary>
        public int nFuncs, nPts;

        /// <summary>Index for regression. Relates to frequency: freq[maxIdx] = 1.0f / (float)((k + idxMax) * deltaT)</summary>
        int maxIdx, minIdx;

        /// <summary>Constructor. Attempt to fit an autoregressive model to data u[t] = sum(c0*u[t-minIdx]+...+cn*u[t-maxIdx])</summary>
        /// <param name="data">Data to use. Ideally, of length maxIdx+nPts. Starts from last data sample</param>
        /// <param name="minIdx">Minimum regression index</param>
        /// <param name="maxIdx">Maximum regression index</param>
        /// <param name="step">Stepsize in index</param>
        /// <param name="nPts">Number of samples to use</param>
        /// <param name="lastIdx">Index to analyze. Set to data.Length-1 for last point</param>
        public LinearPredictor(float[] data, int minIdx, int maxIdx, int step, int nPts, int lastIdx)
        {
            this.maxIdx = maxIdx;
            this.minIdx = minIdx;

            //normalize data - set min to 0 to prevent negative autocorrelation
            float max = float.MinValue;
            float min = float.MaxValue;
            foreach (float f in data)
            {
                max = Math.Max(max, f);
                min = Math.Min(min, f);
            }
            float scaleFac = 1.0f / (max - min);

            ////build matrix
            //int lastIdx = data.Length - 1;

            this.nPts = nPts;

            nFuncs = (maxIdx - minIdx + 1) / step;

            ////make matrix size multiple of 32, useful because of block cholesky in GPU
            //nFuncs = 32 + 32 * (int)(nFuncs / 32);
            maxIdx = nFuncs * step + minIdx - 1;

            //add interceptor
            nFuncs++;

            M = new float[nPts, nFuncs];
            y = new float[nPts];

            for (int i = 0; i < nPts; i++)
            {
                if (lastIdx - i >= 0) y[i] = (data[lastIdx - i] - min) * scaleFac;
                else
                {
                }

                for (int j = 0; j < nFuncs - 1; j += 1)
                {
                    int idx = lastIdx - j - minIdx - i;
                    //hope there is enough data
                    if (idx >= 0) M[i, j] = (data[lastIdx - j * step - minIdx - i] - min) * scaleFac;
                    else
                    {
                    }
                }
                M[i, nFuncs - 1] = 1;
            }
        }

        #region Linear predictors: LS, LpLq
        /// <summary>Attempt to build a sparse model using LpLq technique</summary>
        /// <param name="xInitial">initial guess</param>
        /// <param name="p">P-value</param>
        /// <param name="q">Q-value for regularization</param>
        /// <param name="nIters">Number of iterarions. Hint: 1 to 5</param>
        /// <returns></returns>
        public float[] LinearPredict(float[] xInitial, float p, float q, int nIters)
        {
            float[] x0 = new float[nFuncs];
            float[] lambda = new float[nFuncs];

            for (int k = 0; k < nFuncs; k++)
            {
                //lambda[k] = 1.0f;

                //give more emphasis on lower frequencies
                lambda[k] = 0.2f*(5.0f + (float)Math.Log10(1 + k));// (k + 0.5f * nFuncs) / nFuncs;
                x0[k] = xInitial[k];
            }

            float[] w = new float[nPts];
            for (int k = 0; k < nPts; k++) w[k] = 1.0f;

            for (int iter = 0; iter < nIters; iter++)
            {
                //System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
                //sw.Start();

                x0 = floatOptimization.CurveFitting.PNormMinimization(x0, M, y, w, lambda, p, q);
                //sw.Stop();

                if (iter < nIters - 1)
                {
                    //reweight lambdas for next iteration - attempt to reject negative values
                    for (int k = 0; k < nFuncs; k++) lambda[k] = x0[k] > 0 ? 1.0f / (1e-5f + Math.Abs(x0[k])) : 1.0f / 1e-5f;

                    x0[0] = 0.1f;
                }
            }
            //OpenCLTemplate.CLCalc.Program.Variable v = null;
            return x0;
        }

        /// <summary>Least squares linear predictor</summary>
        public float[] LinearPredictLS()
        {
            //CLCalc.DisableCL();
            return floatOptimization.CurveFitting.LeastSquares(M, y, 8.0f);
            //return LeastSquares(M, y, 1);
        }
        #endregion

        #region Estimate formants


        /// <summary>Attempts to estimate formants from least-squares approximation. [0] - fundamental, [1] - f1, [2] - f2 etc</summary>
        /// <param name="minFreq">Minimum frequency of interest</param>
        /// <param name="maxFreq">Maximum frequency of interest - can set to big values</param>
        /// <param name="audioData">Audio data</param>
        /// <param name="deltaT">Delta time</param>
        /// <param name="t0">Time of interest</param>
        /// <param name="fIntens">Frequency intensities</param>
        /// <returns></returns>
        public static float[] EstimateFormantsLS(float[] audioData, float deltaT, float minFreq, float maxFreq, float t0, out float[] fIntens)
        {
            //acquisition time step - deltaX

            //lower frequency requires longer distance
            int idxMax = (int)(1.0f / (minFreq * deltaT));
            int idxMin = (int)(1.0f / (maxFreq * deltaT));

            //index of time of interest
            int anIdx = (int)(t0 / deltaT);

            int step = 1;
            LinearPredictor lp = new LinearPredictor(audioData, idxMin, idxMax, step, 2 * (idxMax - idxMin), anIdx);


            float[] cLS = lp.LinearPredictLS();

            //finds maximum coefficient
            float cMax = cLS[0];
            int idCMax = 0;
            for (int k = 0; k < cLS.Length; k++)
            {
                if (cMax < cLS[k])
                {
                    cMax = cLS[k];
                    idCMax = k;
                }
            }

            //finds extrema
            List<float> formants = new List<float>();
            List<float> formantIntens = new List<float>();
            List<int> idxsFormants = new List<int>();

            formants.Add(1.0f / (float)((idCMax * step + idxMin) * deltaT));
            formantIntens.Add(cLS[idCMax]);
            idxsFormants.Add(idCMax);

            for (int k = idCMax - 1; k > 0; k--)
            {
                float curFreq = 1.0f / (float)((k * step + idxMin) * deltaT);
                if (cLS[k] > cLS[k - 1] && cLS[k] > cLS[k + 1] && cLS[k] > 0)
                {
                    idxsFormants.Add(k);
                    formants.Add(curFreq);
                    formantIntens.Add(cLS[k]);
                }
            }
            fIntens = formantIntens.ToArray();
            return formants.ToArray();
        }

        #endregion

        #region helper functions
        float getFitError(float[] x)
        {
            float[] ans = new float[y.Length];
            float error = 0;
            for (int i = 0; i < y.Length; i++)
            {
                float temp = 0;
                for (int j = 0; j < x.Length; j++)
                {
                    temp += M[i, j] * x[j];
                }
                error += (temp - y[i]) * (temp - y[i]);
            }
            return error;
        }

        /// <summary>Regular cubic spline interpolation that just goes through a given list of points.</summary>
        public class RegularCubicSpline
        {
            private double[] y2a;
            private double[] xa;
            private double[] ya;


            /// <summary>Returns one-time only needed second derivatives calculation for regular cubic spline
            /// interpolation. Send InitialDerivative and FinalDerivative greater than 1E30 for natural conditions.</summary>
            /// <param name="XValues">X array</param>
            /// <param name="YValues">Y array</param>
            /// <param name="InitialDerivative">Derivative dy/dx at point x=0</param>
            /// <param name="FinalDerivative">Derivative dy/dx at point x=x[x.Length-1]</param>
            public RegularCubicSpline(double[] XValues, double[] YValues, double InitialDerivative, double FinalDerivative)
            {
                if (XValues.Length != YValues.Length) throw new Exception("x and y dimensions must be equal for Cubic Spline Second Derivative Calculation");

                int n = XValues.Length - 1;
                y2a = new double[XValues.Length];
                xa = new double[XValues.Length];
                ya = new double[XValues.Length];

                for (int i = 0; i < xa.Length; i++)
                {
                    xa[i] = XValues[i];
                    ya[i] = YValues[i];
                }

                double p, qn, sig, un;
                double[] u = new double[XValues.Length];

                if (InitialDerivative > 9.9E+29)
                {
                    y2a[0] = 0;
                    u[0] = 0;
                }
                else
                {
                    y2a[0] = -0.5;
                    u[0] = (3 / (XValues[1] - XValues[0])) * ((YValues[1] - YValues[0]) / (XValues[1] - XValues[0]) - InitialDerivative);
                }


                for (int i = 1; i < n; i++)
                {
                    sig = (XValues[i] - XValues[i - 1]) / (XValues[i + 1] - XValues[i - 1]);
                    p = sig * y2a[i - 1] + 2;
                    y2a[i] = (sig - 1) / p;
                    u[i] = (YValues[i + 1] - YValues[i]) / (XValues[i + 1] - XValues[i]) - (YValues[i] - YValues[i - 1]) / (XValues[i] - XValues[i - 1]);
                    u[i] = (6 * u[i] / (XValues[i + 1] - XValues[i - 1]) - sig * u[i - 1]) / p;
                }
                if (FinalDerivative > 9.9E+29)
                {
                    qn = 0;
                    un = 0;
                }
                else
                {
                    qn = 0.5;
                    un = (3 / (XValues[n] - XValues[n - 1])) * (FinalDerivative - (YValues[n] - YValues[n - 1]) / (XValues[n] - XValues[n - 1]));
                }
                y2a[n] = (un - qn * u[n - 1]) / (qn * y2a[n - 1] + 1);
                for (int k = n - 1; k >= 0; k--)
                {
                    y2a[k] = y2a[k] * y2a[k + 1] + u[k];
                }
            }

            /// <summary>Evaluates this regular SPLine at a point X.</summary>
            /// <param name="x">X value to calculate function value at.</param>
            public double Eval(double x)
            {
                int klo, khi, k;
                double h, b, a;
                int n = xa.Length - 1;
                klo = 0;
                khi = n;
                while (khi - klo > 1)
                {
                    k = (int)((khi + klo) / 2);
                    if (xa[k] > x) khi = k; else klo = k;
                }
                h = xa[khi] - xa[klo];
                if (h == 0) throw new Exception("Singularity found, cannot interpolate.");
                a = (xa[khi] - x) / h;
                b = (x - xa[klo]) / h;
                return a * ya[klo] + b * ya[khi] + ((a * a * a - a) * y2a[klo] + (b * b * b - b) * y2a[khi]) * (h * h) / 6;
            }

            /// <summary>Evaluates this regular SPLine at various points X.</summary>
            /// <param name="x">X values to calculate function value at.</param>
            public double[] Eval(double[] x)
            {
                double[] y = new double[x.Length];
                for (int i = 0; i < x.Length; i++) y[i] = Eval(x[i]);
                return y;
            }
        }
        #endregion

        #region Compute Extrema, retrieve formants

        /// <summary>Formant</summary>
        public class Formant
        {
            /// <summary>Create formant</summary>
            /// <param name="f">Frequency</param>
            /// <param name="intens">Intensity</param>
            /// <param name="harmonicId">Harmonic index: 0 - fundamental, 1 - 1st harmonic, 2 - 2nd etc </param>
            public Formant(double f, double intens, int harmonicId)
            {
                Frequency = f;
                Intensity = intens;
                this.harmonicId = harmonicId;
            }
            /// <summary>Formant frequency</summary>
            public double Frequency;
            /// <summary>Log-intensity of formant</summary>
            public double Intensity;

            /// <summary>Intensity compared to maximum</summary>
            public double relIntensity;

            /// <summary>Harmonic index: 0 - fundamental, 1 - 1st harmonic, 2 - 2nd etc</summary>
            public int harmonicId;

            /// <summary>string representation</summary>
            public override string ToString()
            {
                return Frequency.ToString() + "Hz " + Intensity.ToString()+"V";
            }
        }

        private class ReinforcedFreq
        {
            public ReinforcedFreq(float i, float f, int idMax)
            {
                intensity = i;
                frequency = f;
                this.idMax = idMax;
            }
            public float intensity;
            public float frequency;
            public int idMax;

            public override string ToString()
            {
                return frequency.ToString() + "Hz " + intensity.ToString();
            }
        }

        /// <summary>Computes reinforced frequencies. Returns fundamental frequency</summary>
        /// <param name="data">Data to use to compute</param>
        /// <param name="anIdx">Index to be analyzed</param>
        /// <param name="stepFreq">Frequency step</param>
        /// <param name="minFreq">Minimum frequency of interest</param>
        /// <param name="maxFreq">Maximum frequency of interest</param>
        /// <param name="formants">Formants found</param>
        /// <param name="refFFT">Reference FFT. Pass null to force method to compute itself</param>
        /// <param name="hnr">Harmonic to noise ratio</param>
        /// <returns></returns>
        public static float ComputeReinforcedFrequencies(float[] data, float stepFreq, int anIdx, float minFreq, float maxFreq, out List<Formant> formants, double[] refFFT, out float hnr)
        {
            //disable OpenCL for now
            //OpenCLTemplate.CLCalc.DisableCL();
            
            //System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
            //System.Diagnostics.Stopwatch swFit = new System.Diagnostics.Stopwatch();

            //sw.Start();

            int idMax = (int)(SampleAudio.maxKeepFreq / stepFreq);

            if (refFFT == null) refFFT = SampleAudio.computeAudioFFT(data, anIdx, SampleAudio.FFTSamples >> 1);

            int NLocalFFT = Math.Min(refFFT.Length, SampleAudio.FFTSamples >> 2);

            float[] localFFT = new float[NLocalFFT];
            float[] freqsFFT = new float[NLocalFFT];
            for (int k = 0; k < localFFT.Length; k++)
            {
                freqsFFT[k] = k * stepFreq;
                localFFT[k] = (float)Math.Pow(10, refFFT[k] * 0.05) * 0.2f;
            }

            int minId = (int)(minFreq / stepFreq);
            int maxId = (int)(maxFreq / stepFreq);
            //LinearPredictor LP = new LinearPredictor(localFFT, minId, maxId, 1, NLocalFFT - 1, NLocalFFT - 2);

            float[] x = new float[maxId - minId + 1];
            for (int k = 0; k < x.Length; k++) x[0] = 0.1f;


            float[] smoothedFFT = new float[localFFT.Length];
            for (int k = 1; k < localFFT.Length-1; k++) smoothedFFT[k] = 0.3333333f*(localFFT[k - 1] + localFFT[k] + localFFT[k + 1]);


            //predict extrema
            List<float> formantFreqs, formantIntens;
            formantFreqs = LinearPredictor.GetExtrema(freqsFFT, smoothedFFT, out formantIntens);
            //formantFreqs = LinearPredictor.GetExtrema(freqsFFT, localFFT, out formantIntens);

            if (formantFreqs.Count == 0)
            {
                formantFreqs.Add(0.01f);
                formantIntens.Add(0.01f);
            }
            
            float minFreqAllowed = 60+0.5f*stepFreq;

            List<ReinforcedFreq> lstReinf = new List<ReinforcedFreq>();
            for (int k = 0; k < formantFreqs.Count; k++) if (formantFreqs[k] > minFreqAllowed) lstReinf.Add(new ReinforcedFreq(formantIntens[k], formantFreqs[k], k));


            float maxIntens = formantIntens[0];
            float freqMaxIntens = formantFreqs[0];
            for (int k = 0; k < formantIntens.Count; k++)
            {
                if (maxIntens < formantIntens[k])
                {
                    maxIntens = formantIntens[k];
                    freqMaxIntens = formantFreqs[k];
                }
            }

            if (lstReinf.Count <= 0) lstReinf.Add(new ReinforcedFreq(0, 0, 0));
            float minAcceptableFreq = 0;
            minAcceptableFreq = lstReinf[0].frequency - 1.1f * stepFreq;
            for (int k = 1; k < lstReinf.Count; k++)
            {
                //current lower bound has too little intensity
                if (lstReinf[k - 1].intensity < 0.05f * maxIntens)
                    minAcceptableFreq = lstReinf[k].frequency * 0.85f - 1.1f * stepFreq;
                else break;
            }



            #region greatest common divisor attempts
            ////attempts to find approximate greatest common divisor of all amplified frequencies
            //List<float> gcdCandidates = new List<float>();
            //float gcd = lstReinf[0].frequency;
            //int nMaxUse = Math.Min(lstReinf.Count, 5);

            //for (int k = 1; k < nMaxUse; k++)
            //{
            //    if (lstReinf[k].intensity > 0.15f * lstReinf[0].intensity)
            //    {
            //        gcd = ApproxGCD(lstReinf[k].frequency, gcd, 2 * stepFreq);
            //        gcdCandidates.Add(gcd);
            //    }
                
            //    for (int j = k + 1; j < nMaxUse; j++)
            //    {
            //        float candidate = ApproxGCD(lstReinf[k].frequency, lstReinf[j].frequency, 4 * stepFreq);
            //        if (candidate <= lstReinf[0].frequency) gcdCandidates.Add(candidate);
            //    }
            //    //for (int j = 1; j < nMaxUse; j++)
            //    //{
            //    //    if (j != k) gcd = ApproxGCD(lstReinf[k].frequency, lstReinf[j].frequency, 2 * stepFreq);
            //    ////    if (j != k && formantIntens[k] >= 0.08f * maxIntens && formantIntens[j] >= 0.08f * maxIntens)
            //    ////    {
            //    ////        //gcd = ApproxGCD(gcd, formantFreqs[k], 2 * stepFreq);
            //    ////        float temp = ApproxGCD(formantFreqs[j], formantFreqs[k], 2 * stepFreq);
            //    ////        if (temp > freqMaxIntens) temp = ApproxGCD(freqMaxIntens, formantFreqs[k], 2 * stepFreq);

            //    ////        gcdCandidates.Add(temp);
            //    ////    }
            //    //}
            //}

            //gcdCandidates.Sort();
            ////hopefully found fundamental frequency; check for formants
            //float f0 = 0;
            //if (gcdCandidates.Count > 0) f0 = gcdCandidates[gcdCandidates.Count >> 1];
            //f0 = gcd;
            #endregion

            lstReinf = lstReinf.OrderBy(c => -c.intensity).ToList();

            float minReinfIntensityToConsider = 0.14f;
            while ((formantIntens.Count > 1) && (formantIntens[0] < minReinfIntensityToConsider * lstReinf[0].intensity || formantFreqs[0] < minFreqAllowed))
            {
                formantIntens.RemoveAt(0);
                formantFreqs.RemoveAt(0);
            }
            float f0 = formantFreqs[0];

            //compute upper bound for frequency
            float F0upperBound = f0;

            if (lstReinf.Count > 0)
            {
                F0upperBound = lstReinf[0].frequency;
                if (lstReinf.Count > 2 && lstReinf[1].intensity > minReinfIntensityToConsider * lstReinf[0].intensity) F0upperBound = ApproxGCD(lstReinf[0].frequency, lstReinf[1].frequency, 4 * stepFreq);
                if (lstReinf.Count > 3 && lstReinf[2].intensity > minReinfIntensityToConsider * lstReinf[0].intensity) F0upperBound = Math.Min(F0upperBound, ApproxGCD(lstReinf[0].frequency, lstReinf[2].frequency, 4 * stepFreq));
                if (F0upperBound < minFreqAllowed) F0upperBound = lstReinf[0].frequency;
            }
            
            //Compensate subharmonics
            if (F0upperBound < minAcceptableFreq) 
                F0upperBound *= 2;
            if (F0upperBound < minAcceptableFreq) 
                F0upperBound *= 2;
            if (F0upperBound < minAcceptableFreq) 
                F0upperBound *= 2;

            f0 = Math.Min(F0upperBound, f0);



            //float intensF0 = (float)refFFT[(int)(f0 / stepFreq)];
            formants = new List<Formant>();

            if (f0 > 0)
            {
                //go on to check on spectrogram
                int nn = 1;
                int curIdF = (int)Math.Round(f0 / stepFreq);
                float curF = f0;
                if (curIdF <= 0) curIdF = 1;
                while (curIdF > 0 && curIdF < localFFT.Length - 2)
                {
                    float refVal = (float)(localFFT[curIdF - 1] + localFFT[curIdF] + localFFT[curIdF + 1]) * 0.33333333333f;
                    formants.Add(new Formant(curF, refVal, nn - 1));

                    nn++;
                    curF = nn * f0;
                    curIdF = (int)Math.Round(nn * f0 / stepFreq);
                }


                //normalize intensity
                double intens0 = 3.0 / Math.Exp(formants[0].Intensity);
                for (int k = 0; k < formants.Count; k++)
                {
                    formants[k].relIntensity = 1; // 10 * Math.Log(1 + Math.Exp(formants[k].LogIntensity) * intens0);
                }
            }
            else formants.Add(new Formant(0, 0, 0));

            //Formant formant0 = formants[0];
            //formants.RemoveAt(0);
            //formants = formants.OrderBy(ff => -ff.LogIntensity).ToList<Formant>();
            //formants.Insert(0, formant0);

            //sw.Stop();

            //HNR
            float[] harmonics = ClassifTreinoVoz.PhonemeRecognition.HarmonicNoiseIntensities(localFFT, f0, stepFreq, false);
            hnr = (float)(20.0 * Math.Log(harmonics[0] / harmonics[1], 10));



            bool vocalized = true;
            float totalIntens = 0;
            float totalFormantIntens = 0;
            float totalMidFormantIntens = 0;
            float scaleF0 = f0 / stepFreq;
            foreach (float ff in smoothedFFT) totalIntens += ff;
            foreach (Formant ff in formants) totalFormantIntens += (float)ff.Intensity;
            foreach (Formant ff in formants) totalMidFormantIntens += smoothedFFT[(int)((ff.harmonicId + 0.5f) * scaleF0)];

            float iRatio = totalFormantIntens / totalIntens;
            float iRatioFormantMid = totalMidFormantIntens / totalFormantIntens;

            if (iRatioFormantMid > 0.90f && iRatio < 0.13f) vocalized = false;
            if (!vocalized) formants[0].Frequency = 0;

            return vocalized ? f0 : 0;
        }

        /// <summary>Approximate greatest common divisor</summary>
        private static float ApproxGCD(float a, float b, float threshold)
        {
            while (Math.Abs(a - b) > threshold)
            {
                if (a > b) a = a - b;
                else b = b - a;
            }
            return Math.Max(a, b);
        }

        /// <summary>Another attempt at GCD</summary>
        private static float ApproxGCD2OLD(List<float> f, List<float> fIntens, float fMaxIntens, float maxIntens, float stepFreq)
        {
            //approximate gcd
            float secondMaxIntens = fIntens[0];
            float secondF = f[0];
            if (secondMaxIntens == maxIntens)
            {
                secondMaxIntens = fIntens[1];
                secondF = f[1];
            }

            for (int k = 0; k < fIntens.Count; k++)
            {
                if (secondMaxIntens < fIntens[k] && fIntens[k] != maxIntens)
                {
                    secondF = f[k];
                    secondMaxIntens = fIntens[k];
                }
            }
            int window = 3;
            float tryf0 = window * ApproxGCD(fMaxIntens, secondF, 2 * stepFreq);

            List<float> f0Candidates = new List<float>();
            List<float> f0Vals = new List<float>();
            float minVal = float.MaxValue;
            float candidate = tryf0;
            for (int k = 1; k < window + 1; k++)
            {
                candidate = tryf0 / (float)k;
                float val = 0;
                for (int p = 0; p < f.Count; p++)
                {
                    float temp = f[p] / candidate;
                    temp = (float)Math.Abs(Math.Round(temp) - temp);
                    val += fIntens[p] * temp;
                }
                //val = val * (1.0f + 0.1f * k);
                f0Candidates.Add(candidate);
                f0Vals.Add(val);
                if (minVal > val) minVal = val;
            }
            
            return ApproxGCD(fMaxIntens, secondF, 2 * stepFreq);

            //for (int k = 1; k < f0Candidates.Count - 1; k++)
            //{
            //    if (f0Vals[k] <= minVal * 2f && f0Vals[k] < f0Vals[k - 1] && f0Vals[k] < f0Vals[k + 1])
            //        return f0Candidates[k];
            //}

            //return 0;
        }

        /// <summary>Computes extrema of y=f(x) by parabolic approximation</summary>
        /// <param name="valsX">X values</param>
        /// <param name="valsY">Y values</param>
        /// <param name="maxY">Maximum interpolated Y valoes</param>
        /// <returns></returns>
        public static List<float> GetExtrema(float[] valsX, float[] valsY, out List<float> maxY)
        {
            //predict extrema
            List<float> maxX = new List<float>();
            maxY = new List<float>();
            for (int k = 1; k < valsX.Length - 2; k++)
            {
                if (valsY[k] > valsY[k - 1] && valsY[k] > valsY[k + 1])
                {
                    float intens;
                    maxX.Add(getXPosOfMax(valsX[k - 1], valsX[k], valsX[k + 1], valsY[k - 1], valsY[k], valsY[k + 1], out intens));
                    maxY.Add(intens);
                }
            }
            return maxX;
        }

        /// <summary>Retrieve position of maximum of parabola that goes through x0,x1,x2 and y0,y1,y2</summary>
        static float getXPosOfMax(float x0, float x1, float x2, float y0, float y1, float y2, out float ymax)
        {
            //return 0.5f * (x0 * x0 * (y1 - y2) + x2 * x2 * (y0 - y1) + x1 * x1 * (y2 - y0)) / (x0 * (y1 - y2) + x1 * (y2 - y0) + x2 * (y0 - y1));

            float invdenom = 1.0f / ((x0 - x1) * (x0 - x2) * (x1 - x2));
            float a = (x0 * (y2 - y1) + x1 * (y0 - y2) + x2 * (y1 - y0)) * invdenom;
            float b = (x0 * x0 * (y1 - y2) + x2 * x2 * (y0 - y1) + x1 * x1 * (y2 - y0)) * invdenom;
            float c = (x0 * x0 * (x1 * y2 - x2 * y1) + x1 * x1 * (x2 * y0 - x0 * y2) + x2 * x2 * (y1 * x0 - y0 * x1)) * invdenom;

            float inv2A = 0.5f / a;

            ymax = c - b * b * 0.5f * inv2A;
            return -b * inv2A;
        }
        /// <summary>Retrieve position of maximum of parabola that goes through x0,x1,x2 and y0,y1,y2</summary>
        static float getXPosOfMax(float x0, float x1, float x2, float y0, float y1, float y2)
        {
            return 0.5f * (x0 * x0 * (y1 - y2) + x2 * x2 * (y0 - y1) + x1 * x1 * (y2 - y0)) / (x0 * (y1 - y2) + x1 * (y2 - y0) + x2 * (y0 - y1));
        }

        /// <summary>Applies median filter to a signal</summary>
        /// <param name="x">Vector</param>
        /// <param name="idx0">Initial index</param>
        /// <param name="idxf">Final index</param>
        /// <param name="windowSize">Window size forward and backward - 1 uses 3 elements, 2 uses 5 etc</param>
        public static void MedianFilterFundamentalFrequencies(List<float> x, int idx0, int idxf, int windowSize)
        {
            if (windowSize < 1) windowSize = 1;
            if (idx0 < windowSize) idx0 = windowSize;
            if (idxf > x.Count - 1 - windowSize) idxf = x.Count - 1 - windowSize;

            float[] xCp = new float[x.Count];
            for (int k = 0; k < x.Count; k++) xCp[k] = x[k];

            for (int k = idx0; k <= idxf; k++)
            {
                List<float> lstVals = new List<float>();
                for (int p = k - windowSize; p <= k + windowSize; p++)
                {
                    lstVals.Add(xCp[p]);
                }
                lstVals.Sort();
                x[k] = lstVals[windowSize];
            }
        }

        #region Dynamic programming for formant tracking

        #region Try without F0
        /// <summary>Tracks formants using dynamic programming</summary>
        /// <param name="lstFormant">Sequence of harmonics</param>
        /// <param name="idx0">Initial index in harmonics list</param>
        /// <param name="idxf">Final index in harmonics list</param>
        //public static List<float[]> TrackFormants(List<List<Formant>> lstFormant, int idx0, int idxf, string txtdistPenalty, string txtChangeFreqPenalty, string nFreqsCal, string txtvariationPenalty)
        public static List<float[]> TrackFormantsNoF0(List<float[]> FFTs, float stepFreq, List<List<Formant>> lstFormant, int idx0, int idxf, string txtdistPenalty, string txtChangeFreqPenalty, string nFreqsCal, string txtvariationPenalty)
        {
            double distancePenalty = double.Parse(txtdistPenalty);//1e-10;
            double changeFreqPenalty = double.Parse(txtChangeFreqPenalty);// 1.1e-9; // 
            double variationPenalty = double.Parse(txtvariationPenalty);//1e-5f;

            //window size for smoothing
            int windowSize = (int)(44100.0 * 8e-4) / 2;


            //copy relevant data without f0
            int nCols = idxf - idx0 + 1;
            int nRows = 0;
            for (int k = 0; k < nCols; k++) nRows = Math.Max(FFTs[k + idx0].Length, nRows);

            double[,] M = new double[nRows, nCols];
            for (int j = 0; j < nCols; j++)
            {
                int idF0 = 2 + (int)(lstFormant[j + idx0][0].Frequency / stepFreq);
                if (idF0 > nCols - 1) idF0 = nCols - 1;

                for (int i = 0; i <= idF0; i++)
                    M[i, j] = double.MinValue;

                for (int i = idF0; i < FFTs[j + idx0].Length; i++)
                    M[i, j] = Math.Exp(FFTs[j + idx0][i]);
            }


            List<double[]> smoothPaths = DynamicTrack(M, int.Parse(nFreqsCal), windowSize, (int)(250.0f/stepFreq), distancePenalty, changeFreqPenalty, variationPenalty, double.MinValue);


            //TODO:
            //draw paths, temporary here
            for (int k = 0; k < 7; k++)
            {
                for (int j = 0; j < nCols; j++)
                {
                    if (k + 1 <= lstFormant[j + idx0].Count - 1) lstFormant[j + idx0][k + 1].Frequency = 0; // lstFormant[j + idx0][0].Frequency * (1 + (double)paths[k][j]);
                }
            }


            for (int k = 0; k < smoothPaths.Count; k++)
            {
                for (int j = 0; j < nCols; j++)
                {
                    while (lstFormant[j + idx0].Count <= k + 1) lstFormant[j + idx0].Add(new Formant(0, 0, -1));

                    lstFormant[j + idx0][k + 1].Frequency = smoothPaths[k][j] * stepFreq;
                    //lstFormant[j + idx0][k + 1].Frequency = lstFormant[j + idx0][0].Frequency * (2 + paths[k][j]);
                }
            }


            return null;
        }
        #endregion

        /// <summary>Tracks formants using dynamic programming</summary>
        /// <param name="lstFormant">Sequence of harmonics</param>
        /// <param name="idx0">Initial index in harmonics list</param>
        /// <param name="idxf">Final index in harmonics list</param>
        //public static List<float[]> TrackFormants(List<List<Formant>> lstFormant, int idx0, int idxf, string txtdistPenalty, string txtChangeFreqPenalty, string nFreqsCal, string txtvariationPenalty)
        public static List<float[]> TrackFormants(List<List<Formant>> lstFormant, int idx0, int idxf, string txtdistPenalty, string txtChangeFreqPenalty, string nFreqsCal, string txtvariationPenalty)
        {
            double distancePenalty = double.Parse(txtdistPenalty);//1e-10;
            double changeFreqPenalty = double.Parse(txtChangeFreqPenalty);// 1.1e-9; // 
            double variationPenalty = double.Parse(txtvariationPenalty);//1e-5f;

            //window size for smoothing
            int windowSize = (int)(44100.0 * 8e-4) / 2;

            //copy relevant data without f0
            int nCols = idxf - idx0 + 1;
            int nRows = 0;
            for (int k = 0; k < nCols; k++) nRows = Math.Max(lstFormant[k + idx0].Count, nRows);

            //dont copy f0
            nRows--;
            double[,] M = new double[nRows, nCols];
            for (int j = 0; j < nCols; j++)
            {
                for (int i = 1; i < lstFormant[j + idx0].Count; i++)
                {
                    M[i - 1, j] = Math.Exp(lstFormant[j + idx0][i].Intensity);
                }
            }


            List<double[]> smoothPaths = DynamicTrack(M, int.Parse(nFreqsCal), windowSize, 2, distancePenalty, changeFreqPenalty, variationPenalty, double.MinValue * 0.1);


            //TODO:
            //draw paths, temporary here
            for (int k = 0; k < 7; k++)
            {
                for (int j = 0; j < nCols; j++)
                {
                    if (k + 1 <= lstFormant[j + idx0].Count - 1) lstFormant[j + idx0][k + 1].Frequency = 0; // lstFormant[j + idx0][0].Frequency * (1 + (double)paths[k][j]);
                }
            }


            for (int k = 0; k < smoothPaths.Count; k++)
            {
                for (int j = 0; j < nCols; j++)
                {
                    while (lstFormant[j + idx0].Count <= k + 1) lstFormant[j + idx0].Add(new Formant(0, 0, -1));

                    lstFormant[j + idx0][k + 1].Frequency = lstFormant[j + idx0][0].Frequency * (2 + smoothPaths[k][j]);
                    //lstFormant[j + idx0][k + 1].Frequency = lstFormant[j + idx0][0].Frequency * (2 + paths[k][j]);
                }
            }


            return null;
        }


        /// <summary>Track high intensity paths</summary>
        /// <param name="M">Matrix</param>
        /// <param name="nPaths">Number of paths</param>
        /// <param name="smoothWindowSize">Window size to use for smoothing</param>
        /// <param name="distancePenalty">Distance penalty</param>
        /// <param name="removeNeighborhood">Neighborhood to remove from best path so far</param>
        /// <param name="changePenalty">Penalty for changing line</param>
        /// <param name="variationPenalty">Variation penalty, to try to track constant frequency</param>
        /// <param name="pathCrossedPenalty">Penalty to path already visited</param>
        /// <returns></returns>
        public static List<double[]> DynamicTrack(double[,] M, int nPaths, int smoothWindowSize, int removeNeighborhood, double distancePenalty, double changePenalty, double variationPenalty, double pathCrossedPenalty)
        {
            List<int[]> paths = new List<int[]>();
            List<double[]> smoothPaths = new List<double[]>();

            int nRows = M.GetLength(0);
            int nCols = M.GetLength(1);

            double[,] intM = new double[nRows, nCols];

            //compute path for next formant
            for (int idF = 0; idF < nPaths; idF++)
            {
                //initialize integration
                for (int i = 0; i < nRows; i++) intM[i, 0] = M[i, 0];

                //perform integration
                for (int j = 1; j < nCols; j++)
                {
                    for (int i = 0; i < nRows; i++)
                    {
                        double v0 = double.MinValue, v1 = double.MinValue, v2 = double.MinValue;
                        if (i > 0) v0 = intM[i - 1, j - 1] - 1.41 * distancePenalty - changePenalty - variationPenalty * Math.Abs(M[i, j] - M[i - 1, j - 1]);
                        v1 = intM[i, j - 1] - distancePenalty - variationPenalty * Math.Abs(M[i, j] - M[i, j - 1]);
                        if (i < nRows - 1) v2 = intM[i + 1, j - 1] - 1.41 * distancePenalty - changePenalty - variationPenalty * Math.Abs(M[i, j] - M[i + 1, j - 1]);

                        intM[i, j] = Math.Max(v0, Math.Max(v1, v2)) + M[i, j];
                    }
                }

                //List<double> TEMPvals = new List<double>(); TEMPvals.Add(intM[0, nCols - 1]);
                int idMax = 0;
                double valMax = intM[0, nCols - 1];
                //find most amplified path
                for (int i = 1; i < nRows; i++)
                {
                    //TEMPvals.Add(intM[i, nCols - 1]);

                    if (valMax < intM[i, nCols - 1])
                    {
                        valMax = intM[i, nCols - 1];
                        idMax = i;
                    }
                }

                //track path which was used
                int[] path = new int[nCols];
                path[nCols - 1] = idMax;
                M[idMax, nCols - 1] = double.MinValue;
                for (int j = nCols - 2; j >= 0; j--)
                {
                    int i = path[j + 1];
                    double v0 = double.MinValue, v1 = double.MinValue, v2 = double.MinValue;
                    if (i > 0) v0 = intM[i - 1, j];
                    if (i < 0) i = 0;
                    if (i >= intM.GetLength(0)) i = intM.GetLength(0) - 1;
                    v1 = intM[i, j];
                    if (i < nRows - 1) v2 = intM[i + 1, j];

                    if (v0 >= v1 && v0 >= v2) path[j] = i - 1;
                    else if (v1 >= v0 && v1 >= v2) path[j] = i;
                    else path[j] = i + 1;

                }

                //integrate path
                double[] pathInt = new double[nCols];
                double[] smoothPath = new double[nCols];
                pathInt[0] = path[0];
                for (int j = 1; j < nCols; j++)
                {
                    pathInt[j] = pathInt[j - 1] + path[j];
                }

                //smooth path
                for (int j = 0; j < nCols; j++)
                {
                    int idxP0 = j - smoothWindowSize;
                    int idxPf = j + smoothWindowSize;
                    if (idxP0 < 0) idxP0 = 0;
                    if (idxPf > nCols - 1) idxPf = nCols - 1;
                    smoothPath[j] = (pathInt[idxPf] - pathInt[idxP0]) / (double)(idxPf - idxP0 + 1);
                }


                //remove path which was used
                for (int j = 0; j < nCols; j++)
                {
                    int idxRemove = (int)smoothPath[j];

                    //closest to what point?
                    if (smoothPath[j] - idxRemove > 0.5) idxRemove++;

                    M[idxRemove, j] = double.MinValue;
                    if (idxRemove + 1 <= nRows - 1) M[idxRemove + 1, j] = double.MinValue;

                    for (int p = idxRemove + 2; p < idxRemove + 3 + idF; p++)
                        if (p <= nRows - 1) M[p, j] = -Math.Abs(pathCrossedPenalty); // double.MinValue;

                    int idMaxRemove = idxRemove + removeNeighborhood;
                    for (int i = 0; i < idxRemove + removeNeighborhood; i++)
                        if (i <= nRows - 1) M[i, j] = -Math.Abs(pathCrossedPenalty);

                    //if (idxRemove - 1 >= 0) M[idxRemove - 1, j] = 0; // double.MinValue;
                    //if (idxRemove - 2 >= 0) M[idxRemove - 2, j] = 0; // double.MinValue;


                    //M[path[j], j] = double.MinValue;

                    //if (path[j] > 0) M[path[j] - 1, j] = double.MinValue;
                    //if (path[j] < nRows - 1) M[path[j] + 1, j] = double.MinValue;

                    //M[path[j + 1], j] = double.MinValue;
                    //M[path[j], j + 1] = double.MinValue;
                }

                smoothPaths.Add(smoothPath);
                paths.Add(path);
            }

            return smoothPaths;
        }

        #region Mean filters
        /// <summary>Mean Filter</summary>
        public static double[] MeanFilter(double[] x, int windowSize)
        {
            int n = x.Length;
            //integrate path
            double[] pathInt = new double[n];
            double[] smoothPath = new double[n];
            pathInt[0] = x[0];
            for (int j = 1; j < n; j++)
            {
                pathInt[j] = pathInt[j - 1] + x[j];
            }

            //smooth path
            for (int j = 0; j < n; j++)
            {
                int idxP0 = j - windowSize;
                int idxPf = j + windowSize;
                if (idxP0 < 0) idxP0 = 0;
                if (idxPf > n - 1) idxPf = n - 1;
                smoothPath[j] = (pathInt[idxPf] - pathInt[idxP0]) / (double)(idxPf - idxP0 + 1);
            }

            return smoothPath;
        }
        /// <summary>Mean Filter</summary>
        public static float[] MeanFilter(float[] x, int windowSize)
        {
            int n = x.Length;
            //integrate path
            float[] pathInt = new float[n];
            float[] smoothPath = new float[n];
            pathInt[0] = x[0];
            for (int j = 1; j < n; j++)
            {
                pathInt[j] = pathInt[j - 1] + x[j];
            }

            //smooth path
            for (int j = 0; j < n; j++)
            {
                int idxP0 = j - windowSize;
                int idxPf = j + windowSize;
                if (idxP0 < 0) idxP0 = 0;
                if (idxPf > n - 1) idxPf = n - 1;
                smoothPath[j] = (pathInt[idxPf] - pathInt[idxP0]) / (float)(idxPf - idxP0 + 1);
            }

            return smoothPath;
        }

        #endregion

        #endregion


        #endregion

        #region Linear interpolation
        /// <summary>Linear interpolation</summary>
        /// <param name="x">Times (ascending order)</param>
        /// <param name="y">Values</param>
        /// <param name="t">Time to interpolate at</param>
        /// <returns></returns>
        public static float LinInterp(List<float> x, List<float> y, float t)
        {
            if (x.Count != y.Count) throw new Exception("Interpolation error: x and y have different number of elements");
            int id = x.BinarySearch(t);
            if (id < 0)
            {
                id = ~id;
                id--;
            }
            if (id < 0) return y[0];
            if (id >= x.Count - 1) return y[x.Count - 1];

            float w = 1.0f - (t - x[id]) / (x[id + 1] - x[id]);

            return y[id] * w + (1.0f - w) * y[id + 1];
        }


        #endregion
    }
}
