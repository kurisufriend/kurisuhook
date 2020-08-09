using System;
using System.Drawing;
using System.Collections.Generic;
using System.Text;
using recode;
using recode.lib;
using recode.sdk;

namespace recode.modules
{
	public static class thirdperson
	{
		public static void run()
		{
			G.player.observermode = G.settings.observermode;
		}
	}
}
