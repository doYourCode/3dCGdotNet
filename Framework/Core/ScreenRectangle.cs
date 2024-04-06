// <copyright file="ScreenRectangle.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Framework.Core
{
    using Framework.Core.Buffer;
    using Framework.Core.Resource;
    using Framework.Core.Vertex;

    /// <summary>
    /// TODO.
    /// </summary>
    public class ScreenRectangle : ResourceObject
    {
        private VertexArrayObject vao;

        private VertexBufferObject posVbo;

        private VertexBufferObject uvVbo;

        /// <summary>
        /// Initializes a new instance of the <see cref="ScreenRectangle"/> class.
        /// TODO.
        /// </summary>
        public ScreenRectangle()
            : base("ScreenRectangle ", 0)
        {
            float[] positionData =
            {
#pragma warning disable SA1137 // Elements should have the same indentation

            // .2D positions (just ignore the third values
            //  X       Y       Z
                1.0f,  -1.0f,   0.0f,
               -1.0f,  -1.0f,   0.0f,
               -1.0f,   1.0f,   0.0f,

                1.0f,   1.0f,   0.0f,
                1.0f,  -1.0f,   0.0f,
               -1.0f,   1.0f,   0.0f,

#pragma warning restore SA1137 // Elements should have the same indentation
            };

            float[] texCoordData =
            {
            // .Texture coordinates
            //  U       V
                1.0f,   0.0f,
                0.0f,   0.0f,
                0.0f,   1.0f,

                1.0f,   1.0f,
                1.0f,   0.0f,
                0.0f,   1.0f,
            };

            this.posVbo = new VertexBufferObject(positionData);
            this.uvVbo = new VertexBufferObject(texCoordData);

            VertexFormat format = new VertexFormat();
            format.AddAttribute(this.posVbo, VertexAttributeType.Position);
            format.AddAttribute(this.uvVbo, VertexAttributeType.TexCoord0);

            this.vao = new VertexArrayObject(format);
        }

        /// <summary>
        /// Gets TODO.
        /// </summary>
        public VertexArrayObject Vao
        {
            get { return this.vao; } private set { }
        }

        /// <summary>
        /// TODO.
        /// </summary>
        /// <param name="isManualDispose"> PARAM TODO. </param>
        protected override void Dispose(bool isManualDispose)
        {
            this.vao.Dispose();
            this.posVbo.Dispose();
            this.uvVbo.Dispose();
        }
    }
}
