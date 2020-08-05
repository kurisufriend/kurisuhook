using System;
using System.Collections.Generic;
using System.Text;
using recode.lib;

namespace recode.modules
{
	public static class trigger
	{
		public static void run()
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
