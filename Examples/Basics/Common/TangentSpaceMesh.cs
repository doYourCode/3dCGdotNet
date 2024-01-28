using Assimp;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using System.Collections.Generic;

namespace _3dCG.Examples
{
    internal class TangentSpaceMesh
    {
        private static readonly byte POSITION = 0;
        private static readonly byte COLOR = 1;
        private static readonly byte UV = 2;
        private static readonly byte NORMAL = 3;
        private static readonly byte TANGENT = 4;
        private static readonly byte[] OFFSET = { 0, 0, 16, 24, 36 };
        private static readonly byte POSITION_COUNT = 3;
        private static readonly byte COLOR_COUNT = 4;
        private static readonly byte UV_COUNT = 2;
        private static readonly byte NORMAL_COUNT = 3;
        private static readonly byte TANGENT_COUNT = 3;
        private static readonly byte POSITION_SIZE = (byte)(POSITION_COUNT * sizeof(float));
        private static readonly byte DATA_SIZE = (byte)((COLOR_COUNT + UV_COUNT + NORMAL_COUNT + TANGENT_COUNT) * sizeof(float));

        private int _vao;
        private int _vertexBuffer;
        private int _colorUVNormalBuffer;
        private int _indexBuffer;
        private int _indexCount; // Total amount of triangles in the object

        public TangentSpaceMesh(string filePath)
        {
            // Create assimp context (vertex data saved into the 3d model)
            var context = new AssimpContext();
            // Loads the data into a "scene"
            var scene = context.ImportFile(filePath, PostProcessSteps.Triangulate | PostProcessSteps.GenerateSmoothNormals | PostProcessSteps.FlipUVs | PostProcessSteps.CalculateTangentSpace);

            var positions = new List<Vector3>();
            var colors = new List<Color4>();
            var uvs = new List<Vector2>();
            var normals = new List<Vector3>();
            var tangents = new List<Vector3>();
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
                        uvs.Add(mesh.TextureCoordinateChannels[0][i].ToOpenTK_UV());
                    else
                        uvs.Add(Vector2.Zero);

                    normals.Add(mesh.Normals[i].ToOpenTK());
                    tangents.Add(mesh.Tangents[i].ToOpenTK());
                }

                for (int i = 0; i < mesh.FaceCount; i++)
                {
                    var face = mesh.Faces[i];
                    indices.Add(face.Indices[0]);
                    indices.Add(face.Indices[1]);
                    indices.Add(face.Indices[2]);
                }
            }

            _indexCount = indices.Count;

            // Create interleaved buffer for colors, uvs and normals
            var interleaved = new List<float>();
            for (int i = 0; i < positions.Count; i++)
            {
                interleaved.AddRange(new[] { colors[i].R, colors[i].G, colors[i].B, colors[i].A });
                interleaved.AddRange(new[] { uvs[i].X, uvs[i].Y });
                interleaved.AddRange(new[] { normals[i].X, normals[i].Y, normals[i].Z });
                interleaved.AddRange(new[] { tangents[i].X, tangents[i].Y, tangents[i].Z });
            }

            // Create and bind VAO
            _vao = GL.GenVertexArray();
            GL.BindVertexArray(_vao);

            // Create and bind vertex position buffer
            _vertexBuffer = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBuffer);
            GL.BufferData(BufferTarget.ArrayBuffer, POSITION_SIZE * positions.Count, positions.ToArray(), BufferUsageHint.StaticDraw);
            GL.VertexAttribPointer(POSITION, POSITION_COUNT, VertexAttribPointerType.Float, false, 0, OFFSET[POSITION]);
            GL.EnableVertexAttribArray(POSITION);

            // Create and bind color/uv/normal buffer
            _colorUVNormalBuffer = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, _colorUVNormalBuffer);
            GL.BufferData(BufferTarget.ArrayBuffer, sizeof(float) * interleaved.Count, interleaved.ToArray(), BufferUsageHint.StaticDraw);
            GL.VertexAttribPointer(COLOR, COLOR_COUNT, VertexAttribPointerType.Float, false, DATA_SIZE, OFFSET[COLOR]);
            GL.EnableVertexAttribArray(COLOR);
            GL.VertexAttribPointer(UV, UV_COUNT, VertexAttribPointerType.Float, false, DATA_SIZE, OFFSET[UV]);
            GL.EnableVertexAttribArray(UV);
            GL.VertexAttribPointer(NORMAL, NORMAL_COUNT, VertexAttribPointerType.Float, false, DATA_SIZE, OFFSET[NORMAL]);
            GL.EnableVertexAttribArray(NORMAL);
            GL.VertexAttribPointer(TANGENT, TANGENT_COUNT, VertexAttribPointerType.Float, false, DATA_SIZE, OFFSET[TANGENT]);
            GL.EnableVertexAttribArray(TANGENT);

            // Create and bind index buffer (information about the faces triangulation)
            _indexBuffer = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, _indexBuffer);
            GL.BufferData(BufferTarget.ElementArrayBuffer, sizeof(int) * _indexCount, indices.ToArray(), BufferUsageHint.StaticDraw);

            // Unbind VAO
            GL.BindVertexArray(0);
        }

        public void Draw()
        {
            GL.BindVertexArray(_vao);
            GL.DrawElements(BeginMode.Triangles, _indexCount, DrawElementsType.UnsignedInt, 0);
            GL.BindVertexArray(0);
        }

        public void Delete()
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.BindVertexArray(0);
            GL.DeleteBuffer(_vertexBuffer);
            GL.DeleteBuffer(_colorUVNormalBuffer);
            GL.DeleteBuffer(_indexBuffer);
            GL.DeleteVertexArray(_vao);
        }
    }
}
