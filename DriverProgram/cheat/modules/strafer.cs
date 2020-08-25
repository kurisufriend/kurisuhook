using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using recode.lib;
using recode.sdk;

namespace recode.modules
{
    public static class strafer // pasted garbage
    {
        public static bool strafe = false;
        public static void run()
        {
            while (true)
            {
                Thread.Sleep(1);
                if (G.settings.strafer)
                {
                    Vec3 oldAngle = G.player.viewangles;

                    while (winapi.GetAsyncKeyState((int)winapi.VirtualKeys.Space) != 0)
                    {
                        Thread.Sleep(1);

                        strafe = true;
                        Vec3 cuurentAngle = G.player.viewangles;
                        if (cuurentAngle.y > oldAngle.y)
                        {
                            ClientCMD.Exec("-moveright");
                            Thread.Sleep(1);
                            ClientCMD.Exec("+moveleft");
                        }
                        else if (cuurentAngle.y < oldAngle.y)
                        {
                            ClientCMD.Exec("-moveleft");
                            Thread.Sleep(1);
                            ClientCMD.Exec("+moveright");
                        }
                        oldAngle = G.player.viewangles;
                    }
                    if (strafe)
                    {
                        ClientCMD.Exec("-moveright");
                        Thread.Sleep(1);
                        ClientCMD.Exec("-moveleft");
                        strafe = false;
                    }
                }
            }
        }
    }
}
