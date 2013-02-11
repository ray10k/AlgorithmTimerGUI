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
			bool alpha = hasAlpha(retval.PixelFormat);

			unsafe
			{
				byte* start = (byte*)data.Scan0.ToPointer();
				for (int i = 0; i < data.Stride; i++)
				{
					for (int j = 0; j < data.Height; j++)
					{
						int current = (j*data.Stride)+i;
						if (!alpha ||i%bytesPerPixel != bytesPerPixel-1)
						{
							*(start+current) = (byte)(*(start+current)^0xff);
						}
					}
				}
			}
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
