// <copyright file="HelloWindow.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Examples
{
    using Framework.Utils.GUI;
    using OpenTK.Windowing.Common;
    using OpenTK.Windowing.Desktop;

    /// <inheritdoc/>
    internal class HelloWindow : GameWindow
    {
        // Contador de FPS é um objeto que exibe quantos milisegundos leva cada
        // quadro para ser concluído.
        private FPSCounter fpsCounter;

        /// <summary>
        /// Initializes a new instance of the <see cref="HelloWindow"/> class.
        /// </summary>
        /// <param name="gameWindowSettings"> Internal game window settings. </param>
        /// <param name="nativeWindowSettings"> Native (OS) window settings. </param>
        public HelloWindow(
            GameWindowSettings gameWindowSettings,
            NativeWindowSettings nativeWindowSettings)
            : base(gameWindowSettings, nativeWindowSettings)
        {
        }

        /// <inheritdoc/>
        protected override void OnLoad()
        {
            /* O método 'OnLoad' é chamado uma vez, logo após a criação da janela.
             * Sua função é permitir que o usuário configure a janela, os objetos
             * e carregue os dados necessários antes de iniciar o loop de renderização.
            */

            base.OnLoad();

            // O contador de FPS precisa ser inicializado aqui.
            this.fpsCounter = new FPSCounter(this);
        }

        /// <inheritdoc/>
        protected override void OnUpdateFrame(FrameEventArgs args)
        {
            /* O método 'OnUpdateFrame' é normalmente executado uma vez por frame
             * (pode ser alterado). Neste método chamamos as principais funções
             * da lógica de negócio do programa.
            */

            base.OnUpdateFrame(args);

            // O contador de FPS precisa ser atualizado quadro-a-quadro.
            this.fpsCounter.Update(args);
        }
    }
}