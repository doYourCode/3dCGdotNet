using Framework.Core;
using Framework.Core.Buffer;
using Framework.Utils.Common.Mesh;
using OpenTK.Graphics.OpenGL4;

namespace Framework.Utils
{
    /// <summary>
    /// Funções de utilidade para desenho de forma genérica.
    /// </summary>
    public static class Draw
    {
        /* -------------------------------------------- Variáveis de classe -------------------------------------------- */

        private static  ScreenRectangle screenRectangle = new ScreenRectangle();


        /* ---------------------------------------------- Interface pública ---------------------------------------------- */

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Vao"></param>
        /// <param name="First"></param>
        /// <param name="count"></param>
        /// <param name="pointSize"></param>
        public static void Points(VertexArrayObject Vao, int First, int count, int pointSize)
        {
            GL.PointSize(pointSize);

            Vao.Bind();

            GL.DrawArrays(PrimitiveType.Points, First, count);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Vao"></param>
        /// <param name="first"></param>
        /// <param name="count"></param>
        /// <param name="thickness"></param>
        public static void Lines(VertexArrayObject Vao, int first, int count, int thickness)
        {
            GL.LineWidth(thickness);

            Vao.Bind();

            GL.DrawArrays(PrimitiveType.LineLoop, first, count);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Vao"></param>
        /// <param name="first"></param>
        /// <param name="count"></param>
        public static void Triangles(VertexArrayObject Vao, int first, int count)
        {
            Vao.Bind();

            GL.DrawArrays(PrimitiveType.Triangles, first, count);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Vao"></param>
        /// <param name="indexCount"></param>
        public static void Elements(VertexArrayObject Vao, int indexCount)
        {
            Vao.Bind();

            GL.DrawElements(BeginMode.Triangles, indexCount, DrawElementsType.UnsignedInt, 0);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mesh"></param>
        public static void Mesh(BasicMesh Mesh)
        {
            Mesh.Draw();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Fbo"></param>
        /// <param name="ScreenRectangle"></param>
        public static void ScreenRectangle(FrameBufferObject Fbo, ScreenRectangle ScreenRectangle)
        {
            ScreenRectangle.Vao.Bind();

            GL.Disable(EnableCap.DepthTest);                        // Desligamos o "Depth testing" para garantir que o desenho será feito sobre qualquer outro elemento da tela

            GL.BindTexture(TextureTarget.Texture2D, Fbo.TextureId);

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
