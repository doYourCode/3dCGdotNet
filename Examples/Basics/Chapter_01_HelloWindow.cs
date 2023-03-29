using OpenTK.Windowing.Desktop;

namespace _3dCG.Examples
{
    internal class Chapter_01_HelloWindow : GameWindow
    {

        public Chapter_01_HelloWindow(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings) : base(gameWindowSettings, nativeWindowSettings)
        {
            Title = "Hello Window!";
        }
    }
}
