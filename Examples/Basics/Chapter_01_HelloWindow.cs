using OpenTK.Windowing.Common;
using System.ComponentModel;
using System;
using OpenTK.Windowing.Desktop;

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

        // Método chamado quando a janela é carregada.
        protected override void OnLoad()
        {
            base.OnLoad();
            Console.WriteLine("Janela carregada!");
        }

        // Método chamado quando o tamanho da janela é alterado.
        protected override void OnResize(ResizeEventArgs e)
        {
            base.OnResize(e);
            Console.WriteLine($"Tamanho da janela alterado: Largura={e.Width}, Altura={e.Height}");
        }

        // Método chamado quando o cursor do mouse entra na área da janela.
        protected override void OnMouseEnter()
        {
            base.OnMouseEnter();
            Console.WriteLine("Mouse entrou na janela.");
        }

        // Método chamado quando o cursor do mouse sai da área da janela.
        protected override void OnMouseLeave()
        {
            base.OnMouseLeave();
            Console.WriteLine("Mouse saiu da janela.");
        }

        // Método chamado quando um botão do mouse é pressionado.
        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            base.OnMouseDown(e);
            Console.WriteLine($"Botão do mouse pressionado: {e.Button}");
        }

        // Método chamado quando um botão do mouse é solto.
        protected override void OnMouseUp(MouseButtonEventArgs e)
        {
            base.OnMouseUp(e);
            Console.WriteLine($"Botão do mouse solto: {e.Button}");
        }

        // Método chamado quando uma tecla do teclado é pressionada.
        protected override void OnKeyDown(KeyboardKeyEventArgs e)
        {
            base.OnKeyDown(e);
            Console.WriteLine($"Tecla pressionada: {e.Key}");
        }

        // Método chamado quando uma tecla do teclado é solta.
        protected override void OnKeyUp(KeyboardKeyEventArgs e)
        {
            base.OnKeyUp(e);
            Console.WriteLine($"Tecla solta: {e.Key}");
        }

        // Método chamado quando a janela está prestes a fechar.
        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            Console.WriteLine("Fechando janela...");
        }
    }
}
