using BenchmarkDotNet.Attributes;

namespace efcore_vs_dapper
{
    public class EfCoreVsDapperBench
    {
        [Benchmark]
        public void SelectEntityByIdEf()
        {
            
        }
        
        [Benchmark]
        public void SelectEntityByIdDapper()
        {
            
        }
        
        [Benchmark]
        public void SelectFieldByIdEf()
        {
            
        }
        
        [Benchmark]
        public void SelectFieldByIdDapper()
        {
            
        }
    }
}