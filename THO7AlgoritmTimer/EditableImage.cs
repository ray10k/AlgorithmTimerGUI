using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace THO7AlgoritmTimerApplication
{
	class EditableImage //A class that contains an image in an easy-to-access format.
	{
		private int _height, _width;
		private EditablePixel[] _pixels;

		public EditableImage(Bitmap original) //create an EditableImage with the same sizes and contents as the given bitmap.
		{
			this._height = original.Height;
			this._width = original.Width;
			this._pixels = new EditablePixel[this._height * this._width];

			BitmapData rawData = original.LockBits(new Rectangle(0, 0, original.Width, original.Height),
				System.Drawing.Imaging.ImageLockMode.ReadOnly, original.PixelFormat);

			int bytes = rawData.Stride * rawData.Height;
			int pixels = original.Height * original.Width;
			byte[] temp = new byte[bytes];
			System.Runtime.InteropServices.Marshal.Copy(rawData.Scan0,temp,0,bytes);
			int bpp = getBytesPerPixel(original.PixelFormat);

			if (bpp <0)
			{
				throw new Exception("incorrect pixelformat.");
			}

			for (int i = 0; i < temp.Length; i+= bpp)
			{
				this._pixels[i/bpp] = new EditablePixel(temp[i], temp[i + 1], temp[i + 2]);
			}

			original.UnlockBits(rawData);
		}
		public EditableImage (int height, int width) //construct an EditableImage with certain dimensions, but without any pixel data.
		{
			this._height = height;
			this._width = width;
			this._pixels = new EditablePixel[height * width];
		}

		public EditablePixel GetPixel(int x, int y)
		{
			if (x >= 0 && x < this._width &&
				y >= 0 && y < this._height)
			{
				return this._pixels[x+(y*this._width)];
			}
			else
			{
				return null;
			}
		}
		public void SetPixel(int x, int y, EditablePixel pixel)
		{
			if (x >= 0 && x < this._width &&
				y >= 0 && y < this._height)
			{
				this._pixels[x + (y * this._width)] = pixel;
			}
		}
		public int Height
		{
			get { return this._height; }
		}
		public int Width
		{
			get { return this._width; }
		}
		public int Pixels
		{
			get { return this._height * this._width; }
		}

		public EditablePixel this[int i]
		{
			get
			{
				return this._pixels[i];
			}
			set
			{
				this._pixels[i] = value;
			}
		}


		public Bitmap Bitmap //Here you have a good argument to be careful with the use of properties.
		{					
			get //returns a Bitmap representation of the current image.
			{
				Bitmap retval = new Bitmap(_width, _height, PixelFormat.Format32bppRgb);

				BitmapData rawData = retval.LockBits(new Rectangle(0, 0, _width, _height),
					ImageLockMode.ReadWrite, retval.PixelFormat);
				int bytes = 4 * this.Pixels;
				byte[] newBytes = new byte[bytes];

				for (int i = 0; i < bytes; i += 4)
				{
					EditablePixel current = this._pixels[i/4];
					newBytes[i] = current.Red;
					newBytes[i + 1] = current.Green;
					newBytes[i + 2] = current.Blue;
				}
				System.Runtime.InteropServices.Marshal.Copy(newBytes, 0, rawData.Scan0, bytes);
				retval.UnlockBits(rawData);

				return retval;
			}

		}

		private int getBytesPerPixel(PixelFormat input)
		{
			int retval = -1;

			switch (input)
			{
				case PixelFormat.Format24bppRgb:
					retval = 3;
					break;
				case PixelFormat.Format32bppArgb:
				case PixelFormat.Format32bppPArgb:
				case PixelFormat.Format32bppRgb:
					retval = 4;
					break;
			}

			return retval;
		}
	}
}
