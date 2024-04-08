// <copyright file="HelloTriangleOOP.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Examples
{
    using Framework.Core.Buffer;
    using Framework.Core.Vertex;
    using Framework.Utils;
    using Framework.Utils.GUI;
    using OpenTK.Graphics.OpenGL4;
    using OpenTK.Windowing.Common;
    using OpenTK.Windowing.Desktop;

    /// <inheritdoc/>
    internal class HelloTriangleOOP : GameWindow
    {
        private FPSCounter fpsCounter;

        private VertexBufferObject vbo;
        private VertexArrayObject vao;

        /// <summary>
        /// Initializes a new instance of the <see cref="HelloTriangleOOP"/> class.
        /// </summary>
        /// <param name="gameWindowSettings"> PARAM TODO. </param>
        /// <param name="nativeWindowSettings"> PARAM2 TODO. </param>
        public HelloTriangleOOP(
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
            // .X       Y        Z
                -0.75f, -0.75f,  0.0f,  // Vértice 0 -> canto inferior esquerdo
                0.7f,   -0.75f,  0.0f,  // Vértice 1 -> canto inferior direito
                0.0f,   0.75f,   0.0f,  // Vértice 2 -> canto superior (no centro da tela)
            };

            // Criação e configuração de um VertexBuffer (VBO)
            this.vbo = new VertexBufferObject(data);

            // Configura atributo "Position" e o associa ao VBO
            VertexFormat vertexFormat = new VertexFormat();

            vertexFormat.AddAttribute(this.vbo, VertexAttributeType.Position);

            // Cria o VertexArrayObject (VAO)
            this.vao = new VertexArrayObject(vertexFormat);

            GL.ClearColor(1.0f, 1.0f, 0.0f, 1.0f);
        }

        /// <inheritdoc/>
        protected override void OnRenderFrame(FrameEventArgs args)
        {
            base.OnRenderFrame(args);

            GL.Clear(ClearBufferMask.ColorBufferBit);

            // Realiza o desenho do triângulo
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
        }
    }
}
