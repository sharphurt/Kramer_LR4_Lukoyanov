using System;
using System.Linq;

namespace MatrixMath_LR4_Lukoyanov
{
    public static class Utils
    {
        public static Matrix ArrayToSquareMatrix(double[] elements)
        {
            var nb = (int)Math.Sqrt(elements.Length);

            var mat = new Matrix(nb, nb);

            for (var i = 0; i < nb; ++i)
            for (var j = 0; j < nb; ++j)
                mat[i, j] = elements[i * nb + j];

            return mat;
        }
    }
}