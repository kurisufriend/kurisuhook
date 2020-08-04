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
			if (winapi.GetAsyncKeyState((int)winapi.VirtualKeys.ExtraButton2) != 0 && G.player.enemyincross)
			{
				G.player.shoot();
			}
		}
	}
}
