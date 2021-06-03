using System;
using System.Linq;
using MatrixMath_LR4_Lukoyanov;
using NUnit.Framework;

namespace MatrixMathTest
{
    [TestFixture]
    public class Tests
    {
        [Test]
        public void CreateMatrixTest()
        {
            Assert.Throws(typeof(ArgumentException), () => new Matrix(new double[,] { }));
            Assert.Throws(typeof(ArgumentException), () => new Matrix(null));
            var elements = new double[,] {{1, 2, 3}, {5, 6, 7}};
            Assert.AreEqual(elements, new Matrix(elements).Elements);
        }

        [TestCase("1 2 3 9 8 7 4 5 6", 0)]
        [TestCase("2 10 8 2", -76)]
        [TestCase("4 6 10 7 1 4 9 8 10 2 10 7 10 7 6 10 3 6 1 10 10 7 1 8 3", 52)]
        [TestCase("6 4 10 1 10 5 1 9 3 2 9 10 6 2 6 2", 1718)]
        [TestCase("10 2 6 1 5 10 3 6 7 2 9 7 4 6 8 9", -648)]
        public void DeterminantTest(string matrix, double det)
        {
            var determinant = Utils.ArrayToSquareMatrix(matrix.Split(' ').Select(double.Parse).ToArray())
                .Determinant;
            Assert.AreEqual(determinant, det);
        }

        [Test]
        public void MultiplyTest1()
        {
            var matrix1 = new Matrix(new double[,] {{3, 2, 1}, {6, 5, 4}, {3, 8, 1}});
            var matrix2 = new Matrix(new double[,] {{12, 8, 1}, {4, 9, 2}, {3, 6, 0}});
            var result = new double[,] {{47, 48, 7}, {104, 117, 16}, {71, 102, 19}};

            Assert.AreEqual(result, (matrix1 * matrix2).Elements);
        }


        [Test]
        public void MultiplyTest2()
        {
            var matrix1 = new Matrix(new double[,] {{1, 12}, {38, 11}, {9, 21}});
            var matrix2 = new Matrix(new double[,] {{13, 4, 1, 8, 4}, {3, 17, 21, 0, 9}});
            var result = new double[,]
            {
                {49, 208, 253, 8, 112},
                {527, 339, 269, 304, 251},
                {180, 393, 450, 72, 225}
            };

            Assert.AreEqual(result, (matrix1 * matrix2).Elements);
        }


        [Test]
        public void MultiplyTest3()
        {
            var matrix1 = new Matrix(new double[,] {{1}, {38}});
            var matrix2 = new Matrix(new double[,] {{13, 4}});
            var result = new double[,]
            {
                {13, 4},
                {494, 152}
            };

            Assert.AreEqual(result, (matrix1 * matrix2).Elements);
        }

        [Test]
        public void MultiplyTest4()
        {
            var matrix1 = new Matrix(new double[,] {{31, 209, 21}, {9, 8, 31}, {6, 72, 81}});
            var matrix2 = new Matrix(new double[,] {{7}, {18}, {29}});
            var result = new double[,] {{4588}, {1106}, {3687}};

            Assert.AreEqual(result, (matrix1 * matrix2).Elements);
        }

        [Test]
        public void InverseTest1()
        {
            var matrix = new Matrix(new double[,]
            {
                {43, 129, 9},
                {67, 13, 23},
                {0, 191, 18}
            });

            var inverse = new Matrix(new double[,]
            {
                {-4159, -603, 2850},
                {-1206, 774, -386},
                {12797, -8213, -8084}
            }).ElementwiseOperation(e => e / -219238.0);

            Assert.AreEqual(inverse, matrix.Inverse());
        }

        [Test]
        public void InverseTest2()
        {
            var matrix = new Matrix(new double[,]
            {
                {98, 103, 887},
                {301, 345, 13},
                {39, 91, 3}
            });

            var inverse = new Matrix(new double[,]
            {
                {-148, 80408, -304676},
                {-396, -34299, 265713},
                {13936, -4901, 2807}
            }).ElementwiseOperation(e => e / 12305940.0);

            Assert.AreEqual(inverse, matrix.Inverse());
        }

        [Test]
        public void InverseTest3()
        {
            var matrix = new Matrix(new double[,] {{1, 2}, {3, 6}});
            Assert.Throws(typeof(ArgumentException), () => matrix.Inverse());
        }

        [Test]
        public void KramerSolverTest1()
        {
            var matrix = new Matrix(new double[,] {{21, 82, 1}, {47, 19, 37}, {8, 91, 12}});
            var vector = new Vector(new double[] {81, 3, 54});
            var result = new Vector(new double[] {-94128, -60396, 143790}).ElementwiseOperation(e => e / -83770.0);

            var (kramerResult, answer) = new KramerSolver().Solve(matrix, vector);
            Assert.AreEqual(kramerResult, LinearSystemSolveResult.Single);
            Assert.AreEqual(answer, result);
        }

        [Test]
        public void KramerSolverTest2()
        {
            var matrix = new Matrix(new double[,] {{1, 2}, {3, 6}});
            var vector = new Vector(new double[] {8, 12});

            var (result, answer) = new KramerSolver().Solve(matrix, vector);
            Assert.AreEqual(result, LinearSystemSolveResult.InfinityOrAbsence);
            Assert.AreEqual(answer, new Vector(0));
        }

        [Test]
        public void MatrixToVectorConversion()
        {
            var matrix = new Matrix(new double[,] {{1, 2, 3}, {4, 5, 6}, {7, 8, 9}});
            Assert.Throws(typeof(InvalidOperationException), () => matrix.ToVector());
        }

        [Test]
        public void MatrixToVectorConversion1()
        {
            var matrix = new Matrix(new double[,] {{1}, {2}, {3}, {4}, {5}}).ToVector();
            var vector = new Vector(new double[] {1, 2, 3, 4, 5});
            Assert.AreEqual(vector, matrix);
        }

        [Test]
        public void MatrixToVectorConversion2()
        {
            var matrix = new Matrix(new double[,] {{1, 2, 3, 4, 5}}).ToVector();
            var vector = new Vector(new double[] {1, 2, 3, 4, 5});
            Assert.AreEqual(vector, matrix);
        }

        [Test]
        public void InverseSolverTest1()
        {
            var matrix = new Matrix(new double[,] {{21, 82, 1}, {47, 19, 37}, {8, 91, 12}});
            var vector = new Vector(new double[] {81, 3, 54});
            var result = new Vector(new double[] {-94128, -60396, 143790}).ElementwiseOperation(e => e / -83770.0);

            var (kramerResult, answer) = new InverseMatrixSolver().Solve(matrix, vector);
            Assert.AreEqual(kramerResult, LinearSystemSolveResult.Single);
            Assert.AreEqual(answer, result);
        }

        [Test]
        public void InverseSolverTest2()
        {
            var matrix = new Matrix(new double[,] {{1, 2}, {3, 6}});
            var vector = new Vector(new double[] {8, 12});

            var (result, answer) = new InverseMatrixSolver().Solve(matrix, vector);
            Assert.AreEqual(result, LinearSystemSolveResult.InfinityOrAbsence);
            Assert.AreEqual(answer, new Vector(0));
        }

        [Test]
        public void VectorToVerticalMatrixTest()
        {
            var vector = new Vector(new double[] {1, 2, 3, 4, 5});
            var matrix = new Matrix(new double[,] {{1}, {2}, {3}, {4}, {5}});
            Assert.AreEqual(matrix, vector.ToVerticalMatrix());
        }
    }
}