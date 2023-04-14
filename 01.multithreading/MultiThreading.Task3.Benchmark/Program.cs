using BenchmarkDotNet.Running;

namespace MultiThreading.Task3.Benchmark
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run<MatrixMultiplierBenchmark>();
        }
    }
}