using System;
using System.Linq;
using BenchmarkDotNet.Running;
using efcore_vs_dapper.Schema;

namespace efcore_vs_dapper
{
    class Program
    {
        static void Main(string[] args)
        {
            //BenchmarkRunner.Run<EfCoreVsDapperBench>();

            var ctx = new BenchContext();
            ctx.Database.EnsureCreated();
            ctx.Persons.Add(new Person(1, "Ivan"));
            ctx.Persons.Add(new Person(2, "Peter"));
            ctx.SaveChanges();
            
            ctx = new BenchContext();
            var person = ctx.Persons.Single(p => p.Id == 1);
            Console.WriteLine(person);
        }
    }
}
