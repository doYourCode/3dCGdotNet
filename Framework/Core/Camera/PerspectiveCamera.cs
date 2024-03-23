using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

namespace Framework.Core.Camera
{
    /// <summary>
    /// 
    /// </summary>
    public class PerspectiveCamera
    {
        /* -------------------------------------------- Variáveis de classe -------------------------------------------- */
#if DEBUG
        /// <summary>
        /// Representa o quantitativo de objetos do tipo câmera com perspectiva.
        /// </summary>
        public static UInt32 Count { get { return count; } private set { } }

        private static UInt32 count = 0;
#endif

        /* ---------------------------------------------- Variáveis membro ---------------------------------------------- */

        /// <summary>
        /// 
        /// </summary>
        public Vector3 Position { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public float AspectRatio { private get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Vector3 Front => front;

        /// <summary>
        /// 
        /// </summary>
        public Vector3 Up => up;

        /// <summary>
        /// 
        /// </summary>
        public Vector3 Right => right;

        /// <summary>
        /// 
        /// </summary>
        public float Pitch
        {
            get => MathHelper.RadiansToDegrees(pitch);
            set
            {
                var angle = MathHelper.Clamp(value, -89f, 89f);
                pitch = MathHelper.DegreesToRadians(angle);
                UpdateVectors();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public float Yaw
        {
            get => MathHelper.RadiansToDegrees(yaw);
            set
            {
                yaw = MathHelper.DegreesToRadians(value);
                UpdateVectors();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public float Fov
        {
            get => MathHelper.RadiansToDegrees(fov);
            set
            {
                var angle = MathHelper.Clamp(value, 1f, 90f);
                fov = MathHelper.DegreesToRadians(angle);
            }
        }


        private int positionUniformLocation;

        private Vector3 front = -Vector3.UnitZ;

        private Vector3 up = Vector3.UnitY;

        private Vector3 right = Vector3.UnitX;

        private float pitch;

        private float yaw = -MathHelper.PiOver2;

        private float fov = MathHelper.PiOver2;


        /* ---------------------------------------------- Interface pública ---------------------------------------------- */

        /// <summary>
        /// 
        /// </summary>
        /// <param name="position"></param>
        /// <param name="aspectRatio"></param>
        public PerspectiveCamera(Vector3 position, float aspectRatio)
        {
            Position = position;
            AspectRatio = aspectRatio;
#if DEBUG
            PerspectiveCamera.count++;
#endif
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Matrix4 GetViewMatrix()
        {
            return Matrix4.LookAt(Position, Position + front, up);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Matrix4 GetProjectionMatrix()
        {
            return Matrix4.CreatePerspectiveFieldOfView(fov, AspectRatio, 0.01f, 100f);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Matrix4 GetViewProjectionMatrix()
        {
            return GetViewMatrix() * GetProjectionMatrix();
        }

        /// <summary>
        /// 
        /// </summary>
        private void UpdateVectors()
        {
            front.X = MathF.Cos(pitch) * MathF.Cos(yaw);
            front.Y = MathF.Sin(pitch);
            front.Z = MathF.Cos(pitch) * MathF.Sin(yaw);

            front = Vector3.Normalize(front);

            right = Vector3.Normalize(Vector3.Cross(front, Vector3.UnitY));
            up = Vector3.Normalize(Vector3.Cross(right, front));
        }

        /// <summary>
        /// 
        /// </summary>
        public void UpdateUniforms()
        {
            GL.Uniform3(this.positionUniformLocation, this.Position.X, this.Position.Y, this.Position.Z);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="shader"></param>
        public void GetUniformLocations(Shader shader)
        {
            this.positionUniformLocation = GL.GetUniformLocation(shader.ID, "viewPosition");
        }

        /// <summary>
        /// 
        /// </summary>
        public void Delete()
        {
#if DEBUG
            PerspectiveCamera.count--;
#endif
        }
    }
}