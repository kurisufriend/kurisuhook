using System;
using System.Collections.Generic;
using System.Text;

namespace recode.sdk
{
	public static class models
	{
		public enum indices : short
		{
			nothing = 0,
			ct_mirage = 164,
			t_mirage = 161,
			ct_inferno = 178,
			t_inferno = 169,
			ct_overpass = 147,
			t_overpass = 152,
			ct_vertigo = 195,
			t_vertigo = 188,
			ct_nuke = 152,
			t_nuke = 150,
			ct_train = 236,
			t_train = 235,
			ct_dust2 = 71,
			t_dust2 = 62,
			ct_cache = 313,
			t_cache = 311
		}
		public static string[] indexArr = Enum.GetNames(typeof(indices));
		public static Array indexArrVals = Enum.GetValues(typeof(indices));
		public enum bones : Int32
		{
			head = 8,
			neck = 7,
			pelvis = 0,
			right_shoulder = 38,
			right_elbow = 39,
			right_hand = 40,
			right_knee = 73,
			right_foot = 74,
			left_shoulder = 10,
			left_elbow = 11,
			left_hand = 12,
			left_knee = 66,
			left_foot = 67
		}
		public static string[] bonesArr = Enum.GetNames(typeof(bones));
		public static Array bonesArrVals = Enum.GetValues(typeof(bones));
	}
}
