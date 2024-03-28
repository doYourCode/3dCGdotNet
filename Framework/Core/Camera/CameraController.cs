using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.GraphicsLibraryFramework;
using OpenTK.Windowing.Desktop;

namespace Framework.Core.Camera
{
    /// <summary>
    /// 
    /// </summary>
    public class CameraController
    {
        /* ---------------------------------------------- Variáveis membro ---------------------------------------------- */

        private PerspectiveCamera camera;

        private GameWindow window;

        private bool firstMove = true;

        private Vector2 lastPos;

        /* ---------------------------------------------- Interface pública ---------------------------------------------- */

        /// <summary>
        /// 
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
        /// <param name="Event"></param>
        /// <param name="Input"></param>
        /// <param name="Mouse"></param>
        public void Update(FrameEventArgs Event, KeyboardState Input, MouseState Mouse)
        {
            if (!window.IsFocused) // Check to see if the window is focused
            {
                return;
            }

            if (Input.IsKeyDown(Keys.Escape))
            {
                window.Close();
            }

            const float cameraSpeed = 1.5f;
            const float sensitivity = 0.2f;

            if (Input.IsKeyDown(Keys.W))
            {
                camera.Position += camera.Front * cameraSpeed * (float)Event.Time; // Forward
            }

            if (Input.IsKeyDown(Keys.S))
            {
                camera.Position -= camera.Front * cameraSpeed * (float)Event.Time; // Backwards
            }
            if (Input.IsKeyDown(Keys.A))
            {
                camera.Position -= camera.Right * cameraSpeed * (float)Event.Time; // Left
            }
            if (Input.IsKeyDown(Keys.D))
            {
                camera.Position += camera.Right * cameraSpeed * (float)Event.Time; // Right
            }
            if (Input.IsKeyDown(Keys.Space))
            {
                camera.Position += camera.Up * cameraSpeed * (float)Event.Time; // Up
            }
            if (Input.IsKeyDown(Keys.LeftShift))
            {
                camera.Position -= camera.Up * cameraSpeed * (float)Event.Time; // Down
            }

            if (firstMove) // This bool variable is initially set to true.
            {
                lastPos = new Vector2(Mouse.X, Mouse.Y);
                firstMove = false;
            }
            else
            {
                // Calculate the offset of the mouse position
                var deltaX = Mouse.X - lastPos.X;
                var deltaY = Mouse.Y - lastPos.Y;
                lastPos = new Vector2(Mouse.X, Mouse.Y);

                // Apply the camera pitch and yaw (we clamp the pitch in the camera class)
                camera.Yaw += deltaX * sensitivity;
                camera.Pitch -= deltaY * sensitivity; // Reversed since y-coordinates range from bottom to top
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Event"></param>
        public void MouseUpdate(MouseWheelEventArgs Event)
        {
            camera.Fov -= Event.OffsetY;
        }
    }
}
