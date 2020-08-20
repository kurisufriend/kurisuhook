using System;
using System.Drawing;
using System.Collections.Generic;
using System.Text;
using recode;
using recode.lib;
using recode.sdk;

namespace recode.modules
{
	public static class glow
	{
		public static void run()
		{
			foreach (Entity ent in G.entitylist)
			{
				if (ent != null && ent.team != G.player.team)
				{
					GlowStruct gs = new GlowStruct(G.settings.glowcolor);
					// GlowObjectMamager->GlowObjectDefinitions[i];
					Memory.write<GlowStruct>(client.glowobject + ent.glowindex * 0x38 + 0x4, gs);
					Memory.write<bool>(client.glowobject + ent.glowindex * 0x38 + 0x24, true);
					Memory.write<bool>(client.glowobject + ent.glowindex * 0x38 + 0x2C, G.settings.fullbloom);
				}
			}
		}
	}
}
