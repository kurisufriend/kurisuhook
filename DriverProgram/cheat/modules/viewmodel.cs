using System;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading;
using recode.lib;
using recode.sdk;

namespace recode.modules
{
	public static class viewmodel
	{
        public static ConVar hands = new ConVar("viewmodel_fov");
        public static ConVar vmx = new ConVar("viewmodel_offset_x");
        public static ConVar vmy = new ConVar("viewmodel_offset_y");
        public static ConVar vmz = new ConVar("viewmodel_offset_z");
        public static bool alt;
        public static void run()
		{
            hands.ClearCallbacks();
            vmx.ClearCallbacks();
            vmy.ClearCallbacks();
            vmz.ClearCallbacks();
			while (true)
			{
				Thread.Sleep(1);
				if (G.settings.viewmodelchanger)
				{
                    if (G.settings.viewmodelfov != hands.GetFloat())
                    {
                        ClientCMD.Exec("viewmodel_fov " + G.settings.viewmodelfov.ToString());
                    }
                    if (G.settings.viewmodelx != vmx.GetFloat())
                    {
                        ClientCMD.Exec("viewmodel_offset_x " + G.settings.viewmodelx.ToString());
                    }
                    if (G.settings.viewmodely != vmy.GetFloat())
                    {
                        ClientCMD.Exec("viewmodel_offset_y " + G.settings.viewmodely.ToString());
                    }
                    if (G.settings.viewmodelz != vmz.GetFloat())
                    {
                        ClientCMD.Exec("viewmodel_offset_z " + G.settings.viewmodelz.ToString());
                    }
                    if (G.settings.flipviewmodel)
                    {
                        new ConVar("cl_righthand").SetValue(alt ? 1 : 0);
                        alt = !alt;
                    }
                }
			}
		}
	}
}
