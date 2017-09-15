﻿using System;
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
		byte[] a_red;
		byte[] a_green;
		byte[] a_blue;


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
			rgbValues = new byte[bytes];

			// Copy the RGB values into the array.
			System.Runtime.InteropServices.Marshal.Copy(ptr, rgbValues, 0, bytes);


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

			// Copy the RGB values into the array.
			System.Runtime.InteropServices.Marshal.Copy(ptr, rgbValues, 0, bytes);


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

			// Copy the RGB values into the array.
			System.Runtime.InteropServices.Marshal.Copy(ptr, rgbValues, 0, bytes);

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

		private void GSequal_Click(object sender, EventArgs e)
		{
			Bitmap gs = pictureBox.Image as Bitmap;
			simple_gs(gs);
			pictureBox.Image = gs;
			pictureBox.Refresh();
		}

		private void GShdtv_Click(object sender, EventArgs e)
		{
			Bitmap hdtv = pictureBox.Image as Bitmap;
			hdtv_gs(hdtv);
			pictureBox.Image = hdtv;
			pictureBox.Refresh();
		}

		private void GSdifference_Click(object sender, EventArgs e)
		{
			Bitmap hdtv = pictureBox.Image as Bitmap;
			hdtv_gs(hdtv);
			pictureBox.Image = hdtv;
			pictureBox.Refresh();

			Bitmap gs = pictureBox.Image as Bitmap;
			hdtv_gs(gs);
			pictureBox.Image = gs;
			pictureBox.Refresh();


			

			Bitmap res = pictureBox.Image as Bitmap;

		}

		private void GShistogram_Click(object sender, EventArgs e)
		{

		}

		private void redToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Bitmap bm = pictureBox.Image as Bitmap;
			channel(0, bm);
			pictureBox.Image = bm;
			pictureBox.Refresh();
			
		}

		private void greenToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Bitmap bm = pictureBox.Image as Bitmap;
			channel(1, bm);
			pictureBox.Image = bm;
			pictureBox.Refresh();
		}

		private void blueToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Bitmap bm = pictureBox.Image as Bitmap;
			channel(2, bm);
			pictureBox.Image = bm;
			pictureBox.Refresh();
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
			a_blue = new byte[255];
			a_red = new byte[255];
			a_green = new byte[255];
			int hist_x_step = pictureBox.Image.Width / 255;
			
		}
	}
}
