using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using recode;
using recode.sdk;
using recode.lib;

namespace kurisuhook.cheat.sdk
{
	[StructLayout(LayoutKind.Explicit)]
	public unsafe struct CInput
	{
		[FieldOffset(0)] public int pvftable;
		[FieldOffset(12)] public byte m_fTrackIRAvailable;
		[FieldOffset(13)] public byte m_fMouseInitialized;
		[FieldOffset(14)] public byte m_fMouseActive;
		[FieldOffset(15)] public byte m_fJoystickAdvancedInit;
		[FieldOffset(60)] public void* m_pKeys;
		[FieldOffset(172)] public byte m_fCameraInterceptingMouse;
		[FieldOffset(173)] public byte m_fCameraInThirdPerson;
		[FieldOffset(174)] public byte m_fCameraMovingWithMouse;
		[FieldOffset(175)] public byte m_fCameraDistanceMove;
		[FieldOffset(176)] public Vec3 m_vecCameraOffset;
		[FieldOffset(188)] public int m_nCameraOldX;
		[FieldOffset(192)] public int m_nCameraOldY;
		[FieldOffset(196)] public int m_nCameraX;
		[FieldOffset(200)] public int m_nCameraY;
		[FieldOffset(204)] public byte m_CameraIsOrthographic;
		[FieldOffset(236)] public float m_flLastForwardMove;
		[FieldOffset(240)] public int m_nClearInputState;
		[FieldOffset(244)] public int m_pCommands;
		[FieldOffset(248)] public Int32 m_pVerifiedCommands;
	}


	public struct VerifiedUserCMD
	{
		public CUserCmd m_cmd;
		public UInt32 m_crc;
	};

	[StructLayout(LayoutKind.Explicit)]
	public unsafe struct CUserCmd
	{
		[FieldOffset(0x00)] public int pvftable;
		[FieldOffset(0x04)] public int m_iCmdNumber;
		[FieldOffset(0x08)] public int m_iTickCount;
		[FieldOffset(0x0C)] public Vec3 m_vecViewAngles;
		[FieldOffset(0x24)] public float m_flForwardmove;
		[FieldOffset(0x28)] public float m_flSidemove;
		[FieldOffset(0x2C)] public float m_flUpmove;
		[FieldOffset(0x30)] public int m_iButtons;
		[FieldOffset(0x34)] public char m_bImpulse;
		[FieldOffset(0x38)] public int m_iWeaponSelect;
		[FieldOffset(0x3C)] public int m_iWeaponSubtype;
		[FieldOffset(0x40)] public int m_iRandomSeed;
		[FieldOffset(0x44)] public short m_siMouseDx;
		[FieldOffset(0x46)] public short m_siMouseDy;
		[FieldOffset(0x48)] public byte m_bHasBeenPredicted;
		[FieldOffset(0x4C)] public fixed byte pad[0x18];
	}
}
