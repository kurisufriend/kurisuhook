using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using ImGuiNET;
using recode.lib;

namespace recode.modules
{
	public static class fakelag
	{
		public static int choked = 0;
		public static void run()
		{
			while (true)
			{
				Thread.Sleep(1);
				if (G.settings.fakelag)
				{
					if (choked < G.settings.lagamount)
					{
						engine.sendpackets = false;
						choked++;
					}
					else
					{
						engine.sendpackets = true;
						choked = 0;
					}
					Thread.Sleep(15);
				}
			}
		}
	}
}
