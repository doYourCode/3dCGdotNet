// <copyright file="VertexArrayObject.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Framework.Core.Buffer
{
    using Framework.Core.Resource;
    using Framework.Core.Vertex;
    using Framework.Utils;
    using OpenTK.Graphics.OpenGL4;

    /// <summary>
    /// Representa um container para associação de diferentes instâncias de VertexBufferObject (VBO) e permite o desenho
    /// utilizando-os também associados a um ElementBufferObject (EBO) com índices. Esse objeto permite realizar desenhos
    /// com diferentes atributos. Ex: renderização de passos de depth requerem apenas posição, enquanto o desenho final
    /// pode requerer outros atributos como UVs, Normal, Color etc.
    /// </summary>
    public class VertexArrayObject : OpenGLObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VertexArrayObject"/> class.
        /// TODO.
        /// </summary>
        /// <param name="vertexFormat"> PARAM TODO. </param>
        public VertexArrayObject(VertexFormat vertexFormat)
            : base("VertexArrayObject", (uint)GL.GenVertexArray())
        {
            this.Bind();

            this.Setup(vertexFormat);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="VertexArrayObject"/> class.
        /// TODO.
        /// </summary>
        /// <param name="label"> PARAM TODO. </param>
        /// <param name="vertexFormat"> PARAM2 TODO. </param>
        public VertexArrayObject(string label, VertexFormat vertexFormat)
            : base(label, (uint)GL.GenVertexArray())
        {
            this.Bind();

            this.Setup(vertexFormat);
        }

        /// <summary>
        /// TODO.
        /// </summary>
        /// <param name="vbo"> PARAM TODO. </param>
        /// <param name="layout"> PARAM2 TODO. </param>
        /// <param name="size"> PARAM3 TODO. </param>
        /// <param name="dataType"> PARAM4 TODO. </param>
        /// <param name="stride"> PARAM5 TODO. </param>
        /// <param name="offset"> PARAM6 TODO. </param>
        public void LinkVBO(
            VertexBufferObject vbo,
            int layout,
            uint size,
            VertexAttribPointerType dataType,
            int stride,
            int offset)
        {
            vbo.Bind();

            GL.VertexAttribPointer(layout, (int)size, dataType, false, stride, offset);
            GL.EnableVertexAttribArray(layout);
        }

        /// <summary>
        /// Vincula o VAO para alteração ou desenho.
        /// </summary>
        public void Bind()
        {
            GL.BindVertexArray(this.ID);
        }

        /// <summary>
        /// Desvincula o VAO.
        /// </summary>
        public void Unbind()
        {
            GL.BindVertexArray(CONSTANTS.NONE);
        }

        /// <summary>
        /// TODO.
        /// </summary>
        /// <param name="isManualDispose"> PARAM TODO. </param>
        protected override void Dispose(bool isManualDispose)
        {
            GL.BindVertexArray(CONSTANTS.NONE);
            GL.DeleteVertexArray(this.ID);
        }

        /// <summary>
        /// TODO.
        /// </summary>
        /// <param name="vertexFormat"> PARAM TODO. </param>
        private void Setup(VertexFormat vertexFormat)
        {
            // Associa todos os buffera de atributos únicos, se existirem
            if (vertexFormat.UniqueVertexAttributes.Count > 0)
            {
                foreach (VertexAttribute attrib in vertexFormat.UniqueVertexAttributes.Keys)
                {
#pragma warning disable CS8604 // Possible null reference argument.
                    this.LinkVBO(
                        vertexFormat.UniqueVertexAttributes.GetValueOrDefault(attrib),
                        attrib.Layout,
                        attrib.Size,
                        attrib.DataType,
                        0,
                        0);
#pragma warning restore CS8604 // Possible null reference argument.
                }
            }

            // Associa todos os buffera de atributos entrelaçados, se existirem
            if (vertexFormat.InterleavedVertexAttributes.Count > 0)
            {
                foreach (VertexAttribute attrib in vertexFormat.InterleavedVertexAttributes.Keys)
                {
#pragma warning disable CS8604 // Possible null reference argument.
                    this.LinkVBO(
                        vertexFormat.InterleavedVertexAttributes.GetValueOrDefault(attrib),
                        attrib.Layout,
                        attrib.Size,
                        attrib.DataType,
                        (int)vertexFormat.InterleavedStride,
                        (int)vertexFormat.InterleavedOffsets.GetValueOrDefault(attrib));
#pragma warning restore CS8604 // Possible null reference argument.
                }
            }
        }
    }
}
