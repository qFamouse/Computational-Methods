using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace ComputationalMethods.Integrate.OdeSolvers
{
    class LeonhardEuler
    {
        public static double[] Simple(double y0, double start, double end, int N, Func<double, double, double> f)
        {
            double[] y = new double[N];
            y[0] = y0;
            double h = (end - start) / (N-1); // N-1 -> Because there are actually partitions N-1 partitions
            double x = start;

            for (int i = 1; i < N; i++, x+=h)
            {
                y[i] = y[i - 1] + h * f(x, y[i - 1]);
            }

            return y;
        }

        public static double[] Improved(double y0, double start, double end, int N, Func<double, double, double> f)
        {
            double[] y = new double[N];
            y[0] = y0;
            double h = (end - start) / (N - 1); // N-1 -> Because there are actually partitions N-1 partitions
            double h2 = h / 2; // Half of h
            double x = start;

            for (int i = 1; i < N; i++, x += h)
            {
                y[i] = y[i - 1] + h * f(x + h2, y[i-1] + h2 * f(x, y[i-1]));
            }

            return y;
        }
    }
}
