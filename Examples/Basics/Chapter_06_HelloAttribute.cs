using OpenTK.Graphics.OpenGL;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;

namespace _3dCG.Examples.Basics
{
    internal class Chapter_06_HelloAttribute : GameWindow
    {

        private const int POSITION = 0;
        private const int COLOR = 1;
        private readonly int[] OFFSET = { 0, 12 };
        private const int VERTEX_SIZE = 6 * sizeof(float);

        private int _vertexBufferObject;
        private int _vertexArrayObject;

        private Shader _shader;

        public Chapter_06_HelloAttribute(
            GameWindowSettings gameWindowSettings,
            NativeWindowSettings nativeWindowSettings) :
            base(gameWindowSettings, nativeWindowSettings)
        {
            Title = "Hello Attribute!";
        }

        protected override void OnLoad()
        {
            base.OnLoad();

            float[] _data =
            {  // Position          // Color RGB
                -0.75f, -0.75f, 0.0f, 1.0f, 0.0f, 0.0f,
                0.75f , -0.75f, 0.0f, 0.0f, 1.0f, 0.0f,
                0.0f  , 0.75f , 0.0f, 0.0f, 0.0f, 1.0f,
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
            // Position attribute
            GL.VertexAttribPointer(POSITION, 3, VertexAttribPointerType.Float, false, VERTEX_SIZE, OFFSET[POSITION]);
            GL.EnableVertexAttribArray(POSITION);
            // Color attribute
            GL.VertexAttribPointer(COLOR, 3, VertexAttribPointerType.Float, false, VERTEX_SIZE, OFFSET[COLOR]);
            GL.EnableVertexAttribArray(COLOR);

            _shader = new Shader("HelloAttribute");

            GL.ClearColor(0.1f, 0.1f, 0.2f, 1.0f);
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
