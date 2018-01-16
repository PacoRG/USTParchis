using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System.Reflection;

namespace Infraestructure.Database
{
    public class DatababseContextFactory : IDesignTimeDbContextFactory<DatabaseContext>
    {
        public DatabaseContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<DatabaseContext>();

            builder.UseSqlServer("Server=\\SQLExpress;Database=efmigrations2017;Trusted_Connection=True;MultipleActiveResultSets=true",
                optionsBuilder => optionsBuilder.MigrationsAssembly(typeof(DatabaseContext).GetTypeInfo().Assembly.GetName().Name));

            return new DatabaseContext(builder.Options);
        }
    }
}
