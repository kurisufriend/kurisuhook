using System;
using System.Collections.Generic;
using System.Text;
using recode;
using recode.lib;
using SixLabors.ImageSharp.PixelFormats;

namespace recode.sdk
{
	class Weapon
	{
		private Int32 address;
		public Weapon(int _address)
		{
			address = _address;
		}
		public short modelindex
		{
			get
			{
				return Memory.read<short>(address + offsets.n_ModelIndex);
			}
		}
		public int viewmodelindex
		{
			get
			{
				return Memory.read<int>(address + offsets.m_iViewModelIndex);
			}
			set
			{
				Memory.write<int>(address + offsets.m_iViewModelIndex, value);
			}
		}
		public int econid
		{
			get
			{
				return Memory.read<Int32>(address + hazedumper.netvars.m_iItemDefinitionIndex);
			}
			set
			{
				Memory.write<Int32>(address + hazedumper.netvars.m_iItemDefinitionIndex, value);
			}
		}
	}
}
