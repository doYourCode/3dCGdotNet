using OpenTK.Graphics.OpenGL4;
using System.Numerics;

namespace Framework.Core.Light
{
    /// <summary>
    /// Tipos de luz: Directional | Spot | Point | Area
    /// </summary>
    enum LightType
    {
        Directional,    // TODO: implementar essas diferenças na classe Light e uma forma de refletir essa
        Spot,           // mudança nos shaders (shaders modulares? hot-loading? precisa pesquisar)
        Point,
        Area
    }


    /// <summary>
    /// Representação genérica de um ponto // direção que seja uma fonte de luz. Essa classe fornece os
    /// dados que utilizaremos nos shaders para escrever os algoritmos de efeitos de iluminação. (Ex: Lambert,
    /// Phong, Blinn, Oren-Nayar ou mesmo PBR shading)
    /// </summary>
    public class Light
    {
        /* -------------------------------------------- Variáveis de classe -------------------------------------------- */
#if DEBUG
        /// <summary>
        /// Representa o quantitativo de objetos do tipo Luz.
        /// </summary>
        public static UInt32 Count { get { return count; } private set { } }


        private static UInt32 count = 0;
#endif

        /* ---------------------------------------------- Variáveis membro ---------------------------------------------- */
        
        /// <summary>
        /// Posição do ponto de luz nos eixos X Y e Z.
        /// </summary>
        public Vector3 Position { get => position; set => position = value; }

        /// <summary>
        /// Direção da fonte de luz (em luzes direcionais) representadas por um vetor 3d.
        /// </summary>
        public Vector3 Direction { get => direction; set => direction = value; }

        /// <summary>
        /// Cor da luz representada pelos valores RGB. Obs: note que não há o canal alpha pois a intensidade 
        /// da luz é representada por outra variável desta mesma classe.
        /// </summary>
        public Vector3 Color { get => color; set => color = value; }

        /// <summary>
        /// Itensidade da fonte de luz. Obs: pode ser maior do que 1.0, atente-se para as formas corretas de renderizar
        /// high dynamic range (HDR).
        /// </summary>
        public float Intensity { get => intensity; set => intensity = value; }

        /// <summary>
        /// Liga ou desliga a projeção de sombras pela fonte de luz.
        /// <br />
        /// ATENÇÃO: este efeito tem forte influência sobre o desempenho do render, use-o com cautela.
        /// </summary>
        public bool CastShadow { get => castShadow; set => castShadow = value; } // TODO: implementar os efeitos de sombra


        private int positionUniformLocation;

        private int directionUniformLocation;

        private int colorUniformLocation;

        private int intensityUniformLocation;


        internal Vector3 position;

        internal Vector3 direction;

        internal Vector3 color;

        internal float intensity;

        internal bool castShadow;


        /* ---------------------------------------------- Interface pública ---------------------------------------------- */

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Position"></param>
        /// <param name="Color"></param>
        /// <param name="Direction"></param>
        /// <param name="Intensity"></param>
        /// <param name="CastShadow"></param>
        public Light(Vector3 Position, Vector3 Color, Vector3 Direction, float Intensity = 1.0f, bool CastShadow = false)
        {
            this.position = Position;
            this.direction = Direction;
            this.color = Color;
            this.intensity = Intensity;
            this.castShadow = CastShadow;
#if DEBUG
            Light.count++;
#endif
        }

        /// <summary>
        /// 
        /// </summary>
        public Light() : this(Vector3.Zero, Vector3.One, Vector3.Zero) { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Position"></param>
        /// <param name="Intensity"></param>
        /// <param name="CastShadow"></param>
        public Light(Vector3 Position, float Intensity = 1.0f, bool CastShadow = false) : this(Position, Vector3.One, Vector3.Zero, Intensity, CastShadow) { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Position"></param>
        /// <param name="Color"></param>
        /// <param name="Intensity"></param>
        /// <param name="CastShadow"></param>
        public Light(Vector3 Position, Vector3 Color, float Intensity = 1.0f, bool CastShadow = false) : this(Position, Color, Vector3.Zero, Intensity, CastShadow) { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="shader"></param>
        public void GetUniformLocations(Shader shader)
        {
            this.positionUniformLocation = GL.GetUniformLocation(shader.ID, "lightPosition");
            this.directionUniformLocation = GL.GetUniformLocation(shader.ID, "lightDirection");
            this.colorUniformLocation = GL.GetUniformLocation(shader.ID, "lightColor");
            this.intensityUniformLocation = GL.GetUniformLocation(shader.ID, "lightIntensity");
        }

        /// <summary>
        /// 
        /// </summary>
        public void UpdateUniforms()
        {
            GL.Uniform3(this.positionUniformLocation, this.position.X, this.position.Y, this.position.Z);
            GL.Uniform3(this.directionUniformLocation, this.direction.X, this.direction.Y, this.direction.Z);
            GL.Uniform3(this.colorUniformLocation, this.color.X, this.color.Y, this.color.Z);
            GL.Uniform1(this.intensityUniformLocation, this.intensity);
        }

        /// <summary>
        /// 
        /// </summary>
        public void Delete()
        {
#if DEBUG
            Light.count--;
#endif
        }
    }
}
