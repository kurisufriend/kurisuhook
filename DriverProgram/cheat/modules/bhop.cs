﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using recode.lib;

namespace recode.modules
{
	public static class bhop
	{
		public static void run()
		{
			while (true)
			{
				Thread.Sleep(1);
				if (G.settings.bunnyhop)
				{
					if (winapi.GetAsyncKeyState((int)winapi.VirtualKeys.Space) != 0 && G.player.onGround && G.player.velocity != 0f)
					{
						G.player.jump();
					}
				}
			}
		}
	}
}
