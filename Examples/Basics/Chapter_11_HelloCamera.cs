using Assimp;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;

namespace _3dCG.Examples.Basics
{
    internal class Chapter_11_HelloCamera : GameWindow
    {
        private float _tick = 0.0f;

        private Shader _shader;
        private Texture _texture;
        private BasicMesh _mesh;
        private Matrix4 _modelMatrix;
        private Camera _camera;
        private CameraController _cameraController;

        public Chapter_11_HelloCamera(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings) : base(gameWindowSettings, nativeWindowSettings)
        {
            Title = "Hello Camera!";
        }

        protected override void OnLoad()
        {
            base.OnLoad();

            GL.Enable(EnableCap.DepthTest);

            _shader = new Shader("HelloCamera");

            _texture = Texture.LoadFromFile("Resources/Texture/Suzanne.png");

            _mesh = new BasicMesh("Resources/Mesh/Suzanne.obj");

            // We initialize the camera so that it is 3 units back from where the rectangle is.
            // We also give it the proper aspect ratio.
            _camera = new Camera(Vector3.UnitZ * 3, Size.X / (float)Size.Y);

            _cameraController = new CameraController(_camera, this);

            GL.ClearColor(0.1f, 0.1f, 0.2f, 1.0f);

            // We make the mouse cursor invisible and captured so we can have proper FPS-camera movement.
            CursorState = CursorState.Grabbed;
        }

        protected override void OnRenderFrame(FrameEventArgs args)
        {
            base.OnRenderFrame(args);

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            _texture.Use(TextureUnit.Texture0);
            _shader.Use();

            _mesh.Draw();

            SwapBuffers();
        }

        protected override void OnUpdateFrame(FrameEventArgs args)
        {
            base.OnUpdateFrame(args);

            // Rotate the model matrix
            _modelMatrix = Matrix4.Identity * Matrix4.CreateRotationY((float)MathHelper.DegreesToRadians(_tick));
            // Identity matrix (per object)
            _shader.SetMatrix4("model", _modelMatrix);
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
