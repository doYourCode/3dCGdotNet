// <copyright file="VertexBufferObject.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Framework.Core.Buffer
{
    using Framework.Core.Resource;
    using Framework.Utils;
    using OpenTK.Graphics.OpenGL4;

    /// <summary>
    /// Representa um buffer contendo os dados necessários para a descrição de um ou mais atributos de vértices.
    /// </summary>
    public class VertexBufferObject : OpenGLObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VertexBufferObject"/> class.
        /// </summary>
        /// <param name="data"> Array de vértices contendo os dados a serem enviados ao buffer.¹²
        /// <br />
        /// ATENÇÃO: ¹ certifique-se de que o array não é nulo. ² Na implementação atual só é permitido utilizar
        /// números reais de precisão simples (float).
        /// </param>
        /// <param name="usage"> Indicativo de para que os dados do buffer serão usados.
        /// <br />
        /// Valores comuns: BufferUsageHint.StaticDraw | BufferUsageHint.DynamicDraw | BufferUsageHint.StreamDraw.
        /// Há outros valores possíveis, verifique as referências da API.
        /// </param>
        public VertexBufferObject(float[] data, BufferUsageHint usage = BufferUsageHint.StaticDraw)
            : base("VertexBufferObject", (uint)GL.GenBuffer())
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, this.ID);

            GL.BufferData(BufferTarget.ArrayBuffer, sizeof(float) * data.Length, data, usage);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="VertexBufferObject"/> class.
        /// </summary>
        /// <param name="data"> Array de vértices contendo os dados a serem enviados ao buffer.¹²
        /// <br />
        /// ATENÇÃO: ¹ certifique-se de que o array não é nulo. ² Na implementação atual só é permitido utilizar
        /// números reais de precisão simples (float).
        /// </param>
        /// <param name="usage"> Indicativo de para que os dados do buffer serão usados.
        /// <br />
        /// Valores comuns: BufferUsageHint.StaticDraw | BufferUsageHint.DynamicDraw | BufferUsageHint.StreamDraw.
        /// Há outros valores possíveis, verifique as referências da API.
        /// </param>
        /// <param name="label"> Rótulo identificador do VBO. </param>
        public VertexBufferObject(
            string label,
            float[] data,
            BufferUsageHint usage = BufferUsageHint.StaticDraw)
            : base(label, (uint)GL.GenBuffer())
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, this.ID);

            GL.BufferData(BufferTarget.ArrayBuffer, sizeof(float) * data.Length, data, usage);
        }

        /// <summary>
        /// Vincula o buffer para alteração ou desenho.
        /// </summary>
        public void Bind()
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, this.ID);
        }

        /// <summary>
        /// Desvincula o buffer.
        /// </summary>
        public void Unbind()
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, CONSTANTS.NONE);
        }

        /// <inheritdoc/>
        protected override void Dispose(bool isManualDispose)
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, CONSTANTS.NONE);
            GL.DeleteBuffer(this.ID);
        }
    }
}
