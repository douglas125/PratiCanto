using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace PratiCanto
{
    public static class LayoutFuncs
    {
        public static Bitmap MakeDegrade(Color c1, Color c2, int w, int h, bool putBorder)
        {
            Bitmap bmp = new Bitmap(w, h);
            Graphics g = Graphics.FromImage(bmp);

            MakeDegrade(g, c1, c2, w, h, putBorder);

            return bmp;
        }

        public static void MakeDegrade(Graphics g, Color c1, Color c2, int w, int h, bool putBorder)
        {
            if (w == 0 || h == 0) return;
            LinearGradientBrush b = new LinearGradientBrush(new Point(0, 0), new Point(0, h), c1, c2);
            g.FillRectangle(b, 0, 0, w, h);

            if (putBorder && c2.R<c1.R) g.DrawRectangle(new Pen(c2), 0, 0, w - 2, h - 1);
            else g.DrawRectangle(new Pen(c1), 0, 0, w - 2, h - 1);
        }


        static Font f = new Font("Arial", 9, FontStyle.Regular);

        /// <summary>Applies software theme to groupbox</summary>
        public static void DoGroupBoxLayout(GroupBox gb, Graphics g)
        {
            MakeDegrade(g, Color.White, Color.FromArgb(236, 236, 236), gb.Width, gb.Height, true);
            g.DrawString(gb.Text, f, Brushes.Black, 5, 5);
        }

        /// <summary>Makes degrade preserving inner image</summary>
        public static Bitmap MakeDegradeWithImage(Image img, Color c1, Color c2, int w, int h, bool putBorder)
        {
            Bitmap ans = new Bitmap(w, h);

            Graphics g = Graphics.FromImage(ans);
            MakeDegrade(g, c1, c2, w, h, putBorder);

            //center image
            if (img != null) g.DrawImage(img, (w >> 1) - (img.Width >> 1), (h >> 1) - (img.Height >> 1), img.Width, img.Height);

            return ans;
        }

        /// <summary>Degrade with theme</summary>
        public static Bitmap MakeDegradeWithImageGray(Image img, int w, int h, bool putBorder)
        {
            return MakeDegradeWithImage(img, Color.White, Color.FromArgb(223, 223, 223), w, h, putBorder);
        }
        /// <summary>Degrade with theme</summary>
        public static Bitmap MakeDegradeWithImageRed(Image img, int w, int h, bool putBorder)
        {
            return MakeDegradeWithImage(img, Color.FromArgb(218, 30, 30), Color.FromArgb(213, 140, 140), w, h, putBorder);
        }

        public static void AdjustButtonGraphic(Button b, bool useRed)
        {
            b.FlatStyle = FlatStyle.Flat;
            b.FlatAppearance.BorderSize = 0;

            if (!useRed) b.Image = LayoutFuncs.MakeDegradeWithImageGray(b.Image, b.Width, b.Height, true);
            else b.Image = LayoutFuncs.MakeDegradeWithImageRed(b.Image, b.Width, b.Height, true);
        }

        #region Mouse enter/leave in buttons
        /// <summary>Animates enter/leave of button with a border highlight</summary>
        /// <param name="b">Button to animate</param>
        public static void AnimateEnterLeaveBorder(Button b)
        {
            b.MouseEnter += new EventHandler(PratiCanto.LayoutFuncs.b_MouseEnter);
            b.MouseLeave += new EventHandler(PratiCanto.LayoutFuncs.b_MouseLeave);
        }
        static void b_MouseLeave(object sender, EventArgs e)
        {
            ((Button)sender).FlatAppearance.BorderSize = 0;
        }

        static void b_MouseEnter(object sender, EventArgs e)
        {
            ((Button)sender).FlatAppearance.BorderSize = 1;
        }

        public static void AnimateEnterLeaveBgColor(Button b)
        {
            b.MouseEnter += new EventHandler(PratiCanto.LayoutFuncs.b_MouseEnterBg);
            b.MouseLeave += new EventHandler(PratiCanto.LayoutFuncs.b_MouseLeaveBg);

        }
        static void b_MouseLeaveBg(object sender, EventArgs e)
        {
            ((Button)sender).BackColor = Color.Transparent;
        }

        static void b_MouseEnterBg(object sender, EventArgs e)
        {
            ((Button)sender).BackColor = Color.Red;
        }


        #endregion
    }
}
