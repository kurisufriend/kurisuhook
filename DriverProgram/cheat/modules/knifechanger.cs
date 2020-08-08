using System;
using System.Collections.Generic;
using System.Text;
using recode;
using recode.lib;
using recode.sdk;

using static recode.sdk.weapons.knifeDefinitionIndex;

namespace kurisuhook.cheat.modules
{
	class knifechanger
	{
		const int precache_bayonet_ct = 89;
		const int precache_bayonet_t = 64;
		public static void run()
		{
			Weapon weapon = new Weapon(G.player.curweapon);
			int vmid = weapon.viewmodelindex;
			int modelindex = (int)weapons.knifeArrVals.GetValue(G.settings.knife);
			int offset = (modelindex < 11) ? 1 : 2;
			if (weapon.econid == (int)WEAPON_KNIFE && vmid > 0)
			{
				G.player.viewmodelmodelindex = vmid + precache_bayonet_ct + 3 * modelindex + offset;
			}
			else if (weapon.econid == (int)WEAPON_KNIFE_T && vmid > 0)
			{
				G.player.viewmodelmodelindex = vmid + precache_bayonet_t + 3 * modelindex + offset;
			}
		}
	}
}
