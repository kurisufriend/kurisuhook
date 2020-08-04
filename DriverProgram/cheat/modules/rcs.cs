using System;
using System.Drawing;
using System.Collections.Generic;
using System.Text;
using recode;
using recode.lib;
using recode.sdk;

namespace recode.modules
{
	class rcs
	{
		public static Vec3 cache = new Vec3(0, 0, 0);
		public static void run()
		{
			Vec3 punchangles = new Vec3(G.player.aimpunch.x * (2.0f * G.settings.rcsintensityx), G.player.aimpunch.y * (2.0f * G.settings.rcsintensityy), G.player.aimpunch.z);
			if (G.player.shotsfired > 1)
			{
				if (!G.settings.rcssmoothing)
					G.player.viewangles = (utils.NormalizedAngle(G.player.viewangles + cache - punchangles));
				else
					G.player.viewangles = (utils.NormalizedAngle( utils.LinearInterp (G.player.viewangles, G.player.viewangles + cache - punchangles, G.settings.rcsmoothingintensity) ));
			}
			cache = punchangles;
		}
	}
}
