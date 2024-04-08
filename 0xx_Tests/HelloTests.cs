// <copyright file="HelloTests.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Examples
{
    using Framework.Core;
    using Framework.Utils;
    using OpenTK.Windowing.Common;
    using OpenTK.Windowing.Desktop;

    /// <summary>
    /// TODO.
    /// </summary>
    internal class HelloTests : GameWindow
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HelloTests"/> class.
        /// TODO.
        /// </summary>
        /// <param name="gameWindowSettings"> PARAM TODO. </param>
        /// <param name="nativeWindowSettings"> PARAM2 TODO. </param>
        public HelloTests(
            GameWindowSettings gameWindowSettings,
            NativeWindowSettings nativeWindowSettings)
            : base(gameWindowSettings, nativeWindowSettings)
        {
        }

        /// <inheritdoc/>
        protected override void OnLoad()
        {
            Transform transform = new Transform();
            transform.SetPosition(10, 20, 30);
            transform.SetRotation(100, 200, 300, 0);
            transform.SetScale(0.1f, 0.2f, 0.3f);

            Print.Mat4(transform.GetModelMatrix());

            base.OnLoad();
        }

        /// <inheritdoc/>
        protected override void OnRenderFrame(FrameEventArgs args)
        {
            base.OnRenderFrame(args);
        }

        /// <inheritdoc/>
        protected override void OnUnload()
        {
            base.OnUnload();
        }
    }
}
