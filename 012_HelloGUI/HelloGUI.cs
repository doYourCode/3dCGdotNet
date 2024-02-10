using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Graphics.OpenGL4;
using ImGuiNET;

using Framework.Utils.Common;
using Framework.Utils.Common.Mesh;
using Framework.Core;
using Assimp;

namespace Examples
{
    public class HelloGUI : GameWindow
    {
        ImGuiController _controller;

        Dictionary<String, BasicMesh> meshes;
        BasicMesh currentMesh;

        Texture texture;

        Shader shader;

        Transform transform;

        private float tick = 0.0f;

        readonly System.Numerics.Vector2 modalInitPos = new System.Numerics.Vector2(16, 16);
        readonly System.Numerics.Vector2 modalInitSize = new System.Numerics.Vector2(330, 200);

        readonly String[] items = { "Cube", "Monkey", "Teapot" };
        String currentItem = "Cube";
        String previousItem = "";

        bool rotate;
        System.Numerics.Vector3 rotationSpeed;

        public HelloGUI(
            GameWindowSettings gameWindowSettings,
            NativeWindowSettings nativeWindowSettings) :
            base(gameWindowSettings, nativeWindowSettings) { }

        protected override void OnLoad()
        {
            base.OnLoad();

            meshes = new Dictionary<String, BasicMesh>();

            meshes.Add("Cube", new BasicMesh("Resources/Mesh/Cube.obj", true));
            meshes.Add("Monkey", new BasicMesh("Resources/Mesh/Suzanne.obj"));
            meshes.Add("Teapot", new BasicMesh("Resources/Mesh/Teapot.obj"));

            texture = Texture.LoadFromFile("Resources/Texture/Uv_checker_01.png", OpenTK.Graphics.OpenGL.TextureUnit.Texture0);

            shader = new Shader("HelloTransformation");

            transform = new Transform();

            _controller = new ImGuiController(ClientSize.X, ClientSize.Y);
        }

        protected override void OnResize(ResizeEventArgs e)
        {
            base.OnResize(e);

            GL.Viewport(0, 0, ClientSize.X, ClientSize.Y);

            _controller.WindowResized(ClientSize.X, ClientSize.Y);
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            _controller.Update(this, (float)e.Time);

            GL.ClearColor(new Color4(0, 32, 48, 255));
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit | ClearBufferMask.StencilBufferBit);

            shader.Use();

            texture.Use(OpenTK.Graphics.OpenGL.TextureUnit.Texture0);

            if(currentMesh != null)
                currentMesh.Draw();

            GL.Disable(EnableCap.DepthTest);
            ShowGUI();

            _controller.Render();
            GL.Enable(EnableCap.DepthTest);

            ImGuiController.CheckGLError("End of frame");

            SwapBuffers();
        }

        protected override void OnUpdateFrame(FrameEventArgs args)
        {
            base.OnUpdateFrame(args);

            if(currentItem != previousItem)
                ChangeItem(currentItem);

            if(rotate)
            {
                transform.SetRotationX(tick * rotationSpeed.X);
                transform.SetRotationY(tick * rotationSpeed.Y);
                transform.SetRotationZ(tick * rotationSpeed.Z);

                tick += 0.01f;
            }

            shader.SetMatrix4("model", transform.GetModelMatrix());
        }

        protected override void OnTextInput(TextInputEventArgs e)
        {
            base.OnTextInput(e);


            _controller.PressChar((char)e.Unicode);
        }

        protected override void OnMouseWheel(MouseWheelEventArgs e)
        {
            base.OnMouseWheel(e);

            _controller.MouseScroll(e.Offset);
        }

        private void ShowGUI()
        {
            ImGui.Begin("Configurações", ImGuiWindowFlags.NoResize | ImGuiWindowFlags.NoMove);
            ImGui.SetWindowPos(modalInitPos);
            ImGui.SetWindowSize(modalInitSize);

            ImGui.LabelText("", "Select a mesh");

            ImGui.Spacing();

            if (ImGui.BeginCombo("", currentItem))
            {
                for (int n = 0; n < items.Length; n++)
                {
                    bool is_selected = (currentItem == items[n]);

                    if (ImGui.Selectable(items[n], is_selected))
                        currentItem = items[n];

                    if (is_selected)
                        ImGui.SetItemDefaultFocus();
                }
                ImGui.EndCombo();
            }

            ImGui.Spacing();

            ImGui.Checkbox("Rotate mesh", ref rotate);

            if(rotate)
            {
                ImGui.Spacing();

                ImGui.LabelText("", "X         Y         Z");

                ImGui.DragFloat3("Rotation speed", ref rotationSpeed, 0.01f, -1.0f, 1.0f);

                if(ImGui.Button("Reset rotation"))
                {
                    tick = 0.0f;
                }
            }

            ImGui.End();
        }

        private void ChangeItem(String item)
        {
            currentMesh = meshes.GetValueOrDefault(currentItem);
            previousItem = currentItem;
        }
    }
}
