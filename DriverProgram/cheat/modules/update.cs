using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using recode.lib;
using recode.sdk;

namespace recode.modules
{
	public static class update
	{
		public static void run()
		{
			while (true)
			{
				G.player = new LocalPlayer(utils.getLocalPlayer());
				G.playeraddress = utils.getLocalPlayer();
				G.entitylist = utils.getEntityList();
				if (G.player.modelindex != 0)
					G.normalhands = G.player.modelindex;
				Thread.Sleep(1);
			}
		}
	}
}
