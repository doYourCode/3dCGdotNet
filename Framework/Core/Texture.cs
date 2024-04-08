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
    /// TODO.
    /// </summary>
    public class Texture : OpenGLObject
    {
        private static string rootPath = string.Empty;

        /// <summary>
        /// Initializes a new instance of the <see cref="Texture"/> class.
        /// </summary>
        public Texture()
            : base("Texture", (uint)GL.GenTexture())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Texture"/> class.
        /// </summary>
        /// <param name="id"> PARAM TODO. </param>
        public Texture(uint id)
            : base("Texture", id)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Texture"/> class.
        /// </summary>
        /// <param name="label"> PARAM TODO. </param>
        public Texture(string label)
            : base(label, (uint)GL.GenTexture())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Texture"/> class.
        /// </summary>
        /// /// <param name="label"> PARAM TODO. </param>
        /// <param name="id"> PARAM2 TODO. </param>
        public Texture(string label, uint id)
            : base(label, id)
        {
        }

        /// <summary>
        /// Gets or sets TODO.
        /// </summary>
        public static string RootPath
        {
            get { return rootPath; } set { rootPath = value; }
        }

        /// <summary>
        /// TODO.
        /// </summary>
        /// <param name="path"> PARAM TODO. </param>
        /// <param name="unit"> PARAM2 TODO. </param>
        /// /// <param name="label"> PARAM3 TODO. </param>
        /// <param name="invertY"> PARAM4 TODO. </param>
        /// <returns> RETURN TODO. </returns>
        public static Texture LoadFromFile(string path, TextureUnit unit, string? label = null,  bool invertY = false)
        {
            path = rootPath + path;

            uint handle = (uint)GL.GenTexture();
            GL.ActiveTexture(unit);
            GL.BindTexture(TextureTarget.Texture2D, handle);

            if (invertY)
            {
                StbImage.stbi_set_flip_vertically_on_load(1);
            }

            using (Stream stream = File.OpenRead(path))
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

            if (label == null)
            {
                return new Texture(handle);
            }

            return new Texture(label, handle);
        }

        /// <summary>
        /// TODO.
        /// </summary>
        /// <param name="width"> PARAM TODO. </param>
        /// <param name="height"> PARAM2 TODO. </param>
        /// <param name="unit"> PARAM3 TODO. </param>
        /// <returns> RETURN TODO. </returns>
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
        /// TODO.
        /// </summary>
        /// <param name="unit"> PARAM TODO. </param>
        public void Use(TextureUnit unit)
        {
            GL.ActiveTexture(unit);
            GL.BindTexture(TextureTarget.Texture2D, this.ID);
        }

        /// <summary>
        /// TODO.
        /// </summary>
        /// <param name="isManualDispose"> PARAM TODO. </param>
        protected override void Dispose(bool isManualDispose)
        {
            GL.BindTexture(TextureTarget.Texture2D, CONSTANTS.NONE);
            GL.DeleteTexture(this.ID);
        }
    }
}
