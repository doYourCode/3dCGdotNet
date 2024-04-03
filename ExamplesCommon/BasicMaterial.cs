// <copyright file="BasicMaterial.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace ExamplesCommon
{
    using Framework.Core;
    using Framework.Core.Material;
    using OpenTK.Mathematics;

    /// <summary>
    /// 
    /// </summary>
    public class BasicMaterial : Material
    {
        private static Dictionary<ShaderType, uint> instancesCount =
            new Dictionary<ShaderType, uint>()
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
        /// <param name="shaderType"></param>
        /// <param name="format"></param>
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
        /// 
        /// </summary>
        public Texture AlbedoMap { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Texture SpecularMap { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Texture AmbientOcclusionMap { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Texture NormalMap { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Texture HeightMap { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Vector3 DiffuseColor { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Vector3 SpecularColor { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ShaderType"></param>
        /// <returns></returns>
        private bool AddInstance(ShaderType ShaderType)
        {
            if (instancesCount[ShaderType] < uint.MaxValue)
            {
                instancesCount[ShaderType]++;
                return true;
            }
#if DEBUG
            Console.WriteLine("AddInstance(...) -> Could not add any instance.");
#endif
            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ShaderType"></param>
        /// <returns></returns>
        private bool RemoveInstance(ShaderType ShaderType)
        {
            if (instancesCount[ShaderType] > UInt32.MinValue)
            {
                instancesCount[ShaderType]--;
                return true;
            }
#if DEBUG
            else
                Console.WriteLine("RemoveInstance(...) -> There is no instance to be removed.");
#endif
            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="isManualDispose"></param>
        /// <exception cref="NotImplementedException"></exception>
        protected override void Dispose(bool isManualDispose)
        {
            if (!RemoveInstance(this.shaderType))
                Shader.Dispose();

            this.AlbedoMap.Dispose();
            this.SpecularMap.Dispose();
            this.AmbientOcclusionMap.Dispose();
            this.NormalMap.Dispose();
            this.HeightMap.Dispose();
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public enum ShaderType
    {
        Oren_Nayar,             //
        Phong,                  //
        Lambertian,             // TODO: implementar em um material mais robusto os tipos
        Half_Lambert,           // de materiais p/ carregar diferentes shaders
        Gouraud,                //
    }
}
