using BenchmarkDotNet.Running;

namespace efcore_vs_dapper
{
    class Program
    {
        static void Main(string[] args)
        {
            BenchmarkRunner.Run<EfCoreVsDapperBench>();
        }
    }
}
