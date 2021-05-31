using System;

namespace MatrixMath_LR4_Lukoyanov
{
    public partial class Vector
    {
        public override bool Equals(object obj)
        {
            return obj is Vector vector && ElementwiseComparison(vector);
        }

        private bool ElementwiseComparison(Vector vector)
        {
            for (var i = 0; i < vector.Length; i++)
                if (Math.Abs(vector[i] - Elements[i]) > 1e-6)
                    return false;
            
            return true;
        }
    }
}