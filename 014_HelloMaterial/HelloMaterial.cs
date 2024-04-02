using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Graphics.OpenGL4;
using ExamplesCommon;
using Framework.Core;
using Framework.Core.Light;
using Framework.Core.Camera;
using ShaderType = ExamplesCommon.ShaderType;
using Framework.Utils.GUI;
using Framework.Utils.GUI.ViewLayer;
using Framework.Core.Material;

namespace Examples
{
    public class HelloMaterial : GameWindow
    {
        ViewLayer view;

        Dictionary<String, BasicMesh> meshes;
        BasicMesh currentMesh;

        Texture texture;
        Transform transform;

        Light light;
        AmbientLight ambientLight;

        BasicMaterial basicMaterial;

        private PerspectiveCamera camera;
        private CameraController cameraController;

        public HelloMaterial(
            GameWindowSettings gameWindowSettings,
            NativeWindowSettings nativeWindowSettings) :
            base(gameWindowSettings, nativeWindowSettings)
        { }

        protected override void OnLoad()
        {
            base.OnLoad();

            // Modelos 3D
            meshes = new Dictionary<String, BasicMesh>
            {
                { "Monkey", new BasicMesh("Monkey.fbx") }
            };


            // Textura
            texture = Texture.LoadFromFile("Suzanne.png", TextureUnit.Texture0);


            // Transform
            transform = new Transform();
            transform.SetRotationY(3.14f);


            // Material
            MaterialFormat format = new MaterialFormat();
            format.AddFloat("roughness", 0.5f);
            format.AddFloat("specularIntensity", 1.0f);
            format.AddFloat("specularPower", 9.0f);
            format.AddVector3("specularColor", new System.Numerics.Vector3(0.9f, 0.9f, 1.0f));

            basicMaterial = new BasicMaterial(ShaderType.Phong, format);
            basicMaterial.GetUniformLocations();

            // Luz
            light = new Light(
                new System.Numerics.Vector3(2.0f, 2.0f, 2.0f),
                new System.Numerics.Vector3(1.0f, 1.0f, 1.0f),
                new System.Numerics.Vector3(0.0f, 0.0f, 0.0f),
                1.0f,
                false
                );
            light.GetUniformLocations(basicMaterial.Shader);

            ambientLight = new AmbientLight(
                new System.Numerics.Vector3(0.0f, 0.0f, 0.0f),
                1.0f
                );
            ambientLight.GetUniformLocations(basicMaterial.Shader);


            // Câmera
            camera = new PerspectiveCamera(Vector3.UnitZ * 1.5f, Size.X / (float)Size.Y);
            camera.GetUniformLocations(basicMaterial.Shader);

            cameraController = new CameraController(camera, this);


            // GUI
            view = new ViewLayer();

            view.Load(this);

            view.SetList(meshes.Keys.ToArray());

            // GUI de Luz
            LightView lightView = new LightView(light, ambientLight);
            view.LightView = lightView;

            // GUI de material
            MaterialView materialView = new MaterialView(basicMaterial);
            view.MaterialView = materialView;
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

            basicMaterial.Shader.Use();

            texture.Use(TextureUnit.Texture0);

            if (currentMesh != null)
                currentMesh.Draw();

            view.Render(e);

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
            basicMaterial.UpdateUniforms();

            camera.UpdateUniforms();

            basicMaterial.Shader.SetMatrix4("model", transform.GetModelMatrix());
            basicMaterial.Shader.SetMatrix4("view", camera.GetViewMatrix());
            basicMaterial.Shader.SetMatrix4("projection", camera.GetProjectionMatrix());

            cameraController.Update(args, KeyboardState, MouseState);
        }

        protected override void OnUnload()
        {
            base.OnUnload();

            foreach (var mesh in meshes)
            {
                mesh.Value.Dispose();
            }

            texture.Dispose();
            basicMaterial.Dispose();

            view.Unload();
        }

        protected override void OnTextInput(TextInputEventArgs e)
        {
            base.OnTextInput(e);

            view.GetController().PressChar((char)e.Unicode);
        }

        protected override void OnMouseWheel(MouseWheelEventArgs e)
        {
            base.OnMouseWheel(e);

            cameraController.MouseUpdate(e);

            view.GetController().MouseScroll(e.Offset);
        }

        private void ChangeMesh(String item)
        {
            currentMesh = meshes.GetValueOrDefault(view.CurrentItem);
            view.PreviousItem = view.CurrentItem;
        }
    }
}
