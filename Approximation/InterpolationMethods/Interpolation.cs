using System.Collections.Generic;

namespace ComputationalMethods.Approximation.InterpolationMethods {
    class Interpolation {
        public IInterpolation InterpolationMethod { get; set; }

        public Interpolation(IInterpolation interpolationMethod) {
            this.InterpolationMethod = interpolationMethod;
        }

        public double Solve(double x, int n, FunctionValueTable functionValues) {
            return InterpolationMethod.Solve(x, n, functionValues);
        }

        public double Inaccuracy(double fx, double polynomial, bool retAsPercentage = false) {
            return InterpolationMethod.Inaccuracy(fx, polynomial, retAsPercentage);
        }

        public static FunctionValueTable CutLocalValues(double x, int degree, FunctionValueTable values)
        {
            var localValues = new List<(double x, double y)>();

            int index = 0; // index for first element which more then 'x'
            for (; index < values.Points.Count && x > values.Points[index].x; index++)
            {
                // Search element which more then 'x'
            }
            localValues.Add(values.Points[index]);

            int move = 1;

            while (localValues.Count != degree + 1 && localValues.Count < values.Points.Count)
            {
                if (index - move >= 0)
                {
                    localValues.Insert(0, values.Points[index - move]);
                }

                if (index + move < values.Points.Count)
                {
                    localValues.Add(values.Points[index + move]);
                }

                move++;
            }

            return new FunctionValueTable(localValues);
        }
    }
}
