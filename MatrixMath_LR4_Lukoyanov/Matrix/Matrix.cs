using System;
using System.Linq;

namespace MatrixMath_LR4_Lukoyanov
{
    public partial class Matrix
    {
        public readonly double[,] Elements;

        public Matrix(double[,] elements)
        {
            if (elements is null || elements.GetLength(0) == 0 || elements.GetLength(1) == 0)
                throw new ArgumentException("Matrix cannot be null or empty");
            Elements = elements;
        }

        public Matrix(int rows, int cols) => Elements = new double[rows, cols];

        public Matrix(int rows, int cols, double[] elements)
        {
            Elements = new double[rows, cols];
            for (var i = 0; i < rows; i++)
            for (var j = 0; j < cols; j++)
                Elements[i, j] = elements[j * cols + i];
        }

        public int RowCount => Elements.GetLength(0);

        public int ColumnCount => Elements.GetLength(1);

        public int Count => Elements.Length;

        public double this[int row, int col]
        {
            get => Elements[row, col];
            set => Elements[row, col] = value;
        }

        public Vector ToVector()
        {
            if (RowCount != 1 && ColumnCount != 1)
                throw new InvalidOperationException("Matrix is 2-dimensional");
            var result = new Vector(RowCount * ColumnCount);
            var index = 0;
            for (var i = 0; i < RowCount; i++)
            for (var j = 0; j < ColumnCount; j++)
                result[index++] = Elements[i, j];
            return result;
        }
    }
}