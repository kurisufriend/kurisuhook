﻿using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using ImGuiNET;
using recode.lib;

namespace recode.sdk
{
	public class Entity
	{
		private Int32 address;
		public Entity(Int32 _address)
		{
			address = _address;
		}
		public static Entity fromid(int id)
		{
			return new Entity(Memory.read<Int32>(G.client + hazedumper.signatures.dwEntityList + ((id - 1) * 0x10)));
		}
		public Int32 getaddress()
		{
			return this.address;
		}
		public int health
		{
			get
			{
				return Memory.read<Int32>(this.address + hazedumper.netvars.m_iHealth);
			}
		}
		public int team
		{
			get
			{
				return Memory.read<Int32>(this.address + hazedumper.netvars.m_iTeamNum);
			}
		}
		public int flags
		{
			get
			{
				return Memory.read<Int32>(this.address + hazedumper.netvars.m_iTeamNum);
			}
		}
		public int tickbase
		{
			get
			{
				return Memory.read<Int32>(this.address + hazedumper.netvars.m_nTickBase);
			}
		}
		public int fov
		{
			get
			{
				return Memory.read<Int32>(this.address + hazedumper.netvars.m_iFOV);
			}
			set
			{
				Memory.write<Int32>(this.address + hazedumper.netvars.m_iFOV, value);
			}
		}
		public int shotsfired
		{
			get
			{
				return Memory.read<Int32>(this.address + hazedumper.netvars.m_iShotsFired);
			}
		}
		public int movetype
		{
			get
			{
				return Memory.read<Int32>(this.address + hazedumper.netvars.m_MoveType);
			}
		}
		public Int32 spotted
		{
			get
			{
				return Memory.read<Int32>(this.address + hazedumper.netvars.m_bSpotted);
			}
			set
			{
				Memory.write<Int32>(this.address + hazedumper.netvars.m_bSpotted, value);
			}
		}
		public int glowindex
		{
			get
			{
				return Memory.read<Int32>(this.address + hazedumper.netvars.m_iGlowIndex);
			}
		}
		public bool scoped
		{
			get
			{
				return Memory.read<bool>(this.address + hazedumper.netvars.m_bIsScoped);
			}
		}
		public bool visible
		{
			get
			{
				return Memory.read<bool>(this.address + hazedumper.netvars.m_bSpottedByMask);
			}
		}
		public int modelindex
		{
			get
			{
				return Memory.read<Int32>(this.address + offsets.n_ModelIndex);
			}
			set
			{
				Memory.write<Int32>(this.address + offsets.n_ModelIndex, value);
			}
		}
		public int observermode
		{
			get
			{
				return Memory.read<Int32>(this.address + hazedumper.netvars.m_iObserverMode);
			}
			set
			{
				Memory.write<Int32>(this.address + hazedumper.netvars.m_iObserverMode, value);
			}
		}
		public int crosshairid
		{
			get
			{
				return Memory.read<Int32>(this.address + hazedumper.netvars.m_iCrosshairId);
			}
		}
		public int accountid
		{
			get
			{
				return Memory.read<Int32>(this.address + hazedumper.netvars.m_iAccountID);
			}
		}
		public Vec3 aimpunch
		{
			get
			{
				return Memory.read<Vec3>(this.address + hazedumper.netvars.m_aimPunchAngle);
			}
			set
			{
				Memory.write<Vec3>(this.address + hazedumper.netvars.m_aimPunchAngle, value);
			}
		}
		public Vec3 viewangles
		{
			get
			{
				return Memory.read<Vec3>(engine.clientstate + hazedumper.signatures.dwClientState_ViewAngles);
			}
			set
			{
				Memory.write<Vec3>(engine.clientstate + hazedumper.signatures.dwClientState_ViewAngles, value);
			}
		}
		public bool enemyincross
		{
			get
			{
				return (this.crosshairid != 0 && this.crosshairent.team != 0 && this.crosshairent.isenemy && this.crosshairent.health > 0);
			}
		}
		public Entity crosshairent
		{
			get
			{
				return Entity.fromid(this.crosshairid);
			}
		}
		public bool isenemy
		{
			get
			{
				return (this.team == G.player.team) ? false : true;
			}
		}
		public float velocity
		{
			get
			{
				Vector3 vel = Memory.read<Vector3>(this.address + hazedumper.netvars.m_vecVelocity);
				return (vel.X + vel.Y + vel.Z);
			}
		}
		public bool onGround
		{
			get
			{
				Int32 fFlag = Memory.read<Int32>((this.address + hazedumper.netvars.m_fFlags));
				return (fFlag == 263 || fFlag == 257) ? true : false;
			}
		}
		public Vec3 getbonepos(int BoneID)
		{
			int BoneMatrix = Memory.read<Int32>(this.address + hazedumper.netvars.m_dwBoneMatrix);
			Vec3 position = new Vec3
			{
				x = Memory.read<float>(BoneMatrix + 0x30 * BoneID + 0x0C),
				y = Memory.read<float>(BoneMatrix + 0x30 * BoneID + 0x1C),
				z = Memory.read<float>(BoneMatrix + 0x30 * BoneID + 0x2C)
			};
			return position;
		}
	}
}