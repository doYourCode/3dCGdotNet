using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using Framework.Core;
using Framework.Core.Camera;
using ExamplesCommon;

namespace Examples
{
    internal class HelloCamera : GameWindow
    {
        private float tick = 0.0f;

        private Shader shader;
        private Texture texture;
        private BasicMesh mesh;
        private Transform transform;
        private PerspectiveCamera camera;
        private CameraController cameraController;

        public HelloCamera(
            GameWindowSettings gameWindowSettings,
            NativeWindowSettings nativeWindowSettings) :
            base(gameWindowSettings, nativeWindowSettings) { }

        protected override void OnLoad()
        {
            base.OnLoad();

            GL.Enable(EnableCap.DepthTest);

            shader = new Shader("HelloCamera");

            texture = Texture.LoadFromFile("Suzanne.png", TextureUnit.Texture0);

            mesh = new BasicMesh("Monkey.fbx");

            transform = new Transform();

            // We initialize the camera so that it is 3 units back from where the rectangle is.
            // We also give it the proper aspect ratio.
            camera = new PerspectiveCamera(Vector3.UnitZ * 3, Size.X / (float)Size.Y);

            cameraController = new CameraController(camera, this);

            GL.ClearColor(0.1f, 0.1f, 0.2f, 1.0f);

            // We make the mouse cursor invisible and captured so we can have proper FPS-camera movement.
            CursorState = CursorState.Grabbed;
        }

        protected override void OnRenderFrame(FrameEventArgs args)
        {
            base.OnRenderFrame(args);

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            texture.Use(TextureUnit.Texture0);
            shader.Use();

            mesh.Draw();

            SwapBuffers();
        }

        protected override void OnUpdateFrame(FrameEventArgs args)
        {
            base.OnUpdateFrame(args);

            // Rotate the model matrix
            transform.SetRotationY(tick);
            // Identity matrix (per object)
            shader.SetMatrix4("model", transform.GetModelMatrix());
            // Camera matrices
            shader.SetMatrix4("view", camera.GetViewMatrix());
            shader.SetMatrix4("projection", camera.GetProjectionMatrix());

            tick += 0.01f;

            cameraController.Update(args, KeyboardState, MouseState);
        }

        protected override void OnMouseWheel(MouseWheelEventArgs e)
        {
            base.OnMouseWheel(e);

            cameraController.MouseUpdate(e);
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
