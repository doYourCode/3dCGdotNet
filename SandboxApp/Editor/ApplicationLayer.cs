// <copyright file="ApplicationLayer.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Examples
{
    using Framework.Core;
    using Framework.Core.Buffer;
    using Framework.Utils;
    using Framework.Utils.GUI;
    using OpenTK.Graphics.OpenGL4;
    using OpenTK.Windowing.Common;
    using OpenTK.Windowing.Desktop;

    /// <inheritdoc/>
    public class ApplicationLayer : GameWindow
    {
        private FPSCounter fpsCounter;

        private ViewLayer view;

        private FrameBufferObject renderFrameBuffer;
        private FrameBufferObject rectFrameBuffer;

        private ScreenRectangle screenRectangle;

        private Shader shader;

        private Texture tex;
        private Texture tex2;

        private System.Numerics.Vector2 viewportSize;

        private int vertexBufferObject;
        private int vertexArrayObject;

        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationLayer"/> class.
        /// </summary>
        /// <param name="gameWindowSettings"> PARAM TODO. </param>
        /// <param name="nativeWindowSettings"> PARAM2 TODO. </param>
        public ApplicationLayer(
            GameWindowSettings gameWindowSettings,
            NativeWindowSettings nativeWindowSettings)
            : base(gameWindowSettings, nativeWindowSettings)
        {
            this.view = new ViewLayer();
        }

        /// <summary>
        /// Gets or sets the viewport size.
        /// </summary>
        public System.Numerics.Vector2 ViewportSize
        {
            get => this.viewportSize;
            set => this.viewportSize = value;
        }

        /// <summary>
        /// Gets the framebuffer object.
        /// </summary>
        public FrameBufferObject FrameBuffer { get => this.renderFrameBuffer; }

        /// <inheritdoc/>
        protected override void OnLoad()
        {
            base.OnLoad();

            this.fpsCounter = new FPSCounter(this);

            float[] data =
            {
            // Posições
            // .X       Y        Z
                -0.75f, -0.75f,  0.0f,  // Vértice 0 -> canto inferior esquerdo
                0.7f,   -0.75f,  0.0f,  // Vértice 1 -> canto inferior direito
                0.0f,   0.75f,   0.0f,  // Vértice 2 -> canto superior (no centro da tela)
            };

            this.vertexArrayObject = GL.GenVertexArray();
            GL.BindVertexArray(this.vertexArrayObject);

            this.vertexBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, this.vertexBufferObject);

            GL.BufferData(BufferTarget.ArrayBuffer, data.Length * sizeof(float), data, BufferUsageHint.StaticDraw);

            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);

            this.view.Load(this);

            this.renderFrameBuffer = new FrameBufferObject(
                (ushort)this.viewportSize.X,
                (ushort)this.viewportSize.Y);

            this.rectFrameBuffer = new FrameBufferObject(
                (ushort)this.viewportSize.X,
                (ushort)this.viewportSize.Y);


            this.screenRectangle = new ();

            this.shader = new Shader("ScreenRect");
            GL.Uniform1(GL.GetUniformLocation(this.shader.ID, "screenTexture"), 0);

            //this.tex = Texture.LoadFromFile("Uv_checker_01.png", TextureUnit.Texture0);
            //this.tex2 = Texture.LoadFromFile("Uv_checker_02.png", TextureUnit.Texture1);
        }

        /// <inheritdoc/>
        protected override void OnResize(ResizeEventArgs e)
        {
            base.OnResize(e);

            this.viewportSize = new System.Numerics.Vector2(e.Width, e.Height);

            GL.Viewport(0, 0, e.Width, e.Height);

            this.view.Resize(e.Width, e.Height);
        }

        /// <inheritdoc/>
        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            // BEGIN RENDER SCENE
            {
                // Enable depth testing for the scene rendering.
                GL.Enable(EnableCap.DepthTest);

                //this.renderFrameBuffer.Bind();

                GL.ClearColor(1.0f, 0.5f, 0.0f, 1.0f);
                GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

                // Ativa o VAO para ser desenhado
                GL.BindVertexArray(this.vertexArrayObject);

                // Chamada à função Draw
                GL.DrawArrays(PrimitiveType.Triangles, 0, 3);
                GL.BindVertexArray(CONSTANTS.NONE);

                this.renderFrameBuffer.Unbind();

                // END RENDER SCENE
            }

            this.renderFrameBuffer.BlitTexture(this.rectFrameBuffer);

            // Resets the viewport for the screen size.
            GL.Viewport(0, 0, this.ClientSize.X, this.ClientSize.Y);

            // GUI rendering disables Depth testing.
            //this.view.Render(e);

            //ImGuiController.CheckGLError("End of frame");

            //this.shader.Use();

            //this.rectFrameBuffer.Draw(this.screenRectangle, this.shader);

            //this.rectFrameBuffer.Texture.Use(TextureUnit.Texture0);
            //this.tex2.Use(TextureUnit.Texture1);

            //this.frameBuffer.Texture.Use(TextureUnit.Texture0);

            //Draw.ScreenRectangle(this.screenRectangle);

            this.SwapBuffers();
        }

        /// <inheritdoc/>
        protected override void OnUpdateFrame(FrameEventArgs args)
        {
            base.OnUpdateFrame(args);

            this.fpsCounter.Update(args);

            this.view.Update(this, args);
        }

        /// <inheritdoc/>
        protected override void OnUnload()
        {
            base.OnUnload();

            this.view.Unload();
        }

        /// <inheritdoc/>
        protected override void OnTextInput(TextInputEventArgs e)
        {
            base.OnTextInput(e);

            this.view.Controller.PressChar((char)e.Unicode);
        }

        /// <inheritdoc/>
        protected override void OnMouseWheel(MouseWheelEventArgs e)
        {
            base.OnMouseWheel(e);

            this.view.Controller.MouseScroll(e.Offset);
        }
    }
}