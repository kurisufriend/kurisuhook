using System;
using System.Collections.Generic;
using System.Text;

// offsets found using ghidra pattern scan them later // actually ida thanks ny2rogen or whatever your name was
namespace recode
{
	public static class offsets
	{
		public static Int32 n_ModelIndex = 0x258;
		public static Int32 m_hViewModel = 0x32f8;
		public static Int32 m_iViewModelIndex = 0x3240;
		public static Int32 m_totalHitsOnServer = 0x0A3A8;
		public static Int32 m_bFlipViewModel = 0x32C4; // masterlooser viewmodel makes cheat better instantly // nvm useless shit literally does nothing
		public static Int32 m_iNumRoundKills = 0x3954;
	}
}
