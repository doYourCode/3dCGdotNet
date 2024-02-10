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
            var _window = new HelloTransformation(
                GameWindowSettings.Default,
                new NativeWindowSettings()
                {
                    Title = "Hello Transformation",
                    ClientSize = new Vector2i(800, 800),
                    WindowBorder = WindowBorder.Fixed,
                    WindowState = WindowState.Normal,
                    APIVersion = new Version(3, 3),
                    Vsync = VSyncMode.On
                });

            Shader.SetRootPath("Resources/Shader/");

            _window.Run();
        }
    }
}
