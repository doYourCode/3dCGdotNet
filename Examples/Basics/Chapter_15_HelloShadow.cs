using Assimp;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;

namespace _3dCG.Examples.Basics
{
    internal class Chapter_15_HelloShadow : GameWindow
    {
        private float _tick = 0.0f;

        private Shader _shader;
        private Texture _texture;
        private BasicMesh _mesh;
        private Matrix4 _modelMatrix;
        private Camera _camera;
        private CameraController _cameraController;
        private Light _light;

        int _lightPositionLocation;
        int _lightDirectionLocation;
        int _lightColorLocation;

        public Chapter_15_HelloShadow(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings) : base(gameWindowSettings, nativeWindowSettings)
        {
            Title = "Hello Shadow!";
        }

        protected override void OnLoad()
        {
            base.OnLoad();

            GL.Enable(EnableCap.DepthTest);

            _shader = new Shader("HelloLight");

            _texture = Texture.LoadFromFile("Resources/Texture/Suzanne.png");

            _mesh = new BasicMesh("Resources/Mesh/Suzanne.obj");

            // We initialize the camera so that it is 3 units back from where the rectangle is.
            // We also give it the proper aspect ratio.
            _camera = new Camera(Vector3.UnitZ * 3, Size.X / (float)Size.Y);

            _cameraController = new CameraController(_camera, this);

            _light = new Light
            {
                Position = new Vector3D(1.0f, 1.0f, 1.0f),
                Direction = new Vector3D(1.0f, 1.0f, 1.0f),
                ColorDiffuse = new Color3D(1.0f, 1.0f, 1.0f)
            };

            _lightPositionLocation = GL.GetUniformLocation(_shader.Handle, "lightPosition");
            _lightDirectionLocation = GL.GetUniformLocation(_shader.Handle, "lightDirection");
            _lightColorLocation = GL.GetUniformLocation(_shader.Handle, "lightColor");

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

            // Uniforms update
            GL.Uniform3(_lightPositionLocation, _light.Position.X, _light.Position.Y, _light.Position.Z);
            GL.Uniform3(_lightDirectionLocation, _light.Direction.X, _light.Direction.Y, _light.Direction.Z);
            GL.Uniform3(_lightColorLocation, _light.ColorDiffuse.R, _light.ColorDiffuse.G, _light.ColorDiffuse.B);

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
