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

        // Este método é chamado quando o usuário move o mouse dentro da janela
        protected override void OnMouseMove(MouseMoveEventArgs e)
        {
            base.OnMouseMove(e);

            // Imprime a posição do mouse
            Console.WriteLine($"Posição do mouse: ({e.X}, {e.Y})");
        }

        // Este método é chamado quando o usuário pressiona uma tecla do teclado
        protected override void OnKeyDown(KeyboardKeyEventArgs e)
        {
            base.OnKeyDown(e);

            // Imprime qual tecla foi pressionada
            Console.WriteLine($"Tecla pressionada: {e.Key}");
        }

        // Este método é chamado quando o usuário redimensiona a janela
        protected override void OnResize(ResizeEventArgs e)
        {
            base.OnResize(e);

            // Imprime o novo tamanho da janela
            Console.WriteLine($"Tamanho da janela: ({e.Width}, {e.Height})");
        }

        // Este método é chamado quando o foco da janela é alterado.
        protected override void OnFocusedChanged(FocusedChangedEventArgs e)
        {
            base.OnFocusedChanged(e);

            // Imprime se a janela está em foco ou não.
            Console.WriteLine($"Foco da janela alterado: {e.IsFocused}");
        }
    }
}
