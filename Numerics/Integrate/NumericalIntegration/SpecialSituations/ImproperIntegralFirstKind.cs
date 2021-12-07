using System;

namespace ComputationalMethods.Numerics.Integrate.NumericalIntegration.SpecialSituations
{
    class ImproperIntegralFirstKind : IIntegration
    {
        public int NumberOfPartitions { get; set; } = 20;

        public double Solve(Func<double, double> f, double intervalBegin, double intervalEnd = 0)
        {
            if (!Double.IsPositiveInfinity(intervalEnd))
            {
                throw new ArgumentException("This Method only for intervalEnd = +Infinity");
            }
            // Input Data //
            intervalEnd = 1;
            double h = 1;
            // Legacy Integration Method //
            Integration finder = new Integration(new Gauss());
            finder.NumberOfPartitions = 6;
            // Main Method //
            double I = finder.Solve(f, intervalBegin, intervalEnd);

            intervalEnd += h;

            double R = finder.Solve(f, intervalBegin, intervalEnd);

            double e = 0.0001;

            while (R - I > e / 2)
            {
                I = R;
                intervalEnd += h;
                R = finder.Solve(f, intervalBegin, intervalEnd);
            }

            return R;
        }
    }
}
