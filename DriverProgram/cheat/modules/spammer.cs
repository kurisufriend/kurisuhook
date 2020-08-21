using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using recode.sdk;
using recode.lib;
using System.Data;
using Vulkan;
using System.Security.Cryptography.X509Certificates;
using System.Net;
using System.IO;

namespace recode.modules
{
	class spammer
	{
		public static string[] themes = new string[] { "dad jokes", "insults" };
		public static async void run()
		{
			var webclient = new WebClient();
			while (true)
			{
				Thread.Sleep(1);
				if (G.settings.spammer)
				{
					webclient.Headers.Add("Accept", "text/plain");
					ClientCMD.Exec("say \"" + (G.settings.themespammer ? await webclient.DownloadStringTaskAsync(new Uri((G.settings.theme == 0 ? "https://icanhazdadjoke.com/" : "https://insult.mattbas.org/api/insult"))) : G.settings.spamstring) + "\"");
					Thread.Sleep(G.settings.spamdelay * 500);
				}
			}
		}
	}
}

