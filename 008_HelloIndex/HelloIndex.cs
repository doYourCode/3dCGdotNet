// <copyright file="HelloIndex.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Examples
{
    using Framework.Core;
    using Framework.Utils.GUI;
    using OpenTK.Graphics.OpenGL4;
    using OpenTK.Windowing.Common;
    using OpenTK.Windowing.Desktop;

    /// <inheritdoc/>
    internal class HelloIndex : GameWindow
    {
        private const int VERTEXSIZE = 8 * sizeof(float);
        private const int POSITION = 0;
        private const int COLOR = 1;
        private const int UV = 2;
        private readonly int[] vOFFSET = { 0, 12, 24 };

        private FPSCounter fpsCounter;

        private int indexCount = 0;

        private int vertexBufferObject;
        private int vertexArrayObject;
        private int indexBuffer;

        private Shader shader;
        private Texture texture;

        /// <summary>
        /// Initializes a new instance of the <see cref="HelloIndex"/> class.
        /// </summary>
        /// <param name="gameWindowSettings"> PARAM TODO. </param>
        /// <param name="nativeWindowSettings"> PARAM2 TODO. </param>
        public HelloIndex(
            GameWindowSettings gameWindowSettings,
            NativeWindowSettings nativeWindowSettings)
            : base(gameWindowSettings, nativeWindowSettings)
        {
        }

        /// <inheritdoc/>
        protected override void OnLoad()
        {
            base.OnLoad();

            this.fpsCounter = new FPSCounter(this);

            float[] data =
            {
            // .Position                            Color                               Texture coordinates
            // .X           Y           Z           R           G           B           U           V
                -0.125000f, 0.083333f,  -0.000050f, 0.000000f,  1.000000f,  0.000000f,  1.000000f,  -0.000000f, // Triangle
                -0.500000f, 0.833333f,  -0.000050f, 0.000000f,  0.000000f,  1.000000f,  0.500000f,  -1.000000f,
                -0.875000f, 0.083333f,  -0.000050f, 1.000000f,  0.000000f,  0.000000f,  0.000000f,  -0.000000f,

                0.125000f,  0.125000f,  -0.000050f, 1.000000f,  0.000000f,  0.000000f,  0.000000f,  -0.000000f, // Square
                0.875000f,  0.125000f,  -0.000050f, 0.000000f,  1.000000f,  0.000000f,  1.000000f,  -0.000000f,
                0.875000f,  0.875000f,  -0.000050f, 0.000000f,  0.000000f,  1.000000f,  1.000000f,  -1.000000f,
                0.125000f,  0.875000f,  -0.000050f, 0.000000f,  0.000000f,  1.000000f,  0.000000f,  -1.000000f,

                -0.125000f, -0.500000f, -0.000050f, 0.000000f,  1.000000f,  0.000000f,  1.000000f,  -0.500000f, // Hexagon
                -0.312500f, -0.175241f, -0.000050f, 1.000000f,  0.000000f,  0.000000f,  0.750000f,  -1.000000f,
                -0.687500f, -0.175241f, -0.000050f, 1.000000f,  0.000000f,  0.000000f,  0.250000f,  -1.000000f,
                -0.875000f, -0.500000f, -0.000050f, 0.000000f,  1.000000f,  0.000000f,  0.000000f,  -0.500000f,
                -0.687500f, -0.824760f, -0.000050f, 0.000000f,  0.000000f,  1.000000f,  0.250000f,  -0.000000f,
                -0.312500f, -0.824760f, -0.000050f, 0.000000f,  0.000000f,  1.000000f,  0.750000f,  -0.000000f,

                0.875000f,  -0.500000f, -0.000050f, 1.000000f,  0.000000f,  0.000000f,  1.000000f,  -0.500000f, // Circle
                0.868952f,  -0.432643f, -0.000050f, 0.944444f,  0.000000f,  0.000000f,  0.991936f,  -0.589810f,
                0.851518f,  -0.369226f, -0.000050f, 0.888888f,  0.000000f,  0.000000f,  0.968690f,  -0.674366f,
                0.823760f,  -0.310813f, -0.000050f, 0.833333f,  0.000000f,  0.000000f,  0.931680f,  -0.752249f,
                0.786743f,  -0.258469f, -0.000050f, 0.777777f,  0.000000f,  0.000000f,  0.882324f,  -0.822041f,
                0.741531f,  -0.213257f, -0.000050f, 0.722222f,  0.000000f,  0.000000f,  0.822041f,  -0.882324f,
                0.689187f,  -0.176240f, -0.000050f, 0.666666f,  0.000000f,  0.000000f,  0.752249f,  -0.931680f,
                0.630774f,  -0.148483f, -0.000050f, 0.611111f,  0.000000f,  0.000000f,  0.674366f,  -0.968690f,
                0.567357f,  -0.131048f, -0.000050f, 0.555555f,  0.000000f,  0.000000f,  0.589810f,  -0.991936f,
                0.500000f,  -0.125000f, -0.000050f, 0.500000f,  0.000000f,  0.000000f,  0.500000f,  -1.000000f,
                0.432643f,  -0.131048f, -0.000050f, 0.444444f,  0.000000f,  0.000000f,  0.410190f,  -0.991936f,
                0.369226f,  -0.148483f, -0.000050f, 0.388888f,  0.000000f,  0.000000f,  0.325634f,  -0.968690f,
                0.310813f,  -0.176240f, -0.000050f, 0.333333f,  0.000000f,  0.000000f,  0.247751f,  -0.931680f,
                0.258469f,  -0.213257f, -0.000050f, 0.277777f,  0.000000f,  0.000000f,  0.177959f,  -0.882324f,
                0.213257f,  -0.258469f, -0.000050f, 0.222222f,  0.000000f,  0.000000f,  0.117676f,  -0.822041f,
                0.176240f,  -0.310814f, -0.000050f, 0.166666f,  0.000000f,  0.000000f,  0.068320f,  -0.752249f,
                0.148483f,  -0.369226f, -0.000050f, 0.111111f,  0.000000f,  0.000000f,  0.031310f,  -0.674366f,
                0.131048f,  -0.432643f, -0.000050f, 0.055555f,  0.000000f,  0.000000f,  0.008064f,  -0.589810f,
                0.125000f,  -0.500000f, -0.000050f, 0.111111f,  0.000000f,  0.000000f,  0.000000f,  -0.500000f,
                0.131048f,  -0.567358f, -0.000050f, 0.166666f,  0.000000f,  0.000000f,  0.008064f,  -0.410190f,
                0.148482f,  -0.630774f, -0.000050f, 0.222222f,  0.000000f,  0.000000f,  0.031310f,  -0.325634f,
                0.176240f,  -0.689187f, -0.000050f, 0.277777f,  0.000000f,  0.000000f,  0.068320f,  -0.247751f,
                0.213257f,  -0.741531f, -0.000050f, 0.333333f,  0.000000f,  0.000000f,  0.117676f,  -0.177959f,
                0.258469f,  -0.786743f, -0.000050f, 0.388888f,  0.000000f,  0.000000f,  0.177959f,  -0.117676f,
                0.310813f,  -0.823760f, -0.000050f, 0.444444f,  0.000000f,  0.000000f,  0.247751f,  -0.068320f,
                0.369226f,  -0.851518f, -0.000050f, 0.500000f,  0.000000f,  0.000000f,  0.325634f,  -0.031310f,
                0.432643f,  -0.868952f, -0.000050f, 0.555555f,  0.000000f,  0.000000f,  0.410190f,  -0.008064f,
                0.500000f,  -0.875000f, -0.000050f, 0.611111f,  0.000000f,  0.000000f,  0.500000f,  -0.000000f,
                0.567358f,  -0.868952f, -0.000050f, 0.666666f,  0.000000f,  0.000000f,  0.589810f,  -0.008064f,
                0.630774f,  -0.851518f, -0.000050f, 0.722222f,  0.000000f,  0.000000f,  0.674366f,  -0.031310f,
                0.689187f,  -0.823760f, -0.000050f, 0.777777f,  0.000000f,  0.000000f,  0.752249f,  -0.068320f,
                0.741531f,  -0.786743f, -0.000050f, 0.833333f,  0.000000f,  0.000000f,  0.822041f,  -0.117676f,
                0.786743f,  -0.741531f, -0.000050f, 0.888888f,  0.000000f,  0.000000f,  0.882324f,  -0.177959f,
                0.823760f,  -0.689187f, -0.000050f, 0.944444f,  0.000000f,  0.000000f,  0.931680f,  -0.247751f,
                0.851517f,  -0.630774f, -0.000050f, 1.000000f,  0.000000f,  0.000000f,  0.968690f,  -0.325634f,
                0.868952f,  -0.567358f, -0.000050f, 1.000000f,  0.000000f,  0.000000f,  0.991936f,  -0.410190f,
            };

            int[] indices =
            {
                0, 1, 2, // Triangle
                3, 4, 5, 5, 6, 3, // Square
                10, 7, 8, 9, 8, 10, 11, 7, 10, 11, 12, 7, // Hexagon
                13, 14, 15, 13, 15, 16, 13, 16, 17, 13, 17, 18, 13, 18, 19, 13, 19, 20, 13, 20, 21, 13, 21, 22,  // Circle
                13, 22, 23, 13, 23, 24, 13, 24, 25, 13, 25, 26, 13, 26, 27, 13, 27, 28, 13, 28, 29, 13, 29, 30,
                13, 30, 31, 13, 31, 32, 13, 32, 33, 13, 33, 34, 13, 34, 35, 13, 35, 36, 13, 36, 37, 13, 37, 38,
                13, 38, 39, 13, 39, 40, 13, 40, 41, 13, 41, 42, 13, 42, 43, 13, 43, 44, 13, 44, 45, 13, 45, 46,
                13, 46, 47, 13, 47, 48,
            };

            this.indexCount = indices.Length;

            // Generate the buffer
            this.vertexBufferObject = GL.GenBuffer();

            // Points to the active buffer
            GL.BindBuffer(BufferTarget.ArrayBuffer, this.vertexBufferObject);

            // Insert the data into the buffer
            GL.BufferData(BufferTarget.ArrayBuffer, data.Length * sizeof(float), data, BufferUsageHint.StaticDraw);

            // Generate the array object buffer
            this.vertexArrayObject = GL.GenVertexArray();

            // Points to the array object
            GL.BindVertexArray(this.vertexArrayObject);

            // Position attribute
            GL.VertexAttribPointer(POSITION, 3, VertexAttribPointerType.Float, false, VERTEXSIZE, this.vOFFSET[POSITION]);
            GL.EnableVertexAttribArray(POSITION);

            // Color attribute
            GL.VertexAttribPointer(COLOR, 3, VertexAttribPointerType.Float, false, VERTEXSIZE, this.vOFFSET[COLOR]);
            GL.EnableVertexAttribArray(COLOR);

            // Texture coordinates attribute
            GL.VertexAttribPointer(UV, 2, VertexAttribPointerType.Float, false, VERTEXSIZE, this.vOFFSET[UV]);
            GL.EnableVertexAttribArray(UV);

            // Create and bind index buffer (information about the faces triangulation)
            this.indexBuffer = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, this.indexBuffer);
            GL.BufferData(BufferTarget.ElementArrayBuffer, sizeof(int) * this.indexCount, indices, BufferUsageHint.StaticDraw);

            this.shader = new Shader("HelloIndex");

            this.texture = Texture.LoadFromFile("Uv_checker_01.png", TextureUnit.Texture0);

            GL.ClearColor(0.1f, 0.1f, 0.2f, 1.0f);
        }

        /// <inheritdoc/>
        protected override void OnRenderFrame(FrameEventArgs args)
        {
            base.OnRenderFrame(args);

            GL.Clear(ClearBufferMask.ColorBufferBit);

            this.texture.Use(TextureUnit.Texture0);
            this.shader.Use();

            GL.BindVertexArray(this.vertexArrayObject);
            GL.DrawElements(BeginMode.Triangles, this.indexCount, DrawElementsType.UnsignedInt, 0);
            GL.BindVertexArray(0);

            this.SwapBuffers();
        }

        /// <inheritdoc/>
        protected override void OnUpdateFrame(FrameEventArgs args)
        {
            base.OnUpdateFrame(args);

            this.fpsCounter.Update(args);
        }

        /// <inheritdoc/>
        protected override void OnUnload()
        {
            base.OnUnload();

            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0);
            GL.BindVertexArray(0);

            GL.DeleteBuffer(this.vertexBufferObject);
            GL.DeleteBuffer(this.indexBuffer);
            GL.DeleteVertexArray(this.vertexArrayObject);

            this.texture.Dispose();
            this.shader.Dispose();
        }
    }
}
