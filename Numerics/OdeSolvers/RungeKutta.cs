using System;

namespace ComputationalMethods.Numerics.OdeSolvers
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

        public static double[,] FourthOrder(double[] y0, double start, double end, int N, Func<double, double[], double[]> f)
        {
            // Initial Values //
            double[,] y = new double[N, y0.Length +1]; // +1 for 'x' values
            double h = (end - start) / (N - 1); // N-1 -> Because there are actually partitions N-1 partitions
            double h2 = h / 2; // Half of h
            double x = start;
            y[0, 0] = x;

            for (int i = 0; i < y0.Length; i++)
            {
                y[0, i + 1] = y0[i]; // + 1 because on y[i,0] we have 'x' value
            }
            // Solution algorithm //
            var yI = (double[])y0.Clone(); // Clone y0 because we need to edit it
            for (int i = 1; i < N; i++)
            {
                var yIlocal = (double[])yI.Clone();
                // K1
                double[] K1 = f(x, yI); 
                // K2
                for (int j = 0; j < yI.Length; j++)
                {
                    yIlocal[j] = yI[j] + 0.05 * K1[j];
                }
                double[] K2 = f(x+0.05, yIlocal);
                // K3
                for (int j = 0; j < yI.Length; j++)
                {
                    yIlocal[j] = yI[j] + 0.05 * K2[j];
                }
                double[] K3 = f(x + 0.05, yIlocal);
                // K4
                for (int j = 0; j < yI.Length; j++)
                {
                    yIlocal[j] = yI[j] + 0.1 * K3[j];
                }
                double[] K4 = f(x + 0.1, yIlocal);
                // y
                for (int k = 0, j = 1; k < yI.Length; k++, j++)
                {
                    yI[j - 1] = y[i, j] = yI[k] + (h / 6) * (K1[k] + 2 * K2[k] + 2 * K3[k] + K4[k]);
                }
                // x
                y[i, 0] = x += h;
            }

            return y;
        }
    }
}
