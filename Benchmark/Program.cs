using BenchmarkDotNet.Running;
using System.Reflection;

namespace Benchmark
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            BenchmarkRunner.Run<GrpcVsRestBenchmark>();
        }
    }
}
