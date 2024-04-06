// <copyright file="Light.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Framework.Core.Light
{
    using System.Numerics;
    using OpenTK.Graphics.OpenGL4;

    /// <summary>
    /// Tipos de luz: Directional | Spot | Point | Area.
    /// </summary>
    internal enum LightType
    {
        /// <summary>
        /// TODO.
        /// </summary>
        Directional,

        /// <summary>
        /// TODO.
        /// </summary>
        Spot,

        /// <summary>
        /// TODO.
        /// </summary>
        Point,

        /// <summary>
        /// TODO.
        /// </summary>
        Area,
    }

    /// <summary>
    /// Representação genérica de um ponto // direção que seja uma fonte de luz.
    /// Essa classe fornece os dados que utilizaremos nos shaders para escrever
    /// os algoritmos de efeitos de iluminação. (Ex: Lambert, Phong, Blinn, Oren-Nayar
    /// ou mesmo PBR shading).
    /// </summary>
    public class Light
    {
        private int positionUniformLocation;

        private int directionUniformLocation;

        private int colorUniformLocation;

        private int intensityUniformLocation;

        private Vector3 position;

        private Vector3 direction;

        private Vector3 color;

        private float intensity;

        private bool castShadow;

        /// <summary>
        /// Initializes a new instance of the <see cref="Light"/> class.
        /// TODO.
        /// </summary>
        /// <param name="position"> PARAM TODO. </param>
        /// <param name="color"> PARAM2 TODO. </param>
        /// <param name="direction"> PARAM3 TODO. </param>
        /// <param name="intensity"> PARAM4 TODO. </param>
        /// <param name="castShadow"> PARAM5 TODO. </param>
        public Light(
            Vector3 position,
            Vector3 color,
            Vector3 direction,
            float intensity = 1.0f,
            bool castShadow = false)
        {
            this.position = position;
            this.direction = direction;
            this.color = color;
            this.intensity = intensity;
            this.castShadow = castShadow;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Light"/> class.
        /// TODO.
        /// </summary>
        public Light()
            : this(Vector3.Zero, Vector3.One, Vector3.Zero)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Light"/> class.
        /// </summary>
        /// <param name="position"> PARAM TODO. </param>
        /// <param name="intensity"> PARAM2 TODO. </param>
        /// <param name="castShadow"> PARAM3 TODO. </param>
        public Light(
            Vector3 position,
            float intensity = 1.0f,
            bool castShadow = false)
            : this(
                  position,
                  Vector3.One,
                  Vector3.Zero,
                  intensity,
                  castShadow)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Light"/> class.
        /// TODO.
        /// </summary>
        /// <param name="position"> PARAM TODO. </param>
        /// <param name="color"> PARAM2 TODO. </param>
        /// <param name="intensity"> PARAM3 TODO. </param>
        /// <param name="castShadow"> PARAM4 TODO. </param>
        public Light(
            Vector3 position,
            Vector3 color,
            float intensity = 1.0f,
            bool castShadow = false)
            : this(
                position,
                color,
                Vector3.Zero,
                intensity,
                castShadow)
        {
        }

        /// <summary>
        /// Gets or sets TODO.
        /// </summary>
        public Vector3 Position { get => this.position; set => this.position = value; }

        /// <summary>
        /// Gets or sets TODO.
        /// </summary>
        public Vector3 Direction { get => this.direction; set => this.direction = value; }

        /// <summary>
        /// Gets or sets TODO.
        /// </summary>
        public Vector3 Color { get => this.color; set => this.color = value; }

        /// <summary>
        /// Gets or sets TODO.
        /// </summary>
        public float Intensity { get => this.intensity; set => this.intensity = value; }

        /// <summary>
        /// Gets or sets a value indicating whether TODO.
        /// </summary>
        public bool CastShadow { get => this.castShadow; set => this.castShadow = value; } // TODO: implementar os efeitos de sombra

        /// <summary>
        /// Gets or sets TODO.
        /// </summary>
        public ref Vector3 PositionRef { get => ref this.position; }

        /// <summary>
        /// Gets TODO.
        /// </summary>
        public ref Vector3 DirectionRef { get => ref this.direction; }

        /// <summary>
        /// Gets TODO.
        /// </summary>
        public ref Vector3 ColorRef { get => ref this.color; }

        /// <summary>
        /// Gets TODO.
        /// </summary>
        public ref float IntensityRef { get => ref this.intensity; }

        /// <summary>
        /// Gets a value indicating whether TODO.
        /// </summary>
        public ref bool CastShadowRef { get => ref this.castShadow; }

        /// <summary>
        /// Gets the uniforms set.
        /// </summary>
        /// <param name="shader"> PARAM TODO. </param>
        public void GetUniformLocations(Shader shader)
        {
            this.positionUniformLocation = GL.GetUniformLocation(shader.ID, "lightPosition");
            this.directionUniformLocation = GL.GetUniformLocation(shader.ID, "lightDirection");
            this.colorUniformLocation = GL.GetUniformLocation(shader.ID, "lightColor");
            this.intensityUniformLocation = GL.GetUniformLocation(shader.ID, "lightIntensity");
        }

        /// <summary>
        /// TODO.
        /// </summary>
        public void UpdateUniforms()
        {
            GL.Uniform3(this.positionUniformLocation, this.position.X, this.position.Y, this.position.Z);
            GL.Uniform3(this.directionUniformLocation, this.direction.X, this.direction.Y, this.direction.Z);
            GL.Uniform3(this.colorUniformLocation, this.color.X, this.color.Y, this.color.Z);
            GL.Uniform1(this.intensityUniformLocation, this.intensity);
        }

        /// <summary>
        /// TODO.
        /// </summary>
        public void Dispose()
        {
        }
    }
}