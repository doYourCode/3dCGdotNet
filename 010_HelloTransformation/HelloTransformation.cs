using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Graphics.OpenGL4;
using Framework.Core;
using ExamplesCommon;

namespace Examples
{
    internal class HelloTransformation : GameWindow
    {
        private float tick = 0.0f;
        private float speed = 100.0f;

        private Shader shader;
        private Texture texture;
        private BasicMesh mesh;
        private Transform transform;

        public HelloTransformation(
            GameWindowSettings gameWindowSettings,
            NativeWindowSettings nativeWindowSettings) :
            base(gameWindowSettings, nativeWindowSettings)
        {
            Title = "Hello Transformation!";
        }

        protected override void OnLoad()
        {
            base.OnLoad();

            GL.Enable(EnableCap.DepthTest);

            shader = new Shader("HelloTransformation");

            texture = Texture.LoadFromFile("Suzanne.png", TextureUnit.Texture0);

            mesh = new BasicMesh("Monkey.fbx");

            transform = new Transform();

            GL.ClearColor(0.1f, 0.1f, 0.2f, 1.0f);
        }

        protected override void OnRenderFrame(FrameEventArgs args)
        {
            base.OnRenderFrame(args);

            // Limpar a tela antes de desenhar (usando a clear color)
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            texture.Use(TextureUnit.Texture0);
            shader.Use();

            mesh.Draw();

            SwapBuffers();
        }

        protected override void OnUpdateFrame(FrameEventArgs args)
        {
            base.OnUpdateFrame(args);

            // Rotate the model matrix (experimente alterar esses valores)
            transform.SetRotationY((float)System.Math.Cos(tick));
            // P/ rotacionar nos outros eixos descomente as linhas abaixo
            //_transform.SetRotationX(_tick);
            //_transform.SetRotationZ(_tick);

            // Identity matrix (per object)
            shader.SetMatrix4("model", transform.GetModelMatrix());

            tick += 0.0001f * speed;
        }

        protected override void OnUnload()
        {
            base.OnUnload();

            mesh.Dispose();
            shader.Dispose();
            texture.Dispose();
        }
    }
}
