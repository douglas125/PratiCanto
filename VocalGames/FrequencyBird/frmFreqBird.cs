using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using OpenCLTemplate.CLGLInterop;
using ClassifTreinoVoz;

namespace VocalGames
{
    public partial class frmFreqBird : Form
    {
        #region Audio part
        GuidedSinging gs;
        bool initSound = false;
        private void initGuidedSinging(int inputIdx)
        {
            SoundSynthesis ssMelody = new SoundSynthesis();
            SoundSynthesis.Note n = new SoundSynthesis.Note(double.MaxValue, 0, new SoundSynthesis.WaveShapePiano());
            SoundSynthesis.Note n2 = new SoundSynthesis.Note(0.001, 100, new SoundSynthesis.WaveShapePiano());

            ssMelody.Notes.Add(new List<SoundSynthesis.Note>());
            ssMelody.Notes[0].Add(n2);
            ssMelody.Notes[0].Add(n);

            gs = new GuidedSinging(inputIdx, guidedSingingStopFunc, ssMelody);
            gs.FreeSinging = true;

            gs.StartGuidedSinging("");

            initSound = true;
        }

        private void guidedSingingStopFunc()
        {
        }

        private void frmFreqBird_FormClosing(object sender, FormClosingEventArgs e)
        {
            gs.Stop();
        }


        #endregion

        public frmFreqBird(int inputIdx)
        {
            InitializeComponent();

            initGuidedSinging(inputIdx);
        }

        _2DGraphics dg2d;
        _2DGraphics.Sprite2D charSprite;

        int maxFood = 10, maxPoison = 7;
        List<GLRender.GLVBOModel> VBOfood = new List<GLRender.GLVBOModel>();
        List<GLRender.GLVBOModel> VBOpoison = new List<GLRender.GLVBOModel>();
        List<Bitmap> bmpFood = new List<Bitmap>();
        List<Bitmap> bmpPoison = new List<Bitmap>();

        private void Form1_Load(object sender, EventArgs e)
        {


            delIncPts = IncPts;
            delDecLife = DecLife;

            DirectoryInfo di = new DirectoryInfo(Application.StartupPath);
            FileInfo[] fi = di.GetFiles("char*.png");

            //character
            List<Bitmap> bmps = new List<Bitmap>();
            Color clearColor = Color.Black;
            foreach (FileInfo f in fi)
            {
                Bitmap bmpLoad = new Bitmap(f.FullName);
                clearColor = bmpLoad.GetPixel(0, 0);
                //bmpLoad.MakeTransparent(bmpLoad.GetPixel(0,0));
                bmps.Add(bmpLoad);
            }
            //clearColor = Color.Yellow;
            dg2d = new _2DGraphics(gl2D, clearColor);

            //poison
            fi = di.GetFiles("poison*.png");
            foreach (FileInfo f in fi) bmpPoison.Add(new Bitmap(f.FullName));

            //food
            fi = di.GetFiles("target*.png");
            foreach (FileInfo f in fi) bmpFood.Add(new Bitmap(f.FullName));

            curFoodSpeeds = new double[maxFood];
            for (int k = 0; k < maxFood; k++)
            {
                GLRender.GLVBOModel m = _2DGraphics.CreateTextureHolder(foodSize);
                m.SetTexture(bmpFood[rnd.Next(bmpFood.Count)]);
                m.ShowModel = false;
                VBOfood.Add(m);
                dg2d.Models.Add(m);
            }

            curPoisonSpeeds = new double[maxPoison];
            for (int k = 0; k < maxPoison; k++)
            {
                GLRender.GLVBOModel m = _2DGraphics.CreateTextureHolder(poisonSize);
                m.ShowModel = false;
                m.SetTexture(bmpPoison[rnd.Next(bmpPoison.Count)]);
                VBOpoison.Add(m);
                dg2d.Models.Add(m);
            }


            charSprite = new _2DGraphics.Sprite2D(bmps, new float[] { 0.12f, 0.12f });
            charSprite.SetPositionRotation(0.1, 0.3, 0);
            dg2d.Sprites2D.Add(charSprite);

            gl2D.Invalidate();

            playAnim = true;
            bgWAnim.RunWorkerAsync();
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            if (dg2d!=null) dg2d.SetupViewPorts();
        }

        #region Game logic

        /// <summary>Food size</summary>
        float[] foodSize = new float[] { 0.04f, 0.14f };
        /// <summary>Poison size</summary>
        float[] poisonSize = new float[] { 0.04f, 0.14f };

        /// <summary>How often food can be created {min, max}</summary>
        private double[] FoodCreationTime = new double[] {0.5, 2};

        /// <summary>Food speed {min, max}</summary>
        private double[] foodSpeed = new double[] { 0.1, 0.2 };
        /// <summary>Poison speed {min, max}</summary>
        private double[] poisonSpeed = new double[] { 0.05, 0.1 };

        /// <summary>Speeds of current foods</summary>
        private double[] curFoodSpeeds;
        /// <summary>Speeds of current poisons</summary>
        private double[] curPoisonSpeeds;

        /// <summary>How often poison can be created {min, max}</summary>
        private double[] PoisonCreationTime = new double[] { 1, 3 };

        /// <summary>When will food be created next?</summary>
        private double nextFoodCreateTime = 1;

        /// <summary>When will poison be created next?</summary>
        private double nextPoisonCreateTime = 2;

        Random rnd = new Random();

        double prevGameTime = 0;
        double gameTime = 0;


        private delegate void voidDelegate();
        voidDelegate delIncPts, delDecLife;
        private void IncPts()
        {
            int nPts = int.Parse(lblPts.Text);
            nPts++;
            lblPts.Text = nPts.ToString();
        }
        private void DecLife()
        {
            int nLifes = int.Parse(lblLifes.Text);
            nLifes--;
            lblLifes.Text = nLifes.ToString();

            if (nLifes == 0) playAnim = false;
        }

        /// <summary>Initial - reference pitch</summary>
        float initialPitch = 0;
        /// <summary>Current pitch</summary>
        float curPitch = 0;

        /// <summary>Bird Y position</summary>
        float birdY0 = 0;

        /// <summary>Do next game iteration</summary>
        private void doGameIter()
        {
            prevGameTime = gameTime;
            gameTime = swGame.Elapsed.TotalSeconds;

            #region Uses voice to set bird position

            if (curPitch > 0)
            {
                double[] pos = charSprite.GetPosition();

                if (initialPitch == 0)
                {
                    initialPitch = curPitch;
                    birdY0 = (float)pos[1];
                }
                else
                {
                    double pitch0 = SoundSynthesis.Note.GetHalfStepsAboveA4(initialPitch);
                    double pitchcur = SoundSynthesis.Note.GetHalfStepsAboveA4(curPitch);

                    pos[1] = ((float)(pitchcur - pitch0) * 0.05f + birdY0) * 0.9f + 0.1f * pos[1];

                    if (pos[1] < 0) pos[1] = 0;
                    if (pos[1] > 1) pos[1] = 1;
                    charSprite.SetPositionRotation(pos[0], pos[1], pos[2]);
                }
            }
            else if (curPitch <= 0)
            {
                initialPitch = 0;

            }

            #endregion

            #region Create food and poison
            if (gameTime > nextFoodCreateTime)
            {
                nextFoodCreateTime += FoodCreationTime[0] + rnd.NextDouble() * (FoodCreationTime[1] - FoodCreationTime[0]);
                GLRender.GLVBOModel m = VBOfood.Where(i => i.ShowModel == false).FirstOrDefault();
                if (m != null) //maximum number of food reached
                {
                    m.vetTransl.x = 1;
                    m.vetTransl.y = 0.1 + 0.8 * rnd.NextDouble();
                    m.ShowModel = true;

                    int id = VBOfood.IndexOf(m);
                    curFoodSpeeds[id] = foodSpeed[0] + rnd.NextDouble() * (foodSpeed[1] - foodSpeed[0]);
                }
            }

            for (int k = 0; k < VBOfood.Count; k++)
            {
                if (VBOfood[k].ShowModel)
                {
                    VBOfood[k].vetTransl.x -= curFoodSpeeds[k] * (gameTime - prevGameTime);
                    if (VBOfood[k].vetTransl.x < -foodSize[0]) VBOfood[k].ShowModel = false;
                }
            }

            if (gameTime > nextPoisonCreateTime)
            {
                nextPoisonCreateTime += PoisonCreationTime[0] + rnd.NextDouble() * (PoisonCreationTime[1] - PoisonCreationTime[0]);
                GLRender.GLVBOModel m = VBOpoison.Where(i => i.ShowModel == false).FirstOrDefault();
                if (m != null) //maximum number of food reached
                {
                    m.vetTransl.x = 1;
                    m.vetTransl.y = 0.1 + 0.8 * rnd.NextDouble();
                    m.ShowModel = true;

                    int id = VBOpoison.IndexOf(m);
                    curPoisonSpeeds[id] = poisonSpeed[0] + rnd.NextDouble() * (poisonSpeed[1] - poisonSpeed[0]);
                }
            }

            for (int k = 0; k < VBOpoison.Count; k++)
            {
                if (VBOpoison[k].ShowModel)
                {
                    VBOpoison[k].vetTransl.x -= curPoisonSpeeds[k] * (gameTime - prevGameTime);
                    if (VBOpoison[k].vetTransl.x < -poisonSize[0]) VBOpoison[k].ShowModel = false;
                }
            }
            #endregion

            //prevents in-game collision between models
            for (int k = 1; k < dg2d.Models.Count; k++)
            {
                for (int p = 0; p < k; p++)
                {
                    double dif = Math.Abs(dg2d.Models[k].vetTransl.x - dg2d.Models[p].vetTransl.x);
                    if (Math.Abs(dg2d.Models[k].vetTransl.y - dg2d.Models[p].vetTransl.y) < foodSize[1] && dif < foodSize[0])
                    {
                        double xCenter = 0.5 * (dg2d.Models[k].vetTransl.x + dg2d.Models[p].vetTransl.x);

                        if (dg2d.Models[k].vetTransl.x < dg2d.Models[p].vetTransl.x) dif = -dif;

                        dif = 0.10 * dif;
                        dg2d.Models[k].vetTransl.x += dif;
                        dg2d.Models[p].vetTransl.x -= dif;

                        double s1 = GetSpeed(dg2d.Models[k]);
                        double s2 = GetSpeed(dg2d.Models[p]);
                        double tiny = (s1 + s2) * 0.01;

                        SetSpeed(dg2d.Models[k], s2 + tiny);
                        SetSpeed(dg2d.Models[p], s1 + tiny);
                    }
                }
            }


            //Collision with bird
            double[] birdPos = charSprite.GetPosition();
            for (int k = 0; k < dg2d.Models.Count; k++)
            {
                double difx = Math.Abs(dg2d.Models[k].vetTransl.x - birdPos[0]);
                double dify = Math.Abs(dg2d.Models[k].vetTransl.y - birdPos[1]);

                if (dg2d.Models[k].ShowModel && difx < foodSize[0]*2 && dify < foodSize[1]) //collision
                {
                    if (VBOfood.IndexOf(dg2d.Models[k]) >= 0) Invoke(delIncPts);
                    else Invoke(delDecLife);

                    dg2d.Models[k].ShowModel = false;
                }
            }
        }

        private double GetSpeed(GLRender.GLVBOModel m)
        {
            int id1 = VBOpoison.IndexOf(m);

            if (id1 >= 0)
            {
                return curPoisonSpeeds[id1];
            }
            else
            {
                id1 = VBOfood.IndexOf(m);
                return curFoodSpeeds[id1];
            }
        }

        private void SetSpeed(GLRender.GLVBOModel m, double s)
        {
            int id1 = VBOpoison.IndexOf(m);

            if (id1 >= 0)
            {
                curPoisonSpeeds[id1] = s;
            }
            else
            {
                id1 = VBOfood.IndexOf(m);
                curFoodSpeeds[id1] = s;
            }
        }

        #endregion

        bool playAnim = false;
        System.Diagnostics.Stopwatch swGame = new System.Diagnostics.Stopwatch();

        /// <summary>Voice sample time in seconds</summary>
        double voiceSampleTime = 0.005;

        /// <summary>Next voice sample time</summary>
        double NextSampleTime = 0;

        private void bgWAnim_DoWork(object sender, DoWorkEventArgs e)
        {
            while (!initSound)
            {
            }
            
            swGame.Reset();
            swGame.Start();


            while (playAnim)
            {
                dg2d.curTime = swGame.Elapsed.TotalSeconds;

                //charSprite.SetPositionRotation(0.1f, 0.5 + 0.25 * Math.Sin(dg2d.curTime), 0.0f);
                if (gs != null && !gs.isPaused && swGame.Elapsed.TotalSeconds > NextSampleTime)
                {
                    NextSampleTime += voiceSampleTime;
                    gs.AcquireFreqSample();

                    int N = 30;
                    if (gs.acqFreqSamples.Count > N)
                    {
                        List<float> pitches = new List<float>();

                        for (int k = 0; k < N; k++) pitches.Add(gs.acqFreqSamples[gs.acqFreqSamples.Count - 1 - k].Y);
                        pitches.Sort();
                        curPitch = pitches[N >> 1];
                    }
                }

                doGameIter();

                gl2D.Invalidate();
            }
        }





    }


}
