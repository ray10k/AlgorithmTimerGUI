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

            byte bitsPerPixel = GetBitsPerPixel(rawData.PixelFormat);

            int pixels = rawData.Height * rawData.Stride;

            byte[] intensity = new byte[pixels];

            System.Runtime.InteropServices.Marshal.Copy(rawData.Scan0, intensity, 0, pixels);

            for (int i = 0; i < pixels; i++)
            {
                if (i % 4 != 3&&isAlphaImage(retval.PixelFormat))
                {
                    intensity[i] = 0;
                }
            }

            System.Runtime.InteropServices.Marshal.Copy(intensity, 0, rawData.Scan0, intensity.Length);

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
