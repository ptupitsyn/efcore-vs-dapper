using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace efcore_vs_dapper.Schema
{
    public class BenchContext : DbContext
    {
        private static readonly SqliteConnection Connection = GetInMemoryOpenSqliteConnection(); 
        
        public BenchContext() : base(GetOptions())
        {
            // No-op.
        }
        
        public DbSet<Person> Persons => Set<Person>();

        private static DbContextOptions<BenchContext> GetOptions()
        {
            return new DbContextOptionsBuilder<BenchContext>()
                .UseSqlite(Connection)
                .Options;
        }
        
        private static SqliteConnection GetInMemoryOpenSqliteConnection()
        {
            var connection = new SqliteConnection("Data Source=:memory:;");
            connection.Open();
            return connection;
        }
    }
}