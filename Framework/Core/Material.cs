using Framework.Core.Base;
using OpenTK.Graphics.OpenGL4;
using System.Numerics;

namespace Framework.Core
{
    public class Material : ResourceObject, IDisposable
    {
        #region (Data Fields)
        internal float roughness = 1.0f;

        internal float specularIntensity = 1.0f;

        internal float specularPower = 9.0f;

        internal Vector3 specularColor = new Vector3(1.0f, 1.0f, 1.0f);


        private int roughnessUniformLocation;

        private int specularIntensityUniformLocation;

        private int specularPowerUniformLocation;

        private int specularColorUniformLocation;

        #endregion

        #region (Constructors)

        public Material(string Label, uint ID) : base(Label, ID)
        {
        }

        #endregion

        #region (Public Methods)

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Shader"></param>
        public void GetUniformLocations(Shader Shader)
        {
            this.roughnessUniformLocation = GL.GetUniformLocation(Shader.ID, "roughness");
            this.specularIntensityUniformLocation = GL.GetUniformLocation(Shader.ID, "specularIntensity");
            this.specularPowerUniformLocation = GL.GetUniformLocation(Shader.ID, "specularPower");
            this.specularColorUniformLocation = GL.GetUniformLocation(Shader.ID, "specularColor");

        }

        /// <summary>
        /// 
        /// </summary>
        public void UpdateUniforms()
        {
            GL.Uniform1(this.roughnessUniformLocation, this.roughness);
            GL.Uniform1(this.specularIntensityUniformLocation, this.specularIntensity);
            GL.Uniform1(this.specularPowerUniformLocation, this.specularPower);
            GL.Uniform3(this.specularColorUniformLocation, this.specularColor.X, this.specularColor.Y, this.specularColor.Z);
        }

        #endregion

        #region (Other Methods)

        protected override void Dispose(bool isManualDispose)
        {
            // TODO: dispose all the related resources
        }

        #endregion
    }
}
