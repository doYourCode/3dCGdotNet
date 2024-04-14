// <copyright file="PerspectiveCamera.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Framework.Core.Camera
{
    using OpenTK.Graphics.OpenGL4;
    using OpenTK.Mathematics;

    /// <summary>
    /// TODO.
    /// </summary>
    public class PerspectiveCamera
    {
        private static Matrix4 defaultMatrix;

        private int positionUniformLocation;

        private Vector3 front = -Vector3.UnitZ;

        private Vector3 up = Vector3.UnitY;

        private Vector3 right = Vector3.UnitX;

        private float pitch;

        private float yaw = -MathHelper.PiOver2;

        private float fov = MathHelper.PiOver2;

        /// <summary>
        /// Initializes a new instance of the <see cref="PerspectiveCamera"/> class.
        /// </summary>
        /// <param name="position"> PARAM TODO. </param>
        /// <param name="aspectRatio"> PARAM2 TODO. </param>
        public PerspectiveCamera(Vector3 position, float aspectRatio)
        {
            this.Position = position;
            this.AspectRatio = aspectRatio;
        }

        /// <summary>
        /// Gets or sets TODO.
        /// </summary>
        public Vector3 Position { get; set; }

        /// <summary>
        /// Sets TODO.
        /// </summary>
        public float AspectRatio { private get; set; }

        /// <summary>
        /// Gets TODO.
        /// </summary>
        public Vector3 Front => this.front;

        /// <summary>
        /// Gets TODO.
        /// </summary>
        public Vector3 Up => this.up;

        /// <summary>
        /// Gets TODO.
        /// </summary>
        public Vector3 Right => this.right;

        /// <summary>
        /// Gets or sets TODO.
        /// </summary>
        public float Pitch
        {
            get => MathHelper.RadiansToDegrees(this.pitch);
            set
            {
                var angle = MathHelper.Clamp(value, -89f, 89f);
                this.pitch = MathHelper.DegreesToRadians(angle);
                this.UpdateVectors();
            }
        }

        /// <summary>
        /// Gets or sets TODO.
        /// </summary>
        public float Yaw
        {
            get => MathHelper.RadiansToDegrees(this.yaw);
            set
            {
                this.yaw = MathHelper.DegreesToRadians(value);
                this.UpdateVectors();
            }
        }

        /// <summary>
        /// Gets or sets TODO.
        /// </summary>
        public float Fov
        {
            get => MathHelper.RadiansToDegrees(this.fov);
            set
            {
                var angle = MathHelper.Clamp(value, 1f, 90f);
                this.fov = MathHelper.DegreesToRadians(angle);
            }
        }

        /// <summary>
        /// TODO.
        /// </summary>
        /// <returns> RETURNS TODO. </returns>
        public Matrix4 GetViewMatrix()
        {
            return Matrix4.LookAt(this.Position, this.Position + this.front, this.up);
        }

        /// <summary>
        /// TODO.
        /// </summary>
        /// <returns> RETURNS TODO. </returns>
        public Matrix4 GetProjectionMatrix()
        {
            return Matrix4.CreatePerspectiveFieldOfView(this.fov, this.AspectRatio, 0.01f, 100f);
        }

        /// <summary>
        /// TODO.
        /// </summary>
        /// <returns> RETURNS TODO. </returns>
        public Matrix4 GetViewProjectionMatrix()
        {
            return this.GetViewMatrix() * this.GetProjectionMatrix();
        }

        /// <summary>
        /// TODO.
        /// </summary>
        public void UpdateUniforms()
        {
            GL.Uniform3(this.positionUniformLocation, this.Position.X, this.Position.Y, this.Position.Z);
        }

        /// <summary>
        /// TODO.
        /// </summary>
        /// <param name="shader"> PARAM TODO. </param>
        public void GetUniformLocations(Shader shader)
        {
            this.positionUniformLocation = GL.GetUniformLocation(shader.ID, "viewPosition");
        }

        /// <summary>
        /// TODO.
        /// </summary>
        /// <param name="fovDeg"> PARAM TODO. </param>
        /// <param name="nearPlane"> PARAM2 TODO. </param>
        /// <param name="farPlane"> PARAM3 TODO. </param>
        public void UpdateMatrix(float fovDeg, float nearPlane, float farPlane)
        {
            // Initializes matrices since otherwise they will be the null matrix
            Matrix4 view = default(Matrix4);
            Matrix4 projection = default(Matrix4);

            // Makes camera look in the right direction from the right position
            view = Matrix4.LookAt(this.Position, this.Position + this.front, this.up);
            // Adds perspective to the scene
            projection = Matrix4.CreatePerspectiveFieldOfView(this.fov, this.AspectRatio, 0.01f, 100f);

            // Sets new camera matrix
            defaultMatrix = projection * view;
        }

        /// <summary>
        /// TODO.
        /// </summary>
        private void UpdateVectors()
        {
            this.front.X = MathF.Cos(this.pitch) * MathF.Cos(this.yaw);
            this.front.Y = MathF.Sin(this.pitch);
            this.front.Z = MathF.Cos(this.pitch) * MathF.Sin(this.yaw);

            this.front = Vector3.Normalize(this.front);

            this.right = Vector3.Normalize(Vector3.Cross(this.front, Vector3.UnitY));
            this.up = Vector3.Normalize(Vector3.Cross(this.right, this.front));
        }
    }
}