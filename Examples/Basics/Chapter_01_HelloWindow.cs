///   <date> 27/03/2023 </date>
///   <author> Caio Henriques Sica Lamas </author> 
///   <e-mail> caio.lamas@ifnmg.edu.br </e-mail>

using OpenTK.Windowing.Desktop;

namespace _3dCG.Examples.Basics
{
    internal class Chapter_01_HelloWindow : GameWindow
    {
        /// <summary>
        /// Demonstra como instanciar e configurar uma janela, utilizando a funcionalidade disponível na biblioteca OpenTK.
        /// O construtor recebe classes de configuração, entre elas 'NativeWindowSettings', que permite configurar a resolução / tamanho da tela.
        /// </summary>
        public Chapter_01_HelloWindow(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings) : base(gameWindowSettings, nativeWindowSettings)
        {
            // O título da janela precisa ser configurado pra cada exemplo individualmente.
            Title = "Hello Window!";
        }
    }
}
