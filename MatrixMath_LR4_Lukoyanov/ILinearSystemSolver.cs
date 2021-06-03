namespace MatrixMath_LR4_Lukoyanov
{
    public interface ILinearSystemSolver
    {
        (LinearSystemSolveResult result, Vector answer) Solve(Matrix matrix, Vector vector);
    }
}