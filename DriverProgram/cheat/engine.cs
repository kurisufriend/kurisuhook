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
		public static bool sendpackets
		{
			get
			{
				return Memory.read<bool>(G.engine + hazedumper.signatures.dwbSendPackets);
			}
			set
			{
				Memory.write<bool>(G.engine + hazedumper.signatures.dwbSendPackets, value);
			}
		}
	}
}
