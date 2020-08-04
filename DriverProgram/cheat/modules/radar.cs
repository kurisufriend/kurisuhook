using System;
using System.Collections.Generic;
using System.Text;
using recode;
using recode.lib;
using recode.sdk;

namespace recode.modules
{
	class radar
	{
		public static void run()
		{
			foreach (Entity ent in utils.getEntityList())
			{
				if (ent.spotted == 0)
				{
					ent.spotted = 1;
				}
			}
		}
	}
}
