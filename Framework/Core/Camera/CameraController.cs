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
    /// 
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
        /// <param name="Camera"></param>
        /// <param name="Window"></param>
        public CameraController(PerspectiveCamera Camera, GameWindow Window)
        {
            this.camera = Camera;
            this.window = Window;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        /// <param name="input"></param>
        /// <param name="mouse"></param>
        public void Update(FrameEventArgs e, KeyboardState input, MouseState mouse)
        {
            if (!this.window.IsFocused) // Check to see if the window is focused
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
                this.camera.Position += this.camera.Front * cameraSpeed * (float)e.Time; // Forward
            }
            else if (input.IsKeyDown(Keys.S))
            {
                this.camera.Position -= this.camera.Front * cameraSpeed * (float)e.Time; // Backwards
            }

            if (input.IsKeyDown(Keys.A))
            {
                this.camera.Position -= this.camera.Right * cameraSpeed * (float)e.Time; // Left
            }
            else if (input.IsKeyDown(Keys.D))
            {
                this.camera.Position += this.camera.Right * cameraSpeed * (float)e.Time; // Right
            }

            if (input.IsKeyDown(Keys.Q))
            {
                this.camera.Position += this.camera.Up * cameraSpeed * (float)e.Time; // Up
            }
            else if (input.IsKeyDown(Keys.E))
            {
                this.camera.Position -= this.camera.Up * cameraSpeed * (float)e.Time; // Down
            }

            if (mouse.IsButtonDown(MouseButton.Right))
            {
                if (this.firstMove) // This bool variable is initially set to true.
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
                    this.camera.Pitch -= deltaY * sensitivity; // Reversed since y-coordinates range from bottom to top
                }
            }

            if (mouse.IsButtonReleased(MouseButton.Right))
            {
                this.firstMove = true;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        public void MouseUpdate(MouseWheelEventArgs e)
        {
            this.camera.Fov -= e.OffsetY;
        }
    }
}
