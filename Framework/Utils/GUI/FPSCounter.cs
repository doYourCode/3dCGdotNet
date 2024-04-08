// <copyright file="FPSCounter.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Framework.Utils.GUI
{
    using OpenTK.Windowing.Common;
    using OpenTK.Windowing.Desktop;

    /// <summary>
    /// TODO.
    /// </summary>
    public class FPSCounter
    {
        private static uint bufferSize = 512;

        private static double bufferSizeInv = 1.0 / (double)bufferSize;

        private GameWindow window;

        private double buffer = 0.0;

        private uint bufferIndex = 0;

        private string title;

        /// <summary>
        /// Initializes a new instance of the <see cref="FPSCounter"/> class.
        /// TODO.
        /// </summary>
        /// <param name="window"> PARAM TODO. </param>
        public FPSCounter(GameWindow window)
        {
            this.window = window;
            this.title = window.Title;
        }

        /// <summary>
        /// TODO.
        /// </summary>
        /// <param name="args"> PARAM TODO. </param>
        public void Update(FrameEventArgs args)
        {
            this.bufferIndex++;
            double tmp = this.GetMilliseconds();

            if (this.bufferIndex < FPSCounter.bufferSize)
            {
                this.buffer += args.Time;
            }
            else
            {
                this.window.Title = this.title + " (Ms: " + tmp.ToString("N3")
                    + " | Fps: " + (1000 / tmp).ToString("N1") + ")";

                this.Reset();
            }
        }

        /// <summary>
        /// TODO.
        /// </summary>
        public void Reset()
        {
            this.bufferIndex = 0;
            this.buffer = 0.0f;
        }

        /// <summary>
        /// TODO.
        /// </summary>
        /// <returns> RETURN TODO. </returns>
        public double GetMilliseconds()
        {
            return (this.buffer * 1000) * bufferSizeInv;
        }
    }
}
