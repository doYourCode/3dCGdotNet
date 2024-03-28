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
        AmbientLight ambientLight;
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

            meshes.Add("Monkey", new BasicMesh("Monkey.fbx"));

            view.SetList(meshes.Keys.ToArray());

            texture = Texture.LoadFromFile("Suzanne.png", TextureUnit.Texture0);

            shader = new Shader("HelloLight");

            transform = new Transform();
            transform.SetRotationY(3.14f);

            light = new Light(
                new System.Numerics.Vector3(2.0f, 2.0f, 2.0f),
                new System.Numerics.Vector3(1.0f, 1.0f, 1.0f),
                new System.Numerics.Vector3(0.0f, 0.0f, 0.0f),
                1.0f,
                false
                );
            light.GetUniformLocations(shader);

            ambientLight = new AmbientLight(
                new System.Numerics.Vector3(0.0f, 0.0f, 0.0f),
                1.0f
                );
            ambientLight.GetUniformLocations(shader);

            lightView = new LightView(light, ambientLight);
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

            ambientLight.UpdateUniforms();

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

            shader.Dispose();
            texture.Dispose();

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
