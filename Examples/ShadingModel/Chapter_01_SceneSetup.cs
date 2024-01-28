using _3dCG.Core;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;

namespace _3dCG.Examples.ShadingModel
{
    internal class Chapter_01_SceneSetup : GameWindow
    {
        private float _tick = 0.0f;

        private Shader _shader;
        private BasicMesh _mesh;
        private Transform _transform;
        private Camera _camera;
        private CameraController _cameraController;
        private Core.Light _light;

        int _lightPositionLocation;
        int _lightDirectionLocation;
        int _lightColorLocation;
        int _viewPositionLocation;

        public Chapter_01_SceneSetup(
            GameWindowSettings gameWindowSettings,
            NativeWindowSettings nativeWindowSettings) :
            base(gameWindowSettings, nativeWindowSettings)
        {
            Title = "Hello Light!";
        }

        protected override void OnLoad()
        {
            base.OnLoad();

            GL.Enable(EnableCap.DepthTest);

            _shader = new Shader("UniformColor");

            _mesh = new BasicMesh("Resources/Mesh/ShaderBall.obj");

            _transform = new Transform();

            // We initialize the camera so that it is 3 units back from where the rectangle is.
            // We also give it the proper aspect ratio.
            _camera = new Camera(Vector3.UnitZ * 3, Size.X / (float)Size.Y);

            _cameraController = new CameraController(_camera, this);

            _light = new Light(Vector3.One, Vector3.One, Vector3.One, 1.0f, 0.0f);

            _lightPositionLocation = GL.GetUniformLocation(_shader.Handle, "lightPosition");
            _lightDirectionLocation = GL.GetUniformLocation(_shader.Handle, "lightDirection");
            _lightColorLocation = GL.GetUniformLocation(_shader.Handle, "lightColor");
            _viewPositionLocation = GL.GetUniformLocation(_shader.Handle, "viewPosition");

            GL.ClearColor(0.1f, 0.1f, 0.2f, 1.0f);

            // We make the mouse cursor invisible and captured so we can have proper FPS-camera movement.
            CursorState = CursorState.Grabbed;
        }

        protected override void OnRenderFrame(FrameEventArgs args)
        {
            base.OnRenderFrame(args);

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            _shader.Use();

            // Uniforms update
            GL.Uniform3(_lightPositionLocation, _light.Position);
            GL.Uniform3(_lightDirectionLocation, _light.Direction);
            GL.Uniform3(_lightColorLocation, _light.Color);
            GL.Uniform3(_viewPositionLocation, _camera.Position);

            _mesh.Draw();

            SwapBuffers();
        }

        protected override void OnUpdateFrame(FrameEventArgs args)
        {
            base.OnUpdateFrame(args);

            // Rotate the model matrix
            _transform.SetRotationY(_tick);
            // Identity matrix (per object)
            _shader.SetMatrix4("model", _transform.GetModelMatrix());
            // Camera matrices
            _shader.SetMatrix4("view", _camera.GetViewMatrix());
            _shader.SetMatrix4("projection", _camera.GetProjectionMatrix());

            _tick += 0.01f;

            _cameraController.Update(args, KeyboardState, MouseState);
        }

        protected override void OnMouseWheel(MouseWheelEventArgs e)
        {
            base.OnMouseWheel(e);

            _cameraController.MouseUpdate(e);
        }

        protected override void OnUnload()
        {
            base.OnUnload();

            _mesh.Delete();
        }
    }
}
