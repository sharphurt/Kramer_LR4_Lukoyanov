using System;

namespace MatrixMath_LR4_Lukoyanov
{
    public class InverseMatrixSolver : ILinearSystemSolver
    {
        public (LinearSystemSolveResult result, Vector answer) Solve(Matrix coeff, Vector vector)
        {
            var matrix = new Matrix(coeff.Elements);
            var det = matrix.Determinant;
            if (Math.Abs(det) < 1e-9)
                return (LinearSystemSolveResult.InfinityOrAbsence, new Vector(0));

            var result = matrix.Inverse().Multiply(vector).ToVector();
            return (LinearSystemSolveResult.Single, result);
        }
    }
}