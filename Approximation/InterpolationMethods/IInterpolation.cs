using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Xml;

namespace ComputationalMethods.Approximation.InterpolationMethods {
    interface IInterpolation {
        double Solve(double x, int degree, FunctionValueTable functionValues);
        double Inaccuracy(double fx, double polynomial, bool retAsPercentage = false);
    }
}
