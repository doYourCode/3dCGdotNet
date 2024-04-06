// <copyright file="Vector3Attribute.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Framework.Core.Material.Attributes
{
    using System.Numerics;
    using OpenTK.Graphics.OpenGL4;

    /// <summary>
    /// TODO.
    /// </summary>
    public class Vector3Attribute : MaterialAttribute
    {
        private Vector3 value;

        /// <summary>
        /// Initializes a new instance of the <see cref="Vector3Attribute"/> class.
        /// </summary>
        /// <param name="label"> PARAM TODO. </param>
        /// <param name="value"> PARAM2 TODO. </param>
        public Vector3Attribute(string label, System.Numerics.Vector3 value)
            : base(label)
        {
            this.value = value;

            this.UpdateUniform();
        }

        /// <inheritdoc/>
        public override void UpdateUniform()
        {
            GL.Uniform3(this.UniformLocation, this.value.X, this.value.Y, this.value.Z);
        }

        /// <inheritdoc/>
        public override ref Vector3 GetVector3Ref()
        {
            return ref this.value;
        }
    }
}