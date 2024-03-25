using OpenTK.Graphics.OpenGL4;

namespace Framework.Core.Vertex
{
    /// <summary>
    /// Fornece os dados necessários para adicionar suporte aos atributos listados.
    /// </summary>
    public class Attribute
    {
        /// <summary>
        /// Enumeração dos atributos de vértice que são possíveis.
        /// </summary>
        public enum Type
        {
            Position,
            TexCoord_0,
            Normal,
            Tangent,
            Color,
            TexCoord_1,

            // TODO: BlendWeight, BlendIndices
        }

        /// <summary>
        /// 
        /// </summary>
        public static readonly VertexAttribPointerType[] DataType =
        {
            VertexAttribPointerType.Float,              // Position
            VertexAttribPointerType.Float,              // TexCoord_0
            VertexAttribPointerType.Float,              // Normal
            VertexAttribPointerType.Float,              // Tangent
            VertexAttribPointerType.UnsignedByte,       // Color
            VertexAttribPointerType.Float,              // TexCoord_1

            // TODO: BlendWeight = Float, BlendIndices = Int
        };

        /// <summary>
        /// 
        /// </summary>
        public static readonly UInt32[] Count =
        {
            3,                          // Position
            2,                          // TexCoord_0
            3,                          // Normal
            3,                          // Tangent
            3,                          // Color
            2,                          // TexCoord_1

            // TODO: BlendWeight = 1, BlendIndices = 2
        };

        /// <summary>
        /// 
        /// </summary>
        public static readonly UInt32[] Size =
{
            sizeof(float),                          // Position
            sizeof(float),                          // TexCoord_0
            sizeof(float),                          // Normal
            sizeof(float),                          // Tangent
            sizeof(byte),                           // Color
            sizeof(float),                          // TexCoord_1

            // TODO: BlendWeight = sizeof(float), sizeof(int)
        };
    }
}
