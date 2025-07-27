// Arquivo: AppDBContext.cs
using Care3._0.Models;
using Microsoft.EntityFrameworkCore;

namespace Care3._0.Data
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        {
        }

        public DbSet<Medicament> Medicaments { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
