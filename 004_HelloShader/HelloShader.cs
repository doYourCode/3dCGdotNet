using OpenTK.Graphics.OpenGL;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;

using Framework.Core;
using Framework.Core.Buffer;
using Framework.Core.Vertex;
using Framework.Utils;

namespace Examples
{
    internal class HelloShader : GameWindow
    {
        VertexBufferObject vbo;
        VertexArrayObject vao;

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

            vbo = new VertexBufferObject(data);

            VertexFormat vertexFormat = new VertexFormat();
            vertexFormat.AddAttribute(vbo, VertexAttributeType.Position);

            vao = new VertexArrayObject(vertexFormat);

            shader = new Shader("HelloShader");

            GL.ClearColor(1.0f, 0.0f, 0.0f, 1.0f);
        }

        protected override void OnRenderFrame(FrameEventArgs args)
        {
            base.OnRenderFrame(args);

            GL.Clear(ClearBufferMask.ColorBufferBit);

            shader.Use();

            Draw.Triangles(vao, 0, 3);

            SwapBuffers();
        }

        protected override void OnUnload()
        {
            base.OnUnload();

            vbo.Dispose();
            vao.Dispose();
            shader.Dispose();
        }
    }
}
