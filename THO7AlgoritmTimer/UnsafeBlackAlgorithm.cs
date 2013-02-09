using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;

namespace THO7AlgoritmTimerApplication
{
    class UnsafeBlackAlgorithm:VisionAlgorithm
    {
        public UnsafeBlackAlgorithm(String name)
            : base(name)
        {

        }

        public override Bitmap DoAlgorithm(Bitmap sourceImage)
        {
            Bitmap retval = new Bitmap(sourceImage);

            BitmapData rawData = retval.LockBits(new Rectangle(0,0,retval.Width,retval.Height),
                ImageLockMode.ReadWrite, retval.PixelFormat);

            unsafe
            {
                byte* start = (byte*)rawData.Scan0.ToPointer();

                for (int x = 0; x < rawData.Width; x++)
                {
                    for(int y = 0; y < rawData.Height; y++)
                    {
                        int current = (x*4) + (y*rawData.Stride) ;
                        *(start+current) = 0x00;
                        *(start+current+1) = 0x00;
                        *(start+current+2) = 0x00;
                        *(start+current+3) = 0xff;
                    }
                }
            }
            retval.UnlockBits(rawData);

            return retval;
        }
        private byte getBytesPerPixel(PixelFormat p)
        {
            byte retval = 0;

            switch(p)
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
