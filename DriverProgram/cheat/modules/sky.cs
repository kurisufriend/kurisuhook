using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using recode;
using recode.sdk;
// FCVAR_REPLICATED (1<<13)
// FCVAR_CHEAT (1<<14)
namespace kurisuhook.cheat.modules
{
	public enum skycodes : Int32
	{
		cs_baggage_skybox_,
		cs_tibet,
		vietnam,
		sky_lunacy,
		embassy,
		italy,
		jungle,
		office,
		sky_cs15_daylight01_hdr,
		sky_cs15_daylight02_hdr,
		sky_day02_05,
		nukeblank,
		dustblank,
		sky_venice,
		sky_cs15_daylight03_hdr,
		sky_cs15_daylight04_hdr,
		sky_csgo_cloudy01,
		sky_csgo_night02b,
		vertigo,
		vertigoblue_hdr
	}
	public static class sky
	{
		public static string[] skyArr = Enum.GetNames(typeof(skycodes));
		public static void run()
		{
			ConVar sky = new ConVar("sv_skyname");
			sky.ClearCallbacks();
			sky.SetFlags(sky.GetFlags() & (1 << 14));
			sky.SetFlags(sky.GetFlags() & (1 << 13));
            while (true)
            {
                Thread.Sleep(500);
                if (G.settings.skychanger)
                {
					ClientCMD.Exec("sv_skyname " + skyArr[G.settings.sky]);
                }
            }
        }
	}
}
