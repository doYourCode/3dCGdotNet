using OpenTK.Windowing.Desktop;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using _3dCG.Examples;

namespace _3dCG
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Create the app's window
            var _window = new Chapter_13_HelloNormal(GameWindowSettings.Default,
                new NativeWindowSettings()
                {
                    Size = new Vector2i(800, 800),
                    WindowBorder = WindowBorder.Fixed,
                    WindowState = WindowState.Normal,
                });

            Shader.SetRootPath("Resources/Shader/Basics/");

            // Run it!
            _window.Run();
        }
    }
}
