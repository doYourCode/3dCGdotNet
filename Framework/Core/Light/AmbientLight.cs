using OpenTK.Graphics.OpenGL4;
using System.Numerics;

namespace Framework.Core.Light
{
    /// <summary>
    /// 
    /// </summary>
    public class AmbientLight
    {
        /* ---------------------------------------------- Variáveis membro ---------------------------------------------- */

        /// <summary>
        /// 
        /// </summary>
        public Vector3 Color { get => color; set => color = value; }

        /// <summary>
        /// 
        /// </summary>
        public float Intensity { get => intensity; set => intensity = value; }


        private int colorUniformLocation;

        private int intensityUniformLocation;

        internal Vector3 color;

        internal float intensity;


        /* ---------------------------------------------- Interface pública ---------------------------------------------- */

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Color"></param>
        /// <param name="Intensity"></param>
        public AmbientLight(Vector3 Color, float Intensity = 1.0f)
        {
            this.color = Color;
            this.intensity = Intensity;
        }

        /// <summary>
        /// 
        /// </summary>
        public AmbientLight() : this(Vector3.Zero) { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="shader"></param>
        public void GetUniformLocations(Shader shader)
        {
            this.colorUniformLocation = GL.GetUniformLocation(shader.ID, "ambientColor");
            this.intensityUniformLocation = GL.GetUniformLocation(shader.ID, "ambientIntensity");
        }

        /// <summary>
        /// 
        /// </summary>
        public void UpdateUniforms()
        {
            GL.Uniform3(this.colorUniformLocation, this.color.X, this.color.Y, this.color.Z);
            GL.Uniform1(this.intensityUniformLocation, this.intensity);
        }
    }
}
