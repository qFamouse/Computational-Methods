using System;
using System.Collections.Generic;
using System.Text;

namespace ComputationalMethods.Integrate.OdeSolvers
{
    class RungeKutta
    {
        public static double[] FourthOrder(double y0, double start, double end, int N, Func<double, double, double> f)
        {
            double[] y = new double[N];
            y[0] = y0;
            double h = (end - start) / (N - 1); // N-1 -> Because there are actually partitions N-1 partitions
            double h2 = h / 2; // Half of h
            double x = start;

            for (int i = 1; i < N; i++, x += h)
            {
                double K1 = f(x, y[i-1]);
                double K2 = f(x + h2, y[i-1] + h2 * K1);
                double K3 = f(x + h2, y[i-1] + h2 * K2);
                double K4 = f(x + h, y[i-1] + h * K3);

                double Δy = (h / 6) * (K1 + 2 * K2 + 2 * K3 + K4);

                y[i] = Δy + y[i - 1];
            }

            return y;
        }
    }
}
