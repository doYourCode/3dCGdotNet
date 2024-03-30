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
            NativeWindowSettings settings = new NativeWindowSettings()
            {
                Title = "Hello Texture",
                ClientSize = new Vector2i(800, 800),
                WindowBorder = WindowBorder.Fixed,
                WindowState = WindowState.Normal,
                APIVersion = new Version(3, 3),
                Vsync = VSyncMode.On
            };

            var window = new HelloTexture(GameWindowSettings.Default, settings);

            Shader.RootPath = "Resources/Shader/";
            // Configura uma pasta raiz para carregar os arquivos de textura (a exemplo do que é feito com os Shaders)
            Texture.RootPath = "Resources/Texture/";

            window.Run();
        }
    }
}
