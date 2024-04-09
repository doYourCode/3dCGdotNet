// <copyright file="Constants.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Framework.Utils
{
#pragma warning disable SA1310 // Field names should not contain underscore

    /// <summary>
    /// Constantes úteis para realização de alguns cálculos ou para melhorar a legibilidade / semântica do código.
    /// </summary>
    public static class CONSTANTS
    {
        /// <summary>
        /// The mathematical constant, Pi.
        /// </summary>
        public const double PI = 3.14159265358979323846;

        /// <summary>
        /// The mathematical constant, Pi.
        /// </summary>
        public const double GOLDEN_RATIO = 1.61803398874989484820;

        /// <summary>
        /// The mathematical constant, Pi.
        /// </summary>
        public const double NATURAL_LOG_BASE = 2.71828182845904523536;

        /// <summary>
        /// The mathematical constant, Pi.
        /// </summary>
        public const double EULER_CONST = 0.57721566490153286060;

        /// <summary>
        /// The value of Zero.
        /// </summary>
        public const uint ZERO = 0;

        /// <summary>
        /// The value of One.
        /// </summary>
        public const uint ONE = 1;

        /// <summary>
        /// Same as zero, but useful for semantic reasons.
        /// </summary>
        public const uint NONE = 0;

        /// <summary>
        /// TODO.
        /// </summary>
        public const double MAX_FPS = 120;
    }

#pragma warning restore SA1310 // Field names should not contain underscore
}