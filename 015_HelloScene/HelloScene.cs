// <copyright file="HelloScene.cs" company="PlaceholderCompany">
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
    using OpenTK.Graphics.OpenGL4;
    using OpenTK.Mathematics;
    using OpenTK.Windowing.Common;
    using OpenTK.Windowing.Desktop;
    using ShaderType = ExamplesCommon.ShaderType;

    /// <summary>
    /// Exemplo de como estruturas shaders e materiais.
    /// </summary>
    public class HelloScene : GameWindow
    {
        private BasicScene scene;

        private Texture texture;
        private Transform transform;

        private Light light;
        private AmbientLight ambientLight;

        private BasicMaterial basicMaterial;

        private PerspectiveCamera camera;
        private CameraController cameraController;

        /// <summary>
        /// Initializes a new instance of the <see cref="HelloScene"/> class.
        /// </summary>
        /// <param name="gameWindowSettings">Configurações internas de janela.</param>
        /// <param name="nativeWindowSettings">Configurações nativas de janela.</param>
        public HelloScene(
            GameWindowSettings gameWindowSettings,
            NativeWindowSettings nativeWindowSettings)
            : base(gameWindowSettings, nativeWindowSettings)
        {
        }

        /// <inheritdoc/>
        protected override void OnLoad()
        {
            base.OnLoad();

            // Cena
            this.scene = new BasicScene("test.fbx");

            // Textura
            this.texture = Texture.LoadFromFile("Uv_checker_01.png", TextureUnit.Texture0);

            // Transform
            this.transform = new Transform();
            this.transform.SetRotationY(3.14f);

            // Material
            MaterialFormat format = new MaterialFormat();
            format.AddFloat("roughness", 0.5f);
            format.AddFloat("specularIntensity", 1.0f);
            format.AddFloat("specularPower", 9.0f);
            format.AddVector3("specularColor", new System.Numerics.Vector3(0.9f, 0.9f, 1.0f));

            this.basicMaterial = new BasicMaterial(ShaderType.Phong, format);
            this.basicMaterial.GetUniformLocations();

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
        }

        /// <inheritdoc/>
        protected override void OnResize(ResizeEventArgs e)
        {
            base.OnResize(e);

            GL.Viewport(0, 0, this.ClientSize.X, this.ClientSize.Y);
        }

        /// <inheritdoc/>
        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            GL.ClearColor(new Color4(0, 32, 48, 255));
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit | ClearBufferMask.StencilBufferBit);

            this.basicMaterial.Shader.Use();

            this.texture.Use(TextureUnit.Texture0);

            this.scene.Draw();

            ImGuiController.CheckGLError("End of frame");

            this.SwapBuffers();
        }

        /// <inheritdoc/>
        protected override void OnUpdateFrame(FrameEventArgs args)
        {
            base.OnUpdateFrame(args);

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

            this.texture.Dispose();
            this.basicMaterial.Dispose();
        }

        /// <inheritdoc/>
        protected override void OnMouseWheel(MouseWheelEventArgs e)
        {
            base.OnMouseWheel(e);

            this.cameraController.MouseUpdate(e);
        }
    }
}
