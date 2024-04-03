// <copyright file="Print.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Framework.Utils
{
    /// <summary>
    /// TODO.
    /// </summary>
    public static class Print
    {
        /// <summary>
        /// TODO.
        /// </summary>
        /// <param name="matrix"> PARAM TODO. </param>
        public static void Mat4(Assimp.Matrix4x4 matrix)
        {
            Console.WriteLine(matrix);
        }

        /// <summary>
        /// TODO.
        /// </summary>
        /// <param name="matrix"> PARAM TODO. </param>
        public static void Mat4(OpenTK.Mathematics.Matrix4 matrix)
        {
            Console.WriteLine(matrix);
        }

        /// <summary>
        /// TODO.
        /// </summary>
        /// <param name="transform"> PARAM TODO. </param>
        public static void Transform(Core.Transform transform)
        {
            Console.WriteLine("\tA\tB\tC\tD");

            Console.Write("1");
            Console.WriteLine();

            Console.Write("2");

            Console.Write("3");

            Console.Write("4");
        }
    }
}
