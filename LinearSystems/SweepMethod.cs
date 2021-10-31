using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputationalMethods.LinearSystems
{
    class SweepMethod
    {
        public static double[] Solve(double[,] matrix, double[] freeMembers)
        {
            // VARIABLES //
            double[] A = new double[freeMembers.Length - 1]; // Array with A1, A2 ...
            double[] B = new double[freeMembers.Length - 1]; // Array with B1, B2 ...
            int abLength = A.GetLength(0);
            double[] x = new double[freeMembers.Length];
            // CALC THE FIRST ELEMENTS A & B //
            double c0 = matrix[0, 1]; // first c
            double b0 = matrix[0, 0]; // first c
            double f0 = freeMembers[0]; // first f
            A[0] = -1 * (c0 / b0);
            B[0] = f0 / b0;
            // CALC THE REST ELEMENTS A & B //
            for (int i = 1; i < abLength; i++)
            {
                A[i] = -1 * (matrix[i, i + 1] / (A[i - 1] * matrix[i, i - 1] + matrix[i, i])); // -(c / A*a*b)
                B[i] = (freeMembers[i] - matrix[i, i - 1] * B[i - 1]) / (A[i - 1] * matrix[i, i - 1] + matrix[i, i]); // (f - a*B) / (A*a+b)
            }
            // CALC END ELEMENT X //
            double _f = freeMembers[freeMembers.Length - 1]; // last f
            double _a = matrix[abLength, abLength - 1]; // last a
            double _B = B[abLength - 1]; // last B
            double _b = matrix[abLength, abLength]; // last b
            double _A = A[abLength - 1]; // last A

            int round = 3;

            x[freeMembers.Length - 1] = Math.Round((_f - _a * _B) / (_b + _a * _A), round);
            // CALC THE REST ELEMENTS X //
            for (int i = freeMembers.Length - 2; i >= 0; i--)
            {
                x[i] = Math.Round(A[i] * x[i + 1] + B[i], round);
            }
            // END //
            return x;
        }
    }
}