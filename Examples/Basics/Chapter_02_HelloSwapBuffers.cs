using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;

namespace _3dCG.Examples.Basics
{
    /// <summary>
    /// Demonstra como limpar e dispor um Frame Buffer, utilizando a funcionalidade disponível na biblioteca OpenTK.
    /// </summary>
    internal class Chapter_02_HelloSwapBuffers : GameWindow
    {
        Color4 bgColor = new Color4(0.0f, 0.0f, 0.0f, 1.0f);

        public Chapter_02_HelloSwapBuffers(
            GameWindowSettings gameWindowSettings,
            NativeWindowSettings nativeWindowSettings) :
            base(gameWindowSettings, nativeWindowSettings)
        {
            Title = "Hello Swap Buffers!";
        }

        // O método 'OnLoad' é chamado uma vez, logo após a criação da janela. Sua função é permitir que o usuário configure a janela, os objetos e carregue os dados necessários antes de iniciar o loop de renderização.
        protected override void OnLoad()
        {
            base.OnLoad();

            // Configura a cor de fundo do Frame Buffer. (está sendo passado por valor)
            GL.ClearColor(bgColor);
        }

        protected override void OnUpdateFrame(FrameEventArgs args)
        {
            base.OnUpdateFrame(args);

            int taxaDeAtualizacao = 10;

            bgColor.R += 0.0001f * taxaDeAtualizacao;
            bgColor.G += 0.00007f * taxaDeAtualizacao;
            bgColor.B += 0.00013f * taxaDeAtualizacao;
            if (bgColor.R > 1.0f) { bgColor.R = 0.0f; }
            if (bgColor.G > 1.0f) { bgColor.G = 0.0f; }
            if (bgColor.B > 1.0f) { bgColor.B = 0.0f; }

            GL.ClearColor(bgColor); // Lembrar que está passando os dados por valor, não por referência
        }


        /// O método 'OnRenderFrame' é chamado a cada frame, logo após atualizar a lógica de negócio. Sua função é permitir que o usuário desenhe na tela, utilizando os dados disponíveis.
        protected override void OnRenderFrame(FrameEventArgs args)
        {
            base.OnRenderFrame(args);

            // Limpa o Frame Buffer, utilizando a cor configurada anteriormente.
            GL.Clear(ClearBufferMask.ColorBufferBit);

            // AQUI VOCÊ DESENHA OS OBJETOS

            // Disponibiliza o Frame Buffer na tela, alternando entre dois buffers que são pintados em sequência (Double buffering).
            SwapBuffers();
        }
    }
}
