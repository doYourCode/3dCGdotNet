using OpenTK.Mathematics;

namespace _3dCG.Core
{
    internal class Light
    {
        public Vector3 Position { get; set; }
        public Vector3 Direction { get; set; }
        public Vector3 Color { get; set; }
        public float AmbientIntensity { get; set; }
        public float DiffuseIntensity { get; set; }

        Matrix4 lightProjection, lightView;
        public Matrix4 LightSpaceMatrix { get; set; }
        float near_plane = 1.0f, far_plane = 7.5f;

        public Light(Vector3 position, Vector3 direction, Vector3 color, float ambientIntensity, float diffuseIntensity)
        {
            Position = position;
            Direction = direction;
            Color = color;
            AmbientIntensity = ambientIntensity;
            DiffuseIntensity = diffuseIntensity;

            lightProjection = Matrix4.CreateOrthographic(-10.0f, 10.0f, near_plane, far_plane);
            lightView = Matrix4.LookAt(Position, Vector3.Zero, new Vector3(0.0f, 1.0f, 0.0f));
            LightSpaceMatrix = lightProjection * lightView;
        }

        public Matrix4 GetViewProjectionMatrix()
        {
            float nearPlane = 1.0f;
            float farPlane = 100.0f;
            Matrix4 lightProjection = Matrix4.CreateOrthographicOffCenter(-10.0f, 10.0f, -10.0f, 10.0f, nearPlane, farPlane);
            Matrix4 lightView = Matrix4.LookAt(Position, Position + Direction, Vector3.UnitY);
            return lightView * lightProjection;
        }

        public Matrix4 GetViewMatrix()
        {
            return Matrix4.LookAt(Position, Position + Direction, Vector3.UnitY);
        }

        public Matrix4 GetProjectionMatrix()
        {
            float nearPlane = 1.0f;
            float farPlane = 100.0f;
            return Matrix4.CreateOrthographicOffCenter(-10.0f, 10.0f, -10.0f, 10.0f, nearPlane, farPlane);
        }

    }
}
