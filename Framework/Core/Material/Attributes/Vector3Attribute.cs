using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace Framework.Core.Material.Attributes
{
    public class Vector3Attribute : MaterialAttribute
    {
        private Vector3 value;

        public Vector3Attribute(string Label, System.Numerics.Vector3 Value) : base(Label)
        {
            value = Value;

            UpdateUniform();
        }

        public override void UpdateUniform()
        {
            GL.Uniform3(this.UniformLocation, value.X, value.Y, value.Z);
        }

        /// <inheritdoc/>
        public override ref Vector3 GetVector3Ref()
        {
            return ref this.value;
        }
    }
}