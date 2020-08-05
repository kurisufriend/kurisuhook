using System;
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
			foreach (Entity ent in utils.getEntityList())
			{
				if (ent.isenemy && ent.health > 0 && !ent.dormant)
				{
					Vec3 ang = NormalizedAngle(RCS(CalcAngle(G.player.eyeposition, ent.getbonepos(8))));
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
	}
}
