using OpenTK.Graphics.OpenGL4;
using System.Numerics;

namespace Framework.Core.Light
{
    /// <summary>
    /// 
    /// </summary>
    public class AmbientLight
    {
        #region (Data Fields)

        private int colorUniformLocation;

        private int intensityUniformLocation;

        internal Vector3 color;

        internal float intensity;

        #endregion

        #region (Constructors)

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

        #endregion

        #region (Properties)

        /// <summary>
        /// 
        /// </summary>
        public Vector3 Color { get => color; set => color = value; }

        /// <summary>
        /// 
        /// </summary>
        public float Intensity { get => intensity; set => intensity = value; }

        #endregion

        #region (Public Methods)

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Shader"></param>
        public void GetUniformLocations(Shader Shader)
        {
            this.colorUniformLocation = GL.GetUniformLocation(Shader.ID, "ambientColor");
            this.intensityUniformLocation = GL.GetUniformLocation(Shader.ID, "ambientIntensity");
        }

        /// <summary>
        /// 
        /// </summary>
        public void UpdateUniforms()
        {
            GL.Uniform3(this.colorUniformLocation, this.color.X, this.color.Y, this.color.Z);
            GL.Uniform1(this.intensityUniformLocation, this.intensity);
        }

        #endregion
    }
}
