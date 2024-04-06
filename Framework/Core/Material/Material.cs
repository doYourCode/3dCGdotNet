// <copyright file="Material.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Framework.Core.Material
{
    using Framework.Core.Resource;

    /// <summary>
    /// TODO.
    /// </summary>
    public class Material : ResourceObject, IDisposable
    {
        private Shader shader;

        private MaterialFormat format;

        /// <summary>
        /// Initializes a new instance of the <see cref="Material"/> class.
        /// TODO.
        /// </summary>
        /// <param name="format"> PARAM TODO. </param>
        /// <param name="label"> PARAM2 TODO. </param>
        /// <param name="iD"> PARAM3 TODO. </param>
        public Material(MaterialFormat format, string label, uint iD)
            : base(label, iD)
        {
            this.format = format;
        }

        /// <summary>
        /// Gets or Sets TODO.
        /// </summary>
        public Shader Shader { get => this.shader; set => this.shader = value; }

        /// <summary>
        /// Gets TODO.
        /// </summary>
        public MaterialFormat Format { get => this.format; }

        /// <summary>
        /// Gets TODO.
        /// </summary>
        public ref MaterialFormat FormatRef { get => ref this.format; }

        /// <summary>
        /// TODO.
        /// </summary>
        public void GetUniformLocations()
        {
            this.format.GetUniformLocations(this.shader);
        }

        /// <summary>
        /// TODO.
        /// </summary>
        public void UpdateUniforms()
        {
            this.format.UpdateUniforms();
        }

        /// <summary>
        /// TODO.
        /// </summary>
        /// <param name="isManualDispose"> PARAM TODO. </param>
        protected override void Dispose(bool isManualDispose)
        {
            base.Dispose(isManualDispose);
        }
    }
}
