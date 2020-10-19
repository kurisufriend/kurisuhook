using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;
using System.Threading;
using recode.lib;
using recode.sdk;

namespace recode.modules
{
    public static class strafer
    {
        public static bool strafing = false;
        public static void run()
        {
            while (true)
            {
                Thread.Sleep(1);
                if (G.settings.strafer)
                {
                    Vec3 oldAngle = G.player.viewangles;
                    bool alt = false;
                    while (winapi.GetAsyncKeyState((int)winapi.vkeyArrVals.GetValue(G.settings.straferkey)) != 0)
                    {
                        Thread.Sleep(1);
                        strafing = true;
                        //fdouitble idealRotation = Math.Min(Rad2Deg(Math.Asin(30.0 / (double)G.player.velocity)) * 0.5, 45.0);
                        Vec3 currentAngle = G.player.viewangles;
                        if (currentAngle.y > oldAngle.y) // strafing left
                        {
                            ClientCMD.Exec("-moveright");
                            Thread.Sleep(1);
                            ClientCMD.Exec("+moveleft");
                        }
                        else if (currentAngle.y < oldAngle.y) // strafing right
                        {
                            ClientCMD.Exec("-moveleft");
                            Thread.Sleep(1);
                            ClientCMD.Exec("+moveright");
                        }
                        oldAngle = G.player.viewangles;
                    }
                    if (strafing) // reset 
                    {
                        Thread.Sleep(1);
                        ClientCMD.Exec("-moveright");
                        Thread.Sleep(1);
                        ClientCMD.Exec("-moveleft");
                        strafing = false;
                    }
                }
            }
        }
        public static double Rad2Deg(double radians)
        {
            double degrees = (180 / Math.PI) * radians;
            return (degrees);
        }
    }
}
