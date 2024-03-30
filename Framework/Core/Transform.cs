using Framework.Core.Camera;
using OpenTK.Mathematics;

namespace Framework.Core
{
    /// <summary>
    /// 
    /// </summary>
    public class Transform
    {
        #region (Data Fields)

        internal Vector3 position;

        internal Vector3 rotation;

        internal Vector3 scale;

        #endregion

        #region (Constructors)

        /// <summary>
        /// Construtor padrão, com todos os parâmetros
        /// </summary>
        /// <param name="Position"> Posição </param>
        /// <param name="Rotation"> Rotação </param>
        /// <param name="Scale"> Escala </param>
        public Transform(Vector3 Position, Vector3 Rotation, Vector3 Scale)
        {
            this.position = Position;
            this.rotation = Rotation;
            this.scale = Scale;
        }

        /// <summary>
        /// Construtor sem parâmetros, inicializa posição e rotação zerados e escala 1:1
        /// </summary>
        public Transform() : this(Vector3.Zero, Vector3.Zero, Vector3.One) { }

        /// <summary>
        /// Construtor com apenas posição, inicializa rotação zerada e escala 1:1
        /// </summary>
        public Transform(Vector3 Position) : this(Position, Vector3.Zero, Vector3.One) { }

        /// <summary>
        /// Construtor com posição e rotação, inicializa a escala 1:1
        /// </summary>
        public Transform(Vector3 Position, Vector3 Rotation) : this(Position, Rotation, Vector3.One) { }

        #endregion

        #region (Properties)

        /// <summary>
        /// 
        /// </summary>
        public Vector3 Position { get => position; set => position = value; }

        /// <summary>
        /// 
        /// </summary>
        public Vector3 Rotation { get => rotation; set => rotation = value; }

        /// <summary>
        /// 
        /// </summary>
        public Vector3 Scale { get => scale; set => scale = value; }

        #endregion

        #region (Public Methods)

        /// <summary>
        /// Gets an object space matrix of a transformed model.
        /// </summary>
        /// <returns></returns>
        public Matrix4 GetModelMatrix()
        {
            Matrix4 posMat = Matrix4.CreateTranslation(position);
            Matrix4 scaleMat = Matrix4.CreateScale(scale);
            Matrix4 rotX = Matrix4.CreateRotationX(rotation.X);
            Matrix4 rotY = Matrix4.CreateRotationY(rotation.Y);
            Matrix4 rotZ = Matrix4.CreateRotationZ(rotation.Z);
            Matrix4 rotMat = rotX * rotY * rotZ;

            return posMat * rotMat * scaleMat;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Camera"></param>
        /// <returns></returns>
        public Matrix4 GetModelViewProjectionMatrix(PerspectiveCamera Camera)
        {
            return Camera.GetViewMatrix() * Camera.GetProjectionMatrix() * GetModelMatrix();
        }


        // Getters alternativos
        public Vector3 GetPosition() { return this.position; }
        public Vector3 GetRotation() { return this.rotation; }
        public Vector3 GetScale() { return this.scale; }


        // Setters alternativos (p/ dar maior flexibilidade para a API pública)

        // Position
        public void SetPosition(Vector3 Position) { this.position = Position; }
        public void SetPositionX(float X) { this.position.X = X; }
        public void SetPositionY(float Y) { this.position.Y = Y; }
        public void SetPositionZ(float Z) { this.position.Z = Z; }

        public void SetPositionXY(Vector2 Position)
        {
            this.position.X = Position.X;
            this.position.Y = Position.Y;
        }

        public void SetPosition(float X, float Y, float Z)
        {
            this.position.X = X;
            this.position.Y = Y;
            this.position.Z = Z;
        }

        // Rotation
        public void SetRotation(Vector3 Rotation) { this.rotation = Rotation; }
        public void SetRotationX(float rot) { this.rotation.X = rot; }
        public void SetRotationY(float rot) { this.rotation.Y = rot; }
        public void SetRotationZ(float rot) { this.rotation.Z = rot; }

        public void SetRotation(float X, float Y, float Z)
        {
            this.rotation.X = X;
            this.rotation.Y = Y;
            this.rotation.Z = Z;
        }

        // Scale
        public void SetScale(Vector3 Scale) { this.scale = Scale; }
        public void SetScaleX(float scl) { this.scale.X = scl; }
        public void SetScaleY(float scl) { this.scale.Y = scl; }
        public void SetScaleZ(float scl) { this.scale.Z = scl; }

        public void SetScale(float X, float Y, float Z)
        {
            this.scale.X = X;
            this.scale.Y = Y;
            this.scale.Z = Z;
        }

        #endregion
    }
}
