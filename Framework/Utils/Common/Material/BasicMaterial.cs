using Framework.Core;
using OpenTK.Mathematics;

namespace Framework.Utils.Common.Material
{
    /// <summary>
    /// 
    /// </summary>
    public class BasicMaterial
    {
        #region (Data Fields)

        private static Dictionary<ShaderType, UInt32> instancesCount = new Dictionary<ShaderType, UInt32>()
            {
                { ShaderType.Oren_Nayar_Blinn, 0 },
                { ShaderType.Phong, 0 },
                { ShaderType.Lambertian, 0 },
                { ShaderType.Half_Lambert, 0 },
                { ShaderType.Gouraud, 0 },
                { ShaderType.Cell_Shading, 0 }
            };

        private ShaderType shaderType;

        #endregion

        #region (Constructors)

        /// <summary>
        /// 
        /// </summary>
        public BasicMaterial(ShaderType ShaderType)
        {
            if(this.AddInstance(ShaderType))
            {
                Console.WriteLine(ShaderType.ToString());
                Shader = new Shader(ShaderType.ToString());
            }
            
        }

        #endregion

        #region (Properties)

        /// <summary>
        /// 
        /// </summary>
        public Shader Shader { get; private set; }

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
        public Texture AmbientocclusionMap { get; set; }

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

        #region (Public Methods)

        public void Delete()
        {
            if (!RemoveInstance(this.shaderType))
                Shader.Dispose();

            AlbedoMap.Dispose();
            SpecularMap.Dispose();
            AmbientocclusionMap.Dispose();
            NormalMap.Dispose();
            HeightMap.Dispose();
        }

        #endregion

        #region (Other Methods)

        private bool AddInstance(ShaderType ShaderType)
        {
            if(instancesCount[ShaderType] < UInt32.MaxValue)
            {
                instancesCount[ShaderType]++;
                return true;
            }
#if DEBUG
            Console.WriteLine("AddInstance(...) -> Could not add any instance.");
#endif
            return false;
        }

        private bool RemoveInstance(ShaderType ShaderType)
        {
            if (instancesCount[ShaderType] > UInt32.MinValue)
            {
                instancesCount[ShaderType]--;
                return true;
            }
#if DEBUG
            Console.WriteLine("RemoveInstance(...) -> There is no instance to be removed.");
#endif
            return false;
        }

        #endregion
    }

    #region (Enums)

    /// <summary>
    /// 
    /// </summary>
    public enum ShaderType
    {                                   
        Oren_Nayar_Blinn,       //
        Phong,                  //
        Lambertian,             // TODO: implementar em um material mais robusto os tipos
        Half_Lambert,           // de materiais p/ carregar diferentes shaders
        Gouraud,                //
        Cell_Shading,           //
    }

    #endregion
}
