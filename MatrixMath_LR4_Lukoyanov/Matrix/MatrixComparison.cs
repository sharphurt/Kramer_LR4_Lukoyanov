using System;
using System.Linq;

namespace MatrixMath_LR4_Lukoyanov
{
    public partial class Matrix
    {
        public override bool Equals(object obj) => obj is Matrix matrix && ElementwiseComparison(matrix);

        public override int GetHashCode() =>
            Elements.Cast<double>().Aggregate(0, (current, element) => current ^ element.GetHashCode());

        private bool ElementwiseComparison(Matrix matrix)
        {
            if (RowCount != matrix.RowCount || ColumnCount != matrix.ColumnCount)
                return false;
            for (var i = 0; i < RowCount; i++)
            for (var j = 0; j < ColumnCount; j++)
                if (Math.Abs(this[i, j] - matrix[i, j]) > 1e-6)
                    return false;

            return true;
        }
    }
}