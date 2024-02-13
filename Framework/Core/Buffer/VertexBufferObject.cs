using OpenTK.Graphics.OpenGL4;

namespace Framework.Core.Buffer
{
    public struct VertexBufferObject
    {
        int Handle;

        public VertexBufferObject(float[] vertices, BufferUsageHint usage = BufferUsageHint.StaticDraw)
        {
            Handle = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, Handle);
            GL.BufferData(BufferTarget.ArrayBuffer, sizeof(float) * vertices.Length, vertices, usage);
        }

        public void Bind()
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, Handle);
        }

        public void Unbind()
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
        }

        public void Delete()
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.DeleteBuffer(Handle);
        }
    }
}
