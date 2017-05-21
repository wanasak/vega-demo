using Microsoft.EntityFrameworkCore;
using vega_demo.Models;

namespace vega_demo.Persistence
{
    public class VegaDbContext : DbContext
    {
        public DbSet<Make> Makes { get; set; }
        public DbSet<Model> Models { get; set; }
        
        public VegaDbContext(DbContextOptions<VegaDbContext> options) : base(options)
        {
            
        }
    }
}