using System;
using System.Drawing;
using System.Collections.Generic;
using System.Text;
using recode;
using recode.lib;
using recode.sdk;
using System.Reflection.Metadata.Ecma335;

namespace recode.modules
{
	class aim
	{
		public static void run()
		{
			Entity target = utils.getTarget();
			Vec3 angtoaim = utils.NormalizedAngle(utils.LinearInterp(G.player.viewangles, utils.RCS(utils.CalcAngle(G.player.eyeposition, target.getbonepos(8)), G.settings.aimbotrcs ? 1 : 0), (G.settings.aimbotsmoothing > 0) ? G.settings.aimbotsmoothing : 1));
			if ((G.settings.aimkey != 0) && (winapi.GetAsyncKeyState((int)winapi.vkeyArrVals.GetValue(G.settings.aimkey)) == 0))
			{
				return;
			}
			float distance = utils.Vec3Distance(G.player.viewangles, utils.NormalizedAngle(utils.RCS(utils.CalcAngle(G.player.eyeposition, target.getbonepos(8)))));
			if (distance < G.settings.aimbotfov)
				G.player.viewangles = angtoaim;
		}
	}
}
