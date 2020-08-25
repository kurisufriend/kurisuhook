using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace recode.sdk
{
    [StructLayout(LayoutKind.Sequential)]
    public struct Matrix
    {
        public float m11;
        public float m12;
        public float m13;
        public float m14;
        public float m21;
        public float m22;
        public float m23;
        public float m24;
        public float m31;
        public float m32;
        public float m33;
        public float m34;
        public float m41;
        public float m42;
        public float m43;
        public float m44;
    }
}
