using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Numerics;
using System.Runtime.InteropServices;

namespace recode.sdk
{
    [StructLayout(LayoutKind.Sequential)]
    public struct GlowStruct
    {
        // 0x4 entptr
        public float r, g, b, a;// 0x8, 0xC, 0x10, 0x14
        public GlowStruct(Vector4 color)
        {
            r = color.X;
            g = color.Y;
            b = color.Z;
            a = color.W;
        }
    }
}
