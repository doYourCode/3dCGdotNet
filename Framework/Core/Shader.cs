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
    /// TODO.
    /// </summary>
    public class Shader : OpenGLObject
    {
        private static string rootPath = string.Empty;

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
                var location = GL.GetUniformLocation(this.ID, key);
                this.uniformLocations.Add(key, location);
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Shader"/> class.
        /// </summary>
        /// <param name="shaderName"> PARAM TODO. </param>
        public Shader(string shaderName)
            : this(rootPath + shaderName + ".vert", rootPath + shaderName + ".frag")
        {
        }

        /// <summary>
        /// Gets or Sets path.
        /// </summary>
        public static string RootPath
        {
            get { return rootPath; } set { rootPath = value; }
        }

        /// <summary>
        /// TODO.
        /// </summary>
        /// <param name="path"> PARAM TODO. </param>
        public static void SetRootPath(string path)
        {
            Shader.rootPath = path;
        }

        /// <summary>
        /// TODO.
        /// </summary>
        public void Use()
        {
            GL.UseProgram(this.ID);
        }

        /// <summary>
        /// TODO.
        /// </summary>
        /// <param name="attribName"> PARAM TODO. </param>
        /// <returns> RETURN TODO. </returns>
        public int GetAttribLocation(string attribName)
        {
            return GL.GetAttribLocation(this.ID, attribName);
        }

        /// <summary>
        /// TODO.
        /// </summary>
        /// <param name="name"> PARAM TODO. </param>
        /// <param name="data"> PARAM2 TODO. </param>
        public void SetInt(string name, int data)
        {
            GL.UseProgram(this.ID);
            GL.Uniform1(this.uniformLocations[name], data);
        }

        /// <summary>
        /// TODO.
        /// </summary>
        /// <param name="name"> PARAM TODO. </param>
        /// <param name="data"> PARAM2 TODO. </param>
        public void SetFloat(string name, float data)
        {
            GL.UseProgram(this.ID);
            GL.Uniform1(this.uniformLocations[name], data);
        }

        /// <summary>
        /// TODO.
        /// </summary>
        /// <param name="name"> PARAM TODO. </param>
        /// <param name="data"> PARAM2 TODO. </param>
        public void SetMatrix4(string name, Matrix4 data)
        {
            GL.UseProgram(this.ID);
            GL.UniformMatrix4(this.uniformLocations[name], true, ref data);
        }

        /// <summary>
        /// TODO.
        /// </summary>
        /// <param name="name"> PARAM TODO. </param>
        /// <param name="data"> PARAM2 TODO. </param>
        public void SetVector3(string name, Vector3 data)
        {
            GL.UseProgram(this.ID);
            GL.Uniform3(this.uniformLocations[name], data);
        }

        /// <summary>
        /// TODO.
        /// </summary>
        /// <param name="name"> PARAM TODO. </param>
        /// <param name="data"> PARAM2 TODO. </param>
        public void SetVector4(string name, Vector4 data)
        {
            GL.UseProgram(this.ID);
            GL.Uniform4(this.uniformLocations[name], data);
        }

        /// <inheritdoc/>
        protected override void Dispose(bool isManualDispose)
        {
            GL.UseProgram(CONSTANTS.NONE);
            GL.DeleteProgram(this.ID);
        }

        /// <summary>
        /// TODO.
        /// </summary>
        /// <param name="shader"> PARAM TODO. </param>
        /// <exception cref="Exception"> Ex. TODO. </exception>
        private static void CompileShader(int shader)
        {
            GL.CompileShader(shader);

            GL.GetShader(shader, ShaderParameter.CompileStatus, out var code);
            if (code != (int)All.True)
            {
                var infoLog = GL.GetShaderInfoLog(shader);
                throw new Exception($"Error occurred whilst compiling Shader({shader}).\n\n{infoLog}");
            }
        }

        /// <summary>
        /// TODO.
        /// </summary>
        /// <param name="program"> PARAM TODO. </param>
        /// <exception cref="Exception"> E. TODO. </exception>
        private static void LinkProgram(int program)
        {
            GL.LinkProgram(program);

            GL.GetProgram(program, GetProgramParameterName.LinkStatus, out var code);
            if (code != (int)All.True)
            {
                var infoLog = GL.GetProgramInfoLog(program);
                throw new Exception($"Error occurred whilst linking Program({program}).\n\n{infoLog}");
            }
        }
    }
}