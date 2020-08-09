using System;
using System.Collections.Generic;
using System.Text;
using recode.lib;

namespace recode.modules
{
	public static class fov
	{
		public static void run()
		{
			if (G.player.fov != G.settings.fov && !G.player.scoped)
			{
				G.player.fov = G.settings.fov;
			}
		}
	}
}
