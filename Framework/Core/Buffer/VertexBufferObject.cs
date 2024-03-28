using Framework.Core.Base;
using Framework.Utils;
using OpenTK.Graphics.OpenGL4;

namespace Framework.Core.Buffer
{
    /// <summary>
    /// Representa um buffer contendo os dados necessários para a descrição de um ou mais atributos de vértices.
    /// </summary>
    public class VertexBufferObject : ResourceObject
    {
        #region (Constructors)

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="Data"> Array de vértices contendo os dados a serem enviados ao buffer.¹²
        /// <br />
        /// ATENÇÃO: ¹ certifique-se de que o array não é nulo. ² Na implementação atual só é permitido utilizar
        /// números reais de precisão simples (float).
        /// </param>
        /// <param name="Usage"> Indicativo de para que os dados do buffer serão usados.
        /// <br />
        /// Valores comuns: BufferUsageHint.StaticDraw | BufferUsageHint.DynamicDraw | BufferUsageHint.StreamDraw.
        /// Há outros valores possíveis, verifique as referências da API.
        /// </param>
        public VertexBufferObject(float[] Data, BufferUsageHint Usage = BufferUsageHint.StaticDraw) : base("VertexBufferObject ", (UInt32)GL.GenBuffer())
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, id);

            GL.BufferData(BufferTarget.ArrayBuffer, sizeof(float) * Data.Length, Data, Usage);
        }

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="Data"> Array de vértices contendo os dados a serem enviados ao buffer.¹²
        /// <br />
        /// ATENÇÃO: ¹ certifique-se de que o array não é nulo. ² Na implementação atual só é permitido utilizar
        /// números reais de precisão simples (float).
        /// </param>
        /// <param name="Usage"> Indicativo de para que os dados do buffer serão usados.
        /// <br />
        /// Valores comuns: BufferUsageHint.StaticDraw | BufferUsageHint.DynamicDraw | BufferUsageHint.StreamDraw.
        /// Há outros valores possíveis, verifique as referências da API.
        /// </param>
        /// <param name="Label"> Rótulo identificador do VBO. </param>
        public VertexBufferObject(string Label, float[] Data, BufferUsageHint Usage = BufferUsageHint.StaticDraw) : base(Label, (UInt32)GL.GenBuffer())
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, id);

            GL.BufferData(BufferTarget.ArrayBuffer, sizeof(float) * Data.Length, Data, Usage);
        }

        #endregion

        #region (Public Methods)

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

        #endregion

        #region (Other Methods)

        /// <summary>
        /// Apaga os dados do buffer da VRAM.
        /// <br />
        /// ATENÇÃO: esta ação também desvincula o buffer.
        /// </summary>
        protected override void Dispose(bool isManualDispose)
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, CONSTANTS.NONE);
            GL.DeleteBuffer(id);
        }

        #endregion
    }
}
