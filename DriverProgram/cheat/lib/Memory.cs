using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography.X509Certificates;
using System.Diagnostics;
using System.Net.Http.Headers;
using System.Drawing;
using System.Reflection.Metadata.Ecma335;
using System.IO;

namespace recode.lib
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct CharCodes
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 255)]
        public int[] tab;
    };
    class MemoryAPI
    {
        [DllImport("kernel32.dll")]
        public static extern IntPtr OpenProcess(UInt32 dwDesiredAccess, Int32 bInheritHandle, UInt32 dwProcessId);
        [DllImport("kernel32.dll")]
        public static extern Int32 CloseHandle(IntPtr hObject);
        [DllImport("kernel32.dll")]
        public static extern Int32 ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, [In, Out] byte[] buffer, UInt32 size, out IntPtr lpNumberOfBytesRead);
        [DllImport("kernel32.dll")]
        public static extern Int32 WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, [In, Out] byte[] buffer, UInt32 size, out IntPtr lpNumberOfBytesWritten);
        public enum processHeaders : uint // winapi headers for permission reqs when getting a process handle. from https://pastebin.com/M2NhKWuL, source on msdn at https://docs.microsoft.com/en-us/windows/win32/procthread/process-security-and-access-rights
        {
            PROCESS_ALL_ACCESS = PROCESS_CREATE_PROCESS | PROCESS_CREATE_THREAD | PROCESS_DUP_HANDLE | PROCESS_QUERY_INFORMATION |
                               PROCESS_QUERY_LIMITED_INFORMATION | PROCESS_SET_INFORMATION | PROCESS_SET_QUOTA | PROCESS_SUSPEND_RESUME |
                               PROCESS_TERMINATE | PROCESS_VM_OPERATION | PROCESS_VM_READ | PROCESS_VM_WRITE,
            PROCESS_CREATE_PROCESS = 0x0080,
            PROCESS_CREATE_THREAD = 0x0002,
            PROCESS_DUP_HANDLE = 0x0040,
            PROCESS_QUERY_INFORMATION = 0x0400,
            PROCESS_QUERY_LIMITED_INFORMATION = 0x1000,
            PROCESS_SET_INFORMATION = 0x0200,
            PROCESS_SET_QUOTA = 0x0100,
            PROCESS_SUSPEND_RESUME = 0x0800,
            PROCESS_TERMINATE = 0x0001,
            PROCESS_VM_OPERATION = 0x0008,
            PROCESS_VM_READ = 0x0010,
            PROCESS_VM_WRITE = 0x0020
        }
    }
    public static class Memory
    {
        static uint pid;
        static Process process;
        static public IntPtr handle;
        public static int setup(string processName, bool isDebug = false)
        {
            try
            {
                pid = (uint)Process.GetProcessesByName(processName)[0].Id;
            }
            catch (IndexOutOfRangeException)
            {
                debug.fatal("The process was not found.");
            }
            pid = (uint)Process.GetProcessesByName(processName)[0].Id;
            process = Process.GetProcessesByName(processName)[0];
            var processHandle = MemoryAPI.OpenProcess((uint)MemoryAPI.processHeaders.PROCESS_ALL_ACCESS, 1, pid);
            if (processHandle != null)
            {
                handle = processHandle;
                return 1;
            }
            else
            {
                debug.fatal("Failed to access process.");
            }
            return 0;
        }
        public static int getModuleBA(string moduleName)
        {
            int resultBA = 0;
            foreach (ProcessModule module in process.Modules)
            {
                if (module.ModuleName == moduleName)
                {
                    resultBA = (Int32)module.BaseAddress;
                }
            }
            return resultBA;
        }
        public static int getModuleSize(string moduleName)
        {
            int resultBA = 0;
            foreach (ProcessModule module in process.Modules)
            {
                if (module.ModuleName == moduleName)
                {
                    resultBA = (Int32)module.ModuleMemorySize;
                }
            }
            return resultBA;
        }

        public static int FindPattern(byte[] pattern, string mask, int moduleBase, int moduleSize) //animusoftawre
        {
            byte[] moduleBytes = new byte[moduleSize];
            IntPtr numBytes = IntPtr.Zero;

            if (MemoryAPI.ReadProcessMemory(handle, (IntPtr)moduleBase, moduleBytes, (uint)moduleSize, out numBytes) != 0)
            {
                for (int i = 0; i < moduleSize; i++)
                {
                    bool found = true;

                    for (int l = 0; l < mask.Length; l++)
                    {
                        found = mask[l] == '?' || moduleBytes[l + i] == pattern[l];

                        if (!found)
                            break;
                    }

                    if (found)
                        return i;
                }
            }

            return 0;
        }

        public static string ReadText(IntPtr address)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                int offset = 0;
                byte read;
                while ((read = ReadMemory(address + offset, 1)[0]) != 0)
                {
                    ms.WriteByte(read);
                    offset++;
                }
                var data = ms.ToArray();
                return Encoding.UTF8.GetString(data, 0, data.Length);
            }
        }
        public static T getStructure<T>(byte[] bytes)
        {
            var handle = GCHandle.Alloc(bytes, GCHandleType.Pinned);
            var structure = (T)Marshal.PtrToStructure(handle.AddrOfPinnedObject(), typeof(T));
            handle.Free();
            return structure;
        }
        public static byte[] ReadMemory(IntPtr address, int length)
        {
            byte[] data = new byte[length];
            if (MemoryAPI.ReadProcessMemory(handle, address, data, (uint)data.Length, out IntPtr unused) == 0)
            {
                return null;
            }
            return data;
        }

        public static T read<T>(Int32 address)
        {
            int length = Marshal.SizeOf(typeof(T));

            if (typeof(T) == typeof(bool))
                length = 1;

            byte[] buffer = new byte[length];
            MemoryAPI.ReadProcessMemory(handle, (IntPtr)address, buffer, (UInt32)length, out _);
            return getStructure<T>(buffer);
        }
        public static string readstring(Int32 address, int size)
        {
            byte[] buffer = new byte[size];
            IntPtr nBytesRead = IntPtr.Zero;
            MemoryAPI.ReadProcessMemory(handle, (IntPtr)address, buffer, (UInt32)size, out nBytesRead);
            string text =  Encoding.UTF8.GetString(buffer);
            if (text.Contains('\0'))
                text = text.Substring(0, text.IndexOf('\0'));
            return text;
        }
        public static void write<T>(int address, T data)
        {
            int sizeRead = Marshal.SizeOf(data);
            var buffer = new byte[sizeRead];
            IntPtr ptr = Marshal.AllocHGlobal(sizeRead);
            Marshal.StructureToPtr(data, ptr, true);
            Marshal.Copy(ptr, buffer, 0, sizeRead);
            Marshal.FreeHGlobal(ptr);

            MemoryAPI.WriteProcessMemory(handle, (IntPtr)address, buffer, (uint)sizeRead, out _);
        }
        public static void writeBytes(Int32 address, byte[] value)
        {
            MemoryAPI.WriteProcessMemory(handle, (IntPtr)address, value, (uint)value.Length, out _);
        }
        public static void close()
        {
            MemoryAPI.CloseHandle(handle);
        }
    }

    public class Allocator
    {
        public Dictionary<IntPtr, IntPtr> AllocatedSize = new Dictionary<IntPtr, IntPtr>();

        public IntPtr AlloacNewPage(IntPtr size)
        {
            var Address = winapi.VirtualAllocEx(Memory.handle, IntPtr.Zero, (IntPtr)4096, (int)0x1000 | (int)0x2000, 0x40);

            AllocatedSize.Add(Address, size);

            return Address;
        }

        public void Free()
        {
            foreach (var key in AllocatedSize)
                winapi.VirtualFreeEx(Memory.handle, key.Key, 4096, (int)0x1000 | (int)0x2000);
        }

        public IntPtr Alloc(int size)
        {
            for (int i = 0; i < AllocatedSize.Count; ++i)
            {
                var key = AllocatedSize.ElementAt(i).Key;
                int value = (int)AllocatedSize[key] + size;
                if (value < 4096)
                {
                    IntPtr CurrentAddres = IntPtr.Add(key, (int)AllocatedSize[key]);
                    AllocatedSize[key] = new IntPtr(value);
                    return CurrentAddres;
                }
            }

            return AlloacNewPage(new IntPtr(size));
        }

    }
}
