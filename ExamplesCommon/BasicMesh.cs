// <copyright file="BasicMesh.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace ExamplesCommon
{
    using Assimp;
    using Framework.Core.Buffer;
    using Framework.Core.Resource;
    using Framework.Core.Vertex;
    using OpenTK.Graphics.OpenGL4;

    /// <summary>
    /// 
    /// </summary>
    public class BasicMesh : ResourceObject
    {
        #region (Data Fields)

        private static string rootPath = string.Empty;

        private VertexArrayObject vao;

        private VertexBufferObject positionVbo;

        private VertexBufferObject colorNormalTexCoordVbo;

        private ElementBufferObject ebo;

        #endregion

        #region (Constructors)

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="invertUv"></param>
        public BasicMesh(string filePath,
                         bool invertUv = false) : base(filePath.ToString(), 0)
        {
            filePath = rootPath + filePath;
            // Create assimp context (vertex data saved into the 3d model)
            var context = new AssimpContext();
            // Loads the data into a "scene"
            var scene = context.ImportFile(filePath,
                                           PostProcessSteps.Triangulate |
                                           PostProcessSteps.GenerateSmoothNormals |
                                           PostProcessSteps.FlipUVs);

            // Arrays p/ guardar copia dos dados na RAM
            var positions = new List<float>();
            var colors = new List<Color3D>();
            var uvs = new List<Vector2D>();
            var normals = new List<Vector3D>();
            var indices = new List<int>();

            // Loads the vertex data into the lists
            foreach (var mesh in scene.Meshes)
            {
                for (int i = 0; i < mesh.VertexCount; i++)
                {
                    positions.AddRange(new[]
                    { mesh.Vertices[i].X, mesh.Vertices[i].Y, mesh.Vertices[i].Z });

                    if (mesh.HasVertexColors(0))
                        colors.Add(new Color3D(mesh.VertexColorChannels[0][i].R,
                                               mesh.VertexColorChannels[0][i].G,
                                               mesh.VertexColorChannels[0][i].B));
                    else
                        colors.Add(new Color3D(1.0f, 1.0f, 1.0f));

                    if (mesh.HasTextureCoords(0))
                        uvs.Add(new Vector2D(mesh.TextureCoordinateChannels[0][i].X,
                                             mesh.TextureCoordinateChannels[0][i].Y));
                    else
                        uvs.Add(new Vector2D(0.0f, 0.0f));

                    normals.Add(mesh.Normals[i]);
                }

                for (int i = 0; i < mesh.FaceCount; i++)
                {
                    var face = mesh.Faces[i];
                    indices.AddRange(new[]
                    { face.Indices[0], face.Indices[1], face.Indices[2] });
                }
            }

            // Create interleaved buffer for colors, uvs and normals
            var interleaved = new List<float>();
            for (int i = 0; i < positions.Count / 3; i++)
            {
                interleaved.AddRange(new[] { colors[i].R, colors[i].G, colors[i].B });
                interleaved.AddRange(new[] { uvs[i].X, uvs[i].Y });
                interleaved.AddRange(new[] { normals[i].X, normals[i].Y, normals[i].Z });
            }

            positionVbo = new VertexBufferObject(positions.ToArray());
            colorNormalTexCoordVbo = new VertexBufferObject(interleaved.ToArray());

            // Configura um formato de vértice adequado para receber os dados.
            // No caso presente há um buffer isolado com as posições e um buffer
            // enrelaçado com dados de cor, uvs e normais.
            VertexFormat format = new VertexFormat();
            format.AddAttribute(positionVbo, VertexAttributeType.Position);
            format.AddAttributesGroup(colorNormalTexCoordVbo,
                                      VertexAttributeType.Color,
                                      VertexAttributeType.TexCoord0,
                                      VertexAttributeType.Normal);
#if DEBUG
            Console.WriteLine("BasicMesh layout setup:\n");
            format.PrintLayout();
            Console.WriteLine("\n");
#endif

            vao = new VertexArrayObject(format);

            ebo = new ElementBufferObject(indices.ToArray());
        }

        #endregion

        #region (Properties)

        /// <summary>
        /// Caminho para a pasta raiz para carregar arquivos de Shader.
        /// </summary>
        public static string RootPath { get { return rootPath; } set { rootPath = value; } }

        #endregion

        #region (Public Methods)

        /// <summary>
        /// 
        /// </summary>
        public void Draw()
        {
            GL.BindVertexArray(vao.ID);
            GL.DrawElements(BeginMode.Triangles,
                            ebo.IndexCount,
                            DrawElementsType.UnsignedInt,
                            0);
        }

        #endregion

        /// <inheritdoc/>
        protected override void Dispose(bool isManualDispose)
        {
            vao.Dispose();
            positionVbo.Dispose();
            colorNormalTexCoordVbo.Dispose();
            ebo.Dispose();
        }
    }
}
