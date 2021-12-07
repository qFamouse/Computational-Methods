using System;

namespace ComputationalMethods.Numerics.Integrate.NumericalIntegration
{
    class Trapezoid : IIntegration
    {
        public int NumberOfPartitions { get; set; } = 100;

        public double Solve(Func<double, double> f, double intervalBegin, double intervalEnd)
        {
            double h = Math.Round((intervalEnd - intervalBegin) / NumberOfPartitions, 1);

            double integralValue = 0;
            for (int i = 1; i < NumberOfPartitions; i++)
            {
                integralValue += (f(intervalBegin + i * h) + f(intervalBegin + (i - 1) * h));
            }

            integralValue *= h / 2;

            return integralValue;
        }
    }
}
