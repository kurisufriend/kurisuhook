using System;
using System.Collections.Generic;
using System.Text;

namespace recode.lib
{
	public static class debug
	{
		public static void fatal(string reason)
		{
			Console.WriteLine(reason);
			Console.ReadLine();
			Environment.Exit(0);
		}
	}
}
