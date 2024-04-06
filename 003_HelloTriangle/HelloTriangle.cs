// <copyright file="HelloTriangle.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Examples
{
    using Framework.Utils;
    using OpenTK.Graphics.OpenGL;
    using OpenTK.Windowing.Common;
    using OpenTK.Windowing.Desktop;

    /// <summary>
    /// Demonstra como desenhar um triângulo na tela, carregar um buffer de dados na memória de video, e fazer uma
    /// chamada de desenho utilizando a funcionalidade disponível na biblioteca OpenTK.
    /// </summary>
    internal class HelloTriangle : GameWindow
    {
        // Constantes relacionadas aos atributos dos vértices
        private static readonly int POSITION = 0;
        private static readonly int[] OFFSET = { 0 };
        private static readonly int VERTEX_SIZE = 3 * sizeof(float);

        private float pointSize = 4.0f;

        // Para guardar os dados de vértices na memória de vídeo nós usaremos buffers, esses buffers requerem
        // uma referência (handlers) que guardamos como índices em números inteiros.
        private int vertexBufferObject;
        private int vertexArrayObject;

        /// <summary>
        /// Initializes a new instance of the <see cref="HelloTriangle"/> class.
        /// </summary>
        /// <param name="gameWindowSettings"> PARAM TODO. </param>
        /// <param name="nativeWindowSettings"> PARAM2 TODO. </param>
        public HelloTriangle(
            GameWindowSettings gameWindowSettings,
            NativeWindowSettings nativeWindowSettings)
            : base(gameWindowSettings, nativeWindowSettings)
        {
            this.Title = "Hello Triangle!";
        }

        /// <inheritdoc/>
        protected override void OnLoad()
        {
            base.OnLoad();

            // Dados que serão carregados no buffer (por hora só temos as posições dos vértices de um triângulo)
            float[] data =
            {
                // Posições (no eixo X, Y e Z respectivamente)
                -0.75f, -0.75f, 0.0f, // Vértice 0 -> canto inferior esquerdo
                0.7f , -0.75f, 0.0f, // Vértice 1 -> canto inferior direito
                0.0f  , 0.75f , 0.0f  // Vértice 2 -> canto superior (no centro da tela)
            };

            // Gerar o buffer do objeto array de vértices (VAO) e armazenar o identificador do buffer na variável
            // vertexArrayObject (VAO)
            //
            // O VAO é um objeto que armazena o estado de todos os atributos de um buffer de vértices (VBO)
            //
            // Você pode criar o VAO antes ou depois dos VBOs, mas ambos devem ser criados antes de definir os dados
            // de atributo. Um VAO é simplesmente um contêiner para relacionar vários buffers (VBOs)
            this.vertexArrayObject = GL.GenVertexArray();

            // Aponta para o objeto array de vértices (VAO) que está ativo no momento (o objeto array de vértices que
            // está ativo é o que foi armazenado na variável vertexArrayObject)
            GL.BindVertexArray(this.vertexArrayObject);

            // Gerar o objeto buffer de vértices (VBO) e armazenar o identificador do buffer na variável vertexBufferObject
            this.vertexBufferObject = GL.GenBuffer();

            // Ativa o buffer para ser alterado pelas próximas chamadas (Isso é necessário pois o opengl é como uma grande
            // maquina de estados, e para cada chamada é necessário informar o estado atual)
            GL.BindBuffer(BufferTarget.ArrayBuffer, this.vertexBufferObject);

            // Carrega os dados no buffer de vértices (VBO) que está ativo no momento (o buffer de vértices que está ativo
            // é o que foi armazenado na variável vertexBufferObject)
            GL.BufferData(BufferTarget.ArrayBuffer, data.Length * sizeof(float), data, BufferUsageHint.StaticDraw);

            // Cria e ativa um ponteiro de atributo associado ao buffer de vértices (VBO) que está ativo no momento
            GL.VertexAttribPointer(POSITION, 3, VertexAttribPointerType.Float, false, VERTEX_SIZE, OFFSET[POSITION]);
            GL.EnableVertexAttribArray(POSITION);

            GL.ClearColor(1.0f, 1.0f, 0.0f, 1.0f);
        }

        /// <inheritdoc/>
        protected override void OnRenderFrame(FrameEventArgs args)
        {
            base.OnRenderFrame(args);
            GL.Clear(ClearBufferMask.ColorBufferBit);

            // Ativa o VAO para ser desenhado
            GL.BindVertexArray(this.vertexArrayObject);

            // Chamada à função Draw
            GL.DrawArrays(PrimitiveType.Triangles, 0, 3);
            GL.BindVertexArray(0);

            this.SwapBuffers();
        }

        /// <inheritdoc/>
        protected override void OnUpdateFrame(FrameEventArgs args)
        {
            base.OnUpdateFrame(args);

            GL.PointSize(this.pointSize);
        }

        /// <inheritdoc/>
        protected override void OnUnload()
        {
            base.OnUnload();

            // A memória de vídeo (que recebe os dados dos buffers) não é gerenciada pela máquina virtual da linguagem,
            // dessa forma é necessário o programador apagar os dados quando eles não forem mais necessários.
            GL.BindBuffer(BufferTarget.ArrayBuffer, CONSTANTS.ZERO);
            GL.BindVertexArray(CONSTANTS.ZERO);
            GL.DeleteBuffer(this.vertexBufferObject);
            GL.DeleteVertexArray(this.vertexArrayObject);
        }

        /// <inheritdoc/>
        protected override void OnMouseWheel(MouseWheelEventArgs e)
        {
            base.OnMouseWheel(e);

            this.pointSize += e.OffsetY;
            if (this.pointSize > 8.0f)
            {
                this.pointSize = 8.0f;
            }
            else if (this.pointSize < 2.0f)
            {
                this.pointSize = 2.0f;
            }
        }
    }
}
