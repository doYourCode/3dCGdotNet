// <copyright file="HelloCamera.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Examples
{
    using ExamplesCommon;
    using Framework.Core;
    using Framework.Core.Camera;
    using Framework.Utils;
    using Framework.Utils.GUI;
    using OpenTK.Graphics.OpenGL4;
    using OpenTK.Mathematics;
    using OpenTK.Windowing.Common;
    using OpenTK.Windowing.Desktop;

    /// <inheritdoc/>
    internal class TestApp : GameWindow
    {
        // FBO EXAMPLE DATA

        private int WIDTH;
        private int HEIGHT;

        private const float FBOSCALE = 1.0f;

        private int FBOWIDTH;
        private int FBOHEIGHT;

        private const int SHADOWSIZE = 1024;

        // Number of samples per pixel for MSAA
        private int samples = 8;

        // Controls the gamma function
        private float gamma = 2.2f;

        private Shader shaderProgram;
        private Shader framebufferProgram;
        private Shader shadowMapProgram;

        private Vector4 lightColor = new Vector4(1.0f, 1.0f, 1.0f, 1.0f);
        private Vector3 lightPos = new Vector3(0.5f, 0.5f, 0.5f);

        private int normalFBO, framebufferTexture, RBO, postProcessingFBO, postProcessingTexture, shadowMapFBO, shadowMapTexture;

        private Matrix4 orthgonalProjection, lightView, lightProjection;

        private float[] clampColor = { 1.0f, 1.0f, 1.0f, 1.0f };

        // END OF FBO EXAMPLE DATA

        private FPSCounter fpsCounter;

        private float tick = 0.0f;

        private Shader shader;
        private Texture monkeyTexture;
        private BasicMesh mesh;
        private Transform transform;
        private PerspectiveCamera camera;
        private CameraController cameraController;

        /// <summary>
        /// Initializes a new instance of the <see cref="TestApp"/> class.
        /// </summary>
        /// <param name="gameWindowSettings"> PARAM TODO. </param>
        /// <param name="nativeWindowSettings"> PARAM2 TODO. </param>
        public TestApp(
            GameWindowSettings gameWindowSettings,
            NativeWindowSettings nativeWindowSettings)
            : base(gameWindowSettings, nativeWindowSettings)
        {
            WIDTH = this.ClientSize.X;
            HEIGHT = this.ClientSize.Y;

            FBOWIDTH = (int)(WIDTH * FBOSCALE);
            FBOHEIGHT = (int)(HEIGHT * FBOSCALE);
        }

        /// <inheritdoc/>
        protected override void OnLoad()
        {
            base.OnLoad();

            this.fpsCounter = new FPSCounter(this);

            GL.Enable(EnableCap.DepthTest);

            this.shader = new Shader("HelloCamera");

            this.monkeyTexture = Texture.LoadFromFile("Suzanne.png", TextureUnit.Texture3);

            this.mesh = new BasicMesh("Monkey.fbx");

            this.transform = new Transform();

            // We initialize the camera so that it is 3 units back from where the rectangle is.
            // We also give it the proper aspect ratio.
            this.camera = new PerspectiveCamera(Vector3.UnitZ * 2, this.Size.X / (float)this.Size.Y);
            this.camera.Fov *= 0.8f; // Zoom in.

            this.cameraController = new CameraController(this.camera, this);

            GL.ClearColor(0.1f, 0.1f, 0.2f, 1.0f);

            // FBO EXAMPLE DATA INIT

            this.shaderProgram = new ("default");
            this.framebufferProgram = new ("framebuffer");
            this.shadowMapProgram = new ("shadowMap");

            this.shaderProgram.Use();
            GL.Uniform4(GL.GetUniformLocation(this.shaderProgram.ID, "lightColor"), this.lightColor.X, this.lightColor.Y, this.lightColor.Z, this.lightColor.W);
            GL.Uniform3(GL.GetUniformLocation(this.shaderProgram.ID, "lightPos"), this.lightPos.X, this.lightPos.Y, this.lightPos.Z);
            this.framebufferProgram.Use();
            GL.Uniform1(GL.GetUniformLocation(this.framebufferProgram.ID, "fboTexture"), 0);
            GL.Uniform1(GL.GetUniformLocation(this.framebufferProgram.ID, "gamma"), this.gamma);

            // Enables the Depth Buffer
            GL.Enable(EnableCap.DepthTest);

            // Enables Multisampling
            GL.Enable(EnableCap.Multisample);

            // Enables Cull Facing
            GL.Enable(EnableCap.CullFace);
            // Keeps front faces
            GL.CullFace(CullFaceMode.Front);
            // Uses counter clock-wise standard
            GL.FrontFace(FrontFaceDirection.Cw);

            // BUFFERS

            // Create Frame Buffer Object
            GL.GenFramebuffers(1, out this.normalFBO);
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, this.normalFBO);

            // Create Framebuffer Texture
            GL.GenTextures(1, out this.framebufferTexture);
            GL.BindTexture(TextureTarget.Texture2DMultisample, this.framebufferTexture);
            GL.TexImage2DMultisample(TextureTargetMultisample.Texture2DMultisample, (int)this.samples, PixelInternalFormat.Rgb32f, WIDTH, HEIGHT, true);
            GL.TexParameter(TextureTarget.Texture2DMultisample, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2DMultisample, TextureParameterName.TextureMagFilter, (int)TextureMinFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2DMultisample, TextureParameterName.TextureWrapS, (int)TextureWrapMode.ClampToEdge);   // Previne "edge bleeding"
            GL.TexParameter(TextureTarget.Texture2DMultisample, TextureParameterName.TextureWrapT, (int)TextureWrapMode.ClampToEdge);
            GL.FramebufferTexture2D(FramebufferTarget.Framebuffer, FramebufferAttachment.ColorAttachment0, TextureTarget.Texture2DMultisample, this.framebufferTexture, 0);

            // Create Render Buffer Object
            GL.GenRenderbuffers(1, out this.RBO);
            GL.BindRenderbuffer(RenderbufferTarget.Renderbuffer, this.RBO);
            GL.RenderbufferStorageMultisample(RenderbufferTarget.Renderbuffer, this.samples, RenderbufferStorage.Depth24Stencil8, WIDTH, HEIGHT);
            GL.FramebufferRenderbuffer(FramebufferTarget.Framebuffer, FramebufferAttachment.DepthStencilAttachment, RenderbufferTarget.Renderbuffer, this.RBO);

            // Error checking framebuffer
            var fboStatus = GL.CheckFramebufferStatus(FramebufferTarget.Framebuffer);

            if (fboStatus != FramebufferErrorCode.FramebufferComplete)
            {
                Console.WriteLine("Framebuffer " + this.framebufferTexture + " error:" + fboStatus);
            }

            // Create THE SECOND Frame Buffer Object
            GL.GenFramebuffers(1, out this.postProcessingFBO);

            GL.BindFramebuffer(FramebufferTarget.Framebuffer, this.postProcessingFBO);

            // Create Framebuffer Texture
            //GL.ActiveTexture(TextureUnit.Texture0);
            GL.GenTextures(1, out this.postProcessingTexture);
            GL.BindTexture(TextureTarget.Texture2D, this.postProcessingTexture);
            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgb16f, this.FBOWIDTH, this.FBOHEIGHT, 0, PixelFormat.Rgb, PixelType.UnsignedByte, (IntPtr)null);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMinFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.ClampToEdge);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.ClampToEdge);
            GL.FramebufferTexture2D(FramebufferTarget.Framebuffer, FramebufferAttachment.ColorAttachment0, TextureTarget.Texture2D, this.postProcessingTexture, 0);

            // Error checking framebuffer
            fboStatus = GL.CheckFramebufferStatus(FramebufferTarget.Framebuffer);

            if (fboStatus != FramebufferErrorCode.FramebufferComplete)
            {
                Console.WriteLine("Framebuffer " + this.postProcessingTexture + " error:" + fboStatus);
            }

            // SHADOW FBO & TEXTURE

            // Framebuffer for Shadow Map
            GL.GenFramebuffers(1, out this.shadowMapFBO);

            // Texture for Shadow Map FBO

            GL.GenTextures(1, out this.shadowMapTexture);

            GL.BindTexture(TextureTarget.Texture2D, this.shadowMapTexture);
            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.DepthComponent, SHADOWSIZE, SHADOWSIZE, 0, PixelFormat.DepthComponent, PixelType.Float, (IntPtr)null);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Nearest);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMinFilter.Nearest);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.ClampToBorder);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.ClampToBorder);
            // Prevents darkness outside the frustrum
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureBorderColor, this.clampColor);

            GL.BindFramebuffer(FramebufferTarget.Framebuffer, this.shadowMapFBO);
            GL.FramebufferTexture2D(FramebufferTarget.Framebuffer, FramebufferAttachment.DepthAttachment, TextureTarget.Texture2D, this.shadowMapTexture, 0);
            // Needed since we don't touch the color buffer
            GL.DrawBuffer(DrawBufferMode.None);
            GL.ReadBuffer(ReadBufferMode.None);
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, 0);

            // END OF SHADOW FBO & TEXTURE

            // Matrices needed for the light's perspective
            this.orthgonalProjection = Matrix4.CreateOrthographic(70, 70, 0.1f, 75.0f);
            this.lightView = Matrix4.LookAt(20.0f * this.lightPos, new Vector3(0.0f, 0.0f, 0.0f), new Vector3(0.0f, 1.0f, 0.0f));
            this.lightProjection = this.orthgonalProjection * this.lightView;

            this.shadowMapProgram.Use();
            GL.UniformMatrix4(GL.GetUniformLocation(this.shadowMapProgram.ID, "lightProjection"), false, ref this.lightProjection);

            // END OF FBO EXAMPLE DATA INIT

            // We make the mouse cursor invisible and captured so we can have proper FPS-camera movement.
            this.CursorState = CursorState.Grabbed;
        }

        /// <inheritdoc/>
        protected override void OnRenderFrame(FrameEventArgs args)
        {
            base.OnRenderFrame(args);

            // START OF FBO EXAMPLE DRAW

            // SHADOW RENDERING

            GL.Enable(EnableCap.DepthTest);

            // Preparations for the Shadow Map
            GL.Viewport(0, 0, SHADOWSIZE, SHADOWSIZE);
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, this.shadowMapFBO);
            GL.Clear(ClearBufferMask.DepthBufferBit);
            // Draw
            this.shadowMapProgram.Use();
            this.mesh.Draw();

            // END OF SHADOW RENDERING

            // Preparations for the Frame Buffer
            GL.Viewport(0, 0, WIDTH, HEIGHT);
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, this.normalFBO);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            // Send the light matrix to the shader
            this.monkeyTexture.Use(TextureUnit.Texture3);
            this.shader.Use();
            GL.UniformMatrix4(GL.GetUniformLocation(this.shader.ID, "lightProjection"), false, ref this.lightProjection);

            // Bind the Shadow Map
            GL.ActiveTexture(TextureUnit.Texture0 + 2);
            GL.BindTexture(TextureTarget.Texture2D, this.shadowMapTexture);
            GL.Uniform1(GL.GetUniformLocation(this.shader.ID, "shadowMap"), 2);

            this.mesh.Draw();

            // Make it so the multisampling FBO is read while the post-processing FBO is drawn
            GL.BindFramebuffer(FramebufferTarget.ReadFramebuffer, this.normalFBO);
            GL.BindFramebuffer(FramebufferTarget.DrawFramebuffer, this.postProcessingFBO);
            // BLIT IT!
            GL.BlitFramebuffer(0, 0, this.WIDTH, this.HEIGHT, 0, 0, this.FBOWIDTH, this.FBOHEIGHT, ClearBufferMask.ColorBufferBit, BlitFramebufferFilter.Linear);

#if true

            // Bind default FBO
            GL.Viewport(0, 0, WIDTH, HEIGHT);
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, 0);
            // Draw the framebuffer rectangle
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            GL.ActiveTexture(TextureUnit.Texture0);
            GL.BindTexture(TextureTarget.Texture2D, this.postProcessingTexture);
            this.framebufferProgram.Use();

            Draw.ScreenRectangle();

#endif

            this.SwapBuffers();
        }

        /// <inheritdoc/>
        protected override void OnUpdateFrame(FrameEventArgs args)
        {
            base.OnUpdateFrame(args);

            this.fpsCounter.Update(args);

            // Rotate the model matrix
            this.transform.SetRotation(Quaternion.FromAxisAngle(Vector3.UnitY, this.tick));

            // Identity matrix (per object)
            this.shader.SetMatrix4("model", this.transform.GetModelMatrix());

            // Camera matrices
            this.shader.SetMatrix4("view", this.camera.GetViewMatrix());
            this.shader.SetMatrix4("projection", this.camera.GetProjectionMatrix());

            this.tick += 0.01f;

            this.cameraController.Update(args, this.KeyboardState, this.MouseState);
        }

        /// <inheritdoc/>
        protected override void OnMouseWheel(MouseWheelEventArgs e)
        {
            base.OnMouseWheel(e);

            this.cameraController.MouseUpdate(e);
        }

        /// <inheritdoc/>
        protected override void OnUnload()
        {
            base.OnUnload();

            this.mesh.Dispose();
            this.shader.Dispose();
            this.monkeyTexture.Dispose();
        }
    }
}
