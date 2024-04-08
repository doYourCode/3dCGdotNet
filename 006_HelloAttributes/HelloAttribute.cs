// <copyright file="HelloAttribute.cs" company="PlaceholderCompany">
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
    internal class HelloAttribute : GameWindow
    {
        private FPSCounter fpsCounter;

        private VertexBufferObject vbo;
        private VertexArrayObject vao;

        private Shader shader;

        /// <summary>
        /// Initializes a new instance of the <see cref="HelloAttribute"/> class.
        /// </summary>
        /// <param name="gameWindowSettings"> PARAM TODO. </param>
        /// <param name="nativeWindowSettings"> PARAM2 TODO. </param>
        public HelloAttribute(
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
            // .Position                Color
            // .X       Y       Z       R       G       B
                -0.75f, -0.75f, 0.0f,   1.0f,   0.0f,   0.0f,
                0.75f, -0.75f,  0.0f,   0.0f,   1.0f,   0.0f,
                0.0f,   0.75f,  0.0f,   0.0f,   0.0f,   1.0f,
            };

            this.vbo = new VertexBufferObject(data);

            VertexFormat vertexFormat = new VertexFormat();
            vertexFormat.AddAttributesGroup(this.vbo, VertexAttributeType.Position, VertexAttributeType.Color);

            this.vao = new VertexArrayObject(vertexFormat);

            this.shader = new Shader("HelloAttribute");

            GL.ClearColor(0.1f, 0.1f, 0.2f, 1.0f);
        }

        /// <inheritdoc/>
        protected override void OnRenderFrame(FrameEventArgs args)
        {
            base.OnRenderFrame(args);

            GL.Clear(ClearBufferMask.ColorBufferBit);

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
