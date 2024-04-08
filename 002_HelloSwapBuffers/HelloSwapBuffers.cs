// <copyright file="HelloSwapBuffers.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Examples
{
    using Framework.Utils.GUI;
    using OpenTK.Graphics.OpenGL;
    using OpenTK.Mathematics;
    using OpenTK.Windowing.Common;
    using OpenTK.Windowing.Desktop;

    /// <inheritdoc/>
    internal class HelloSwapBuffers : GameWindow
    {
        private FPSCounter fpsCounter;

        private Color4 backgroundColor = new Color4(0.0f, 0.0f, 0.0f, 1.0f);

        private int taxaDeAtualizacao = 100;

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

        /// <inheritdoc/>
        protected override void OnLoad()
        {
            base.OnLoad();

            this.fpsCounter = new FPSCounter(this);

            // Configura a cor de fundo do Frame Buffer.
            GL.ClearColor(this.backgroundColor);
        }

        /// <inheritdoc/>
        protected override void OnUpdateFrame(FrameEventArgs args)
        {
            base.OnUpdateFrame(args);

            this.fpsCounter.Update(args);

            // Diferença de tempo para cada frame, usada para suavisar qualquer
            // animação de cores ou movimento.
            float dT = (float)args.Time;

            /* Para criar o efeito de variação de cores o que é feito é atualizar
             * cada canal de cor por uma taxa. Note que para obter um efeito
             * diferente para cada canal há um valor fixo e um variável por canal.
            */

            this.backgroundColor.R += 0.00100f * this.taxaDeAtualizacao * dT;
            if (this.backgroundColor.R > 1.0f)
            {
                this.backgroundColor.R = 0.0f;
            }

            this.backgroundColor.G += 0.00013f * this.taxaDeAtualizacao * dT;
            if (this.backgroundColor.G > 1.0f)
            {
                this.backgroundColor.G = 0.0f;
            }

            this.backgroundColor.B += 0.00333f * this.taxaDeAtualizacao * dT;
            if (this.backgroundColor.B > 1.0f)
            {
                this.backgroundColor.B = 0.0f;
            }

            GL.ClearColor(this.backgroundColor);
        }

        /// <inheritdoc/>
        protected override void OnRenderFrame(FrameEventArgs args)
        {
            /* O método 'OnRenderFrame' é chamado uma vez a cada frame. Sua função
             * é chamar as funções que permitem desenhar na tela , utilizando os
             * dados disponíveis.
            */

            base.OnRenderFrame(args);

            // Limpa o Frame Buffer, utilizando a cor de fundo.
            GL.Clear(ClearBufferMask.ColorBufferBit);

            // AQUI ( entre GL.Clear(...) e this.SwapBuffers() ) VOCÊ DEVERÁ DESENHAR OS OBJETOS

            // Disponibiliza o Frame Buffer na tela, alternando entre dois buffers
            // que são pintados em sequência (Double buffering).
            this.SwapBuffers();
        }
    }
}
