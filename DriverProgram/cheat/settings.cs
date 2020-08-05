using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;
using System.IO;
using Newtonsoft.Json;
using recode.lib;

namespace recode
{
	public class settings
	{
		// misc
		public bool bunnyhop;
		public bool fovchanger;
		public int fov;
		public bool nohands;
		public bool thirdperson;
		// shoot
		public bool triggerbot;
		public Int32 triggerkey;
		public bool rcs;
		public float rcsintensityx;
		public float rcsintensityy;
		public bool rcssmoothing;
		public float rcsmoothingintensity;
		public bool rcsafter;
		public bool aimbot;
		public float aimbotsmoothing;
		public bool aimbotrcs;
		public Int32 aimkey;
		public float aimbotfov;
		// visuals
		public bool glow;
		public bool fullbloom;
		public Vector4 glowcolor;
		public Vector4 glowenemycolor;
		public bool radar;

		public void save(string name)
		{
			if (name == "")
				return;
			var f = File.CreateText(name);
			f.Write(JsonConvert.SerializeObject(G.settings));
			f.Close();
		}
		public void load(string name)
		{
			if (!File.Exists(name))
				return;
			string source = File.ReadAllText(name);
			G.settings =  JsonConvert.DeserializeObject<settings>(source);
		}
	}
}
