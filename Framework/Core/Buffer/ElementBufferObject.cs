using Framework.Core.Base;
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
    public class ElementBufferObject : ResourceObject
    {
        #region (Data Fields)

        private int indexCount = 0;

        #endregion

        #region (Constructors)

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
        public ElementBufferObject(int[] Indices, BufferUsageHint Usage = BufferUsageHint.StaticDraw) : base("ElementBufferObject ", (UInt32)GL.GenBuffer())
        {
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, id);
            GL.BufferData(BufferTarget.ElementArrayBuffer, sizeof(uint) * Indices.Length, Indices, Usage);

            this.indexCount = Indices.Length;
        }

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
        public ElementBufferObject(string Label, int[] Indices, BufferUsageHint Usage = BufferUsageHint.StaticDraw) : base(Label, (UInt32)GL.GenBuffer())
        {
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, id);
            GL.BufferData(BufferTarget.ElementArrayBuffer, sizeof(uint) * Indices.Length, Indices, Usage);

            this.indexCount = Indices.Length;
        }

        #endregion

        #region (Properties)

        /// <summary>
        /// 
        /// </summary>
        public int IndexCount { get { return indexCount; } private set { } }

        #endregion

        #region (Public Methods)

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

        #endregion

        #region (Other Methods)

        /// <summary>
        /// Apaga os dados do buffer da VRAM.
        /// <br />
        /// ATENÇÃO: esta ação também desvincula o buffer.
        /// </summary>
        protected override void Dispose(bool isManualDispose)
        {
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, CONSTANTS.NONE);
            GL.DeleteBuffer(ID);
        }

        #endregion
    }
}
