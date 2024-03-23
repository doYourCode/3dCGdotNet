using Framework.Utils;
using OpenTK.Graphics.OpenGL4;

namespace Framework.Core.Buffer
{
    /// <summary>
    /// Representa um container para associação de diferentes instâncias de VertexBufferObject (VBO) e permite o desenho
    /// utilizando-os também associados a um ElementBufferObject (EBO) com índices. Esse objeto permite realizar desenhos
    /// com diferentes atributos. Ex: renderização de passos de depth requerem apenas posição, enquanto o desenho final
    /// pode requerer outros atributos como UVs, Normal, Color etc.
    /// </summary>
    public class VertexArrayObject
    {
        /* ----------------------------------------- Variáveis de classe ----------------------------------------- */
#if DEBUG
        /// <summary>
        /// Representa o quantitativo de VAOs existentes na VRAM.
        /// </summary>
        public static UInt32 Count { get { return count; } private set { } }


        private static UInt32 count = 0;
#endif

        /* ---------------------------------------------- Variáveis membro ---------------------------------------------- */

        /// <summary>
        /// Id que reflete o endereço do buffer na VRAM
        /// </summary>
        public UInt32 ID { get { return id; } private set { } }


        private UInt32 id;


        /* ---------------------------------------------- Interface pública ---------------------------------------------- */

        public VertexArrayObject()
        {
            id = (UInt32)GL.GenVertexArray();
#if DEBUG
            VertexArrayObject.count++;
#endif
        }

        /// <summary>
        /// Associa um VBO arbitrário a um atributo relacionado ao VAO atual. Obs: esta é a maneira atual de anexar 
        /// VBOs a VAOs. Não é o único jeito, talvez não seja o melhor mas é fácil de aprender.
        /// </summary>
        /// <param name="Vbo"></param>
        /// <param name="Layout"></param>   // TODO: fornecer outras interfaces públicas para essa mesma tarefa
        /// <param name="Stride"></param>
        /// <param name="Offset"></param>
        public void LinkVBO(VertexBufferObject Vbo, int Layout, int Stride, int Offset)
        {
            Vbo.Bind();

            GL.VertexAttribPointer(Layout, 3, VertexAttribPointerType.Float, false, Stride, Offset);
            GL.EnableVertexAttribArray(Layout);

            Vbo.Unbind();
        }

        /// <summary>
        /// Vincula o VAO para alteração ou desenho.
        /// </summary>
        public void Bind()
        {
            GL.BindVertexArray(id);
        }

        /// <summary>
        /// Desvincula o VAO.
        /// </summary>
        public void Unbind()
        {
            GL.BindVertexArray(CONSTANTS.NONE);
        }

        /// <summary>
        /// Apaga os dados do VAO da VRAM.
        /// <br />
        /// ATENÇÃO: esta ação também desvincula o VAO.
        /// </summary>
        public void Delete()
        {
            GL.BindVertexArray(CONSTANTS.NONE);
            GL.DeleteVertexArray(id);
#if DEBUG
            VertexArrayObject.count--;
#endif
        }
    }
}
