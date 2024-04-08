// <copyright file="ViewLayer.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Examples
{
    using Framework.Core.Resource;
    using Framework.Utils.GUI;
    using Framework.Utils.GUI.ViewLayer;
    using ImGuiNET;
    using OpenTK.Graphics.OpenGL4;
    using OpenTK.Windowing.Common;
    using OpenTK.Windowing.Desktop;

    /// <inheritdoc/>
    internal class ViewLayer : IViewLayer
    {
        private ImGuiController controller;

        private string[] items;

        private bool rotate;

        private System.Numerics.Vector3 rotationSpeed;

        /// <summary>
        /// Gets or Sets TODO.
        /// </summary>
        public LightView LightView { get; set; }

        /// <summary>
        /// Gets or Sets TODO.
        /// </summary>
        public MaterialView MaterialView { get; set; }

        /// <summary>
        /// Gets or Sets TODO.
        /// </summary>
        public string CurrentItem { get; set; } = string.Empty;

        /// <summary>
        /// Gets or Sets TODO.
        /// </summary>
        public string PreviousItem { get; set; } = string.Empty;

        /// <summary>
        /// Gets or Sets TODO.
        /// </summary>
        public float Tick { get; set; } = 0.0f;

        /// <summary>
        /// Gets or sets a value indicating whether TODO.
        /// </summary>
        public bool Rotate { get => this.rotate; set => this.rotate = value; }

        /// <summary>
        /// Gets or Sets TODO.
        /// </summary>
        public System.Numerics.Vector3 RotationSpeed { get => this.rotationSpeed; set => this.rotationSpeed = value; }

        /// <inheritdoc/>
        public void Load(GameWindow window)
        {
            this.controller = new ImGuiController(window.ClientSize.X, window.ClientSize.Y);
        }

        /// <inheritdoc/>
        public void Update(GameWindow window, FrameEventArgs args)
        {
            this.controller.Update(window, (float)args.Time);
        }

        /// <inheritdoc/>
        public void Render(FrameEventArgs e)
        {
            GL.Disable(EnableCap.DepthTest);

            this.RenderView();

            this.controller.Render();

            GL.Enable(EnableCap.DepthTest);
        }

        /// <inheritdoc/>
        public void Unload()
        {
            this.controller.Dispose();
        }

        /// <summary>
        /// TODO.
        /// </summary>
        /// <param name="width"> PARAM TODO. </param>
        /// <param name="height"> PARAM2 TODO. </param>
        public void Resize(int width, int height)
        {
            this.controller.WindowResized(width, height);
        }

        /// <inheritdoc/>
        public void RenderView()
        {
            ImGui.Begin("Configurações");

            ImGui.LabelText(string.Empty, "Select a mesh");

            ImGui.Spacing();

            if (ImGui.BeginCombo(string.Empty, this.CurrentItem))
            {
                for (int n = 0; n < this.items.Length; n++)
                {
                    bool is_selected = this.CurrentItem == this.items[n];

                    if (ImGui.Selectable(this.items[n], is_selected))
                    {
                        this.CurrentItem = this.items[n];
                    }

                    if (is_selected)
                    {
                        ImGui.SetItemDefaultFocus();
                    }
                }

                ImGui.EndCombo();
            }

            ImGui.Spacing();

            ImGui.Checkbox("Rotate mesh", ref this.rotate);

            if (this.Rotate)
            {
                ImGui.Spacing();

                ImGui.LabelText(string.Empty, "X         Y         Z");

                ImGui.DragFloat3("Rotation speed", ref this.rotationSpeed, 0.01f, -1.0f, 1.0f);

                if (ImGui.Button("Reset rotation"))
                {
                    this.Tick = 0.0f;
                }
            }

            ImGui.End();

            ImGui.Begin("VRAM Resources - Items(" + DisposableResource.DisposableResources.Keys.Count + ")");

            foreach (string res in DisposableResource.DisposableResources.Keys)
            {
                ImGui.Text(res);
            }

            ImGui.End();

            this.LightView.RenderResourceView();
            this.MaterialView.RenderResourceView();
        }

        /// <summary>
        /// TODO.
        /// </summary>
        /// <param name="items"> PARAM TODO. </param>
        public void SetList(string[] items)
        {
            this.items = items;
            this.CurrentItem = items[0];
        }

        /// <summary>
        /// TODO.
        /// </summary>
        /// <returns> RETURN TODO. </returns>
        public ImGuiController GetController()
        {
            return this.controller;
        }
    }
}
