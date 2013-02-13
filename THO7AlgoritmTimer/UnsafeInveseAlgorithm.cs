using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;

namespace THO7AlgoritmTimerApplication
{
	class UnsafeInveseAlgorithm:VisionAlgorithm
	{
		public UnsafeInveseAlgorithm(String name)
			: base(name)
		{

		}

		public override Bitmap DoAlgorithm(Bitmap sourceImage)
		{
			Bitmap retval = new Bitmap(sourceImage);

			BitmapData data = retval.LockBits(new Rectangle(0, 0, retval.Width, retval.Height),
				ImageLockMode.ReadWrite, retval.PixelFormat);

			byte bytesPerPixel = GetBytesPerPixel(retval.PixelFormat);
			int pixels = retval.Height * retval.Width;

			unsafe
			{
				byte* start = (byte*)data.Scan0.ToPointer();
				if (bytesPerPixel == 2)
				{
					for (int i = 0; i < pixels; i++)
					{
						int current = 2 * i;
						*(start + current + 0) ^= 0xff;
						*(start + current + 1) ^= 0x7f;
					}
				}
				else if (bytesPerPixel == 3)
				{
					for (int i = 0; i < pixels; i++)
					{
						int current = 3 * i;
						*(start + current + 0) ^= 0xff;
						*(start + current + 1) ^= 0xff;
						*(start + current + 2) ^= 0xff;
					}
				}
				else if (bytesPerPixel == 4)
				{
					for (int i = 0; i < pixels; i++)
					{
						int current = 4 * i;
						*(start + current + 0) ^= 0xff;
						*(start + current + 1) ^= 0xff;
						*(start + current + 2) ^= 0xff;
					}
				}
				else if (bytesPerPixel == 6)
				{
					for (int i = 0; i < pixels; i++)
					{
						int current = 6 * i;
						*(start + current + 0) ^= 0xff;
						*(start + current + 1) ^= 0xff;
						*(start + current + 2) ^= 0xff;
						*(start + current + 3) ^= 0xff;
						*(start + current + 4) ^= 0xff;
						*(start + current + 5) ^= 0xff;
					}
				}
				else if (bytesPerPixel == 8)
				{
					for (int i = 0; i < pixels; i++)
					{
						int current = 8 * i;
						*(start + current + 0) ^= 0xff;
						*(start + current + 1) ^= 0xff;
						*(start + current + 2) ^= 0xff;
						*(start + current + 3) ^= 0xff;
						*(start + current + 4) ^= 0xff;
						*(start + current + 5) ^= 0xff;
					}
				}
			}
			retval.UnlockBits(data);
			
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
