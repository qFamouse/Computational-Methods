using System;

namespace ComputationalMethods.Numerics.Integrate.NumericalIntegration
{
    public interface IIntegration
    {
        int NumberOfPartitions { get; set; }
        public double Solve(Func<double, double> f, double intervalBegin, double intervalEnd);
    }
}
