using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;

namespace _3dCG.Examples.Basics
{
    /// <summary>
    /// Demonstra como limpar e dispor um Frame Buffer, utilizando a funcionalidade disponível na biblioteca OpenTK.
    /// </summary>
    internal class Chapter_02_HelloSwapBuffers : GameWindow
    {

        public Chapter_02_HelloSwapBuffers(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings) : base(gameWindowSettings, nativeWindowSettings)
        {
            Title = "Hello Swap Buffers!";
        }

        // O método 'OnLoad' é chamado uma vez, logo após a criação da janela. Sua função é permitir que o usuário configure a janela, os objetos e carregue os dados necessários antes de iniciar o loop de renderização.
        protected override void OnLoad()
        {
            base.OnLoad();

            // Configura a cor de fundo do Frame Buffer.
            GL.ClearColor(1.0f, 0.0f, 0.0f, 1.0f);
        }

        /// O método 'OnRenderFrame' é chamado a cada frame, logo após atualizar a lógica de negócio. Sua função é permitir que o usuário desenhe na tela, utilizando os dados disponíveis.
        protected override void OnRenderFrame(FrameEventArgs args)
        {
            base.OnRenderFrame(args);

            // Limpa o Frame Buffer, utilizando a cor configurada anteriormente.
            GL.Clear(ClearBufferMask.ColorBufferBit);
            // Disponibiliza o Frame Buffer na tela, alternando entre dois buffers que são pintados em sequência (Double buffering).
            SwapBuffers();
        }
    }
}
