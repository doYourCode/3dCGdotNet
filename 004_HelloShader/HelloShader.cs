using OpenTK.Graphics.OpenGL;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;

using Framework.Core;

namespace Examples
{
    internal class HelloShader : GameWindow
    {
        private const int POSITION = 0;
        private readonly int[] OFFSET = { 0 };
        private const int VERTEX_SIZE = 3 * sizeof(float);

        private int vertexBufferObject;
        private int vertexArrayObject;

        private Shader shader;

        public HelloShader(
            GameWindowSettings gameWindowSettings,
            NativeWindowSettings nativeWindowSettings) :
            base(gameWindowSettings, nativeWindowSettings) { }

        protected override void OnLoad()
        {
            base.OnLoad();

            float[] data =
            {
                -0.75f, -0.75f, 0.0f,
                0.75f , -0.75f, 0.0f,
                0.0f  , 0.75f , 0.0f
            };


            vertexBufferObject = GL.GenBuffer();

            GL.BindBuffer(BufferTarget.ArrayBuffer, vertexBufferObject);

            GL.BufferData(BufferTarget.ArrayBuffer, data.Length * sizeof(float), data, BufferUsageHint.StaticDraw);

            vertexArrayObject = GL.GenVertexArray();

            GL.BindVertexArray(vertexArrayObject);

            GL.VertexAttribPointer(POSITION, 3, VertexAttribPointerType.Float, false, VERTEX_SIZE, OFFSET[POSITION]);
            GL.EnableVertexAttribArray(POSITION);

            shader = new Shader("HelloShader");

            GL.ClearColor(1.0f, 0.0f, 0.0f, 1.0f);
        }

        protected override void OnRenderFrame(FrameEventArgs args)
        {
            base.OnRenderFrame(args);

            GL.Clear(ClearBufferMask.ColorBufferBit);

            shader.Use();

            GL.BindVertexArray(vertexArrayObject);
            GL.DrawArrays(PrimitiveType.Triangles, 0, 3);
            GL.BindVertexArray(0);

            SwapBuffers();
        }

        protected override void OnUnload()
        {
            base.OnUnload();

            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.BindVertexArray(0);
            GL.DeleteBuffer(vertexBufferObject);
            GL.DeleteVertexArray(vertexArrayObject);
        }
    }
}
