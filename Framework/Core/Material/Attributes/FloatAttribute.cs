// <copyright file="FloatAttribute.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Framework.Core.Material.Attributes
{
    using OpenTK.Graphics.OpenGL4;

    /// <summary>
    /// Float variant of a <see cref="MaterialAttribute"/> object.
    /// </summary>
    public class FloatAttribute : MaterialAttribute
    {
        private float value;

        /// <summary>
        /// Initializes a new instance of the <see cref="FloatAttribute"/> class.
        /// </summary>
        /// <param name="label">The name of the attribute, to be expressed also
        /// in the shader code.</param>
        /// <param name="value">It's value.</param>
        public FloatAttribute(string label, float value)
            : base(label)
        {
            this.Label = label;
            this.value = value;
            this.UpdateUniform();
        }

        /// <inheritdoc/>
        public override ref float GetFloatRef()
        {
            return ref this.value;
        }

        /// <inheritdoc/>
        public override void UpdateUniform()
        {
            GL.Uniform1(this.UniformLocation, this.value);
        }
    }
}
