using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Graphics.OpenGL4;
using ImGuiNET;
using Framework.Core.Resource;
using Framework.Utils.GUI;
using Framework.Utils.GUI.ViewLayer;

namespace Examples
{
    internal class ViewLayer : IViewLayer
    {
        ImGuiController controller;

        public LightView LightView { get; set; }
        public MaterialView MaterialView { get; set; }

        private String[] items;
        private bool rotate;
        private System.Numerics.Vector3 rotationSpeed;

        public string CurrentItem { get; set; } = "";
        public string PreviousItem { get; set; } = "";
        public float Tick { get; set; } = 0.0f;
        public bool Rotate { get => rotate; set => rotate = value; }
        public System.Numerics.Vector3 RotationSpeed { get => rotationSpeed; set => rotationSpeed = value; }

        public void Load(GameWindow window)
        {
            controller = new ImGuiController(window.ClientSize.X, window.ClientSize.Y);
        }

        public void Update(GameWindow window, FrameEventArgs args)
        {
            controller.Update(window, (float)args.Time);
        }

        public void Render(FrameEventArgs e)
        {
            GL.Disable(EnableCap.DepthTest);

            RenderView();

            controller.Render();

            GL.Enable(EnableCap.DepthTest);
        }

        public void Unload()
        {
            controller.Dispose();
        }

        public void Resize(int width, int height)
        {
            controller.WindowResized(width, height);
        }

        public void RenderView()
        {
            ImGui.Begin("Configurações");

            ImGui.LabelText("", "Select a mesh");

            ImGui.Spacing();

            if (ImGui.BeginCombo("", CurrentItem))
            {
                for (int n = 0; n < items.Length; n++)
                {
                    bool is_selected = (CurrentItem == items[n]);

                    if (ImGui.Selectable(items[n], is_selected))
                        CurrentItem = items[n];

                    if (is_selected)
                        ImGui.SetItemDefaultFocus();
                }
                ImGui.EndCombo();
            }

            ImGui.Spacing();

            ImGui.Checkbox("Rotate mesh", ref rotate);

            if (Rotate)
            {
                ImGui.Spacing();

                ImGui.LabelText("", "X         Y         Z");

                ImGui.DragFloat3("Rotation speed", ref rotationSpeed, 0.01f, -1.0f, 1.0f);

                if (ImGui.Button("Reset rotation"))
                {
                    Tick = 0.0f;
                }
            }

            ImGui.End();

            ImGui.Begin("VRAM Resources - Items(" + DisposableResource.DisposableResources.Keys.Count + ")");

            foreach(string res in DisposableResource.DisposableResources.Keys)
            {
                ImGui.Text(res);
            }

            ImGui.End();

            LightView.RenderResourceView();

            MaterialView.RenderResourceView();
        }

        public void SetList(string[] items)
        {
            this.items = items;
            CurrentItem = items[0];
        }

        public ImGuiController GetController()
        {
            return controller;
        }
    }
}
