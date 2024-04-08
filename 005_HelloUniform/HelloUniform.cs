// <copyright file="HelloUniform.cs" company="PlaceholderCompany">
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
    internal class HelloUniform : GameWindow
    {
        private FPSCounter fpsCounter;

        private VertexBufferObject vbo;
        private VertexArrayObject vao;

        private Shader shader;

        // Que para criar uma uniform são necessárias 2 variáveis (1 p valor e outra p/ endereço na vram)
        private int tickUniformLocation;
        private float tick = 0.0f;

        /// <summary>
        /// Initializes a new instance of the <see cref="HelloUniform"/> class.
        /// </summary>
        /// <param name="gameWindowSettings"> PARAM TODO. </param>
        /// <param name="nativeWindowSettings"> PARAM2 TODO. </param>
        public HelloUniform(
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

            this.shader = new Shader("HelloUniform");

            this.tickUniformLocation = GL.GetUniformLocation(this.shader.ID, "tick");

            GL.ClearColor(1.0f, 0.0f, 0.0f, 1.0f);
        }

        /// <inheritdoc/>
        protected override void OnRenderFrame(FrameEventArgs args)
        {
            base.OnRenderFrame(args);

            GL.Clear(ClearBufferMask.ColorBufferBit);

            this.shader.Use();

            GL.Uniform1(this.tickUniformLocation, this.tick);

            Draw.Triangles(this.vao, 0, 3);

            this.SwapBuffers();
        }

        /// <inheritdoc/>
        protected override void OnUpdateFrame(FrameEventArgs args)
        {
            base.OnUpdateFrame(args);

            this.tick += 1.66f * (float)args.Time;

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
