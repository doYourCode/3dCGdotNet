using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace Framework.Core.Material.Attributes
{
    public class Vector4Attribute : MaterialAttribute
    {
        internal Vector4 value;
        public Vector4Attribute(string Label, System.Numerics.Vector4 Value) : base(Label)
        {
            value = Value;

            UpdateUniform();
        }

        public override void UpdateUniform()
        {
            GL.Uniform4(this.UniformLocation, value.X, value.Y, value.Z, value.W);
        }

        /// <inheritdoc/>
        public override ref Vector4 GetVector4Ref()
        {
            return ref this.value;
        }
    }
}
