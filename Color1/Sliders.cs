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
		byte[] pixels;
		byte[] or;
		public Form1 p;
		Bitmap bmp;
		public Sliders(byte[] original, Bitmap b)
		{
			InitializeComponent();
			//p = this.ParentForm as Form1;
			pixels = original;
			or = original;
			bmp = b;
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


		private void trackbar_scroll(object sender, EventArgs e)
		{
			var sat_dif = saturation_trackBar.Value / 100.0 + 1.0;
			var val_dif = value_trackBar.Value / 100.0 + 1.0;
			var hue_dif = hue_trackBar.Value;

			for (int counter = 0; counter < pixels.Length; counter += 3)
			{
				byte b = pixels[counter];
				byte g = pixels[counter + 1];
				byte r = pixels[counter + 2];

				Color c = Color.FromArgb(r, g, b);
				double h, s, v = 0.0;
				ColorToHSV(c, out h, out s, out v);

				h += hue_dif;
				if (h > 360.0)
					h -= 360;

				v *= val_dif;
				if (v > 1.0)
					v = 1.0;

				s *= sat_dif;
				if (s > 1.0)
					s = 1.0;
				Color nc = ColorFromHSV(h, s, v);

				pixels[counter] = nc.B;
				pixels[counter+1] = nc.G;
				pixels[counter+2] = nc.R;
			}
			Bitmap bmp = new Bitmap(w, h);
			Rectangle rect = new Rectangle(0, 0, w, h);
			System.Drawing.Imaging.BitmapData bmpData =
				bmp.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadWrite,
				bmp.PixelFormat);

			// Get the address of the first line.
			IntPtr ptr = bmpData.Scan0;

			// Declare an array to hold the bytes of the bitmap.
			int bytes = Math.Abs(bmpData.Stride) * bmp.Height;
			//rgbValues = new byte[bytes];
			//rgbValues = new byte[bytes];
			//original.CopyTo(rgbValues, 0);

			// Copy the RGB values into the array.
			//System.Runtime.InteropServices.Marshal.Copy(ptr, rgbValues, 0, bytes);


			//for (int counter = 0; counter < rgbValues.Length; counter += 3)
			//{
			//	byte b = rgbValues[counter];
			//	byte g = rgbValues[counter + 1];
			//	byte r = rgbValues[counter + 2];
			//	byte gs = (byte)(0.33 * b + 0.33 * g + 0.33 * r);
			//	rgbValues[counter] = gs;
			//	rgbValues[counter + 1] = gs;
			//	rgbValues[counter + 2] = gs;
			//}

			System.Runtime.InteropServices.Marshal.Copy(pixels, 0, ptr, bytes);

			// Unlock the bits.
			bmp.UnlockBits(bmpData);
			((p.Controls["pictureBox"]) as PictureBox).Image = bmp;
			
		}

		private void saturation_trackBar_Scroll(object sender, EventArgs e)
		{
			var sat_dif = saturation_trackBar.Value;

		}

		private void value_trackBar_Scroll(object sender, EventArgs e)
		{
			
		}

		private void hue_trackBar_Scroll(object sender, EventArgs e)
		{
			

			for (int counter = 0; counter < pixels.Length; counter += 3)
			{
				byte b = pixels[counter];
				byte g = pixels[counter + 1];
				byte r = pixels[counter + 2];

				Color c = Color.FromArgb(r, g, b);

			}
		}
	}
}
