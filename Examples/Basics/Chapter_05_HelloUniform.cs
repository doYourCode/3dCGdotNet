using OpenTK.Graphics.OpenGL;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;

namespace _3dCG.Examples.Basics
{
    internal class Chapter_05_HelloUniform : GameWindow
    {

        private const int POSITION = 0;
        private readonly int[] OFFSET = { 0 };
        private const int VERTEX_SIZE = 3 * sizeof(float);

        private int _vertexBufferObject;
        private int _vertexArrayObject;

        private Shader _shader;

        // Que para criar uma uniform são necessárias 2 variáveis (1 p valor e outra p/ endereço na vram)
        private int _tickUniformLocation;
        private float _tick = 0.0f;

        public Chapter_05_HelloUniform(
            GameWindowSettings gameWindowSettings,
            NativeWindowSettings nativeWindowSettings) :
            base(gameWindowSettings, nativeWindowSettings)
        {
            Title = "Hello Uniform!";
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

            _shader = new Shader("HelloUniform");

            _tickUniformLocation = GL.GetUniformLocation(_shader.Handle, "tick");

            GL.ClearColor(1.0f, 0.0f, 0.0f, 1.0f);
        }

        protected override void OnRenderFrame(FrameEventArgs args)
        {
            base.OnRenderFrame(args);

            GL.Clear(ClearBufferMask.ColorBufferBit);

            _shader.Use();

            GL.Uniform1(_tickUniformLocation, _tick);

            GL.BindVertexArray(_vertexArrayObject);
            GL.DrawArrays(PrimitiveType.Triangles, 0, 3);
            GL.BindVertexArray(0);

            SwapBuffers();
        }

        protected override void OnUpdateFrame(FrameEventArgs args)
        {
            base.OnUpdateFrame(args);

            _tick += 0.01f;
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
