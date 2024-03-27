using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Graphics.OpenGL4;

using Framework.Core;
using Framework.Utils.Common.Mesh;

namespace Examples
{
    internal class HelloMesh : GameWindow
    {
        private Shader shader;
        private Texture texture;
        private BasicMesh mesh;

        public HelloMesh(
            GameWindowSettings gameWindowSettings,
            NativeWindowSettings nativeWindowSettings) :
            base(gameWindowSettings, nativeWindowSettings) { }

        protected override void OnLoad()
        {
            base.OnLoad();

            GL.Enable(EnableCap.DepthTest);

            shader = new Shader("HelloMesh");

            texture = Texture.LoadFromFile("Resources/Texture/Suzanne.png", TextureUnit.Texture0);

            mesh = new BasicMesh("Resources/Mesh/Monkey.fbx");

            GL.ClearColor(0.1f, 0.1f, 0.2f, 1.0f);
        }

        protected override void OnRenderFrame(FrameEventArgs args)
        {
            base.OnRenderFrame(args);

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            // Bind em textura e shader
            texture.Use(TextureUnit.Texture0);
            shader.Use();
            // Dizer quais buffers vão ser desenhados c/ esse conjunto textura/shader
            mesh.Draw();

            SwapBuffers();
        }

        protected override void OnUnload()
        {
            base.OnUnload();

            mesh.Delete();
        }
    }
}
