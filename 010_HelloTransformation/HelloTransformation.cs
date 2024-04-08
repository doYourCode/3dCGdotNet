// <copyright file="HelloTransformation.cs" company="PlaceholderCompany">
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
    internal class HelloTransformation : GameWindow
    {
        private FPSCounter fpsCounter;

        private float tick = 0.0f;
        private float speed = 100.0f;

        private Shader shader;
        private Texture texture;
        private BasicMesh mesh;
        private Transform transform;

        /// <summary>
        /// Initializes a new instance of the <see cref="HelloTransformation"/> class.
        /// </summary>
        /// <param name="gameWindowSettings"> PARAM TODO. </param>
        /// <param name="nativeWindowSettings"> PARAM2 TODO. </param>
        public HelloTransformation(
            GameWindowSettings gameWindowSettings,
            NativeWindowSettings nativeWindowSettings)
            : base(gameWindowSettings, nativeWindowSettings)
        {
            this.Title = "Hello Transformation!";
        }

        /// <inheritdoc/>
        protected override void OnLoad()
        {
            base.OnLoad();

            this.fpsCounter = new FPSCounter(this);

            GL.Enable(EnableCap.DepthTest);

            this.shader = new Shader("HelloTransformation");

            this.texture = Texture.LoadFromFile("Suzanne.png", TextureUnit.Texture0);

            this.mesh = new BasicMesh("Monkey.fbx");

            this.transform = new Transform();

            GL.ClearColor(0.1f, 0.1f, 0.2f, 1.0f);
        }

        /// <inheritdoc/>
        protected override void OnRenderFrame(FrameEventArgs args)
        {
            base.OnRenderFrame(args);

            // Limpar a tela antes de desenhar (usando a clear color)
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            this.texture.Use(TextureUnit.Texture0);
            this.shader.Use();

            this.mesh.Draw();

            this.SwapBuffers();
        }

        /// <inheritdoc/>
        protected override void OnUpdateFrame(FrameEventArgs args)
        {
            base.OnUpdateFrame(args);

            this.fpsCounter.Update(args);

            // Rotate the model matrix (experimente alterar esses valores)
            this.transform.SetRotationY((float)System.Math.Cos(this.tick));

            // Identity matrix (per object)
            this.shader.SetMatrix4("model", this.transform.GetModelMatrix());

            this.tick += 0.0001f * this.speed;
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
