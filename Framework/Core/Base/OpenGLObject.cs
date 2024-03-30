using System;
using System.Runtime.CompilerServices;
using System.Reflection;

namespace Framework.Core.Base
{
    /// <summary>
    /// Represents an OpenGL handle.<br/>
    /// Must be disposed explicitly, otherwise there will be a memory leak which will be logged as a warning.
    /// </summary>
    public abstract class OpenGLObject : DisposableResource , IEquatable<OpenGLObject>
    {
        #region (Constructors)

        /// <summary>
        /// Construtor para a classe base.
        /// </summary>
        /// 
        /// <param name="Label"> Rótulo identificador do recurso, pode ser útil
        /// para buscas futuras. </param>
        /// 
        /// <param name="ID"> Deve ser informada a partir das classes base,
        /// geradas com funções GL.GenEtc(...). </param>
        protected OpenGLObject(string Label, UInt32 ID) : base(Label + ID)
        {
            this.id = ID;
        }

        /// <summary>
        /// Construtor alternativo (com rótulo produzido automaticamente)
        /// </summary>
        /// 
        /// <param name="Label"> Rótulo identificador do recurso, pode ser útil
        /// para buscas futuras. </param>
        /// 
        /// <param name="ID"> Deve ser informada a partir das classes base,
        /// geradas com funções GL.GenEtc(...). </param>
        protected OpenGLObject(UInt32 ID) : base("ResourceObject" + ID)
        {
            this.id = ID;
        }

        #endregion  // End of Constructors region

        #region (Public Methods)
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Other"></param>
        /// <returns></returns>
        public bool Equals(OpenGLObject Other)
        {
            return Other != null && id.Equals(Other.id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Object"></param>
        /// <returns></returns>
        public override bool Equals(object Object)
        {
            return Object is OpenGLObject && Equals((OpenGLObject)Object);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return id.GetHashCode();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("{0}({1})", GetType().Name, id);
        }

        #endregion  // End of Public Methods region
    }
}