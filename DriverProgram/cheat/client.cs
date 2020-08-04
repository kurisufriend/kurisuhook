using System;
using System.Collections.Generic;
using System.Text;
using recode;
using recode.lib;
using recode.sdk;

namespace recode
{
	public class client
	{
		public static Int32 glowobject
		{
			get
			{
				return Memory.read<Int32>(G.client + hazedumper.signatures.dwGlowObjectManager);
			}
		}
		public static Int32 entitylist
		{
			get
			{
				return Memory.read<Int32>(G.client + hazedumper.signatures.dwEntityList);
			}
		}
	}
}
