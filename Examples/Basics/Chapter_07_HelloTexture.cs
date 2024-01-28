using OpenTK.Graphics.OpenGL;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;

namespace _3dCG.Examples.Basics
{
    internal class Chapter_07_HelloTexture : GameWindow
    {

        private const int POSITION = 0;
        private const int COLOR = 1;
        private const int UV = 2;
        private readonly int[] OFFSET = { 0, 12, 24 };
        private const int VERTEX_SIZE = 8 * sizeof(float);

        private int _vertexBufferObject;
        private int _vertexArrayObject;

        private Shader _shader;
        private Texture _texture;

        public Chapter_07_HelloTexture(
            GameWindowSettings gameWindowSettings,
            NativeWindowSettings nativeWindowSettings) :
            base(gameWindowSettings, nativeWindowSettings)
        {
            Title = "Hello Texture!";
        }

        protected override void OnLoad()
        {
            base.OnLoad();

            float[] _data =
            {  
                // Position          // Color          // Uv coords (texture coordinates)
                -0.75f, -0.75f, 0.0f, 1.0f, 0.0f, 0.0f, 0.0f, 1.0f,
                0.75f , -0.75f, 0.0f, 0.0f, 1.0f, 0.0f, 1.0f, 1.0f,
                0.0f  , 0.75f , 0.0f, 0.0f, 0.0f, 1.0f, 0.5f, 0.0f,
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
            // Texture coordinates attribute
            GL.VertexAttribPointer(UV, 2, VertexAttribPointerType.Float, false, VERTEX_SIZE, OFFSET[UV]);
            GL.EnableVertexAttribArray(UV);

            _shader = new Shader("HelloTexture");

            _texture = Texture.LoadFromFile("Resources/Texture/Uv_checker_01.png", TextureUnit.Texture0);

            GL.ClearColor(0.1f, 0.1f, 0.2f, 1.0f);
        }

        protected override void OnRenderFrame(FrameEventArgs args)
        {
            base.OnRenderFrame(args);

            GL.Clear(ClearBufferMask.ColorBufferBit);

            _texture.Use(TextureUnit.Texture0);
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
