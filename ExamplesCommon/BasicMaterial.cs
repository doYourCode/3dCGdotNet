// <copyright file="BasicMaterial.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace ExamplesCommon
{
    using Framework.Core;
    using Framework.Core.Material;
    using OpenTK.Mathematics;

    /// <summary>
    /// TODO.
    /// </summary>
    public enum ShaderType
    {
#pragma warning disable CRRSP08 // A misspelled word has been found

        /// <summary>
        /// TODO.
        /// </summary>
        Oren_Nayar,

        /// <summary>
        /// TODO.
        /// </summary>
        Phong,

        /// <summary>
        /// TODO.
        /// </summary>
        Lambertian,

        /// <summary>
        /// TODO.
        /// </summary>
        Half_Lambert,

        /// <summary>
        /// TODO.
        /// </summary>
        Gouraud,

#pragma warning restore CRRSP08 // A misspelled word has been found
    }

    /// <summary>
    /// TODO.
    /// </summary>
    public class BasicMaterial : Material
    {
        private static Dictionary<ShaderType, uint> instancesCount = new ()
            {
                { ShaderType.Oren_Nayar, 0 },
                { ShaderType.Phong, 0 },
                { ShaderType.Lambertian, 0 },
                { ShaderType.Half_Lambert, 0 },
                { ShaderType.Gouraud, 0 },
            };

        private ShaderType shaderType;

        /// <summary>
        /// Initializes a new instance of the <see cref="BasicMaterial"/> class.
        /// </summary>
        /// <param name="shaderType"> PARAM TODO. </param>
        /// <param name="format"> PARAM2 TODO. </param>
        [Obsolete("This constructor is deprecated, please use other instead.")]
        public BasicMaterial(ShaderType shaderType, MaterialFormat format)
            : base(format, shaderType.ToString(), 0)
        {
            if (this.AddInstance(shaderType))
            {
                this.Shader = new Shader(shaderType.ToString());
            }

            this.AlbedoMap = new Texture("Albedo");
            this.SpecularMap = new Texture("Specular");
            this.AmbientOcclusionMap = new Texture("AO");
            this.NormalMap = new Texture("Normal");
            this.HeightMap = new Texture();

#if DEBUG
            Console.WriteLine("BasicMaterial layout setup:\n");
            format.PrintLayout();
            Console.WriteLine("\n");
#endif
        }

        /// <summary>
        /// Gets or sets TODO.
        /// </summary>
        public Texture AlbedoMap { get; set; }

        /// <summary>
        /// Gets or sets TODO.
        /// </summary>
        public Texture SpecularMap { get; set; }

        /// <summary>
        /// Gets or sets TODO.
        /// </summary>
        public Texture AmbientOcclusionMap { get; set; }

        /// <summary>
        /// Gets or sets TODO.
        /// </summary>
        public Texture NormalMap { get; set; }

        /// <summary>
        /// Gets or sets TODO.
        /// </summary>
        public Texture HeightMap { get; set; }

        /// <summary>
        /// Gets or sets TODO.
        /// </summary>
        public Vector3 DiffuseColor { get; set; }

        /// <summary>
        /// Gets or sets TODO.
        /// </summary>
        public Vector3 SpecularColor { get; set; }

        /// <summary>
        /// TODO.
        /// </summary>
        /// <param name="isManualDispose"> PARAM TODO. </param>
        /// <exception cref="NotImplementedException"> EXCP. TODO. </exception>
        protected override void Dispose(bool isManualDispose)
        {
            if (!this.RemoveInstance(this.shaderType))
            {
                this.Shader.Dispose();
            }

            this.AlbedoMap.Dispose();
            this.SpecularMap.Dispose();
            this.AmbientOcclusionMap.Dispose();
            this.NormalMap.Dispose();
            this.HeightMap.Dispose();
        }

        /// <summary>
        /// TODO.
        /// </summary>
        /// <param name="shaderType"> PARAM TODO. </param>
        /// <returns> RETURN TODO. </returns>
        private bool AddInstance(ShaderType shaderType)
        {
            if (instancesCount[shaderType] < uint.MaxValue)
            {
                instancesCount[shaderType]++;
                return true;
            }
#if DEBUG
            Console.WriteLine("AddInstance(...) -> Could not add any instance.");
#endif
            return false;
        }

        /// <summary>
        /// TODO.
        /// </summary>
        /// <param name="shaderType"> PARAM TODO. </param>
        /// <returns> RETURN TODO. </returns>
        private bool RemoveInstance(ShaderType shaderType)
        {
            if (instancesCount[shaderType] > uint.MinValue)
            {
                instancesCount[shaderType]--;
                return true;
            }
#if DEBUG
            else
            {
                Console.WriteLine("RemoveInstance(...) -> There is no instance to be removed.");
            }
#endif
            return false;
        }
    }
}
