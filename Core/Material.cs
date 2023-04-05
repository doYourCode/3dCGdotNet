using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Text;

namespace _3dCG.Core
{
    internal class Material
    {
        // TODO

        private Shader Shader { get; set; }
        private Texture Albedo { get; set; }
        private Texture NormalMap { get; set; }
        private Texture SpecularMap { get; set; }
        private Texture HeightMap { get; set; }

        private Vector3 DiffuseColor { get; set; }

        public Material()
        {
            //this.Shader = new Shader("Shaders/vertex.glsl", "Shaders/fragment.glsl");
        }

    }
}
