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
        /* ---------------------------------------------- Variáveis membro ---------------------------------------------- */

        /// <summary>
        /// Id que reflete o endereço do buffer na VRAM
        /// </summary>
        public UInt32 ID;


        /* ---------------------------------------------- Interface pública ---------------------------------------------- */

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="indices"> Array de índices contendo os dados a serem enviados ao buffer.¹²
        /// <br />
        /// ATENÇÃO: ¹ certifique-se de que o array não é nulo. ² Na implementação atual só é permitido utilizar
        /// números inteiros como índices.
        /// </param>
        /// <param name="usage"> Indicativo de para que os dados do buffer serão usados.
        /// <br />
        /// Valores comuns: BufferUsageHint.StaticDraw / BufferUsageHint.DynamicDraw / BufferUsageHint.StreamDraw.
        /// Há outros valores possíveis, verifique as referências da API.
        /// </param>
        public ElementBufferObject(int[] indices, BufferUsageHint usage = BufferUsageHint.StaticDraw)
        {
            ID = (UInt32)GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, ID);
            GL.BufferData(BufferTarget.ElementArrayBuffer, sizeof(int) * indices.Length, indices, usage);
        }

        public void Bind()
        {
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, ID);
        }

        public void Unbind()
        {
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0);
        }

        public void Delete()
        {
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0);
            GL.DeleteBuffer(ID);
        }
    }
}
