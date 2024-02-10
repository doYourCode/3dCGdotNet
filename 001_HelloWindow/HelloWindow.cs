using OpenTK.Windowing.Desktop;

namespace Examples
{
    /// <summary>
    /// Cria uma janela vazia utilizando a funcionalidade disponível na biblioteca OpenTK.
    /// </summary>
    internal class HelloWindow : GameWindow
    {
        public HelloWindow(
            GameWindowSettings gameWindowSettings,
            NativeWindowSettings nativeWindowSettings) :
            base(gameWindowSettings, nativeWindowSettings) { }
    }
}