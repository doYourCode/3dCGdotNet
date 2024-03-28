using Framework.Core.Base;
using Framework.Core.Vertex;
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
    public class VertexArrayObject : ResourceObject
    {
        #region (Constructors)

        /// <summary>
        /// 
        /// </summary>
        /// <param name="VertexFormat"></param>
        public VertexArrayObject(VertexFormat VertexFormat) : base("VertexArrayObject", (UInt32)GL.GenVertexArray())
        {
            this.Bind();

            this.Setup(VertexFormat);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="VertexFormat"></param>
        public VertexArrayObject(string Label, VertexFormat VertexFormat) : base(Label,(UInt32)GL.GenVertexArray())
        {
            this.Bind();

            this.Setup(VertexFormat);
        }

        #endregion

        #region (Public Methods)

        /// <summary>
        /// Associa um VBO arbitrário a um atributo relacionado ao VAO atual. Obs: esta é a maneira atual de anexar 
        /// VBOs a VAOs. Não é o único jeito, talvez não seja o melhor mas é fácil de aprender.
        /// </summary>
        /// <param name="Vbo"></param>
        /// <param name="Layout"></param>   // TODO: fornecer outras interfaces públicas para essa mesma tarefa
        /// <param name="Stride"></param>
        /// <param name="Offset"></param>
        public void LinkVBO(VertexBufferObject Vbo, int Layout, UInt32 Size, VertexAttribPointerType DataType, int Stride, int Offset)
        {
            Vbo.Bind();

            GL.VertexAttribPointer(Layout, (int)Size, DataType, false, Stride, Offset);
            GL.EnableVertexAttribArray(Layout);
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

        /* ---------------------------------------------- Métodos privados ---------------------------------------------- */

        /// <summary>
        /// 
        /// </summary>
        /// <param name="VertexFormat"></param>
        private void Setup(VertexFormat VertexFormat)
        {
            // Associa todos os buffera de atributos únicos, se existirem
            if (VertexFormat.UniqueVertexAttributes.Count > 0)
            {
                foreach (VertexAttribute attrib in VertexFormat.UniqueVertexAttributes.Keys)
                {
                    LinkVBO(VertexFormat.UniqueVertexAttributes.GetValueOrDefault(attrib),
                        attrib.Layout,
                        attrib.Size,
                        attrib.DataType,
                        0, 0
                    );
                }
            }

            // Associa todos os buffera de atributos entrelaçados, se existirem
            if (VertexFormat.InterleavedVertexAttributes.Count > 0)
            {
                foreach (VertexAttribute attrib in VertexFormat.InterleavedVertexAttributes.Keys)
                {
                    LinkVBO(VertexFormat.InterleavedVertexAttributes.GetValueOrDefault(attrib),
                        attrib.Layout,
                        attrib.Size,
                        attrib.DataType,
                        (int)VertexFormat.InterleavedStride,
                        (int)VertexFormat.InterleavedOffsets.GetValueOrDefault(attrib)
                    );
                }
            }
        }

        #endregion

        #region (Other Methods)

        /// <summary>
        /// 
        /// </summary>
        /// <param name="isManualDispose"></param>
        protected override void Dispose(bool isManualDispose)
        {
            GL.BindVertexArray(CONSTANTS.NONE);
            GL.DeleteVertexArray(id);
        }

        #endregion
    }
}
