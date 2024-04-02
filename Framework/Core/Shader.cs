using Framework.Core.Resource;
using Framework.Utils;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;

namespace Framework.Core
{
    /// <summary>
    /// 
    /// </summary>
    public class Shader : OpenGLObject
    {
        #region (Data Fields)

        /// <summary>
        /// Caminho para a pasta raiz para carregar arquivos de Shader.
        /// </summary>
        public static string RootPath { get { return rootPath; } set { rootPath = value; } }

        private static string rootPath = "";

        private readonly Dictionary<string, int> uniformLocations;

        #endregion

        #region (Constructors)

        /// <summary>
        /// 
        /// </summary>
        /// <param name="VertPath"></param>
        /// <param name="FragPath"></param>
        public Shader(string VertPath, string FragPath) : base((UInt32)GL.CreateProgram())
        {
            var shaderSource = File.ReadAllText(VertPath);
            var vertexShader = GL.CreateShader(ShaderType.VertexShader);
            GL.ShaderSource(vertexShader, shaderSource);
            CompileShader(vertexShader);

            shaderSource = File.ReadAllText(FragPath);
            var fragmentShader = GL.CreateShader(ShaderType.FragmentShader);
            GL.ShaderSource(fragmentShader, shaderSource);
            CompileShader(fragmentShader);


            GL.AttachShader((Int32)ID, vertexShader);
            GL.AttachShader((Int32)ID, fragmentShader);

            LinkProgram((Int32)ID);

            GL.DetachShader((Int32)ID, vertexShader);
            GL.DetachShader((Int32)ID, fragmentShader);
            GL.DeleteShader(fragmentShader);
            GL.DeleteShader(vertexShader);

            GL.GetProgram(ID, GetProgramParameterName.ActiveUniforms, out var numberOfUniforms);

            uniformLocations = new Dictionary<string, int>();

            for (var i = 0; i < numberOfUniforms; i++)
            {
                var key = GL.GetActiveUniform((Int32)ID, i, out _, out _);
                var location = GL.GetUniformLocation(ID, key);
                uniformLocations.Add(key, location);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ShaderName"></param>
        public Shader(string ShaderName) : this(rootPath + ShaderName + ".vert", rootPath + ShaderName + ".frag") { }

        #endregion

        #region (Public Methods)

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Path"></param>
        public static void SetRootPath(string Path)
        {
            rootPath = Path;
        }

        /// <summary>
        /// 
        /// </summary>
        public void Use()
        {
            GL.UseProgram(ID);
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
            GL.Uniform1(uniformLocations[Name], Data);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Data"></param>
        public void SetFloat(string Name, float Data)
        {
            GL.UseProgram(ID);
            GL.Uniform1(uniformLocations[Name], Data);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Data"></param>
        public void SetMatrix4(string Name, Matrix4 Data)
        {
            GL.UseProgram(ID);
            GL.UniformMatrix4(uniformLocations[Name], true, ref Data);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Data"></param>
        public void SetVector3(string Name, Vector3 Data)
        {
            GL.UseProgram(ID);
            GL.Uniform3(uniformLocations[Name], Data);
        }

        #endregion

        #region (Other Methods)

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
                throw new Exception($"Error occurred whilst linking Program({Program})");
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
            GL.DeleteProgram(ID);
        }

        #endregion
    }
}
