using OpenTK.Mathematics;

namespace Framework.Core
{
    /// <summary>
    /// 
    /// </summary>
    public class Material
    {
        /* ---------------------------------------------- Variáveis membro ---------------------------------------------- */

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
        public Texture NormalMap { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Texture SpecularMap { get; set; }

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


        /* ---------------------------------------------- Interface pública ---------------------------------------------- */

        /// <summary>
        /// 
        /// </summary>
        public Material()
        {
            this.Shader = new Shader("Shaders/vertex.glsl", "Shaders/fragment.glsl");
        }

    }

    /// <summary>
    /// 
    /// </summary>
    public enum MaterialType
    {
        Metallic_PBR,
        Specular_PBR,
        Oren_Nayar_Blinn,
        Phong,
        Lambertian,
        Half_Lambert,
        Gouraud,            
        Cell_Shading,
    }
}
