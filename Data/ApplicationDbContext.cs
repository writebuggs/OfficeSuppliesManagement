using Microsoft.EntityFrameworkCore;
using OfficeSuppliesManagement.Models;

namespace OfficeSuppliesManagement.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Inbound> Inbounds { get; set; }
        public DbSet<Outbound> Outbounds { get; set; }
    }
}
