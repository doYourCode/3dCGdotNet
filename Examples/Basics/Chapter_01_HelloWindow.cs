using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using System;

namespace _3dCG.Examples.Basics
{
    /// <summary>
    /// Demonstra como instanciar e configurar uma janela, utilizando a funcionalidade disponível na biblioteca OpenTK.
    /// </summary>
    internal class Chapter_01_HelloWindow : GameWindow
    {
        /// <summary>
        /// O construtor recebe classes de configuração, entre elas 'NativeWindowSettings', que permite configurar a resolução / tamanho da tela, etc.
        /// </summary>
        public Chapter_01_HelloWindow(
            GameWindowSettings gameWindowSettings,
            NativeWindowSettings nativeWindowSettings) :
            base(gameWindowSettings, nativeWindowSettings)
        {
            // O título da janela precisa ser configurado pra cada exemplo individualmente.
            Title = "Hello Window!";
        }

    }
}
