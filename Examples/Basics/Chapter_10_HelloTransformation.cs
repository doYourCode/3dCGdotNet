using _3dCG.Core;
using OpenTK.Graphics.OpenGL;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;

namespace _3dCG.Examples.Basics
{
    internal class Chapter_10_HelloTransformation : GameWindow
    {
        private float _tick = 0.0f;
        private float _speed = 100.0f;

        private Shader _shader;
        private Texture _texture;
        private BasicMesh _mesh;
        private Transform _transform;

        public Chapter_10_HelloTransformation(
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

            _shader = new Shader("HelloTransformation");

            _texture = Texture.LoadFromFile("Resources/Texture/Suzanne.png", TextureUnit.Texture0);

            _mesh = new BasicMesh("Resources/Mesh/Suzanne.obj");

            _transform = new Transform();

            GL.ClearColor(0.1f, 0.1f, 0.2f, 1.0f);
        }

        protected override void OnRenderFrame(FrameEventArgs args)
        {
            base.OnRenderFrame(args);

            // Limpar a tela antes de desenhar (usando a clear color)
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            _texture.Use(TextureUnit.Texture0);
            _shader.Use();

            _mesh.Draw();

            SwapBuffers();
        }

        protected override void OnUpdateFrame(FrameEventArgs args)
        {
            base.OnUpdateFrame(args);

            // Rotate the model matrix (experimente alterar esses valores)
            _transform.SetRotationY((float)System.Math.Cos(_tick));
            // P/ rotacionar nos outros eixos descomente as linhas abaixo
            //_transform.SetRotationX(_tick);
            //_transform.SetRotationZ(_tick);

            // Identity matrix (per object)
            _shader.SetMatrix4("model", _transform.GetModelMatrix());

            _tick += 0.0001f * _speed;
        }

        protected override void OnUnload()
        {
            base.OnUnload();

            _mesh.Delete();
        }
    }
}
