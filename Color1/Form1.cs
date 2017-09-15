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

namespace Color1
{
	public partial class Form1 : Form
	{

		string filepath = "";
		 byte[] rgbValues;

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
			}
		}

		private void GSequal_Click(object sender, EventArgs e)
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

			// Copy the RGB values into the array.
			System.Runtime.InteropServices.Marshal.Copy(ptr, rgbValues, 0, bytes);

			// Set every third value to 255. A 24bpp bitmap will look red.  
			for (int counter = 0; counter < rgbValues.Length; counter += 3)
			{
				byte b = rgbValues[counter];
				byte g = rgbValues[counter+1];
				byte r = rgbValues[counter+2];
				byte gs = (byte)(0.33 * b + 0.33 * g + 0.33 * r);
				rgbValues[counter] = gs;
				rgbValues[counter+1] = gs;
				rgbValues[counter+2] = gs;
			}
			// Copy the RGB values back to the bitmap
			System.Runtime.InteropServices.Marshal.Copy(rgbValues, 0, ptr, bytes);

			// Unlock the bits.
			bmp.UnlockBits(bmpData);

			int i = 0;
			for (int counter = 0; counter < rgbValues.Length; counter += 3)
			{
				bmp.SetPixel(i % bmp.Width, i / bmp.Width,
					Color.FromArgb(rgbValues[counter], rgbValues[counter + 1], rgbValues[counter + 2]));
				i++;

			}
			pictureBox.Refresh();
		}

		private void GShdtv_Click(object sender, EventArgs e)
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

			// Copy the RGB values into the array.
			System.Runtime.InteropServices.Marshal.Copy(ptr, rgbValues, 0, bytes);

			// Set every third value to 255. A 24bpp bitmap will look red.  
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
			// Copy the RGB values back to the bitmap
			System.Runtime.InteropServices.Marshal.Copy(rgbValues, 0, ptr, bytes);

			// Unlock the bits.
			bmp.UnlockBits(bmpData);

			int i = 0;
			for (int counter = 0; counter < rgbValues.Length; counter += 3)
			{
				bmp.SetPixel(i % bmp.Width, i / bmp.Width,
					Color.FromArgb(rgbValues[counter], rgbValues[counter + 1], rgbValues[counter + 2]));
				i++;

			}
			pictureBox.Refresh();
		}

		private void GSdifference_Click(object sender, EventArgs e)
		{

		}

		private void GShistogram_Click(object sender, EventArgs e)
		{

		}
	}
}
