using OpenTK.Windowing.Desktop;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using Framework.Core;
using ExamplesCommon;

namespace Examples
{
    internal class Program
    {
        static void Main(string[] args)
        {
            NativeWindowSettings settings = new NativeWindowSettings()
            {
                Title = "Hello Mesh",
                ClientSize = new Vector2i(800, 800),
                WindowBorder = WindowBorder.Fixed,
                WindowState = WindowState.Normal,
                APIVersion = new Version(3, 3),
                Vsync = VSyncMode.On
            };

            var window = new HelloMesh(GameWindowSettings.Default, settings);

            Shader.RootPath = "Resources/Shader/";
            Texture.RootPath = "Resources/Texture/";
            // Configura uma pasta raiz para carregar arquivos de modelos 3d (mesh)
            BasicMesh.RootPath = "Resources/Mesh/";

            window.Run();
        }
    }
}
