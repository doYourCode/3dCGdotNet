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
            Vao.Bind();

            GL.PointSize(pointSize);

            GL.DrawArrays(PrimitiveType.Points, first, count);

            Vao.Unbind();
        }

        public static void Lines(VertexArrayObject Vao, int first, int count, int thickness)
        {
            Vao.Bind();

            GL.LineWidth(thickness);

            GL.DrawArrays(PrimitiveType.LineLoop, first, count);

            Vao.Unbind();
        }

        public static void Triangles(VertexArrayObject Vao, int first, int count)
        {
            Vao.Bind();

            GL.DrawArrays(PrimitiveType.Triangles, first, count);

            Vao.Unbind();
        }

        public static void Elements(VertexArrayObject Vao, int indexCount)
        {
            Vao.Bind(); Console.WriteLine("INDEX SIZE: " + indexCount);

            GL.DrawElements(BeginMode.Triangles, indexCount, DrawElementsType.UnsignedInt, 0);

            Vao.Unbind();
        }
    }
}
