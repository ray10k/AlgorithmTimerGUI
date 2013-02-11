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

			byte pixelBytes = GetBytesPerPixel(retval.PixelFormat);
			bool alpha = hasAlpha(retval.PixelFormat);

			int totalBytes = data.Stride *data.Height;

			byte[] imageBytes = new byte[totalBytes];
			byte max = 255;

			System.Runtime.InteropServices.Marshal.Copy(data.Scan0,imageBytes,0,totalBytes);
			for (int i = 0; i < totalBytes; i++)
			{
				if (!alpha || i % pixelBytes != (pixelBytes - 1))
				{
					imageBytes[i] = (byte)(max - imageBytes[i]);
				}
			}
			System.Runtime.InteropServices.Marshal.Copy(imageBytes, 0, data.Scan0, totalBytes);

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

		private byte GetBytesPerPixel(PixelFormat pixelFormat)
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
