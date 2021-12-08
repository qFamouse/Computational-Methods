using System;
using System.Collections.Generic;
using System.Data;
using System.Runtime.Serialization.Formatters;
using System.Text;
using ComputationalMethods.Numerics.LinearSystems;
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
        public static double[] Solve(Func<double, double> pX, Func<double, double> qX, Func<double, double> fX,
            double start, double end, int N,
            double α1, double β1, double γ1,
            double α2, double β2, double γ2)
        {
            if (Math.Pow(α1, 2) + Math.Pow(β1, 2) < 0 ||
                Math.Pow(α2, 2) + Math.Pow(β2, 2) < 0)
            {
                throw new ArgumentException("αi^2 + βi^2 must be greater then 0");
            }

            double h = (end - start) / (N - 1);

            double a(double x) { return 1 - (h / 2) * pX(x); }         // Ai = 1 - h/2 * p(x)

            double b(double x) { return Math.Pow(h, 2) * qX(x) - 2; }  // Bi = h^2 * q(Xi) - 2

            double c(double x) { return 1 + (h / 2) * pX(x); }         // Ci =  1 + h/2 * p(Xi)

            var matrix = new double[N, N];
            var freeMembers = new double[N];

            // Difference approximation for boundary conditions
            matrix[0, 0] = α1 - β1 / h;
            matrix[0, 1] = β1 / h;
            freeMembers[0] = γ1;

            matrix[N - 1, N - 1] = α2 + β2 / h;
            matrix[N - 1, N - 2] = -(β2 / h);
            freeMembers[N - 1] = γ2;

            // Main cycle of filling matrix
            double x = start + h;
            for (int i = 1; i < N - 1; i++, x+=h)
            {
                matrix[i, i - 1] = a(x); // Ai = 1 - h/2 * p(Xi)
                matrix[i, i]     = b(x); // Bi = h^2 * q(Xi) - 2
                matrix[i, i + 1] = c(x); // Ci = 1 + h/2 * p(Xi)

                freeMembers[i]   = Math.Pow(h, 2) * fX(x);
            }

            var y = SweepMethod.Solve(matrix, freeMembers);

            return y;
        }
    }
}
