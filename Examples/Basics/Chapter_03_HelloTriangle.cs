using OpenTK.Graphics.OpenGL;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using System;

namespace _3dCG.Examples.Basics
{
    /// <summary>
    /// Demonstra como desenhar um triângulo na tela, carregar um buffer de dados na memória de video, e fazer uma chamada de desenho utilizando a funcionalidade disponível na biblioteca OpenTK.
    /// </summary>
    internal class Chapter_03_HelloTriangle : GameWindow
    {

        private float _pointSize = 4.0f;

        // TODO: Criar uma classe para gerenciar os buffers de dados, e permitir que o usuário configure os atributos de cada buffer.

        // Constantes relacionadas aos atributos dos vértices
        private const int POSITION = 0;
        private readonly int[] OFFSET = { 0 };
        private const int VERTEX_SIZE = 3 * sizeof(float);

        // Para guardar os dados de vértices na memória de vídeo nós usaremos buffers, esses buffers requerem umareferência (handlers) que guardamos como índices em números inteiros.
        private int _vertexBufferObject;
        private int _vertexArrayObject;

        public Chapter_03_HelloTriangle(
            GameWindowSettings gameWindowSettings,
            NativeWindowSettings nativeWindowSettings) :
            base(gameWindowSettings, nativeWindowSettings)
        {
            Title = "Hello Triangle!";
        }

        protected override void OnLoad()
        {
            base.OnLoad();

            // Dados que serão carregados no buffer (por hora só temos as posições dos vértices de um triângulo)
            float[] _data =
            {
                // Posições (no eixo X, Y e Z respectivamente)

                -0.75f, -0.75f, 0.0f, // Vértice 0 -> canto inferior esquerdo
                0.7f , -0.75f, 0.0f, // Vértice 1 -> canto inferior direito
                0.0f  , 0.75f , 0.0f  // Vértice 2 -> canto superior (no centro da tela)
            };

            // Gerar o buffer do objeto array de vértices (VAO) e armazenar o identificador do buffer na variável _vertexArrayObject (VAO)
            // O VAO é um objeto que armazena o estado de todos os atributos de um buffer de vértices (VBO)
            // Você pode criar o VAO antes ou depois dos VBOs, mas ambos devem ser criados antes de definir os dados de atributo. Um VAO é simplesmente um contêiner para o estado relacionado (ou seja, as fontes de dados para cada atributo, além de uma matriz de elementos)
            _vertexArrayObject = GL.GenVertexArray();

            // Aponta para o objeto array de vértices (VAO) que está ativo no momento (o objeto array de vértices que está ativo é o que foi armazenado na variável _vertexArrayObject)
            GL.BindVertexArray(_vertexArrayObject);

            // Gerar o objeto buffer de vértices (VBO) e armazenar o identificador do buffer na variável _vertexBufferObject
            _vertexBufferObject = GL.GenBuffer();

            // Ativa o buffer para ser alterado pelas próximas chamadas (Isso é necessário pois o opengl é como uma grande maquina de estados, e para cada chamada é necessário informar o estado atual)
            GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject);

            // Carrega os dados no buffer de vértices (VBO) que está ativo no momento (o buffer de vértices que está ativo é o que foi armazenado na variável _vertexBufferObject)
            GL.BufferData(BufferTarget.ArrayBuffer, _data.Length * sizeof(float), _data, BufferUsageHint.StaticDraw);

            // Cria e ativa um ponteiro de atributo associado ao buffer de vértices (VBO) que está ativo no momento
            GL.VertexAttribPointer(POSITION, 3, VertexAttribPointerType.Float, false, VERTEX_SIZE, OFFSET[POSITION]);
            GL.EnableVertexAttribArray(POSITION);

            GL.ClearColor(1.0f, 1.0f, 0.0f, 1.0f);

        }
        protected override void OnRenderFrame(FrameEventArgs args)
        {
            base.OnRenderFrame(args);
            GL.Clear(ClearBufferMask.ColorBufferBit);

            // Ativa o VAO para ser desenhado
            GL.BindVertexArray(_vertexArrayObject);
            // Chamada à função Draw
            GL.DrawArrays(PrimitiveType.Triangles, 0, 3);
            GL.BindVertexArray(0);

            SwapBuffers();
        }

        protected override void OnUpdateFrame(FrameEventArgs args)
        {
            base.OnUpdateFrame(args);

            GL.PointSize(_pointSize);
            
        }

        /// <summary>
        /// Apaga os dados não gerenciados carregados na memória de vídeo
        /// </summary>
        protected override void OnUnload()
        {
            base.OnUnload();

            // A memória de vídeo (que recebe os dados dos buffers) não é gerenciada pela máquina virtual da linguagem, dessa forma é necessário o programador apagar os dados quando eles não forem mais necessários.
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.BindVertexArray(0);
            GL.DeleteBuffer(_vertexBufferObject);
            GL.DeleteVertexArray(_vertexArrayObject);
        }

        protected override void OnMouseWheel(MouseWheelEventArgs e)
        {
            base.OnMouseWheel(e);

            _pointSize += e.OffsetY;
            if (_pointSize > 8.0f ) { _pointSize = 8.0f; }
            else if (_pointSize < 2.0f) { _pointSize = 2.0f; }
        }
    }
}
