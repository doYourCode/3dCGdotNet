// <copyright file="Vector4Attribute.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Framework.Core.Material.Attributes
{
    using System.Numerics;
    using OpenTK.Graphics.OpenGL4;

    /// <summary>
    /// TODO.
    /// </summary>
    public class Vector4Attribute : MaterialAttribute
    {
        private Vector4 value;

        /// <summary>
        /// Initializes a new instance of the <see cref="Vector4Attribute"/> class.
        /// </summary>
        /// <param name="label"> PARAM TODO. </param>
        /// <param name="value"> PARAM2 TODO. </param>
        public Vector4Attribute(string label, System.Numerics.Vector4 value)
            : base(label)
        {
            this.value = value;

            this.UpdateUniform();
        }

        /// <inheritdoc/>
        public override void UpdateUniform()
        {
            GL.Uniform4(this.UniformLocation, this.value.X, this.value.Y, this.value.Z, this.value.W);
        }

        /// <inheritdoc/>
        public override ref Vector4 GetVector4Ref()
        {
            return ref this.value;
        }
    }
}
