using System;
using System.Drawing;
using System.Collections.Generic;
using System.Text;
using System.Media;
using recode;
using recode.lib;
using recode.sdk;

namespace recode.modules
{
	public enum sounds
	{
		skeet,
		error,
		iago
	}
	public static class hits
	{
		public static int hitcount = G.player.totalhits;
		public static string env = Environment.CurrentDirectory;
		public static string[] soundsArr = Enum.GetNames(typeof(sounds));
		public static void run()
		{
			if (G.player.totalhits > hitcount)
			{
				System.Media.SoundPlayer player = new System.Media.SoundPlayer(env + "\\" + soundsArr[G.settings.hitsound] + ".wav"); // reminds me of 70 char string concats in php
				player.Play();
				hitcount = G.player.totalhits;
			}
			if (G.player.totalhits == 0)
				hitcount = 0;
		}
	}
}
