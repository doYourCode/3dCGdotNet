using OpenTK.Graphics.OpenGL4;

namespace Framework.Core.Buffer
{
    public struct VertexArrayObject
    {
        int Handle;

        public VertexArrayObject()
        {
            Handle = GL.GenVertexArray();
        }

        public void LinkVBO(VertexBufferObject vbo, int layout)
        {
            vbo.Bind();

            GL.VertexAttribPointer(layout, 3, VertexAttribPointerType.Float, false, 0, 0);
            GL.EnableVertexAttribArray(layout);

            vbo.Unbind();
        }

        public void Bind()
        {
            GL.BindVertexArray(Handle);
        }

        public void Unbind()
        {
            GL.BindVertexArray(0);
        }

        public void Delete()
        {
            GL.BindVertexArray(0);
            GL.DeleteVertexArray(Handle);
        }
    }
}
