using Framework.Core.Resource;

namespace Framework.Core.Material
{
    public class Material : ResourceObject, IDisposable
    {
        #region (Data Fields)

        private Shader shader;

        public MaterialFormat format;

        #endregion

        #region (Constructors)

        public Material(MaterialFormat Format, string Label, uint ID) : base(Label, ID)
        {
            this.format = Format;
        }

        #endregion

        #region (Properties)

        public Shader Shader { get => shader; set => shader = value; }

        #endregion

        #region (Public Methods)

        /// <summary>
        /// 
        /// </summary>
        public void GetUniformLocations()
        {
            format.GetUniformLocations(shader);
        }

        /// <summary>
        /// 
        /// </summary>
        public void UpdateUniforms()
        {
            format.UpdateUniforms();
        }

        #endregion

        #region (Other Methods)

        protected override void Dispose(bool isManualDispose)
        {
            base.Dispose(isManualDispose);
        }

        #endregion
    }
}
