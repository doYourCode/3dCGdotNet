// <copyright file="ViewLayer.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Examples
{
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

        private GameWindow window;

        private bool sceneTreeActive;

        private bool logActive;

        private bool assetsActive;

        private bool setNPropsActive;

        private bool viewportActive;

        /// <summary>
        /// Gets the ImGui controller object reference..
        /// </summary>
        public ImGuiController Controller { get => this.controller; }

        /// <summary>
        ///  Gets a value indicating whether the Viewport is active.
        /// </summary>
        public bool ViewportActive { get => this.viewportActive; }

        /// <inheritdoc/>
        public void Load(GameWindow window)
        {
            this.controller = new ImGuiController(window.ClientSize.X, window.ClientSize.Y);
            this.window = window;
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
            this.window.Dispose();
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
            ImGui.BeginMainMenuBar();

            if (ImGui.BeginMenu("File"))
            {
                ImGui.MenuItem("New Project", "Crtl + N");
                ImGui.MenuItem("Open Project", "Crtl + O");
                ImGui.MenuItem("Save Project", "Crtl + S");
                ImGui.MenuItem("Close Project");

                ImGui.Separator();

                ImGui.MenuItem("New Scene", "Alt + N");
                ImGui.MenuItem("Load Scene", "Alt + L");
                ImGui.MenuItem("Save Scene", "Alt + S");
                ImGui.MenuItem("Close Scene");

                ImGui.Separator();

                ImGui.MenuItem("Save All", "Crtl + LShift + S");
                ImGui.MenuItem("Close All");

                ImGui.Separator();

                ImGui.MenuItem("Options", "Ctrl + Alt + O");

                ImGui.Separator();

                if (ImGui.MenuItem("Exit", "LShift + ESC"))
                {
                    this.window.Close();
                }

                ImGui.EndMenu();
            }

            ImGui.EndMainMenuBar();

            uint dockSpaceId = ImGui.DockSpaceOverViewport();

            ImGui.Begin(
                "Scene Tree",
                ref this.sceneTreeActive,
                ImGuiWindowFlags.NoMove);

            ImGui.End();

            ImGui.Begin(
                "Log",
                ref this.logActive,
                ImGuiWindowFlags.NoMove);

            ImGui.End();

            ImGui.Begin(
                "Assets",
                ref this.assetsActive,
                ImGuiWindowFlags.NoMove);

            ImGui.End();

            ImGui.Begin(
                "Settings & Properties",
                ref this.setNPropsActive,
                ImGuiWindowFlags.NoMove);

            ImGui.End();

            ImGui.Begin(
                "Viewport",
                ref this.viewportActive,
                ImGuiWindowFlags.NoMove);

            ImGui.End();
        }
    }
}