using OpenTK.Graphics.OpenGL;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;

namespace _3dCG.Examples.Basics
{
    internal class Chapter_04_HelloShader : GameWindow
    {

        private const int POSITION = 0;
        private readonly int[] OFFSET = { 0 };
        private const int VERTEX_SIZE = 3 * sizeof(float);

        private int _vertexBufferObject;
        private int _vertexArrayObject;

        private Shader _shader;

        public Chapter_04_HelloShader(
            GameWindowSettings gameWindowSettings,
            NativeWindowSettings nativeWindowSettings) :
            base(gameWindowSettings, nativeWindowSettings)
        {
            Title = "Hello Shader!";
        }

        protected override void OnLoad()
        {
            base.OnLoad();

            float[] _data =
            {
                -0.75f, -0.75f, 0.0f,
                0.75f , -0.75f, 0.0f,
                0.0f  , 0.75f , 0.0f
            };

            // Generate the buffer
            _vertexBufferObject = GL.GenBuffer();
            // Points to the active buffer
            GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject);
            // Insert the data into the buffer
            GL.BufferData(BufferTarget.ArrayBuffer, _data.Length * sizeof(float), _data, BufferUsageHint.StaticDraw);
            // Generate the array object buffer
            _vertexArrayObject = GL.GenVertexArray();
            // Points to the array object
            GL.BindVertexArray(_vertexArrayObject);
            // Creates an attribute pointer
            GL.VertexAttribPointer(POSITION, 3, VertexAttribPointerType.Float, false, VERTEX_SIZE, OFFSET[POSITION]);
            GL.EnableVertexAttribArray(POSITION);

            _shader = new Shader("HelloShader");

            GL.ClearColor(1.0f, 0.0f, 0.0f, 1.0f);
        }

        protected override void OnRenderFrame(FrameEventArgs args)
        {
            base.OnRenderFrame(args);

            GL.Clear(ClearBufferMask.ColorBufferBit);

            _shader.Use();

            GL.BindVertexArray(_vertexArrayObject);
            GL.DrawArrays(PrimitiveType.Triangles, 0, 3);
            GL.BindVertexArray(0);

            SwapBuffers();
        }

        protected override void OnUnload()
        {
            base.OnUnload();

            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.BindVertexArray(0);
            GL.DeleteBuffer(_vertexBufferObject);
            GL.DeleteVertexArray(_vertexArrayObject);
        }
    }
}
