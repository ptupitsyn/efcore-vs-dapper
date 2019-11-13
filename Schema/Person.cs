// ReSharper disable AutoPropertyCanBeMadeGetOnly.Local
namespace efcore_vs_dapper.Schema
{
    public class Person
    {
        public long Id { get; set; }
        
        public string Name { get; set; }

        public override string ToString()
        {
            return $"{nameof(Id)}: {Id}, {nameof(Name)}: {Name}";
        }
    }
}