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
		public int modelindex
		{
			get
			{
				return Memory.read<int>(address + offsets.n_ModelIndex);
			}
			set
			{
				Memory.write<int>(address + offsets.n_ModelIndex, value);
			}
		}
		public int entityquality
		{
			get
			{
				return Memory.read<int>(address + hazedumper.netvars.m_iEntityQuality);
			}
			set
			{
				Memory.write<int>(address + hazedumper.netvars.m_iEntityQuality, value);
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
