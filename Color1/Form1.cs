using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Color1
{
	public partial class Form1 : Form
	{

		string filepath = "";
		byte[] rgbValues;
        byte[] original;
		byte[] a_red;
		byte[] a_green;
		byte[] a_blue;
        byte[] saturation;
		int bytes;
        int w;
        int h;


		public Form1()
		{
			InitializeComponent();
		}

		private void openToolStripMenuItem_Click(object sender, EventArgs e)
		{
			OpenFileDialog ofd = new OpenFileDialog();
			ofd.Title = "Open...";
			ofd.Filter = "Image files (*.jpg, *.jpeg, *.png) | *.jpg; *.jpeg; *.png";
			if (ofd.ShowDialog() == DialogResult.OK)
			{
				filepath = ofd.FileName;
				this.Text = Path.GetFileName(filepath);
				if (pictureBox.Image != null)
					pictureBox.Image.Dispose();
				pictureBox.Image = new Bitmap(ofd.FileName);
                grayscaleToolStripMenuItem.Enabled = true;
                rGBToolStripMenuItem.Enabled = true;
                hSVToolStripMenuItem.Enabled = true;
                foreach (ToolStripItem item in grayscaleToolStripMenuItem.DropDownItems)
                    item.Enabled = true;
                foreach (ToolStripItem item in rGBToolStripMenuItem.DropDownItems)
                    item.Enabled = true;
                foreach (ToolStripItem item in hSVToolStripMenuItem.DropDownItems)
                    item.Enabled = true;

                Bitmap bmp = pictureBox.Image as Bitmap;
                w = bmp.Width;
                h = bmp.Height;
                Rectangle rect = new Rectangle(0, 0, w, h);
                System.Drawing.Imaging.BitmapData bmpData =
                    bmp.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadWrite,
                    bmp.PixelFormat);

                // Get the address of the first line.
                IntPtr ptr = bmpData.Scan0;

                // Declare an array to hold the bytes of the bitmap.
                int bytes = Math.Abs(bmpData.Stride) * bmp.Height;
                original = new byte[bytes];

                // Copy the RGB values into the array.
                System.Runtime.InteropServices.Marshal.Copy(ptr, original, 0, bytes);

                System.Runtime.InteropServices.Marshal.Copy(original, 0, ptr, bytes);

                // Unlock the bits.
                bmp.UnlockBits(bmpData);

            }
		}


		private void simple_gs(Bitmap bmp)
		{

			Rectangle rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
			System.Drawing.Imaging.BitmapData bmpData =
				bmp.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadWrite,
				bmp.PixelFormat);

			// Get the address of the first line.
			IntPtr ptr = bmpData.Scan0;

			// Declare an array to hold the bytes of the bitmap.
			int bytes = Math.Abs(bmpData.Stride) * bmp.Height;
            //rgbValues = new byte[bytes];
            rgbValues = new byte[bytes];
            original.CopyTo(rgbValues,0);

			// Copy the RGB values into the array.
			//System.Runtime.InteropServices.Marshal.Copy(ptr, rgbValues, 0, bytes);


			for (int counter = 0; counter < rgbValues.Length; counter += 3)
			{
				byte b = rgbValues[counter];
				byte g = rgbValues[counter + 1];
				byte r = rgbValues[counter + 2];
				byte gs = (byte)(0.33 * b + 0.33 * g + 0.33 * r);
				rgbValues[counter] = gs;
				rgbValues[counter + 1] = gs;
				rgbValues[counter + 2] = gs;
			}

			System.Runtime.InteropServices.Marshal.Copy(rgbValues, 0, ptr, bytes);

			// Unlock the bits.
			bmp.UnlockBits(bmpData);
		}

		private void hdtv_gs(Bitmap bmp)
		{

			Rectangle rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
			System.Drawing.Imaging.BitmapData bmpData =
				bmp.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadWrite,
				bmp.PixelFormat);

			// Get the address of the first line.
			IntPtr ptr = bmpData.Scan0;

			// Declare an array to hold the bytes of the bitmap.
			int bytes = Math.Abs(bmpData.Stride) * bmp.Height;
            rgbValues = new byte[bytes];
            original.CopyTo(rgbValues, 0);

            // Copy the RGB values into the array.
            //System.Runtime.InteropServices.Marshal.Copy(ptr, rgbValues, 0, bytes);


			for (int counter = 0; counter < rgbValues.Length; counter += 3)
			{
				byte b = rgbValues[counter];
				byte g = rgbValues[counter + 1];
				byte r = rgbValues[counter + 2];
				byte gs = (byte)(0.0722 * b + 0.7152 * g + 0.2126 * r);
				rgbValues[counter] = gs;
				rgbValues[counter + 1] = gs;
				rgbValues[counter + 2] = gs;
			}

			System.Runtime.InteropServices.Marshal.Copy(rgbValues, 0, ptr, bytes);

			// Unlock the bits.
			bmp.UnlockBits(bmpData);
		}

		private void channel(int c, Bitmap bmp)
		{
			Rectangle rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
			System.Drawing.Imaging.BitmapData bmpData =
				bmp.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadWrite,
				bmp.PixelFormat);

			// Get the address of the first line.
			IntPtr ptr = bmpData.Scan0;

			// Declare an array to hold the bytes of the bitmap.
			int bytes = Math.Abs(bmpData.Stride) * bmp.Height;
            rgbValues = new byte[bytes];
            original.CopyTo(rgbValues, 0);

            // Copy the RGB values into the array.
            //System.Runtime.InteropServices.Marshal.Copy(ptr, rgbValues, 0, bytes);

			// b-0 g-1 r-2
			for (int counter = 0; counter < rgbValues.Length; counter += 3)
			{
				if (c == 0)
				{
					rgbValues[counter] = 0;
					rgbValues[counter + 1] = 0;
				}
				else if (c == 1)
				{
					rgbValues[counter] = 0;
					rgbValues[counter + 2] = 0;
				}
				else if (c == 2)
				{
					rgbValues[counter + 1] = 0;
					rgbValues[counter + 2] = 0;
				}
			}

			System.Runtime.InteropServices.Marshal.Copy(rgbValues, 0, ptr, bytes);

			// Unlock the bits.
			bmp.UnlockBits(bmpData);
		}

        private void difference_gs(Bitmap bmp)
        {
            Rectangle rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
            System.Drawing.Imaging.BitmapData bmpData =
                bmp.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadWrite,
                bmp.PixelFormat);

            // Get the address of the first line.
            IntPtr ptr = bmpData.Scan0;

            // Declare an array to hold the bytes of the bitmap.
            int bytes = Math.Abs(bmpData.Stride) * bmp.Height;
            //rgbValues = new byte[bytes];
            rgbValues = new byte[bytes];
            original.CopyTo(rgbValues, 0);

            // Copy the RGB values into the array.
            //System.Runtime.InteropServices.Marshal.Copy(ptr, rgbValues, 0, bytes);


            for (int counter = 0; counter < rgbValues.Length; counter += 3)
            {
                byte b = rgbValues[counter];
                byte g = rgbValues[counter + 1];
                byte r = rgbValues[counter + 2];
                byte gs_simple = (byte)(0.3333 * b + 0.3333 * g + 0.3333 * r);
                byte gs_hdtv = (byte)(0.0722 * b + 0.7152 * g + 0.2126 * r);
                byte diff = (byte)(gs_hdtv - gs_simple + 0.3819 * 255);
                rgbValues[counter] = diff;
                rgbValues[counter + 1] = diff;
                rgbValues[counter + 2] = diff;
            }

            System.Runtime.InteropServices.Marshal.Copy(rgbValues, 0, ptr, bytes);

            // Unlock the bits.
            bmp.UnlockBits(bmpData);
        }

		private void GSequal_Click(object sender, EventArgs e)
		{
			Bitmap gs = pictureBox.Image as Bitmap;
			simple_gs(gs);
            var newForm = new pictureForm(gs);
            newForm.Text = "Greyscale(simple)";
            newForm.Show();
		}

		private void GShdtv_Click(object sender, EventArgs e)
		{
			Bitmap hdtv = pictureBox.Image as Bitmap;
			hdtv_gs(hdtv);
            var newForm = new pictureForm(hdtv);
            newForm.Text = "Greyscale(HDTV)";
            newForm.Show();
            
		}

		private void GSdifference_Click(object sender, EventArgs e)
		{
			Bitmap diff = pictureBox.Image as Bitmap;
            difference_gs(diff);
            var newForm = new pictureForm(diff);
            newForm.Text = "Greyscale(Difference)";
            newForm.Show();
		}

		private void GShistogram_Click(object sender, EventArgs e)
		{
            Bitmap bmp = pictureBox.Image as Bitmap;
            hdtv_gs(bmp);
			//GShist(bmp);

            var newForm = new pictureForm(GShist(bmp));
            newForm.Text = "Histogram(HDTV)";
            newForm.Show();
        }

		private void SHistogram_Click(object sender, EventArgs e)
		{
			Bitmap bmp = pictureBox.Image as Bitmap;
			simple_gs(bmp);
			//GShist(bmp);

			var newForm = new pictureForm(GShist(bmp));
			newForm.Text = "Histogram(simple)";
			newForm.Show();
		}

		private Bitmap GShist(Bitmap bmp)
		{
			saturation = new byte[256];

			for (int i = 0; i < bmp.Width; ++i)
				for (int j = 0; j < bmp.Height; ++j)
				{
					Color color = bmp.GetPixel(i, j);
					++saturation[color.R];
				}

			int width = 256, height = 300;

			Bitmap histogram = new Bitmap(width, height);

			int max_saturation = saturation.Max();

			double point = (double)max_saturation / height;

			for (int i = 0; i < width; ++i)
			{
				for (int j = height - 1; j > height - saturation[i] / point; --j)
				{
					histogram.SetPixel(i, j, Color.Black);
				}
			}

			return histogram;
		}

		private void redToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Bitmap bm = pictureBox.Image as Bitmap;
			channel(0, bm);
            var newForm = new pictureForm(bm);
            newForm.Text = "Red";
            newForm.Show();

        }

		private void greenToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Bitmap bm = pictureBox.Image as Bitmap;
			channel(1, bm);
            var newForm = new pictureForm(bm);
            newForm.Text = "Green";
            newForm.Show();
        }

		private void blueToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Bitmap bm = pictureBox.Image as Bitmap;
			channel(2, bm);
            var newForm = new pictureForm(bm);
            newForm.Text = "Blue";
            newForm.Show();
        }

		private void hist(Bitmap bmp)
		{
			Rectangle rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
			System.Drawing.Imaging.BitmapData bmpData =
				bmp.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadWrite,
				bmp.PixelFormat);

			// Get the address of the first line.
			IntPtr ptr = bmpData.Scan0;

			// Declare an array to hold the bytes of the bitmap.
			int bytes = Math.Abs(bmpData.Stride) * bmp.Height;
			rgbValues = new byte[bytes];

			// Copy the RGB values into the array.
			System.Runtime.InteropServices.Marshal.Copy(ptr, rgbValues, 0, bytes);

			// b-0 g-1 r-2
			for (int counter = 0; counter < rgbValues.Length; counter += 3)
			{
				a_blue[rgbValues[counter]]++;
				a_green[rgbValues[counter+1]]++;
				a_red[rgbValues[counter+2]]++;
			}

			System.Runtime.InteropServices.Marshal.Copy(rgbValues, 0, ptr, bytes);

			// Unlock the bits.
			bmp.UnlockBits(bmpData);
		}


		private void histogramToolStripMenuItem1_Click(object sender, EventArgs e)
		{


			Bitmap bmp = pictureBox.Image as Bitmap;

			Rectangle rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
			System.Drawing.Imaging.BitmapData bmpData =
				bmp.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadWrite,
				bmp.PixelFormat);

			// Get the address of the first line.
			IntPtr ptr = bmpData.Scan0;

			// Declare an array to hold the bytes of the bitmap.
			int bytes = Math.Abs(bmpData.Stride) * bmp.Height;
			rgbValues = new byte[bytes];
			original.CopyTo(rgbValues, 0);

			// Copy the RGB values into the array.
			//System.Runtime.InteropServices.Marshal.Copy(ptr, rgbValues, 0, bytes);

			a_blue = new byte[256];
			a_red = new byte[256];
			a_green = new byte[256];

			// b-0 g-1 r-2
			for (int counter = 0; counter < rgbValues.Length; counter += 3)
			{
				++a_red[rgbValues[counter + 2]];
				++a_green[rgbValues[counter + 1]];
				++a_blue[rgbValues[counter]];
			}

			System.Runtime.InteropServices.Marshal.Copy(rgbValues, 0, ptr, bytes);

			// Unlock the bits.
			bmp.UnlockBits(bmpData);

            int width = 768, height = 300;

            Bitmap histogram = new Bitmap(width, height);

            int max_b = a_blue.Max();
            int max_g = a_green.Max();
            int max_r = a_red.Max();
            int max = Math.Max(Math.Max(max_b, max_g), max_r);

            double point = (double)max / height;

            for (int i = 0; i < width / 3; ++i)
            {
                for (int j = height - 1; j > height - a_red[i] / point; --j)
                {
                    histogram.SetPixel(i, j, Color.Red);
                }
            }

            for (int i = width / 3; i < width * 2 / 3; ++i)
            {
                for (int j = height - 1; j > height - a_green[i - 256] / point; --j)
                {
                    histogram.SetPixel(i, j, Color.Green);
                }
            }

            for (int i = width * 2 / 3; i < width; ++i)
            {
                for (int j = height - 1; j > height - a_blue[i - 512] / point; --j)
                {
                    histogram.SetPixel(i, j, Color.Blue);
                }
            }

            var newForm = new pictureForm(histogram);
            newForm.Text = "Histogram(RGB)";
            newForm.Show();

        }

        private void convertToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

		private void editToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Sliders s = new Sliders(original,pictureBox.Image as Bitmap);
			s.p = this;
			s.Show();
		}
	}
}
