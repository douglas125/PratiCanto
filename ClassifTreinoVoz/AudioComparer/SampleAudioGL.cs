using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using OpenTK.Graphics.OpenGL;
using System.Drawing;
using OpenCLTemplate.CLGLInterop;


namespace AudioComparer
{
    /// <summary>Drawings and HPC for audio</summary>
    public partial class SampleAudioGL
    {
        #region Configuration parameters

        /// <summary>Draw formants. Lines or points?</summary>
        public OpenTK.Graphics.OpenGL.BeginMode FORMANTSBEGINMODE = BeginMode.Points;

        /// <summary>Maximum number of formants to plot</summary>
        public int MAXFORMANTS = 3; //6; //4;

        /// <summary>Highlight harmonics?</summary>
        public static bool highlightHarmonics = false;

        /// <summary>Amplify F0 to make easier to analyze voice changes</summary>
        public float F0Amplification = 1;

        /// <summary>Maximum intensity to view</summary>
        public float IntensRange = 400;

        /// <summary>Maximum number of time points to show</summary>
        private static int MAXTIMEPTS = 3000;

        /// <summary>Maximum number of FFT graphs in real time</summary>
        private static int MAXRealTimePTSFFT = 1000;

        /// <summary>Maximum number of FFT points to keep</summary>
        private static int MAXFFTPtsKEEP = 500;

        /// <summary>View spectre in color?</summary>
        public bool ViewColorSpectre = false;
        /// <summary>View monochromatic spectre?</summary>
        public bool ViewBWSpectre = true;
        /// <summary>View fundamental frequency?</summary>
        public bool ViewFundFreq = true;
        /// <summary>View fundamental intensity?</summary>
        public bool ViewFundIntensity = true;
        /// <summary>View formants?</summary>
        public bool ViewFormants = false;
        /// <summary>View Harmonic to Noise Ratio?</summary>
        public bool ViewHNR = false;

        #endregion

        /// <summary>Sample audio to use</summary>
        public SampleAudio sampleAudio;
        /// <summary>Set Sample Audio to use</summary>
        /// <param name="sa">Sampleaudio</param>
        public void SetSampleAudio(SampleAudio sa)
        {
            if (sa == null) return;

            this.sampleAudio = sa;
            if (sa.audioSpectre == null) sa.RecomputeSpectre();
            p0SelX = 0; pfSelX = 0;
            ResetDrawingRanges();
            UpdateDrawing();
        }

        /// <summary>Display audio information</summary>
        public OpenTK.GLControl glIntens, glSpectre;
        /// <summary>Picture to display information</summary>
        public PictureBox pbInfoBar;

        /// <summary>Label containing info</summary>
        public Label lblInfo;

        /// <summary>Textbox containing annotations</summary>
        private ToolStripTextBox txtAnnotations;

        /// <summary>viewPort: [0] - xmin, [1] - xmax, [2] - ymin, [3] - ymax</summary>
        float[] GLIntensGraphRange, GLSpectreGraphRange;

        /// <summary>Constructor</summary>
        /// <param name="GLIntens">OpenGL control for intensity of audio</param>
        /// <param name="GLSpectre">OpenGL control for displaying spectre</param>
        /// <param name="pbInfoBar">Picturebox with information bar</param>
        /// <param name="lblInfo">Info Label. Set to null if not using</param>
        /// <param name="txtAnnotations">Textbox with annotations. Set to null if not using</param>
        public SampleAudioGL(OpenTK.GLControl GLIntens, OpenTK.GLControl GLSpectre, PictureBox pbInfoBar, Label lblInfo, ToolStripTextBox txtAnnotations)
        {
            this.lblInfo = lblInfo;
            this.txtAnnotations = txtAnnotations;
            GLInitDraw(GLIntens, GLSpectre, pbInfoBar);

            //Initialize annotations menu
            InitAnnotations();
            pbInfoBar.ContextMenu = cmAnnotations;
        }


        bool GLLoaded = false;

        #region OpenGL initialization

        /// <summary>Initializes Sample Audio OpenGL drawing</summary>
        /// <param name="GLIntens">OpenGL control for intensity of audio</param>
        /// <param name="GLSpectre">OpenGL control for displaying spectre</param>
        /// <param name="pbInfoBar">Picturebox with information bar</param>
        public void GLInitDraw(OpenTK.GLControl GLIntens, OpenTK.GLControl GLSpectre, PictureBox pbInfoBar)
        {
            this.pbInfoBar = pbInfoBar;
            this.glIntens = GLIntens;
            this.glSpectre = GLSpectre;

            glIntens.MakeCurrent();
            InitGL();

            glSpectre.MakeCurrent();
            InitGL();

            GLLoaded = true;
            GL.LineWidth(2);
            GL.PointSize(2);

            //GLIntensGraphRange = new float[] { 0, sa.time[sa.time.Length - 1], -10000, 10000 };
            //GLSpectreGraphRange = new float[] { 0, sa.time[sa.time.Length - 1], 0, 10000 };
            GLIntensGraphRange = new float[] { 0, 1, 0, 1 };
            GLSpectreGraphRange = new float[] { 0, 1, 0, (float)SampleAudio.maxKeepFreq };

            SetupViewPorts();

            //drawing
            glIntens.Paint += new PaintEventHandler(glIntens_Paint);
            glSpectre.Paint += new PaintEventHandler(glSpectre_Paint);
            pbInfoBar.Paint += new PaintEventHandler(pbInfoBar_Paint);

            //mouse selection
            glIntens.MouseDown += new MouseEventHandler(glIntens_MouseDown);
            glIntens.MouseUp += new MouseEventHandler(glIntens_MouseUp);
            glIntens.MouseMove += new MouseEventHandler(glIntens_MouseMove);
            glSpectre.MouseDown += new MouseEventHandler(glSpectre_MouseDown);
            glSpectre.MouseUp += new MouseEventHandler(glSpectre_MouseUp);
            glSpectre.MouseMove += new MouseEventHandler(glSpectre_MouseMove);
            glSpectre.MouseWheel += new MouseEventHandler(glSpectre_MouseWheel);
            pbInfoBar.MouseDown += new MouseEventHandler(pbInfoBar_MouseDown);
            pbInfoBar.MouseUp += new MouseEventHandler(pbInfoBar_MouseUp);
            pbInfoBar.MouseMove += new MouseEventHandler(pbInfoBar_MouseMove);

            //initializes 3D models
            int[] elems = new int[MAXTIMEPTS];
            for (int k = 0; k < MAXTIMEPTS; k++) elems[k] = k;

            //intensity graph
            VBOIntensGraph = new GLRender.GLVBOModel(BeginMode.LineStrip);
            VBOIntensGraph.ModelColor = new float[] { 0, 0, 0, 1 };
            VBOIntensGraph.SetElemData(elems);

            //spectre graph - MAXTIMEPTS x MAXFFTPtsKeep
            VBOSpectreGraph = new GLRender.GLVBOModel(BeginMode.Quads);
            int[] specElems = remakeSpecElems(MAXTIMEPTS, MAXFFTPtsKEEP);
            VBOSpectreGraph.SetElemData(specElems);
            GLspectreDim = specElems.Length;

            //fundamental frequency
            VBOFundFreq = new GLRender.GLVBOModel(BeginMode.LineStrip);
            VBOFundFreq.ModelColor = new float[] { 0.1f, 0.75f, 0.75f, 1 };
            VBOFundFreq.SetElemData(elems);

            //formants
            VBOFundFormants = new GLRender.GLVBOModel(FORMANTSBEGINMODE);
            VBOFundFormants.ModelColor = new float[] { 0.8f, 0, 0, 1 };
            VBOFundFormants.SetElemData(new int[] { 0, 1, 2, 3, 4 });

            //intensity
            VBOFundIntensity = new GLRender.GLVBOModel(BeginMode.LineStrip);
            VBOFundIntensity.ModelColor = new float[] { 0.77f, 0.77f, 0.0f, 1 };
            VBOFundIntensity.SetElemData(elems);

            //Harmonic to noise ratio
            VBOHNR = new GLRender.GLVBOModel(BeginMode.LineStrip);
            VBOHNR.ModelColor = new float[] { 0.0f, 0.5f, 0.0f, 1 };
            VBOHNR.SetElemData(elems);

            //spectre scale
            VBOFreqScale = new GLRender.GLVBOModel(BeginMode.Quads);
            VBOFreqScale.SetElemData(new int[] { 0, 1, 2, 3 });
            VBOFreqScale.ModelColor = new float[] { 1, 1, 1, 1 };
            VBOFreqScale.SetTexCoordData(new float[] { 0, 1, 1, 1, 1, 0, 0, 0 });
            VBOFreqScale.SetTexture(BuildVBOFreqScaleTexture());

            //selections
            VBOSelectIntens = new GLRender.GLVBOModel(BeginMode.Quads);
            VBOSelectIntens.SetElemData(new int[] { 0, 1, 2, 3 });
            VBOSelectIntens.ModelColor = new float[] { 0.2f, 1.0f, 0.2f, 0.22f };

            VBOSelectSpectre = new GLRender.GLVBOModel(BeginMode.Quads);
            VBOSelectSpectre.SetElemData(new int[] { 0, 1, 2, 3 });
            VBOSelectSpectre.ModelColor = new float[] { 0.2f, 1.0f, 0.2f, 0.22f };

            //current play time
            VBOCurTimeIntens = new GLRender.GLVBOModel(BeginMode.Lines);
            VBOCurTimeIntens.SetElemData(new int[] { 0, 1 });
            VBOCurTimeIntens.ModelColor = new float[] { 1, 0, 0, 1 };

            VBOCurTimeSpec = new GLRender.GLVBOModel(BeginMode.Lines);
            VBOCurTimeSpec.SetElemData(new int[] { 0, 1 });
            VBOCurTimeSpec.ModelColor = new float[] { 1, 0, 0, 1 };
        }


        /// <summary>Number of items in spectre</summary>
        int GLspectreDim;

        /// <summary>Remakes spectre element array</summary>
        /// <param name="timePts">Points in time</param>
        /// <param name="FreqPts">Points in frequency</param>
        private static int[] remakeSpecElems(int timePts, int FreqPts)
        {
            int[] specElems = new int[4 * (timePts - 1) * (FreqPts - 1)];
            for (int x = 0; x < timePts - 1; x++)
            {
                for (int y = 0; y < FreqPts - 1; y++)
                {
                    int idElem = 4 * (x + y * (timePts - 1));
                    int idVert = x + y * timePts;

                    specElems[idElem] = idVert;
                    specElems[idElem + 1] = idVert + 1;
                    specElems[idElem + 2] = idVert + 1 + timePts;
                    specElems[idElem + 3] = idVert + timePts;
                }
            }
            return specElems;
        }


        /// <summary>Initializes OpenGL</summary>
        public static void InitGL()
        {
            //AntiAliasing e blend
            GL.Enable(EnableCap.LineSmooth);
            GL.Hint(HintTarget.LineSmoothHint, HintMode.DontCare);
            GL.Enable(EnableCap.PolygonSmooth);
            GL.Hint(HintTarget.PolygonSmoothHint, HintMode.DontCare);

            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);


            //Z-Buffer
            GL.ClearDepth(1.0f);
            GL.DepthFunc(DepthFunction.Lequal);
            GL.Enable(EnableCap.DepthTest);
            GL.Hint(HintTarget.PerspectiveCorrectionHint, HintMode.DontCare);

            //Materiais, funcoes para habilitar cor
            GL.ColorMaterial(MaterialFace.FrontAndBack, ColorMaterialParameter.AmbientAndDiffuse); //tem q vir antes do enable
            GL.Enable(EnableCap.ColorMaterial);

            //// Create light components
            //float[] ambientLight = { 0.5f, 0.5f, 0.5f, 1.0f };
            //float[] diffuseLight = { 2.3f, 2.3f, 2.3f, 1.0f };
            //float[] specularLight = { 0.3f, 0.3f, 0.3f, 1.0f };
            //float[] position = { 0.0f, -40.0f, 0.0f, 1.0f };

            //// Assign created components to GL_LIGHT1
            //GL.Light(LightName.Light1, LightParameter.Ambient, ambientLight);
            //GL.Light(LightName.Light1, LightParameter.Diffuse, diffuseLight);
            //GL.Light(LightName.Light1, LightParameter.Specular, specularLight);
            //GL.Light(LightName.Light1, LightParameter.Position, position);

            //GL.Enable(EnableCap.Light1);// Enable Light One

            GL.ShadeModel(ShadingModel.Smooth);
            //GL.Enable(EnableCap.Lighting);

            //Normals recalculation
            GL.Enable(EnableCap.Normalize);

            //Textura
            GL.Enable(EnableCap.Texture2D);

            //Line and point sizes
            GL.LineWidth(1);
            //GL.PointSize(2);
        }
        
        #endregion

        #region Show annotations in spectrogram

        #region Interaction with annotation - mouse

        /// <summary>Selected annotation</summary>
        private GLAnnotation selectedAnnotation = null;

        /// <summary>Selects annotation when mouse goes over</summary>
        /// <param name="X">X mouse coord</param>
        /// <param name="Y">Y mouse coord</param>
        private void SelectAnnotationOnMouseOver(int X, int Y)
        {
            if (lstGLAnnot.Count == 0 || isclickMovingAnnot) return;
            
            GLAnnotation previousSelAnnotation = null;

            //check if mouse is over annotation
            float t, F;
            getMouseCoordsInTF(X, Y, out t, out F);

            if (selectedAnnotation != null)
            {
                previousSelAnnotation = selectedAnnotation;
                selectedAnnotation = null;
            }
            foreach (GLAnnotation gla in lstGLAnnot)
            {
                if (gla.origAnnotation.startTime <= t && t <= gla.origAnnotation.stopTime && gla.origAnnotation.startFreq <= F && F <= gla.origAnnotation.stopFreq)
                {
                    //mouse is over an annotation
                    selectedAnnotation = gla;
                    gla.VBOAnnot.ModelColor = new float[] { 1, 1, 0, 1 };
                    break;
                }
            }
            if (selectedAnnotation != previousSelAnnotation)
            {
                if (previousSelAnnotation != null) previousSelAnnotation.VBOAnnot.ModelColor = new float[] { 1, 1, 1, 1 };
                glIntens.Invalidate();
            }
        }

        bool isclickMovingAnnot = false;
        double f0AnnotSel;
        private void StartMovingAnnotation(int X, int Y)
        {
            float t, F;
            getMouseCoordsInTF(X, Y, out t, out F);

            isclickMovingAnnot = true;
            f0AnnotSel = F - selectedAnnotation.origAnnotation.startFreq;
        }
        private void StopMovingAnnotation(int X, int Y)
        {
            isclickMovingAnnot = false;
            sampleAudio.SaveAnnotations();
        }
        private void ContinueMovingAnnotation(int X, int Y)
        {
            float t, F;
            getMouseCoordsInTF(X, Y, out t, out F);

            if (isclickMovingAnnot && selectedAnnotation != null)
            {
                double delta = selectedAnnotation.origAnnotation.stopFreq - selectedAnnotation.origAnnotation.startFreq;
                selectedAnnotation.origAnnotation.startFreq = F - f0AnnotSel;
                if (selectedAnnotation.origAnnotation.startFreq < 0) selectedAnnotation.origAnnotation.startFreq = 0;
                selectedAnnotation.origAnnotation.stopFreq = selectedAnnotation.origAnnotation.startFreq + delta;
                selectedAnnotation.SetAnnotation(selectedAnnotation.origAnnotation);

                glSpectre.Invalidate();
            }
        }
        
        private void IncrementAnnotationFreqSpan(int ticks)
        {
            if (selectedAnnotation != null)
            {
                selectedAnnotation.origAnnotation.stopFreq += 0.5*ticks;
                selectedAnnotation.SetAnnotation(selectedAnnotation.origAnnotation);

                glSpectre.Invalidate();
                sampleAudio.SaveAnnotations();
            }
        }
        #endregion

        /// <summary>Annotations to show in OpenGL</summary>
        private List<GLAnnotation> lstGLAnnot = new List<GLAnnotation>();

        /// <summary>Draws annotations</summary>
        public void DrawAnnotations()
        {
            if (sampleAudio == null) return;
            foreach (GLAnnotation gla in lstGLAnnot) gla.DrawAnnotation = false;
            int lastGLAnnot = -1;

            //Remake annotations if needed
            for (int k = 0; k < sampleAudio.Annotations.Count; k++)
            {
                if (sampleAudio.Annotations[k].ShowInSpectrogram)
                {
                    lastGLAnnot++;
                    if (lstGLAnnot.Count <= lastGLAnnot)
                    {
                        lstGLAnnot.Add(new GLAnnotation(sampleAudio.Annotations[k]));

                    }
                    else lstGLAnnot[lastGLAnnot].SetAnnotation(sampleAudio.Annotations[k]);

                }
            }
        }

        /// <summary>Annotations class</summary>
        private class GLAnnotation
        {
            /// <summary>Last annotation used here</summary>
            public SampleAudio.AudioAnnotation origAnnotation;
            
            /// <summary>GL Object</summary>
            public GLRender.GLVBOModel VBOAnnot;

            /// <summary>Draw this annotation?</summary>
            public bool DrawAnnotation = true;

            /// <summary>Annotation text</summary>
            public string annotText;

            /// <summary>Constructor</summary>
            /// <param name="ann">Base annotation</param>
            public GLAnnotation(SampleAudio.AudioAnnotation ann)
            {
                VBOAnnot = new GLRender.GLVBOModel(BeginMode.Quads);
                VBOAnnot.ModelColor = new float[] { 1, 1, 1, 1 };

                SetAnnotation(ann);
            }

            /// <summary>Sets which annotation will be used</summary>
            /// <param name="ann">Annotation to use</param>
            public void SetAnnotation(SampleAudio.AudioAnnotation ann)
            {
                origAnnotation = ann;

                DrawAnnotation = true;
                VBOAnnot.SetElemData(new int[] { 0, 1, 2, 3 });
                VBOAnnot.SetTexCoordData(new float[] { 0, 1, 0, 0, 1, 0, 1, 1 });
                VBOAnnot.SetVertexData(new float[] 
                    {
                        (float)ann.startTime,(float)ann.startFreq,0,
                        (float)ann.startTime,(float)ann.stopFreq,0,
                        (float)ann.stopTime,(float)ann.stopFreq,0,
                        (float)ann.stopTime,(float)ann.startFreq,0,
                    }); 

                if (annotText == "" || annotText != ann.Text)
                {
                    annotText = ann.Text;
                    Bitmap bmpAnnot = buildAnnotImg(ann);
                    VBOAnnot.SetTexture(bmpAnnot);

                    

                    //VBOAnnot.SetVertexData(new float[] 
                    //{
                    //    0,0,0,
                    //    0,0+bmpAnnot.Height,0,
                    //    bmpAnnot.Width,0+bmpAnnot.Height,0,
                    //    bmpAnnot.Width,0,0,
                    //});
                }
            }

            /// <summary>Dispose of OpenGL objects</summary>
            public void Dispose()
            {
                VBOAnnot.Dispose();
            }

            /// <summary>Builds bitmap for annotation</summary>
            /// <param name="ann">Annotation</param>
            private Bitmap buildAnnotImg(SampleAudio.AudioAnnotation ann)
            {
                string annTxt = ann.Text.Replace("[2]", "");

                Bitmap bmp = new Bitmap(1, 1);
                Graphics g = Graphics.FromImage(bmp);
                SizeF s = g.MeasureString(annTxt, ann.annFont);

                Bitmap bmpRet = new Bitmap((int)(1+s.Width), (int)(1+s.Height));
                Graphics gg = Graphics.FromImage(bmpRet);

                gg.FillRectangle(Brushes.White, 0, 0, bmpRet.Width, bmpRet.Height);
                gg.DrawString(annTxt, ann.annFont, Brushes.Black, 0, 0);

                return bmpRet;
            }
        }

        #endregion

        #region Drawing and sampling

        /// <summary>Intensity graph</summary>
        GLRender.GLVBOModel VBOIntensGraph;
        /// <summary>Spectre</summary>
        GLRender.GLVBOModel VBOSpectreGraph;

        /// <summary>Display audio selection in intensity box</summary>
        GLRender.GLVBOModel VBOSelectIntens;
        /// <summary>Display audio selection in intensity box</summary>
        GLRender.GLVBOModel VBOSelectSpectre;

        /// <summary>Fundamental frequency graph</summary>
        GLRender.GLVBOModel VBOFundFreq;
        /// <summary>HNR graph</summary>
        GLRender.GLVBOModel VBOHNR;
        /// <summary>Intensity graph</summary>
        GLRender.GLVBOModel VBOFundIntensity;
        /// <summary>Formants!</summary>
        GLRender.GLVBOModel VBOFundFormants;

        /// <summary>Draw scale in frequency data</summary>
        GLRender.GLVBOModel VBOFreqScale;

        /// <summary>Current playback time in intensity graph</summary>
        GLRender.GLVBOModel VBOCurTimeIntens;
        /// <summary>Current playback time in spectre graph</summary>
        GLRender.GLVBOModel VBOCurTimeSpec;

        /// <summary>Updates drawings in OpenGL controls</summary>
        public void UpdateDrawing()
        {
            SampleAudioDataToVBO();
            SampleSpectreDataToVBO();
            SampleFundamentalFreqIntensFormantDataToVBO();
            GetVBOFreqScale();

            //Annotations
            DrawAnnotations();

            glIntens.Invalidate();
            glSpectre.Invalidate();
            pbInfoBar.Invalidate();
        }

        #region Realtime drawings during recording

        /// <summary>Real time spectrograms</summary>
        public List<double[]> realTimeSpectres = new List<double[]>();
        /// <summary>Real time waveform exhibit</summary>
        public List<double> realTimeSamples = new List<double>();
        /// <summary>Last index which was sampled</summary>
        public int realTimeLastSampleIdx = 0;

        /// <summary>Real time spectre colors</summary>
        float[] realTimeSpecColors;

        /// <summary>insert Position at sample</summary>
        int insertPosSample = 0;
        /// <summary>insert Position at fft</summary>
        int insertPosFFT = 0;

        /// <summary>Reset realtime drawings</summary>
        public void ResetRealTimeDrawing()
        {
            realTimeLastSampleIdx = 0;
            realTimeSamples.Clear();
            realTimeSpectres.Clear();
            insertPosSample = 0;
            insertPosFFT = 0;
            realTimeSpecColors = null;
        }

        /// <summary>Draws realtime data</summary>
        /// <param name="rt">Realtime data</param>
        public void DrawRealTime(SampleAudio.RealTime rt)
        {

            float deltaT = rt.time[1];
            int nSamples = 5;
            int stepFFT = (rt.audioReceived.Count - 1 - realTimeLastSampleIdx) / nSamples;
            int stepSampleExtra = MAXTIMEPTS / MAXRealTimePTSFFT;

            //samples to compute ffts
            float[] fftSamples = new float[SampleAudio.FFTSamples + MAXRealTimePTSFFT];
            int fftSamplesLen = SampleAudio.FFTSamples + MAXRealTimePTSFFT;
            int rtAudioRecCount = rt.audioReceived.Count;
            for (int k = 0; k < fftSamplesLen && k < rtAudioRecCount; k++)
            {
                int id = k + rtAudioRecCount - fftSamplesLen;
                if (id < 0) id = 0;
                fftSamples[k] = rt.audioReceived[id];
            }

            //initializes intensities if not acquired yet
            if (minMaxSpectre == null || minMaxSpectre[1] == 0) minMaxSpectre = new float[] { -130, 8 };// GetMinMaxSpectre(realTimeSpectres);
            //retrieve color samples
            if (realTimeSpecColors == null) realTimeSpecColors = new float[4 * MAXRealTimePTSFFT * MAXFFTPtsKEEP];


            float stepFreq = (float)rt.waveSource.WaveFormat.SampleRate / (float)SampleAudio.FFTSamples;
            int idMax = (int)(SampleAudio.maxKeepFreq / stepFreq);

            for (int sampleId = realTimeLastSampleIdx; sampleId < rtAudioRecCount - 1; sampleId += stepFFT)
            {
                for (int k = 0; k < stepSampleExtra; k++)
                {
                    int id = k * stepFFT / (stepSampleExtra - 1) - stepFFT + sampleId;
                    if (id < 0) id = 0;
                    realTimeSamples.Add(rt.audioReceived[id]);
                }

                int idFFT = sampleId - realTimeLastSampleIdx;

                if (idFFT >= fftSamples.Length) idFFT = fftSamples.Length - 1;

                double[] tempFFT;

                if (ViewColorSpectre || ViewBWSpectre) tempFFT = SampleAudio.computeAudioFFT(fftSamples, idFFT, idMax);
                else tempFFT = new double[idMax];
                realTimeSpectres.Add(tempFFT);

                if (realTimeSpectres.Count < MAXRealTimePTSFFT && (ViewColorSpectre || ViewBWSpectre) ) FillRealTimeSpecColors(realTimeSpectres.Count - 1, tempFFT.Length, realTimeSpecColors, tempFFT, minMaxSpectre, ViewColorSpectre, ViewBWSpectre);
            }

            //adjust viewport
            GLIntensGraphRange[0] = 0;
            GLIntensGraphRange[1] = MAXTIMEPTS * deltaT * stepSampleExtra * stepFFT;
            if (GLIntensGraphRange[2] == 0)
            {
                GLIntensGraphRange[2] = -15000;
                GLIntensGraphRange[3] = 15000;
            }
            GLSpectreGraphRange[0] = 0;
            GLSpectreGraphRange[1] = MAXTIMEPTS * deltaT * stepFFT;
            SetupViewPorts();

            //limit samples
            while (realTimeSamples.Count > MAXTIMEPTS)
            {
                double d = realTimeSamples[realTimeSamples.Count - 1];
                realTimeSamples.RemoveAt(insertPosSample);
                realTimeSamples.Insert(insertPosSample, d);
                insertPosSample++; if (insertPosSample >= MAXTIMEPTS - 1) insertPosSample = 0;

                //realTimeSamples.RemoveAt(0);
                realTimeSamples.RemoveAt(realTimeSamples.Count - 1);
            }

            while (realTimeSpectres.Count > MAXRealTimePTSFFT)
            {
                double[] s = realTimeSpectres[realTimeSpectres.Count - 1];
                realTimeSpectres.RemoveAt(insertPosFFT);
                realTimeSpectres.Insert(insertPosFFT, s);

                if (ViewColorSpectre || ViewBWSpectre) FillRealTimeSpecColors(insertPosFFT, realTimeSpectres[insertPosFFT].Length, realTimeSpecColors, realTimeSpectres[insertPosFFT], minMaxSpectre, ViewColorSpectre, ViewBWSpectre);

                insertPosFFT++; if (insertPosFFT >= MAXRealTimePTSFFT - 1) insertPosFFT = 0;

                //realTimeSpectres.RemoveAt(0);
                realTimeSpectres.RemoveAt(realTimeSpectres.Count - 1);
            }
            

            float[] samples = new float[3 * MAXTIMEPTS];


            //remake elems if needed
            int freqPts = realTimeSpectres[0].Length;
            if (GLspectreDim != 4 * (MAXRealTimePTSFFT - 1) * (freqPts - 1) || realTimeLastSampleIdx == 0)
            {
                int[] specElems = remakeSpecElems(MAXRealTimePTSFFT, freqPts);
                VBOSpectreGraph.SetElemData(specElems);
                GLspectreDim = specElems.Length;
            }

            int maxId = Math.Min(realTimeSamples.Count, MAXTIMEPTS);
            for (int k = 0; k < MAXTIMEPTS; k++)
            {
                int k3 = 3 * k;
                samples[k3] = deltaT * k * stepSampleExtra * stepFFT;
            }
            for (int k = 0; k < maxId ; k++)
            {
                int k3 = 3 * k;
                samples[k3 + 1] = (float)realTimeSamples[k];
            }
            VBOIntensGraph.SetVertexData(samples);
            glIntens.Invalidate();

            if (ViewColorSpectre || ViewBWSpectre)
            {
                maxId = Math.Min(realTimeSpectres.Count, MAXRealTimePTSFFT);
                if (realTimeLastSampleIdx == 0)
                {
                    float[] specVertexes = new float[3 * MAXRealTimePTSFFT * MAXFFTPtsKEEP];

                    for (int k = 0; k < MAXRealTimePTSFFT; k++)
                    {
                        float scaley = 1.0f / (float)(freqPts - 1);
                        for (int y = 0; y < freqPts; y++)
                        {
                            int idVert = 3 * (k + y * MAXRealTimePTSFFT);
                            specVertexes[idVert] = deltaT * k * stepSampleExtra * stepFFT;
                            specVertexes[idVert + 1] = (float)y * scaley * (float)SampleAudio.maxKeepFreq;
                        }
                    }
                    VBOSpectreGraph.SetVertexData(specVertexes);
                }


                //for (int x = 0; x < maxId; x++)
                //{
                //    FillRealTimeSpecColors(x, freqPts, realTimeSpecColors, realTimeSpectres[x], minMaxSpectre, ViewColorSpectre, ViewBWSpectre);
                //}
                VBOSpectreGraph.SetColorData(realTimeSpecColors);
                glSpectre.Invalidate();
            }

            realTimeLastSampleIdx = rt.audioReceived.Count;
        }

        /// <summary>Fills a certain column of specColors</summary>
        private static void FillRealTimeSpecColors(int x, int freqPts, float[] specColors, double[] specIntens, float[] minMaxSpectre, bool viewColorSpectre, bool viewBWSpectre)
        {
            for (int y = 0; y < freqPts; y++)
            {
                int idColor = 4 * (x + y * MAXRealTimePTSFFT);

                List<float> smoothSpectre = AdjustForVisualization(specIntens, minMaxSpectre);

                float[] c = new float[] { 1, 1, 1, 1 };
                if (viewColorSpectre) c = SampleAudio.FcnGetColor((float)smoothSpectre[y], minMaxSpectre[0], minMaxSpectre[1]);
                else if (viewBWSpectre) c = SampleAudio.FcnGetBWColor((float)smoothSpectre[y], minMaxSpectre[0], minMaxSpectre[1]);
                specColors[idColor] = c[0];
                specColors[idColor + 1] = c[1];
                specColors[idColor + 2] = c[2]; // 0.0f;
                specColors[idColor + 3] = 1.0f;

            }
        }

        #endregion


        /// <summary>Draw current playback time?</summary>
        public bool DrawCurrentPlaybackTime = false;
        /// <summary>Draw current playback time</summary>
        /// <param name="time">Time to draw</param>
        /// <param name="invalidateSpectre">Invalidate spectre to force redraw?</param>
        public void DrawCurPbTime(float time, bool invalidateSpectre)
        {
            if (sampleAudio == null) return;
            VBOCurTimeIntens.SetVertexData(new float[] 
            {
                time, GLIntensGraphRange[2],0.1f,
                time, GLIntensGraphRange[3],0.1f,
            });

            VBOCurTimeSpec.SetVertexData(new float[] 
            {
                time, GLSpectreGraphRange[2],0.1f,
                time, GLSpectreGraphRange[3],0.1f,
            });
            glIntens.Invalidate();
            if (invalidateSpectre) glSpectre.Invalidate();
        }


        #region Drawing ranges and viewport
        /// <summary>Resets drawing ranges to view all data</summary>
        public void ResetDrawingRanges()
        {
            if (sampleAudio == null) return;
            float imax = float.MinValue;
            float imin = float.MaxValue;
            foreach (float ff in sampleAudio.audioData)
            {
                imax = Math.Max(ff, imax);
                imin = Math.Min(ff, imin);
            }
            GLIntensGraphRange[0] = sampleAudio.time[0];
            GLIntensGraphRange[1] = sampleAudio.time[sampleAudio.time.Length - 1];
            GLIntensGraphRange[2] = imin;
            GLIntensGraphRange[3] = imax;

            GLSpectreGraphRange[0] = sampleAudio.time[0];
            GLSpectreGraphRange[1] = sampleAudio.time[sampleAudio.time.Length - 1];
            
            SetupViewPorts();

            //retrieves minimum and maximum intensity of spectre
            if (sampleAudio != null) minMaxSpectre = GetMinMaxSpectre(sampleAudio.audioSpectre);

            VBOFreqScale.SetTexture(BuildVBOFreqScaleTexture());
        }

        /// <summary>Setup view ports after resize</summary>
        public void SetupViewPorts()
        {
            SetupViewport(glIntens, GLIntensGraphRange);
            SetupViewport(glSpectre, GLSpectreGraphRange);
            pbInfoBar.Invalidate();
        }

        /// <summary>Adjusts viewport of GL control</summary>
        /// <param name="c">Control to adjust</param>
        /// <param name="GraphRange">Desired graph range to display in control</param>
        public static void SetupViewport(OpenTK.GLControl c, float[] GraphRange)
        {
            c.MakeCurrent();

            int w = c.Width;
            int h = c.Height;

            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();

            GL.Enable(EnableCap.Blend);

            GL.Ortho(GraphRange[0], GraphRange[1], GraphRange[2], GraphRange[3], -1, 1); // Bottom-left corner pixel has coordinate (0, 0)

            GL.Viewport(0, 0, w, h); // Use all of the glControl painting area

            c.Invalidate();
        }

        #endregion

        #region Paint events
        void glIntens_Paint(object sender, PaintEventArgs e)
        {
            if (!GLLoaded) return;
            OpenTK.GLControl glg = (OpenTK.GLControl)sender;
            glg.MakeCurrent();
            GL.ClearColor(Color.White);

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadIdentity();

            VBOIntensGraph.DrawModel();

            if (DrawCurrentPlaybackTime) VBOCurTimeIntens.DrawModel();

            if (p0SelX != pfSelX) VBOSelectIntens.DrawModel();

            glg.SwapBuffers();
        }

        void glSpectre_Paint(object sender, PaintEventArgs e)
        {
            if (!GLLoaded) return;
            OpenTK.GLControl glg = (OpenTK.GLControl)sender;
            glg.MakeCurrent();
            GL.ClearColor(Color.White);

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadIdentity();

            //VBOIntensGraph.DrawModel();
            if (ViewBWSpectre || ViewColorSpectre) VBOSpectreGraph.DrawModel();

            if (ViewHNR) VBOHNR.DrawModel();
            if (ViewFundFreq) VBOFundFreq.DrawModel();
            if (ViewFundIntensity) VBOFundIntensity.DrawModel();
            if (ViewFormants) VBOFundFormants.DrawModel();

            foreach (GLAnnotation gla in lstGLAnnot) if (gla.DrawAnnotation) gla.VBOAnnot.DrawModel();

            //dont draw scale when recording
            if (realTimeLastSampleIdx == 0) VBOFreqScale.DrawModel();

            if (DrawCurrentPlaybackTime) VBOCurTimeSpec.DrawModel();

            if (p0SelX != pfSelX) VBOSelectSpectre.DrawModel();

            glg.SwapBuffers();
        }

        Font f = new Font("Arial", 11, FontStyle.Regular);
        void pbInfoBar_Paint(object sender, PaintEventArgs e)
        {
            if (!GLLoaded) return;

            float scalex = pbInfoBar.Width / (GLIntensGraphRange[1] - GLIntensGraphRange[0]);

            #region annotations

            if (sampleAudio != null)
            {
                foreach (SampleAudio.AudioAnnotation saann in sampleAudio.Annotations)
                {
                    PointF[] annCoords = GetAnnotationCoords(saann);

                    float p0Ann = annCoords[0].X;
                    float pfAnn = annCoords[1].X;
                    if ((p0Ann < 0 && pfAnn < 0) || (p0Ann > pbInfoBar.Width - 1 && pfAnn < pbInfoBar.Width - 1))
                    {
                        //outside of bounds
                    }
                    else
                    {
                        //start and stop heights, for double line draw
                        float h0draw = annCoords[0].Y;
                        float hfdraw = annCoords[1].Y;
                        string drawText = saann.Text;

                        if (saann.Text.StartsWith("[2]")) drawText = drawText.Replace("[2]", "");

                        Brush bb = Brushes.LightBlue;
                        if (selAnnotation != null && selAnnotation == saann) bb = Brushes.Yellow;

                        e.Graphics.FillRectangle(bb, p0Ann, h0draw, pfAnn - p0Ann, hfdraw - h0draw);
                        e.Graphics.DrawLine(Pens.Black, p0Ann, h0draw, p0Ann, hfdraw);
                        e.Graphics.DrawLine(Pens.Black, pfAnn, h0draw, pfAnn, hfdraw);
                        e.Graphics.DrawLine(Pens.Black, p0Ann, h0draw, pfAnn, h0draw);
                        e.Graphics.DrawLine(Pens.Black, p0Ann, hfdraw, pfAnn, hfdraw);

                        SizeF s = e.Graphics.MeasureString(drawText, f);

                        e.Graphics.DrawString(drawText, f, Brushes.Black, (p0Ann + pfAnn - s.Width) * 0.5f, h0draw + 0.5f * (hfdraw - h0draw - s.Height));
                    }
                }
            }

            #endregion

            #region selection
            if (p0SelX != pfSelX)
            {
                //display information in bar
                float p0Bar = (p0SelX - GLIntensGraphRange[0]) * scalex;
                float pfBar = (pfSelX - GLIntensGraphRange[0]) * scalex;

                if ((p0Bar < 0 && pfBar < 0) || (p0Bar > pbInfoBar.Width - 1 && pfBar < pbInfoBar.Width - 1))
                {
                    //outside of bounds
                }
                else
                {
                    Color c = Color.FromArgb(210, 0, 255, 0);
                    e.Graphics.FillRectangle(new SolidBrush(c), p0Bar, 0, pfBar - p0Bar, pbInfoBar.Height);

                    string timetxt = Math.Round(Math.Abs(pfSelX - p0SelX), 4).ToString() + " s";
                    SizeF s = e.Graphics.MeasureString(timetxt, f);

                    e.Graphics.DrawString(timetxt, f, Brushes.Black, (p0Bar + pfBar - s.Width) * 0.5f, 0.5f * (pbInfoBar.Height - s.Height));
                }
            }
            #endregion

        }

        #region Edit annotations



        /// <summary>Receive information bar item texts, for localization</summary>
        public static string InfoBarItems = 
@"&Select
&Replace text with textbox
R&emove";

        ContextMenu cmAnnotations;
        private void InitAnnotations()
        {
            cmAnnotations = new ContextMenu();
            cmAnnotations.Popup += new EventHandler(cmAnnotations_Popup);
        }

        void cmAnnotations_Popup(object sender, EventArgs e)
        {
            if (selAnnotation != null) BuildAnnotationsMenu();
            else cmAnnotations.MenuItems.Clear();
        }


        /// <summary>Currently selected annotation</summary>
        private SampleAudio.AudioAnnotation selAnnotation = null;

        /// <summary>Is mouse close to annotation Right?</summary>
        private bool CloseToAnnotationRight = false;

        /// <summary>Is mouse close to annotation Left?</summary>
        private bool CloseToAnnotationLeft = false;

        /// <summary>Selects annotation at coordinate X, Y</summary>
        private void SelectAnnotation(int X, int Y)
        {
            if (sampleAudio == null || sampleAudio.Annotations == null) return;

            SampleAudio.AudioAnnotation curAnnotation = null;
            pbInfoBar.Cursor = Cursors.Cross;
            CloseToAnnotationLeft = false; CloseToAnnotationRight = false;
            foreach (SampleAudio.AudioAnnotation ann in sampleAudio.Annotations)
            {
                PointF[] coords = GetAnnotationCoords(ann);
                if (coords[0].X <= X && coords[0].Y <= Y && X <= coords[1].X && Y <= coords[1].Y)
                {
                    curAnnotation = ann;

                    if (X - coords[0].X < 10)
                    {
                        CloseToAnnotationLeft = true;
                        pbInfoBar.Cursor = Cursors.PanWest;
                    }
                    else if (coords[1].X - X < 10)
                    {
                        CloseToAnnotationRight = true;
                        pbInfoBar.Cursor = Cursors.PanEast;
                    }

                    break;
                }
            }

            bool needsRedraw = false;
            needsRedraw = (curAnnotation != selAnnotation);
            
            selAnnotation = curAnnotation;
            if (needsRedraw) pbInfoBar.Invalidate();
        }

        /// <summary>Retrieves screen coordinates where annotation ann will be drawn. Returns rectangle points</summary>
        /// <param name="ann">Audio annotation</param>
        /// <returns></returns>
        private PointF[] GetAnnotationCoords(SampleAudio.AudioAnnotation ann)
        {
            float scalex = pbInfoBar.Width / (GLIntensGraphRange[1] - GLIntensGraphRange[0]);

            float x0Ann = ((float)ann.startTime - GLIntensGraphRange[0]) * scalex;
            float xfAnn = ((float)ann.stopTime - GLIntensGraphRange[0]) * scalex;

            float y0Ann, yfAnn;
            if (ann.Text.StartsWith("[2]"))
            {
                y0Ann = pbInfoBar.Height >> 1;
                yfAnn = pbInfoBar.Height;
            }
            else
            {
                y0Ann = 0;
                yfAnn = pbInfoBar.Height >> 1;
            }

            return new PointF[] { new PointF(x0Ann, y0Ann), new PointF(xfAnn, yfAnn) };
        }

        #region Move annotation

        bool ClickMoveAnnotation = false;
        float x0RedimAnnot = 0;
        float origTime = 0;
        private void StartResizeAnnotation(int x0)
        {
            ClickMoveAnnotation = true;
            x0RedimAnnot = x0;
            if (CloseToAnnotationLeft) origTime = (float)selAnnotation.startTime;
            else origTime = (float)selAnnotation.stopTime;
        }
        private void ContinueResizeAnnotation(int x)
        {
            if (!ClickMoveAnnotation) return;

            float deltaT = (GLIntensGraphRange[1] - GLIntensGraphRange[0]) * ((float)x - x0RedimAnnot) / (float)glIntens.Width;

            if (CloseToAnnotationLeft) selAnnotation.startTime = origTime + deltaT;
            else selAnnotation.stopTime = origTime + deltaT;
            SelectRegion((float)selAnnotation.startTime, (float)selAnnotation.stopTime);
        }
        private void StopResizeAnnotation()
        {
            ClickMoveAnnotation = false;
            try { sampleAudio.SaveAnnotations(); }
            catch { }
            DrawAnnotations();
            glSpectre.Invalidate();
        }

        #endregion

        #region Annotations floating menu
        private void BuildAnnotationsMenu()
        {
            cmAnnotations.MenuItems.Clear();
            string[] items = InfoBarItems.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);

            MenuItem menuSelect = new MenuItem(items[0]);
            menuSelect.Click += new EventHandler(menuSelect_Click);
            cmAnnotations.MenuItems.Add(menuSelect); //Select

            if (txtAnnotations != null)
            {
                MenuItem menuReplace = new MenuItem(items[1]);
                cmAnnotations.MenuItems.Add(menuReplace); //Replace text with textbox
                menuReplace.Click += new EventHandler(menuReplace_Click);
            }

            cmAnnotations.MenuItems.Add("-");

            MenuItem menuDel = new MenuItem(items[2]);
            menuDel.Click += new EventHandler(menuDel_Click);
            cmAnnotations.MenuItems.Add(menuDel); //Remove

            MenuItem menuShowSpectrogram = new MenuItem(items[3]);
            menuShowSpectrogram.Click+=new EventHandler(menuShowSpectrogram_Click);
            cmAnnotations.MenuItems.Add(menuShowSpectrogram); //Show/hide in spectrogram


            MenuItem menuFontSpectrogram = new MenuItem(items[4]);
            menuFontSpectrogram.Click += new EventHandler(menuFontSpectrogram_Click);
            cmAnnotations.MenuItems.Add(menuFontSpectrogram); //Show/hide in spectrogram
        }

        void menuFontSpectrogram_Click(object sender, EventArgs e)
        {
            FontDialog fd = new FontDialog();
            fd.Font = selAnnotation.annFont;
            fd.ShowDialog();
            selAnnotation.annFont = fd.Font;
            foreach (GLAnnotation gla in lstGLAnnot)
            {
                if (gla.origAnnotation == selAnnotation)
                {
                    gla.annotText = "";
                    gla.SetAnnotation(gla.origAnnotation);
                    sampleAudio.SaveAnnotations();
                    glSpectre.Invalidate();
                }
            }
        }

        void  menuShowSpectrogram_Click(object sender, EventArgs e)
        {
            selAnnotation.ShowInSpectrogram = !selAnnotation.ShowInSpectrogram;

            DrawAnnotations();
            glSpectre.Invalidate();
        }

        void menuDel_Click(object sender, EventArgs e)
        {
            sampleAudio.Annotations.Remove(selAnnotation);
            try { sampleAudio.SaveAnnotations(); }
            catch { }
            selAnnotation = null;
            pbInfoBar.Invalidate();
        }

        void menuReplace_Click(object sender, EventArgs e)
        {
            string replaceTxt = txtAnnotations.Text;

            if (replaceTxt.StartsWith("[1]")) replaceTxt = replaceTxt.Replace("[1]", "");
            else if (selAnnotation.Text.StartsWith("[2]") && !replaceTxt.StartsWith("[2]")) replaceTxt = "[2]" + replaceTxt;

            selAnnotation.Text = replaceTxt;
            try { sampleAudio.SaveAnnotations(); }
            catch { }
            pbInfoBar.Invalidate();

            foreach (GLAnnotation gla in lstGLAnnot)
            {
                if (gla.origAnnotation == selAnnotation)
                {
                    gla.SetAnnotation(gla.origAnnotation);
                    glSpectre.Invalidate();
                }
            }
        }

        void menuSelect_Click(object sender, EventArgs e)
        {
             SelectRegion((float)selAnnotation.startTime, (float)selAnnotation.stopTime);
        }
        #endregion


        #endregion



        #endregion

        #region Sampling

        /// <summary>Retrieves texture with text containing scale in frequency</summary>
        private void GetVBOFreqScale()
        {
            float w = 0.028f * (GLSpectreGraphRange[1] - GLSpectreGraphRange[0]);
            float[] vertexes = new float[] 
            {
                GLSpectreGraphRange[0],   GLSpectreGraphRange[2],0.1f,
                GLSpectreGraphRange[0]+w, GLSpectreGraphRange[2],0.1f,
                GLSpectreGraphRange[0]+w, (float)SampleAudio.maxKeepFreq,0.1f,
                GLSpectreGraphRange[0],   (float)SampleAudio.maxKeepFreq,0.1f,
            };
            VBOFreqScale.SetVertexData(vertexes);
        }

        /// <summary>Generate texture to be used in scale</summary>
        private Bitmap BuildVBOFreqScaleTexture()
        {
            int w = (int)(0.028 * glSpectre.Width); if (w < 1) w = 1;
            int h = glSpectre.Height;
            if (w <= 0) w = 1; if (h <= 0) h = 1;

            Bitmap ans = new Bitmap(w, h); 

            Graphics g = Graphics.FromImage(ans);
            g.FillRectangle(Brushes.White,0,0,ans.Width,ans.Height);
            AddMarkers(g, ans.Height, 40, 0, SampleAudio.maxKeepFreq, "Hz", 0);

            return ans;
        }
        /// <summary>Add markers to a graph</summary>
        /// <param name="g">Graph to add graph to</param>
        /// <param name="dimension">Dimention in use: height or width</param>
        /// <param name="nMarks">Number of marks to add</param>
        /// <param name="min">Minimum value in graph</param>
        /// <param name="max">Maximum value in graph</param>
        /// <param name="offsetX">X offset in pixels</param>
        /// <param name="unit">Unit string</param>
        private static void AddMarkers(Graphics g, int dimension, int nMarks, double min, double max, string unit, int offsetX)
        {
            double stepfreq = (max - min) / nMarks;
            double curVal = min;

            double step = (double)dimension / nMarks;
            int curY = dimension - 1;

            for (int k = 0; k < nMarks; k++)
            {
                Pen p = new Pen(Brushes.Red, 3);
                g.DrawLine(p, offsetX, curY, offsetX + 7, curY);
                g.DrawString(Math.Round(curVal).ToString() + unit, new Font("Arial", 8), Brushes.Red, offsetX + 8, curY - 7);

                curY -= (int)step;
                curVal += stepfreq;
            }
        }

        /// <summary>Extracts sample of fundamental frequency to VBO using current viewport. Samples F0, intensity, HNR, formants</summary>
        private void SampleFundamentalFreqIntensFormantDataToVBO()
        {
            if (sampleAudio == null) return;
            //fetches indexes
            int idx0 = (int)(GLIntensGraphRange[0] / sampleAudio.baseFreqsTimes[1]);
            int idxf = (int)(GLIntensGraphRange[1] / sampleAudio.baseFreqsTimes[1]);
            if (idxf > sampleAudio.audioSpectre.Count - 1) idxf = sampleAudio.audioSpectre.Count - 1;

            float[] vertFundFreq = new float[3 * MAXTIMEPTS];
            float[] vertFundHNR = new float[3 * MAXTIMEPTS];
            float[] vertFundIntens = new float[3 * MAXTIMEPTS];

            //samples MAXTIMEPTS points. May sample the same one multiple times if zoom is too big
            float scale = 1.0f / (MAXTIMEPTS - 1.0f);
            for (int k = 0; k < MAXTIMEPTS; k++)
            {
                int curIdx = (int)(idx0 + (idxf - idx0) * (float)k * scale);
                int k3 = 3 * k;

                vertFundFreq[k3] = sampleAudio.baseFreqsTimes[curIdx];
                vertFundFreq[k3 + 1] = sampleAudio.baseFreqsF0[curIdx] * F0Amplification;

                vertFundIntens[k3] = sampleAudio.baseFreqsTimes[curIdx];
                vertFundIntens[k3 + 1] = sampleAudio.baseFreqsIntensity[curIdx] * (((float)SampleAudio.maxKeepFreq - GLSpectreGraphRange[2])) / IntensRange;

                vertFundHNR[k3] = sampleAudio.baseFreqsTimes[curIdx];
                vertFundHNR[k3 + 1] = sampleAudio.baseHNR[curIdx] * 100.0f;
            }
            VBOFundIntensity.SetVertexData(vertFundIntens);
            VBOFundFreq.SetVertexData(vertFundFreq);
            VBOHNR.SetVertexData(vertFundHNR);

            //samples formants
            List<float> vertFormants = new List<float>();
            List<float> colorFormants = new List<float>();
            List<int> elemFormants = new List<int>();
            for (int idFormant = 1; idFormant < MAXFORMANTS + 1; idFormant++)
            {
                for (int k = 0; k < MAXTIMEPTS; k++)
                {
                    int curIdx = (int)(idx0 + (idxf - idx0) * (float)k * scale);
                    int nextIdx = (int)(idx0 + (idxf - idx0) * (float)(1 + k) * scale);
                    int k3 = 3 * k;
                    if (nextIdx > sampleAudio.baseFormants.Count - 1) break;

                    if (sampleAudio.baseFormants[curIdx].Count > idFormant && sampleAudio.baseFormants[nextIdx].Count > idFormant)
                    {
                        vertFormants.Add(sampleAudio.baseFreqsTimes[curIdx]);
                        vertFormants.Add((float)sampleAudio.baseFormants[curIdx][idFormant].Frequency);
                        vertFormants.Add(0);

                        float r = (float)sampleAudio.baseFormants[curIdx][idFormant].relIntensity;
                        if (r > 1) r = 1;
                        colorFormants.Add(r);
                        colorFormants.Add(0); colorFormants.Add(0); colorFormants.Add(r);

                        if (VBOFundFormants.DrawMode == BeginMode.Lines)
                        {
                            //add these back if drawing lines - needs formant TRACKING
                            vertFormants.Add(sampleAudio.baseFreqsTimes[nextIdx]);
                            vertFormants.Add((float)sampleAudio.baseFormants[nextIdx][idFormant].Frequency);
                            vertFormants.Add(0);

                            r = (float)sampleAudio.baseFormants[nextIdx][idFormant].relIntensity;
                            if (r > 1) r = 1;
                            colorFormants.Add(r);
                            colorFormants.Add(0); colorFormants.Add(0); colorFormants.Add(r);

                            elemFormants.Add(elemFormants.Count);
                        }
                        elemFormants.Add(elemFormants.Count);
                    }

                }
            }
            VBOFundFormants.SetElemData(elemFormants.ToArray());
            VBOFundFormants.SetVertexData(vertFormants.ToArray());
            VBOFundFormants.SetColorData(colorFormants.ToArray());
        }


        /// <summary>Extracts sample of audio data to VBO using current viewport</summary>
        private void SampleAudioDataToVBO()
        {
            if (sampleAudio == null || sampleAudio.time == null) return;

            //fetches indexes
            int idx0 = (int)(GLIntensGraphRange[0] / sampleAudio.time[1]); if (idx0 < 0) idx0 = 0;
            int idxf = (int)(GLIntensGraphRange[1] / sampleAudio.time[1]); if (idxf >= sampleAudio.time.Length) idxf = sampleAudio.time.Length - 1;
            if (idxf > sampleAudio.time.Length - 1) idxf = sampleAudio.time.Length - 1;


            float[] vertexes = new float[3 * MAXTIMEPTS];

            //samples MAXTIMEPTS points. May sample the same one multiple times if zoom is too big
            float scale = 1.0f / (MAXTIMEPTS - 1.0f);
            for (int k = 0; k < MAXTIMEPTS; k++)
            {
                int curIdx = (int)(idx0 + (idxf - idx0) * (float)k * scale);
                if (curIdx > sampleAudio.time.Length - 1) curIdx = sampleAudio.time.Length - 1;

                int k3 = 3 * k;

                vertexes[k3] = sampleAudio.time[curIdx];
                vertexes[k3 + 1] = sampleAudio.audioData[curIdx];
            }

            VBOIntensGraph.SetVertexData(vertexes);
        }

        /// <summary>Minimum and maximum intensities of spectre</summary>
        private float[] minMaxSpectre;
        /// <summary>Extracts sample of spectre to VBO using current viewport</summary>
        private void SampleSpectreDataToVBO()
        {
            if (sampleAudio == null) return;
            if (sampleAudio.audioSpectre == null) sampleAudio.RecomputeSpectre();

            //fetches indexes
            int idx0 = (int)(GLIntensGraphRange[0] / sampleAudio.baseFreqsTimes[1]);
            int idxf = (int)(GLIntensGraphRange[1] / sampleAudio.baseFreqsTimes[1]);
            if (idxf > sampleAudio.audioSpectre.Count - 1)
                idxf = sampleAudio.audioSpectre.Count - 1;

            float stepFreq = (float)sampleAudio.waveFormat.SampleRate / (float)SampleAudio.FFTSamples;

            SampleSpectreToVBO(sampleAudio.audioSpectre, sampleAudio.baseFreqsTimes, sampleAudio.baseFreqsF0, stepFreq, GLspectreDim, idx0, idxf, VBOSpectreGraph, ViewColorSpectre, ViewBWSpectre, minMaxSpectre);
        }

        private static void SampleSpectreToVBO(List<float[]> audioSpectre,List<float> baseTimes,List<float> baseFreqs, float stepFreq, int GLspectreDim, int idx0, int idxf, 
            GLRender.GLVBOModel VBOSpectreGraph, bool ViewColorSpectre, bool ViewBWSpectre, float[] minMaxSpectre)
        {
            float[] specVertexes = new float[3 * MAXTIMEPTS * MAXFFTPtsKEEP];
            float[] specColors = new float[4 * MAXTIMEPTS * MAXFFTPtsKEEP];

            //remake elems if needed
            int freqPts = audioSpectre[0].Length;
            if (GLspectreDim != 4 * (MAXTIMEPTS - 1) * (freqPts - 1))
            {
                int[] specElems = remakeSpecElems(MAXTIMEPTS, freqPts);
                VBOSpectreGraph.SetElemData(specElems);
                GLspectreDim = specElems.Length;
            }
            float scalex = 1.0f / (MAXTIMEPTS - 1.0f);
            float scaley = 1.0f / (float)(freqPts - 1);


            for (int x = 0; x < MAXTIMEPTS; x++)
            {
                int curIdx = (int)(idx0 + (idxf - idx0) * (float)x * scalex);

                List<float> smoothSpectre = AdjustForVisualization(audioSpectre[curIdx], minMaxSpectre);

                for (int y = 0; y < freqPts; y++)
                {
                    int idColor = 4 * (x + y * MAXTIMEPTS);
                    int idVert = 3 * (x + y * MAXTIMEPTS);

                    specVertexes[idVert] = baseTimes[curIdx];
                    specVertexes[idVert + 1] = (float)y * scaley * (float)SampleAudio.maxKeepFreq;

                    float[] c = new float[] { 1, 1, 1, 1 };
                    //if (ViewColorSpectre) c = SampleAudio.FcnGetColor(audioSpectre[curIdx][y], minMaxSpectre[0], minMaxSpectre[1]);
                    //else if (ViewBWSpectre) c = SampleAudio.FcnGetBWColor(audioSpectre[curIdx][y], minMaxSpectre[0], minMaxSpectre[1]);
                    if (ViewColorSpectre) c = SampleAudio.FcnGetColor(smoothSpectre[y], minMaxSpectre[0], minMaxSpectre[1]);
                    else if (ViewBWSpectre) c = SampleAudio.FcnGetBWColor(smoothSpectre[y], minMaxSpectre[0], minMaxSpectre[1]);

                    //regular colors
                    specColors[idColor] = c[0];
                    specColors[idColor + 1] = c[1];
                    specColors[idColor + 2] = c[2]; // 0.0f;
                    specColors[idColor + 3] = 1.0f;

                    if (highlightHarmonics && baseFreqs != null && baseFreqs[curIdx] > 0 && ViewBWSpectre)
                    {
                        float curFreq = y * stepFreq;
                        float closestHarmonic = (float)Math.Round(curFreq / baseFreqs[curIdx]) * baseFreqs[curIdx];

                        //if (Math.Abs(curFreq - closestHarmonic) <= stepFreq*1.1f && curFreq > 60)
                        if (Math.Abs(curFreq - closestHarmonic) < 0.21f * baseFreqs[curIdx] && curFreq > 60)
                        {
                            specColors[idColor + 2] = 1; // 0.0f;
                        }
                    }

                }
            }
            VBOSpectreGraph.SetVertexData(specVertexes);
            VBOSpectreGraph.SetColorData(specColors);
        }

        /// <summary>Adjusts data for better visualization</summary>
        private static List<float> AdjustForVisualization(float[] data, float[] minMax)
        {
            List<float> ans = new List<float>();
            float scale = 1.0f / (minMax[1] - minMax[0]);
            foreach (float ff in data)
            {
                float correction = (ff - minMax[0]) * scale; //0 to 1
                //correction = ((float)Math.Exp(correction)-1) * 0.68197670f;
                correction *= correction * correction;
                //correction *= correction;
                ans.Add(correction * (minMax[1] - minMax[0]) + minMax[0]);
            }
            return ans;
        }
        /// <summary>Adjusts data for better visualization</summary>
        private static List<float> AdjustForVisualization(double[] data, float[] minMax)
        {
            List<float> ans = new List<float>();
            float scale = 1.0f / (minMax[1] - minMax[0]);
            foreach (double ff in data)
            {
                float correction = (float)(ff - minMax[0]) * scale; //0 to 1
                //correction = ((float)Math.Exp(correction)-1) * 0.68197670f;
                correction *= correction * correction;
                //correction *= correction;
                ans.Add(correction * (minMax[1] - minMax[0]) + minMax[0]);
            }
            return ans;
        }


        /// <summary>Retrieves statistical minimum and maximum values of spectre intensities. Returns {min, max}</summary>
        private static float[] GetMinMaxSpectre(List<float[]> vals)
        {
            float min = float.MaxValue;
            float max = float.MinValue;
            float mean = 0;
            float stdDev = 0;

            for (int k = 0; k < vals.Count; k++)
            {
                for (int j = 0; j < vals[k].Length; j++)
                {
                    mean += vals[k][j];
                    min = Math.Min(min, vals[k][j]);
                    max = Math.Max(max, vals[k][j]);
                }
            }
            mean /= ((float)vals.Count * (float)vals[0].Length);
            for (int k = 0; k < vals.Count; k++)
            {
                for (int j = 0; j < vals[k].Length; j++)
                {
                    stdDev += (vals[k][j] - mean) * (vals[k][j] - mean);
                }
            }
            stdDev = (float)Math.Sqrt(stdDev / ((float)vals.Count * (float)vals[0].Length - 1));
            min = mean - 3 * stdDev;
            return new float[] { min, max };
        }
        /// <summary>Retrieves statistical minimum and maximum values of spectre intensities. Returns {min, max}</summary>
        private static float[] GetMinMaxSpectre(List<double[]> vals)
        {
            float min = float.MaxValue;
            float max = float.MinValue;
            float mean = 0;
            float stdDev = 0;

            for (int k = 0; k < vals.Count; k++)
            {
                for (int j = 0; j < vals[k].Length; j++)
                {
                    mean += (float)vals[k][j];
                    min = Math.Min(min, (float)vals[k][j]);
                    max = Math.Max(max, (float)vals[k][j]);
                }
            }
            mean /= ((float)vals.Count * (float)vals[0].Length);
            for (int k = 0; k < vals.Count; k++)
            {
                for (int j = 0; j < vals[k].Length; j++)
                {
                    stdDev += ((float)vals[k][j] - mean) * ((float)vals[k][j] - mean);
                }
            }
            stdDev = (float)Math.Sqrt(stdDev / ((float)vals.Count * (float)vals[0].Length - 1));
            min = mean - 3 * stdDev;
            return new float[] { min, max };
        }

        #endregion

        #endregion

        #region Mouse selection
        /// <summary>Retrieves time-frequency coordinates for mouse X and Y position</summary>
        void getMouseCoordsInTF(int X, int Y, out float t, out float F)
        {
            t = (float)X / (float)glSpectre.Width * (GLSpectreGraphRange[1] - GLSpectreGraphRange[0]) + GLSpectreGraphRange[0];
            F = (float)(glSpectre.Height - Y) / (float)glSpectre.Height * (GLSpectreGraphRange[3] - GLSpectreGraphRange[2]) + GLSpectreGraphRange[2];
        }

        void glIntens_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left) StartSelecting(e.X);
        }
        void glIntens_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left) ContinueSelecting(e.X);
        }
        void glIntens_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left) StopSelecting(e.X);
        }

        void glSpectre_MouseWheel(object sender, MouseEventArgs e)
        {
            //modify spectre span of comments
            IncrementAnnotationFreqSpan(e.Delta);
        }

        void glSpectre_MouseMove(object sender, MouseEventArgs e)
        {
            //Select annotations
            if (!clicked) SelectAnnotationOnMouseOver(e.X, e.Y);

            if (e.Button == MouseButtons.Left)
            {
                if (selectedAnnotation != null) ContinueMovingAnnotation(e.X, e.Y);
                else ContinueSelecting(e.X);
            }

            if (lblInfo != null)
            {
                float t, F;
                getMouseCoordsInTF(e.X, e.Y, out t, out F);
                float Intens = (float)(glSpectre.Height - e.Y) / (float)glSpectre.Height * IntensRange / (float)SampleAudio.maxKeepFreq * (GLSpectreGraphRange[3] - GLSpectreGraphRange[2]);

                lblInfo.Text = Math.Round(t, 3).ToString() + "s " + Math.Round(F, 2).ToString() + "Hz " + Math.Round(Intens, 2).ToString() + " dBr";
            }
        }

        void glSpectre_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (selectedAnnotation != null) StopMovingAnnotation(e.X, e.Y);
                StopSelecting(e.X);
            }
        }

        void glSpectre_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (selectedAnnotation != null) StartMovingAnnotation(e.X, e.Y);
                else StartSelecting(e.X);
            }
        }
        void pbInfoBar_MouseMove(object sender, MouseEventArgs e)
        {
            if (!clicked && !ClickMoveAnnotation)
            {
                //check if some annotation has been selected
                SelectAnnotation(e.X, e.Y);
            }
            ContinueResizeAnnotation(e.X);
            //do selection
            if (e.Button == MouseButtons.Left) ContinueSelecting(e.X);
        }
        void pbInfoBar_MouseUp(object sender, MouseEventArgs e)
        {
            StopResizeAnnotation();
            if (e.Button == MouseButtons.Left) StopSelecting(e.X);
        }

        void pbInfoBar_MouseDown(object sender, MouseEventArgs e)
        {
            if (CloseToAnnotationLeft || CloseToAnnotationRight) StartResizeAnnotation(e.X);
            else if (e.Button == MouseButtons.Left) StartSelecting(e.X);
        }

        /// <summary>Selecting?</summary>
        bool clicked = false;
        /// <summary>Initial selected x coordinate</summary>
        public float p0SelX;
        /// <summary>Initial selected x coordinate</summary>
        public float pfSelX;

        /// <summary>Returns true if a portion of the audio is selected</summary>
        public bool IsSelected
        {
            get
            {
                return (p0SelX != pfSelX);
            }
        }

        /// <summary>Selects a region</summary>
        /// <param name="t0">Start time</param>
        /// <param name="tf">Final time</param>
        public void SelectRegion(float t0, float tf)
        {
            if (sampleAudio == null) return;

            p0SelX = t0;
            pfSelX = tf;
            VBOSelectIntens.SetVertexData(new float[] 
                {
                    p0SelX,GLIntensGraphRange[2],0,
                    pfSelX,GLIntensGraphRange[2],0,
                    pfSelX,GLIntensGraphRange[3],0,
                    p0SelX,GLIntensGraphRange[3],0,
                });
            VBOSelectSpectre.SetVertexData(new float[] 
                {
                    p0SelX,GLSpectreGraphRange[2],0,
                    pfSelX,GLSpectreGraphRange[2],0,
                    pfSelX,GLSpectreGraphRange[3],0,
                    p0SelX,GLSpectreGraphRange[3],0,
                });

            glIntens.Invalidate();
            glSpectre.Invalidate();
            pbInfoBar.Invalidate();
        }

        void StartSelecting(int x)
        {
            if (sampleAudio == null) return;
            clicked = true;
            p0SelX = GLIntensGraphRange[0] + (GLIntensGraphRange[1] - GLIntensGraphRange[0]) * (float)x / (float)glIntens.Width;
            pfSelX = p0SelX;

            SelectRegion(p0SelX, pfSelX);

        }
        void StopSelecting(int x)
        {
            if (sampleAudio == null) return;
            if (clicked)
            {
                clicked = false;
                pfSelX = GLIntensGraphRange[0] + (GLIntensGraphRange[1] - GLIntensGraphRange[0]) * (float)x / (float)glIntens.Width;

                if (p0SelX > pfSelX)
                {
                    float temp = p0SelX;
                    p0SelX = pfSelX;
                    pfSelX = temp;
                }
                if (p0SelX < 0) p0SelX = 0;
                if (pfSelX > sampleAudio.time[sampleAudio.time.Length - 1]) pfSelX = sampleAudio.time[sampleAudio.time.Length - 1];


                glIntens.Invalidate();
                glSpectre.Invalidate();
                pbInfoBar.Invalidate();
            }

        }
        void ContinueSelecting(int x)
        {
            if (clicked)
            {
                pfSelX = GLIntensGraphRange[0] + (GLIntensGraphRange[1] - GLIntensGraphRange[0]) * (float)x / (float)glIntens.Width;
                SelectRegion(p0SelX, pfSelX);
            }
        }
        #endregion

        #region Zoom in/out

        /// <summary>Zoom in to selection</summary>
        public void ZoomIn()
        {
            //zoom in to area
            if (p0SelX != pfSelX)
            {
                GLIntensGraphRange[0] = p0SelX;
                GLIntensGraphRange[1] = pfSelX;

                //deselects
                pfSelX = p0SelX;
            }
            else //no area to zoom to
            {
                float meanPos = 0.5f * (GLIntensGraphRange[0] + GLIntensGraphRange[1]);
                GLIntensGraphRange[0] = meanPos - 0.5f * (meanPos - GLIntensGraphRange[0]);
                GLIntensGraphRange[1] = meanPos + 0.5f * (GLIntensGraphRange[1] - meanPos);
            }
            //spectre must be set to same place as intensity
            GLSpectreGraphRange[0] = GLIntensGraphRange[0];
            GLSpectreGraphRange[1] = GLIntensGraphRange[1];

            //redraw
            SetupViewPorts();
            UpdateDrawing();
        }

        /// <summary>Reset zoom</summary>
        public void ZoomOut()
        {
            ResetDrawingRanges();
            UpdateDrawing();
        }

        /// <summary>Increase spectre height</summary>
        public void IncreaseSpectreHeight()
        {
            GLSpectreGraphRange[3] *= 0.5f;
            SetupViewPorts();
            glSpectre.Invalidate();
        }

        /// <summary>Reset spectre height to original value</summary>
        public void ResetSpectreHeight()
        {
            GLSpectreGraphRange[3] = (float)SampleAudio.maxKeepFreq;
            SetupViewPorts();
            glSpectre.Invalidate();
        }

        #endregion
    }
}
