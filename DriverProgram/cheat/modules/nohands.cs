using System;
using System.Drawing;
using System.Collections.Generic;
using System.Text;
using recode;
using recode.lib;
using recode.sdk;

namespace recode.modules
{
	public static class nohands
	{
		public static void run()
		{
			G.player.modelindex = (short)models.indexArrVals.GetValue(G.settings.model);
		}
	}
}
