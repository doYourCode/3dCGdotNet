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

        private Dictionary<string, Transform> scenetransforms;

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
            this.scenetransforms = new Dictionary<string, Transform>();
            this.sceneMaterials = new Dictionary<string, BasicMaterial>();
            this.sceneShaders = new Dictionary<string, Shader>();

            AssimpContext context = new AssimpContext();

            Scene scene = context.ImportFile(
                filePath,
                PostProcessSteps.Triangulate | PostProcessSteps.GenerateSmoothNormals | PostProcessSteps.FlipUVs);

            foreach (Mesh mesh in scene.Meshes)
            {
                this.sceneMeshes.Add(mesh.Name, new BasicMesh(mesh));
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
        public void Draw()
        {
            foreach (BasicMesh mesh in this.sceneMeshes.Values)
            {
                mesh.Draw();
            }
        }
    }
}
