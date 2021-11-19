using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace ComputationalMethods.Integrate.OdeSolvers
{
    class LeonhardEuler
    {
        #region Public Static Methods

        #region Simple Method

        public static double[] Simple(double y0, double start, double end, int N, Func<double, double, double> f)
        {
            double[] y = new double[N];
            y[0] = y0;
            double h = CalculateStep(start, end, N);
            double x = start;

            for (int i = 1; i < N; i++, x+=h)
            {
                y[i] = y[i - 1] + h * f(x, y[i - 1]);
            }

            return y;
        }

        public static double[,] Simple(double[] y0, double start, double end, int N, Func<double, double[], double[]> f)
        {
            // Initial Values //
            double[,] y = new double[N, y0.Length +1]; // +1 for 'x' values
            double h = CalculateStep(start, end, N);
            double x = start;
            y[0, 0] = x;

            for (int i = 0; i < y0.Length; i++)
            {
                y[0, i + 1] = y0[i]; // + 1 because on y[i,0] we have 'x' value
            }
            // Solution algorithm //
            var yI = (double[])y0.Clone(); // Clone y0 because we need to edit it
            for (int i = 1; i < N; i++) // row
            {
                double[] fI = f(x, yI); // Calculate functions value
                y[i, 0] = x += h; // Put 'x+=h' value to array with go to next step
                for (int j = 1; j <= y0.Length; j++) // column
                {
                    // Calculate to array and yI(for next calculating).
                    yI[j - 1] = y[i, j] = y[i - 1, j] + h * fI[j - 1];
                }
            }
            // Return //
            return y;
        }

        #endregion

        public static double[] Improved(double y0, double start, double end, int N, Func<double, double, double> f)
        {
            double[] y = new double[N];
            y[0] = y0;
            double h = CalculateStep(start, end, N);
            double h2 = h / 2; // Half of h
            double x = start;

            for (int i = 1; i < N; i++, x += h)
            {
                y[i] = y[i - 1] + h * f(x + h2, y[i-1] + h2 * f(x, y[i-1]));
            }

            return y;
        }

        public static double[] CauchyImproved(double y0, double start, double end, int N, Func<double, double, double> f)
        {
            double[] y = new double[N];
            y[0] = y0;
            double h = CalculateStep(start, end, N);
            double h2 = h / 2; // Half of h
            double x = start;

            for (int i = 1; i < N; i++, x += h)
            {
                y[i] = y[i - 1] + h2 * (f(x, y[i - 1]) + f(x + h, y[i - 1] + h * f(x, y[i - 1])));
            }

            return y;
        }

        #endregion

        #region Private Static Methods

        private static double CalculateStep(double start, double end, int N) // h
        {
            return (end - start) / (N - 1); // N-1 -> Because there are actually partitions N-1 partitions
        }

        #endregion
    }
}
