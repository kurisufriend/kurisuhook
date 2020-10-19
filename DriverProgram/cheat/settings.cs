﻿using System;
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
		public bool strafer;
		public Int32 straferkey;
		public bool fovchanger;
		public int fov;
		public bool modelchanger;
		public Int32 model;
		public bool perspectivechanger;
		public int observermode;
		public bool speclist;
		public bool watermark = true;
		public bool flashchanger;
		public float maxflash;
		public bool spammer;
		public bool themespammer;
		public Int32 theme;
		public string spamstring = "";
		public int spamdelay = 1;
		public bool hitsounds;
		public Int32 hitsound;
		public bool skychanger;
		public Int32 sky;
		public bool fakelag;
		public int lagamount = 1;
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
		public bool aimvisible;
		public float aimbotsmoothing;
		public bool aimbotrcs;
		public Int32 aimkey;
		public float aimbotfov;
		public Int32 aimbone;
		public bool nearest;
		public bool awpbot;
		public Int32 funnytarget;
		public bool noknifeaim;
		// visuals
		public bool glow;
		public bool fullbloom;
		public Vector4 glowcolor;
		public bool radar;
		public bool chams;
		public Vector4 chamscolor;
		public bool colorhands;
		public Vector4 handcolor;
		public bool viewmodelchanger;
		public int viewmodelfov;
		public int viewmodelx;
		public int viewmodely;
		public int viewmodelz;
		public bool flipviewmodel;
		public bool crosshair;
		public bool recoilcrosshair;
		public bool esp;
		public bool grenade;
		public bool grenadetrace;
		public bool tracers;
		public bool moonman;
		public int powerlevel;
		// purple people eater
		public bool knifechanger;
		public Int32 knife;
		public void save(string name)
		{
			if (name == "")
				return;
			var f = File.CreateText(name + ".khook");
			f.Write(JsonConvert.SerializeObject(G.settings));
			f.Close();
		}
		public void load(string name)
		{
			if (!File.Exists(name + ".khook"))
				return;
			string source = File.ReadAllText(name + ".khook");
			G.settings =  JsonConvert.DeserializeObject<settings>(source);
		}
	}
}
