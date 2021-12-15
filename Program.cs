using System;
using ComputationalMethods.Approximation.ApproximationMethods;
using ComputationalMethods.Approximation.InterpolationMethods;
using ComputationalMethods.Numerics.Integrate.NumericalIntegration;
using ComputationalMethods.Numerics.Integrate.NumericalIntegration.SpecialSituations;
using ComputationalMethods.Numerics.OdeSolvers;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.OdeSolvers;
using RungeKutta = ComputationalMethods.Numerics.OdeSolvers.RungeKutta;
using MathNetRungeKutta = MathNet.Numerics.OdeSolvers.RungeKutta;


namespace ComputationalMethods {
    class Program {
        #region Main
        static void Main(string[] args)
        {
            Lab11();
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

        #endregion

        #region Ordinary differential equation

        #region Cauchy problem

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

        #region System of linear differential equations

        static void Lab8()
        {
            /* Condition */
            double[] y0 = new double[]
            {
                /* Example */
                //0.5, // y(0)
                //1    // z(0)
                /* Variant 10 */
                1,     // y(0)
                2      // z(0)
            };
            double start = 0;
            double end = 0.5;
            int N = 6;
            Func<double, double[], double[]> funcs = (x, funcs) =>
            {
                double y = funcs[0];
                double z = funcs[1];

                return new double[]
                {
                    /* Example */
                    //Math.Pow(Math.E, -Math.Abs(z*z + y*y)) + 2 * x, // y'
                    //2 * Math.Pow(y, 2) + z // z'
                    /* Variant 10 */
                    Math.Cos(y+2*z), // y'
                    (2 / (4*y+x)) + x + 1 // z'
                };
            };


            /* Decision */
            Console.WriteLine("LeonhardEuler Simple Method");
            double[,] answer = LeonhardEuler.Simple(y0, start, end, N, funcs);
            Matrix.Matrix.Writeln2D(answer);

            Console.WriteLine("RungeKutta FourthOrder Method");
            answer = RungeKutta.FourthOrder(y0, start, end, N, funcs);
            Matrix.Matrix.Writeln2D(answer);

            /* Answer with MathNet */
            Vector<double> y0Math = Vector<double>.Build.Dense(new[] { 0.5, 1 });
            Func<double, Vector<double>, Vector<double>> funcsMath = (x, funcsVector) =>
            {
                double[] funcs = funcsVector.ToArray();
                double y = funcs[0];
                double z = funcs[1];

                return Vector<double>.Build.Dense(new[]
                {
                    /* Example */
                    //Math.Pow(Math.E, -Math.Abs(z*z + y*y)) + 2 * x, // y'
                    //2 * Math.Pow(y, 2) + z // z'
                    /* Variant 10 */
                    Math.Cos(y+2*z), // y'
                    (2 / (4*y+x)) + x + 1 // z'
                });
            };
            Vector<double>[] res = MathNetRungeKutta.FourthOrder(y0Math, start, end, N, funcsMath);

            double[] x = new double[N];
            double[] y = new double[N];
            for (int i = 0; i < N; i++)
            {
                double[] temp = res[i].ToArray();
                x[i] = temp[0];
                y[i] = temp[1];
            }
            Console.WriteLine("Library answer (RungeKutta)");
            Matrix.Matrix.Write2D(x);
            Matrix.Matrix.Write2D(y);
        }

        #endregion

        #region Solving boundary value problems for ordinary differential equations

        static void Lab9()
        {
            /* Condition from example */
            Func<double, double> p = x => 1 / x;
            Func<double, double> q = x => -2;
            Func<double, double> f = x => -2 * Math.Pow(x, 2);

            double start = 0.5;
            double end = 1;
            int N = 6;
            double h = 0.1;

            var answer = ReductionMethod.Solve(p, q, f, start, end, N, 0, 1, 1, 1, 0, 3);

            foreach (var y in answer)
            {
                //Console.WriteLine($"x: {start,3:g4} y: {y,5:g6} exact value: {Math.Pow(start, 2) + 2}");
                start = Math.Round(start + h, 1);
            }

            /* Condition Variant 10 */
            p = x => Math.Pow(x, 2);
            q = x => -2d / Math.Pow(x, 2);
            f = x => 1;

            start = 0.5;
            end = 1;
            N = 6;
            h = 0.1;

            answer = ReductionMethod.Solve(p, q, f, start, end, N, 1, -1, 6, 1, 0, 1);

            foreach (var y in answer)
            {
                Console.WriteLine($"x: {start,5:g1} y: {y,7:g5} exact value: {1 / start,5:g4}");
                start = Math.Round(start + h, 1);
            }
        }

        static void Lab10()
        {
            /* Condition from example */
            Func<double, double> p = x => 1;
            Func<double, double> q = x => -(1 / x);
            Func<double, double> f = x => 2 * x + 4;

            double start = 0;
            double end = 1;
            int N = 11;
            double h = 0.1;

            var answer = GridMethod.Solve(p, q, f, start, end, N, 1, 0, 0, 1, 0, 3);

            foreach (var y in answer)
            {
                //Console.WriteLine($"x: {start,4:g4} y: {y,5:g5} exact value: {2 * Math.Pow(start, 2) + start,5:g5}");
                start += h;
            }

            /* Condition Variant 10 */
            p = x => -Math.Pow(x + 3, 2);
            q = x => -(2 / Math.Pow(x + 3, 2));
            f = x => 3;

            start = 0;
            end = 1;
            N = 11;
            h = 0.1;

            answer = GridMethod.Solve(p, q, f, start, end, N, 1d, -1d, 4/3d, 1, 0, 3/4d);

            foreach (var y in answer)
            {
                Console.WriteLine($"x: {start,4:g4} y: {y,7:g5} exact value: {3 / (start + 3),5:g5}");
                start += h;
            }
        }

        static void Lab11()
        {

            Console.WriteLine();

            Func<double, double> p = x => 0;
            Func<double, double> q = x => 1;
            Func<double, double> f = x => -x;

            double start = 0;
            double end = 1;
            int N = 11;
            double h = 0.1;

            CollocationMethod.Solve(p, q, f, start, end, N, 1, 0, 0, 1, 0, 0);
        }
        #endregion

        #endregion
    }
}
