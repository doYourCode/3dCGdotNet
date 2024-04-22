// <copyright file="Program.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Examples
{
    using ExamplesCommon;
    using Framework.Core;
    using Framework.Utils;
    using OpenTK.Mathematics;
    using OpenTK.Windowing.Common;
    using OpenTK.Windowing.Desktop;

    /// <summary>
    /// Entry point.
    /// </summary>
    internal class Program
    {
        private static void Main(string[] args)
        {
            NativeWindowSettings settings = new NativeWindowSettings()
            {
                Title = "Sandbox Editor",
                ClientSize = GetScreenSize(),
                WindowBorder = WindowBorder.Resizable,
                WindowState = WindowState.Maximized,
                APIVersion = new Version(4, 2),
                Vsync = VSyncMode.Off,
            };

            GameWindowSettings gmSettings = new GameWindowSettings()
            {
                UpdateFrequency = CONSTANTS.MAX_FPS,
            };

            var appWindow = new TestApp2(gmSettings, settings);

            Shader.RootPath = "Resources/Shader/";
            Texture.RootPath = "Resources/Texture/";
            BasicMesh.RootPath = "Resources/Mesh/";

            appWindow.Run();
        }

        /// <summary>
        /// Queries the system for the current monitor's screen size.
        /// </summary>
        /// <returns> The size of the screen in pixels. </returns>
        private static Vector2i GetScreenSize()
        {
            var tmpWindow = new GameWindow(
                GameWindowSettings.Default,
                NativeWindowSettings.Default);

            MonitorInfo mi = Monitors.GetMonitorFromWindow(tmpWindow);

            tmpWindow.Dispose();
            tmpWindow.Close();

            return new Vector2i(mi.HorizontalResolution, mi.VerticalResolution);
        }
    }
}