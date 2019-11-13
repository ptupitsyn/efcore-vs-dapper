using System;
using System.Linq;
using BenchmarkDotNet.Attributes;
using Dapper;
using efcore_vs_dapper.Schema;
using Microsoft.EntityFrameworkCore;

namespace efcore_vs_dapper
{
    public class EfCoreVsDapperBench
    {
        private static readonly Func<BenchContext, Person> CompiledPersonQuery = 
            EF.CompileQuery((BenchContext ctx) => ctx.Persons.Single(p => p.Id == 1));
        
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
        public void SelectEntityByIdEfCompiled()
        {
            var person = CompiledPersonQuery(new BenchContext());
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