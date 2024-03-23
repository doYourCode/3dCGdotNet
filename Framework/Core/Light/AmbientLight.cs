using OpenTK.Graphics.OpenGL4;
using System.Numerics;

namespace Framework.Core.Light
{
    public class AmbientLight
    {
        private int colorUniformLocation;
        private int intensityUniformLocation;

        internal Vector3 color;
        internal float intensity;

        public Vector3 Color { get => color; set => color = value; }
        public float Intensity { get => intensity; set => intensity = value; }

        public AmbientLight(Vector3 Color, float Intensity = 1.0f)
        {
            this.color = Color;
            this.intensity = Intensity;
        }

        public AmbientLight() : this(Vector3.Zero) { }

        public void GetUniformLocations(Shader shader)
        {
            this.colorUniformLocation = GL.GetUniformLocation(shader.ID, "ambientColor");
            this.intensityUniformLocation = GL.GetUniformLocation(shader.ID, "ambientIntensity");
        }

        public void UpdateUniforms()
        {
            GL.Uniform3(this.colorUniformLocation, this.color.X, this.color.Y, this.color.Z);
            GL.Uniform1(this.intensityUniformLocation, this.intensity);
        }
    }
}
