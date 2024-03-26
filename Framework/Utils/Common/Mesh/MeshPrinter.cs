using Assimp;
using OpenTK.Mathematics;

/*namespace Framework.Utils.Common.Mesh
{
    /// <summary>
    /// Expõe os dados de vértices para o formato de texto.
    /// </summary>
    public class MeshPrinter
    {
        public MeshPrinter(string filePath, bool invertUv = false)
        {
            // Create assimp context (vertex data saved into the 3d model)
            var context = new AssimpContext();
            // Loads the data into a "scene"
            var scene = context.ImportFile(filePath, PostProcessSteps.Triangulate | PostProcessSteps.GenerateSmoothNormals | PostProcessSteps.FlipUVs);

            // Arrays p/ guardar copia dos dados na RAM
            var positions = new List<Vector3>();
            var colors = new List<Color4>();
            var uvs = new List<Vector2>();
            var normals = new List<Vector3>();
            var indices = new List<int>();

            // Loads the vertex data into the lists
            foreach (var mesh in scene.Meshes)
            {
                for (int i = 0; i < mesh.VertexCount; i++)
                {
                    positions.Add(mesh.Vertices[i].ToOpenTK());

                    if (mesh.HasVertexColors(0))
                        colors.Add(mesh.VertexColorChannels[0][i].ToOpenTK());
                    else
                        colors.Add(Color4.White);

                    if (mesh.HasTextureCoords(0))
                        uvs.Add(mesh.TextureCoordinateChannels[0][i].ToOpenTK_UV(invertUv));
                    else
                        uvs.Add(Vector2.Zero);

                    normals.Add(mesh.Normals[i].ToOpenTK());
                }

                for (int i = 0; i < mesh.FaceCount; i++)
                {
                    var face = mesh.Faces[i];
                    indices.Add(face.Indices[0]);
                    indices.Add(face.Indices[1]);
                    indices.Add(face.Indices[2]);

                    for(int j = 0; j < 5; j++)
                    {
                        Console.Write(face.Indices[0] + "," + face.Indices[1] + "," + face.Indices[2] + ",");
                    }
                    Console.Write("\n");
                }
            }

            // Create interleaved buffer for colors, uvs and normals
            var interleaved = new List<float>();
            for (int i = 0; i < positions.Count; i++)
            {
                interleaved.AddRange(new[] { colors[i].R, colors[i].G, colors[i].B, colors[i].A });
                interleaved.AddRange(new[] { uvs[i].X, uvs[i].Y });
                interleaved.AddRange(new[] { normals[i].X, normals[i].Y, normals[i].Z });
            }
        }
    }
}*/
