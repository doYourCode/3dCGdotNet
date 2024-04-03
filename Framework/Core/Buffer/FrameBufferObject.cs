// <copyright file="FrameBufferObject.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Framework.Core.Buffer
{
    using OpenTK.Graphics.OpenGL4;
    using Framework.Utils;
    using OpenTK.Mathematics;
    using Framework.Core.Resource;

    /// <summary>
    /// 
    /// </summary>
    public class FrameBufferObject : OpenGLObject
    {
        private readonly Color4 defaultBackgroundColor =
    new Color4(0.0f, 0.0f, 0.0f, 1.0f);

        private Texture texture;

        private ushort width;

        private ushort height;

        private ushort numSamples;

        private float gamma;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Width"></param>
        /// <param name="Height"></param>
        /// <param name="IsMultisample"></param>
        /// <param name="NumSamples"></param>
        /// <param name="Gamma"></param>
        public FrameBufferObject(ushort Width = 512,
                                 ushort Height = 512,
                                 bool IsMultisample = false,
                                 ushort NumSamples = 1,
                                 float Gamma = 1.0f) :
                                    base("FrameBufferObject",
                                        (UInt32)GL.GenBuffer())
        {
            this.width = Width;
            this.height = Height;
            this.numSamples = NumSamples;
            this.gamma = Gamma;


            GL.BindFramebuffer(FramebufferTarget.Framebuffer, ID);

            // Gera um endereço para uma textura de uso interno
            texture = new Texture("FBO (" + ID + ") - Texture ", (UInt32)GL.GenTexture());

            if (IsMultisample)
            {
                // Configurar parâmetros para criar textura e framebuffer com multisampling
                GL.BindTexture(TextureTarget.Texture2DMultisample, this.texture.ID);

                GL.TexImage2DMultisample(TextureTargetMultisample.Texture2DMultisample,
                                         NumSamples,
                                         PixelInternalFormat.Rgb32f,
                                         this.width,
                                         this.height,
                                         true);

                GL.TexParameter(TextureTarget.Texture2DMultisample,
                                TextureParameterName.TextureMinFilter,
                                (int)TextureMinFilter.Nearest);
                GL.TexParameter(TextureTarget.Texture2DMultisample,
                                TextureParameterName.TextureMagFilter,
                                (int)TextureMinFilter.Nearest);

                GL.TexParameter(TextureTarget.Texture2DMultisample,
                                TextureParameterName.TextureWrapS,
                                (int)TextureWrapMode.ClampToEdge);   // Previne "edge bleeding"
                GL.TexParameter(TextureTarget.Texture2DMultisample,
                                TextureParameterName.TextureWrapT,
                                (int)TextureWrapMode.ClampToEdge);

                GL.FramebufferTexture2D(FramebufferTarget.Framebuffer,
                                        FramebufferAttachment.ColorAttachment0,
                                        TextureTarget.Texture2DMultisample,
                                        this.texture.ID,
                                        0);


                // É necessário criar e configurar um render buffer nesse caso
                // para fazer o multisampling a partir dele. TODO: implementar
                // um RenderBufferObject para substituir esse trecho por uma versão mais "OOP"

                UInt32 rboId;
                rboId = (UInt32)GL.GenRenderbuffer();

                GL.BindRenderbuffer(RenderbufferTarget.Renderbuffer, rboId);

                GL.RenderbufferStorageMultisample(RenderbufferTarget.Renderbuffer,
                                                  NumSamples,
                                                  RenderbufferStorage.Depth24Stencil8,
                                                  this.width,
                                                  this.height);
                GL.FramebufferRenderbuffer(FramebufferTarget.Framebuffer,
                                           FramebufferAttachment.DepthStencilAttachment,
                                           RenderbufferTarget.Renderbuffer,
                                           rboId);
            }
            else
            {
                // Configurar parâmetros para criar textura e framebuffer sem multisampling
                GL.BindTexture(TextureTarget.Texture2DMultisample, this.texture.ID);

                GL.TexImage2D(TextureTarget.Texture2D,
                              0,
                              PixelInternalFormat.Rgb32f,
                              this.width,
                              this.height,
                              0,
                              PixelFormat.Rgb,
                              PixelType.UnsignedByte,
                              (IntPtr)null);

                GL.TexParameter(TextureTarget.Texture2D,
                                TextureParameterName.TextureMinFilter,
                                (int)TextureMinFilter.Nearest);
                GL.TexParameter(TextureTarget.Texture2D,
                                TextureParameterName.TextureMagFilter,
                                (int)TextureMinFilter.Nearest);

                GL.TexParameter(TextureTarget.Texture2D,
                                TextureParameterName.TextureWrapS,
                                (int)TextureWrapMode.ClampToEdge);   // Previne "edge bleeding"
                GL.TexParameter(TextureTarget.Texture2D,
                                TextureParameterName.TextureWrapT,
                                (int)TextureWrapMode.ClampToEdge);

                GL.FramebufferTexture2D(FramebufferTarget.Framebuffer,
                                        FramebufferAttachment.ColorAttachment0,
                                        TextureTarget.Texture2D,
                                        this.texture.ID,
                                        0);
            }

            // Error checking framebuffer
            var fboStatus = GL.CheckFramebufferStatus(FramebufferTarget.Framebuffer);

            if (fboStatus != FramebufferErrorCode.FramebufferComplete)
                Console.WriteLine("Framebuffer " + this.texture.ID + " error:" + fboStatus);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="isManualDispose"></param>
        /// <exception cref="NotImplementedException"></exception>
        protected override void Dispose(bool isManualDispose)
        {
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, CONSTANTS.NONE);
            GL.DeleteFramebuffer(this.ID);

            this.texture.Dispose();
        }

        /// <summary>
        /// Gets the internal Texture object.
        /// </summary>
        public Texture Texture { get => this.texture; private set { } }

        /// <summary>
        /// Binds the buffer.
        /// </summary>
        public void Bind()
        {
            // Reconfigurar a viewport do OpenGL para as dimensões do FBO
            GL.Viewport(0, 0, this.width, this.height);

            GL.BindFramebuffer(FramebufferTarget.Framebuffer, this.ID);

            GL.ClearColor(this.defaultBackgroundColor);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

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
            // TODO
        }

        /// <summary>
        /// Unbinds any bound custom FrameBuffer, it makes OpenGL bind the Default.
        /// </summary>
        public static void BindDefault()
        {
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, CONSTANTS.NONE);
        }
    }
}
