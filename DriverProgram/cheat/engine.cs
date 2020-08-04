using System;
using System.Collections.Generic;
using System.Text;
using recode;
using recode.lib;
using recode.sdk;

namespace recode
{
	public class engine
	{
		public static Int32 clientstate
		{
			get
			{
				return Memory.read<Int32>(G.engine + hazedumper.signatures.dwClientState);
			}
		}
	}
}
