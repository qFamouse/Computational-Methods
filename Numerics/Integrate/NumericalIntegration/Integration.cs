using System;

namespace ComputationalMethods.Numerics.Integrate.NumericalIntegration
{
    public class Integration
    {
        /// <summary>
        /// Determines which method should usable
        /// </summary>
        public IIntegration IntegrationMethod { get; set; }
        /// <summary>
        /// Determines how many partitions create
        /// </summary>
        public int NumberOfPartitions
        {
            get => IntegrationMethod.NumberOfPartitions;
            set => IntegrationMethod.NumberOfPartitions = value;
        }
        /// <summary>
        /// Constructor where we choose integration method
        /// </summary>
        /// <param name="integrationMethod"></param>
        public Integration(IIntegration integrationMethod)
        {
            this.IntegrationMethod = integrationMethod;
        }

        public double Solve(Func<double, double> f, double intervalBegin, double intervalEnd)
        {
            if (f is null)
            {
                throw new ArgumentNullException(nameof(f), "Function is null");
            }

            if (intervalEnd <= intervalBegin)
            {
                throw new ArgumentException("The end interval is less than the start interval");
            }

            return this.IntegrationMethod.Solve(f, intervalBegin, intervalEnd);
        }

    }
}
