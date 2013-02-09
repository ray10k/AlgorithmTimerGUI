using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace THO7AlgoritmTimerApplication
{
    class BlackAlgorithm:VisionAlgorithm
    {
        public BlackAlgorithm(String name)
            : base(name)
        {

        }


        public override System.Drawing.Bitmap DoAlgorithm(System.Drawing.Bitmap sourceImage)
        {
            Bitmap retval = new Bitmap(sourceImage.Width, sourceImage.Height,sourceImage.PixelFormat);

            for (int x = 0; x < sourceImage.Width; x++)
            {
                for (int y = 0; y < sourceImage.Height; y++)
                {
                    retval.SetPixel(x,y,Color.FromArgb(0,0,0));
                }
            }

            return retval;
        }
    }
}
