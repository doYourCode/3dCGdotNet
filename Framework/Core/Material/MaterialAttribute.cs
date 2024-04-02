using OpenTK.Graphics.OpenGL4;

namespace Framework.Core.Material
{

    public abstract class MaterialAttribute
    {
        #region (Data Fields)

        internal string label;

        protected int uniformLocation;

        #endregion

        #region (Constructors)

        public MaterialAttribute(string Label)
        {
            this.label = Label;
        }

        #endregion

        #region (Properties)

        public string Label { get => label; set { } }
        public int UniformLocation { get => uniformLocation; set { } }

        public abstract ref float FloatRef { get; }
        public abstract ref System.Numerics.Vector2 Vector2Ref { get; }
        public abstract ref System.Numerics.Vector3 Vector3Ref { get; }
        public abstract ref System.Numerics.Vector4 Vector4Ref { get; }

        #endregion

        #region (Public Methods)

        public void GetUniformLocation(Shader shader)
        {
            this.uniformLocation = GL.GetUniformLocation(shader.ID, label);
        }

        public abstract void UpdateUniform();

        #endregion
    }

    public class FloatAttribute : MaterialAttribute
    {
        #region (Data Fields)

        internal float value;

        #endregion

        #region (Constructors)

        public FloatAttribute(string Label, float Value) : base(Label)
        {
            this.label = Label;
            this.value = Value;
            this.UpdateUniform();
        }

        #endregion

        #region (Properties)

        public override ref float FloatRef { get => ref value; }

        #endregion

        #region (Public Methods)
        public override void UpdateUniform()
        {
            GL.Uniform1(this.uniformLocation, value);
        }

        #endregion

        #region (Unsupported)

        public override ref System.Numerics.Vector2 Vector2Ref { get { throw new NotSupportedException(); } }
        public override ref System.Numerics.Vector3 Vector3Ref { get { throw new NotSupportedException(); } }
        public override ref System.Numerics.Vector4 Vector4Ref { get { throw new NotSupportedException(); } }

        #endregion
    }

    public class Vector2Attribute : MaterialAttribute
    {
        #region (Data Fields)

        internal System.Numerics.Vector2 value;

        #endregion

        #region (Constructors)

        public Vector2Attribute(string Label, System.Numerics.Vector2 Value) : base(Label)
        {
            this.value = Value;

            this.UpdateUniform();
        }

        #endregion

        #region (Properties)

        public override ref System.Numerics.Vector2 Vector2Ref { get => ref value; }

        #endregion

        #region (Public Methods)

        public override void UpdateUniform()
        {
            GL.Uniform2(this.uniformLocation, value.X, value.Y);
        }

        #endregion

        #region (Unsupported)

        public override ref float FloatRef { get { throw new NotSupportedException(); } }
        public override ref System.Numerics.Vector3 Vector3Ref { get { throw new NotSupportedException(); } }
        public override ref System.Numerics.Vector4 Vector4Ref { get { throw new NotSupportedException(); } }

        #endregion
    }

    public class Vector3Attribute : MaterialAttribute
    {
        #region (Data Fields)

        internal System.Numerics.Vector3 value;

        #endregion

        #region (Constructors)

        public Vector3Attribute(string Label, System.Numerics.Vector3 Value) : base(Label)
        {
            this.value = Value;

            this.UpdateUniform();
        }

        #endregion

        #region (Properties)

        public override ref System.Numerics.Vector3 Vector3Ref { get => ref value; }

        #endregion

        #region (Public Methods)

        public override void UpdateUniform()
        {
            GL.Uniform3(this.uniformLocation, value.X, value.Y, value.Z);
        }

        #endregion

        #region (Unsupported)

        public override ref System.Numerics.Vector2 Vector2Ref { get { throw new NotSupportedException(); } }
        public override ref float FloatRef { get { throw new NotSupportedException(); } }
        public override ref System.Numerics.Vector4 Vector4Ref { get { throw new NotSupportedException(); } }

        #endregion
    }

    public class Vector4Attribute : MaterialAttribute
    {
        #region (Data Fields)

        internal System.Numerics.Vector4 value;

        #endregion

        #region (Constructors)

        public Vector4Attribute( string Label, System.Numerics.Vector4 Value) : base(Label)
        {
            this.value = Value;

            this.UpdateUniform();
        }

        #endregion

        #region (Properties)

        public override ref System.Numerics.Vector4 Vector4Ref { get => ref value; }

        #endregion

        #region (Public Methods)

        public override void UpdateUniform()
        {
            GL.Uniform4(this.uniformLocation, value.X, value.Y, value.Z, value.W);
        }

        #endregion

        #region (Unsupported)

        public override ref System.Numerics.Vector2 Vector2Ref { get { throw new NotSupportedException(); } }
        public override ref System.Numerics.Vector3 Vector3Ref { get { throw new NotSupportedException(); } }
        public override ref float FloatRef { get { throw new NotSupportedException(); } }

        #endregion
    }
}
