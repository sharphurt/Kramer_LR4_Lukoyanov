using System;


namespace MatrixMath_LR4_Lukoyanov
{
    public partial class Matrix
    {
        private Matrix RowCoFactor(int row)
        {
            var size = RowCount;
            var subMatrix = new Matrix(size - 1, size - 1);
            for (var i = 0; i < size; i++)
            {
                if (i == row)
                    continue;

                var subI = i < row ? i : i - 1;
                for (var j = 1; j < size; j++)
                {
                    var subJ = j - 1;
                    subMatrix[subI, subJ] = this[i, j];
                }
            }

            return subMatrix;
        }

        public double Determinant
        {
            get {
                var size = RowCount;
                if (size == 1) return this[0, 0];
                if (size == 2) return this[0, 0] * this[1, 1] - this[0, 1] * this[1, 0];

                double determinant = 0;
                var sign = 1;
                for (var k = 0; k < size; ++k)
                {
                    var mat0 = RowCoFactor(k);
                    determinant += sign * this[k, 0] * mat0.Determinant;
                    sign *= -1;
                }

                return determinant;
            }
        }

        private static Matrix Multiply(Matrix matrix, Matrix other)
        {
            if (matrix.ColumnCount != other.RowCount)
                throw new ArgumentException("Matrices should have same size");

            var result = new Matrix(matrix.RowCount, other.ColumnCount);

            for (var row = 0; row < result.RowCount; row++)
            for (var col = 0; col < result.ColumnCount; col++)
            {
                var tmp = .0;
                for (var i = 0; i < matrix.ColumnCount; i++)
                    tmp += matrix[row, i] * other[i, col];

                result[row, col] = tmp;
            }

            return result;
        }

        public Matrix Multiply(Vector vector) =>
            Multiply(this, new Matrix(vector.Length, 1, vector.Elements));


        public static Matrix operator *(Matrix matrix, Matrix other) => Multiply(matrix, other);

        public Matrix Inverse()
        {
            if (RowCount != ColumnCount)
                throw new ArgumentException(
                    "Обратная матрица существует только для квадратных, невырожденных, матриц.");

            var matrix = new Matrix(RowCount, ColumnCount);
            var determinant = Determinant;

            if (Math.Abs(determinant) < 1e-9)
                throw new ArgumentException(
                    "Обратная матрица существует только для квадратных, невырожденных, матриц.");

            var sign = 1;

            for (var i = 0; i < RowCount; i++)
            for (var t = 0; t < ColumnCount; t++)
            {
                var tmp = Exclude(i, t);
                matrix[t, i] = sign / determinant * tmp.Determinant;
                sign *= -1;
            }

            return matrix;
        }

        private Matrix Exclude(int row, int column)
        {
            if (row > RowCount || column > ColumnCount)
                throw new IndexOutOfRangeException("Строка или столбец не принадлежат матрице.");

            var ret = new Matrix(RowCount - 1, ColumnCount - 1);
            var offsetX = 0;
            for (var i = 0; i < RowCount; i++)
            {
                var offsetY = 0;

                if (i == row)
                {
                    offsetX++;
                    continue;
                }

                for (var t = 0; t < ColumnCount; t++)
                {
                    if (t == column)
                    {
                        offsetY++;
                        continue;
                    }

                    ret[i - offsetX, t - offsetY] = this[i, t];
                }
            }

            return ret;
        }
        
        public Matrix ElementwiseOperation(Func<double, double> func)
        {
            var result = new Matrix(RowCount, ColumnCount);
            for (var i = 0; i < RowCount; i++)
            for (var j = 0; j < ColumnCount; j++)
                result[i, j] = func(this[i, j]);

            return result;
        }
    }
}