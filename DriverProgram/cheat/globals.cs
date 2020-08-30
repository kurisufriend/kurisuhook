using System;
using System.Collections.Generic;
using System.Text;
using recode.sdk;

namespace recode
{
	public static class G
	{
		public static Int32 client;
		public static Int32 engine;
		public static Int32 vstdlib;
		public static LocalPlayer player;
		public static Int32 playeraddress;
		public static Int32 normalhands; // used to reset nohands
		public static settings settings;
		public static Entity[] entitylist;
		public static Entity[] bigentitylist;
	}
}
