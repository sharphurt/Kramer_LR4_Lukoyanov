using System;

namespace MatrixMath_LR4_Lukoyanov
{
    public class KramerSolver : ILinearSystemSolver
    {
        public (LinearSystemSolveResult result, Vector answer) Solve(Matrix coeff, Vector vector)
        {
            var matrix = new Matrix(coeff.Elements);
            var det = matrix.Determinant;
            if (Math.Abs(det) < 1e-9)
                return (LinearSystemSolveResult.InfinityOrAbsence, new Vector(0));

            var vectorLength = vector.Length;

            var dets = new Vector(vectorLength);
            var el = new Vector(vectorLength);
            for (var j = 0; j < vectorLength; j++)
            {
                for (var i = 0; i < vectorLength; i++)
                {
                    el[i] = matrix[i, j];
                    matrix[i, j] = vector[i];
                }

                dets[j] = matrix.Determinant;
                for (var i = 0; i < vectorLength; i++)
                    matrix[i, j] = el[i];
            }

            var results = new Vector(vectorLength);
            for (var i = 0; i < vectorLength; i++)
                results[i] = dets[i] / det;

            return (LinearSystemSolveResult.Single, results);
        }
    }
}