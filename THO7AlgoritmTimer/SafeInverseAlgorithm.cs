using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;

namespace THO7AlgoritmTimerApplication
{
	class SafeInverseAlgorithm:VisionAlgorithm
	{
		public SafeInverseAlgorithm(String name)
			: base(name)
		{

		}

		public override Bitmap DoAlgorithm(Bitmap sourceImage)
		{
			Bitmap retval = new Bitmap(sourceImage);

			BitmapData data = retval.LockBits(new Rectangle(0, 0, retval.Width, retval.Height),
				ImageLockMode.ReadWrite, retval.PixelFormat);

			byte bytesPerPixel = getBytesPerPixel(retval.PixelFormat);
			bool alpha = hasAlpha(retval.PixelFormat);

			int totalBytes = data.Stride *data.Height;
			int pixels = retval.Height * retval.Width;
			byte[] newPixels = new byte[totalBytes];

			System.Runtime.InteropServices.Marshal.Copy(data.Scan0,newPixels,0,totalBytes);

			if (bytesPerPixel == 3)
			{
				for (int i = 0; i < pixels; i++)
				{
					int current = i * 3;
					newPixels[current] ^= 0xff;
					newPixels[current + 1] ^= 0xff;
					newPixels[current + 2] ^= 0xff;
				}
			}
			else if (bytesPerPixel == 4)
			{
				for (int i = 0; i < pixels; i++)
				{
					int current = i * 4;
					newPixels[current] ^= 0xff;
					newPixels[current + 1] ^= 0xff;
					newPixels[current + 2] ^= 0xff;
				}
			}
			else if (bytesPerPixel == 6)
			{
				for (int i = 0; i < pixels; i++)
				{
					int current = i * 6;
					newPixels[current] ^= 0xff;
					newPixels[current + 1] ^= 0xff;
					newPixels[current + 2] ^= 0xff;
					newPixels[current + 3] ^= 0xff;
					newPixels[current + 4] ^= 0xff;
					newPixels[current + 5] ^= 0xff;
				}
			}
			else if (bytesPerPixel == 8)
			{
				for (int i = 0; i < pixels; i++)
				{
					int current = i * 8;
					newPixels[current] ^= 0xff;
					newPixels[current + 1] ^= 0xff;
					newPixels[current + 2] ^= 0xff;
					newPixels[current + 3] ^= 0xff;
					newPixels[current + 4] ^= 0xff;
					newPixels[current + 5] ^= 0xff;
				}
			}

			System.Runtime.InteropServices.Marshal.Copy(newPixels, 0, data.Scan0, totalBytes);

			retval.UnlockBits(data);
			
			return retval;
		}

		private bool hasAlpha(PixelFormat pixelFormat)
		{
			bool retval = false;

			switch (pixelFormat)
			{
				case PixelFormat.Format16bppArgb1555:
				case PixelFormat.Format32bppArgb:
				case PixelFormat.Format32bppPArgb:
				case PixelFormat.Format64bppArgb:
				case PixelFormat.Format64bppPArgb:
					retval = true;
					break;
			}

			return retval;
		}

		private byte getBytesPerPixel(PixelFormat pixelFormat)
		{
			byte retval = 0;
			switch (pixelFormat)
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
					retval = 6;
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
