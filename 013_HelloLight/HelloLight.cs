using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Graphics.OpenGL4;

using Framework.Utils.Common;
using Framework.Utils.Common.Mesh;
using Framework.Core;
using Framework.Utils.View;
using Framework.Core.Light;
using Framework.Core.Camera;

namespace Examples
{
    public class HelloLight : GameWindow
    {
        ViewLayer view;

        Dictionary<String, BasicMesh> meshes;
        BasicMesh currentMesh;

        Texture texture;
        Shader shader;
        Transform transform;

        Light light;
        LightView lightView;

        private PerspectiveCamera camera;

        public HelloLight(
            GameWindowSettings gameWindowSettings,
            NativeWindowSettings nativeWindowSettings) :
            base(gameWindowSettings, nativeWindowSettings)
        { }

        protected override void OnLoad()
        {
            base.OnLoad();

            view = new ViewLayer();

            view.Load(this);

            meshes = new Dictionary<String, BasicMesh>();

            meshes.Add("Cube", new BasicMesh("Resources/Mesh/Cube.fbx", true));
            meshes.Add("Icosahedron", new BasicMesh("Resources/Mesh/Icosahedron.fbx"));
            meshes.Add("Monkey", new BasicMesh("Resources/Mesh/Monkey.fbx"));
            meshes.Add("Sphere", new BasicMesh("Resources/Mesh/Sphere.fbx"));
            meshes.Add("Teapot", new BasicMesh("Resources/Mesh/Teapot.fbx"));
            meshes.Add("Torus", new BasicMesh("Resources/Mesh/Torus.fbx", true));

            view.SetList(meshes.Keys.ToArray());

            texture = Texture.LoadFromFile("Resources/Texture/Uv_checker_01.png", TextureUnit.Texture0);

            shader = new Shader("HelloLight");

            transform = new Transform();

            light = new Light(
                new System.Numerics.Vector3(2.0f, 2.0f, 2.0f));
            light.GetUniformLocations(shader);

            lightView = new LightView(light);
            view.LightView = lightView;

            camera = new PerspectiveCamera(Vector3.UnitZ * 1.5f, Size.X / (float)Size.Y);
            camera.GetUniformLocations(shader);
        }

        protected override void OnResize(ResizeEventArgs e)
        {
            base.OnResize(e);

            GL.Viewport(0, 0, ClientSize.X, ClientSize.Y);

            view.Resize(ClientSize.X, ClientSize.Y);
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            GL.ClearColor(new Color4(0, 32, 48, 255));
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit | ClearBufferMask.StencilBufferBit);

            shader.Use();

            texture.Use(TextureUnit.Texture0);

            if (currentMesh != null)
                currentMesh.Draw();

            view.Render();

            lightView.DrawControl();

            ImGuiController.CheckGLError("End of frame");

            SwapBuffers();
        }

        protected override void OnUpdateFrame(FrameEventArgs args)
        {
            base.OnUpdateFrame(args);

            view.Update(this, args);

            if (view.CurrentItem != view.PreviousItem)
                ChangeMesh(view.CurrentItem);

            if (view.Rotate)
            {
                transform.SetRotationX(view.Tick * view.RotationSpeed.X);
                transform.SetRotationY(view.Tick * view.RotationSpeed.Y);
                transform.SetRotationZ(view.Tick * view.RotationSpeed.Z);

                view.Tick += 0.01f;
            }

            light.UpdateUniforms();

            camera.UpdateUniforms();

            shader.SetMatrix4("model", transform.GetModelMatrix());

            shader.SetMatrix4("view", camera.GetViewMatrix());
            shader.SetMatrix4("projection", camera.GetProjectionMatrix());
        }

        protected override void OnUnload()
        {
            base.OnUnload();

            foreach (var mesh in meshes)
            {
                mesh.Value.Delete();
            }

            view.UnLoad();
        }

        protected override void OnTextInput(TextInputEventArgs e)
        {
            base.OnTextInput(e);

            view.GetController().PressChar((char)e.Unicode);
        }

        protected override void OnMouseWheel(MouseWheelEventArgs e)
        {
            base.OnMouseWheel(e);

            view.GetController().MouseScroll(e.Offset);
        }

        private void ChangeMesh(String item)
        {
            currentMesh = meshes.GetValueOrDefault(view.CurrentItem);
            view.PreviousItem = view.CurrentItem;
        }
    }
}
