using System;

namespace ComputationalMethods.Approximation.InterpolationMethods {
    class Newton : IInterpolation {
        public double Solve(double x, int degree, FunctionValueTable functionValues) {
            // CHECK FOR ERRORS //
            if (functionValues.Points.Count < degree) {
                throw new Exception("Not enough items in the list");
            }

            double polynomial = functionValues.Points[0].y;
            for (int i = 1; i < degree; i++) {
                double basicsPol = 0;

                for (int j = 0; j <= i; j++) {
                    double den = 1;
                    for (int k = 0; k <= i; k++) {
                        if (k != j) {
                            den *= (functionValues.Points[j].x - functionValues.Points[k].x);
                        }
                    }
                    basicsPol += functionValues.Points[j].y / den;
                }


                for (int k = 0; k < i; k++) {
                    basicsPol *= x - functionValues.Points[k].x;
                }

                polynomial += basicsPol;
            }

            return polynomial;
        }
        public double Inaccuracy(double fx, double Nx, bool retAsPercentage = false) {
            double inaccuracy = Math.Abs(fx - Nx);
            return retAsPercentage ? (inaccuracy * 100) / fx : inaccuracy;
        }
    }
}
