using ImGuiNET;

using Framework.Core.Light;

namespace Framework.Utils.View
{
    public class LightView
    {
        private Light light;

        public LightView(Light light)
        {
            this.light = light;
        }

        public void DrawControl()
        {
            ImGui.Begin("Light");

            ImGui.DragFloat3("Position", ref light.position, 0.001f, -1.0f, 1.0f);
            ImGui.DragFloat3("Direction", ref light.direction, 0.001f);
            ImGui.ColorPicker3("Color", ref light.color, ImGuiColorEditFlags.NoAlpha);
            ImGui.DragFloat("Intensity", ref light.intensity, 0.001f, 0.0f, 1.0f);
            ImGui.Checkbox("Cast Shadow", ref light.castShadow);

            ImGui.End();
        }

        public void DrawGuismo()
        {
            // TODO: Draw a textured point to represent the light and it's target
        }
    }
}
