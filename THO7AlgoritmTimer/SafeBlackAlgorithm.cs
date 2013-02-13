using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;

namespace THO7AlgoritmTimerApplication
{
    class SafeBlackAlgorithm:VisionAlgorithm
    {
        public SafeBlackAlgorithm(String name)
            : base(name)
        {

        }


        public override Bitmap DoAlgorithm(Bitmap original)
        {
            Bitmap retval = new Bitmap(original);

            BitmapData rawData = retval.LockBits(new Rectangle(0, 0, retval.Width, retval.Height), 
                System.Drawing.Imaging.ImageLockMode.ReadOnly, retval.PixelFormat);

            byte bytesPerPixel = getBytesPerPixel(rawData.PixelFormat);
            int bytes = rawData.Height * rawData.Stride;
            byte[] newPixels = new byte[bytes];
			int pixels = retval.Height * retval.Width;

            System.Runtime.InteropServices.Marshal.Copy(rawData.Scan0, newPixels, 0, bytes);

            
            if(bytesPerPixel == 3)
			{
				for (int i = 0; i < pixels; i++)
				{
					int current = i * 3;
					newPixels[current] = 0x00;
					newPixels[current+1] = 0x00;
					newPixels[current+2] = 0x00;
				}
            }
			else if (bytesPerPixel == 4)
			{
				for (int i = 0; i < pixels; i++)
				{
					int current = i * 4;
					newPixels[current] = 0x00;
					newPixels[current + 1] = 0x00;
					newPixels[current + 2] = 0x00;
				}
			}
			else if (bytesPerPixel == 6)
			{
				for (int i = 0; i < pixels; i++)
				{
					int current = i * 6;
					newPixels[current] = 0x00;
					newPixels[current + 1] = 0x00;
					newPixels[current + 2] = 0x00;
					newPixels[current + 3] = 0x00;
					newPixels[current + 4] = 0x00;
					newPixels[current + 5] = 0x00;
				}
			}
			else if(bytesPerPixel == 8)
			{
				for (int i = 0; i < pixels; i++)
				{
					int current = i * 8;
					newPixels[current] = 0x00;
					newPixels[current + 1] = 0x00;
					newPixels[current + 2] = 0x00;
					newPixels[current + 3] = 0x00;
					newPixels[current + 4] = 0x00;
					newPixels[current + 5] = 0x00;
				}
			}

            System.Runtime.InteropServices.Marshal.Copy(newPixels, 0, rawData.Scan0, newPixels.Length);

            retval.UnlockBits(rawData);

            return retval;
        }

        private bool isAlphaImage(PixelFormat pixelFormat)
        {
            bool retval = false;
            switch (pixelFormat)
            {
                case PixelFormat.Format32bppArgb:
                case PixelFormat.Format32bppPArgb:
                case PixelFormat.Format64bppArgb:
                case PixelFormat.Format64bppPArgb:
                case PixelFormat.Format16bppArgb1555:
                    retval = true;
                    break;
            }

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

        private byte GetBitsPerPixel(PixelFormat pixelFormat)
        {
            byte retval = 0;
            switch (pixelFormat)
            {
                case PixelFormat.Format1bppIndexed:
                    retval = 1;
                    break;
                case PixelFormat.Format4bppIndexed:
                    retval = 4;
                    break;
                case PixelFormat.Format16bppArgb1555:
                case PixelFormat.Format16bppGrayScale:
                    retval = 5;
                    break;
                case PixelFormat.Format24bppRgb:
                case PixelFormat.Format32bppArgb:
                case PixelFormat.Format32bppPArgb:
                case PixelFormat.Format32bppRgb:
                case PixelFormat.Format8bppIndexed:
                    retval = 8;
                    break;
                case PixelFormat.Format48bppRgb:
                case PixelFormat.Format64bppArgb:
                case PixelFormat.Format64bppPArgb:
                    retval = 16;
                    break;
            }

            return retval;
        }
    }
}
