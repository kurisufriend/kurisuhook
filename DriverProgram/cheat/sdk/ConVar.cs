using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;
using System.IO;

using recode;
using recode.lib;
using recode.sdk;
using System.Reflection;

namespace recode.sdk
{
    public class ConVar
    {
        public int pThis;
        public ConVar(int Pointer)
        {
            pThis = Pointer;
        }
        public ConVar(string name)
        {
            pThis = GetConVarAddress(name);
        }

        public int GetStringHash(string name)
        {
            CharCodes codes = Memory.read<CharCodes>(G.vstdlib + hazedumper.signatures.convar_name_hash_table);
            int v2 = 0;
            int v3 = 0;
            for (int i = 0; i < name.Length; i += 2)
            {
                v3 = codes.tab[v2 ^ char.ToUpper(name[i])];
                if (i + 1 == name.Length)
                    break;
                v2 = codes.tab[v3 ^ char.ToUpper(name[i + 1])];
            }
            return v2 | (v3 << 8);
        }

        public void ClearCallbacks()
        {
            Memory.write<int>(pThis + 0x44 + 0xC, 0);
        }
        public int GetConVarAddress(string name)
        {
            var hash = GetStringHash(name);

            int CvarEngine = Memory.read<int>(G.vstdlib + hazedumper.signatures.interface_engine_cvar);
            int Pointer = Memory.read<int>(Memory.read<int>(CvarEngine + 0x34) + ((byte)hash * 4));
            Encoding enc = Encoding.UTF8;
            while ((IntPtr)Pointer != IntPtr.Zero)
            {
                if (Memory.read<int>(Pointer) == hash)
                {
                    int ConVarPointer = Memory.read<int>(Pointer + 0x4);

                    if (Memory.ReadText((IntPtr)Memory.read<int>(ConVarPointer + 0xC)) == name)
                    {
                        return ConVarPointer;
                    }
                }

                Pointer = Memory.read<int>(Pointer + 0xC);
            }
            return (int)IntPtr.Zero;
        }
    }
}
