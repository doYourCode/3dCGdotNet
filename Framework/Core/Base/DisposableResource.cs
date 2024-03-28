using System;
using System.Reflection;
using System.Reflection.Emit;
using Framework.Utils.Log;

namespace Framework.Core.Base
{
    /// <summary>
    /// Represents an OpenGL resource.<br/>
    /// Must be disposed explicitly, otherwise a warning will be logged indicating a memory leak.<br/>
    /// Can be derived to inherit the dispose pattern.
    /// </summary>
    public abstract class DisposableResource : IDisposable
    {
        #region (Data Fields)

        protected bool isDisposed;

        private static Dictionary<string, DisposableResource> disposableResources = new Dictionary<string, DisposableResource>();

        private static readonly IFwLogger Logger = LogFactory.GetLogger(typeof(DisposableResource));

        #endregion

        #region (Constructors)

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="Label"> Este rótulo identificará cada instância de DisposableResource alocada. </param>
        protected DisposableResource(string Label)
        {
            this.isDisposed = false;

            this.RegisterInstance(Label, this);
        }

        /// <summary>
        /// Construtor alternativo (sem parâmetros)
        /// </summary>
        protected DisposableResource()
        {
            this.isDisposed = false;

            this.RegisterInstance(this.GetType().Name + this.GetHashCode(), this);
        }

        #endregion

        #region (Destructors)

        /// <summary>
        /// Called by the garbage collector and an indicator for a resource leak because the manual dispose prevents
        /// this destructor from being called.
        /// </summary>
        ~DisposableResource()
        {
            Logger?.WarnFormat("GLResource leaked: {0}", this);
            Dispose(false);
#if DEBUG
            throw new Exception(string.Format("GLResource leaked: {0}", this));
#endif
        }

        #endregion

        #region (Properties)

        /// <summary>
        /// Gets a values specifying if this resource has already been disposed.
        /// </summary>
        public bool IsDisposed { get { return isDisposed; } private set { } }

        /// <summary>
        /// 
        /// </summary>
        public static Dictionary<string, DisposableResource> DisposableResources
        {
            get { return disposableResources; }
            private set { } 
        }

        #endregion

        #region (Public Methods)

        /// <summary>
        /// Releases all OpenGL handles related to this resource.
        /// </summary>
        public void Dispose()
        {
            // safely handle multiple calls to dispose
            if (isDisposed) return;
            isDisposed = true;
            // dipose this resource
            Dispose(true);
            // prevent the destructor from being called
            GC.SuppressFinalize(this);
            // make sure the garbage collector does not eat our object before it is properly disposed
            GC.KeepAlive(this);
        }

        /// <summary>
        /// Automatically calls <see cref="Dispose()"/> on all <see cref="DisposableResource"/> objects found on the
        /// given object.
        /// </summary>
        /// <param name="Object"></param>
        public static void DisposeAll(object Object)
        {
            // get all fields, including backing fields for properties
            foreach (var field in Object.GetType().GetFields(
                BindingFlags.Instance |
                BindingFlags.Public |
                BindingFlags.NonPublic))
            {
                // check if it should be released
                if (typeof(DisposableResource).IsAssignableFrom(field.FieldType))
                {
                    // and release it
                    ((DisposableResource)field.GetValue(Object)).Dispose();
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Label"></param>
        /// <param name="dispRes"></param>
        public void RegisterInstance(string Label, DisposableResource dispRes)
        {
            DisposableResource.disposableResources.Add(Label, dispRes);
        }

        #endregion

        #region (Other Methods)

        /// <summary>
        /// Releases all OpenGL handles related to this resource.
        /// </summary>
        /// <param name="isManualDispose">True if the call is performed explicitly and within the OpenGL thread, false
        /// if it is caused by the garbage collector and therefore from another thread and the result of a resource
        /// leak.</param>
        protected abstract void Dispose(bool isManualDispose);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Object"></param>
        /// <returns></returns>
        protected static string TypeToString(object Object)
        {
            return Object.GetType().Name;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("{0}({1})", GetType().Name, "NO_ID");
        }

        #endregion
    }
}