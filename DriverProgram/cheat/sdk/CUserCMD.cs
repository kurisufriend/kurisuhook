using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using recode;
using recode.sdk;
using recode.lib;

namespace kurisuhook.cheat.sdk
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct Input_t
    {
        [MarshalAs(UnmanagedType.U1, SizeConst = 4)]
        public int m_pVftable;                   // 0x00
        [MarshalAs(UnmanagedType.U1, SizeConst = 1)]
        public bool m_bTrackIRAvailable;          // 0x04
        [MarshalAs(UnmanagedType.U1, SizeConst = 1)]
        public bool m_bMouseInitialized;          // 0x05
        [MarshalAs(UnmanagedType.U1, SizeConst = 1)]
        public bool m_bMouseActive;               // 0x06
        [MarshalAs(UnmanagedType.U1, SizeConst = 1)]
        public bool m_bJoystickAdvancedInit;      // 0x07
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 44)]
        public int[] Unk1;                     // 0x08
        [MarshalAs(UnmanagedType.U1, SizeConst = 4)]
        public int m_pKeys;                      // 0x34
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 64)]
        public int[] Unk2;                    // 0x38
        [MarshalAs(UnmanagedType.U1, SizeConst = 1)]
        public bool m_bCameraInterceptingMouse;   // 0x9C
        [MarshalAs(UnmanagedType.U1, SizeConst = 1)]
        public bool m_bCameraInThirdPerson;       // 0x9D
        [MarshalAs(UnmanagedType.U1, SizeConst = 1)]
        public bool m_bCameraMovingWithMouse;     // 0x9E
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
        public Vec3 m_vecCameraOffset;            // 0xA0
        [MarshalAs(UnmanagedType.U1, SizeConst = 1)]
        public bool m_bCameraDistanceMove;        // 0xAC
        [MarshalAs(UnmanagedType.U1, SizeConst = 4)]
        public int m_nCameraOldX;                // 0xB0
        [MarshalAs(UnmanagedType.U1, SizeConst = 4)]
        public int m_nCameraOldY;                // 0xB4
        [MarshalAs(UnmanagedType.U1, SizeConst = 4)]
        public int m_nCameraX;                   // 0xB8
        [MarshalAs(UnmanagedType.U1, SizeConst = 4)]
        public int m_nCameraY;                   // 0xBC
        [MarshalAs(UnmanagedType.U1, SizeConst = 4)]
        public bool m_bCameraIsOrthographic;      // 0xC0
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
        public Vec3 m_vecPreviousViewAngles;      // 0xC4
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
        public Vec3 m_vecPreviousViewAnglesTilt;  // 0xD0
        [MarshalAs(UnmanagedType.U1, SizeConst = 4)]
        public float m_flLastForwardMove;          // 0xDC
        [MarshalAs(UnmanagedType.U1, SizeConst = 4)]
        public int m_nClearInputState;           // 0xE0
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
        public int[] Unk3;                    // 0xE4
        [MarshalAs(UnmanagedType.U1, SizeConst = 4)]
        public int m_pCommands;                  // 0xEC
        [MarshalAs(UnmanagedType.U1, SizeConst = 4)]
        public int m_pVerifiedCommands;          // 0xF0
    };

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct UserCmd_t
    {
        [MarshalAs(UnmanagedType.U1, SizeConst = 4)]
        public int pVft;                // 0x00
        [MarshalAs(UnmanagedType.U1, SizeConst = 4)]
        public int m_iCmdNumber;        // 0x04
        [MarshalAs(UnmanagedType.U1, SizeConst = 4)]
        public int m_iTickCount;        // 0x08
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
        public Vec3 m_vecViewAngles;     // 0x0C
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
        public Vec3 m_vecAimDirection;   // 0x18
        [MarshalAs(UnmanagedType.U1, SizeConst = 4)]
        public float m_flForwardmove;     // 0x24
        [MarshalAs(UnmanagedType.U1, SizeConst = 4)]
        public float m_flSidemove;        // 0x28
        [MarshalAs(UnmanagedType.U1, SizeConst = 4)]
        public float m_flUpmove;          // 0x2C
        [MarshalAs(UnmanagedType.U1, SizeConst = 4)]
        public int m_iButtons;          // 0x30
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public int m_bImpulse;          // 0x34
        public int[] Pad1;
        [MarshalAs(UnmanagedType.U1, SizeConst = 4)]
        public int m_iWeaponSelect;     // 0x38
        [MarshalAs(UnmanagedType.U1, SizeConst = 4)]
        public int m_iWeaponSubtype;    // 0x3C
        [MarshalAs(UnmanagedType.U1, SizeConst = 4)]
        public int m_iRandomSeed;       // 0x40
        [MarshalAs(UnmanagedType.U1, SizeConst = 2)]
        public UInt16 m_siMouseDx;         // 0x44
        [MarshalAs(UnmanagedType.U1, SizeConst = 2)]
        public UInt16 m_siMouseDy;         // 0x46
        [MarshalAs(UnmanagedType.U1, SizeConst = 1)]
        bool m_bHasBeenPredicted; // 0x48
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
        public int[] Pad2;
    }; // size is 100 or 0x64
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    struct VerifiedUserCmd_t
    {
        public UserCmd_t m_Command;
        public UInt32 m_Crc;
    };
}
