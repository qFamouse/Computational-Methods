using System;
using System.Collections.Generic;
using System.Data;
using System.Runtime.Serialization.Formatters;
using System.Text;
using MathNet.Numerics.LinearAlgebra;

namespace ComputationalMethods.Numerics.OdeSolvers
{
    class GridMethod
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pX">p(x)</param>
        /// <param name="qX">q(c)</param>
        /// <param name="fX">f(x)</param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="N"></param>
        /// <param name="α1"></param>
        /// <param name="β1"></param>
        /// <param name="γ1"></param>
        /// <param name="α2"></param>
        /// <param name="β2"></param>
        /// <param name="γ2"></param>
        public static void Solve(Func<double, double> pX, Func<double, double> qX, Func<double, double> fX,
            double start, double end, int N,
            double α1, double β1, double γ1,
            double α2, double β2, double γ2)
        {
            double h = (end - start) / (N - 1);


            double a(double x) // Ci = 1 - h/2 * p(x)
            {
                return 1 - (h / 2) * pX(x);
            }

            double b(double x) // Bi = h^2 * q(Xi) - 2
            {
                return Math.Pow(h, 2) * qX(x) - 2;
            }

            double c(double x) // Ai = 1 + h/2 * p(Xi)
            {
                return 1 + (h / 2) * pX(x);
            }

            var size = N - 2;

            var matrix = new double[size, size];
            var freeMembers = new double[size];


            double x = start + h;
            for (int row = 0; row < size; row++)
            {
                int column = row;

                if (column - 1 >= 0)
                {
                    matrix[row, column - 1] = a(x);
                    Matrix.Matrix.Write2D(matrix);
                    Console.WriteLine();
                }

                if (column < size)
                {
                    matrix[row, column] = b(x);
                    Matrix.Matrix.Write2D(matrix);
                    Console.WriteLine();
                }

                if (column + 1 < size)
                {
                    matrix[row, column + 1] = c(x);
                    Matrix.Matrix.Write2D(matrix);
                    Console.WriteLine();
                }

                freeMembers[row] = Math.Pow(h, 2) * fX(x);

                x += h;
            }


            var A = Matrix<double>.Build.DenseOfArray(matrix);
            var B = Vector<double>.Build.Dense(freeMembers);

            var answ = A.Solve(B);

        }
    }
}
