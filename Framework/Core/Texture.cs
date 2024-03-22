using OpenTK.Graphics.OpenGL4;
using PixelFormat = OpenTK.Graphics.OpenGL4.PixelFormat;
using StbImageSharp;
using System;

namespace Framework.Core
{
    public class Texture
    {
        /* -------------------------------------------- Variáveis de classe -------------------------------------------- */

#if DEBUG
        /// <summary>
        /// Representa o quantitativo de texturas existentes na VRAM.
        /// </summary>
        public static UInt32 Count { get { return count; } private set { } }

        private static UInt32 count = 0;
#endif


        /* ---------------------------------------------- Variáveis membro ---------------------------------------------- */

        /// <summary>
        /// Id que reflete o endereço da textura na VRAM
        /// </summary>
        public UInt32 ID { get { return id; } private set { } }

        private UInt32 id;


        /* ---------------------------------------------- Interface pública ---------------------------------------------- */

        public static Texture LoadFromFile(string path, TextureUnit unit, bool invertY = false)
        {
            UInt32 handle = (UInt32)GL.GenTexture();
            GL.ActiveTexture(unit);
            GL.BindTexture(TextureTarget.Texture2D, handle);

            if (invertY)
                StbImage.stbi_set_flip_vertically_on_load(1);

            using (Stream stream = File.OpenRead(path))
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

        public static Texture CreateInMemory(int width, int height, TextureUnit unit)
        {
            UInt32 handle = (UInt32)GL.GenTexture();
            GL.ActiveTexture(unit);
            GL.BindTexture(TextureTarget.Texture2D, handle);

            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, width, height, 0, PixelFormat.Rgba, PixelType.UnsignedByte, (System.IntPtr)0);

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Repeat);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Repeat);

            return new Texture(handle);
        }

        public Texture(UInt32 handle)
        {
            id = handle;
        }

        public void Use(TextureUnit unit)
        {
            GL.ActiveTexture(unit);
            GL.BindTexture(TextureTarget.Texture2D, id);
        }
    }
}
