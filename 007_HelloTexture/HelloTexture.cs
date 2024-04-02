using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;

using Framework.Core;
using Framework.Core.Buffer;
using Framework.Core.Vertex;
using Framework.Utils;

namespace Examples
{
    internal class HelloTexture : GameWindow
    {
        VertexBufferObject vbo;
        VertexArrayObject vao;

        private Shader shader;

        private Texture texture;

        public HelloTexture(
            GameWindowSettings gameWindowSettings,
            NativeWindowSettings nativeWindowSettings) :
            base(gameWindowSettings, nativeWindowSettings) { }

        protected override void OnLoad()
        {
            base.OnLoad();

            float[] data =
            {  
            //  Position                Color                   Texture coordinates
            //  X       Y       Z       R       G       B       U       V
               -0.75f, -0.75f,  0.0f,   1.0f,   0.0f,   0.0f,   0.0f,   1.0f,
                0.75f, -0.75f,  0.0f,   0.0f,   1.0f,   0.0f,   1.0f,   1.0f,
                0.0f,   0.75f,  0.0f,   0.0f,   0.0f,   1.0f,   0.5f,   0.0f,
            };

            vbo = new VertexBufferObject(data);

            VertexFormat vertexFormat = new VertexFormat();
            vertexFormat.AddAttributesGroup(vbo, VertexAttributeType.Position, VertexAttributeType.Color, VertexAttributeType.TexCoord0);

            vao = new VertexArrayObject(vertexFormat);

            shader = new Shader("HelloTexture");

            texture = Texture.LoadFromFile("Uv_checker_01.png", TextureUnit.Texture0);

            GL.ClearColor(0.1f, 0.1f, 0.2f, 1.0f);
        }

        protected override void OnRenderFrame(FrameEventArgs args)
        {
            base.OnRenderFrame(args);

            GL.Clear(ClearBufferMask.ColorBufferBit);

            texture.Use(TextureUnit.Texture0);

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
            texture.Dispose();
        }
    }
}
