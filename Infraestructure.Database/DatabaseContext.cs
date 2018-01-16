using Domain.Model;
using DomainServices.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Database
{
    public class DatabaseContext : DbContext , IDatabaseContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options)
          : base(options)
        { }

        public DbSet<March> Marchs { get; set; }

        public DbSet<Sheet> Sheets { get; set; }

        public override int SaveChanges()
        {
            return base.SaveChanges();
        }
    }
}
