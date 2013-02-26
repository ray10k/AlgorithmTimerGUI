using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace THO7AlgoritmTimerApplication
{
	class MedianFilter : VisionAlgorithm
	{
		private int _radius = 0;

		public MedianFilter(string name, int radius = 0):base(name)
		{
			this._radius = radius;
		}

		public override Bitmap DoAlgorithm(Bitmap sourceImage)
		{
			Bitmap retval = new Bitmap(sourceImage);

			EditableImage unedited = new EditableImage(retval);
			EditableImage edited = new EditableImage(retval.Height,retval.Width);

			int height = unedited.Height;
			int width = unedited.Width;
			int median = (_radius * _radius) /2;

			for (int x = 0; x < width; x++)
			{
				for (int y = 0; y < height; y++)
				{
					List<EditablePixel> pixelArea = new List<EditablePixel>();
					for (int areaX = this._radius * -1; areaX <= this._radius; areaX++)
					{
						for (int areaY = this._radius * -1; areaY <= this._radius; areaY++)
						{
							EditablePixel p = unedited.GetPixel(x+areaX,y+areaY);
							if (p != null)
							{
								pixelArea.Add(p);
							}
						}
					}
					pixelArea.Sort();
					edited.SetPixel(x,y,pixelArea[pixelArea.Count>>1]);
				}
			}

			return edited.Bitmap;
		}
	}
}
