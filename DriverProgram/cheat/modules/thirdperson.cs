using System;
using System.Drawing;
using System.Collections.Generic;
using System.Text;
using recode;
using recode.lib;
using recode.sdk;

namespace recode.modules
{
	class thirdperson
	{
		public static void run()
		{
			if (G.settings.thirdperson && G.player.observermode != 1)
				G.player.observermode = 1;
			else if (!G.settings.thirdperson && G.player.observermode != 0)
				G.player.observermode = 0;
		}
	}
}
