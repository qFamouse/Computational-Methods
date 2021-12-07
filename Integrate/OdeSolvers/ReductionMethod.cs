using MathNet.Numerics.LinearAlgebra;
using System;
using System.Collections.Generic;
using System.Text;

namespace ComputationalMethods.Integrate.OdeSolvers
{
    class ReductionMethod
    {
        /// <summary>
        /// Reduction method for solving the boundary value problem
        /// </summary>
        /// We have this task: u''(x) + p(x)*u'(x) + q(x)*u(x) = f(x)
        /// <param name="p">Function from the task</param>
        /// <param name="q">Function from the task</param>
        /// <param name="f">Function from the task</param>
        /// <param name="start">The beginning of the segment [a,b]</param>
        /// <param name="end">End of the segment [a,b]</param>
        /// <param name="N">Number of partitions</param>
        ///
        /// For example, we have:
        /// u'(0.5) = 1   => 0*u(0.5) + u'(0.5) = 1
        /// u(1)    = 3   => u(1)     + 0*u'(1) = 3
        ///
        /// Then, α1 β1 γ1  => 0 1 1  (We take the numbers from the entry above)
        ///       α2 β2 γ2  => 1 0 3
        /// 
        /// <param name="α1"></param>
        /// <param name="β1"></param>
        /// <param name="γ1"></param>
        /// <param name="α2"></param>
        /// <param name="β2"></param>
        /// <param name="γ2"></param>
        public static double[] Solve(Func<double, double> p, Func<double, double> q, Func<double, double> f,
            double start, double end, int N,
            double α1, double β1, double γ1,
            double α2, double β2, double γ2)
        {
            // Initial data //
            double[] y0 = { 0, 0 };
            double[] y1 = { 1, 0 };
            double[] y2 = { 0, 1 };

            double[] FirstCauchyTask(double x, double[] parameters)
            {
                double y = parameters[0];
                double z = parameters[1];

                return new double[]
                {
                    z,
                    f(x) - q(x) * y - p(x) * z
                };
            }

            double[] SecondCauchyTask(double x, double[] parameters)
            {
                double y = parameters[0];
                double z = parameters[1];

                return new double[]
                {
                    z,
                    -p(x) * z - q(x) * y
                };
            }

            double[] ThirdCauchyTask(double x, double[] parameters)
            {
                double y = parameters[0];
                double z = parameters[1];

                return new double[]
                {
                    z,
                    -p(x) * z - q(x) * y
                };
            }

            // Solving systems using the method RungeKutta //
            var solveY0 = RungeKutta.FourthOrder(y0, start, end, N, FirstCauchyTask);
            var solveY1 = RungeKutta.FourthOrder(y1, start, end, N, SecondCauchyTask);
            var solveY2 = RungeKutta.FourthOrder(y2, start, end, N, ThirdCauchyTask);

            /* Generating a linear system of equations */
            // Auxiliary variables 
            int b = solveY0.GetLength(0) - 1; // last element from array (aka end)

            int idxY = 1; // Index 'Y' column from the solved cauchy problem 
            int idxDy = 2;// Index 'S' (S is derivative of y) column from the solved cauchy problem 

            // Install condition of linear system
            double[,] matrix =
            {
                { α1, β1 },
                { α2 * solveY1[b, 1] + solveY1[b, 2] * β2, α2 * solveY2[b,1] + β2 * solveY2[b, 2]}
            };
            double[] freeMembers = { γ1, γ2 - α2*solveY0[b,1] - β2 * solveY0[b, 2]};

            // Solve system by MathNet library. // -- Also you can you some custom method from LinearSystems folder
            var A = Matrix<double>.Build.DenseOfArray(matrix);
            var B = Vector<double>.Build.Dense(freeMembers);

            var c1c2 = A.Solve(B).ToArray(); // c2, c1

            double c1 = c1c2[0];
            double c2 = c1c2[1];

            // 
            var answer = new double[N];

            for (int i = 0; i < N; i++)
            {
                answer[i] = solveY0[i, idxY] + c1 * solveY1[i, idxY] + c2 * solveY2[i, idxY];
            }

            return answer;
        }
    }
}
