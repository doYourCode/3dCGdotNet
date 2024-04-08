// <copyright file="CameraController.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Framework.Core.Camera
{
    using OpenTK.Mathematics;
    using OpenTK.Windowing.Common;
    using OpenTK.Windowing.Desktop;
    using OpenTK.Windowing.GraphicsLibraryFramework;

    /// <summary>
    /// TODO.
    /// </summary>
    public class CameraController
    {
        private PerspectiveCamera camera;

        private GameWindow window;

        private bool firstMove = true;

        private Vector2 lastPos;

        /// <summary>
        /// Initializes a new instance of the <see cref="CameraController"/> class.
        /// </summary>
        /// <param name="camera"> PARAM TODO. </param>
        /// <param name="window"> PARAM2 TODO. </param>
        public CameraController(PerspectiveCamera camera, GameWindow window)
        {
            this.camera = camera;
            this.window = window;
        }

        /// <summary>
        /// TODO.
        /// </summary>
        /// <param name="e"> PARAM TODO. </param>
        /// <param name="input"> PARAM2 TODO. </param>
        /// <param name="mouse"> PARAM3 TODO. </param>
        public void Update(FrameEventArgs e, KeyboardState input, MouseState mouse)
        {
            // Check to see if the window is focused
            if (!this.window.IsFocused)
            {
                return;
            }

            if (input.IsKeyDown(Keys.Escape))
            {
                this.window.Close();
            }

            const float cameraSpeed = 1.5f;
            const float sensitivity = 0.2f;

            if (input.IsKeyDown(Keys.W))
            {
                // Forward
                this.camera.Position += this.camera.Front * cameraSpeed * (float)e.Time;
            }
            else if (input.IsKeyDown(Keys.S))
            {
                // Backwards
                this.camera.Position -= this.camera.Front * cameraSpeed * (float)e.Time;
            }

            if (input.IsKeyDown(Keys.A))
            {
                // Left
                this.camera.Position -= this.camera.Right * cameraSpeed * (float)e.Time;
            }
            else if (input.IsKeyDown(Keys.D))
            {
                // Right
                this.camera.Position += this.camera.Right * cameraSpeed * (float)e.Time;
            }

            if (input.IsKeyDown(Keys.Q))
            {
                // Up
                this.camera.Position += this.camera.Up * cameraSpeed * (float)e.Time;
            }
            else if (input.IsKeyDown(Keys.E))
            {
                // Down
                this.camera.Position -= this.camera.Up * cameraSpeed * (float)e.Time;
            }

            if (mouse.IsButtonDown(MouseButton.Right))
            {
                if (this.firstMove)
                {
                    this.lastPos = new Vector2(mouse.X, mouse.Y);
                    this.firstMove = false;
                }
                else
                {
                    // Calculate the offset of the mouse position
                    var deltaX = mouse.X - this.lastPos.X;
                    var deltaY = mouse.Y - this.lastPos.Y;
                    this.lastPos = new Vector2(mouse.X, mouse.Y);

                    // Apply the camera pitch and yaw (we clamp the pitch in the camera class)
                    this.camera.Yaw += deltaX * sensitivity;

                    // Reversed since y-coordinates range from bottom to top
                    this.camera.Pitch -= deltaY * sensitivity;
                }
            }

            if (mouse.IsButtonReleased(MouseButton.Right))
            {
                this.firstMove = true;
            }
        }

        /// <summary>
        /// TODO.
        /// </summary>
        /// <param name="e"> PARAM TODO. </param>
        public void MouseUpdate(MouseWheelEventArgs e)
        {
            this.camera.Fov -= e.OffsetY;
        }
    }
}
