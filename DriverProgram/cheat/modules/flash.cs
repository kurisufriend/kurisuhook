using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using recode;
using recode.lib;
using recode.sdk;

namespace kurisuhook.cheat.modules
{
	public static class flash
	{
		public static void run()
		{
			if (G.player.maxflashalpha != G.settings.maxflash)
			{
				G.player.maxflashalpha = G.settings.maxflash;
			}
		}
	}
}
