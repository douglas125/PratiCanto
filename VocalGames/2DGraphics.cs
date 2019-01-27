using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTK.Graphics.OpenGL;
using OpenCLTemplate.CLGLInterop;
using System.Drawing;


namespace VocalGames
{
    public class _2DGraphics
    {
        /// <summary>OpenGL viewer</summary>
        OpenTK.GLControl Ctrl2D;

        /// <summary>Current time</summary>
        public double curTime = 0;

        /// <summary>OpenGL initialized?</summary>
        bool GLLoaded = false;

        /// <summary>Clear color</summary>
        Color backClearColor;

        //GLRender.GLVBOModel m;

        /// <summary>Initializes a 2D opengl window</summary>
        /// <param name="Ctrl2D">OpenGL Control</param>
        public _2DGraphics(OpenTK.GLControl Ctrl2D, Color bgClearColor)
        {
            this.Ctrl2D = Ctrl2D;
            this.backClearColor = bgClearColor;

            Ctrl2D.MakeCurrent();
            InitGL();

            SetupViewport(Ctrl2D, new float[] { 0, 1, 0, 1, -1, 1 });

            Ctrl2D.Paint += new System.Windows.Forms.PaintEventHandler(Ctrl2D_Paint);


            GLLoaded = true;

            //m = new GLRender.GLVBOModel(BeginMode.Quads);

            //m.SetVertexData(new float[] 
            //{0,0,0,
            // 0.2f,0,0,
            // 0.2f,0.2f,0,
            // 0,0.2f,0
            //});

            //m.SetColorData(new float[] 
            //{1,0,0,1,
            // 1,0,0,1,
            // 1,1,0,1,
            // 1,0,0,1
            //});


            //m.SetElemData(new int[] { 0, 1, 2, 3 });

            Ctrl2D.Invalidate();
        }

        #region Setup and initialization
        /// <summary>Adjusts viewports</summary>
        public void SetupViewPorts()
        {
            if(GLLoaded) SetupViewport(Ctrl2D, new float[] { 0, 1, 0, 1, -1, 1 });
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

        #region Draw
        void Ctrl2D_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            if (!GLLoaded) return;
            OpenTK.GLControl glg = (OpenTK.GLControl)sender;
            glg.MakeCurrent();
            GL.ClearColor(backClearColor);

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadIdentity();

            //m.DrawModel();
            foreach (Sprite2D s in Sprites2D) s.Draw(curTime);

            //Draw static models
            foreach (GLRender.GLVBOModel m in Models) if (m.ShowModel) m.DrawModel();

            glg.SwapBuffers();
        }
        #endregion


        /// <summary>2D Sprites</summary>
        public List<Sprite2D> Sprites2D = new List<Sprite2D>();

        /// <summary>Sprite 2D Class</summary>
        public class Sprite2D
        {
            /// <summary>Current sprite</summary>
            int curSprite = 0;

            /// <summary>Sprite plane model</summary>
            private GLRender.GLVBOModel modelSprite;

            /// <summary>Sprite bitmaps</summary>
            List<Bitmap> bmps;

            /// <summary>Constructor.</summary>
            /// <param name="bmps">Bitmaps of this sprite</param>
            /// <param name="dimensions">Dimensions, 0 to 1, width/height</param>
            public Sprite2D(List<Bitmap> bmps, float[] dimensions)
            {
                this.bmps = bmps;

                modelSprite = CreateTextureHolder(dimensions);
                NextSprite();
            }

            /// <summary>Changes image texture to next sprite</summary>
            private void NextSprite()
            {
                modelSprite.SetTexture(bmps[curSprite]);
                curSprite++; if (curSprite > bmps.Count - 1) curSprite = 0;
            }

            /// <summary>Time to change from one sprite to another</summary>
            public double SpriteChangeTime = 0.1;
            /// <summary>Last time sprite was changed</summary>
            public double LastChangeTime = 0;

            /// <summary>Draws this sprite. Goes to next image if necessary</summary>
            /// <param name="elapSeconds"></param>
            public void Draw(double elapSeconds)
            {
                if (elapSeconds - LastChangeTime > SpriteChangeTime)
                {
                    LastChangeTime = elapSeconds;
                    NextSprite();
                }
                modelSprite.DrawModel();
            }

            /// <summary>Sets Sprite position and angle</summary>
            /// <param name="x">X position, 0 to 1</param>
            /// <param name="y">Y position, 0 to 1</param>
            /// <param name="ang">Angle in radians</param>
            public void SetPositionRotation(double x, double y, double ang)
            {
                modelSprite.vetTransl.x = x;
                modelSprite.vetTransl.y = y;
                modelSprite.vetRot.z = ang;
            }

            /// <summary>Retrieves object position and orientation</summary>
            /// <returns></returns>
            public double[] GetPosition()
            {
                return new double[] { modelSprite.vetTransl.x, modelSprite.vetTransl.y, modelSprite.vetRot.z };
            }
        }

        /// <summary>Models to be displayed</summary>
        public List<GLRender.GLVBOModel> Models = new List<GLRender.GLVBOModel>();


        /// <summary>Creates a texture holder VBO model</summary>
        /// <param name="dimensions">Model dimensions {width, height}</param>
        /// <returns></returns>
        public static GLRender.GLVBOModel CreateTextureHolder(float[] dimensions)
        {
            GLRender.GLVBOModel model = new GLRender.GLVBOModel(BeginMode.Quads);

            float w = dimensions[0] * 0.5f;
            float h = dimensions[1] * 0.5f;
            model.SetVertexData(new float[] {
                -w,-h,0,
                w,-h,0,
                w,h,0,
                -w,h,0
                });

            model.SetTexCoordData(new float[] {
                    0,1,
                    1,1,
                    1,0,
                    0,0
                });

            model.SetElemData(new int[] { 0, 1, 2, 3 });

            return model;
        }

    }
}
