using System;
using System.Collections.Generic;
using System.Text;
using recode.lib;

namespace recode.sdk
{
	public class LocalPlayer : Entity
	{
		public LocalPlayer(Int32 _address) : base(_address) {}
		public void jump()
		{
			Memory.write<Int32>(G.client + hazedumper.signatures.dwForceJump, 6);
		}
		public void shoot()
		{
			Memory.write<Int32>(G.client + hazedumper.signatures.dwForceAttack, 6);
		}
	}
}
