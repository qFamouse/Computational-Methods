using System;
using System.Collections.Generic;
using System.Text;

namespace ComputationalMethods.Approximation.InterpolationMethods {
    class Lagrange : IInterpolation {
        public double Solve(double x, int degree, FunctionValueTable functionValues) {
            // CHECK FOR ERRORS //
            if (functionValues.Points.Count < degree + 1) {
                throw new Exception("Not enough items in the list");
            }
            // ALGORITHM //
            double polynomial = 0;

            for (int k = 0; k <= degree; k++) {
                double basicsPol = 1;
                for (int i = 0; i <= degree; i++) {
                    if (k != i) { // if k == i skip
                        basicsPol *= (x - functionValues.Points[i].x) / (functionValues.Points[k].x - functionValues.Points[i].x);
                    }
                }
                polynomial += basicsPol * functionValues.Points[k].y;
            }

            return polynomial;
        }
        public double Inaccuracy(double fx, double Lx, bool retAsPercentage = false) {
            double inaccuracy = Math.Abs(fx - Lx);
            return retAsPercentage ? (inaccuracy * 100) / fx : inaccuracy;
        }
    }
}
