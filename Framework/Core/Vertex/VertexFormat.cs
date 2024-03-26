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
        public Dictionary<Attribute, VertexBufferObject> UniqueVertexAttributes { get => uniqueVertexAttributes; private set { } }

        /// <summary>
        /// 
        /// </summary>
        public Dictionary<Attribute, VertexBufferObject> InterleavedVertexAttributes { get => interleavedVertexAttributes; private set { } }

        /// <summary>
        /// 
        /// </summary>
        public Dictionary<Attribute, uint> InterleavedOffsets { get => interleavedOffsets; private set { } }

        /// <summary>
        /// 
        /// </summary>
        public uint InterleavedStride { get => interleavedStride; private set { } }


        private Dictionary<Attribute, VertexBufferObject> uniqueVertexAttributes;

        private Dictionary<Attribute, VertexBufferObject> interleavedVertexAttributes;

        private Dictionary<Attribute, UInt32> interleavedOffsets;

        private UInt32 interleavedStride = 0;


        /* ---------------------------------------------- Interface pública ---------------------------------------------- */

        /// <summary>
        /// 
        /// </summary>
        public VertexFormat()
        {
            uniqueVertexAttributes = new Dictionary<Attribute, VertexBufferObject>();

            interleavedVertexAttributes = new Dictionary<Attribute, VertexBufferObject>();

            interleavedOffsets = new Dictionary<Attribute, UInt32>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="AttribType"></param>
        /// <param name="Vbo"></param>
        public void AddAttribute(VertexAttributeType AttribType, VertexBufferObject Vbo)
        {
            this.uniqueVertexAttributes.Add(new Attribute(AttribType), Vbo);
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
                Attribute attribute = new Attribute(attribType);

                this.interleavedVertexAttributes.Add(attribute, Vbo);

                this.interleavedOffsets.Add(attribute, this.interleavedStride);

                this.interleavedStride += attribute.SizeInBytes;

                //temp += attribute.SizeInBytes;
            }
#if DEBUG
            Console.WriteLine("- Vertex stride: " + this.interleavedStride);
#endif
        }
    }
}
