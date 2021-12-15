using MathNet.Numerics.LinearAlgebra;
using System;
using System.Collections.Generic;
using System.Text;

namespace ComputationalMethods.Numerics.OdeSolvers
{
    class CollocationMethod
    {
        public static void Solve(Func<double, double> pX, Func<double, double> qX, Func<double, double> fX,
            double start, double end, int N,
            double α1, double β1, double γ1,
            double α2, double β2, double γ2)
        {
            Func<double, double>[] ϕ = new[]
            {
                new Func<double, double>(x => x * (1 - x)),
                new Func<double, double>(x => Math.Pow(x, 2) * (1 - x))
            };

            double ϕ0(double x) { return 0; }
            double ϕ1(double x) { return x * (1 - x); }
            double ϕ2(double x) { return Math.Pow(x, 2) * (1 - x); }

            if (ϕ1(start) != ϕ1(end) ||
                ϕ2(start) != ϕ2(end))
            {
                throw new ArgumentException("Bad");
            }

            double x1 = 0.25;
            double x2 = 0.5;

            double[] Xk = new[] {0.25, 0.5};

            Func<double, double>[] dd = new[]
            {
                new Func<double, double>(x => -2),
                new Func<double, double>(x => 2 - 6 * x)
            };

            //double ϕ1dd(double x) { return -2;}
            //double ϕ2dd(double x) { return 2 - 6 * x;}

            double[,] A = new double[2, 2];
            for (int i = 0; i < 2; i++)
            {
                for (int k = 0; k < 2; k++)
                {
                    A[k, i] = dd[i](Xk[k]) + ϕ[i](Xk[k]);
                }
            }

            Matrix.Matrix.Write2D(A);

            var gg = Matrix<double>.Build.DenseOfArray(A);
            var bb = Vector<double>.Build.Dense(Xk);

            var c1c2 = gg.Solve(bb).ToArray(); // c2, c1

            double h = 0.1;

            double[] y = new double[N];
            double x = start;
            for (int i = 0; i < N; i++)
            {
                y[i] = c1c2[0] * ϕ1(start) + c1c2[1] * ϕ2(start);
                start += h;
            }

            System.Diagnostics.Debugger.Break();
        }
    }
}
