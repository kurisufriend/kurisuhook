using System;
using System.Drawing;
using System.Collections.Generic;
using System.Text;
using recode;
using recode.lib;
using recode.sdk;
using System.Reflection.Metadata.Ecma335;
using System.Threading;

namespace recode.modules
{
	public static class aim
	{
		public static void run()
		{
			while (true)
			{
				Thread.Sleep(1);
				if (G.settings.aimbot)
				{
					Entity target = utils.getTarget();
					if (G.settings.aimvisible && !target.visible)
						continue;
					Vec3 angtoaim = utils.NormalizedAngle(utils.NonlinearInterp(G.player.viewangles, utils.RCS(utils.CalcAngle(G.player.eyeposition, target.getbonepos((int)models.bonesArrVals.GetValue(G.settings.aimbone))), G.settings.aimbotrcs ? 1 : 0), (G.settings.aimbotsmoothing > 0) ? G.settings.aimbotsmoothing : 1));
					if ((G.settings.aimkey != 0) && (winapi.GetAsyncKeyState((int)winapi.vkeyArrVals.GetValue(G.settings.aimkey)) == 0))
					{
						continue;
					}
					float distance = utils.Vec3Distance(G.player.viewangles, utils.NormalizedAngle(utils.RCS(utils.CalcAngle(G.player.eyeposition, target.getbonepos((int)models.bonesArrVals.GetValue(G.settings.aimbone))))));
					if (distance < G.settings.aimbotfov && !target.dormant && target.health > 0)
						G.player.viewangles = angtoaim;
				}
			}
		}
	}
}
