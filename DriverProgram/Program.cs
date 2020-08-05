﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Numerics;
using System.Threading;
using ClickableTransparentOverlay;
using Coroutine;
using ImGuiNET;
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

            CoroutineHandler.Start(UpdateOverlaySample2());
            CoroutineHandler.Start(SubmitRenderLogic());
            Overlay.RunInfiniteLoop();
        }

        private static IEnumerator<Wait> SubmitRenderLogic()
        {
            Thread bhopthread = new Thread(new ThreadStart(bhop.run))
            {
                Priority = ThreadPriority.Highest,
                IsBackground = true,
            };
            bhopthread.Start();
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
                    ImGui.ColorEdit4("enemy color", ref G.settings.glowenemycolor);
                    ImGui.NewLine();
                    ImGui.Checkbox("radarESP", ref G.settings.radar);
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

                if (count % 200 == 0)
                {
                    G.player = new LocalPlayer(utils.getLocalPlayer());
                    G.playeraddress = utils.getLocalPlayer();
                    if (G.player.modelindex != 0)
                        G.normalhands = G.player.modelindex;
                }


                ImGui.End();
                //Thread.Sleep(1);
                count++;
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
