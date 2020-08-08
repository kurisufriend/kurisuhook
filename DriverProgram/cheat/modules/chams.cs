using System;
using System.Drawing;
using System.Collections.Generic;
using System.Text;
using recode;
using recode.lib;
using recode.sdk;

namespace recode.modules
{
	public static class chams
	{
		public static void run()
		{
			foreach (Entity ent in G.entitylist)
			{
				if (ent != null && ent.isenemy)
				{
					ent.clrrender = new bytecolor(G.settings.chamscolor);
				}
			}
		}
	}
}
