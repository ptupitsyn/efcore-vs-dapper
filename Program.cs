using BenchmarkDotNet.Running;
using efcore_vs_dapper.Schema;

namespace efcore_vs_dapper
{
    class Program
    {
        static void Main(string[] args)
        {
            // Init
            var ctx = new BenchContext();
            ctx.Database.EnsureCreated();
            ctx.Persons.Add(new Person(1, "Ivan"));
            ctx.Persons.Add(new Person(2, "Peter"));
            ctx.SaveChanges();
            
            // Bench
            BenchmarkRunner.Run<EfCoreVsDapperBench>();
        }
    }
}
