﻿using ImGuiNET;
using Framework.Core.Light;

namespace Framework.Utils.GUI.ViewLayer
{
    /// <summary>
    /// 
    /// </summary>
    public class LightView : IimResourceView
    {
        private Light light;

        private AmbientLight ambientLight;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="light"></param>
        /// <param name="AmbientLight"></param>
        public LightView(Light light, AmbientLight AmbientLight)
        {
            this.light = light;
            ambientLight = AmbientLight;
        }

        /// <summary>
        /// 
        /// </summary>
        public void RenderResourceView()
        {
            ImGui.Begin("Light");

            if (ImGui.BeginTabBar("Settings"))
            {
                if (ImGui.BeginTabItem("Diffuse"))
                {
                    ImGui.DragFloat3("Position", ref light.position, 0.001f);
                    ImGui.DragFloat3("Direction", ref light.direction, 0.001f);

                    ImGui.ColorPicker3("Color",
                                       ref light.color,
                                       ImGuiColorEditFlags.PickerHueWheel);

                    ImGui.DragFloat("Intensity",
                                    ref light.intensity,
                                    0.001f,
                                    0.0f,
                                    2.0f);

                    ImGui.EndTabItem();
                }

                if (ImGui.BeginTabItem("Ambient"))
                {
                    ImGui.ColorPicker3("Color",
                                       ref ambientLight.color,
                                       ImGuiColorEditFlags.PickerHueWheel);

                    ImGui.DragFloat("Intensity",
                                    ref ambientLight.intensity,
                                    0.001f,
                                    0.0f,
                                    2.0f);

                    ImGui.EndTabItem();
                }

                if (ImGui.BeginTabItem("Shadow"))
                {
                    ImGui.Checkbox("Cast Shadow", ref light.castShadow);

                    if(light.castShadow)
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
