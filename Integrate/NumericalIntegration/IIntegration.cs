using System;
using System.Collections.Generic;
using System.Text;

namespace ComputationalMethods.Integrate.NumericalIntegration
{
    public interface IIntegration
    {
        int NumberOfPartitions { get; set; }
        public double Solve(Func<double, double> f, double intervalBegin, double intervalEnd);
    }
}
