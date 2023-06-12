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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CategoryFeature>().HasKey(sc => new { sc.FeatureID, sc.CategoryID });

        }

        public DbSet<UNITINS_DoisIrmaos.Models.Vehicle> Vehicle { get; set; }
    }
}
