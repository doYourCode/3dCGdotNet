using OpenTK.Graphics.OpenGL;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;

namespace _3dCG.Examples.Basics
{
    internal class Chapter_09_HelloMesh : GameWindow
    {

        private Shader _shader;
        private Texture _texture;
        private BasicMesh _mesh;

        public Chapter_09_HelloMesh(
            GameWindowSettings gameWindowSettings,
            NativeWindowSettings nativeWindowSettings) :
            base(gameWindowSettings, nativeWindowSettings)
        {
            Title = "Hello Mesh!";
        }

        protected override void OnLoad()
        {
            base.OnLoad();

            GL.Enable(EnableCap.DepthTest);

            _shader = new Shader("HelloMesh");

            _texture = Texture.LoadFromFile("Resources/Texture/Suzanne.png", TextureUnit.Texture0);

            _mesh = new BasicMesh("Resources/Mesh/Suzanne.obj");

            GL.ClearColor(0.1f, 0.1f, 0.2f, 1.0f);
        }

        protected override void OnRenderFrame(FrameEventArgs args)
        {
            base.OnRenderFrame(args);

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            // Bind em textura e shader
            _texture.Use(TextureUnit.Texture0);
            _shader.Use();
            // Dizer quais buffers vão ser desenhados c/ esse conjunto textura/shader
            _mesh.Draw();

            SwapBuffers();
        }

        protected override void OnUnload()
        {
            base.OnUnload();

            _mesh.Delete();
        }
    }
}
