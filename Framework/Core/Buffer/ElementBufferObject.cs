// <copyright file="ElementBufferObject.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Framework.Core.Buffer
{
    using Framework.Core.Resource;
    using Framework.Utils;
    using OpenTK.Graphics.OpenGL4;

    /// <summary>
    /// Representa um buffer contendo os dados de índices para descrição de
    /// pontos, linhas, triângulos ou elementos geométricos semelhantes. Esse
    /// tipo de buffer será utilizado para desenhar utilizando a função
    /// GL.DrawElements(...) cujos índices usados para renderização indexada
    /// serão retirados do ElementBufferObject (EBO).
    /// <br />
    /// ATENÇÃO: esse objeto deve ser sempre vinculado a um VertexArrayObject
    /// (VAO), portanto uma chamada à função Bind() do VAO pretendido deve ser
    /// realizada antes de vincular o novo EBO.
    /// </summary>
    public class ElementBufferObject : OpenGLObject
    {
        private int indexCount = 0;

        /// <summary>
        /// Initializes a new instance of the <see cref="ElementBufferObject"/> class.
        /// </summary>
        /// <param name="indices"> Array de índices contendo os dados a serem
        /// enviados ao buffer.¹²
        /// <br />
        /// ATENÇÃO: ¹ certifique-se de que o array não é nulo. ² Na implementação
        /// atual só é permitido utilizar números inteiros como índices.
        /// </param>
        /// <param name="usage"> Indicativo de para que os dados do buffer serão usados.
        /// <br />
        /// Valores comuns: BufferUsageHint.StaticDraw |
        /// BufferUsageHint.DynamicDraw | BufferUsageHint.StreamDraw.
        /// Há outros valores possíveis, verifique as referências da API.
        /// </param>
        public ElementBufferObject(
            int[] indices,
            BufferUsageHint usage = BufferUsageHint.StaticDraw)
            : base("ElementBufferObject", (uint)GL.GenBuffer())
        {
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, this.ID);
            GL.BufferData(
                BufferTarget.ElementArrayBuffer,
                sizeof(uint) * indices.Length,
                indices,
                usage);

            this.indexCount = indices.Length;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ElementBufferObject"/> class.
        /// </summary>
        /// <param name="label"> Atenção. </param>
        /// <param name="indices"> Array de índices contendo os dados a serem
        /// enviados ao buffer.¹²
        /// <br />
        /// ATENÇÃO: ¹ certifique-se de que o array não é nulo. ² Na implementação
        /// atual só é permitido utilizar números inteiros como índices.
        /// </param>
        /// <param name="usage"> Indicativo de para que os dados do buffer serão
        /// usados.
        /// <br />
        /// Valores comuns: BufferUsageHint.StaticDraw | BufferUsageHint.DynamicDraw
        /// | BufferUsageHint.StreamDraw. Há outros valores possíveis, verifique as
        /// referências da API.
        /// </param>
        public ElementBufferObject(
            string label,
            int[] indices,
            BufferUsageHint usage = BufferUsageHint.StaticDraw)
            : base(label, (uint)GL.GenBuffer())
        {
            this.Bind();
            GL.BufferData(
                BufferTarget.ElementArrayBuffer,
                sizeof(uint) * indices.Length,
                indices,
                usage);

            this.indexCount = indices.Length;
        }

        /// <summary>
        /// Gets the buffer's total number of indices.
        /// </summary>
        public int IndexCount
        {
            get { return this.indexCount; } private set { }
        }

        /// <summary>
        /// Vincula o buffer para alteração ou desenho.
        /// </summary>
        public void Bind()
        {
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, this.ID);
        }

        /// <summary>
        /// Desvincula o buffer.
        /// </summary>
        public void Unbind()
        {
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, CONSTANTS.NONE);
        }

        /// <inheritdoc/>
        protected override void Dispose(bool isManualDispose)
        {
            this.Unbind();
            GL.DeleteBuffer(this.ID);
        }
    }
}
