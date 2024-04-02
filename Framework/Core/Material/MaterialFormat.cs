using OpenTK.Mathematics;

namespace Framework.Core.Material
{
    public class MaterialFormat
    {
        internal Dictionary<string, MaterialAttribute> attributes;

        internal ref Dictionary<string, MaterialAttribute> Attributes { get => ref attributes; } 

        public MaterialFormat()
        {
            attributes = new Dictionary<string, MaterialAttribute>();
        }

        public void GetUniformLocations(Shader shader)
        {
            foreach(MaterialAttribute attribute in attributes.Values)
            {
                attribute.GetUniformLocation(shader);
            }
        }

        public void UpdateUniforms()
        {
            foreach(MaterialAttribute attribute in attributes.Values)
            {
                attribute.UpdateUniform();
            }
        }

        public void AddFloat(string name, float value)
        {
            attributes.Add(name, new FloatAttribute(name, value));
        }

        public void AddVector2(string name, System.Numerics.Vector2 value)
        {
            attributes.Add(name, new Vector2Attribute(name, value));
        }

        public void AddVector3(string name, System.Numerics.Vector3 value)
        {
            attributes.Add(name, new Vector3Attribute(name, value));
        }

        public void AddVector4(string name, System.Numerics.Vector4 value)
        {
            attributes.Add(name, new Vector4Attribute(name, value));
        }

        public void PrintLayout()
        {
            foreach (MaterialAttribute attribute in attributes.Values)
            {
                Console.WriteLine(attribute.label);
            }
        }
    }
}
