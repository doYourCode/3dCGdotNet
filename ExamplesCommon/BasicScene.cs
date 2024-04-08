// <copyright file="BasicScene.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace ExamplesCommon
{
    using Assimp;
    using Framework.Core;
    using Framework.Core.Resource;

    /// <summary>
    /// A simple implementation of a scene using a list og scene elements.
    /// </summary>
    public class BasicScene : ResourceObject
    {
        private static string rootPath = string.Empty;

        private Dictionary<string, BasicMesh> sceneMeshes;

        private Dictionary<string, Transform> sceneTransforms;

        private Dictionary<string, BasicMaterial> sceneMaterials;

        private Dictionary<string, Shader> sceneShaders;

        /// <summary>
        /// Initializes a new instance of the <see cref="BasicScene"/> class.
        /// </summary>
        /// <param name="filePath">The path that points to the scene file.</param>
        public BasicScene(string filePath)
            : base(filePath.ToString(), 0)
        {
            filePath = rootPath + filePath;

            this.sceneMeshes = new Dictionary<string, BasicMesh>();
            this.sceneTransforms = new Dictionary<string, Transform>();
            this.sceneMaterials = new Dictionary<string, BasicMaterial>();
            this.sceneShaders = new Dictionary<string, Shader>();

            AssimpContext context = new ();

            Scene scene = context.ImportFile(
                filePath,
                PostProcessSteps.Triangulate | PostProcessSteps.GenerateSmoothNormals | PostProcessSteps.FlipUVs);

            uint meshCount = (uint)scene.RootNode.ChildCount;

            foreach (Node node in scene.RootNode.Children)
            {
                Transform transform = new ();

                Vector3D scale = new ();
                Assimp.Quaternion rotation = new ();
                Vector3D position = new ();

                node.Transform.Decompose(out scale, out rotation, out position);
                Console.WriteLine(node.Name);

                Console.WriteLine("Position: " + position);

                Console.WriteLine("Rotation: " + rotation);

                Console.WriteLine("Scale: " + scale);

                foreach (int i in node.MeshIndices)
                {
                    // TODO: add transform support
                    Mesh mesh = scene.Meshes[i];
                    this.sceneMeshes.Add(mesh.Name, new BasicMesh(mesh));
                    this.sceneTransforms.Add(mesh.Name, new Transform(position, rotation, scale));
                }
            }
        }

        /// <summary>
        /// Gets or sets a path for the Scene loading.
        /// </summary>
        public static string RootPath
        {
            get { return rootPath; }
            set { rootPath = value; }
        }

        /// <summary>
        /// Draws the whole scene.
        /// </summary>
        /// <param name="shader"> PARAM TODO. </param>
        public void Draw(Shader shader)
        {
            foreach (BasicMesh mesh in this.sceneMeshes.Values)
            {
                shader.SetMatrix4("model", this.sceneTransforms[mesh.Label].GetModelMatrix());
                mesh.Draw();
            }
        }
    }
}
