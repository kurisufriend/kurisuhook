/*
       db   dD db    db d8888b. d888888b .d8888. db    db db   db  .d88b.   .d88b.  db   dD 
       88 ,8P' 88    88 88  `8D   `88'   88'  YP 88    88 88   88 .8P  Y8. .8P  Y8. 88 ,8P' 
       88,8P   88    88 88oobY'    88    `8bo.   88    88 88ooo88 88    88 88    88 88,8P   
       88`8b   88    88 88`8b      88      `Y8b. 88    88 88~~~88 88    88 88    88 88`8b   
       88 `88. 88b  d88 88 `88.   .88.   db   8D 88b  d88 88   88 `8b  d8' `8b  d8' 88 `88. 
       YP   YD ~Y8888P' 88   YD Y888888P `8888Y' ~Y8888P' YP   YP  `Y88P'   `Y88P'  YP   YD 
 ___     ___   ____   ______       ____    ____  _____ ______    ___       __  _  ____  ___    _____
|   \   /   \ |    \ |      T     |    \  /    T/ ___/|      T  /  _]     |  l/ ]l    j|   \  / ___/
|    \ Y     Y|  _  Y|      |     |  o  )Y  o  (   \_ |      | /  [_      |  ' /  |  T |    \(   \_ 
|  D  Y|  O  ||  |  |l_j  l_j     |   _/ |     |\__  Tl_j  l_jY    _]     |    \  |  | |  D  Y\__  T
|     ||     ||  |  |  |  |       |  |   |  _  |/  \ |  |  |  |   [_      |     Y |  | |     |/  \ |
|     |l     !|  |  |  |  |       |  |   |  |  |\    |  |  |  |     T     |  .  | j  l |     |\    |
l_____j \___/ l__j__j  l__j       l__j   l__j__j \___j  l__j  l_____j     l__j\_j|____jl_____j \___j
*/
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Numerics;
using System.Threading;
using ClickableTransparentOverlay;
using Coroutine;
using ImGuiNET;
using kurisuhook.cheat.modules;
using recode;
using recode.lib;
using recode.modules;
using recode.sdk;

namespace DriverProgram
{
    class Program
    {
        public static bool isdebug = false;

        public static bool showall = true;
        public static bool showmain = true;
        public static bool showmisc = false;
        public static bool showshoot = false;
        public static bool showvisuals = false;
        public static bool showskins = false;

        public static int count = 0;

        public static string configname = "";

        private static Random randomGen = new Random();
        private static Vector2[] circleCenters = new Vector2[200];

        static void Main()
        {
            if (Memory.setup("csgo") != 1) { debug.fatal("error during memory setup"); }
            if (utils.setup() != 1) { debug.fatal("error during utils setup"); }
            int playercache = utils.getLocalPlayer();
            G.playeraddress = playercache;
            G.player = new LocalPlayer(playercache);
            G.settings = new settings();
            G.entitylist = utils.getEntityList();

            CoroutineHandler.Start(UpdateOverlaySample2());
            CoroutineHandler.Start(SubmitRenderLogic());
            Overlay.RunInfiniteLoop();
        }

        private static IEnumerator<Wait> SubmitRenderLogic()
        {
            Thread thread = new Thread(new ThreadStart(threaded))
            {
                Priority = ThreadPriority.Highest,
                IsBackground = true,
            };
            thread.Start();
            while (true)
            {
                yield return new Wait(Overlay.OnRender);

                if (NativeMethods.IsKeyPressedBetter((int)winapi.VirtualKeys.Insert))
                    showall = !showall;
                if (NativeMethods.IsKeyPressedBetter((int)winapi.VirtualKeys.End))
                    Environment.Exit(0);

                // top left watermark
                {
                    ImGui.SetNextWindowPos(new Vector2(0f, 0f));
                    ImGui.SetNextWindowBgAlpha(0.9f);
                    ImGui.Begin(
                        "kurisuhook",
                        ImGuiWindowFlags.NoInputs |
                        ImGuiWindowFlags.NoCollapse |
                        ImGuiWindowFlags.NoTitleBar |
                        ImGuiWindowFlags.AlwaysAutoResize |
                        ImGuiWindowFlags.NoResize);

                    ImGui.Text("kurisuhook" + " | " + (isdebug ? "debug" : "release") + ((configname == "") ? "" : " | ") + configname);
                    //ImGui.Text(new Weapon(G.player.curweapon).econid.ToString());
                    ImGui.End();
                }

                if (showmain && showall)
                {
                    bool isRunning = true;
                    if (!ImGui.Begin("welcome to kurisuhook!", ref isRunning, ImGuiWindowFlags.AlwaysAutoResize))
                    {
                        Overlay.Close = !isRunning;
                        ImGui.End();
                        continue;
                    }

                    Overlay.Close = !isRunning;

                    ImGui.Checkbox("assistance", ref showshoot);
                    ImGui.Checkbox("visuals", ref showvisuals);
                    ImGui.Checkbox("poorfag zone", ref showskins);
                    ImGui.Checkbox("misc.", ref showmisc);
                    ImGui.NewLine();
                    ImGui.InputText("config name", ref configname, 20);
                    ImGui.Text(configname);
                    if (ImGui.Button("Save"))
                        G.settings.save(configname);
                    if (ImGui.Button("Load"))
                        G.settings.load(configname);

                    ImGui.End();

                    // kurisu pic
                    ImGui.Begin("");
                    if (File.Exists("image.png"))
                        ImGui.Image(Overlay.AddOrGetImagePointer("image.png"), new Vector2(120.5f, 329.5f));

                    ImGui.End();
                }

                // misc window
                if (showmisc && showall)
                {
                    ImGui.Begin("misc.", ImGuiWindowFlags.AlwaysAutoResize);
                    ImGui.Checkbox("bunnyhop", ref G.settings.bunnyhop);
                    ImGui.NewLine();
                    ImGui.Checkbox("FOV changer", ref G.settings.fovchanger);
                    ImGui.SliderInt("fov", ref G.settings.fov, 0, 180);
                    ImGui.NewLine();
                    ImGui.Checkbox("model changer", ref G.settings.modelchanger);
                    ImGui.Combo("model", ref G.settings.model, models.indexArr, models.indexArr.Length);
                    ImGui.NewLine();
                    ImGui.Checkbox("perspective changer", ref G.settings.perspectivechanger);
                    ImGui.SliderInt("perspective", ref G.settings.observermode, 0, 5);
                    ImGui.NewLine();
                    ImGui.Checkbox("spectator list", ref G.settings.speclist);
                    ImGui.End();
                }
                // shooty window
                if (showshoot && showall)
                {
                    ImGui.Begin("pew pew", ImGuiWindowFlags.AlwaysAutoResize);
                    ImGui.Checkbox("triggerbot", ref G.settings.triggerbot);
                    ImGui.Combo("triggerbot key", ref G.settings.triggerkey, winapi.vkeyArr, winapi.vkeyArr.Length);
                    ImGui.NewLine();
                    ImGui.Checkbox("recoil control", ref G.settings.rcs);
                    ImGui.Checkbox("ignore first shot", ref G.settings.rcsafter);
                    ImGui.SliderFloat("rcs amount X", ref G.settings.rcsintensityx, 0, 1);
                    ImGui.SliderFloat("rcs amount Y", ref G.settings.rcsintensityy, 0, 1);
                    ImGui.Checkbox("rcs smoothing", ref G.settings.rcssmoothing);
                    ImGui.SliderFloat("smoothing amount", ref G.settings.rcsmoothingintensity, 1.0f, 5.0f);
                    ImGui.NewLine();
                    ImGui.Checkbox("aimbot", ref G.settings.aimbot);
                    ImGui.Combo("aimbot key", ref G.settings.aimkey, winapi.vkeyArr, winapi.vkeyArr.Length);
                    ImGui.Checkbox("factor recoil", ref G.settings.aimbotrcs);
                    ImGui.SliderFloat("aimbot smoothing amount", ref G.settings.aimbotsmoothing, 1.0f, 50.0f);
                    ImGui.SliderFloat("aimbot fov", ref G.settings.aimbotfov, 0.0f, 180.0f);
                    ImGui.End();
                }
                // visuals window
                if (showvisuals && showall)
                {
                    ImGui.Begin("visuals", ImGuiWindowFlags.AlwaysAutoResize);
                    ImGui.Checkbox("glowESP", ref G.settings.glow);
                    ImGui.Checkbox("fullbloom (fake chams)", ref G.settings.fullbloom);
                    ImGui.ColorEdit4("glow color", ref G.settings.glowcolor);
                    ImGui.NewLine();
                    ImGui.Checkbox("radarESP", ref G.settings.radar);
                    ImGui.NewLine();
                    ImGui.Checkbox("chams", ref G.settings.chams);
                    ImGui.ColorEdit4("chams color", ref G.settings.chamscolor);
                    ImGui.End();
                }
                // spec list
                if (G.settings.speclist)
                {
                    ImGui.Begin("spectator list", ImGuiWindowFlags.AlwaysAutoResize);
                    foreach (Entity ent in utils.getEntityList())
                    {
                        if (ent.spectating == G.player.getaddress())
                            ImGui.Text(ent.getaddress().ToString());
                    }
                    ImGui.Text("                   ");
                    ImGui.End();
                }
                // skins window
                if (showskins && showall)
                {
                    ImGui.Begin("knife changer", ImGuiWindowFlags.AlwaysAutoResize);
                    ImGui.Checkbox("knife changer", ref G.settings.knifechanger);
                    ImGui.Combo("knife", ref G.settings.knife, weapons.knifeArr,weapons.knifeArr.Length);
                    ImGui.End();
                }
                if (G.settings.fovchanger)
                {
                    fov.run();
                }

                if (G.settings.triggerbot)
                {
                    trigger.run();
                }

                if (G.settings.glow)
                {
                    glow.run();
                }

                if (G.settings.radar)
                {
                    radar.run();
                }

                if (G.settings.modelchanger)
                {
                    nohands.run();
                }

                if (G.settings.rcs)
                {
                    rcs.run();
                }

                if (G.settings.aimbot)
                {
                    aim.run();
                }

                if (G.settings.perspectivechanger)
                {
                    thirdperson.run();
                }

                if (G.settings.chams)
                {
                    chams.run();
                }

                if (count % 200 == 0)
                {
                    G.player = new LocalPlayer(utils.getLocalPlayer());
                    G.playeraddress = utils.getLocalPlayer();
                    G.entitylist = utils.getEntityList();
                    if (G.player.modelindex != 0)
                        G.normalhands = G.player.modelindex;
                }

                ImGui.End();
                //Thread.Sleep(1);
                count++;
            }
        }
        public static void threaded()
        {
            while (true)
            {
                Thread.Sleep(1);
                if (G.settings.bunnyhop)
                    bhop.run();
                if (G.settings.knifechanger)
                    knifechanger.run();
            }
        }
        private static IEnumerator<Wait> UpdateOverlaySample2()
        {
            while (true)
            {
                yield return new Wait(1);
                for (int i = 0; i < circleCenters.Length; i++)
                {
                    circleCenters[i].X = randomGen.Next(0, 2560);
                    circleCenters[i].Y = randomGen.Next(0, 1440);
                }
            }
        }
    }
}
