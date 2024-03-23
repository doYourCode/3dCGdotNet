using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;

namespace Framework.Core
{
    /// <summary>
    /// 
    /// </summary>
    public class Shader
    {
        /* -------------------------------------------- Variáveis de classe -------------------------------------------- */
#if DEBUG
        /// <summary>
        /// Representa o quantitativo de shaders carregados na VRAM.
        /// </summary>
        public static UInt32 Count { get { return count; } private set { } }

        private static UInt32 count = 0;
#endif

        /// <summary>
        /// Caminho para a pasta raiz para carregar arquivos de Shader.
        /// </summary>
        public static string RootPath { get { return rootPath; } private set { } }

        private static string rootPath = "";


        /* ---------------------------------------------- Variáveis membro ---------------------------------------------- */

        /// <summary>
        /// Id que reflete o endereço do programa de shader na VRAM
        /// </summary>
        public UInt32 ID { get { return id; } private set { } }


        private UInt32 id;

        private readonly Dictionary<string, int> uniformLocations;


        /* ---------------------------------------------- Interface pública ---------------------------------------------- */

        /// <summary>
        /// 
        /// </summary>
        /// <param name="VertPath"></param>
        /// <param name="FragPath"></param>
        public Shader(string VertPath, string FragPath)
        {
            var shaderSource = File.ReadAllText(VertPath);
            var vertexShader = GL.CreateShader(ShaderType.VertexShader);
            GL.ShaderSource(vertexShader, shaderSource);
            CompileShader(vertexShader);

            shaderSource = File.ReadAllText(FragPath);
            var fragmentShader = GL.CreateShader(ShaderType.FragmentShader);
            GL.ShaderSource(fragmentShader, shaderSource);
            CompileShader(fragmentShader);

            id = (UInt32)GL.CreateProgram();

            GL.AttachShader((Int32)id, vertexShader);
            GL.AttachShader((Int32)id, fragmentShader);

            LinkProgram((Int32)id);

            GL.DetachShader((Int32)id, vertexShader);
            GL.DetachShader((Int32)id, fragmentShader);
            GL.DeleteShader(fragmentShader);
            GL.DeleteShader(vertexShader);

            GL.GetProgram(id, GetProgramParameterName.ActiveUniforms, out var numberOfUniforms);

            uniformLocations = new Dictionary<string, int>();

            for (var i = 0; i < numberOfUniforms; i++)
            {
                var key = GL.GetActiveUniform((Int32)id, i, out _, out _);
                var location = GL.GetUniformLocation(id, key);
                uniformLocations.Add(key, location);
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
            rootPath = Path;
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
                throw new Exception($"Error occurred whilst linking Program({Program})");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void Use()
        {
            GL.UseProgram(id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="AttribName"></param>
        /// <returns></returns>
        public int GetAttribLocation(string AttribName)
        {
            return GL.GetAttribLocation(id, AttribName);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Data"></param>
        public void SetInt(string Name, int Data)
        {
            GL.UseProgram(id);
            GL.Uniform1(uniformLocations[Name], Data);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Data"></param>
        public void SetFloat(string Name, float Data)
        {
            GL.UseProgram(id);
            GL.Uniform1(uniformLocations[Name], Data);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Data"></param>
        public void SetMatrix4(string Name, Matrix4 Data)
        {
            GL.UseProgram(id);
            GL.UniformMatrix4(uniformLocations[Name], true, ref Data);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Data"></param>
        public void SetVector3(string Name, Vector3 Data)
        {
            GL.UseProgram(id);
            GL.Uniform3(uniformLocations[Name], Data);
        }
    }
}
