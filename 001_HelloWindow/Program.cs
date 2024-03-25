using OpenTK.Windowing.Desktop;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;

using Framework.Core;

namespace Examples
{
    /// <summary>
    /// Ponto de entrada para todos os programas de exemplo.
    /// </summary>
    internal class Program
    {
        static void Main(string[] args)
        {
            // Configurações de janela
            NativeWindowSettings settings = new NativeWindowSettings()
            {
                Title = "Hello Window",              // Título da janela
                ClientSize = new Vector2i(800, 800), // Tamanho da janela em pixels (altura x largura)
                WindowBorder = WindowBorder.Fixed,   // Não poderá ser redimensionada
                WindowState = WindowState.Normal,    // Modo janela ou fullscreen
                APIVersion = new Version(3, 3),      // Opengl 3.3 + GLSL 330
                Vsync = VSyncMode.On                // Fixa a taxa de quadros equivalente à do monitor
            };

            // Instanciação da janela
            var appWindow = new HelloWindow(GameWindowSettings.Default, settings);

            // Configura a pasta raiz para carregar arquivos de Shader ( O que é shader? Ver projeto 004_HelloShader )
            Shader.SetRootPath("Resources/Shader/");

            // Executa a aplicação
            appWindow.Run();
        }
    }
}
