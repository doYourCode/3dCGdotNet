using Assimp;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

namespace Framework.Utils.Common.Mesh
{
    public class MeshPrinter
    {
        private static readonly byte POSITION = 0;
        private static readonly byte COLOR = 1;
        private static readonly byte UV = 2;
        private static readonly byte NORMAL = 3;
        private static readonly byte[] OFFSET = { 0, 0, 16, 24 };
        private static readonly byte POSITION_COUNT = 3;
        private static readonly byte COLOR_COUNT = 4;
        private static readonly byte UV_COUNT = 2;
        private static readonly byte NORMAL_COUNT = 3;
        private static readonly byte POSITION_SIZE = (byte)(POSITION_COUNT * sizeof(float));
        private static readonly byte DATA_SIZE = (byte)((COLOR_COUNT + UV_COUNT + NORMAL_COUNT) * sizeof(float));

        private int vao;
        private int vertexBuffer;
        private int colorUVNormalBuffer;
        private int indexBuffer;
        private int indexCount; // Total amount of triangles in the object

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

            indexCount = indices.Count;

            // Create interleaved buffer for colors, uvs and normals
            var interleaved = new List<float>();
            for (int i = 0; i < positions.Count; i++)
            {
                interleaved.AddRange(new[] { colors[i].R, colors[i].G, colors[i].B, colors[i].A });
                interleaved.AddRange(new[] { uvs[i].X, uvs[i].Y });
                interleaved.AddRange(new[] { normals[i].X, normals[i].Y, normals[i].Z });
            }

            // Create and bind VAO
            vao = GL.GenVertexArray();
            GL.BindVertexArray(vao);

            // Buffers p/ guardar copia dos dados na RAM

            // Create and bind vertex position buffer
            vertexBuffer = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, vertexBuffer);
            GL.BufferData(BufferTarget.ArrayBuffer, POSITION_SIZE * positions.Count, positions.ToArray(), BufferUsageHint.StaticDraw);
            GL.VertexAttribPointer(POSITION, POSITION_COUNT, VertexAttribPointerType.Float, false, 0, OFFSET[POSITION]);
            GL.EnableVertexAttribArray(POSITION);

            // Create and bind color/uv/normal buffer
            colorUVNormalBuffer = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, colorUVNormalBuffer);
            GL.BufferData(BufferTarget.ArrayBuffer, sizeof(float) * interleaved.Count, interleaved.ToArray(), BufferUsageHint.StaticDraw);
            GL.VertexAttribPointer(COLOR, COLOR_COUNT, VertexAttribPointerType.Float, false, DATA_SIZE, OFFSET[COLOR]);
            GL.EnableVertexAttribArray(COLOR);
            GL.VertexAttribPointer(UV, UV_COUNT, VertexAttribPointerType.Float, false, DATA_SIZE, OFFSET[UV]);
            GL.EnableVertexAttribArray(UV);
            GL.VertexAttribPointer(NORMAL, NORMAL_COUNT, VertexAttribPointerType.Float, false, DATA_SIZE, OFFSET[NORMAL]);
            GL.EnableVertexAttribArray(NORMAL);

            // Create and bind index buffer (information about the faces triangulation)
            indexBuffer = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, indexBuffer);
            GL.BufferData(BufferTarget.ElementArrayBuffer, sizeof(int) * indexCount, indices.ToArray(), BufferUsageHint.StaticDraw);

            // Unbind VAO
            GL.BindVertexArray(0);
        }

        public void Draw()
        {
            GL.BindVertexArray(vao);
            GL.DrawElements(BeginMode.Triangles, indexCount, DrawElementsType.UnsignedInt, 0);
            GL.BindVertexArray(0);
        }

        public void Delete()
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.BindVertexArray(0);
            GL.DeleteBuffer(vertexBuffer);
            GL.DeleteBuffer(colorUVNormalBuffer);
            GL.DeleteBuffer(indexBuffer);
            GL.DeleteVertexArray(vao);
        }
    }
}
