using ImGuiNET;
using Framework.Core.Material;

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
                ref material.format.attributes["roughness"].FloatRef,
                0.001f,
                0.0f,
                1.0f);

            ImGui.DragFloat("Spec. Intensity",
                ref material.format.attributes["specularIntensity"].FloatRef,
                0.001f,
                0.0f,
                3.0f);

            ImGui.DragFloat("Spec. Power",
                ref material.format.attributes["specularPower"].FloatRef,
                0.1f,
                1.0f,
                18.0f);

            ImGui.ColorPicker3("Color",
                   ref material.format.attributes["specularColor"].Vector3Ref,
                   ImGuiColorEditFlags.PickerHueWheel);

            ImGui.Checkbox("Use Maps", ref useMaps);

            if(useMaps)
            {
                //ImGui.image(...)
                ImGui.Text("TODO: insert texture loaders here.");
            }

            ImGui.End();
        }

        #endregion
    }
}
