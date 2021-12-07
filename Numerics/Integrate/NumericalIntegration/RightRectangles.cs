using System;

namespace ComputationalMethods.Numerics.Integrate.NumericalIntegration
{
    class RightRectangles : IIntegration
    {
        /// <summary>
        /// Determines how many parts to split, Default 100
        /// </summary>
        public int NumberOfPartitions { get; set; } = 100;

        public double Solve(Func<double, double> f, double intervalBegin, double intervalEnd)
        {
            double h = Math.Round((intervalEnd - intervalBegin) / NumberOfPartitions, 1);

            double integralValue = 0;

            for (int i = 1; i < NumberOfPartitions; i++)
            {
                integralValue += f(intervalBegin + h * i);
            }

            integralValue *= h;

            return integralValue;
        }
    }
}
