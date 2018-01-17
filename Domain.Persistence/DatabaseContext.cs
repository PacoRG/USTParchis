using Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Database
{
    public class DatabaseContext : DbContext 
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options)
          : base(options)
        { }

        public DbSet<Game> Games { get; set; }

        public override int SaveChanges()
        {
            return base.SaveChanges();
        }
    }
}
