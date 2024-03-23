using Framework.Core.Camera;
using OpenTK.Mathematics;

namespace Framework.Core
{
    /// <summary>
    /// 
    /// </summary>
    public class Transform // Necessário p/ otimizar animações de rotação, escala e tanslação
    {
        /* -------------------------------------------- Variáveis de classe -------------------------------------------- */
#if DEBUG
        /// <summary>
        /// Representa o quantitativo de objetos do tipo Luz.
        /// </summary>
        public static UInt32 Count { get { return count; } private set { } }


        private static UInt32 count = 0;
#endif

        /* ---------------------------------------------- Variáveis membro ---------------------------------------------- */

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


        internal Vector3 position;

        internal Vector3 rotation;

        internal Vector3 scale;


        /* ---------------------------------------------- Interface pública ---------------------------------------------- */

        /// <summary>
        /// Construtor padrão, com todos os parâmetros
        /// </summary>
        /// <param name="pos"> Posição </param>
        /// <param name="rot"> Rotação </param>
        /// <param name="scl"> Escala </param>
        public Transform(Vector3 pos, Vector3 rot, Vector3 scl)
        {
            position = pos;
            rotation = rot;
            scale = scl;
        }

        /// <summary>
        /// Construtor sem parâmetros, inicializa posição e rotação zerados e escala 1:1
        /// </summary>
        public Transform() : this(Vector3.Zero, Vector3.Zero, Vector3.One) { }

        /// <summary>
        /// Construtor com apenas posição, inicializa rotação zerada e escala 1:1
        /// </summary>
        public Transform(Vector3 pos) : this(pos, Vector3.Zero, Vector3.One) { }

        /// <summary>
        /// Construtor com posição e rotação, inicializa a escala 1:1
        /// </summary>
        public Transform(Vector3 pos, Vector3 rot) : this(pos, rot, Vector3.One) { }

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
        /// <param name="camera"></param>
        /// <returns></returns>
        public Matrix4 GetModelViewProjectionMatrix(PerspectiveCamera camera)
        {
            return camera.GetViewMatrix() * camera.GetProjectionMatrix() * GetModelMatrix();
        }


        // Getters alternativos
        public Vector3 GetPosition() { return position; }
        public Vector3 GetRotation() { return rotation; }
        public Vector3 GetScale() { return scale; }


        // Setters alternativos (p/ dar maior flexibilidade para a API pública)

        // Position
        public void SetPosition(Vector3 Pos) { position = Pos; }
        public void SetPositionX(float X) { position.X = X; }
        public void SetPositionY(float Y) { position.Y = Y; }
        public void SetPositionZ(float Z) { position.Z = Z; }

        public void SetPositionXY(Vector2 Pos)
        {
            position.X = Pos.X;
            position.Y = Pos.Y;
        }

        public void SetPosition(float X, float Y, float Z)
        {
            position.X = X;
            position.Y = Y;
            position.Z = Z;
        }

        // Rotation
        public void SetRotation(Vector3 rot) { rotation = rot; }
        public void SetRotationX(float rot) { rotation.X = rot; }
        public void SetRotationY(float rot) { rotation.Y = rot; }
        public void SetRotationZ(float rot) { rotation.Z = rot; }

        public void SetRotation(float X, float Y, float Z)
        {
            rotation.X = X;
            rotation.Y = Y;
            rotation.Z = Z;
        }

        // Scale
        public void SetScale(Vector3 scl) { scale = scl; }
        public void SetScaleX(float scl) { scale.X = scl; }
        public void SetScaleY(float scl) { scale.Y = scl; }
        public void SetScaleZ(float scl) { scale.Z = scl; }

        public void SetScale(float X, float Y, float Z)
        {
            scale.X = X;
            scale.Y = Y;
            scale.Z = Z;
        }
    }
}
