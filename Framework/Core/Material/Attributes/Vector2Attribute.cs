// <copyright file="Vector2Attribute.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Framework.Core.Material.Attributes
{
    using System.Numerics;
    using OpenTK.Graphics.OpenGL4;

    public class Vector2Attribute : MaterialAttribute
    {
        private System.Numerics.Vector2 value;


        public Vector2Attribute(string Label, System.Numerics.Vector2 Value) : base(Label)
        {
            this.value = Value;

            UpdateUniform();
        }

        public override void UpdateUniform()
        {
            GL.Uniform2(this.UniformLocation, value.X, value.Y);
        }

        /// <inheritdoc/>
        public override ref Vector2 GetVector2Ref()
        {
            return ref this.value;
        }
    }
}
