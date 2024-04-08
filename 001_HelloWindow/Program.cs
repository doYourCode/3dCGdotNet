// <copyright file="Program.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Examples
{
    using Framework.Utils;
    using OpenTK.Mathematics;
    using OpenTK.Windowing.Common;
    using OpenTK.Windowing.Desktop;

    /// <summary>
    /// Entry point.
    /// </summary>
    internal class Program
    {
        /* O ponto de entrada do programa, a exemplo do modelo adotado para todos
         * os outros programas de exemplos e exercícios. Obs. Apenas neste primeiro
         * programa está comentado este trecho de código-fonte. */
        private static void Main(string[] args)
        {
            // Configurações de janela (relacionas às funções nativas do S.O.)
            NativeWindowSettings settings = new NativeWindowSettings()
            {
                Title = "Hello Window",                 // Título da janela
                ClientSize = new Vector2i(800, 800),    // Tamanho da janela em pixels
                WindowBorder = WindowBorder.Fixed,      // Não poderá ser redimensionada
                WindowState = WindowState.Normal,       // Modo janela ou fullscreen
                APIVersion = new Version(3, 3),         // Opengl 3.3 + GLSL 330
                Vsync = VSyncMode.Off,                  // FPS arbitrário
            };

            // Mais configurações de janela (relacionadas às funções da engine)
            GameWindowSettings gmSettings = new GameWindowSettings()
            {
                UpdateFrequency = CONSTANTS.MAX_FPS,    // FPS configurado na Framework
            };

            // Instanciação da janela
            var appWindow = new HelloWindow(gmSettings, settings);

            // Executa a aplicação
            appWindow.Run();
        }
    }
}