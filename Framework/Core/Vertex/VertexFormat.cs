using Framework.Core.Buffer;

namespace Framework.Core.Vertex
{
    /// <summary>
    /// 
    /// </summary>
    public class VertexFormat
    {
        /* ---------------------------------------------- Variáveis membro ---------------------------------------------- */

        /// <summary>
        /// Conjunto de atributos associados ao formato.
        /// </summary>
        public Dictionary<VertexAttribute, VertexBufferObject> UniqueVertexAttributes { get => uniqueVertexAttributes; private set { } }

        /// <summary>
        /// 
        /// </summary>
        public Dictionary<VertexAttribute, VertexBufferObject> InterleavedVertexAttributes { get => interleavedVertexAttributes; private set { } }

        /// <summary>
        /// 
        /// </summary>
        public Dictionary<VertexAttribute, uint> InterleavedOffsets { get => interleavedOffsets; private set { } }

        /// <summary>
        /// 
        /// </summary>
        public uint InterleavedStride { get => interleavedStride; private set { } }


        private Dictionary<VertexAttribute, VertexBufferObject> uniqueVertexAttributes;

        private Dictionary<VertexAttribute, VertexBufferObject> interleavedVertexAttributes;

        private Dictionary<VertexAttribute, UInt32> interleavedOffsets;

        private UInt32 interleavedStride = 0;


        /* ---------------------------------------------- Interface pública ---------------------------------------------- */

        /// <summary>
        /// 
        /// </summary>
        public VertexFormat()
        {
            uniqueVertexAttributes = new Dictionary<VertexAttribute, VertexBufferObject>();

            interleavedVertexAttributes = new Dictionary<VertexAttribute, VertexBufferObject>();

            interleavedOffsets = new Dictionary<VertexAttribute, UInt32>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="AttribType"></param>
        /// <param name="Vbo"></param>
        public void AddAttribute(VertexBufferObject Vbo, VertexAttributeType AttribType)
        {
            this.uniqueVertexAttributes.Add(new VertexAttribute(AttribType), Vbo);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Vbo"></param>
        /// <param name="attribs"></param>
        public void AddAttributesGroup(VertexBufferObject Vbo, params VertexAttributeType[] Attribs)
        {
            foreach(VertexAttributeType attribType in Attribs)
            {
#if DEBUG
                Console.WriteLine("- Attribute: " + attribType.ToString());
                Console.WriteLine("offset: " + this.interleavedStride);
#endif
                VertexAttribute attribute = new VertexAttribute(attribType);

                this.interleavedVertexAttributes.Add(attribute, Vbo);

                this.interleavedOffsets.Add(attribute, this.interleavedStride);

                this.interleavedStride += attribute.SizeInBytes;
            }
#if DEBUG
            Console.WriteLine("- Vertex stride: " + this.interleavedStride);
#endif
        }
    }
}
