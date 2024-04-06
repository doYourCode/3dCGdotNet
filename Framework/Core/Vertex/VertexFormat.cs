// <copyright file="VertexFormat.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Framework.Core.Vertex
{
    using Framework.Core.Buffer;
    using OpenTK.Graphics.OpenGL4;

    /// <summary>
    /// TODO.
    /// </summary>
    public class VertexFormat
    {
        private Dictionary<VertexAttribute, VertexBufferObject> uniqueVertexAttributes;

        private Dictionary<VertexAttribute, VertexBufferObject> interleavedVertexAttributes;

        private Dictionary<VertexAttribute, uint> interleavedOffsets;

        private uint interleavedStride = 0;

        /// <summary>
        /// Initializes a new instance of the <see cref="VertexFormat"/> class.
        /// TODO.
        /// </summary>
        public VertexFormat()
        {
            this.uniqueVertexAttributes = new Dictionary<VertexAttribute, VertexBufferObject>();

            this.interleavedVertexAttributes = new Dictionary<VertexAttribute, VertexBufferObject>();

            this.interleavedOffsets = new Dictionary<VertexAttribute, uint>();
        }

        /// <summary>
        /// Gets TODO.
        /// </summary>
        public Dictionary<VertexAttribute, VertexBufferObject> UniqueVertexAttributes
        {
            get => this.uniqueVertexAttributes;
            private set { }
        }

        /// <summary>
        /// Gets TODO.
        /// </summary>
        public Dictionary<VertexAttribute, VertexBufferObject> InterleavedVertexAttributes
        {
            get => this.interleavedVertexAttributes;
            private set { }
        }

        /// <summary>
        /// Gets TODO.
        /// </summary>
        public Dictionary<VertexAttribute, uint> InterleavedOffsets
        {
            get => this.interleavedOffsets;
            private set { }
        }

        /// <summary>
        /// Gets TODO.
        /// </summary>
        public uint InterleavedStride
        {
            get => this.interleavedStride; private set { }
        }

        /// <summary>
        /// TODO.
        /// </summary>
        /// <param name="vbo"> PARAM2 TODO. </param>
        /// <param name="attribType"> PARAM TODO. </param>
        public void AddAttribute(VertexBufferObject vbo, VertexAttributeType attribType)
        {
            this.uniqueVertexAttributes.Add(new VertexAttribute(attribType), vbo);
        }

        /// <summary>
        /// TODO.
        /// </summary>
        /// <param name="vbo"> PARAM TODO. </param>
        /// <param name="attribs"> PARAM2 TODO. </param>
        public void AddAttributesGroup(
            VertexBufferObject vbo,
            params VertexAttributeType[] attribs)
        {
            foreach (VertexAttributeType attribType in attribs)
            {
                VertexAttribute attribute = new VertexAttribute(attribType);

                this.interleavedVertexAttributes.Add(attribute, vbo);

                this.interleavedOffsets.Add(attribute, this.interleavedStride);

                this.interleavedStride += attribute.SizeInBytes;
            }
        }

        /// <summary>
        /// TODO.
        /// </summary>
        /// <param name="vbo"> PARAM TODO. </param>
        /// <param name="layout"> PARAM2 TODO. </param>
        /// <param name="dataType"> PARAM3 TODO. </param>
        /// <param name="dataSize"> PARAM4 TODO. </param>
        /// <param name="dataTypeSizeInBytes"> PARAM5 TODO. </param>
        public void AddCustomAttribute(
            VertexBufferObject vbo,
            int layout,
            VertexAttribPointerType dataType,
            uint dataSize,
            uint dataTypeSizeInBytes)
        {
            VertexAttribute attrib = new VertexAttribute(layout, dataType, dataSize, dataTypeSizeInBytes);
            this.uniqueVertexAttributes.Add(attrib, vbo);
        }

        /// <summary>
        /// TODO.
        /// </summary>
        public void PrintLayout()
        {
            foreach (VertexAttribute attrib in this.uniqueVertexAttributes.Keys)
            {
                Console.WriteLine("layout(location = " + attrib.Layout + ") in vec" + attrib.Size + " v" + ((VertexAttributeType)attrib.Layout).ToString() + ";");
            }

            foreach (VertexAttribute attrib in this.interleavedVertexAttributes.Keys)
            {
                Console.WriteLine("layout(location = " + attrib.Layout + ") in vec" + attrib.Size + " v" + ((VertexAttributeType)attrib.Layout).ToString() + ";");
            }
        }
    }
}
