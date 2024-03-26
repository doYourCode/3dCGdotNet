using Framework.Core.Buffer;
using OpenTK.Graphics.OpenGL4;

namespace Framework.Utils
{
    /// <summary>
    /// Funções de utilidade para desenho de forma genérica.
    /// </summary>
    public static class Draw
    {
        public static void Points(VertexArrayObject Vao, int first, int count, int pointSize)
        {
            GL.PointSize(pointSize);

            Vao.Bind();

            GL.DrawArrays(PrimitiveType.Points, first, count);
        }

        public static void Lines(VertexArrayObject Vao, int first, int count, int thickness)
        {
            GL.LineWidth(thickness);

            Vao.Bind();

            GL.DrawArrays(PrimitiveType.LineLoop, first, count);
        }

        public static void Triangles(VertexArrayObject Vao, int first, int count)
        {
            Vao.Bind();

            GL.DrawArrays(PrimitiveType.Triangles, first, count);
        }

        public static void Elements(VertexArrayObject Vao, int indexCount)
        {
            Vao.Bind();

            GL.DrawElements(BeginMode.Triangles, indexCount, DrawElementsType.UnsignedInt, 0);
        }
    }
}
