// <copyright file="IViewLayer.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Framework.Utils.GUI.ViewLayer
{
    using OpenTK.Windowing.Common;
    using OpenTK.Windowing.Desktop;

    /// <summary>
    /// Provides signature methods for a View Layer object.
    /// </summary>
    public interface IViewLayer
    {
        /// <summary>
        /// Loads the necessary data for rendering the View Layer.
        /// </summary>
        /// <param name="window">The current context window.</param>
        public void Load(GameWindow window);

        /// <summary>
        /// Updates the View Layer state.
        /// </summary>
        /// <param name="window">The current context window.</param>
        /// <param name="args">Current state arguments.</param>
        public void Update(GameWindow window, FrameEventArgs args);

        /// <summary>
        /// Renders a View Layer frame.
        /// </summary>
        /// <param name="e">Current frame arguments.</param>
        public void Render(FrameEventArgs e);

        /// <summary>
        /// Unloads the data.
        /// </summary>
        public void Unload();

        /// <summary>
        /// Renders the View objects.
        /// </summary>
        public void RenderView();
    }
}
