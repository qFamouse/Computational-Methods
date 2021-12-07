using System;
using ComputationalMethods.Numerics.LinearSystems;

namespace ComputationalMethods.Approximation.ApproximationMethods
{
    class LeastSquaresMethod
    {
        public static double[] Solve(FunctionValueTable functionValues, int polynomialDegree)
        {
            var matrixDegree = polynomialDegree + 1;
            var matrix = new double[matrixDegree, matrixDegree];
            var freeMembers = new double[matrixDegree];

            for (int row = 0; row < matrixDegree; row++)
            {
                for (int column = 0; column < matrixDegree; column++)
                {
                    for (int element = 0; element < functionValues.Points.Count; element++)
                    {
                        matrix[row, column] +=
                            Math.Pow(functionValues.Points[element].x, polynomialDegree * 2 - row - column);
                    }
                }

                for (int element = 0; element < functionValues.Points.Count; element++)
                {
                    freeMembers[row] += functionValues.Points[element].y *
                                        Math.Pow(functionValues.Points[element].x, polynomialDegree - row);
                }
            }


            return GaussSimpleMethod.Solve(matrix, freeMembers);
        }
    }
}
