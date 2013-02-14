using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;

namespace THO7AlgoritmTimerApplication
{
	class SafeGreyAlgorithm:VisionAlgorithm
	{
		public SafeGreyAlgorithm(String name):base(name)
		{

		}

		public override Bitmap DoAlgorithm(Bitmap sourceImage)
		{
			Bitmap retval = new Bitmap(sourceImage);
			BitmapData rawData = retval.LockBits(new Rectangle(0, 0, retval.Width, retval.Height),
				ImageLockMode.ReadWrite, retval.PixelFormat);

			int pixels = retval.Height * retval.Width;
			int bytes = rawData.Height * rawData.Stride;
			byte[] newPixels = new byte[bytes];
			byte bytesPerPixel = getBytesPerPixel(retval.PixelFormat);

			System.Runtime.InteropServices.Marshal.Copy(rawData.Scan0, newPixels, 0, bytes);

			if(bytesPerPixel == 3)
			{
				for (int i = 0; i < pixels; i++)
				{
					int current = i*3;
					int avg;
					avg = (newPixels[current]+newPixels[current+1]+newPixels[current+2])/3;
					newPixels[current] = (byte)avg;
					newPixels[current+1] = (byte)avg;
					newPixels[current+2] = (byte)avg;
				}
			}
			else if (bytesPerPixel == 4)
			{
				for (int i = 0; i < pixels; i++)
				{
					int current = i * 4;
					int avg;
					avg = (newPixels[current] + newPixels[current + 1] + newPixels[current + 2]) / 3;
					newPixels[current] = (byte)avg;
					newPixels[current + 1] = (byte)avg;
					newPixels[current + 2] = (byte)avg;
				}
			}
			else if (bytesPerPixel == 6)
			{
				for (int i = 0; i < pixels; i++)
				{
					int current = i * 6;
					int avg;
					avg = (newPixels[current+1] + newPixels[current + 3] + newPixels[current + 5]) / 3;
					newPixels[current+1] = (byte)avg;
					newPixels[current + 3] = (byte)avg;
					newPixels[current + 5] = (byte)avg;
				}
			}
			else if (bytesPerPixel == 8)
			{
				for (int i = 0; i < pixels; i++)
				{
					int current = i * 8;
					int avg;
					avg = (newPixels[current + 1] + newPixels[current + 3] + newPixels[current + 5]) / 3;
					newPixels[current + 1] = (byte)avg;
					newPixels[current + 3] = (byte)avg;
					newPixels[current + 5] = (byte)avg;
				}
			}

			System.Runtime.InteropServices.Marshal.Copy(newPixels, 0, rawData.Scan0, newPixels.Length);
			retval.UnlockBits(rawData);


			return retval;
		}

		private byte getBytesPerPixel(PixelFormat p)
		{
			byte retval = 0;

			switch (p)
			{
				case PixelFormat.Format24bppRgb:
					retval = 3;
					break;
				case PixelFormat.Format32bppArgb:
				case PixelFormat.Format32bppPArgb:
				case PixelFormat.Format32bppRgb:
					retval = 4;
					break;
				case PixelFormat.Format48bppRgb:
					retval = 7;
					break;
				case PixelFormat.Format64bppArgb:
				case PixelFormat.Format64bppPArgb:
					retval = 8;
					break;
			}
			return retval;
		}
	}
}
