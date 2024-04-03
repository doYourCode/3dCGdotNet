// <copyright file="Transform.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Framework.Core
{
    using Framework.Core.Camera;
    using OpenTK.Mathematics;

    /// <summary>
    /// TODO.
    /// </summary>
    public class Transform
    {
        private Vector3 position;

        private Vector3 rotation;

        private Vector3 scale;

        /// <summary>
        /// Initializes a new instance of the <see cref="Transform"/> class.
        /// </summary>
        /// <param name="position"> Posição. </param>
        /// <param name="rotation"> Rotação. </param>
        /// <param name="scale"> Escala. </param>
        public Transform(Vector3 position, Vector3 rotation, Vector3 scale)
        {
            this.position = position;
            this.rotation = rotation;
            this.scale = scale;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Transform"/> class.
        /// </summary>
        /// <param name="position"> Posição. </param>
        /// <param name="rotation"> Rotação. </param>
        /// <param name="scale"> Escala. </param>
        public Transform(
            System.Numerics.Vector3 position,
            System.Numerics.Vector3 rotation,
            System.Numerics.Vector3 scale)
        {
            this.position = new ();
            this.position.X = position.X;
            this.position.Y = position.Y;
            this.position.Z = position.Z;

            this.rotation = new ();
            this.rotation.X = rotation.X;
            this.rotation.Y = rotation.Y;
            this.rotation.Z = rotation.Z;

            this.scale = new ();
            this.scale.X = scale.X;
            this.scale.Y = scale.Y;
            this.scale.Z = scale.Z;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Transform"/> class.
        /// </summary>
        public Transform()
            : this(Vector3.Zero, Vector3.Zero, Vector3.One)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Transform"/> class.
        /// TODO.
        /// </summary>
        /// <param name="position"> PARAM TODO. </param>
        public Transform(Vector3 position)
            : this(position, Vector3.Zero, Vector3.One)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Transform"/> class.
        /// TODO.
        /// </summary>
        /// <param name="position"> PARAM TODO. </param>
        /// <param name="rotation"> PARAM2 TODO. </param>
        public Transform(Vector3 position, Vector3 rotation)
            : this(position, rotation, Vector3.One)
        {
        }

        /// <summary>
        /// Gets or sets TODO.
        /// </summary>
        public Vector3 Position { get => this.position; set => this.position = value; }

        /// <summary>
        /// Gets or sets TODO.
        /// </summary>
        public Vector3 Rotation { get => this.rotation; set => this.rotation = value; }

        /// <summary>
        /// Gets or sets TODO.
        /// </summary>
        public Vector3 Scale { get => this.scale; set => this.scale = value; }

        /// <summary>
        /// Gets an object space matrix of a transformed model.
        /// </summary>
        /// <returns> RETURN TODO. </returns>
        public Matrix4 GetModelMatrix()
        {
            Matrix4 posMat = Matrix4.CreateTranslation(this.position);
            Matrix4 scaleMat = Matrix4.CreateScale(this.scale);
            Matrix4 rotX = Matrix4.CreateRotationX(this.rotation.X);
            Matrix4 rotY = Matrix4.CreateRotationY(this.rotation.Y);
            Matrix4 rotZ = Matrix4.CreateRotationZ(this.rotation.Z);
            Matrix4 rotMat = rotX * rotY * rotZ;

            return posMat * rotMat * scaleMat;
        }

        /// <summary>
        /// TODO.
        /// </summary>
        /// <param name="camera">  PARAM TODO. </param>
        /// <returns> RETURN TODO. </returns>
        public Matrix4 GetModelViewProjectionMatrix(PerspectiveCamera camera)
        {
            return camera.GetViewMatrix() * camera.GetProjectionMatrix() * this.GetModelMatrix();
        }

        /// <summary>
        /// TODO.
        /// </summary>
        /// <returns> RETURN TODO. </returns>
        public Vector3 GetPosition()
        {
            return this.position;
        }

        /// <summary>
        /// TODO.
        /// </summary>
        /// <returns> RETURN TODO. </returns>
        public Vector3 GetRotation()
        {
            return this.rotation;
        }

        /// <summary>
        /// TODO.
        /// </summary>
        /// <returns> RETURN TODO. </returns>
        public Vector3 GetScale()
        {
            return this.scale;
        }

        // Setters alternativos (p/ dar maior flexibilidade para a API pública)

        /// <summary>
        /// TODO.
        /// </summary>
        /// <param name="position"> PARAM TODO. </param>
        public void SetPosition(Vector3 position)
        {
            this.position = position;
        }

        /// <summary>
        /// TODO.
        /// </summary>
        /// <param name="x"> PARAM TODO. </param>
        public void SetPositionX(float x)
        {
            this.position.X = x;
        }

        /// <summary>
        /// TODO.
        /// </summary>
        /// <param name="y"> PARAM TODO. </param>
        public void SetPositionY(float y)
        {
            this.position.Y = y;
        }

        /// <summary>
        /// TODO.
        /// </summary>
        /// <param name="z"> PARAM TODO. </param>
        public void SetPositionZ(float z)
        {
            this.position.Z = z;
        }

        /// <summary>
        /// TODO.
        /// </summary>
        /// <param name="position"> PARAM TODO. </param>
        public void SetPositionXY(Vector2 position)
        {
            this.position.X = position.X;
            this.position.Y = position.Y;
        }

        /// <summary>
        /// TODO.
        /// </summary>
        /// <param name="x"> PARAM TODO. </param>
        /// <param name="y"> PARAM2 TODO. </param>
        /// <param name="z"> PARAM3 TODO. </param>
        public void SetPosition(float x, float y, float z)
        {
            this.position.X = x;
            this.position.Y = y;
            this.position.Z = z;
        }

        /// <summary>
        /// TODO.
        /// </summary>
        /// <param name="rotation"> PARAM TODO. </param>
        public void SetRotation(Vector3 rotation)
        {
            this.rotation = rotation;
        }

        /// <summary>
        /// TODO.
        /// </summary>
        /// <param name="rot"> PARAM TODO. </param>
        public void SetRotationX(float rot)
        {
            this.rotation.X = rot;
        }

        /// <summary>
        /// TODO.
        /// </summary>
        /// <param name="rot"> PARAM TODO. </param>
        public void SetRotationY(float rot)
        {
            this.rotation.Y = rot;
        }

        /// <summary>
        /// TODO.
        /// </summary>
        /// <param name="rot"> PARAM TODO. </param>
        public void SetRotationZ(float rot)
        {
            this.rotation.Z = rot;
        }

        /// <summary>
        /// TODO.
        /// </summary>
        /// <param name="x"> PARAM TODO. </param>
        /// <param name="y"> PARAM2 TODO. </param>
        /// <param name="z"> PARAM3 TODO. </param>
        public void SetRotation(float x, float y, float z)
        {
            this.rotation.X = x;
            this.rotation.Y = y;
            this.rotation.Z = z;
        }

        /// <summary>
        /// TODO.
        /// </summary>
        /// <param name="scale"> PARAM TODO. </param>
        public void SetScale(Vector3 scale)
        {
            this.scale = scale;
        }

        /// <summary>
        /// TODO.
        /// </summary>
        /// <param name="scl"> PARAM TODO. </param>
        public void SetScaleX(float scl)
        {
            this.scale.X = scl;
        }

        /// <summary>
        /// TODO.
        /// </summary>
        /// <param name="scl"> PARAM TODO. </param>
        public void SetScaleY(float scl)
        {
            this.scale.Y = scl;
        }

        /// <summary>
        /// TODO.
        /// </summary>
        /// <param name="scl"> PARAM TODO. </param>
        public void SetScaleZ(float scl)
        {
            this.scale.Z = scl;
        }

        /// <summary>
        /// TODO.
        /// </summary>
        /// <param name="x"> PARAM TODO. </param>
        /// <param name="y"> PARAM2 TODO. </param>
        /// <param name="z"> PARAM3 TODO. </param>
        public void SetScale(float x, float y, float z)
        {
            this.scale.X = x;
            this.scale.Y = y;
            this.scale.Z = z;
        }
    }
}
