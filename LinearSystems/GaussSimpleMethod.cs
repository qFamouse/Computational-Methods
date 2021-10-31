using System;

namespace ComputationalMethods.LinearSystems
{
    class GaussSimpleMethod
    {
        public static double[] Solve(double[,] matrix, double[] freeMembers)
        {
            // Check for errors
            if (matrix.GetLength(0) != matrix.GetLength(1))
            {
                return null;
            }
            // Create extendedMatrix
            double[,] extendedMatrix;
            if ((extendedMatrix = Matrix.Matrix.AddColumn(matrix, freeMembers)) == null)
            {
                return null;
            }
            // VARIABLES //
            int extendedMatrixRows = extendedMatrix.GetLength(0);
            int extendedMatrixColumns = extendedMatrix.GetLength(1);
            // ALGORITHM //
            // Convert to triangular view //
            for (int i = 0; i < extendedMatrixRows; i++)
            {
                // Check for bad number //
                if (extendedMatrix[i, i] == 0)
                { // If diagonal element is Zero
                    for (int find = 0; find < extendedMatrixRows; find++)
                    { // Find nonZero element and swap
                        if (extendedMatrix[find, i] != 0)
                        {
                            Matrix.Matrix.SwapRows(extendedMatrix, find, i);
                            break;
                        }
                    }
                }
                // Divide Row by Digit
                Matrix.Matrix.DivideRowByDigit(extendedMatrix, i, extendedMatrix[i, i]);
                // Sub Row From Matrix //
                for (int row = i + 1; row < extendedMatrixRows; row++)
                { // Sub current row from all rows in the matrix
                    double firstNumOfRow = extendedMatrix[row, i];
                    for (int column = 0; column < extendedMatrixColumns; column++)
                    { // Sub rows
                        extendedMatrix[row, column] -= (extendedMatrix[i, column] * firstNumOfRow);
                    }
                }
            }
            // Get Answer //
            double[] answer = new double[extendedMatrixRows];
            for (int i = extendedMatrixRows - 1; i >= 0; i--)
            {
                answer[i] = extendedMatrix[i, extendedMatrixRows];
                for (int j = extendedMatrixRows - 1; j > i; j--)
                {
                    answer[i] -= answer[j] * extendedMatrix[i, j];
                }

                answer[i] = Math.Round(answer[i], 3);
            }
            return answer;
        }
    }
}
