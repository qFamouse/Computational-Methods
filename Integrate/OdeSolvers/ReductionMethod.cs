using System;
using System.Collections.Generic;
using System.Text;

namespace ComputationalMethods.Integrate.OdeSolvers
{
    class ReductionMethod
    {
        public static void Test(Func<double, double> p, Func<double, double> q, Func<double, double> f, 
            double start, double end, int N)
        {
            // Initial values //
            double[] y0 = { 0, 0 };
            double[] y1 = { 1, 0 };
            double[] y2 = { 0, 1 };

            Func<double, double[], double[]> odeSystem0 = (x, param) =>
            {
                double y = param[0];
                double z = param[1];

                return new double[]
                {
                    z,
                    f(x) - q(x) * y - p(x) * z
                };
            };

            Func<double, double[], double[]> odeSystem1 = (x, param) =>
            {
                double y = param[0];
                double z = param[1];

                return new double[]
                {
                    z,
                    -p(x) * z - q(x) * y
                };
            };

            Func<double, double[], double[]> odeSystem2 = (x, param) =>
            {
                double y = param[0];
                double z = param[1];

                return new double[]
                {
                    z,
                    -p(x) * z - q(x) * y
                };
            };

            // Solving systems using the method RungeKutta //
            var solveY0 = RungeKutta.FourthOrder(y0, start, end, N, odeSystem0);
            var solveY1 = RungeKutta.FourthOrder(y1, start, end, N, odeSystem1);
            var solveY2 = RungeKutta.FourthOrder(y2, start, end, N, odeSystem2);
        }
    }
}
