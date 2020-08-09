using System;
using System.Collections.Generic;
using System.Text;
using recode;
using recode.lib;
using recode.sdk;

namespace recode.modules
{
	public static class radar
	{
		public static void run()
		{
			foreach (Entity ent in G.entitylist)
			{
				if (ent.spotted == 0)
				{
					ent.spotted = 1;
				}
			}
		}
	}
}
