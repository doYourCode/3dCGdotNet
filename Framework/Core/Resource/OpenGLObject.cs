// <copyright file="OpenGLObject.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Framework.Core.Resource
{
    /// <summary>
    /// Represents an OpenGL handle.<br/>
    /// Must be disposed explicitly, otherwise there will be a memory leak which will be logged as a warning.
    /// </summary>
    public abstract class OpenGLObject : DisposableResource
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OpenGLObject"/> class.
        /// </summary>
        /// <param name="label"> Rótulo identificador do recurso, pode ser útil
        /// para buscas futuras. </param>
        /// <param name="id"> Deve ser informada a partir das classes base,
        /// geradas com funções GL.GenEtc(...). </param>
        public OpenGLObject(string label, uint id)
            : base(label, id)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OpenGLObject"/> class.
        /// </summary>
        /// <param name="id"> Deve ser informada a partir das classes base,
        /// geradas com funções GL.GenEtc(...). </param>
        public OpenGLObject(uint id)
            : base("OpenGLObject", id)
        {
        }
    }
}