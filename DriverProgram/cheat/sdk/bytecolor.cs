using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using Veldrid.Sdl2;

namespace recode.sdk
{
	public struct bytecolor
	{
		public byte r, g, b, a;
		public bytecolor(Vector4 c)
		{
			r = (byte)(c.X * 255);
			g = (byte)(c.Y * 255);
			b = (byte)(c.Z * 255);
			a = (byte)(c.W * 255);
		}
	}
}
