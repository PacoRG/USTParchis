using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Database
{
    public class DatabaseContext : DbContext 
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options)
          : base(options)
        { }

        public override int SaveChanges()
        {
            return base.SaveChanges();
        }
    }
}
