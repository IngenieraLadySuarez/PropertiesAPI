using Microsoft.EntityFrameworkCore;

namespace PropertiesAPI.Models
{
    public class PropertiesContext : DbContext
    {
        public PropertiesContext(DbContextOptions<PropertiesContext> options) : base(options)
        {
        }

        public DbSet<Owner> Owner { get; set; }

        public DbSet<Property> Property { get; set; }

        public DbSet<PropertyImage> PropertyImage { get; set; }

        public DbSet<PropertyTrace> PropertyTrace { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Owner>().HasKey(o => o.Name);
            modelBuilder.Entity<Property>().HasKey(o => o.IdProperty);
            modelBuilder.Entity<PropertyImage>().HasKey(o => o.file);
            modelBuilder.Entity<PropertyTrace>().HasKey(o => o.Name);
        }
    }
}
