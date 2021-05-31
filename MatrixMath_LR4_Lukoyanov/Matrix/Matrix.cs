using System;
using System.Linq;

namespace MatrixMath_LR4_Lukoyanov
{
    public partial class Matrix
    {
        public readonly double[,] Elements;

        public Matrix(double[,] elements) => Elements = elements;

        public Matrix(int rows, int cols) => Elements = new double[rows, cols];

        public Matrix(int rows, int cols, double[] elements)
        {
            Elements = new double[rows, cols];
            for (var i = 0; i < rows; i++)
            for (var j = 0; j < cols; j++)
                Elements[i, j] = elements[i * rows + cols];
        }

        public int RowCount => Elements.GetLength(0);

        public int ColumnCount => Elements.GetLength(1);

        public int Count => Elements.Length;

        public double this[int row, int col]
        {
            get => Elements[row, col];
            set => Elements[row, col] = value;
        }


    }
}