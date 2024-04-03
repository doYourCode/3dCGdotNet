// <copyright file="Texture.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Framework.Core
{
    using Framework.Core.Resource;
    using Framework.Utils;
    using OpenTK.Graphics.OpenGL4;
    using StbImageSharp;
    using PixelFormat = OpenTK.Graphics.OpenGL4.PixelFormat;

    /// <summary>
    /// 
    /// </summary>
    public class Texture : OpenGLObject
    {
        private static string rootPath = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        public Texture()
            : base("Texture ", (uint)GL.GenTexture()) { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        public Texture(UInt32 Id)
            : base("Texture ", Id) { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Label"></param>
        public Texture(string Label)
            : base(Label, (uint)GL.GenTexture()) { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        public Texture(string Label, uint Id)
            : base(Label, Id) { }

        /// <summary>
        /// Caminho para a pasta raiz para carregar arquivos de Shader.
        /// </summary>
        public static string RootPath { get { return rootPath; } set { rootPath = value; } }

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

            uint handle = (uint)GL.GenTexture();
            GL.ActiveTexture(Unit);
            GL.BindTexture(TextureTarget.Texture2D, handle);

            if (InvertY)
            {
                StbImage.stbi_set_flip_vertically_on_load(1);
            }

            using (Stream stream = File.OpenRead(Path))
            {
                ImageResult image = ImageResult.FromStream(stream, ColorComponents.RedGreenBlueAlpha);

                GL.TexImage2D(
                    TextureTarget.Texture2D,
                    0,
                    PixelInternalFormat.Rgba,
                    image.Width,
                    image.Height,
                    0,
                    PixelFormat.Rgba,
                    PixelType.UnsignedByte,
                    image.Data);
            }

            GL.TexParameter(
                TextureTarget.Texture2D,
                TextureParameterName.TextureMinFilter,
                (int)TextureMinFilter.Linear);

            GL.TexParameter(
                TextureTarget.Texture2D,
                TextureParameterName.TextureMagFilter,
                (int)TextureMagFilter.Linear);

            GL.TexParameter(
                TextureTarget.Texture2D,
                TextureParameterName.TextureWrapS,
                (int)TextureWrapMode.Repeat);

            GL.TexParameter(
                TextureTarget.Texture2D,
                TextureParameterName.TextureWrapT,
                (int)TextureWrapMode.Repeat);

            GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);

            return new Texture(handle);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="unit"></param>
        /// <returns></returns>
        public static Texture CreateInMemory(
            int width,
            int height,
            TextureUnit unit)
        {
            uint handle = (uint)GL.GenTexture();
            GL.ActiveTexture(unit);
            GL.BindTexture(TextureTarget.Texture2D, handle);

            GL.TexImage2D(
                TextureTarget.Texture2D,
                0,
                PixelInternalFormat.Rgba,
                width,
                height,
                0,
                PixelFormat.Rgba,
                PixelType.UnsignedByte,
                (System.IntPtr)0);

            GL.TexParameter(
                TextureTarget.Texture2D,
                TextureParameterName.TextureMinFilter,
                (int)TextureMinFilter.Linear);

            GL.TexParameter(
                TextureTarget.Texture2D,
                TextureParameterName.TextureMagFilter,
                (int)TextureMagFilter.Linear);

            GL.TexParameter(
                TextureTarget.Texture2D,
                TextureParameterName.TextureWrapS,
                (int)TextureWrapMode.Repeat);

            GL.TexParameter(
                TextureTarget.Texture2D,
                TextureParameterName.TextureWrapT,
                (int)TextureWrapMode.Repeat);

            return new Texture(handle);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="unit"></param>
        public void Use(TextureUnit unit)
        {
            GL.ActiveTexture(unit);
            GL.BindTexture(TextureTarget.Texture2D, ID);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="isManualDispose"></param>
        protected override void Dispose(bool isManualDispose)
        {
            GL.BindTexture(TextureTarget.Texture2D, CONSTANTS.NONE);
            GL.DeleteTexture(ID);
        }
    }
}
