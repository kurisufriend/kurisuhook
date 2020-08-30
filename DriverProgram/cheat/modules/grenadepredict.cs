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
			
			ConVar svGrenadeTrajectory = new ConVar("sv_grenade_trajectory");
			svGrenadeTrajectory.ClearCallbacks();
			svGrenadeTrajectory.SetFlags(clGrenadePreview.GetFlags() & (1 << 13));
			svGrenadeTrajectory.SetFlags(clGrenadePreview.GetFlags() & (1 << 14));

			while (true)
			{
				Thread.Sleep(10);
				if (G.settings.grenade && clGrenadePreview.GetInt() == 0)
				{
					clGrenadePreview.SetValue(1);
				}
				else if (!G.settings.grenade && clGrenadePreview.GetInt() != 0)
				{
					clGrenadePreview.SetValue(0);
				}
				if (G.settings.grenadetrace && svGrenadeTrajectory.GetInt() == 0)
				{
					svGrenadeTrajectory.SetValue(1);
				}
				else if (!G.settings.grenadetrace && svGrenadeTrajectory.GetInt() != 0)
				{
					svGrenadeTrajectory.SetValue(0);
				}
			}
		}
	}
}

