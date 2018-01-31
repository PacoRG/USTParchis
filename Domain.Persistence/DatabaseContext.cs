using Domain.Model;
using Domain.Model.Band;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;

namespace Infraestructure.Database
{
    public class DatabaseContext : DbContext 
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options)
          : base(options)
        { }

        public DbSet<Song> Songs { get; set; }

        public DbSet<Sheet> Sheets { get; set; }

        public DbSet<Author> Authors { get; set; }

        public DbSet<Link> Links { get; set; }

        public DbSet<BandComponent> Components { get; set; }

        public DbSet<InstrumentPlay> InstrumentPlays { get; set; }

        public override int SaveChanges()
        {
            return base.SaveChanges();
        }
    }
}
