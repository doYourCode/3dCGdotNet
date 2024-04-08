// <copyright file="HelloCamera.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Examples
{
    using ExamplesCommon;
    using Framework.Core;
    using Framework.Core.Camera;
    using Framework.Utils.GUI;
    using OpenTK.Graphics.OpenGL4;
    using OpenTK.Mathematics;
    using OpenTK.Windowing.Common;
    using OpenTK.Windowing.Desktop;

    /// <inheritdoc/>
    internal class HelloCamera : GameWindow
    {
        private FPSCounter fpsCounter;

        private float tick = 0.0f;

        private Shader shader;
        private Texture texture;
        private BasicMesh mesh;
        private Transform transform;
        private PerspectiveCamera camera;
        private CameraController cameraController;

        /// <summary>
        /// Initializes a new instance of the <see cref="HelloCamera"/> class.
        /// </summary>
        /// <param name="gameWindowSettings"> PARAM TODO. </param>
        /// <param name="nativeWindowSettings"> PARAM2 TODO. </param>
        public HelloCamera(
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

            GL.Enable(EnableCap.DepthTest);

            this.shader = new Shader("HelloCamera");

            this.texture = Texture.LoadFromFile("Suzanne.png", TextureUnit.Texture0);

            this.mesh = new BasicMesh("Monkey.fbx");

            this.transform = new Transform();

            // We initialize the camera so that it is 3 units back from where the rectangle is.
            // We also give it the proper aspect ratio.
            this.camera = new PerspectiveCamera(Vector3.UnitZ * 3, this.Size.X / (float)this.Size.Y);

            this.cameraController = new CameraController(this.camera, this);

            GL.ClearColor(0.1f, 0.1f, 0.2f, 1.0f);

            // We make the mouse cursor invisible and captured so we can have proper FPS-camera movement.
            this.CursorState = CursorState.Grabbed;
        }

        /// <inheritdoc/>
        protected override void OnRenderFrame(FrameEventArgs args)
        {
            base.OnRenderFrame(args);

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            this.texture.Use(TextureUnit.Texture0);
            this.shader.Use();

            this.mesh.Draw();

            this.SwapBuffers();
        }

        /// <inheritdoc/>
        protected override void OnUpdateFrame(FrameEventArgs args)
        {
            base.OnUpdateFrame(args);

            this.fpsCounter.Update(args);

            // Rotate the model matrix
            this.transform.SetRotationY(this.tick);

            // Identity matrix (per object)
            this.shader.SetMatrix4("model", this.transform.GetModelMatrix());

            // Camera matrices
            this.shader.SetMatrix4("view", this.camera.GetViewMatrix());
            this.shader.SetMatrix4("projection", this.camera.GetProjectionMatrix());

            this.tick += 0.01f;

            this.cameraController.Update(args, this.KeyboardState, this.MouseState);
        }

        /// <inheritdoc/>
        protected override void OnMouseWheel(MouseWheelEventArgs e)
        {
            base.OnMouseWheel(e);

            this.cameraController.MouseUpdate(e);
        }

        /// <inheritdoc/>
        protected override void OnUnload()
        {
            base.OnUnload();

            this.mesh.Dispose();
            this.shader.Dispose();
            this.texture.Dispose();
        }
    }
}
