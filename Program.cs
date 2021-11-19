using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using ComputationalMethods.Approximation.ApproximationMethods;
using ComputationalMethods.Approximation.InterpolationMethods;
using ComputationalMethods.Integrate.NumericalIntegration;
using ComputationalMethods.Integrate.NumericalIntegration.SpecialSituations;
using ComputationalMethods.Integrate.OdeSolvers;
using MathNet.Numerics.Integration;
using MathNet.Numerics.OdeSolvers;
using RungeKutta = ComputationalMethods.Integrate.OdeSolvers.RungeKutta;

namespace ComputationalMethods {
    class Program {
        #region Main
        static void Main(string[] args)
        {
            Lab7();
        }

        #endregion

        #region Approximation

        #region Testing Interpolation Methods

        static void Lab1(int n = 4, double x1 = 2.24, double x2 = 4.63, double x3 = 7.94)
        {
            /* Create XFunctionValueTable */
            TableBuilder builder = new TableBuilder();
            builder.StartOfRange = 1;
            builder.EndOfRange = 10;
            builder.Increment = 2.0;
            builder.Function = (x) => x == 0 ? 0 : -1 / x;
            FunctionValueTable functionValues = builder.BuildTable();
            /* Show Table */
            Console.WriteLine(functionValues.ToString());
            /* Lagrange Interpolation */
            double Lx;
            Interpolation interpolation = new Interpolation(new Lagrange()); // Use Pattern Strategy

            Console.WriteLine("\nLagrange Interpolation");
            // x1
            Lx = interpolation.Solve(x1, n, Interpolation.CutLocalValues(x1, n, functionValues));
            Console.WriteLine($"x = {x1} : {Lx,20} | Inaccuracy: {interpolation.Inaccuracy(builder.Function.Invoke(x1), Lx)}");
            // x2
            Lx = interpolation.Solve(x2, n, Interpolation.CutLocalValues(x2, n, functionValues));
            Console.WriteLine($"x = {x2} : {Lx,20} | Inaccuracy: {interpolation.Inaccuracy(builder.Function.Invoke(x2), Lx)}");
            // x3
            Lx = interpolation.Solve(x3, n, Interpolation.CutLocalValues(x3, n, functionValues));
            Console.WriteLine($"x = {x3} : {Lx,20} | Inaccuracy: {interpolation.Inaccuracy(builder.Function.Invoke(x3), Lx)}");
        }

        static void Lab2(int n = 3, double x1 = 2.24, double x2 = 4.63, double x3 = 7.94)
        {
            /* Create FunctionValueTable */
            TableBuilder builder = new TableBuilder();
            builder.StartOfRange = 1;
            builder.EndOfRange = 10;
            builder.Increment = 2.0;
            builder.Function = (x) => x == 0 ? 0 : -1 / x;
            FunctionValueTable functionValues = builder.BuildTable();
            /* Show Table */
            Console.WriteLine(functionValues.ToString());
            /* nNewton Interpolation */
            double Nx;
            Interpolation interpolation = new Interpolation(new Newton()); // Use Pattern Strategy

            Console.WriteLine("\nNewton Interpolation");
            // x1
            Nx = interpolation.Solve(x1, n, Interpolation.CutLocalValues(x1, n, functionValues));
            Console.WriteLine($"x = {x1} : {Nx,20} | Inaccuracy: {interpolation.Inaccuracy(builder.Function.Invoke(x1), Nx)}");
            // x2
            Nx = interpolation.Solve(x2, n, Interpolation.CutLocalValues(x2, n, functionValues));
            Console.WriteLine($"x = {x2} : {Nx,20} | Inaccuracy: {interpolation.Inaccuracy(builder.Function.Invoke(x2), Nx)}");
            // x3
            Nx = interpolation.Solve(x3, n, Interpolation.CutLocalValues(x3, n, functionValues));
            Console.WriteLine($"x = {x3} : {Nx,20} | Inaccuracy: {interpolation.Inaccuracy(builder.Function.Invoke(x3), Nx)}");
        }

        static void Lab3()
        {

        }

        #endregion

        #region Testing Approximation Methods

        static void Lab4()
        {
            /* Create FunctionValueTable */
            double[] x = { 0, 0.1, 0.2, 0.3, 0.4, 0.5, 0.6, 0.7 };
            double[] y = { 3.02, 2.81, 2.57, 2.39, 2.18, 1.99, 1.81, 1.85 };

            FunctionValueTable table = new FunctionValueTable(x, y);

            var t = LeastSquaresMethod.Solve(table, 5);

            int degree = t.Length - 1;
            for (int i = 0; i < t.Length; i++)
            {
                string kek = t[i] < 0 ? "" : "+";
                Console.Write($"{kek}{t[i]}x^{degree - i}");
            }

            // https://www.desmos.com/calculator/etdleeqv6k
        }

        #endregion

        #endregion

        #region Numerical Integration

        static void Lab5()
        {
            //Func<double, double> f = (x) => 1 / (1 + Math.Pow(x, 2));
            //double intervalBegin = 0;
            //double intervalEnd = 1;

            Func<double, double> f = (x) => x * Math.Pow(Math.E, 0.8 * x);
            double intervalBegin = 2;
            double intervalEnd = 3;

            Console.WriteLine("Left Rectangles Method");
            Integration finder = new Integration(new LeftRectangles());
            finder.NumberOfPartitions = 11;
            Console.WriteLine("{0:##0.####}", finder.Solve(f, intervalBegin, intervalEnd));

            Console.WriteLine("Right Rectangles Method");
            finder = new Integration(new RightRectangles());
            finder.NumberOfPartitions = 11;
            Console.WriteLine("{0:##0.####}", finder.Solve(f, intervalBegin, intervalEnd));

            Console.WriteLine("Middle Rectangles Method");
            finder = new Integration(new MiddleRectangles());
            finder.NumberOfPartitions = 11;
            Console.WriteLine("{0:##0.####}", finder.Solve(f, intervalBegin, intervalEnd));

            Console.WriteLine("Trapezoid Method");
            finder = new Integration(new Trapezoid());
            finder.NumberOfPartitions = 11;
            Console.WriteLine("{0:##0.####}", finder.Solve(f, intervalBegin, intervalEnd));

            Console.WriteLine("Simpson's Method");
            finder = new Integration(new Simpson());
            finder.NumberOfPartitions = 11;
            Console.WriteLine("{0:##0.####}", finder.Solve(f, intervalBegin, intervalEnd));

            Console.WriteLine("Gauss Method");
            finder = new Integration(new Gauss());
            finder.NumberOfPartitions = 6;
            Console.WriteLine("{0:##0.####}", finder.Solve(f, intervalBegin, intervalEnd));

            Console.WriteLine("Library Answer");
            Console.WriteLine("{0:##0.####}", MathNet.Numerics.Integrate.DoubleExponential(f, intervalBegin, intervalEnd));
        }

        static void Lab6()
        {
            // 10.1 //
            Func<double, double> f = (x) => Math.Log(x) * Math.Log(1 + x);
            double intervalBegin = 0;
            double intervalEnd = 1;

            Console.WriteLine("First Integral (Gauss Method)");
            Integration finder = new Integration(new Gauss());
            finder.NumberOfPartitions = 6;
            Console.WriteLine("{0:##0.####}", finder.Solve(f, intervalBegin, intervalEnd));

            Console.WriteLine("Library Answer");
            Console.WriteLine("{0:##0.####}", MathNet.Numerics.Integrate.DoubleExponential(f, intervalBegin, intervalEnd));

            // 10.2 //
            f = (x) => 1 / ((9 + Math.Pow(x, 2)) * (27 + Math.Pow(x, 3)));
            intervalBegin = 0;
            intervalEnd = Double.PositiveInfinity;
            Console.WriteLine("Second Integral (Special Situation)");
            finder = new Integration(new ImproperIntegralFirstKind());
            Console.WriteLine("{0:##0.####}", finder.Solve(f, intervalBegin, intervalEnd));
        }

        static void Lab7()
        {
            /* Example Condition */
            //double y0 = 4.6;
            //double start = 1.6;
            //double end = 2.6;
            //int N = 11;
            //Func<double, double, double> f = (x, y) => x + Math.Cos(y / 3);

            /* Variant 10 Condition */
            double y0 = 0;
            double start = 0;
            double end = 0.5;
            int N = 6;
            Func<double, double, double> f = (x, y) => (x * y) / (1 + Math.Pow(x, 2)) - 1 / (1 + Math.Pow(x, 2));

            double[] x = new double[N];
            x[0] = start;
            double h = 0.1;
            for (int i = 1; i < x.Length; i++)
            {
                x[i] = x[i - 1] + h;
            }
            // Methods //
            Console.WriteLine("LeonhardEuler Simple Method");
            double[] y = LeonhardEuler.Simple(y0, start, end, N, f);
            Console.WriteLine(new FunctionValueTable(x, y).ToStringInline());

            Console.WriteLine("LeonhardEuler Improved Method");
            y = LeonhardEuler.Improved(y0, start, end, N, f);
            Console.WriteLine(new FunctionValueTable(x, y).ToStringInline());

            Console.WriteLine("LeonhardEuler Euler-Cauchy Method");
            y = LeonhardEuler.CauchyImproved(y0, start, end, N, f);
            Console.WriteLine(new FunctionValueTable(x, y).ToStringInline());

            Console.WriteLine("RungeKutta FourthOrder Method");
            y = RungeKutta.FourthOrder(y0, start, end, N, f);
            Console.WriteLine(new FunctionValueTable(x, y).ToStringInline());

            Console.WriteLine("AdamsBashforth FourthOrder Method -> Answer ");
            y = AdamsBashforth.FourthOrder(y0, start, end, N, f);
            Console.WriteLine(new FunctionValueTable(x, y).ToStringInline());
        }

        #endregion
    }
}
