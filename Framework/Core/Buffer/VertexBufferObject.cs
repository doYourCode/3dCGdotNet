using Framework.Utils;
using OpenTK.Graphics.OpenGL4;

namespace Framework.Core.Buffer
{
    /// <summary>
    /// Representa um buffer contendo os dados necessários para a descrição de um ou mais atributos de vértices.
    /// </summary>
    public class VertexBufferObject
    {
        /* ----------------------------------------- Variáveis de classe ----------------------------------------- */
#if DEBUG
        /// <summary>
        /// Representa o quantitativo de VBOs existentes na VRAM.
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

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="Vertices"> Array de vértices contendo os dados a serem enviados ao buffer.¹²
        /// <br />
        /// ATENÇÃO: ¹ certifique-se de que o array não é nulo. ² Na implementação atual só é permitido utilizar
        /// números reais de precisão simples (float).
        /// </param>
        /// <param name="Usage"> Indicativo de para que os dados do buffer serão usados.
        /// <br />
        /// Valores comuns: BufferUsageHint.StaticDraw | BufferUsageHint.DynamicDraw | BufferUsageHint.StreamDraw.
        /// Há outros valores possíveis, verifique as referências da API.
        /// </param>
        public VertexBufferObject(float[] Vertices, BufferUsageHint Usage = BufferUsageHint.StaticDraw)
        {
            id = (UInt32)GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, id);
            GL.BufferData(BufferTarget.ArrayBuffer, sizeof(float) * Vertices.Length, Vertices, Usage);
#if DEBUG
            VertexBufferObject.count++;
#endif
        }

        /// <summary>
        /// Vincula o buffer para alteração ou desenho.
        /// </summary>
        public void Bind()
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, id);
        }

        /// <summary>
        /// Desvincula o buffer.
        /// </summary>
        public void Unbind()
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, CONSTANTS.NONE);
        }

        /// <summary>
        /// Apaga os dados do buffer da VRAM.
        /// <br />
        /// ATENÇÃO: esta ação também desvincula o buffer.
        /// </summary>
        public void Delete()
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, CONSTANTS.NONE);
            GL.DeleteBuffer(id);
#if DEBUG
            VertexBufferObject.count--;
#endif
        }
    }
}
