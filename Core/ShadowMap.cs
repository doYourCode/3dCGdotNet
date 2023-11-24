using System;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;

// TODO: Corrigir esse código, pois nãp está funcionando como planejado. Precisa definir melhor essa relação com a classe Light e também como o objeto se comporta ao ser renderizado.

namespace _3dCG.Core
{
    public class ShadowMap
    {
        public int Width { get; private set; }
        public int Height { get; private set; }
        public Matrix4 LightViewProjection { get; private set; }
        public int DepthTexture { get; private set; }
        public int Framebuffer { get; private set; }

        public ShadowMap(int width, int height)
        {
            Width = width;
            Height = height;

            DepthTexture = GL.GenTexture();
            GL.BindTexture(TextureTarget.Texture2D, DepthTexture);
            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.DepthComponent, Width, Height, 0, PixelFormat.DepthComponent, PixelType.Float, IntPtr.Zero);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.ClampToEdge);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.ClampToEdge);

            Framebuffer = GL.GenFramebuffer();
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, Framebuffer);
            GL.FramebufferTexture(FramebufferTarget.Framebuffer, FramebufferAttachment.DepthAttachment, DepthTexture, 0);
            GL.DrawBuffer(DrawBufferMode.None);

            if (GL.CheckFramebufferStatus(FramebufferTarget.Framebuffer) != FramebufferErrorCode.FramebufferComplete)
                throw new Exception("Erro ao criar framebuffer");

            GL.BindFramebuffer(FramebufferTarget.Framebuffer, 0);
        }

        public void Begin(Matrix4 lightViewProjection)
        {
            LightViewProjection = lightViewProjection;

            GL.BindFramebuffer(FramebufferTarget.Framebuffer, Framebuffer);
            GL.Viewport(0, 0, Width, Height);
            GL.Clear(ClearBufferMask.DepthBufferBit);
        }

        public void End()
        {
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, 0);
        }
    }
}

/*
namespace _3dCG.Core
{
    internal class ShadowMap
    {
        int _depthMapFBO;
        int depthMap;
        int _width, _height;
        Shader _simpleDepthShader;
        Light _light;

        public ShadowMap(Light light, ushort width, ushort height)
        {
            _light = light;
            _width = width;
            _height = height;

            _simpleDepthShader = new Shader("Shaders/shadow_mapping_depth.vert", "Shaders/shadow_mapping_depth.frag");

            _depthMapFBO = GL.GenFramebuffer();

            // Create texture for shadow map
            depthMap = GL.GenTexture();
            GL.BindTexture(TextureTarget.Texture2D, depthMap);
            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.DepthComponent, _width, _height, 0, PixelFormat.DepthComponent, PixelType.Float, IntPtr.Zero);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Repeat);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Repeat);

            // Attach texture to framebuffer
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, _depthMapFBO);
            GL.FramebufferTexture2D(FramebufferTarget.Framebuffer, FramebufferAttachment.DepthAttachment, TextureTarget.Texture2D, depthMap, 0);
            GL.DrawBuffer(DrawBufferMode.None);
            GL.ReadBuffer(ReadBufferMode.None);
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, 0);
        }

        public void Draw(Shader shader)
        {
            GL.ClearColor(0.1f, 0.1f, 0.1f, 1.0f);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            // 1. render depth of scene to texture (from light's perspective)
            // --------------------------------------------------------------

            //lightProjection = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(45.0f), (float)SHADOW_WIDTH / (float)SHADOW_HEIGHT, near_plane, far_plane); // note that if you use a perspective projection matrix you'll have to change the light position as the current light position isn't enough to reflect the whole scene

            // render scene from light's point of view
            _simpleDepthShader.Use();
            _simpleDepthShader.SetMatrix4("lightSpaceMatrix", _light.LightSpaceMatrix);

            GL.Viewport(0, 0, _width, _height);
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, _depthMapFBO);
            GL.Clear(ClearBufferMask.DepthBufferBit);
            //RenderScene(_simpleDepthShader);
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, 0);

            /*
            // reset viewport
            GL.Viewport(0, 0, SCR_WIDTH, SCR_HEIGHT);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            
            // 2. render scene as normal using the generated depth/shadow map
            // --------------------------------------------------------------
            shader.Use();
            Matrix4 projection = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(camera.Zoom), (float)SCR_WIDTH / (float)SCR_HEIGHT, 0.1f, 100.0f);
            Matrix4 view = camera.GetViewMatrix().ToOpenTK();
            shader.SetMatrix4("projection", ref projection);
            shader.SetMatrix4("view", ref view);
            // set light uniforms
            shader.SetVector3("viewPos", camera.Position.ToOpenTK());
            shader.SetVector3("lightPos", lightPos.ToOpenTK());
            shader.SetMatrix4("lightSpaceMatrix", ref lightSpaceMatrix);
            GL.ActiveTexture(TextureUnit.Texture0);
            GL.BindTexture(TextureTarget.Texture2D, woodTexture);
            GL.ActiveTexture(TextureUnit.Texture1);
            GL.BindTexture(TextureTarget.Texture2D, depthMap);
        }

        public void Bind()
        {
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, _depthMapFBO);
        }

        public void Unbind()
        {
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, 0);
        }

        public void Delete()
        {
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, 0);
            GL.DeleteFramebuffer(_depthMapFBO);
        }


    }
}
*/


/*  OUTRA VERSÃO
 
 using OpenTK;
using OpenTK.Graphics.OpenGL;

public class ShadowMap
{
    public int Width { get; private set; }
    public int Height { get; private set; }
    public Matrix4 LightViewProjection { get; private set; }
    public int DepthTexture { get; private set; }
    public int Framebuffer { get; private set; }

    public ShadowMap(int width, int height)
    {
        Width = width;
        Height = height;

        DepthTexture = GL.GenTexture();
        GL.BindTexture(TextureTarget.Texture2D, DepthTexture);
        GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.DepthComponent, Width, Height, 0, PixelFormat.DepthComponent, PixelType.Float, IntPtr.Zero);
        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);
        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.ClampToEdge);
        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.ClampToEdge);

        Framebuffer = GL.GenFramebuffer();
        GL.BindFramebuffer(FramebufferTarget.Framebuffer, Framebuffer);
        GL.FramebufferTexture(FramebufferTarget.Framebuffer, FramebufferAttachment.DepthAttachment, DepthTexture, 0);
        GL.DrawBuffer(DrawBufferMode.None);

        if (GL.CheckFramebufferStatus(FramebufferTarget.Framebuffer) != FramebufferErrorCode.FramebufferComplete)
            throw new Exception("Erro ao criar framebuffer");

        GL.BindFramebuffer(FramebufferTarget.Framebuffer, 0);
    }

    public void Begin(Matrix4 lightViewProjection)
    {
        LightViewProjection = lightViewProjection;

        GL.BindFramebuffer(FramebufferTarget.Framebuffer, Framebuffer);
        GL.Viewport(0, 0, Width, Height);
        GL.Clear(ClearBufferMask.DepthBufferBit);
    }

    public void End()
    {
        GL.BindFramebuffer(FramebufferTarget.Framebuffer, 0);
    }
}
 
 
 */