// <copyright file="Program.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Examples
{
    using OpenTK.Mathematics;
    using OpenTK.Windowing.Common;
    using OpenTK.Windowing.Desktop;

    /// <summary>
    /// Classe que fornece o ponto de entrada do programa, a exemplo do modelo
    /// adotado para todos os outros programas de exemplos e exercícios.
    /// </summary>
    internal class Program
    {
        private static void Main(string[] args)
        {
            // Configurações de janela
            NativeWindowSettings settings = new NativeWindowSettings()
            {
                Title = "Hello Window",              // Título da janela
                ClientSize = new Vector2i(800, 800), // Tamanho da janela em pixels
                WindowBorder = WindowBorder.Fixed,   // Não poderá ser redimensionada
                WindowState = WindowState.Normal,    // Modo janela ou fullscreen
                APIVersion = new Version(3, 3),      // Opengl 3.3 + GLSL 330
                Vsync = VSyncMode.On,                // FPS fixo na tx. do monitor
            };

            // Instanciação da janela
            var appWindow = new HelloWindow(GameWindowSettings.Default, settings);

            // Executa a aplicação
            appWindow.Run();
        }
    }
}
