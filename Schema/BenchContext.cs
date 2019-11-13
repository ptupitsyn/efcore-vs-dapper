using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace efcore_vs_dapper.Schema
{
    public class BenchContext : DbContext
    {
        public BenchContext(DbContextOptions options) : base(GetOptions())
        {
            // No-op.
        }
        
        public DbSet<Person> Persons => Set<Person>();

        private static DbContextOptions<BenchContext> GetOptions()
        {
            return new DbContextOptionsBuilder<BenchContext>()
                .UseSqlite(GetInMemoryOpenSqliteConnection())
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