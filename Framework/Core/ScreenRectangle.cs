using Framework.Core.Buffer;
using Framework.Core.Vertex;
using OpenTK.Mathematics;

namespace Framework.Core
{
    /// <summary>
    /// 
    /// </summary>
    public class ScreenRectangle
    {
        /* -------------------------------------------- Variáveis de classe -------------------------------------------- */
#if DEBUG
        /// <summary>
        /// Representa o quantitativo de EBOs existentes na VRAM.
        /// </summary>
        public static UInt32 Count { get { return count; } private set { } }


        private static UInt32 count = 0;

        private readonly Color4 defaultBackgroundColor = new Color4(0.0f, 0.0f, 0.0f, 1.0f);
#endif

        /* ---------------------------------------------- Variáveis membro ---------------------------------------------- */

        /// <summary>
        /// 
        /// </summary>
        public VertexArrayObject Vao { get { return vao; } private set { } }


        private VertexArrayObject vao;

        private VertexBufferObject posVbo;

        private VertexBufferObject uvVbo;


        /* ---------------------------------------------- Interface pública ---------------------------------------------- */

        /// <summary>
        /// 
        /// </summary>
        public ScreenRectangle()
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
            format.AddAttribute(uvVbo, VertexAttributeType.TexCoord_0);

            vao = new VertexArrayObject(format);
        }

        /// <summary>
        /// 
        /// </summary>
        public void Delete()
        {
            vao.Dispose();
            posVbo.Dispose();
            uvVbo.Dispose();
        }
    }
}
