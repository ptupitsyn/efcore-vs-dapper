using System;
using System.Linq;
using BenchmarkDotNet.Attributes;
using Dapper;
using efcore_vs_dapper.Schema;

namespace efcore_vs_dapper
{
    public class EfCoreVsDapperBench
    {
        [Benchmark]
        public void SelectEntityByIdEf()
        {
            var ctx = new BenchContext();
            var person = ctx.Persons.Single(p => p.Id == 1);
            if (person.Id != 1)
            {
                throw new Exception();
            }
        }
        
        [Benchmark]
        public void SelectEntityByIdDapper()
        {
            var person = BenchContext.Connection.QuerySingle<Person>(
                "SELECT * FROM Persons WHERE ID = @id", new {id = 1});
            if (person.Id != 1)
            {
                throw new Exception();
            }
        }
        
//        [Benchmark]
//        public void SelectFieldByIdEf()
//        {
//            
//        }
//        
//        [Benchmark]
//        public void SelectFieldByIdDapper()
//        {
//            
//        }
    }
}