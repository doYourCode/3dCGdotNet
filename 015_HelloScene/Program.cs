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
                Title = "Hello Scene",
                ClientSize = new Vector2i(1600, 800),
                WindowBorder = WindowBorder.Fixed,
                WindowState = WindowState.Normal,
                APIVersion = new Version(3, 3),
                Vsync = VSyncMode.On,
            };

            GameWindowSettings gmSettings = new GameWindowSettings()
            {
                UpdateFrequency = CONSTANTS.MAX_FPS,
            };

            var appWindow = new HelloScene(gmSettings, settings);

            Shader.RootPath = "Resources/Shader/";
            Texture.RootPath = "Resources/Texture/";
            BasicScene.RootPath = "Resources/Scene/";

            appWindow.Run();
        }
    }
}