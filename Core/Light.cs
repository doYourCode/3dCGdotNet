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

        public Light(Vector3 position, Vector3 Direction, Vector3 color, float ambientIntensity, float diffuseIntensity)
        {
            Position = position;
            Direction = Direction;
            Color = color;
            AmbientIntensity = ambientIntensity;
            DiffuseIntensity = diffuseIntensity;

            lightProjection = Matrix4.CreateOrthographic(-10.0f, 10.0f, near_plane, far_plane);
            lightView = Matrix4.LookAt(Position, Vector3.Zero, new Vector3(0.0f, 1.0f, 0.0f));
            LightSpaceMatrix = lightProjection * lightView;
        }

    }
}
