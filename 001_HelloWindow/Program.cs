using OpenTK.Windowing.Desktop;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;

using Framework.Core;

namespace Examples
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Instancia e configura a janela da aplicação
            var _window = new HelloWindow(
                GameWindowSettings.Default,
                new NativeWindowSettings()
                {
                    Title = "Hello Window",
                    ClientSize = new Vector2i(800, 800),    // Dimensões da janela
                    WindowBorder = WindowBorder.Fixed,      // Não pode ser redimensionada
                    WindowState = WindowState.Normal,       // Modo janela (altere aqui p/ tela cheia)
                    APIVersion = new Version(3, 3),         // Opengl 3.3 + GLSL 330
                    Vsync = VSyncMode.On                    // Limita o FPS à freq. do monitor
                });

            // Pasta raiz para arquivos de Shader  O que é shader? Ver 004_HelloShader)
            Shader.SetRootPath("Resources/Shader/");     

            // Executa a aplicação
            _window.Run();
        }
    }
}
