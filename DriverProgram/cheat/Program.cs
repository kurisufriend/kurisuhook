using System;
using System.Threading;
using recode.lib;
using recode.modules;
using recode.sdk;

namespace recode
{
	public class Program
	{
		public static void oldmain(string[] args)
		{
			if (Memory.setup("csgo") != 1) { debug.fatal("error during memory setup"); }
			if (utils.setup() != 1) { debug.fatal("error during utils setup"); }
			int playercache = utils.getLocalPlayer();
			G.player = new LocalPlayer(playercache);
			for (int i = 0; winapi.GetAsyncKeyState((int)winapi.VirtualKeys.End) == 0 ; i++)
			{
				bhop.run();
				
				if (i % 200 == 0)
				{
					if (playercache != utils.getLocalPlayer())
					{
						playercache = utils.getLocalPlayer();
						G.player = new LocalPlayer(playercache);
					}
				}

				Thread.Sleep(1);
			}
			Console.WriteLine("fin");
			Console.ReadLine();
		}
	}
}
