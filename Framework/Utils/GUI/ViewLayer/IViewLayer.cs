using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Utils.GUI.ViewLayer
{
    public interface IViewLayer
    {
        public void Load(GameWindow window);
        public void Update(GameWindow window, FrameEventArgs args);
        public void Render(FrameEventArgs e);
        public void Unload();
        public void RenderViewLayer();
    }
}
