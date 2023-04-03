using OpenTK.Windowing.Desktop;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using _3dCG.Examples.Basics;

namespace _3dCG
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Cria e configura uma janela da aplicação
            // TODO: Permitir a seleção dos diferentes exemplos / casos de uso a partir de uma janela central,
            // através de uma lista ou pelos seus nomes.
            // HACK: A configuração da resolução de tela poderia ser carregada diferentemente para namespaces diferentes. Podemos usar json + namespace para rever isso.
            var _window = new Chapter_10_HelloTransformation(GameWindowSettings.Default,
                new NativeWindowSettings()
                {
                    Size = new Vector2i(800, 800),
                    WindowBorder = WindowBorder.Fixed,
                    WindowState = WindowState.Normal,
                });
            // HACK: Precisa pensar numa forma de configurar esse caminho automaticamente para os exemplos, de repente, utilzar o json + namespace de alguma forma para organizar isso.
            Shader.SetRootPath("Resources/Shader/Basics/");

            // Executa a aplicação
            _window.Run();
        }
    }
}
