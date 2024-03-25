using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Framework.Core.Vertex
{
    public class VertexFormat
    {
        private Dictionary<string, Attribute> attributes;

        public VertexFormat()
        {
            attributes = new Dictionary<string, Attribute>();
        }

        public void AddUnique(Attribute attrib)
        {
            // TODO: code to add an unique attribute withing a VBO.
        }
    }
}
