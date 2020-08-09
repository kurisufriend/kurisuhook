using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using recode;
using recode.lib;
using recode.sdk;

namespace kurisuhook.cheat.modules
{
	public static class colorhands
	{
		public static void run()
		{
			Entity vm = new Entity(G.player.viewmodel);
			vm.clrrender = new bytecolor(G.settings.handcolor);
		}
	}
}
