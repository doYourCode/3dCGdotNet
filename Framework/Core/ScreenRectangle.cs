using Framework.Core.Resource;
using Framework.Core.Buffer;
using Framework.Core.Vertex;

namespace Framework.Core
{
    /// <summary>
    /// 
    /// </summary>
    public class ScreenRectangle : ResourceObject
    {
        #region (Data Fields)

        private VertexArrayObject vao;

        private VertexBufferObject posVbo;

        private VertexBufferObject uvVbo;

        #endregion

        #region (Constructors)

        /// <summary>
        /// 
        /// </summary>
        public ScreenRectangle() : base("ScreenRectangle ", 0)
        {
            float[] positionData =
            {
            //  2D positions (just ignore the third values
		    //  X       Y       Z
		        1.0f,  -1.0f,   0.0f,
               -1.0f,  -1.0f,   0.0f,
               -1.0f,   1.0f,   0.0f,

                1.0f,   1.0f,   0.0f,
                1.0f,  -1.0f,   0.0f,
               -1.0f,   1.0f,   0.0f
            };

            float[] texCoordData =
            {
            //  Texture coordinates
            //  U       V
                1.0f,   0.0f,
                0.0f,   0.0f,
                0.0f,   1.0f,

                1.0f,   1.0f,
                1.0f,   0.0f,
                0.0f,   1.0f
            };

            posVbo = new VertexBufferObject(positionData);
            uvVbo = new VertexBufferObject(texCoordData);

            VertexFormat format = new VertexFormat();
            format.AddAttribute(posVbo, VertexAttributeType.Position);
            format.AddAttribute(uvVbo, VertexAttributeType.TexCoord0);

            vao = new VertexArrayObject(format);
        }

        #endregion

        #region (Properties)

        /// <summary>
        /// 
        /// </summary>
        public VertexArrayObject Vao { get { return vao; } private set { } }

        #endregion

        #region (Other Methods)

        /// <summary>
        /// 
        /// </summary>
        protected override void Dispose(bool isManualDispose)
        {
            vao.Dispose();
            posVbo.Dispose();
            uvVbo.Dispose();
        }

        #endregion
    }
}
