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

			EditableImage unedited = new EditableImage(retval); // Image with the original data; prior to application of the filter.
			EditableImage edited = new EditableImage(retval.Height,retval.Width); //"blank" image with the same dimensions as the original, the image with the filter applied will be constructed in this.

			int height = unedited.Height;
			int width = unedited.Width;

			for (int x = 0; x < width; x++)
			{
				for (int y = 0; y < height; y++)
				{ //for each pixel in the original image:
					List<EditablePixel> pixelArea = new List<EditablePixel>(); //create an empty list of EditablePixel s.
					for (int areaX = this._radius * -1; areaX <= this._radius; areaX++)
					{
						for (int areaY = this._radius * -1; areaY <= this._radius; areaY++)
						{// for each pixel in a square around the currently selected pixel:
							EditablePixel p = unedited.GetPixel(x+areaX,y+areaY); //find the current pixel,
							if (p != null)
							{
								pixelArea.Add(p);//add it to the list,
							}
						}
					}
					pixelArea.Sort(); //sort the list of pixels around the current pixel,
					edited.SetPixel(x,y,pixelArea[pixelArea.Count>>1]); //take the middle value (x>>1 == x/2 for integers)
				}
			}

			return edited.Bitmap;//return the resulting image, as a bitmap.
		}
	}
}
