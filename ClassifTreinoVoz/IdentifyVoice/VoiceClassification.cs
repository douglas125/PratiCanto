using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace ClassifTreinoVoz
{
    /// <summary>Voice classification tasks</summary>
    public class VoiceTestAnalysis
    {
        /// <summary>Constructor.</summary>
        /// <param name="MaxFreq">Maximum frequency achieved</param>
        /// <param name="MinFreq">Minimum frequency achieved</param>
        /// <param name="male">Male voice?</param>
        /// <param name="categoryNames">[3] countertenor [4] tenor [5] baritone [6] bass [0] soprano [1] mezzo-soprano [2] contralto</param>
        public VoiceTestAnalysis(double MinFreq, double MaxFreq, bool male, string[] categoryNames)
        {
            this.maxFreq = MaxFreq;
            this.minFreq = MinFreq;

            #region Voice categories
            /*
Soprano: C4–C6          261.626 - 1046.50
Mezzo-soprano: A3–A5    220     - 880.000
Contralto: F3–F5        174.614 - 698.456
Countertenor: E3-E5     164.814 - 659.255
Tenor: C3–C5            130.813 - 523.251
Baritone: G2–G4         97.9989 - 391.995
Bass: E2-E4             82.4069 - 329.628
*/
            if (male)
            {
                //categories.Add("Countertenor"); categories.Add("Tenor"); categories.Add("Baritone"); categories.Add("Bass");
                categories.Add(categoryNames[3]); categories.Add(categoryNames[4]); categories.Add(categoryNames[5]); categories.Add(categoryNames[6]);
                minFreqs.Add(164.814); minFreqs.Add(130.813); minFreqs.Add(97.9989); minFreqs.Add(82.4069);
                maxFreqs.Add(659.255); maxFreqs.Add(523.251); maxFreqs.Add(391.995); maxFreqs.Add(329.628);
            }
            else
            {
                categories.Add(categoryNames[0]); categories.Add(categoryNames[1]); categories.Add(categoryNames[2]);
                minFreqs.Add(261.626); minFreqs.Add(220); minFreqs.Add(174.614);
                maxFreqs.Add(1046.50); maxFreqs.Add(880.000); maxFreqs.Add(698.456);
            }

            //converts to half steps
            foreach (double d in minFreqs) minFreqsHalfSteps.Add(SoundSynthesis.Note.GetHalfStepsAboveA4(d));
            foreach (double d in maxFreqs) maxFreqsHalfSteps.Add(SoundSynthesis.Note.GetHalfStepsAboveA4(d));

            #endregion

        }

        #region Voice classification
        /// <summary>Classification categories</summary>
        List<string> categories = new List<string>();
        /// <summary>Minimum frequencies</summary>
        List<double> minFreqs = new List<double>();
        /// <summary>Maximum frequencies</summary>
        List<double> maxFreqs = new List<double>();
        /// <summary>Minimum frequencies</summary>
        List<double> minFreqsHalfSteps = new List<double>();
        /// <summary>Maximum frequencies</summary>
        List<double> maxFreqsHalfSteps = new List<double>();

        /// <summary>Male voice?</summary>
        public bool MaleVoice = true;

        /// <summary>Maximum frequency achieved</summary>
        public double maxFreq;
        /// <summary>Minimum frequency achieved</summary>
        public double minFreq;

        /// <summary>Classify this voice based on frequencies achieved</summary>
        /// <param name="minFreq">Minimum frequency achieved</param>
        /// <param name="maxFreq">Maximum frequency achieved</param>
        public string ClassifyVoice()
        {
            float bestMatch = 0;
            string strMatch = "";

            for (int k = 0; k < categories.Count; k++)
            {
                double w = maxFreqsHalfSteps[k] - minFreqsHalfSteps[k];
                double min = Math.Max(minFreqsHalfSteps[k], SoundSynthesis.Note.GetHalfStepsAboveA4(minFreq));
                double max = Math.Min(maxFreqsHalfSteps[k], SoundSynthesis.Note.GetHalfStepsAboveA4(maxFreq));

                double match = (max - min) / w;
                if (match > bestMatch)
                {
                    strMatch = categories[k];
                    bestMatch = (float)match;
                }
            }

            return strMatch;
        }

        Font fNotes = new Font("Arial", 10, FontStyle.Bold);
        Font fCategs = new Font("Arial", 14, FontStyle.Bold);

        /// <summary>Retrieves a bitmap that demonstrates this voice classification graphically</summary>
        /// <param name="Width">Desired image width</param>
        /// <param name="Name">Name to write to classification rectangle</param>
        /// <param name="ClassifiedCategory">Category in which voice was classified</param>
        /// <param name="TitleText">Title text to add to image</param>
        public Bitmap GetClassificationImage(int Width, string Name, string ClassifiedCategory, string TitleText)
        {
            Bitmap dum = new Bitmap(1, 1);
            Graphics gDum = Graphics.FromImage(dum);
            SizeF ss = gDum.MeasureString("X", fCategs);

            //height of each category
            float HCategs = ss.Height;
            //spacing between classes
            float HSpace = 5;

            int H = (int)((2+categories.Count) * (HCategs + HSpace));
            Bitmap bmp = new Bitmap(Width, H);

            Graphics g = Graphics.FromImage(bmp);

            //Maximum and minimum frequencies to plot, in half steps
            float minTotalFreqHStep = (float)SoundSynthesis.Note.GetHalfStepsAboveA4(minFreq);
            float maxTotalFreqHStep = (float)SoundSynthesis.Note.GetHalfStepsAboveA4(maxFreq);
            foreach (double d in minFreqsHalfSteps) minTotalFreqHStep = Math.Min((float)d, minTotalFreqHStep);
            foreach (double d in maxFreqsHalfSteps) maxTotalFreqHStep = Math.Max((float)d, maxTotalFreqHStep);

            //step of each note
            float scaleX = (float)(Width-1)/ (maxTotalFreqHStep - minTotalFreqHStep);

            //Categories
            float curH = 0;
            ss = g.MeasureString(TitleText, fNotes);
            g.FillRectangle(Brushes.White, 0, 0, bmp.Width, bmp.Height);
            g.DrawString(TitleText, fCategs, Brushes.Black, 0, curH);

            curH += ss.Height + ss.Height / 2;


            for (int k = 0; k < categories.Count; k++)
            {
                float xMin = scaleX * ((float)minFreqsHalfSteps[k] - minTotalFreqHStep);
                float xMax = scaleX * ((float)maxFreqsHalfSteps[k] - minTotalFreqHStep);

                if (ClassifiedCategory == categories[k]) g.FillRectangle(Brushes.Green, xMin, curH, xMax - xMin, HCategs);
                else g.FillRectangle(Brushes.LightGray, xMin, curH, xMax - xMin, HCategs);

                g.DrawRectangle(Pens.Black, xMin, curH, xMax-xMin, HCategs);
                SizeF s = g.MeasureString(categories[k], fCategs);

                g.DrawString(categories[k], fCategs, Brushes.Black, 0.5f * (xMin + xMax) - 0.5f * s.Width, curH);

                //reference notes
                string minNote = SoundSynthesis.Note.GetNoteName(minFreqs[k]);
                g.DrawString(minNote, fNotes, Brushes.Black, xMin, curH);
                string maxNote = SoundSynthesis.Note.GetNoteName(maxFreqs[k]);
                s = g.MeasureString(maxNote, fNotes);
                g.DrawString(maxNote, fNotes, Brushes.Black, xMax-s.Width, curH);

                curH += HCategs + HSpace;
            }

            //Person
            float xMinPerson = scaleX * ((float)SoundSynthesis.Note.GetHalfStepsAboveA4(minFreq) - minTotalFreqHStep);
            float xMaxPerson = scaleX * ((float)SoundSynthesis.Note.GetHalfStepsAboveA4(maxFreq) - minTotalFreqHStep);

            g.FillRectangle(Brushes.LightGreen, xMinPerson, curH, xMaxPerson - xMinPerson, HCategs);
            g.DrawRectangle(Pens.Black, xMinPerson, curH, xMaxPerson - xMinPerson, HCategs);
            SizeF sPerson = g.MeasureString(Name, fCategs);

            g.DrawString(Name, fCategs, Brushes.Black, 0.5f * (xMinPerson + xMaxPerson) - 0.5f * sPerson.Width, curH);

            //reference notes
            string minNotePerson = SoundSynthesis.Note.GetNoteName(minFreq);
            g.DrawString(minNotePerson, fNotes, Brushes.Black, xMinPerson, curH);
            string maxNotePerson = SoundSynthesis.Note.GetNoteName(maxFreq);
            sPerson = g.MeasureString(maxNotePerson, fNotes);
            g.DrawString(maxNotePerson, fNotes, Brushes.Black, xMaxPerson - sPerson.Width, curH);


            curH += HCategs + HSpace;

            return bmp;
        }

        #endregion

    }

}
