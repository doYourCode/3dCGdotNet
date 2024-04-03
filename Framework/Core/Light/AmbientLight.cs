// <copyright file="AmbientLight.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Framework.Core.Light
{
    using System.Numerics;
    using OpenTK.Graphics.OpenGL4;

    /// <summary>
    /// 
    /// </summary>
    public class AmbientLight
    {
        private int colorUniformLocation;

        private int intensityUniformLocation;

        private Vector3 color;

        private float intensity;

        /// <summary>
        /// Initializes a new instance of the <see cref="AmbientLight"/> class.
        /// </summary>
        /// <param name="color"></param>
        /// <param name="intensity"></param>
        public AmbientLight(Vector3 color, float intensity = 1.0f)
        {
            this.color = color;
            this.intensity = intensity;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AmbientLight"/> class.
        /// </summary>
        public AmbientLight()
            : this(Vector3.Zero) { }

        /// <summary>
        /// 
        /// </summary>
        public Vector3 Color { get => this.color; set => this.color = value; }

        /// <summary>
        /// 
        /// </summary>
        public ref Vector3 ColorRef { get => ref this.color; }

        /// <summary>
        /// 
        /// </summary>
        public float Intensity { get => this.intensity; set => this.intensity = value; }

        public ref float IntensityRef { get => ref this.intensity; }

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
