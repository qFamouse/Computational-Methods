using System;

namespace ComputationalMethods.Matrix
{
    class Matrix
    {
        // Output matrix
        public static void Write2D(double[,] matrix)
        {
            int rows = matrix.GetLength(0);
            int columns = matrix.GetLength(1);
            for (int row = 0; row < rows; row++)
            {
                for (int column = 0; column < columns; column++)
                {
                    Console.Write(matrix[row, column] + "\t");
                }
                Console.WriteLine();
            }
        }
        public static void Write2D(double[] matrix)
        {
            for (int i = 0; i < matrix.Length; i++)
            {
                Console.Write(matrix[i] + "\t");
            }
            Console.WriteLine();
        }
        public static void Writeln2D(double[,] matrix)
        {
            Write2D(matrix);
            Console.WriteLine();
        }
        // MATRIX OPERATIONS //
        // Divide Row by number
        public static void DivideRowByDigit(double[,] matrix, int rowIndex, double divisor)
        {
            for (int column = 0; column < matrix.GetLength(1); column++)
            {
                matrix[rowIndex, column] /= divisor;
            }
        }
        // Subtract Digit By Row
        public static void SubtractDigitByRow(double[,] matrix, int rowIndex, double subtrahend)
        {
            for (int column = 0; column < matrix.GetLength(1); column++)
            {
                matrix[rowIndex, column] -= subtrahend;
            }
        }
        // Swap Rows by index
        public static void SwapRows(double[,] matrix, int indexRow1, int indexRow2)
        {
            int columns = matrix.GetLength(1); // Get count columns
            double temp; // Buffer variable
            for (int column = 0; column < columns; column++)
            {
                temp = matrix[indexRow1, column];
                matrix[indexRow1, column] = matrix[indexRow2, column];
                matrix[indexRow2, column] = temp;
            }
        }
        // Caclucate Determinant
        public static double CaclDeterminant(double[,] inputMatrix)
        {
            // VARIABLES //
            // Matrix //
            double[,] workMatrix = (double[,])inputMatrix.Clone(); // Clone input matrix (because, we changing it)
            int matrixRows = workMatrix.GetLength(0); // Ger count of Matrix Rows
                                                      // Variables for searhing max value in column //
            double maxValue; // max value 
            int maxValueRowIndex; // index of max value
                                  // Other //
            int multiplier = 1; // If we swap rows we should *-1 (for Calculating Determinant)
            double temp; // Variable for convert to triangular view
            // ALGORITHM //
            for (int k = 0; k < matrixRows; k++)
            {
                // Get Max Element In Column //
                maxValue = Math.Abs(workMatrix[k, k]); // take diagonal element as max
                maxValueRowIndex = k; // And index
                for (int row = k + 1; row < matrixRows; row++)
                { // finder
                    if (Math.Abs(workMatrix[row, k]) > maxValue)
                    {
                        maxValue = Math.Abs(workMatrix[row, k]);
                        maxValueRowIndex = row;
                    }
                }
                // Swap Rows if it need //
                if (k != maxValueRowIndex)
                {
                    Matrix.SwapRows(workMatrix, k, maxValueRowIndex);
                    multiplier *= -1; // because we swap rows
                }
                maxValue = workMatrix[k, k]; // save maxValue witout ABS!!
                // Zeroing element below the main diagonal // 
                for (int row = k + 1; row < matrixRows; row++)
                {
                    temp = workMatrix[row, k];
                    for (int column = 0; column < matrixRows; column++)
                    {
                        double mult = temp / maxValue;
                        workMatrix[row, column] -= workMatrix[k, column] * mult;
                    }
                }
            }
            // TAKE ANSWER //
            double det = 1; // variable with determinant
            for (int k = 0; k < matrixRows; k++)
            {
                det *= workMatrix[k, k]; // myltiply the main diagonal
            }
            return det * multiplier;
        }
        // OTHER //
        // Return extended matrix | If Fail returned null
        public static double[,] AddColumn(double[,] matrix, double[] array)
        { // Add new column 'array' in matrix
            // Check for Errors
            if (matrix.GetLength(0) != array.Length)
            {
                return null;
            }
            // Variables
            int matrixRows = matrix.GetLength(0); // Get matrix rows
            int matrixColumns = matrix.GetLength(1); // Get matrix columns

            double[,] newMatrix = new double[matrixRows, matrixColumns + 1];
            // Algorithm
            for (int row = 0; row < matrixRows; row++)
            {
                for (int column = 0; column < matrixColumns; column++)
                {
                    newMatrix[row, column] = matrix[row, column];
                }
                newMatrix[row, matrixColumns] = array[row]; // matrixColumns is point to new column
            }
            return newMatrix;
        }
        public static double[,] AddColumn(double[,] fisrtMatrix, double[,] secondMatrix)
        { // Add new column 'array' in matrix
            // Check for Errors
            if (fisrtMatrix.GetLength(0) != secondMatrix.GetLength(0))
            {
                return null;
            }
            // Variables //
            int newMatrixRows = fisrtMatrix.GetLength(0); // Get matrix rows (firstRows == secondRows)

            int firstMatrixColumns = fisrtMatrix.GetLength(1);
            int secondMatrixColumns = fisrtMatrix.GetLength(1);

            int matrixColumns = firstMatrixColumns + secondMatrixColumns; // Get matrix columns

            double[,] newMatrix = new double[newMatrixRows, matrixColumns];
            // Algorithm //
            for (int row = 0; row < newMatrixRows; row++)
            {
                for (int firstColumn = 0; firstColumn < firstMatrixColumns; firstColumn++)
                {
                    newMatrix[row, firstColumn] = fisrtMatrix[row, firstColumn];
                }

                for (int secondColumn = firstMatrixColumns; secondColumn < matrixColumns; secondColumn++)
                {
                    newMatrix[row, secondColumn] = secondMatrix[row, secondColumn - firstMatrixColumns];
                }
            }
            return newMatrix;
        }
        // Solve Determinant
        public static bool GetDeterminant(double[,] matrix, double det)
        {
            // If not a square matrix
            if (matrix.GetLength(0) != matrix.GetLength(1))
            {
                return false;
            }
            // Special case - length == 2
            if (matrix.GetLength(0) == 2)
            {
                det = matrix[0, 0] * matrix[1, 1] - matrix[0, 1] * matrix[1, 0];
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
