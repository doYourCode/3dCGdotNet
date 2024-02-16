using OpenTK.Graphics.OpenGL4;
using System.Numerics;

namespace Framework.Core.Light
{
    public class Light
    {
        private int positionUniformLocation;
        private int directionUniformLocation;
        private int colorUniformLocation;
        private int intensityUniformLocation;

        internal Vector3 position;
        internal Vector3 direction;
        internal Vector3 color;
        internal float intensity;
        internal bool castShadow;

        public Vector3 Position { get => position; set => position = value; }
        public Vector3 Direction { get => direction; set => direction = value; }
        public Vector3 Color { get => color; set => color = value; }
        public float Intensity { get => intensity; set => intensity = value; }
        public bool CastShadow { get => castShadow; set => castShadow = value; }

        public Light(Vector3 Position, Vector3 Color, Vector3 Direction, float Intensity = 1.0f, bool CastShadow = false)
        {
            this.position = Position;
            this.direction = Direction;
            this.color = Color;
            this.intensity = Intensity;
            this.castShadow = CastShadow;
        }

        public Light() : this(Vector3.Zero, Vector3.Zero, Vector3.One) { }
        public Light(Vector3 Position, float Intensity = 1.0f, bool CastShadow = false) : this(Position, Vector3.Zero, Vector3.One, Intensity, CastShadow) { }
        public Light(Vector3 Position, Vector3 Color, float Intensity = 1.0f, bool CastShadow = false) : this(Position, Vector3.Zero, Color, Intensity, CastShadow) { }

        public void GetUniformLocations(Shader shader)
        {
            this.positionUniformLocation = GL.GetUniformLocation(shader.Handle, "lightPosition");
            this.directionUniformLocation = GL.GetUniformLocation(shader.Handle, "lightDirection");
            this.colorUniformLocation = GL.GetUniformLocation(shader.Handle, "lightColor");
            this.intensityUniformLocation = GL.GetUniformLocation(shader.Handle, "lightIntensity");
        }

        public void UpdateUniforms()
        {
            GL.Uniform3(this.positionUniformLocation, this.position.X, this.position.Y, this.position.Z);
            GL.Uniform3(this.directionUniformLocation, this.direction.X, this.direction.Y, this.direction.Z);
            GL.Uniform3(this.colorUniformLocation, this.color.X, this.color.Y, this.color.Z);
            GL.Uniform1(this.intensityUniformLocation, this.intensity);
        }
    }
}
