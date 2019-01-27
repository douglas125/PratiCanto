using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AudioComparer
{
    /// <summary>Frequency-domain filtering</summary>
    public static class FFTFreqFilter
    {
        /// <summary>Frequency region identification</summary>
        public class freqRegion
        {
            public freqRegion(float F0, float Ff)
            {
                this.f0 = F0;
                this.ff = Ff;
            }

            /// <summary>Initial frequency in range</summary>
            public float f0;
            /// <summary>Final frequency in range</summary>
            public float ff;
        }

        /// <summary>FFT holder</summary>
        static LomontFFT fft = new LomontFFT();

        /// <summary>Filters a given audio in frequency domain. Returns filtered signal containing only desired frequency regions</summary>
        /// <param name="audio">Audio to filter</param>
        /// <param name="idx0">Initial audio[] index</param>
        /// <param name="idxf">Final audio[] index</param>
        /// <param name="SampleRate">Sampling rate</param>
        /// <param name="nFFTSamples">Number of samples to use in FFT</param>
        /// <param name="freqsToPreserve">Frequencies to preserve</param>
        /// <returns></returns>
        public static float[] FFTFilter(float[] audio, int idx0, int idxf, int SampleRate, int nFFTSamples, List<freqRegion> freqsToPreserve)
        {
            float stepFreq = SampleRate / (float)nFFTSamples;

            int step = nFFTSamples >> 1;
            int nFFTs = (idxf - idx0) / step;

            //recompute final index
            idxf = step * nFFTs + idx0;

            float[] filteredAudio = new float[idxf - idx0];

            for (int k = 0; k < nFFTs; k++)
            {
                int curId = idx0 + step * k;

                //will replace step = (nFFTSamples >> 1)  samples
                //need to sample data step/2 to left, step/2 to right

                //2*nFFTSamples because of real/imaginary parts
                double[] sample = new double[2*nFFTSamples];
                int idAudio0 = curId - (nFFTSamples >> 2);
                int idAudiof = curId + (nFFTSamples >> 2) + (nFFTSamples >> 1);

                if (idAudio0 < 0) idAudio0 = 0;
                if (idAudiof > audio.Length - 1) idAudiof = audio.Length - 1;
                for (int p = idAudio0; p < idAudiof; p++) sample[(p - idAudio0) << 1] = audio[p];

                ////hanning window
                //for (int p = 0; p < nFFTSamples; p++)
                //{
                //    double windowFcn = (double)p / (double)(nFFTSamples - 1);
                //    double hanning = 0.5 * (1 - Math.Cos(2 * Math.PI * windowFcn));

                //    sample[p << 1] *= hanning;
                //}

                //forward FFT
                fft.RealFFT(sample, true);

                //do filtering
                for (int indFreq = 1; indFreq < (nFFTSamples>>1); indFreq++)
                {
                    float curFreq = stepFreq * (float)indFreq;
                    bool keepFreq = false;
                    for (int q = 0; q < freqsToPreserve.Count; q++)
                    {
                        if (freqsToPreserve[q].f0 <= curFreq && curFreq <= freqsToPreserve[q].ff)
                        {
                            keepFreq = true; break;
                        }
                    }
                    if (!keepFreq)
                    {
                        sample[(indFreq << 1)] = 0;
                        sample[(indFreq << 1) + 1] = 0;
                        sample[((nFFTSamples - indFreq) << 1)] = 0;
                        sample[((nFFTSamples - indFreq) << 1) + 1] = 0;
                    }
                }

                //inverse FFT
                fft.RealFFT(sample, false);

                //save filtered values
                //for (int p = idAudio0; p < idAudiof; p++) filteredAudio[p - idAudio0 + step * k] = (float)sample[(p - idAudio0) << 1];

                for (int p = 0; p < (nFFTSamples >> 1); p++) filteredAudio[p + step * k] = (float)sample[(p + (nFFTSamples >> 2))<<1];

            }

            return filteredAudio;
        }
    }
}
