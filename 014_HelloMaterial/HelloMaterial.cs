// <copyright file="HelloMaterial.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Examples
{
    using ExamplesCommon;
    using Framework.Core;
    using Framework.Core.Camera;
    using Framework.Core.Light;
    using Framework.Core.Material;
    using Framework.Utils.GUI;
    using Framework.Utils.GUI.ViewLayer;
    using OpenTK.Graphics.OpenGL4;
    using OpenTK.Mathematics;
    using OpenTK.Windowing.Common;
    using OpenTK.Windowing.Desktop;
    using ShaderType = ExamplesCommon.ShaderType;

    /// <summary>
    /// Exemplo de como estruturas shaders e materiais.
    /// </summary>
    public class HelloMaterial : GameWindow
    {
        private FPSCounter fpsCounter;

        private ViewLayer view;

        private Dictionary<string, BasicMesh> meshes;
        private BasicMesh currentMesh;

        private Texture texture;
        private Transform transform;

        private Light light;
        private AmbientLight ambientLight;

        private BasicMaterial basicMaterial;

        private PerspectiveCamera camera;
        private CameraController cameraController;

        /// <summary>
        /// Initializes a new instance of the <see cref="HelloMaterial"/> class.
        /// </summary>
        /// <param name="gameWindowSettings">Configurações internas de janela.</param>
        /// <param name="nativeWindowSettings">Configurações nativas de janela.</param>
        public HelloMaterial(
            GameWindowSettings gameWindowSettings,
            NativeWindowSettings nativeWindowSettings)
            : base(gameWindowSettings, nativeWindowSettings)
        {
        }

        /// <inheritdoc/>
        protected override void OnLoad()
        {
            base.OnLoad();

            this.fpsCounter = new FPSCounter(this);

            // Modelos 3D
            this.meshes = new Dictionary<string, BasicMesh>
            {
                { "Monkey", new BasicMesh("Monkey.fbx") },
            };

            // Textura
            this.texture = Texture.LoadFromFile("Suzanne.png", TextureUnit.Texture0);

            // Transform
            this.transform = new Transform();
            this.transform.SetRotationY(3.14f);

            // Material
            MaterialFormat format = new MaterialFormat();
            format.AddFloat("roughness", 0.5f);
            format.AddFloat("specularIntensity", 1.0f);
            format.AddFloat("specularPower", 9.0f);
            format.AddVector3("specularColor", new System.Numerics.Vector3(0.9f, 0.9f, 1.0f));

#pragma warning disable CS0618 // Type or member is obsolete

            this.basicMaterial = new BasicMaterial(ShaderType.Phong, format);
            this.basicMaterial.GetUniformLocations();

#pragma warning restore CS0618 // Type or member is obsolete

            // Luz
            this.light = new Light(
                new System.Numerics.Vector3(2.0f, 2.0f, 2.0f),
                new System.Numerics.Vector3(1.0f, 1.0f, 1.0f),
                new System.Numerics.Vector3(0.0f, 0.0f, 0.0f),
                1.0f,
                false);

            this.light.GetUniformLocations(this.basicMaterial.Shader);

            this.ambientLight = new AmbientLight(
                new System.Numerics.Vector3(0.0f, 0.0f, 0.0f),
                1.0f);
            this.ambientLight.GetUniformLocations(this.basicMaterial.Shader);

            // Câmera
            this.camera = new PerspectiveCamera(Vector3.UnitZ * 1.5f, this.Size.X / (float)this.Size.Y);
            this.camera.GetUniformLocations(this.basicMaterial.Shader);

            this.cameraController = new CameraController(this.camera, this);

            // GUI
            this.view = new ViewLayer();

            this.view.Load(this);

            this.view.SetList(this.meshes.Keys.ToArray());

            // GUI de Luz
            LightView lightView = new LightView(this.light, this.ambientLight);
            this.view.LightView = lightView;

            // GUI de material
            MaterialView materialView = new MaterialView(this.basicMaterial);
            this.view.MaterialView = materialView;
        }

        /// <inheritdoc/>
        protected override void OnResize(ResizeEventArgs e)
        {
            base.OnResize(e);

            GL.Viewport(0, 0, this.ClientSize.X, this.ClientSize.Y);

            this.view.Resize(this.ClientSize.X, this.ClientSize.Y);
        }

        /// <inheritdoc/>
        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            GL.ClearColor(new Color4(0, 32, 48, 255));
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit | ClearBufferMask.StencilBufferBit);

            this.basicMaterial.Shader.Use();

            this.texture.Use(TextureUnit.Texture0);

            if (this.currentMesh != null)
            {
                this.currentMesh.Draw();
            }

            this.view.Render(e);

            ImGuiController.CheckGLError("End of frame");

            this.SwapBuffers();
        }

        /// <inheritdoc/>
        protected override void OnUpdateFrame(FrameEventArgs args)
        {
            base.OnUpdateFrame(args);

            this.fpsCounter.Update(args);

            this.view.Update(this, args);

            if (this.view.CurrentItem != this.view.PreviousItem)
            {
                this.ChangeMesh(this.view.CurrentItem);
            }

            if (this.view.Rotate)
            {
                this.transform.SetRotationX(this.view.Tick * this.view.RotationSpeed.X);
                this.transform.SetRotationY(this.view.Tick * this.view.RotationSpeed.Y);
                this.transform.SetRotationZ(this.view.Tick * this.view.RotationSpeed.Z);

                this.view.Tick += 0.01f;
            }

            this.light.UpdateUniforms();
            this.ambientLight.UpdateUniforms();
            this.basicMaterial.UpdateUniforms();

            this.camera.UpdateUniforms();

            this.basicMaterial.Shader.SetMatrix4("model", this.transform.GetModelMatrix());
            this.basicMaterial.Shader.SetMatrix4("view", this.camera.GetViewMatrix());
            this.basicMaterial.Shader.SetMatrix4("projection", this.camera.GetProjectionMatrix());

            this.cameraController.Update(args, this.KeyboardState, this.MouseState);
        }

        /// <inheritdoc/>
        protected override void OnUnload()
        {
            base.OnUnload();

            foreach (var mesh in this.meshes)
            {
                mesh.Value.Dispose();
            }

            this.texture.Dispose();
            this.basicMaterial.Dispose();

            this.view.Unload();
        }

        /// <inheritdoc/>
        protected override void OnTextInput(TextInputEventArgs e)
        {
            base.OnTextInput(e);

            this.view.GetController().PressChar((char)e.Unicode);
        }

        /// <inheritdoc/>
        protected override void OnMouseWheel(MouseWheelEventArgs e)
        {
            base.OnMouseWheel(e);

            this.cameraController.MouseUpdate(e);

            this.view.GetController().MouseScroll(e.Offset);
        }

        private void ChangeMesh(string item)
        {
            this.currentMesh = this.meshes.GetValueOrDefault(this.view.CurrentItem);
            this.view.PreviousItem = this.view.CurrentItem;
        }
    }
}
