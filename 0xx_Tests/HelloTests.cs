using Framework.Core.Buffer;
using Framework.Core.Vertex;
using Framework.Utils;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;

namespace Examples
{
    /// <summary>
    /// Demonstra como desenhar um triângulo na tela, carregar um buffer de dados na memória de video, e fazer uma
    /// chamada de desenho utilizando a funcionalidade disponível na biblioteca OpenTK.
    /// </summary>
    internal class HelloTests : GameWindow
    {
        VertexBufferObject vbo;
        VertexArrayObject vao;

        public HelloTests(
            GameWindowSettings gameWindowSettings,
            NativeWindowSettings nativeWindowSettings) :
            base(gameWindowSettings, nativeWindowSettings) { }

        protected override void OnLoad()
        {
            base.OnLoad();

            float[] data =
            {
            //  X       Y       Z
               -0.75f, -0.75f,  0.0f,   // Vértice 0 -> canto inferior esquerdo
                0.7f , -0.75f,  0.0f,    // Vértice 1 -> canto inferior direito
                0.0f,   0.75f,  0.0f    // Vértice 2 -> canto superior (no centro da tela)
            };

            vbo = new VertexBufferObject(data);

            VertexFormat vertexFormat = new VertexFormat();
            // Adiciona atributo "Position" e o associa ao VBO
            vertexFormat.AddAttribute(VertexAttributeType.Position, vbo);

            vao = new VertexArrayObject(vertexFormat);

            GL.ClearColor(1.0f, 1.0f, 0.0f, 1.0f);

        }
        protected override void OnRenderFrame(FrameEventArgs args)
        {
            base.OnRenderFrame(args);

            GL.Clear(ClearBufferMask.ColorBufferBit);

            Draw.Triangles(vao, 0, 3);
            //Draw.Points(vao, 0, 3, 10);
            //Draw.Lines(vao, 0, 3, 10);

            SwapBuffers();
        }

        /// <summary>
        /// Apaga os dados não gerenciados carregados na memória de vídeo
        /// </summary>
        protected override void OnUnload()
        {
            base.OnUnload();

            vbo.Delete();
            vao.Delete();
        }
    }
}
