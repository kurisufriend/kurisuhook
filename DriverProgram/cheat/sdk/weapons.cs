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
			WEAPON_KNIFE_BAYONET = 500,   
			WEAPON_KNIFE_FLIP = 505,       
			WEAPON_KNIFE_GUT = 506,           
			WEAPON_KNIFE_KARAMBIT = 507,    
			WEAPON_KNIFE_M9_BAYONET = 508,   
			WEAPON_KNIFE_TACTICAL = 509,    
			WEAPON_KNIFE_FALCHION = 512,    
			WEAPON_KNIFE_SURVIVAL_BOWIE = 514, 
			WEAPON_KNIFE_BUTTERFLY = 515,      
			WEAPON_KNIFE_PUSH = 516,      
			WEAPON_KNIFE_URSUS = 519,          
			WEAPON_KNIFE_GYPSY_JACKKNIFE = 520, 
			WEAPON_KNIFE_STILETTO = 522,        
			WEAPON_KNIFE_WIDOWMAKER = 523     
		}
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
