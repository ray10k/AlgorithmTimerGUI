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
			//default constructor
		}

		public override Bitmap DoAlgorithm(Bitmap sourceImage)
		{
			Bitmap retval = new Bitmap(sourceImage); //make a working copy of the source image.

			BitmapData rawData = retval.LockBits(new Rectangle(0, 0, retval.Width, retval.Height),
				ImageLockMode.ReadWrite, retval.PixelFormat);//lock the image data in memory, allowing for both readin and writing.

			byte bytesPerPixel = GetBytesPerPixel(retval.PixelFormat); //determine how many bytes are used per pixel.
			int pixels = retval.Height * retval.Width; //calculate the number of pixels in the image.

			unsafe //since this algorithm works with UNMANAGED pointers, the following code is considered unsafe.
			{
				byte* start = (byte*)rawData.Scan0.ToPointer(); //get a pointer to the beginning of the image data.
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
						*(start + current + 0) ^= 0xff;//the algorithm has to skip the Alpha value of each pixel.
						*(start + current + 1) ^= 0xff;//initially, this was done by stepping over each byte and skipping every 
						*(start + current + 2) ^= 0xff;//byte n where n%Alpha byte position was skipped.
						//usage of the modulo operator turned out to be so inefficient, that the code was replaced with this.
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
			retval.UnlockBits(rawData); //undo the lock on the image data, preparing it to be returned.
			
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
