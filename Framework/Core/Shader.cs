// <copyright file="Shader.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Framework.Core
{
    using Framework.Core.Resource;
    using Framework.Utils;
    using OpenTK.Graphics.OpenGL;
    using OpenTK.Mathematics;

    /// <summary>
    /// 
    /// </summary>
    public class Shader : OpenGLObject
    {
        /// <summary>
        /// Caminho para a pasta raiz para carregar arquivos de Shader.
        /// </summary>
        public static string RootPath { get { return rootPath; } set { rootPath = value; } }

        private static string rootPath = "";

        private readonly Dictionary<string, int> uniformLocations;

        /// <summary>
        /// Initializes a new instance of the <see cref="Shader"/> class.
        /// </summary>
        /// <param name="vertPath">Vertex shader file path.</param>
        /// <param name="fragPath">Fragment shader file path.</param>
        public Shader(string vertPath, string fragPath)
            : base((uint)GL.CreateProgram())
        {
            var shaderSource = File.ReadAllText(vertPath);
            var vertexShader = GL.CreateShader(ShaderType.VertexShader);
            GL.ShaderSource(vertexShader, shaderSource);
            CompileShader(vertexShader);

            shaderSource = File.ReadAllText(fragPath);
            var fragmentShader = GL.CreateShader(ShaderType.FragmentShader);
            GL.ShaderSource(fragmentShader, shaderSource);
            CompileShader(fragmentShader);


            GL.AttachShader((int)this.ID, vertexShader);
            GL.AttachShader((int)this.ID, fragmentShader);

            LinkProgram((int)this.ID);

            GL.DetachShader((int)this.ID, vertexShader);
            GL.DetachShader((int)this.ID, fragmentShader);
            GL.DeleteShader(fragmentShader);
            GL.DeleteShader(vertexShader);

            GL.GetProgram(this.ID, GetProgramParameterName.ActiveUniforms, out var numberOfUniforms);

            this.uniformLocations = new Dictionary<string, int>();

            for (var i = 0; i < numberOfUniforms; i++)
            {
                var key = GL.GetActiveUniform((int)this.ID, i, out _, out _);
                var location = GL.GetUniformLocation(ID, key);
                this.uniformLocations.Add(key, location);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ShaderName"></param>
        public Shader(string ShaderName) : this(rootPath + ShaderName + ".vert", rootPath + ShaderName + ".frag") { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Path"></param>
        public static void SetRootPath(string Path)
        {
            Shader.rootPath = Path;
        }

        /// <summary>
        /// 
        /// </summary>
        public void Use()
        {
            GL.UseProgram(this.ID);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="AttribName"></param>
        /// <returns></returns>
        public int GetAttribLocation(string AttribName)
        {
            return GL.GetAttribLocation(ID, AttribName);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Data"></param>
        public void SetInt(string Name, int Data)
        {
            GL.UseProgram(ID);
            GL.Uniform1(this.uniformLocations[Name], Data);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Data"></param>
        public void SetFloat(string Name, float Data)
        {
            GL.UseProgram(ID);
            GL.Uniform1(this.uniformLocations[Name], Data);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Data"></param>
        public void SetMatrix4(string Name, Matrix4 Data)
        {
            GL.UseProgram(ID);
            GL.UniformMatrix4(this.uniformLocations[Name], true, ref Data);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Data"></param>
        public void SetVector3(string Name, Vector3 Data)
        {
            GL.UseProgram(this.ID);
            GL.Uniform3(this.uniformLocations[Name], Data);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Shader"></param>
        /// <exception cref="Exception"></exception>
        private static void CompileShader(int Shader)
        {
            GL.CompileShader(Shader);

            GL.GetShader(Shader, ShaderParameter.CompileStatus, out var code);
            if (code != (int)All.True)
            {
                var infoLog = GL.GetShaderInfoLog(Shader);
                throw new Exception($"Error occurred whilst compiling Shader({Shader}).\n\n{infoLog}");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Program"></param>
        /// <exception cref="Exception"></exception>
        private static void LinkProgram(int Program)
        {
            GL.LinkProgram(Program);

            GL.GetProgram(Program, GetProgramParameterName.LinkStatus, out var code);
            if (code != (int)All.True)
            {
                var infoLog = GL.GetProgramInfoLog(Program);
                throw new Exception($"Error occurred whilst linking Program({Program}).\n\n{infoLog}");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="isManualDispose"></param>
        /// <exception cref="NotImplementedException"></exception>
        protected override void Dispose(bool isManualDispose)
        {
            GL.UseProgram(CONSTANTS.NONE);
            GL.DeleteProgram(this.ID);
        }
    }
}