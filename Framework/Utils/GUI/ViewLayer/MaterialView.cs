using ImGuiNET;
using Framework.Core;

namespace Framework.Utils.GUI.ViewLayer
{
    /// <summary>
    /// 
    /// </summary>
    public class MaterialView : IimResourceControl
    {
        #region (Data Fields)

        private Material material;

        private bool useMaps = false;

        #endregion

        #region (Constructors)

        /// <summary>
        /// 
        /// </summary>
        /// <param name="material"></param>
        public MaterialView(Material material)
        {
            this.material = material;
        }

        #endregion

        #region (Public Methods)

        /// <summary>
        /// 
        /// </summary>
        public void RenderControl()
        {
            ImGui.Begin("Material");

            ImGui.DragFloat("Roughness",
                ref material.roughness,
                0.001f,
                0.0f,
                1.0f);

            ImGui.DragFloat("Spec. Intensity",
                ref material.specularIntensity,
                0.001f,
                0.0f,
                3.0f);

            ImGui.DragFloat("Spec. Power",
                ref material.specularPower,
                0.1f,
                1.0f,
                18.0f);

            ImGui.ColorPicker3("Color",
                   ref material.specularColor,
                   ImGuiColorEditFlags.PickerHueWheel);

            if(ImGui.Checkbox("Use Maps", ref useMaps))
            {
                //ImGui.image(...)
                ImGui.Text("TODO: insert texture loaders here.");
            }

            ImGui.End();
        }

        #endregion
    }
}
