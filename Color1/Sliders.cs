using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Color1
{
	public partial class Sliders : Form
	{
		double[] hsv;
        double[] hsv_diff;
        byte[] or;
        byte[] pixels;
        public Form1 p;
		Bitmap bmp;

        int h_tb, s_tb, v_tb;

		public Sliders(byte[] original, Bitmap b)
		{
			InitializeComponent();
			//p = this.ParentForm as Form1;
			hsv = new double[original.Length];
            hsv_diff = new double[original.Length];
            or = original;
            pixels = original;
			bmp = b;
            originalToHSV();
            h_tb = hue_trackBar.Value;
            s_tb = saturation_trackBar.Value;
            v_tb = value_trackBar.Value;
        }



		public static void ColorToHSV(Color color, out double hue, out double saturation, out double value)
		{
			int max = Math.Max(color.R, Math.Max(color.G, color.B));
			int min = Math.Min(color.R, Math.Min(color.G, color.B));

			hue = color.GetHue();
			saturation = (max == 0) ? 0 : 1d - (1d * min / max);
			value = max / 255d;
		}

		public static Color ColorFromHSV(double hue, double saturation, double value)
		{
			int hi = Convert.ToInt32(Math.Floor(hue / 60)) % 6;
			double f = hue / 60 - Math.Floor(hue / 60);

			value = value * 255;
			int v = Convert.ToInt32(value);
			int p = Convert.ToInt32(value * (1 - saturation));
			int q = Convert.ToInt32(value * (1 - f * saturation));
			int t = Convert.ToInt32(value * (1 - (1 - f) * saturation));

			if (hi == 0)
				return Color.FromArgb(255, v, t, p);
			else if (hi == 1)
				return Color.FromArgb(255, q, v, p);
			else if (hi == 2)
				return Color.FromArgb(255, p, v, t);
			else if (hi == 3)
				return Color.FromArgb(255, p, q, v);
			else if (hi == 4)
				return Color.FromArgb(255, t, p, v);
			else
				return Color.FromArgb(255, v, p, q);
		}

        private void originalToHSV()
        {
            for (int counter = 0; counter < or.Length; counter += 3)
            {
                byte b = or[counter];
                byte g = or[counter + 1];
                byte r = or[counter + 2];

                Color c = Color.FromArgb(r, g, b);
                double h, s, v = 0.0;
                ColorToHSV(c, out h, out s, out v);

                hsv[counter] = h;
                hsv[counter + 1] = s;
                hsv[counter + 2] = v;
            }
        }

        private void changeHSV(int tb_num, double value)
        {
            double h, s, v;
            h = s = v = 0.0;
            for (int counter = 0; counter < hsv.Length; counter += 3)
            {
                if (tb_num == 1)
                {
                    hsv_diff[counter] += value;
                }
                else if (tb_num == 2)
                {
                    hsv_diff[counter + 1] += value;
                }
                else
                {
                    hsv_diff[counter + 2] += value;
                }

                h = hsv[counter] + hsv_diff[counter];

                if (hsv[counter + 1] + hsv_diff[counter + 1] < 1 && hsv[counter + 1] + hsv_diff[counter + 1] > 0)
                    s = hsv[counter + 1] + hsv_diff[counter + 1];

                if (hsv[counter + 2] + hsv_diff[counter + 2] < 1 && hsv[counter + 2] + hsv_diff[counter + 2] > 0)
                    v = hsv[counter + 2] + hsv_diff[counter + 2];

                Color nc = ColorFromHSV(h, s, v);

                pixels[counter] = nc.B;
                pixels[counter + 1] = nc.G;
                pixels[counter + 2] = nc.R;
            }

            //Bitmap bmp = new Bitmap(w, h);
            Rectangle rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
            System.Drawing.Imaging.BitmapData bmpData =
                bmp.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadWrite,
                bmp.PixelFormat);

            // Get the address of the first line.
            IntPtr ptr = bmpData.Scan0;

            // Declare an array to hold the bytes of the bitmap.
            int bytes = Math.Abs(bmpData.Stride) * bmp.Height;

            System.Runtime.InteropServices.Marshal.Copy(pixels, 0, ptr, bytes);

            // Unlock the bits.
            bmp.UnlockBits(bmpData);
            PictureBox pb = ((p.Controls["pictureBox"]) as PictureBox);
            pb.Refresh();

        }


        private void hue_trackBar_Scroll(object sender, EventArgs e)
        {
            double val = hue_trackBar.Value - h_tb;
            h_tb= hue_trackBar.Value;
            changeHSV(1, val);
        }

        private void saturation_trackBar_Scroll(object sender, EventArgs e)
        {
           double val = (saturation_trackBar.Value - s_tb) / 50.0;
            s_tb = saturation_trackBar.Value;
            changeHSV(2, val);
        }

        private void value_trackBar_Scroll(object sender, EventArgs e)
        {
            double val = (value_trackBar.Value - v_tb) / 50.0;
            v_tb = value_trackBar.Value;
            changeHSV(3, val);
        }
    }
}
