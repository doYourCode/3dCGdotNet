// <copyright file="HelloGUI.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Examples
{
    using ExamplesCommon;
    using Framework.Core;
    using Framework.Utils.GUI;
    using OpenTK.Graphics.OpenGL4;
    using OpenTK.Mathematics;
    using OpenTK.Windowing.Common;
    using OpenTK.Windowing.Desktop;

    /// <inheritdoc/>
    public class HelloGUI : GameWindow
    {
        private FPSCounter fpsCounter;

        private ViewLayer view;

        private Dictionary<string, BasicMesh> meshes;
        private BasicMesh currentMesh;

        private Texture texture;
        private Shader shader;
        private Transform transform;

        /// <summary>
        /// Initializes a new instance of the <see cref="HelloGUI"/> class.
        /// </summary>
        /// <param name="gameWindowSettings"> PARAM TODO. </param>
        /// <param name="nativeWindowSettings"> PARAM2 TODO. </param>
        public HelloGUI(
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

            this.view = new ViewLayer();

            this.view.Load(this);

            this.meshes = new Dictionary<string, BasicMesh>
            {
                { "Cube", new BasicMesh("Cube.fbx", true) },
                { "Icosahedron", new BasicMesh("Icosahedron.fbx") },
                { "Monkey", new BasicMesh("Monkey.fbx") },
                { "Sphere", new BasicMesh("Sphere.fbx") },
                { "Teapot", new BasicMesh("Teapot.fbx") },
                { "Torus", new BasicMesh("Torus.fbx", true) },
            };

            this.view.SetList(this.meshes.Keys.ToArray());

            this.texture = Texture.LoadFromFile("Uv_checker_01.png", TextureUnit.Texture0);

            this.shader = new Shader("HelloTransformation");

            this.transform = new Transform();
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

            this.shader.Use();

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

            this.shader.SetMatrix4("model", this.transform.GetModelMatrix());
        }

        /// <inheritdoc/>
        protected override void OnUnload()
        {
            base.OnUnload();

            foreach (var mesh in this.meshes)
            {
                mesh.Value.Dispose();
            }

            this.shader.Dispose();
            this.texture.Dispose();

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

            this.view.GetController().MouseScroll(e.Offset);
        }

        private void ChangeMesh(string item)
        {
            this.currentMesh = this.meshes.GetValueOrDefault(this.view.CurrentItem);
            this.view.PreviousItem = this.view.CurrentItem;
        }
    }
}
