// <copyright file="MaterialView.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Framework.Utils.GUI.ViewLayer
{
    using Framework.Core.Material;
    using ImGuiNET;

    /// <summary>
    /// Draws a GUI made specifically for a Material resource.
    /// </summary>
    public class MaterialView : IimResourceView
    {
        private Material material;

        private bool useMaps = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="MaterialView"/> class.
        /// </summary>
        /// <param name="material">The material to be exposed.</param>
        public MaterialView(Material material)
        {
            this.material = material;
        }

        /// <inheritdoc/>
        public void RenderResourceView()
        {
            ImGui.Begin("Material");

            ImGui.DragFloat(
                "Roughness",
                ref this.material.Format.Attributes["roughness"].GetFloatRef(),
                0.001f,
                0.0f,
                1.0f);

            ImGui.DragFloat(
                "Spec. Intensity",
                ref this.material.Format.Attributes["specularIntensity"].GetFloatRef(),
                0.001f,
                0.0f,
                3.0f);

            ImGui.DragFloat(
                "Spec. Power",
                ref this.material.Format.Attributes["specularPower"].GetFloatRef(),
                0.1f,
                1.0f,
                18.0f);

            ImGui.ColorPicker3(
                "Color",
                ref this.material.Format.Attributes["specularColor"].GetVector3Ref(),
                ImGuiColorEditFlags.PickerHueWheel);

            ImGui.Checkbox("Use Maps", ref this.useMaps);

            if (this.useMaps)
            {
                ImGui.Text("TODO: insert texture loaders here.");
            }

            ImGui.End();
        }
    }
}
