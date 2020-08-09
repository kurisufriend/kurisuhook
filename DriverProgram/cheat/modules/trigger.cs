using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using recode.lib;

namespace recode.modules
{
	public static class trigger
	{
		public static void run()
		{
			while (true)
			{
				Thread.Sleep(1);
				if (G.settings.triggerbot)
				{
					if (G.settings.triggerkey == 0 && G.player.enemyincross)
					{
						G.player.shoot();
					}
					else if (winapi.GetAsyncKeyState((int)winapi.vkeyArrVals.GetValue(G.settings.triggerkey)) != 0 && G.player.enemyincross)
					{
						G.player.shoot();
					}
				}
			}
		}
	}
}
