// <copyright file="LightView.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Framework.Utils.GUI.ViewLayer
{
    using Framework.Core.Light;
    using ImGuiNET;

    /// <summary>
    /// TODO.
    /// </summary>
    public class LightView : IimResourceView
    {
        private Light light;

        private AmbientLight ambientLight;

        /// <summary>
        /// Initializes a new instance of the <see cref="LightView"/> class.
        /// TODO.
        /// </summary>
        /// <param name="light"> PARAM TODO. </param>
        /// <param name="ambientLight"> PARAM2 TODO. </param>
        public LightView(Light light, AmbientLight ambientLight)
        {
            this.light = light;
            this.ambientLight = ambientLight;
        }

        /// <summary>
        /// TODO.
        /// </summary>
        public void RenderResourceView()
        {
            ImGui.Begin("Light");

            if (ImGui.BeginTabBar("Settings"))
            {
                if (ImGui.BeginTabItem("Diffuse"))
                {
                    ImGui.DragFloat3("Position", ref this.light.PositionRef, 0.001f);
                    ImGui.DragFloat3("Direction", ref this.light.DirectionRef, 0.001f);

                    ImGui.ColorPicker3(
                        "Color",
                        ref this.light.ColorRef,
                        ImGuiColorEditFlags.PickerHueWheel);

                    ImGui.DragFloat(
                        "Intensity",
                        ref this.light.IntensityRef,
                        0.001f,
                        0.0f,
                        2.0f);

                    ImGui.EndTabItem();
                }

                if (ImGui.BeginTabItem("Ambient"))
                {
                    ImGui.ColorPicker3(
                        "Color",
                        ref this.ambientLight.ColorRef,
                        ImGuiColorEditFlags.PickerHueWheel);

                    ImGui.DragFloat(
                        "Intensity",
                        ref this.ambientLight.IntensityRef,
                        0.001f,
                        0.0f,
                        2.0f);

                    ImGui.EndTabItem();
                }

                if (ImGui.BeginTabItem("Shadow"))
                {
                    ImGui.Checkbox("Cast Shadow", ref this.light.CastShadowRef);

                    if (this.light.CastShadow)
                    {
                        ImGui.Text("TODO: insert shadow functionality here.");
                    }

                    ImGui.EndTabItem();
                }

                ImGui.EndTabBar();
            }

            ImGui.End();
        }
    }
}
