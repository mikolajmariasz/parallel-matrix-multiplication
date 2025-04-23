using System;
using System.Threading;

namespace parallel_matrix_multiplication
{
    public static class ThreadMatrixMultiplier
    {
        public static Matrix Multiply(Matrix a, Matrix b, int threadCount)
        {
            if (a.Cols != b.Rows)
                throw new ArgumentException("Matrix dimensions are not compatible for multiplication");

            Matrix result = new Matrix(a.Rows, b.Cols);
            Thread[] threads = new Thread[threadCount];
            int rowsPerThread = a.Rows / threadCount;

            for (int t = 0; t < threadCount; t++)
            {
                int startRow = t * rowsPerThread;
                int endRow = (t == threadCount - 1) ? a.Rows : startRow + rowsPerThread;

                threads[t] = new Thread(() =>
                {
                    for (int i = startRow; i < endRow; i++)
                    {
                        for (int j = 0; j < b.Cols; j++)
                        {
                            double sum = 0;
                            for (int k = 0; k < a.Cols; k++)
                            {
                                sum += a[i, k] * b[k, j];
                            }
                            result[i, j] = sum;
                        }
                    }
                });
                threads[t].Start();
            }

            foreach (var thread in threads)
            {
                thread.Join();
            }

            return result;
        }
    }
}