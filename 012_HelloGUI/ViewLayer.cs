using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Graphics.OpenGL4;
using ImGuiNET;

using Framework.Utils.Common;

namespace Examples
{
    internal class ViewLayer
    {
        ImGuiController _controller;

        readonly System.Numerics.Vector2 modalInitPos = new System.Numerics.Vector2(16, 16);
        readonly System.Numerics.Vector2 modalInitSize = new System.Numerics.Vector2(330, 200);

        private String[] items;
        private String currentItem = "";
        private String previousItem = "";

        private bool rotate;
        private System.Numerics.Vector3 rotationSpeed;

        private float tick = 0.0f;

        public string CurrentItem { get => currentItem; set => currentItem = value; }
        public string PreviousItem { get => previousItem; set => previousItem = value; }
        public float Tick { get => tick; set => tick = value; }
        public bool Rotate { get => rotate; set => rotate = value; }
        public System.Numerics.Vector3 RotationSpeed { get => rotationSpeed; set => rotationSpeed = value; }

        public void Load(GameWindow window)
        {
            _controller = new ImGuiController(window.ClientSize.X, window.ClientSize.Y);
        }

        public void Update(GameWindow window, FrameEventArgs args)
        {
            _controller.Update(window, (float)args.Time);
        }
        public void Render()
        {
            GL.Disable(EnableCap.DepthTest);

            ShowGUI();
            _controller.Render();

            GL.Enable(EnableCap.DepthTest);
        }

        public void UnLoad()
        {
            _controller.Dispose();
        }

        public void Resize(int width, int height)
        {
            _controller.WindowResized(width, height);
        }

        public void ShowGUI()
        {
            ImGui.Begin("Configurações", ImGuiWindowFlags.NoResize | ImGuiWindowFlags.NoMove);
            ImGui.SetWindowPos(modalInitPos);
            ImGui.SetWindowSize(modalInitSize);

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
        }

        public void SetList(string[] items)
        {
            this.items = items;
            CurrentItem = items[0];
        }

        public ImGuiController GetController()
        {
            return _controller;
        }
    }
}
