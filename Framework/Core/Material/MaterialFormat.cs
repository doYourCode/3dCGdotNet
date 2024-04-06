// <copyright file="MaterialFormat.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Framework.Core.Material
{
    using Framework.Core.Material.Attributes;

    /// <summary>
    /// Groups a set of attributes and relates them with a given Material and shader.
    /// </summary>
    public class MaterialFormat
    {
        private Dictionary<string, MaterialAttribute> attributes;

        /// <summary>
        /// Initializes a new instance of the <see cref="MaterialFormat"/> class.
        /// </summary>
        public MaterialFormat()
        {
            this.attributes = new Dictionary<string, MaterialAttribute>();
        }

        /// <summary>
        /// Gets a reference to the list containing all the attributes.
        /// </summary>
        internal ref Dictionary<string, MaterialAttribute> AttributesRef { get => ref this.attributes; }

        /// <summary>
        /// Gets TODO.
        /// </summary>
        public Dictionary<string, MaterialAttribute> Attributes => this.attributes;

        /// <summary>
        /// Assign addresses in video memory for the uniforms.
        /// </summary>
        /// <param name="shader">The shader to consume the uniforms.</param>
        public void GetUniformLocations(Shader shader)
        {
            foreach (MaterialAttribute attribute in this.attributes.Values)
            {
                attribute.GetUniformLocation(shader);
            }
        }

        /// <summary>
        /// Update all the uniforms for this format.
        /// </summary>
        public void UpdateUniforms()
        {
            foreach (MaterialAttribute attribute in this.attributes.Values)
            {
                attribute.UpdateUniform();
            }
        }

        /// <summary>
        /// Gets an <see cref="MaterialAttribute"/> by it's name.
        /// </summary>
        /// <param name="name">The name of the attribute.</param>
        /// <returns>Returns the MaterialAttribute.</returns>
        public MaterialAttribute GetAttribute(string name)
        {
            return this.attributes[name];
        }

        /// <summary>
        /// Adds an attritube of the type <see cref="float"/>.
        /// </summary>
        /// <param name="name">The name for the new attribute.</param>
        /// <param name="value">The value for the new attribute.</param>
        public void AddFloat(string name, float value)
        {
            this.attributes.Add(name, new FloatAttribute(name, value));
        }

        /// <summary>
        /// Adds an attritube of the type <see cref="System.Numerics.Vector2"/>.
        /// </summary>
        /// <param name="name">The name for the new attribute.</param>
        /// <param name="value">The value for the new attribute.</param>
        public void AddVector2(string name, System.Numerics.Vector2 value)
        {
            this.attributes.Add(name, new Vector2Attribute(name, value));
        }

        /// <summary>
        /// Adds an attritube of the type <see cref="System.Numerics.Vector3"/>.
        /// </summary>
        /// <param name="name">The name for the new attribute.</param>
        /// <param name="value">The value for the new attribute.</param>
        public void AddVector3(string name, System.Numerics.Vector3 value)
        {
            this.attributes.Add(name, new Vector3Attribute(name, value));
        }

        /// <summary>
        /// Adds an attritube of the type <see cref="System.Numerics.Vector4"/>.
        /// </summary>
        /// <param name="name">The name for the new attribute.</param>
        /// <param name="value">The value for the new attribute.</param>
        public void AddVector4(string name, System.Numerics.Vector4 value)
        {
            this.attributes.Add(name, new Vector4Attribute(name, value));
        }

        /// <summary>
        /// Exibits it's data textually, for use within for the shader code.
        /// </summary>
        public void PrintLayout()
        {
            foreach (MaterialAttribute attribute in this.attributes.Values)
            {
                Console.WriteLine(attribute.Label);
            }
        }
    }
}
