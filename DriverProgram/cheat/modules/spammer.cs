using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using recode.sdk;
using recode.lib;

namespace recode.modules
{
	class spammer
	{
		public static void run()
		{
			while (true)
			{
				Thread.Sleep(1);
				if (G.settings.spammer)
				{
					ClientCMD.Exec("say \"" + G.settings.spamstring + "\"");
					Thread.Sleep(G.settings.spamdelay * 500);
				}
			}
		}
	}
}

