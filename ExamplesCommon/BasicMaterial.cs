using Framework.Core;
using Framework.Core.Material;
using OpenTK.Mathematics;

namespace ExamplesCommon
{
    /// <summary>
    /// 
    /// </summary>
    public class BasicMaterial : Material
    {
        #region (Data Fields)

        private static Dictionary<ShaderType, UInt32> instancesCount =
            new Dictionary<ShaderType, UInt32>()
            {
                { ShaderType.Oren_Nayar, 0 },
                { ShaderType.Phong, 0 },
                { ShaderType.Lambertian, 0 },
                { ShaderType.Half_Lambert, 0 },
                { ShaderType.Gouraud, 0 },
            };

        private ShaderType shaderType;

        #endregion

        #region (Constructors)

        /// <summary>
        /// 
        /// </summary>
        public BasicMaterial(ShaderType ShaderType, MaterialFormat Format) : base(Format, ShaderType.ToString(), 0)
        {
            if (this.AddInstance(ShaderType))
            {
                Shader = new Shader(ShaderType.ToString());
            }

            AlbedoMap = new Texture("Albedo");
            SpecularMap = new Texture("Specular");
            AmbientOcclusionMap = new Texture("AO");
            NormalMap = new Texture("Normal");
            HeightMap = new Texture();

#if DEBUG
            Console.WriteLine("BasicMaterial layout setup:\n");
            Format.PrintLayout();
            Console.WriteLine("\n");
#endif
        }

        #endregion

        #region (Properties)

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

        #endregion

        #region (Other Methods)

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ShaderType"></param>
        /// <returns></returns>
        private bool AddInstance(ShaderType ShaderType)
        {
            if (instancesCount[ShaderType] < UInt32.MaxValue)
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

            AlbedoMap.Dispose();
            SpecularMap.Dispose();
            AmbientOcclusionMap.Dispose();
            NormalMap.Dispose();
            HeightMap.Dispose();
        }

        #endregion
    }

    #region (Enums)

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

    #endregion
}
