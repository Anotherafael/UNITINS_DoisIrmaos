using Microsoft.EntityFrameworkCore;
using UNITINS_DoisIrmaos.Models;

namespace UNITINS_DoisIrmaos.DAL
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options)
            : base(options)
        {

        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Feature> Features { get; set; }
        public DbSet<CategoryFeature> CategoryFeatures { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Protection> Protections { get; set; }
        public DbSet<Acessory> Acessories { get; set; }
        public DbSet<Tax> Taxes { get; set; }
        public DbSet<Employee> Personnel { get; set; }
        public DbSet<Client> Clients { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CategoryFeature>().HasKey(sc => new { sc.FeatureID, sc.CategoryID });

            modelBuilder.Entity<Client>().Property(c => c.Password).IsRequired(false);
            modelBuilder.Entity<Client>().Property(c => c.ConfirmPassword).IsRequired(false);
            modelBuilder.Entity<Client>().Property(c => c.Address).IsRequired(false);
        }

        public DbSet<UNITINS_DoisIrmaos.Models.Vehicle> Vehicle { get; set; }
    }
}
