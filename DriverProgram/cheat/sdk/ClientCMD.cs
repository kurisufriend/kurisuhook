using System;
using System.Collections.Generic;
using System.Text;
using recode.lib;

namespace recode.sdk
{
    class ClientCMD
    {
        static int addy = Memory.FindPattern(new byte[] { 0x55, 0x8B, 0xEC, 0x8B, 0x0D, 0x00, 0x00, 0x00, 0x00, 0x81, 0xF9, 0x00, 0x00, 0x00, 0x00, 0x75, 0x0C, 0xA1, 0x00, 0x00, 0x00, 0x00, 0x35, 0x00, 0x00, 0x00, 0x00, 0xEB, 0x05, 0x8B, 0x01, 0xFF, 0x50, 0x34, 0x50, 0xA1 }, "xxxxx????xx????xxx????x????xxxxxxxxx", G.engine, Memory.getModuleSize("engine.dll"));
        static int Size = 256;
        static IntPtr Address;

        public static void Exec(string szCmd, bool highPriority = false)
        {

            if (Address == IntPtr.Zero)
            {
                Allocator Alloc = new Allocator();
                Address = Alloc.Alloc(Size);
                if (Address == IntPtr.Zero)
                    return;
            }
            if (szCmd.Length > 255)
                szCmd = szCmd.Substring(0, 255);

            var szCmd_bytes = Encoding.UTF8.GetBytes(szCmd + "\0");

            MemoryAPI.WriteProcessMemory(Memory.handle, Address, szCmd_bytes, (uint)szCmd_bytes.Length, out _ );
            IntPtr Thread = winapi.CreateRemoteThread(Memory.handle, (IntPtr)null, IntPtr.Zero, new IntPtr(G.engine + addy), Address, 0, (IntPtr)null);
            winapi.CloseHandle(Thread);
            winapi.WaitForSingleObject(Thread, 0xFFFFFFFF);
        }
    }
}
