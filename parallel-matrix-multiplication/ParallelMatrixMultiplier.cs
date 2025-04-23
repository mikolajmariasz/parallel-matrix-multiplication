using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace parallel_matrix_multiplication
{
    public class ParallelMatrixMultiplier
    {
        public static Matrix Multiply(Matrix a, Matrix b, int maxThreads = 1)
        {
            if (a.Cols != b.Rows)
                throw new ArgumentException("Invalid matrix dimensions for multiplication");

            Matrix result = new Matrix(a.Rows, b.Cols);
            var options = new ParallelOptions { MaxDegreeOfParallelism = maxThreads };

            Parallel.For(0, a.Rows, options, i => // dzielenie wierszy macierzy między dostępne wątki
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
            });

            return result;
        }
    }
}
