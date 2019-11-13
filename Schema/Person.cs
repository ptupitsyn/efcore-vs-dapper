// ReSharper disable AutoPropertyCanBeMadeGetOnly.Local
namespace efcore_vs_dapper.Schema
{
    public class Person
    {
        public Person(long id, string name)
        {
            Id = id;
            Name = name;
        }

        public long Id { get; private set; }
        
        public string Name { get; private set; }

        public override string ToString()
        {
            return $"{nameof(Id)}: {Id}, {nameof(Name)}: {Name}";
        }
    }
}