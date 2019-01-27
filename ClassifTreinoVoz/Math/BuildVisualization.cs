using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace AudioComparer
{
    class BuildVisualization
    {

        #region Build visualization

        public static void ProcessUsingLockbits(Bitmap processedBitmap, float min, float max, float[] vals)
        {
            int W = processedBitmap.Width;
            BitmapData bitmapData = processedBitmap.LockBits(new Rectangle(0, 0, processedBitmap.Width, processedBitmap.Height), ImageLockMode.ReadWrite, processedBitmap.PixelFormat);

            int bytesPerPixel = Bitmap.GetPixelFormatSize(processedBitmap.PixelFormat) / 8;
            int byteCount = bitmapData.Stride * processedBitmap.Height;
            byte[] pixels = new byte[byteCount];
            IntPtr ptrFirstPixel = bitmapData.Scan0;
            Marshal.Copy(ptrFirstPixel, pixels, 0, pixels.Length);
            int heightInPixels = bitmapData.Height;
            int widthInBytes = bitmapData.Width * bytesPerPixel;

            int xx, yy;
            yy = 0;
            for (int y = 0; y < heightInPixels; y++)
            {
                int currentLine = y * bitmapData.Stride;
                xx = 0;
                for (int x = 0; x < widthInBytes; x = x + bytesPerPixel)
                {
                    //int oldBlue = pixels[currentLine + x];
                    //int oldGreen = pixels[currentLine + x + 1];
                    //int oldRed = pixels[currentLine + x + 2];

                    float[] rgb = FcnGetColor(vals[xx + W * yy], min, max);


                    // calculate new pixel value
                    pixels[currentLine + x] = (byte)(rgb[2] * 255.0f);
                    pixels[currentLine + x + 1] = (byte)(rgb[1] * 255.0f);
                    pixels[currentLine + x + 2] = (byte)(rgb[0] * 255.0f);
                    pixels[currentLine + x + 3] = 255;// (byte)(rgb[0] * 255.0f);

                    xx++;
                }
                yy++;
            }

            // copy modified bytes back
            Marshal.Copy(pixels, 0, ptrFirstPixel, pixels.Length);
            processedBitmap.UnlockBits(bitmapData);
        }

        public static Bitmap buildBmp(string filename)
        {
            string sepDec = (1.1).ToString().Substring(1, 1);
            string[] f = File.ReadAllLines(filename);
            int H = f.Length;

            int W = 0;

            float[] vals = null;

            for (int y = 0; y < H; y++)
            {
                string[] line = f[y].Split(',');
                if (W == 0)
                {
                    W = line.Length;
                    vals = new float[W * H];
                }

                for (int x = 0; x < W; x++)
                {
                    vals[x + W * y] = float.Parse(line[x].ToString().Replace(".", sepDec));
                }
            }

            return buildBmp(W, H, vals);
        }

        public static Bitmap buildBmp(int W, int H, float[] vals)
        {
            Bitmap Bmp = new Bitmap(W, H);

            float min = float.MaxValue;
            float max = float.MinValue;
            for (int k = 0; k < vals.Length; k++)
            {
                if (float.IsNaN(vals[k]))
                {
                }
                min = Math.Min(min, vals[k]);
                max = Math.Max(max, vals[k]);
            }

            min = min + 0.4f * (max - min);

            ProcessUsingLockbits(Bmp, min, max, vals);

            return Bmp;
        }

        public static float[] FcnGetColor(float t, float hMin, float hMax)
        {
            if (t < hMin) t = hMin;
            if (t > hMax) t = hMax;

            float invMaxH = 1.0f / (hMax - hMin);
            float zRel = (t - hMin) * invMaxH;

            float cR = 0, cG = 0, cB = 0;


            if (0 <= zRel && zRel < 0.2f)
            {
                cB = 1.0f;
                cG = zRel * 5.0f;
            }
            else if (0.2f <= zRel && zRel < 0.4f)
            {
                cG = 1.0f;
                cB = 1.0f - (zRel - 0.2f) * 5.0f;
            }
            else if (0.4f <= zRel && zRel < 0.6f)
            {
                cG = 1.0f;
                cR = (zRel - 0.4f) * 5.0f;
            }
            else if (0.6f <= zRel && zRel < 0.8f)
            {
                cR = 1.0f;
                cG = 1.0f - (zRel - 0.6f) * 5.0f;
            }
            else
            {
                cR = 1.0f - (zRel - 0.8f) * 5.0f;
                //cG = (zRel - 0.8f) * 5.0f;
                //cB = cG;
            }

            return new float[] { cR, cG, cB, 1.0f };
        }

        #endregion

    }
}
