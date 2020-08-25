﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Drawing;
using System.Text;
using recode.lib;
using recode.sdk;
using System.Runtime.Serialization.Formatters;

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
		public static Entity[] getEntityList()
		{
			List<Entity> list = new List<Entity>();
			for (int i = 0; i < 64; i++)
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
		public static player_info_t getPlayerInfo(int i)
		{
			int pInfo = Memory.read<int>(engine.clientstate + hazedumper.signatures.dwClientState_PlayerInfo);
			pInfo = Memory.read<int>(pInfo + 0x40);
			pInfo = Memory.read<int>(pInfo + 0xC);
			pInfo = Memory.read<int>(pInfo + 0x28 + (i) * 0x34);
			player_info_t info = Memory.read<player_info_t>(pInfo);

			return info;
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
		public static Vec3 LinearInterp(Vec3 src, Vec3 dst, float factor)
		{
			float x1 = src.x;
			float x2 = dst.x;
			float y1 = src.y;
			float y2 = dst.y;

			if (src.Equals(dst)) { return src; };

			if (x1 < x2)
				src.x++;
			if (x1 > x2)
				src.x--;
			if (y1 < y2)
				src.y++;
			if (y1 > y2)
				src.y--;
			return src;
		}
		public static Vec3 NonlinearInterp(Vec3 src, Vec3 dst, float factor)
		{
			return src + (dst - src) / factor;
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
