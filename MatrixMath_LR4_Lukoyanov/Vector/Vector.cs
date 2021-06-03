namespace MatrixMath_LR4_Lukoyanov
{
    public partial class Vector
    {
        public Vector(double[] elements) => Elements = elements;

        public Vector(int length) => Elements = new double[length];

        public double[] Elements { get; }

        public int Length => Elements.Length;
        
        public double this[int index]
        {
            get => Elements[index];
            set => Elements[index] = value;
        }

        public Matrix ToVerticalMatrix() => new Matrix(Length, 1, Elements);
        
    }
}