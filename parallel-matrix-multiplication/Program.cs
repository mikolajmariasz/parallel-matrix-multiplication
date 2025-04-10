using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace parallel_matrix_multiplication
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Matrix Multiplication Benchmark");

            
            int[] matrixSizes = { 100, 200, 300 };
            int[] threadCounts = { 2, 4, 8, 16,  };   
            int repetitions = 30;                   

            var random = new Random();

            foreach (int size in matrixSizes)
            {
                Console.WriteLine($"\nMatrix size: {size}x{size}");
                Console.WriteLine("---------------------------------");

                // matrix generation
                Matrix a = Matrix.GenerateRandom(size, size, random);
                Matrix b = Matrix.GenerateRandom(size, size, random);

                // sequence test (1 thread)
                long sequentialTime = 0;
                for (int i = 0; i < repetitions; i++)
                {
                    var sw = Stopwatch.StartNew();
                    Matrix result = MatrixMultiplier.Multiply(a, b, 1);
                    sw.Stop();
                    sequentialTime += sw.ElapsedMilliseconds;
                }
                sequentialTime /= repetitions;
                Console.WriteLine($"Sequential (1 thread): {sequentialTime} ms");

                // parrarel thread
                foreach (int threads in threadCounts)
                {            
                    long parallelTime = 0;
                    for (int i = 0; i < repetitions; i++)
                    {
                        var sw = Stopwatch.StartNew();
                        Matrix result = MatrixMultiplier.Multiply(a, b, threads);
                        sw.Stop();
                        parallelTime += sw.ElapsedMilliseconds;
                    }
                    parallelTime /= repetitions;

                    double speedup = (double)sequentialTime / parallelTime;
                    Console.WriteLine($"Parallel ({threads} threads): {parallelTime} ms | Speedup: {speedup:F2}x");
                }
            }

            
            Console.WriteLine("\nVerification for small matrices (3x3):");
            Matrix smallA = Matrix.GenerateRandom(3, 3, random);
            Matrix smallB = Matrix.GenerateRandom(3, 3, random);

            Console.WriteLine("\nMatrix A:");
            smallA.Print();

            Console.WriteLine("\nMatrix B:");
            smallB.Print();

            Console.WriteLine("\nResult (1 thread):");
            Matrix result1 = MatrixMultiplier.Multiply(smallA, smallB, 1);
            result1.Print();

            Console.WriteLine("\nResult (4 threads):");
            Matrix result4 = MatrixMultiplier.Multiply(smallA, smallB, 4);
            result4.Print();

            Console.WriteLine("\nBenchmark completed. Press any key to exit...");
            Console.ReadKey();
        }
    }
}