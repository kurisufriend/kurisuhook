using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Drawing;
using System.Text;
using recode.lib;
using recode.sdk;
using System.Runtime.Serialization.Formatters;
using recode.modules;
using kurisuhook.cheat.sdk;
using ImGuiNET;

namespace recode
{
	public static class utils
	{
		public static int setup()
		{
			G.engine = Memory.getModuleBA("engine.dll");
			G.client = Memory.getModuleBA("client.dll");
			G.vstdlib = Memory.getModuleBA("vstdlib.dll");
			if (G.engine != 0 && G.client != 0)
				return 1;
			else
				return 0;
		}
		public static Int32 getLocalPlayer()
		{
			return Memory.read<Int32>(G.client + hazedumper.signatures.dwLocalPlayer);
		}
		public static Entity[] getEntityList(int size = 64)
		{
			List<Entity> list = new List<Entity>();
			for (int i = 0; i < size; i++)
			{
				int obj = Memory.read<Int32>(G.client + hazedumper.signatures.dwEntityList + i * 0x10);	
				if (obj != 0)
				{
					list.Add(new Entity(obj));
				}
			}
			return list.ToArray();
		}
		public static Entity getTarget()
		{
			float distance = Int32.MaxValue;
			Entity best = new Entity(0);
			foreach (Entity ent in G.entitylist)
			{
				if (ent.isenemy && ent.health > 0 && !ent.dormant)
				{
					Vec3 ang = NormalizedAngle(RCS(CalcAngle(G.player.eyeposition, ent.getbonepos((int)models.bonesArrVals.GetValue(G.settings.aimbone)))));
					float curdist = utils.Vec3Distance(G.player.viewangles, ang);
					if (curdist < distance)
					{
						distance = curdist;
						best = ent;
					}
				}
			}
			return best;
		}
		public static Entity getClass(int id)
		{
			float distance = Int32.MaxValue;
			Entity best = new Entity(0);
			foreach (Entity ent in utils.getEntityList(1024))
			{
				if (utils.getClassId(ent) == id && !ent.dormant)
				{
					Vec3 ang = NormalizedAngle(RCS(CalcAngle(G.player.eyeposition, ent.position)));
					float curdist = utils.Vec3Distance(G.player.viewangles, ang);
					if (curdist < distance)
					{
						distance = curdist;
						best = ent;
					}
				}
			}
			return best;
		}
		public static int getBestAimpoint(Entity ent)
		{
			float best = float.MaxValue;
			int bestBone = 8;
			foreach (int bone in models.bonesArrVals)
			{
				float distance = utils.Vec3Distance(G.player.viewangles, utils.NormalizedAngle(utils.RCS(utils.CalcAngle(G.player.eyeposition, ent.getbonepos(bone)))));
				if (distance < best)
				{
					best = distance;
					bestBone = bone;
				}
			}
			return bestBone;
		}
		public static float[] WorldToScreen(Matrix matrix, Entity entity, int width, int height, bool foot = true)
		{
			float zPos = entity.position.z;

			//multiply vector against matrix
			float screenX = (matrix.m11 * entity.position.x) + (matrix.m21 * entity.position.y) + (matrix.m31 * zPos) + matrix.m41;
			float screenY = (matrix.m12 * entity.position.x) + (matrix.m22 * entity.position.y) + (matrix.m32 * zPos) + matrix.m42;
			float screenW = (matrix.m14 * entity.position.x) + (matrix.m24 * entity.position.y) + (matrix.m34 * zPos) + matrix.m44;


			//camera position (eye level/middle of screen)
			float camX = width / 2f;
			float camY = height / 2f;

			//convert to homogeneous position
			float x = camX + (camX * screenX / screenW);
			float y = camY - (camY * screenY / screenW);
			float[] screenPos = { x, y };

			//check it is in the bounds to draw
			if (screenW > 0.001f  //not behind us
				&& 0 + x > 0 && 0 + x < 1920 //not off the left or right of the window
				&& 0 + y > 0 && 0 + y < 1080) //not off the top of bottom of the window
			{
				return screenPos;
			}
			return null;
		}
		public static player_info_t getPlayerInfo(int i)
		{
			int pInfo = Memory.read<int>(engine.clientstate + hazedumper.signatures.dwClientState_PlayerInfo);
			pInfo = Memory.read<int>(pInfo + 0x40);
			pInfo = Memory.read<int>(pInfo + 0xC);
			pInfo = Memory.read<int>(pInfo + 0x28 + (i) * 0x34);
			player_info_t info = Memory.read<player_info_t>(pInfo);

			return info;
		}
		public static int getClassId(Entity entity)
		{
			Int32 clientNetworkable = Memory.read<Int32>(entity.getaddress() + 0x8);
			Int32 getClientClassFn = Memory.read<Int32>(clientNetworkable + 2 * 0x4);
			Int32 entityClientClass = Memory.read<Int32>(getClientClassFn + 1);
			return Memory.read<int>(entityClientClass + 20);
		}
		public static void cmdAim(Vec3 angle, bool shoot = false)
		{
			Input_t input = Memory.read<Input_t>(G.client + hazedumper.signatures.dwInput);
			int desiredCMD = Memory.read<int>(engine.clientstate + hazedumper.signatures.clientstate_last_outgoing_command);
			Int32 incomingCMD = input.m_pCommands + (desiredCMD % 150) * 0x64;
			Int32 currentCMD = input.m_pCommands + ((desiredCMD - 1) % 150) * 0x64;
			Int32 verifiedCMD = input.m_pVerifiedCommands + ((desiredCMD - 1) % 150) * 0x68;
			int cmdNumber = 0;
			while (cmdNumber < desiredCMD)
				cmdNumber = Memory.read<int>(incomingCMD + 0x4);
			UserCmd_t cmd = Memory.read<UserCmd_t>(currentCMD);
			cmd.m_vecViewAngles = angle;
			if (shoot)
				G.player.shoot();
			Memory.write<UserCmd_t>(currentCMD, cmd);
			Memory.write<UserCmd_t>(verifiedCMD, cmd);

		}
		public static Vector4 ColorToVector4(Color color)
		{
			return new Vector4(
				(float)color.R / 255.0f,
				(float)color.G / 255.0f,
				(float)color.B / 255.0f,
				(float)color.A / 255.0f);
		}
		public static float Vec3Distance(Vec3 src, Vec3 dst)
		{
			float hypotenuse = (float)Math.Sqrt((dst.x - src.x) * (dst.x - src.x) + (dst.y - src.y) * (dst.y - src.y) + (dst.z - src.z) * (dst.z - src.z));
			return hypotenuse;
		}
		public static Vec3 CubicInterp(Vec3 src, Vec3 dst, float factor)
		{
			Vec3 delta = dst - src;
			delta.x = (float)Math.Pow((double)delta.x, 2);
			delta.y = (float)Math.Pow((double)delta.y, 2);
			delta.z = (float)Math.Pow((double)delta.z, 2);
			return src + delta;
		}
		public static Vec3 NonlinearInterp(Vec3 src, Vec3 dst, float factor)
		{
			Vec3 delta = dst - src;
			delta /= factor;
			return src + delta;
		}
		public static Vec3 RCS(Vec3 src, float factor = 1f)
		{
			src -= G.player.aimpunch * (2.0f * factor);
			return NormalizedAngle(src);
		}
		public static Vec3 CalcAngle(Vec3 src, Vec3 dst) // animesoftware
		{
			Vec3 angles = new Vec3 { x = 0, y = 0, z = 0 };
			double[] delta = { (src.x - dst.x), (src.y - dst.y), (src.z - dst.z) };
			float hyp = (float)Math.Sqrt(delta[0] * delta[0] + delta[1] * delta[1]);
			angles.x = (float)(Math.Atan(delta[2] / hyp) * 180.0f / Math.PI);
			angles.y = (float)(Math.Atan(delta[1] / delta[0]) * 180.0f / Math.PI);
			if (delta[0] >= 0.0f)
				angles.y += 180.0f;

			return angles;
		}
		public static Vec3 NormalizedAngle(Vec3 src)
		{
			while (src.x > 89.0f)
				src.x -= 180.0f;

			while (src.x < -89.0f)
				src.x += 180.0f;

			while (src.y > 180.0f)
				src.y -= 360.0f;

			while (src.y < -180.0f)
				src.y += 360.0f;

			if (src.y < -180.0f || src.y > 180.0f)
				src.y = 0.0f;

			if (src.x < -89.0f || src.x > 89.0f)
				src.x = 0.0f;

			return src;
		}
		public static float[] WorldToScreen(byte[] matrix, Entity entity, int width, int height, winapi.rect gameWindow, bool foot = true)
		{
			float m11 = BitConverter.ToSingle(matrix, 0), m12 = BitConverter.ToSingle(matrix, 16), m13 = BitConverter.ToSingle(matrix, 32), m14 = BitConverter.ToSingle(matrix, 48);
			float m21 = BitConverter.ToSingle(matrix, 4), m22 = BitConverter.ToSingle(matrix, 20), m23 = BitConverter.ToSingle(matrix, 36), m24 = BitConverter.ToSingle(matrix, 25);
			float m31 = BitConverter.ToSingle(matrix, 8), m32 = BitConverter.ToSingle(matrix, 24), m33 = BitConverter.ToSingle(matrix, 40), m34 = BitConverter.ToSingle(matrix, 56);
			float m41 = BitConverter.ToSingle(matrix, 12), m42 = BitConverter.ToSingle(matrix, 28), m43 = BitConverter.ToSingle(matrix, 44), m44 = BitConverter.ToSingle(matrix, 60);

			float zPos = entity.position.z;

			//multiply vector against matrix
			float screenX = (m11 * entity.position.x) + (m21 * entity.position.y) + (m31 * zPos) + m41;
			float screenY = (m12 * entity.position.x) + (m22 * entity.position.y) + (m32 * zPos) + m42;
			float screenW = (m14 * entity.position.x) + (m24 * entity.position.y) + (m34 * zPos) + m44;


			//camera position (eye level/middle of screen)
			float camX = width / 2f;
			float camY = height / 2f;

			//convert to homogeneous position
			float x = camX + (camX * screenX / screenW);
			float y = camY - (camY * screenY / screenW);
			float[] screenPos = { x, y };

			//check it is in the bounds to draw
			if (screenW > 0.001f  //not behind us
				&& gameWindow.left + x > gameWindow.left && gameWindow.left + x < gameWindow.right //not off the left or right of the window
				&& gameWindow.top + y > gameWindow.top && gameWindow.top + y < gameWindow.bottom) //not off the top of bottom of the window
			{
				return screenPos;
			}
			return null;
		}
	}
}
