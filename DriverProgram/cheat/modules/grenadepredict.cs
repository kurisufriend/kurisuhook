using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using recode.lib;
using recode.sdk;

namespace recode.modules
{
	public static class grenade
	{
		public static void run()
		{
			ConVar clGrenadePreview = new ConVar("cl_grenadepreview");
			clGrenadePreview.ClearCallbacks();
			clGrenadePreview.SetFlags(clGrenadePreview.GetFlags() & (1 << 14));
			// sky.SetFlags(sky.GetFlags() & (1 << 13));
			while (true)
			{
				Thread.Sleep(500);
				if (G.settings.grenade)
				{
					clGrenadePreview.SetValue(1);
				}
				else
				{
					clGrenadePreview.SetValue(0);
				}
			}
		}
	}
}

