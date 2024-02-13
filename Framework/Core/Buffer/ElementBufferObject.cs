using OpenTK.Graphics.OpenGL4;

namespace Framework.Core.Buffer
{
    public struct ElementBufferObject
    {
        int Handle;

        public ElementBufferObject(int[] indices, BufferUsageHint usage = BufferUsageHint.StaticDraw)
        {
            Handle = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, Handle);
            GL.BufferData(BufferTarget.ElementArrayBuffer, sizeof(int) * indices.Length, indices, usage);
        }

        public void Bind()
        {
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, Handle);
        }

        public void Unbind()
        {
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0);
        }

        public void Delete()
        {
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0);
            GL.DeleteBuffer(Handle);
        }
    }
}
