using OpenTK.Graphics.OpenGL4;
using PixelFormat = OpenTK.Graphics.OpenGL4.PixelFormat;
using StbImageSharp;
using Framework.Utils;
using Framework.Core.Base;

namespace Framework.Core
{
    /// <summary>
    /// 
    /// </summary>
    public class Texture : OpenGLObject
    {
        #region (Data Fields)

        private static string rootPath = "";

        #endregion

        #region (Constructors)

        /// <summary>
        /// 
        /// </summary>
        public Texture() : base("Texture ", (UInt32)GL.GenTexture()) { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        public Texture(UInt32 Id) : base("Texture ", Id) { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Label"></param>
        public Texture(string Label) : base(Label, (UInt32)GL.GenTexture()) { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        public Texture(string Label, UInt32 Id) : base(Label, Id) { }

        #endregion

        #region (Properties)

        /// <summary>
        /// Caminho para a pasta raiz para carregar arquivos de Shader.
        /// </summary>
        public static string RootPath { get { return rootPath; } set { rootPath = value; } }

        #endregion

        #region (Public Methods)

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Path"></param>
        /// <param name="Unit"></param>
        /// <param name="InvertY"></param>
        /// <returns></returns>
        public static Texture LoadFromFile(string Path, TextureUnit Unit, bool InvertY = false)
        {
            Path = rootPath + Path;

            UInt32 handle = (UInt32)GL.GenTexture();
            GL.ActiveTexture(Unit);
            GL.BindTexture(TextureTarget.Texture2D, handle);

            if (InvertY)
                StbImage.stbi_set_flip_vertically_on_load(1);

            using (Stream stream = File.OpenRead(Path))
            {
                ImageResult image = ImageResult.FromStream(stream, ColorComponents.RedGreenBlueAlpha);

                GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, image.Width, image.Height, 0, PixelFormat.Rgba, PixelType.UnsignedByte, image.Data);
            }

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Repeat);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Repeat);

            GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);

            return new Texture(handle);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Width"></param>
        /// <param name="Height"></param>
        /// <param name="Unit"></param>
        /// <returns></returns>
        public static Texture CreateInMemory(int Width, int Height, TextureUnit Unit)
        {
            UInt32 handle = (UInt32)GL.GenTexture();
            GL.ActiveTexture(Unit);
            GL.BindTexture(TextureTarget.Texture2D, handle);

            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, Width, Height, 0, PixelFormat.Rgba, PixelType.UnsignedByte, (System.IntPtr)0);

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Repeat);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Repeat);

            return new Texture(handle);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Unit"></param>
        public void Use(TextureUnit Unit)
        {
            GL.ActiveTexture(Unit);
            GL.BindTexture(TextureTarget.Texture2D, id);
        }

        #endregion

        #region (Other Methods)

        /// <summary>
        /// 
        /// </summary>
        /// <param name="isManualDispose"></param>
        protected override void Dispose(bool isManualDispose)
        {
            GL.BindTexture(TextureTarget.Texture2D, CONSTANTS.NONE);
            GL.DeleteTexture(id);
        }

        #endregion
    }
}
