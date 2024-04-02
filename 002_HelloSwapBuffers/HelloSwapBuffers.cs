// <copyright file="HelloSwapBuffers.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Examples
{
    using OpenTK.Graphics.OpenGL;
    using OpenTK.Mathematics;
    using OpenTK.Windowing.Common;
    using OpenTK.Windowing.Desktop;

    /// <summary>
    /// Limpar e dispor um Frame Buffer, utilizando a funcionalidade disponível na biblioteca OpenTK.
    /// </summary>
    internal class HelloSwapBuffers : GameWindow
    {
        private Color4 bgColor = new Color4(0.0f, 0.0f, 0.0f, 1.0f);

        private int taxaDeAtualizacao = 3;

        /// <summary>
        /// Initializes a new instance of the <see cref="HelloSwapBuffers"/> class.
        /// </summary>
        /// <param name="gameWindowSettings">Configurações internas de janela.</param>
        /// <param name="nativeWindowSettings">Configurações nativas de janela.</param>
        public HelloSwapBuffers(
            GameWindowSettings gameWindowSettings,
            NativeWindowSettings nativeWindowSettings)
            : base(gameWindowSettings, nativeWindowSettings)
        {
        }

        /// <summary>
        /// O método 'OnLoad' é chamado uma vez, logo após a criação da janela.
        /// Sua função é permitir que o usuário configure a janela, os objetos e
        /// carregue os dados necessários antes de iniciar o loop de renderização.
        /// </summary>
        protected override void OnLoad()
        {
            base.OnLoad();

            // Configura a cor de fundo do Frame Buffer.
            GL.ClearColor(this.bgColor);
        }

        /// <summary>
        /// O método 'OnUpdateFrame' é comumente chamado uma vez a cada frame (
        /// podendo ser configurado de forma diferente). Neste método chamamos as
        /// principais funções de lógica de negócio do programa.
        /// </summary>
        /// <param name="args">Argumentos com parâmetros individuais por iteração.</param>
        protected override void OnUpdateFrame(FrameEventArgs args)
        {
            base.OnUpdateFrame(args);

            this.bgColor.R += 0.00100f * this.taxaDeAtualizacao;
            if (this.bgColor.R > 1.0f)
            {
                this.bgColor.R = 0.0f;
            }

            this.bgColor.G += 0.00013f * this.taxaDeAtualizacao;
            if (this.bgColor.G > 1.0f)
            {
                this.bgColor.G = 0.0f;
            }

            this.bgColor.B += 0.00333f * this.taxaDeAtualizacao;
            if (this.bgColor.B > 1.0f)
            {
                this.bgColor.B = 0.0f;
            }

            GL.ClearColor(this.bgColor);
        }

        /// <summary>
        /// O método 'OnRenderFrame' é chamado uma vez a cada frame. Sua função
        /// é chamar as funções que permitem desenhar na tela , utilizando os
        /// dados disponíveis.
        /// </summary>
        /// <param name="args">Argumentos com parâmetros individuais por quadro.</param>
        protected override void OnRenderFrame(FrameEventArgs args)
        {
            base.OnRenderFrame(args);

            // Limpa o Frame Buffer, utilizando a cor de fundo.
            GL.Clear(ClearBufferMask.ColorBufferBit);

            // AQUI VOCÊ DEVERÁ DESENHAR OS OBJETOS

            // Disponibiliza o Frame Buffer na tela, alternando entre dois buffers que são pintados em sequência (Double buffering).
            this.SwapBuffers();
        }
    }
}
