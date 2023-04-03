using OpenTK.Mathematics;

namespace _3dCG.Core
{
    public class Transform
    {
        private Vector3 position;
        private Vector3 rotation;
        private Vector3 scale;

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
            this.scale = scl;
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

        public Matrix4 GetModelViewProjectionMatrix(Camera camera)
        {
            return camera.GetViewMatrix() * camera.GetProjectionMatrix() * GetModelMatrix();
        }

        public Vector3 GetPosition() { return position; }
        public Vector3 GetRotation() { return rotation; }
        public Vector3 GetScale() { return scale; }

        public void SetPosition(Vector3 pos) { position = pos; }
        public void SetRotation(Vector3 rot) { rotation = rot; }
        public void SetScale(Vector3 scl) { scale = scl; }
    }
}
