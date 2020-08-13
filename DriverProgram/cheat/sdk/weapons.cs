using System;
using System.Collections.Generic;
using System.Text;

namespace recode.sdk
{
	public static class weapons
	{
		public enum knifeDefinitionIndex : Int32
		{
			WEAPON_KNIFE = 42,
			WEAPON_KNIFE_T = 59,
			KNIFE_BAYONET = 500,
			KNIFE_CSS = 503,
			KNIFE_FLIP = 505,
			KNIFE_GUT = 506,
			KNIFE_KARAMBIT = 507,
			KNIFE_M9_BAYONET = 508,
			KNIFE_TACTICAL = 509,
			KNIFE_FALCHION = 512,
			KNIFE_BOWIE = 514,
			KNIFE_BUTTERFLY = 515,
			KNIFE_PUSH = 516,
			KNIFE_CORD = 517,
			KNIFE_CANIS = 518,
			KNIFE_URSUS = 519,
			KNIFE_GYPSY = 520,
			KNIFE_OUTDOOR = 521,
			KNIFE_STILETTO = 522,
			KNIFE_WIDOWMAKER = 523,
			KNIFE_SKELETON = 525
		}
		public static string[] knifidArr = Enum.GetNames(typeof(knifeDefinitionIndex));
		public static Array knifeidArrVals = Enum.GetValues(typeof(knifeDefinitionIndex));
		public enum knifestuff : Int32
		{
			bayonet = 0,
			classic = 1,
			flip = 2,
			gut = 3,
			karambit = 4,
			m9_bayonet = 5,
			huntsman = 6,
			falchion = 7,
			bowie = 8,
			butterfly = 9,
			shadow_daggers = 10,
			paracord = 11,
			survival = 12,
			ursus = 13,
			navaja = 14,
			nomad = 15,
			stiletto = 16,
			talon = 17,
			skeleton = 18
		}
		public static string[] knifeArr = Enum.GetNames(typeof(knifestuff));
		public static Array knifeArrVals = Enum.GetValues(typeof(knifestuff));
	}
}
