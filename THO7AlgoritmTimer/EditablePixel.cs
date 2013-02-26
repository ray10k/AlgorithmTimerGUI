using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace THO7AlgoritmTimerApplication
{
	class EditablePixel : IComparable
	{
		private byte _red, _green, _blue;

		public EditablePixel(byte red, byte green, byte blue)
		{
			this._red = red;
			this._green = green;
			this._blue = blue;
		}

		public int CompareTo(Object obj)
		{
			if (obj is EditablePixel)
			{
				return this.Avg - ((EditablePixel)obj).Avg;
			}
			else
			{
				return 0;
			}

		}

		public byte Avg
		{
			get { return (byte)((_red + _green + _blue) / 3); }
		}

		public byte Red
		{
			get { return _red; }
			set { _red = value; }
		}
		public byte Green
		{
			get { return _green; }
			set { _green = value; }
		}
		public byte Blue
		{
			get { return _blue; }
			set { _blue = value; }
		}

		public int Integer
		{
			get { return (_red | (_green << 8) | (_blue <<16));}
			set 
			{
				_red = (byte)(value & 0xff);
				_green = (byte)((value >> 8) & 0xff);
				_blue = (byte)((value >> 16) & 0xff);
			}
		}

	}
}
