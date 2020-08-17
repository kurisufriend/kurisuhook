using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using recode.lib;
using recode.sdk;

namespace recode.modules
{
	public static class viewmodel
	{
        public static int curvmfov;
        public static int curvmx;
        public static int curvmy;
        public static int curvmz;
        public static void run()
		{
			while (true)
			{
				Thread.Sleep(1);
				if (G.settings.viewmodelchanger)
				{
                    if (G.settings.viewmodelfov != curvmfov)
                    {
                        ConVar hands = new ConVar("viewmodel_fov");
                        hands.ClearCallbacks();
                        ClientCMD.Exec("viewmodel_fov " + G.settings.viewmodelfov.ToString());
                        curvmfov = G.settings.viewmodelfov;
                    }
                    if (G.settings.viewmodelx != curvmx)
                    {
                        ConVar vmx = new ConVar("viewmodel_offset_x");
                        vmx.ClearCallbacks();
                        ClientCMD.Exec("viewmodel_offset_x " + G.settings.viewmodelx.ToString());
                        curvmx = G.settings.viewmodelx;
                    }
                    if (G.settings.viewmodely != curvmy)
                    {
                        ConVar vmy = new ConVar("viewmodel_offset_y");
                        vmy.ClearCallbacks();
                        ClientCMD.Exec("viewmodel_offset_y " + G.settings.viewmodely.ToString());
                        curvmfov = G.settings.viewmodely;
                    }
                    if (G.settings.viewmodelz != curvmz)
                    {
                        ConVar vmz = new ConVar("viewmodel_offset_z");
                        vmz.ClearCallbacks();
                        ClientCMD.Exec("viewmodel_offset_z " + G.settings.viewmodelz.ToString());
                        curvmx = G.settings.viewmodelz;
                    }
                }
			}
		}
	}
}
