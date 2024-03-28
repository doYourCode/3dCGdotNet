using OpenTK.Graphics.OpenGL4;
using Framework.Utils;
using OpenTK.Mathematics;
using Framework.Core.Base;

namespace Framework.Core.Buffer
{
    /// <summary>
    /// 
    /// </summary>
    public class FrameBufferObject : ResourceObject
    {
        #region (Data Fields)

        private Texture texture;       // Endereço da textura interna do FBO
                                        // TODO: trocar para a versão POO de textura que já está implementada (requer alterações)
        private UInt16 width, height;   // Altura e largura da textura produzida

        private UInt16 numSamples;      // Número de amostras para FBOs com anti-aliasing

        private float gamma;

        private readonly Color4 defaultBackgroundColor = new Color4(0.0f, 0.0f, 0.0f, 1.0f);

        #endregion

        #region (Constructors)

        /* ---------------------------------------------- Interface pública ---------------------------------------------- */

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Width"></param>
        /// <param name="Height"></param>
        /// <param name="IsMultisample"></param>
        /// <param name="NumSamples"></param>
        /// <param name="Gamma"></param>
        public FrameBufferObject(ushort Width = 512, ushort Height = 512, bool IsMultisample = false, ushort NumSamples = 1, float Gamma = 1.0f) : base("FrameBufferObject ", (UInt32)GL.GenBuffer())
        {
            this.width = Width;
            this.height = Height;
            this.numSamples = NumSamples;
            this.gamma = Gamma;


            GL.BindFramebuffer(FramebufferTarget.Framebuffer, id);

            // Gera um endereço para uma textura de uso interno
            texture = new Texture("FBO (" + id + ") - Texture ", (UInt32)GL.GenTexture());

            if (IsMultisample)
            {
                // Configurar parâmetros para criar textura e framebuffer com multisampling
                GL.BindTexture(TextureTarget.Texture2DMultisample, this.texture.ID);

                GL.TexImage2DMultisample(TextureTargetMultisample.Texture2DMultisample, NumSamples, PixelInternalFormat.Rgb32f , this.width, this.height, true);

                GL.TexParameter(TextureTarget.Texture2DMultisample, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Nearest);
                GL.TexParameter(TextureTarget.Texture2DMultisample, TextureParameterName.TextureMagFilter, (int)TextureMinFilter.Nearest);

                GL.TexParameter(TextureTarget.Texture2DMultisample, TextureParameterName.TextureWrapS, (int)TextureWrapMode.ClampToEdge);   // Previne "edge bleeding"
                GL.TexParameter(TextureTarget.Texture2DMultisample, TextureParameterName.TextureWrapT, (int)TextureWrapMode.ClampToEdge);   //

                GL.FramebufferTexture2D(FramebufferTarget.Framebuffer, FramebufferAttachment.ColorAttachment0, TextureTarget.Texture2DMultisample, this.texture.ID, 0);


                // É necessário criar e configurar um render buffer nesse caso para fazer o multisampling a partir dele. TODO: implementar um RenderBufferObject para
                // substituir esse trecho por uma versão mais "OOP"

                UInt32 rboId;
                rboId = (UInt32)GL.GenRenderbuffer();

                GL.BindRenderbuffer(RenderbufferTarget.Renderbuffer, rboId);

                GL.RenderbufferStorageMultisample(RenderbufferTarget.Renderbuffer, NumSamples, RenderbufferStorage.Depth24Stencil8, this.width, this.height);
                GL.FramebufferRenderbuffer(FramebufferTarget.Framebuffer, FramebufferAttachment.DepthStencilAttachment, RenderbufferTarget.Renderbuffer, rboId);
            }
            else
            {
                // Configurar parâmetros para criar textura e framebuffer sem multisampling
                GL.BindTexture(TextureTarget.Texture2DMultisample, this.texture.ID);

                GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgb32f, this.width, this.height, 0, PixelFormat.Rgb, PixelType.UnsignedByte, (IntPtr)null);

                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Nearest);
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMinFilter.Nearest);

                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.ClampToEdge);   // Previne "edge bleeding"
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.ClampToEdge);   //

                GL.FramebufferTexture2D(FramebufferTarget.Framebuffer, FramebufferAttachment.ColorAttachment0, TextureTarget.Texture2D, this.texture.ID, 0);
            }

            // Error checking framebuffer
            var fboStatus = GL.CheckFramebufferStatus(FramebufferTarget.Framebuffer);

            if (fboStatus != FramebufferErrorCode.FramebufferComplete)
                Console.WriteLine("Framebuffer " + this.texture.ID + " error:" + fboStatus);
        }

        #endregion

        #region (Properties)

        /// <summary>
        /// Id que reflete o endereço do buffer na VRAM
        /// </summary>
        public UInt32 ID { get { return id; } private set { } }

        /// <summary>
        /// 
        /// </summary>
        public Texture Texture { get => this.texture; private set { } }

        #endregion

        #region (Public Methods)

        /// <summary>
        /// 
        /// </summary>
        void Bind()
        {
            // Reconfigurar a viewport do OpenGL para as dimensões do FBO
            GL.Viewport(0, 0, this.width, this.height);

            GL.BindFramebuffer(FramebufferTarget.Framebuffer, this.id);
            
            GL.ClearColor(this.defaultBackgroundColor);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            // Ao final é necessário reativar o Depth Testing uma vez que este foi desabilitado para desenhar o FrameRect na função FunFunc(...)
            GL.Enable(EnableCap.DepthTest);
        }

        /// <summary>
        /// 
        /// </summary>
        public void Unbind()
        {
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, CONSTANTS.NONE);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Other"></param>
        public void BlitTexture(FrameBufferObject Other)
        {
            // TODO
        }

        /// <summary>
        /// 
        /// </summary>
        static void BindDefault()
        {
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, CONSTANTS.NONE);
        }

        #endregion

        #region (Other Methods)

        /// <summary>
        /// 
        /// </summary>
        /// <param name="isManualDispose"></param>
        /// <exception cref="NotImplementedException"></exception>
        protected override void Dispose(bool isManualDispose)
        {
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, CONSTANTS.NONE);
            GL.DeleteFramebuffer(this.id);

            texture.Dispose();
        }

        #endregion
    }
}
