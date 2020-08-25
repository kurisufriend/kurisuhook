using System;
using System.Collections.Generic;
using System.Numerics;
using System.Threading;
using Coroutine;
using Veldrid;
using Veldrid.ImageSharp;
using Veldrid.Sdl2;
using Veldrid.StartupUtilities;
using ImGuiNET;

using static ImGuiNET.ImGuiNative;

namespace ClickableTransparentOverlay
{
    // TODO: Implement overlay info, warn, error logger.
    /// <summary>
    /// A class to create clickable transparent overlay.
    /// </summary>
    public static class Overlay
    {
        private readonly static Sdl2Window window;
        private readonly static GraphicsDevice graphicsDevice;
        private readonly static CommandList commandList;
        private readonly static ImGuiController imController;
        private readonly static Vector4 clearColor;
        private readonly static Dictionary<string, Texture> loadedImages;
        private static bool terminal = true;

        /// <summary>
        /// Initializes a new instance of the <see cref="Overlay"/> class.
        /// </summary>
        static Overlay()
        {
            clearColor = new Vector4(0.00f, 0.00f, 0.00f, 0.00f);
            loadedImages = new Dictionary<string, Texture>();
            window = new Sdl2Window(
                "Overlay",
                0,
                0,
                1920,
                1080,
                SDL_WindowFlags.Borderless |
                    SDL_WindowFlags.AlwaysOnTop |
                    SDL_WindowFlags.SkipTaskbar,
                false);
            graphicsDevice = VeldridStartup.CreateDefaultD3D11GraphicsDevice(
                new GraphicsDeviceOptions(false, null, true),
                window);
            commandList = graphicsDevice.ResourceFactory.CreateCommandList();
            imController = new ImGuiController(
                graphicsDevice,
                graphicsDevice.MainSwapchain.Framebuffer.OutputDescription,
                window.Width,
                window.Height);
            window.Resized += () =>
            {
                graphicsDevice.MainSwapchain.Resize((uint)window.Width, (uint)window.Height);
                imController.WindowResized(window.Width, window.Height);
            };

            NativeMethods.InitTransparency(window.Handle);
            NativeMethods.SetOverlayClickable(window.Handle, false);
        }

        /// <summary>
        /// Infinitely renders the over (and execute co-routines) till it's closed.
        /// </summary>
        public static void RunInfiniteLoop()
        {
            DateTime previous = DateTime.Now;
            DateTime current;
            TimeSpan interval;
            float sec;
            while (window.Exists && !Close)
            {
                InputSnapshot snapshot = window.PumpEvents();
                if (!window.Exists) { break; }
                current = DateTime.Now;
                interval = current - previous;
                sec = (float)interval.TotalSeconds;
                previous = current;
                imController.Update(sec > 0 ? sec : 0.001f, snapshot, window.Handle);

                InitStyle();

                CoroutineHandler.Tick(interval.TotalSeconds);
                if (Visible)
                {
                    CoroutineHandler.RaiseEvent(OnRender);
                }

                commandList.Begin();
                commandList.SetFramebuffer(graphicsDevice.MainSwapchain.Framebuffer);
                commandList.ClearColorTarget(0, new RgbaFloat(clearColor.X, clearColor.Y, clearColor.Z, clearColor.W));
                imController.Render(graphicsDevice, commandList);
                commandList.End();
                graphicsDevice.SubmitCommands(commandList);
                graphicsDevice.SwapBuffers(graphicsDevice.MainSwapchain);
            }

            Dispose();
        }

        /// <summary>
        /// To submit ImGui code for generating the UI.
        /// </summary>
        public static Event OnRender = new Event();

        /// <summary>
        /// Safely Closes the Overlay.
        /// Doesn't matter if you set it to true multiple times.
        /// </summary>
        public static bool Close { get; set; } = false;

        /// <summary>
        /// Makes the overlay visible or invisible. Invisible Overlay
        /// will not call OnRender coroutines, however time based
        /// coroutines are still called.
        /// </summary>
        public static bool Visible { get; set; } = true;

        /// <summary>
        /// Gets or sets a value indicating whether to hide the terminal window.
        /// </summary>
        public static bool TerminalWindow
        {
            get => terminal;
            set
            {
                if (value != terminal)
                {
                    NativeMethods.SetConsoleWindow(value);
                }

                terminal = value;
            }
        }

        /// <summary>
        /// Gets or sets the position of the overlay window.
        /// </summary>
        public static Point Position
        {
            get
            {
                return new Point(window.X, window.Y);
            }
            set
            {
                Sdl2Native.SDL_SetWindowPosition(window.SdlWindowHandle, value.X, value.Y);
            }
        }

        /// <summary>
        /// Gets or sets the size of the overlay window.
        /// </summary>
        public static Point Size
        {
            get
            {
                return new Point(window.Width, window.Height);
            }
            set
            {
                Sdl2Native.SDL_SetWindowSize(window.SdlWindowHandle, value.X, value.Y);
            }
        }

        /// <summary>
        /// Adds the image to the Graphic Device as a texture.
        /// Then returns the pointer of the added texture. It also
        /// cache the image internally rather than creating a new texture on every call,
        /// so this function can be called multiple times per frame.
        /// </summary>
        /// <param name="filePath">
        /// Path to the image on disk. If the image is loaded in the memory
        /// save it on the disk before sending to this function. Reason for this
        /// is to cache the Image Texture using filePath as the key.
        /// </param>
        /// <returns>
        /// A pointer to the Texture in the Graphic Device.
        /// </returns>
        public static IntPtr AddOrGetImagePointer(string filePath)
        {
            if (!loadedImages.TryGetValue(filePath, out Texture texture))
            {
                ImageSharpTexture imgSharpTexture = new ImageSharpTexture(filePath);
                texture = imgSharpTexture.CreateDeviceTexture(graphicsDevice, graphicsDevice.ResourceFactory);
                loadedImages.Add(filePath, texture);
            }

            return imController.GetOrCreateImGuiBinding(graphicsDevice.ResourceFactory, texture);
        }

        /// <summary>
        /// Free all resources acquired by the overlay.
        /// </summary>
        private static void Dispose()
        {
            window.Close();
            while (window.Exists)
            {
                Thread.Sleep(1);
            }

            graphicsDevice.WaitForIdle();
            imController.Dispose();
            commandList.Dispose();
            graphicsDevice.Dispose();
            loadedImages.Clear();
            NativeMethods.SetConsoleWindow(true);
        }
        private static void InitStyle()
        {
            var style = ImGui.GetStyle();
            style.WindowRounding = 0;
            style.ChildRounding = 0;
            style.ScrollbarRounding = 0;
            style.WindowTitleAlign = new Vector2(.5f, .5f);
            style.Colors[(int)ImGuiCol.Text] = new Vector4(1.00f, 1.00f, 1.00f, 1.00f);
            style.Colors[(int)ImGuiCol.Text] = new Vector4(1.00f, 1.00f, 1.00f, 1.00f);
            style.Colors[(int)ImGuiCol.WindowBg] = new Vector4(0.06f, 0.06f, 0.06f, 1.00f);
            style.Colors[(int)ImGuiCol.FrameBg] = new Vector4(0.00f, 0.00f, 0.00f, 1.00f);
            style.Colors[(int)ImGuiCol.FrameBgHovered] = new Vector4(0.19f, 0.19f, 0.19f, 1.00f);
            style.Colors[(int)ImGuiCol.FrameBgActive] = new Vector4(0.47f, 0.47f, 0.47f, 1.00f);
            style.Colors[(int)ImGuiCol.TitleBgActive] = new Vector4(0.52f, 0.09f, 0.09f, 1.00f);
            style.Colors[(int)ImGuiCol.TitleBgCollapsed] = new Vector4(0.00f, 0.00f, 0.00f, 0.78f);
            style.Colors[(int)ImGuiCol.ScrollbarBg] = new Vector4(0.02f, 0.02f, 0.02f, 1.00f);
            style.Colors[(int)ImGuiCol.ScrollbarGrab] = new Vector4(0.49f, 0.49f, 0.49f, 1.00f);
            style.Colors[(int)ImGuiCol.Separator] = new Vector4(0.49f, 0.49f, 0.49f, 1.00f);
            style.Colors[(int)ImGuiCol.CheckMark] = new Vector4(1.00f, 1.00f, 1.00f, 1.00f);
            style.Colors[(int)ImGuiCol.SliderGrab] = new Vector4(0.50f, 0.02f, 0.02f, 1.00f);
            style.Colors[(int)ImGuiCol.Button] = new Vector4(0.50f, 0.02f, 0.02f, 1.00f);
            style.Colors[(int)ImGuiCol.ButtonHovered] = new Vector4(0.66f, 0.10f, 0.10f, 1.00f);
            style.Colors[(int)ImGuiCol.ButtonActive] = new Vector4(0.52f, 0.09f, 0.09f, 1.00f);
            style.Colors[(int)ImGuiCol.ResizeGrip] = new Vector4(0.50f, 0.02f, 0.02f, 1.00f);
            style.Colors[(int)ImGuiCol.ResizeGripHovered] = new Vector4(0.52f, 0.09f, 0.09f, 1.00f);
            style.Colors[(int)ImGuiCol.Tab] = new Vector4(0.50f, 0.02f, 0.02f, 1.00f);
            style.Colors[(int)ImGuiCol.TabHovered] = new Vector4(0.52f, 0.09f, 0.09f, 1.00f);
            style.Colors[(int)ImGuiCol.TabActive] = new Vector4(0.22f, 0.22f, 0.22f, 1.00f);
        }
    }
}
