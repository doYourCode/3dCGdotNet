// <copyright file="FrameBufferObject.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Framework.Core.Buffer
{
    using Framework.Core.Resource;
    using Framework.Utils;
    using OpenTK.Graphics.OpenGL4;
    using OpenTK.Mathematics;

    /// <summary>
    /// TODO.
    /// </summary>
    public class FrameBufferObject : OpenGLObject
    {
        private readonly Color4 defaultBackgroundColor = new Color4(
            1.0f,
            1.0f,
            0.0f,
            1.0f);

        private Texture texture;

        private int width;

        private int height;

        private float gamma;

        private uint renderBufferId = 0;

        /// <summary>
        /// Initializes a new instance of the <see cref="FrameBufferObject"/> class.
        /// </summary>
        /// <param name="width"> PARAM TODO. </param>
        /// <param name="height"> PARAM2 TODO. </param>
        /// <param name="isMultisample"> PARAM3 TODO. </param>
        /// <param name="useRenderBuffer"> PARAM4 TODO. </param>
        /// <param name="numSamples"> PARAM5 TODO. </param>
        /// <param name="gamma"> PARAM6 TODO. </param>
        public FrameBufferObject(
            int width = 512,
            int height = 512,
            bool isMultisample = false,
            bool useRenderBuffer = false,
            int numSamples = 1,
            float gamma = 1.0f)
            : base("FrameBufferObject", (uint)GL.GenBuffer())
        {
            this.width = width;
            this.height = height;
            this.gamma = gamma;

            GL.BindFramebuffer(FramebufferTarget.Framebuffer, this.ID);

            // Gera um endereço para uma textura de uso interno
            this.texture = new Texture("FBO (" + this.ID + ") - Texture ", (uint)GL.GenTexture());

            GL.ActiveTexture(TextureUnit.Texture0);

            if (isMultisample)
            {
                // Configurar parâmetros para criar textura e framebuffer com multisampling
                GL.BindTexture(TextureTarget.Texture2DMultisample, this.texture.ID);

                GL.TexImage2DMultisample(
                    TextureTargetMultisample.Texture2DMultisample,
                    numSamples,
                    PixelInternalFormat.Rgb32f,
                    this.width,
                    this.height,
                    true);

                GL.TexParameter(
                    TextureTarget.Texture2DMultisample,
                    TextureParameterName.TextureMinFilter,
                    (int)TextureMinFilter.Nearest);

                GL.TexParameter(
                    TextureTarget.Texture2DMultisample,
                    TextureParameterName.TextureMagFilter,
                    (int)TextureMinFilter.Nearest);

                GL.TexParameter(
                    TextureTarget.Texture2DMultisample,
                    TextureParameterName.TextureWrapS,
                    (int)TextureWrapMode.ClampToEdge);   // Previne "edge bleeding"
                GL.TexParameter(
                    TextureTarget.Texture2DMultisample,
                    TextureParameterName.TextureWrapT,
                    (int)TextureWrapMode.ClampToEdge);

                GL.FramebufferTexture2D(
                    FramebufferTarget.Framebuffer,
                    FramebufferAttachment.ColorAttachment0,
                    TextureTarget.Texture2DMultisample,
                    this.texture.ID,
                    0);

                if (useRenderBuffer)
                {
                    this.renderBufferId = (uint)GL.GenRenderbuffer();

                    GL.BindRenderbuffer(RenderbufferTarget.Renderbuffer, this.renderBufferId);

                    GL.RenderbufferStorageMultisample(
                        RenderbufferTarget.Renderbuffer,
                        numSamples,
                        RenderbufferStorage.Depth24Stencil8,
                        this.width,
                        this.height);

                    GL.FramebufferRenderbuffer(
                        FramebufferTarget.Framebuffer,
                        FramebufferAttachment.DepthStencilAttachment,
                        RenderbufferTarget.Renderbuffer,
                        this.renderBufferId);
                }
            }
            else
            {
                // Configurar parâmetros para criar textura e framebuffer sem multisampling
                GL.BindTexture(TextureTarget.Texture2D, this.texture.ID);

                GL.TexImage2D(
                    TextureTarget.Texture2D,
                    0,
                    PixelInternalFormat.Rgb32f,
                    this.width,
                    this.height,
                    0,
                    PixelFormat.Rgb,
                    PixelType.UnsignedByte,
                    (IntPtr)null);

                GL.TexParameter(
                    TextureTarget.Texture2D,
                    TextureParameterName.TextureMinFilter,
                    (int)TextureMinFilter.Nearest);

                GL.TexParameter(
                    TextureTarget.Texture2D,
                    TextureParameterName.TextureMagFilter,
                    (int)TextureMinFilter.Nearest);

                GL.TexParameter(
                    TextureTarget.Texture2D,
                    TextureParameterName.TextureWrapS,
                    (int)TextureWrapMode.ClampToEdge);   // Previne "edge bleeding"

                GL.TexParameter(
                    TextureTarget.Texture2D,
                    TextureParameterName.TextureWrapT,
                    (int)TextureWrapMode.ClampToEdge);

                GL.FramebufferTexture2D(
                    FramebufferTarget.Framebuffer,
                    FramebufferAttachment.ColorAttachment0,
                    TextureTarget.Texture2D,
                    this.texture.ID,
                    0);

                if (useRenderBuffer)
                {
                    this.renderBufferId = (uint)GL.GenRenderbuffer();

                    GL.BindRenderbuffer(RenderbufferTarget.Renderbuffer, this.renderBufferId);

                    GL.RenderbufferStorage(
                        RenderbufferTarget.Renderbuffer,
                        RenderbufferStorage.Depth24Stencil8,
                        this.width,
                        this.height);

                    GL.FramebufferRenderbuffer(
                        FramebufferTarget.Framebuffer,
                        FramebufferAttachment.DepthStencilAttachment,
                        RenderbufferTarget.Renderbuffer,
                        this.renderBufferId);
                }
            }

            // Error checking framebuffer
            var fboStatus = GL.CheckFramebufferStatus(FramebufferTarget.Framebuffer);

            if (fboStatus != FramebufferErrorCode.FramebufferComplete)
            {
                Console.WriteLine("Framebuffer " + this.ID + " error:" + fboStatus);
            }
        }

        /// <summary>
        /// Gets the internal Texture object.
        /// </summary>
        public Texture Texture { get => this.texture; }

        /// <summary>
        /// Unbinds any bound custom FrameBuffer, it makes OpenGL bind the Default.
        /// </summary>
        public static void BindDefault()
        {
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, CONSTANTS.NONE);
        }

        /// <summary>
        /// Binds the buffer.
        /// </summary>
        public void Bind()
        {
            // Reconfigurar a viewport do OpenGL para as dimensões do FBO
            GL.Viewport(0, 0, this.width, this.height);

            GL.BindFramebuffer(FramebufferTarget.Framebuffer, this.ID);

            GL.ClearColor(this.defaultBackgroundColor);
            //GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            // Ao final é necessário reativar o Depth Testing uma vez que este
            // foi desabilitado para desenhar o FrameRect na função FunFunc(...)
            GL.Enable(EnableCap.DepthTest);
        }

        /// <summary>
        /// Unbinds the buffer.
        /// </summary>
        public void Unbind()
        {
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, CONSTANTS.NONE);
        }

        /// <summary>
        /// Copies the data from a Texture to another.
        /// </summary>
        /// <param name="other">The other texture to blit to.</param>
        public void BlitTexture(FrameBufferObject other)
        {
            // Make it so the multisampling FBO is read while the post-processing FBO is drawn
            GL.BindFramebuffer(FramebufferTarget.ReadFramebuffer, this.ID);
            GL.BindFramebuffer(FramebufferTarget.DrawFramebuffer, other.ID);

            GL.BlitFramebuffer(
                0,
                0,
                this.width,
                this.height,
                0,
                0,
                other.width,
                other.height,
                ClearBufferMask.ColorBufferBit,
                BlitFramebufferFilter.Linear);
        }

        /// <summary>
        /// TODO.
        /// </summary>
        /// <param name="rect"> PARAM TODO. </param>
        /// <param name="shader"> PARAM2 TODO. </param>
        public void Draw(ScreenRectangle rect, Shader shader)
        {
            shader.Use();

            GL.BindVertexArray(rect.Vao.ID);
            GL.Disable(EnableCap.DepthTest); // prevents framebuffer rectangle from being discarded
            GL.ActiveTexture(TextureUnit.Texture0);
            GL.BindTexture(TextureTarget.Texture2D, this.texture.ID);
            GL.DrawArrays(PrimitiveType.Triangles, 0, 6);
        }

        /// <inheritdoc/>
        protected override void Dispose(bool isManualDispose)
        {
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, CONSTANTS.NONE);
            GL.DeleteFramebuffer(this.ID);
            GL.DeleteRenderbuffer(0);

            this.texture.Dispose();
        }
    }
}
