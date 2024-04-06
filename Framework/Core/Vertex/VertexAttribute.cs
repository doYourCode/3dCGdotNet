// <copyright file="VertexAttribute.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Framework.Core.Vertex
{
    using OpenTK.Graphics.OpenGL4;

    /// <summary>
    /// Enumeração dos atributos de vértice que são possíveis.
    /// </summary>
    public enum VertexAttributeType
    {
        /// <summary>
        /// TODO.
        /// </summary>
        Position,

        /// <summary>
        /// TODO.
        /// </summary>
        TexCoord0,

        /// <summary>
        /// TODO.
        /// </summary>
        Normal,

        /// <summary>
        /// TODO.
        /// </summary>
        Tangent,

        /// <summary>
        /// TODO.
        /// </summary>
        Color,

        /// <summary>
        /// TODO.
        /// </summary>
        TexCoord1,
    }

    /// <summary>
    /// Fornece os dados necessários para adicionar suporte aos atributos listados.
    /// </summary>
    public class VertexAttribute
    {
        /// <summary>
        /// TODO.
        /// </summary>
        private static readonly VertexAttribPointerType[] GetDataType =
        {
            VertexAttribPointerType.Float,              // Position
            VertexAttribPointerType.Float,              // TexCoord_0
            VertexAttribPointerType.Float,              // Normal
            VertexAttribPointerType.Float,              // Tangent
            VertexAttribPointerType.Float,              // Color
            VertexAttribPointerType.Float,              // TexCoord_1
        };

        /// <summary>
        /// TODO.
        /// </summary>
        private static readonly uint[] GetSize =
        {
            3,                          // Position
            2,                          // TexCoord_0
            3,                          // Normal
            3,                          // Tangent
            3,                          // Color
            2,                          // TexCoord_1
        };

        /// <summary>
        /// TODO.
        /// </summary>
        private static readonly uint[] GetDataTypeSize =
        {
            sizeof(float),                          // Position
            sizeof(float),                          // TexCoord_0
            sizeof(float),                          // Normal
            sizeof(float),                          // Tangent
            sizeof(float),                           // Color
            sizeof(float),                          // TexCoord_1
        };

        private int layout;

        private VertexAttribPointerType dataType;

        private uint dataTypeSize;

        private uint size;

        /// <summary>
        /// Initializes a new instance of the <see cref="VertexAttribute"/> class.
        /// </summary>
        /// <param name="type"> PARAM TODO. </param>
        public VertexAttribute(VertexAttributeType type)
        {
            this.layout = (int)type;
            this.dataType = GetDataType[(int)type];
            this.dataTypeSize = GetDataTypeSize[(int)type];
            this.size = GetSize[(int)type];
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="VertexAttribute"/> class.
        /// </summary>
        /// <param name="layout"> PARAM TODO. </param>
        /// <param name="dataType"> PARAM2 TODO. </param>
        /// <param name="dataSize"> PARAM3 TODO. </param>
        /// <param name="dataTypeSizeInBytes"> PARAM4 TODO. </param>
        public VertexAttribute(
            int layout,
            VertexAttribPointerType dataType,
            uint dataSize,
            uint dataTypeSizeInBytes)
        {
            this.layout = layout;
            this.dataType = dataType;
            this.size = dataSize;
            this.dataTypeSize = dataTypeSizeInBytes;
        }

        /// <summary>
        /// Gets the layout index.
        /// </summary>
        public int Layout
        {
            get => (int)this.layout; private set { }
        }

        /// <summary>
        /// Gets TODO.
        /// </summary>
        public VertexAttribPointerType DataType
        {
            get => this.dataType; private set { }
        }

        /// <summary>
        /// Gets TODO.
        /// </summary>
        public uint Size
        {
            get => this.size; private set { }
        }

        /// <summary>
        /// Gets TODO.
        /// </summary>
        public uint SizeInBytes
        {
            get => this.size * this.dataTypeSize;

            private set { }
        }
    }
}
