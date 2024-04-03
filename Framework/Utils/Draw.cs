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
        /// 
        /// </summary>
        /// <param name="Vao"></param>
        /// <param name="First"></param>
        /// <param name="Count"></param>
        /// <param name="PointSize"></param>
        public static void Points(VertexArrayObject Vao, int First, int Count, int PointSize)
        {
            GL.PointSize(PointSize);

            Vao.Bind();

            GL.DrawArrays(PrimitiveType.Points, First, Count);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Vao"></param>
        /// <param name="First"></param>
        /// <param name="Count"></param>
        /// <param name="Thickness"></param>
        public static void Lines(VertexArrayObject Vao, int First, int Count, int Thickness)
        {
            GL.LineWidth(Thickness);

            Vao.Bind();

            GL.DrawArrays(PrimitiveType.LineLoop, First, Count);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Vao"></param>
        /// <param name="First"></param>
        /// <param name="Count"></param>
        public static void Triangles(VertexArrayObject Vao, int First, int Count)
        {
            Vao.Bind();

            GL.DrawArrays(PrimitiveType.Triangles, First, Count);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Vao"></param>
        /// <param name="IndexCount"></param>
        public static void Elements(VertexArrayObject Vao, int IndexCount)
        {
            Vao.Bind();

            GL.DrawElements(BeginMode.Triangles, IndexCount, DrawElementsType.UnsignedInt, 0);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Fbo"></param>
        /// <param name="ScreenRectangle"></param>
        public static void ScreenRectangle(FrameBufferObject Fbo, ScreenRectangle ScreenRectangle)
        {
            ScreenRectangle.Vao.Bind();

            GL.Disable(EnableCap.DepthTest);                        // Desligamos o "Depth testing" para garantir que o desenho
                                                                    // será feito sobre qualquer outro elemento da tela
            GL.BindTexture(TextureTarget.Texture2D, Fbo.Texture.ID);

            GL.DrawArrays(PrimitiveType.Triangles, 0, 6);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Fbo"></param>
        public static void ScreenRectangle(FrameBufferObject Fbo)
        {
            ScreenRectangle(Fbo, Draw.screenRectangle);
        }
    }
}
