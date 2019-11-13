﻿using System;
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

        private readonly BenchContext _context;

        public EfCoreVsDapperBench()
        {
            _context = new BenchContext();
            _context.Database.EnsureCreated();
            _context.Persons.Add(new Person(1, "Ivan"));
            _context.Persons.Add(new Person(2, "Peter"));
            _context.SaveChanges();
        }

        [Benchmark]
        public void SelectEntityByIdEf()
        {
            var person = _context.Persons.Single(p => p.Id == 1);
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
        
        private static void VerifyPerson(Person person)
        {
            if (person.Id != 1)
            {
                throw new Exception(person.Name);
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