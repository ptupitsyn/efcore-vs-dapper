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
            EF.CompileQuery((BenchContext ctx) => ctx.Persons.AsNoTracking().Single(p => p.Id == 1));

        private static readonly Func<BenchContext, long, string> CompiledNameQuery =
            EF.CompileQuery((BenchContext ctx, long id) =>
                ctx.Persons.Where(p => p.Id == id).Select(p => p.Name).Single());

        private readonly BenchContext _context;

        public EfCoreVsDapperBench()
        {
            _context = new BenchContext();
            _context.Database.EnsureCreated();
            _context.Persons.Add(new Person {Id = 1, Name = "Ivan"});
            _context.Persons.Add(new Person {Id = 2, Name = "Peter"});
            _context.SaveChanges();
        }

        [Benchmark]
        public void SelectEntityByIdEf()
        {
            var person = _context.Persons.AsNoTracking().Single(p => p.Id == 1);
            VerifyPerson(person);
        }

        [Benchmark]
        public void SelectEntityByIdEfCompiled()
        {
            var person = CompiledPersonQuery(_context);
            VerifyPerson(person);
        }

        [Benchmark]
        public void SelectEntityByIdDapper()
        {
            var person = BenchContext.Connection.QuerySingle<Person>(
                "SELECT * FROM Persons WHERE ID = @id", new {id = 1});
            VerifyPerson(person);
        }

        [Benchmark]
        public void SelectFieldByIdEf()
        {
            var name = _context.Persons.Where(p => p.Id == 2).Select(p => p.Name).Single();
            VerifyName(name);
        }

        [Benchmark]
        public void SelectFieldByIdEfCompiled()
        {
            var name = CompiledNameQuery(_context, 2);
            VerifyName(name);
        }

        [Benchmark]
        public void SelectFieldByIdDapper()
        {
            var name = BenchContext.Connection.QuerySingle<string>(
                "SELECT name FROM Persons WHERE ID = @id", new {id = 2});
            VerifyName(name);
        }

        private static void VerifyPerson(Person person)
        {
            if (person.Id != 1)
            {
                throw new Exception(person.Name);
            }
        }

        private static void VerifyName(string name)
        {
            if (name != "Peter")
            {
                throw new Exception(name);
            }
        }
    }
}