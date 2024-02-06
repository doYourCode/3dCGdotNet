using OpenTK.Windowing.Desktop;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using _3dCG.Examples.Basics;
using _3dCG.Examples.ShadingModel;
using System;
using _3dCG.Examples.ImGuiImpl;

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
            var _window = new Chapter_12_HelloLight(
                GameWindowSettings.Default,
                new NativeWindowSettings()
                {
                    Size = new Vector2i(800, 800),              // Instanciando uma janela
                    WindowBorder = WindowBorder.Fixed,
                    WindowState = WindowState.Normal,
                    APIVersion = new Version(3, 3),
                    Vsync = VSyncMode.On,
                });

            //var _window = new ImGui_Sample();

            _window.UpdateFrequency = 60;
            _window.RenderFrequency = 60;

            // HACK: Precisa pensar numa forma de configurar esse caminho automaticamente para os exemplos, de repente, utilzar o json + namespace de alguma forma para organizar isso.
            Shader.SetRootPath("Resources/Shader/Basics/");     // Pasta raiz para procurar arquivos de Shader -> O que é shader? Veja no cap 4.

            // Executa a aplicação
            _window.Run(); // Executa a janela
        }
    }
}
