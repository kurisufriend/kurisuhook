﻿using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace recode.sdk
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct player_info_t
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
        public char[] __pad0;
        public int m_nXuidLow;
        public int m_nXuidHigh;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 128)]
        public byte[] m_szPlayerName;
        public uint m_nUserID;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 33)]
        public char[] m_szSteamID;
        public uint m_nSteam3ID;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 128)]
        public byte[] m_szFriendsName;
        [MarshalAs(UnmanagedType.U1, SizeConst = 1)]
        public bool m_bIsFakePlayer;
        [MarshalAs(UnmanagedType.U1, SizeConst = 1)]
        public bool m_bIsHLTV;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public int[] m_dwCustomFiles;
        public char m_FilesDownloaded;
    }
}
