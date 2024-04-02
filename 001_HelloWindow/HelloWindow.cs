// <copyright file="HelloWindow.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Examples
{
    using OpenTK.Windowing.Desktop;

    /// <summary>
    /// Cria uma janela vazia utilizando a funcionalidade disponível na biblioteca OpenTK.
    /// </summary>
    internal class HelloWindow : GameWindow
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HelloWindow"/> class.
        /// </summary>
        /// <param name="gameWindowSettings">Configurações internas de janela.</param>
        /// <param name="nativeWindowSettings">Configurações nativas de janela.</param>
        public HelloWindow(
            GameWindowSettings gameWindowSettings,
            NativeWindowSettings nativeWindowSettings)
            : base(gameWindowSettings, nativeWindowSettings)
        {
        }
    }
}