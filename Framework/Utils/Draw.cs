// <copyright file="Draw.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Framework.Utils
{
    using Framework.Core;
    using Framework.Core.Buffer;
    using OpenTK.Graphics.OpenGL4;

    /// <summary>
    /// Funções de utilidade para desenho de forma genérica.
    /// </summary>
    public static class Draw
    {
        private static ScreenRectangle screenRectangle = new ScreenRectangle();

        /// <summary>
        /// TODO.
        /// </summary>
        /// <param name="vao"> PARAM TODO. </param>
        /// <param name="first"> PARAM2 TODO. </param>
        /// <param name="count"> PARAM3 TODO. </param>
        /// <param name="pointSize"> PARAM4 TODO. </param>
        public static void Points(VertexArrayObject vao, int first, int count, int pointSize)
        {
            GL.PointSize(pointSize);

            vao.Bind();

            GL.DrawArrays(PrimitiveType.Points, first, count);
        }

        /// <summary>
        /// TODO.
        /// </summary>
        /// <param name="vao"> PARAM TODO. </param>
        /// <param name="first"> PARAM2 TODO. </param>
        /// <param name="count"> PARAM3 TODO. </param>
        /// <param name="thickness"> PARAM4 TODO. </param>
        public static void Lines(VertexArrayObject vao, int first, int count, int thickness)
        {
            GL.LineWidth(thickness);

            vao.Bind();

            GL.DrawArrays(PrimitiveType.LineLoop, first, count);
        }

        /// <summary>
        /// TODO.
        /// </summary>
        /// <param name="vao"> PARAM TODO. </param>
        /// <param name="first"> PARAM2 TODO. </param>
        /// <param name="count"> PARAM3 TODO. </param>
        public static void Triangles(VertexArrayObject vao, int first, int count)
        {
            vao.Bind();

            GL.DrawArrays(PrimitiveType.Triangles, first, count);
        }

        /// <summary>
        /// TODO.
        /// </summary>
        /// <param name="vao"> PARAM TODO.</param>
        /// <param name="indexCount"> PARAM2 TODO. </param>
        public static void Elements(VertexArrayObject vao, int indexCount)
        {
            vao.Bind();

            GL.DrawElements(BeginMode.Triangles, indexCount, DrawElementsType.UnsignedInt, 0);
        }

        /// <summary>
        /// TODO.
        /// </summary>
        /// <param name="fbo"> PARAM TODO. </param>
        /// <param name="screenRectangle"> PARAM TODO2. </param>
        public static void ScreenRectangle(FrameBufferObject fbo, ScreenRectangle screenRectangle)
        {
            screenRectangle.Vao.Bind();

            GL.Disable(EnableCap.DepthTest);    // Disable depth

            GL.BindTexture(TextureTarget.Texture2D, fbo.Texture.ID);

            GL.DrawArrays(PrimitiveType.Triangles, 0, 6);
        }

        /// <summary>
        /// TODO.
        /// </summary>
        /// <param name="fbo"> PARAM TODO. </param>
        public static void ScreenRectangle(FrameBufferObject fbo)
        {
            ScreenRectangle(fbo, Draw.screenRectangle);
        }
    }
}
