using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace parallel_matrix_multiplication
{
    class Program
    {
        static void Main(string[] args)
        {
            
            int[] matrixSizes = { 300, 600, 900 };
            int[] threadCounts = { 2, 4, 8 };   
            int repetitions = 10;                   

            var random = new Random();

            Console.WriteLine("Prallel Matrix Multiplication Benchmark");
            foreach (int size in matrixSizes)
            {
                Console.WriteLine($"\nMatrix size: {size}x{size}");
                Console.WriteLine("---------------------------------");

                // matrix generation
                Matrix a = Matrix.GenerateRandom(size, size, random);
                Matrix b = Matrix.GenerateRandom(size, size, random);

                // sequence test (1)
                long sequentialTime = 0;
                for (int i = 0; i < repetitions; i++)
                {
                    var sw = Stopwatch.StartNew();
                    Matrix result = ParallelMatrixMultiplier.Multiply(a, b, 1);
                    sw.Stop();
                    sequentialTime += sw.ElapsedMilliseconds;
                }
                sequentialTime /= repetitions;
                Console.WriteLine($"Sequential (1 thread): {sequentialTime} ms");

                // parallel test 
                foreach (int threads in threadCounts)
                {            
                    long parallelTime = 0;
                    for (int i = 0; i < repetitions; i++)
                    {
                        var sw = Stopwatch.StartNew();
                        Matrix result = ParallelMatrixMultiplier.Multiply(a, b, threads);
                        sw.Stop();
                        parallelTime += sw.ElapsedMilliseconds;
                    }
                    parallelTime /= repetitions;

                    double speedup = (double)sequentialTime / parallelTime;
                    Console.WriteLine($"Parallel ({threads} threads): {parallelTime} ms | Speedup: {speedup:F2}x");
                }

                // threads test 
                foreach (int threads in threadCounts)
                {
                    long time = 0;
                    for (int i = 0; i < repetitions; i++)
                    {
                        var sw = Stopwatch.StartNew();
                        Matrix result = ThreadMatrixMultiplier.Multiply(a, b, threads);
                        sw.Stop();
                        time += sw.ElapsedMilliseconds;
                    }
                    time /= repetitions;
                    double speedup = (double)sequentialTime / time;
                    Console.WriteLine($"Threads ({threads} threads):  {time} ms | Speedup: {speedup:F2}x");
                }
            }

            Console.WriteLine("\nBenchmark completed.");
            Console.ReadKey();
        }
    }
}