using System;

namespace MatrixMath_LR4_Lukoyanov
{
    public partial class Vector
    {
        public Vector ElementwiseOperation(Func<double, double> func)
        {
            var result = new Vector(Length);
            for (var i = 0; i < Length; i++)
                result[i] = func(this[i]);

            return result;
        }
    }
}