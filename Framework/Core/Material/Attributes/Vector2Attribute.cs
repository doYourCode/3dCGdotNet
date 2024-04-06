// <copyright file="Vector2Attribute.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Framework.Core.Material.Attributes
{
    using System.Numerics;
    using OpenTK.Graphics.OpenGL4;

    /// <summary>
    /// TODO.
    /// </summary>
    public class Vector2Attribute : MaterialAttribute
    {
        private System.Numerics.Vector2 value;

        /// <summary>
        /// Initializes a new instance of the <see cref="Vector2Attribute"/> class.
        /// TODO.
        /// </summary>
        /// <param name="label"> PARAM TODO. </param>
        /// <param name="value"> PARAM2 TODO. </param>
        public Vector2Attribute(string label, System.Numerics.Vector2 value)
            : base(label)
        {
            this.value = value;

            this.UpdateUniform();
        }

        /// <inheritdoc/>
        public override void UpdateUniform()
        {
            GL.Uniform2(this.UniformLocation, this.value.X, this.value.Y);
        }

        /// <inheritdoc/>
        public override ref Vector2 GetVector2Ref()
        {
            return ref this.value;
        }
    }
}
