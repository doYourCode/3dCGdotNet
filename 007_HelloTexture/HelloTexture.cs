// <copyright file="HelloTexture.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Examples
{
    using Framework.Core;
    using Framework.Core.Buffer;
    using Framework.Core.Vertex;
    using Framework.Utils;
    using Framework.Utils.GUI;
    using OpenTK.Graphics.OpenGL4;
    using OpenTK.Windowing.Common;
    using OpenTK.Windowing.Desktop;

    /// <inheritdoc/>
    internal class HelloTexture : GameWindow
    {
        private FPSCounter fpsCounter;

        private VertexBufferObject vbo;
        private VertexArrayObject vao;

        private Shader shader;

        private Texture texture;

        /// <summary>
        /// Initializes a new instance of the <see cref="HelloTexture"/> class.
        /// </summary>
        /// <param name="gameWindowSettings"> PARAM TODO. </param>
        /// <param name="nativeWindowSettings"> PARAM2 TODO. </param>
        public HelloTexture(
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
            // .Position                Color                   Texture coordinates
            // .X       Y       Z       R       G       B       U       V
               -0.75f, -0.75f,  0.0f,   1.0f,   0.0f,   0.0f,   0.0f,   1.0f,
               0.75f,  -0.75f,  0.0f,   0.0f,   1.0f,   0.0f,   1.0f,   1.0f,
               0.0f,    0.75f,  0.0f,   0.0f,   0.0f,   1.0f,   0.5f,   0.0f,
            };

            this.vbo = new VertexBufferObject(data);

            VertexFormat vertexFormat = new VertexFormat();
            vertexFormat.AddAttributesGroup(this.vbo, VertexAttributeType.Position, VertexAttributeType.Color, VertexAttributeType.TexCoord0);

            this.vao = new VertexArrayObject(vertexFormat);

            this.shader = new Shader("HelloTexture");

            this.texture = Texture.LoadFromFile("Uv_checker_01.png", TextureUnit.Texture0);

            GL.ClearColor(0.1f, 0.1f, 0.2f, 1.0f);
        }

        /// <inheritdoc/>
        protected override void OnRenderFrame(FrameEventArgs args)
        {
            base.OnRenderFrame(args);

            GL.Clear(ClearBufferMask.ColorBufferBit);

            // Precisa informar qual(is) textura(s) vai usar no desenho.
            this.texture.Use(TextureUnit.Texture0);

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
            this.texture.Dispose();
        }
    }
}
