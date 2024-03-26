using Framework.Utils;
using OpenTK.Graphics.OpenGL4;

namespace Framework.Core.Buffer
{
    /// <summary>
    /// Representa um buffer contendo os dados de índices para descrição de pontos, linhas, triângulos ou
    /// elementos geométricos semelhantes. Esse tipo de buffer será utilizado para desenhar utilizando a
    /// função GL.DrawElements(...) cujos índices usados para renderização indexada serão retirados do
    /// ElementBufferObject (EBO).
    /// <br />
    /// ATENÇÃO: esse objeto deve ser sempre vinculado a um VertexArrayObject (VAO), portanto uma chamada à
    /// função Bind() do VAO pretendido deve ser realizada antes de vincular o novo EBO.
    /// </summary>
    public class ElementBufferObject
    {
        /* ----------------------------------------- Variáveis de classe ----------------------------------------- */
#if DEBUG
        /// <summary>
        /// Representa o quantitativo de EBOs existentes na VRAM.
        /// </summary>
        public static UInt32 Count { get { return count; } private set { } }


        private static UInt32 count = 0;
#endif

        /* ---------------------------------------------- Variáveis membro ---------------------------------------------- */

        /// <summary>
        /// Id que reflete o endereço do buffer na VRAM
        /// </summary>
        public UInt32 ID { get { return id; } private set { } }

        /// <summary>
        /// 
        /// </summary>
        public int IndexSize { get { return indexSize; } private set { } }


        private UInt32 id;

        private int indexSize = 0;


        /* ---------------------------------------------- Interface pública ---------------------------------------------- */

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="Indices"> Array de índices contendo os dados a serem enviados ao buffer.¹²
        /// <br />
        /// ATENÇÃO: ¹ certifique-se de que o array não é nulo. ² Na implementação atual só é permitido utilizar
        /// números inteiros como índices.
        /// </param>
        /// <param name="Usage"> Indicativo de para que os dados do buffer serão usados.
        /// <br />
        /// Valores comuns: BufferUsageHint.StaticDraw | BufferUsageHint.DynamicDraw | BufferUsageHint.StreamDraw.
        /// Há outros valores possíveis, verifique as referências da API.
        /// </param>
        public ElementBufferObject(int[] Indices, VertexArrayObject vao, BufferUsageHint Usage = BufferUsageHint.StaticDraw)
        {
            vao.Bind();

            ID = (UInt32)GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, ID);
            GL.BufferData(BufferTarget.ElementArrayBuffer, sizeof(uint) * Indices.Length, Indices, Usage);

            this.indexSize = Indices.Length;
#if DEBUG
            ElementBufferObject.count++;
#endif
        }

        /// <summary>
        /// Vincula o buffer para alteração ou desenho.
        /// </summary>
        public void Bind()
        {
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, ID);
        }

        /// <summary>
        /// Desvincula o buffer.
        /// </summary>
        public void Unbind()
        {
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, CONSTANTS.NONE);
        }

        /// <summary>
        /// Apaga os dados do buffer da VRAM.
        /// <br />
        /// ATENÇÃO: esta ação também desvincula o buffer.
        /// </summary>
        public void Delete()
        {
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, CONSTANTS.NONE);
            GL.DeleteBuffer(ID);
#if DEBUG
            ElementBufferObject.count--;
#endif
        }
    }
}
