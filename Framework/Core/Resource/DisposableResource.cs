// <copyright file="DisposableResource.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Framework.Core.Resource
{
    using System.Reflection;
    using Framework.Utils.Log;

    /// <summary>
    /// Represents an OpenGL resource.<br/>
    /// Must be disposed explicitly, otherwise a warning will be logged indicating
    /// a memory leak.<br/> Can be derived to inherit the dispose pattern.
    /// </summary>
    public abstract class DisposableResource : IDisposable, IEquatable<DisposableResource>
    {
        private static readonly IFwLogger Logger =
            LogFactory.GetLogger(typeof(DisposableResource));

        private static Dictionary<string, DisposableResource> disposableResources = new ();

        private uint id;

        private bool isDisposed;

        /// <summary>
        /// Initializes a new instance of the <see cref="DisposableResource"/> class.
        /// </summary>
        /// <param name="label"> Este rótulo identificará cada instância de
        /// DisposableResource alocada. </param>
        /// <param name="id">Unique identifier.</param>
        protected DisposableResource(string label, uint id)
        {
            this.isDisposed = false;

            this.id = id;

            this.RegisterInstance(label + "_" + this.id, this);
        }

        /// <summary>
        /// Finalizes an instance of the <see cref="DisposableResource"/> class.
        /// Called by the garbage collector and an indicator for a resource leak
        /// because the manual dispose prevents this destructor from being called.
        /// </summary>
        ~DisposableResource()
        {
            Logger?.WarnFormat("GLResource leaked: {0}", this);
            this.Dispose(false);
#if DEBUG
            throw new Exception(string.Format("GLResource leaked: {0}", this));
#endif
        }

        /// <summary>
        /// Gets the reference for all the resources inside a dictionary data structure.
        /// </summary>
        public static Dictionary<string, DisposableResource> DisposableResources
        {
            get { return disposableResources; }
            private set { }
        }

        /// <summary>
        /// Gets an unique identifier of the resource.
        /// </summary>
        public uint ID
        {
            get { return this.id; } private set { }
        }

        /// <summary>
        /// Gets a value indicating whether the resource is already disposed.
        /// </summary>
        public bool IsDisposed
        {
            get { return this.isDisposed; } private set { }
        }

        /// <summary>
        /// Automatically calls <see cref="Dispose()"/> on all
        /// <see cref="DisposableResource"/> objects found on the given object.
        /// </summary>
        /// <param name="obj">The object to be disposed.</param>
        public static void DisposeAll(object obj)
        {
            // get all fields, including backing fields for properties
            foreach (var field in obj.GetType().GetFields(
                BindingFlags.Instance |
                BindingFlags.Public |
                BindingFlags.NonPublic))
            {
                if (field != null)
                {
                    // check if it should be released
                    if (typeof(DisposableResource).IsAssignableFrom(field.FieldType))
                    {
                        // and release it
                        (field.GetValue(obj) as DisposableResource).Dispose();
                    }
                }
            }
        }

        /// <summary>
        /// Releases all OpenGL handles related to this resource.
        /// </summary>
        public void Dispose()
        {
            // safely handle multiple calls to dispose
            if (this.isDisposed)
            {
                return;
            }

            this.isDisposed = true;

            // Dipose this resource
            this.Dispose(true);

            // Prevent the destructor from being called
            GC.SuppressFinalize(this);

            // Make sure the garbage collector does not eat our object before
            // it is properly disposed
            GC.KeepAlive(this);
        }

        /// <summary>
        /// Register this instance of <see cref="DisposableResource"/> for future tracking.
        /// </summary>
        /// <param name="label">The unique identifier string.</param>
        /// <param name="dispRes">The instance to be registered.</param>
        public void RegisterInstance(string label, DisposableResource dispRes)
        {
            DisposableResource.disposableResources.Add(label, dispRes);
        }

        /// <summary>
        /// Test equality with another <see cref="DisposableResource"/> object.
        /// </summary>
        /// <param name="other">The other object to be testes against it.</param>
        /// <returns>Returns true if both are equals.</returns>
        public bool Equals(DisposableResource? other)
        {
            return other != null && this.id.Equals(other.id);
        }

        /// <summary>
        /// Test equality with a <see cref="object"/> tupe object.
        /// </summary>
        /// <param name="obj">The other object to be testes against it.</param>
        /// <returns>Returns true if both are equals.</returns>
        public override bool Equals(object? obj)
        {
            return obj is DisposableResource && this.Equals((DisposableResource)obj);
        }

        /// <summary>
        /// Gets the internal hashing code of the base class id field.
        /// </summary>
        /// <returns>The hash code of the objects base ID.</returns>
        public override int GetHashCode()
        {
            return this.ID.GetHashCode();
        }

        /// <summary>
        /// Releases all OpenGL handles related to this resource.
        /// </summary>
        /// <param name="isManualDispose">True if the call is performed explicitly
        /// and within the OpenGL thread, false if it is caused by the garbage
        /// collector and therefore from another thread and the result of a resource
        /// leak.</param>
        protected abstract void Dispose(bool isManualDispose);
    }
}