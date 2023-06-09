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
        public DbSet<Client> Clients { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
        }
    }
}
