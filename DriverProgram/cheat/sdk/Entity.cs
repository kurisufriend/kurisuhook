using System;
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
		private int id;
		public Entity(Int32 _address)
		{
			address = _address;
			for (int i = 0; i < 64; i++)
			{
				int obj = Memory.read<Int32>(G.client + hazedumper.signatures.dwEntityList + i * 0x10);
				if (obj == address)
					id = i;
			}
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
		public bool dormant
		{
			get
			{
				return Memory.read<bool>(this.address + hazedumper.signatures.m_bDormant);
			}
		}
		public bool visible
		{
			get
			{
				return (Memory.read<Int32>(this.address + hazedumper.netvars.m_bSpottedByMask) & (1 << G.playeraddress)) != 0;
			}
		}
		public short modelindex
		{
			get
			{
				return Memory.read<short>(this.address + offsets.n_ModelIndex);
			}
			set
			{
				Memory.write<short>(this.address + offsets.n_ModelIndex, value);
			}
		}
		public int viewmodel
		{
			get
			{
				int id = Memory.read<Int32>(this.address + offsets.m_hViewModel) & 0xFFF;
				return Memory.read<Int32>(G.client + hazedumper.signatures.dwEntityList + (id - 1) * 0x10);
			}
		}
		public int viewmodelmodelindex
		{
			get
			{
				return Memory.read<Int32>(this.viewmodel + offsets.n_ModelIndex);
			}
			set
			{
				Memory.write<Int32>(this.viewmodel + offsets.n_ModelIndex, value);
			}
		}
		public Int32 curweapon
		{
			get
			{
				int id = Memory.read<Int32>(this.address + hazedumper.netvars.m_hActiveWeapon) & 0xFFF;
				return Memory.read<Int32>(G.client + hazedumper.signatures.dwEntityList + (id - 1) * 0x10);
			}
		}
		public Int32 spectating
		{
			get
			{
				int id = Memory.read<Int32>(this.address + hazedumper.netvars.m_hObserverTarget) & 0xFFF;
				return Memory.read<Int32>(G.client + hazedumper.signatures.dwEntityList + (id - 1) * 0x10);
			}
		}
		public bytecolor clrrender
		{
			get
			{
				return Memory.read<bytecolor>(this.address + hazedumper.netvars.m_clrRender);
			}
			set
			{
				Memory.write<bytecolor>(this.address + hazedumper.netvars.m_clrRender, value);
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
		public Vec3 position
		{
			get
			{
				return Memory.read<Vec3>(this.address + hazedumper.netvars.m_vecOrigin);
			}
		}
		public Vec3 eyeposition
		{
			get
			{
				Vec3 pos = this.position;
				pos.z += Memory.read<float>(this.address + hazedumper.netvars.m_vecViewOffset + 0x8);
				return (pos);
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
