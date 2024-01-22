using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;

namespace _3dCG.Examples.Basics
{
    /// <summary>
    /// Demonstra como limpar e dispor um Frame Buffer, utilizando a funcionalidade disponível na biblioteca OpenTK.
    /// </summary>
    internal class Chapter_02_HelloSwapBuffers : GameWindow
    {
        // Enumeração para identificar o estado atual da cor.
        private enum ColorState { Red, Green, Blue }
        private ColorState currentState = ColorState.Red; // Inicia no estado vermelho.
        private bool isIncreasing = true; // Flag para controlar se a cor está aumentando ou diminuindo.
        private Color4 bgColor = new Color4(0.0f, 0.0f, 0.0f, 1.0f); // Cor de fundo inicial.

        public Chapter_02_HelloSwapBuffers(
            GameWindowSettings gameWindowSettings,
            NativeWindowSettings nativeWindowSettings) :
            base(gameWindowSettings, nativeWindowSettings)
        {
            Title = "Hello Swap Buffers!";
        }

        protected override void OnLoad()
        {
            base.OnLoad();
            GL.ClearColor(bgColor); // Define a cor inicial de limpeza do frame buffer.
        }

        protected override void OnUpdateFrame(FrameEventArgs args)
        {
            base.OnUpdateFrame(args);

            float colorChangeSpeed = 0.001f; // Velocidade de mudança da cor.

            // Alterna entre os estados de cor e ajusta a cor de fundo.
            switch (currentState)
            {
                case ColorState.Red:
                    if (isIncreasing)
                    {
                        bgColor.R += colorChangeSpeed; // Aumenta o canal vermelho.
                        if (bgColor.R >= 1.0f)
                        {
                            isIncreasing = false; // Muda a direção quando atinge o máximo.
                        }
                    }
                    else
                    {
                        bgColor.R -= colorChangeSpeed; // Diminui o canal vermelho.
                        if (bgColor.R <= 0.0f)
                        {
                            isIncreasing = true; // Muda a direção quando atinge o mínimo.
                            currentState = ColorState.Green; // Muda para o próximo estado.
                        }
                    }
                    break;

                case ColorState.Green:
                    // Similar ao caso Red, mas para o canal verde.
                    if (isIncreasing)
                    {
                        bgColor.G += colorChangeSpeed;
                        if (bgColor.G >= 1.0f)
                        {
                            isIncreasing = false;
                        }
                    }
                    else
                    {
                        bgColor.G -= colorChangeSpeed;
                        if (bgColor.G <= 0.0f)
                        {
                            isIncreasing = true;
                            currentState = ColorState.Blue;
                        }
                    }
                    break;

                case ColorState.Blue:
                    // Similar aos casos anteriores, mas para o canal azul.
                    if (isIncreasing)
                    {
                        bgColor.B += colorChangeSpeed;
                        if (bgColor.B >= 1.0f)
                        {
                            isIncreasing = false;
                        }
                    }
                    else
                    {
                        bgColor.B -= colorChangeSpeed;
                        if (bgColor.B <= 0.0f)
                        {
                            isIncreasing = true;
                            currentState = ColorState.Red;
                        }
                    }
                    break;
            }

            GL.ClearColor(bgColor); // Atualiza a cor de fundo do frame buffer.
        }
        protected override void OnRenderFrame(FrameEventArgs args)
        {
            base.OnRenderFrame(args);
            GL.Clear(ClearBufferMask.ColorBufferBit); // Limpa o frame buffer com a cor atual.
            SwapBuffers(); // Troca os buffers para exibir o frame renderizado.
        }
    }
}
