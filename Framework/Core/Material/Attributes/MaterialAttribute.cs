// <copyright file="MaterialAttribute.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Framework.Core.Material.Attributes
{
    using System.Numerics;
    using Framework.Core;
    using OpenTK.Graphics.OpenGL4;

    /// <summary>
    /// Represents an attribute for a given material or set.
    /// </summary>
    public abstract class MaterialAttribute
    {
        private string label;

        private int uniformLocation;

        /// <summary>
        /// Initializes a new instance of the <see cref="MaterialAttribute"/> class.
        /// </summary>
        /// <param name="label">The name of the attribute, to be expressed also
        /// in the shader code.</param>
        public MaterialAttribute(string label)
        {
            this.label = label;
        }

        /// <summary>
        /// Gets or sets the attribute's label.
        /// </summary>
        public string Label
        {
            get => this.label;
            set { this.label = value; }
        }

        /// <summary>
        /// Gets the ID of the related uniform location.
        /// </summary>
        public int UniformLocation
        {
            get => this.uniformLocation; private set { }
        }

        /// <summary>
        /// Associate an uniform location in video memory to use in the <see cref="Shader"/>.
        /// </summary>
        /// <param name="shader">The shader to consume the uniform.</param>
        public void GetUniformLocation(Shader shader)
        {
            this.uniformLocation = GL.GetUniformLocation(shader.ID, this.label);
        }

        /// <summary>
        /// Updates the uniform location in video memory.
        /// </summary>
        public abstract void UpdateUniform();

        /// <summary>
        /// Gets a float variant of Value.
        /// </summary>
        /// <returns>The value.</returns>
        /// <exception cref="NotSupportedException">Throws if not overriden.</exception>
        public virtual ref float GetFloatRef()
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Gets a Vector2 variant of Value.
        /// </summary>
        /// <returns>The value.</returns>
        /// <exception cref="NotSupportedException">Throws if not overriden.</exception>
        public virtual ref Vector2 GetVector2Ref()
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Gets a Vector3 variant of Value.
        /// </summary>
        /// <returns>The value.</returns>
        /// <exception cref="NotSupportedException">Throws if not overriden.</exception>
        public virtual ref Vector3 GetVector3Ref()
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Gets a Vector4 variant of Value.
        /// </summary>
        /// <returns>The value.</returns>
        /// <exception cref="NotSupportedException">Throws if not overriden.</exception>
        public virtual ref Vector4 GetVector4Ref()
        {
            throw new NotSupportedException();
        }
    }
}
