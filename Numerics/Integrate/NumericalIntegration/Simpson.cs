using System;

namespace ComputationalMethods.Numerics.Integrate.NumericalIntegration
{
    class Simpson : IIntegration
    {
        public int NumberOfPartitions { get; set; } = 100;

        public double Solve(Func<double, double> f, double intervalBegin, double intervalEnd)
        {
            double h = Math.Round((intervalEnd - intervalBegin) / NumberOfPartitions, 1);

            double integralValue = 0;

            integralValue += f(intervalBegin) +
                             4 * f((intervalEnd + intervalBegin) / 2) +
                             f(intervalEnd);

            integralValue *= (intervalEnd - intervalBegin) / 6;


            //for (int i = 1; i < NumberOfPartitions; i++)
            //{
            //    integralValue += f(intervalBegin + h * i - 1) +
            //                     4 * f(intervalBegin + h / 2 + i * h) +
            //                     f(intervalBegin + h * i);
            //}

            //integralValue *= h / 6;


            //for (int i = 0; i < NumberOfPartitions; i++)
            //{
            //    integralValue += f(intervalBegin + 2 * i * h) +
            //                     4 * f(intervalBegin + (2 * i + 1) * h) +
            //                     f(intervalBegin + (2 * i + 2) * h);
            //}

            //integralValue *= h / 3;

            return integralValue;
        }
    }
}
