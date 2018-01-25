using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace Infraestructure.Database
{
    public class DatababseContextFactory : IDesignTimeDbContextFactory<DatabaseContext>
    {
        public DatabaseContext CreateDbContext(string[] args)
        {
            var configBuilder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");
            var config = configBuilder.Build();

            var builder = new DbContextOptionsBuilder<DatabaseContext>();

            builder.UseSqlServer(config.GetConnectionString("DatabaseManagement"), sqlOptions =>
                sqlOptions.MigrationsAssembly("Infraestructure.Persistence"));

            return new DatabaseContext(builder.Options);
        }
    }
}
