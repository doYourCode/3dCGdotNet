// <copyright file="HelloShader.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Examples
{
    using Framework.Core;
    using Framework.Core.Buffer;
    using Framework.Core.Vertex;
    using Framework.Utils;
    using Framework.Utils.GUI;
    using OpenTK.Graphics.OpenGL;
    using OpenTK.Windowing.Common;
    using OpenTK.Windowing.Desktop;

    /// <inheritdoc/>
    internal class HelloShader : GameWindow
    {
        private FPSCounter fpsCounter;

        private VertexBufferObject vbo;
        private VertexArrayObject vao;

        private Shader shader;

        /// <summary>
        /// Initializes a new instance of the <see cref="HelloShader"/> class.
        /// </summary>
        /// <param name="gameWindowSettings"> PARAM TODO. </param>
        /// <param name="nativeWindowSettings"> PARAM2 TODO. </param>
        public HelloShader(
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

            float[] data =
            {
            // .X       Y       Z
                -0.75f, -0.75f, 0.0f,
                0.75f,  -0.75f, 0.0f,
                0.0f,   0.75f,  0.0f,
            };

            this.vbo = new VertexBufferObject(data);

            VertexFormat vertexFormat = new VertexFormat();
            vertexFormat.AddAttribute(this.vbo, VertexAttributeType.Position);

            this.vao = new VertexArrayObject(vertexFormat);

            this.shader = new Shader("HelloShader");

            GL.ClearColor(1.0f, 0.0f, 0.0f, 1.0f);
        }

        /// <inheritdoc/>
        protected override void OnRenderFrame(FrameEventArgs args)
        {
            base.OnRenderFrame(args);

            GL.Clear(ClearBufferMask.ColorBufferBit);

            // Precisa indicar qual shader usar antes de chamar qualquer função Draw(...)
            this.shader.Use();

            Draw.Triangles(this.vao, 0, 3);

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

            this.vbo.Dispose();
            this.vao.Dispose();
            this.shader.Dispose();
        }
    }
}
