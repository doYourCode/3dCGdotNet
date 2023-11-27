using _3dCG.Core;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using System.Runtime.InteropServices;

namespace _3dCG.Examples.Basics
{
    internal class Chapter_xx_HelloRenderToTexture : GameWindow
    {
        private float _tick = 0.0f;
        int timeLocation;

        private Shader _shader;
        private Shader _quadShader;
        private Texture _texture;
        private BasicMesh _mesh;
        private Transform _transform;
        private Camera _camera;
        private CameraController _cameraController;

        private float[] data = {
        -1.0f,  -1.0f,  0.0f,
        1.0f,   -1.0f,  0.0f,
        -1.0f,  1.0f,   0.0f,
        -1.0f,  1.0f,   0.0f,
        1.0f,   -1.0f,  0.0f,
        1.0f,   1.0f,   0.0f,
        };

        // Adicione estas variáveis para o framebuffer e a textura de renderização
        private int _screenQuad;
        private int _quadVertexBuffer;
        private int _framebuffer;
        private int _depthRenderBuffer;
        private DrawBuffersEnum[] _drawBuffers = { DrawBuffersEnum.ColorAttachment0};
        private Texture _renderTexture;

        public Chapter_xx_HelloRenderToTexture(
            GameWindowSettings gameWindowSettings,
            NativeWindowSettings nativeWindowSettings) :
            base(gameWindowSettings, nativeWindowSettings)
        {
            Title = "Hello Camera!";
        }

        protected override void OnLoad()
        {
            base.OnLoad();

            GL.Enable(EnableCap.DepthTest);

            _shader = new Shader("HelloRenderToTexture");

            _quadShader = new Shader("HelloRenderToTexture_QuadDraw");
            timeLocation = GL.GetUniformLocation(_quadShader.Handle, "time");

            _texture = Texture.LoadFromFile("Resources/Texture/Suzanne.png", TextureUnit.Texture0);

            _mesh = new BasicMesh("Resources/Mesh/Suzanne.obj");

            _transform = new Transform();

            // We initialize the camera so that it is 3 units back from where the rectangle is.
            // We also give it the proper aspect ratio.
            _camera = new Camera(Vector3.UnitZ * 3, Size.X / (float)Size.Y);

            _cameraController = new CameraController(_camera, this);

            GL.ClearColor(0.1f, 0.1f, 0.2f, 1.0f);

            // We make the mouse cursor invisible and captured so we can have proper FPS-camera movement.
            CursorState = CursorState.Grabbed;

            _renderTexture = Texture.CreateInMemory(Size.X, Size.Y, TextureUnit.Texture1);

            // Crie o framebuffer e a textura de renderização
            _framebuffer = GL.GenFramebuffer();
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, _framebuffer);

            _depthRenderBuffer = GL.GenFramebuffer();
            GL.BindRenderbuffer(RenderbufferTarget.Renderbuffer, _depthRenderBuffer);
            GL.RenderbufferStorage(RenderbufferTarget.Renderbuffer, RenderbufferStorage.DepthComponent, Size.X, Size.Y);
            GL.FramebufferRenderbuffer(FramebufferTarget.Framebuffer, FramebufferAttachment.DepthAttachment, RenderbufferTarget.Renderbuffer, _depthRenderBuffer);

            GL.FramebufferTexture2D(FramebufferTarget.Framebuffer, FramebufferAttachment.ColorAttachment0, TextureTarget.Texture2D, _renderTexture.Handle, 0);
            GL.DrawBuffers(1, _drawBuffers);

            if (GL.CheckFramebufferStatus(FramebufferTarget.Framebuffer) != FramebufferErrorCode.FramebufferComplete)
                return;

            GL.BindFramebuffer(FramebufferTarget.Framebuffer, 0);

            _screenQuad = GL.GenVertexArray();
            GL.BindVertexArray(_screenQuad);

            _quadVertexBuffer = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, _quadVertexBuffer);
            GL.BufferData(BufferTarget.ArrayBuffer, data.Length * sizeof(float), data, BufferUsageHint.StaticDraw);
        }

        protected override void OnRenderFrame(FrameEventArgs args)
        {
            base.OnRenderFrame(args);

            // Renderize para a textura
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, _framebuffer);
            GL.Viewport(0, 0, Size.X / 2, Size.Y / 2);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            _texture.Use(TextureUnit.Texture0);
            _shader.Use();

            _mesh.Draw();

            // Volte para o framebuffer padrão
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, 0);
            GL.Viewport(0, 0, Size.X, Size.Y);
            // QUAD DRAW HERE
            _renderTexture.Use(TextureUnit.Texture1);
            _quadShader.Use();
            // Update uniforms
            GL.Uniform1(timeLocation,_tick);
            GL.BindVertexArray(_screenQuad);

            GL.DrawArrays(PrimitiveType.Triangles, 0, 2);

            GL.BindVertexArray(0);

            SwapBuffers();
        }

        protected override void OnUpdateFrame(FrameEventArgs args)
        {
            base.OnUpdateFrame(args);

            // Rotate the model matrix
            _transform.SetRotationY(_tick);
            // Identity matrix (per object)
            _shader.SetMatrix4("model", _transform.GetModelMatrix());
            // Camera matrices
            _shader.SetMatrix4("view", _camera.GetViewMatrix());
            _shader.SetMatrix4("projection", _camera.GetProjectionMatrix());

            _tick += 0.0001f;

            _cameraController.Update(args, KeyboardState, MouseState);
        }

        protected override void OnMouseWheel(MouseWheelEventArgs e)
        {
            base.OnMouseWheel(e);

            _cameraController.MouseUpdate(e);
        }

        protected override void OnUnload()
        {
            base.OnUnload();

            _mesh.Delete();
        }
    }
}