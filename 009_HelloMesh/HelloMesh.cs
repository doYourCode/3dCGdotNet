// <copyright file="HelloMesh.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Examples
{
    using ExamplesCommon;
    using Framework.Core;
    using Framework.Utils.GUI;
    using OpenTK.Graphics.OpenGL4;
    using OpenTK.Windowing.Common;
    using OpenTK.Windowing.Desktop;

    /// <inheritdoc/>
    internal class HelloMesh : GameWindow
    {
        private FPSCounter fpsCounter;

        private Shader shader;
        private Texture texture;
        private BasicMesh mesh;

        /// <summary>
        /// Initializes a new instance of the <see cref="HelloMesh"/> class.
        /// </summary>
        /// <param name="gameWindowSettings"> PARAM TODO. </param>
        /// <param name="nativeWindowSettings"> PARAM2 TODO. </param>
        public HelloMesh(
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

            GL.Enable(EnableCap.DepthTest);

            this.shader = new Shader("HelloMesh");

            this.texture = Texture.LoadFromFile("Suzanne.png", TextureUnit.Texture0);

            this.mesh = new BasicMesh("Monkey.fbx");

            GL.ClearColor(0.1f, 0.1f, 0.2f, 1.0f);
        }

        /// <inheritdoc/>
        protected override void OnRenderFrame(FrameEventArgs args)
        {
            base.OnRenderFrame(args);

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            // Bind em textura e shader
            this.texture.Use(TextureUnit.Texture0);
            this.shader.Use();

            // Dizer quais buffers vão ser desenhados c/ esse conjunto textura/shader
            this.mesh.Draw();

            this.SwapBuffers();
        }

        /// <inheritdoc/>
        protected override void OnUpdateFrame(FrameEventArgs args)
        {
            base.OnUpdateFrame(args);

            this.fpsCounter.Update(args);
        }

        /// <inheritdoc/>
        protected override void OnUnload()
        {
            base.OnUnload();

            this.mesh.Dispose();
            this.shader.Dispose();
            this.texture.Dispose();
        }
    }
}
