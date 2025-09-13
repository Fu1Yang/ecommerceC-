using ecommerce.Models;
using Microsoft.EntityFrameworkCore;

namespace ecommerce.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) 
        {
            
        }
        // Déclare chaque table comme DbSet
        public DbSet<Users> Users { get; set; }
        public DbSet<Produits> Produits { get; set; }
        public DbSet<Rdv> Rdvs { get; set; }


        // Optionnel : personnaliser le mapping
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Users>().ToTable("Users");
            modelBuilder.Entity<Produits>().ToTable("Produits");
            modelBuilder.Entity<Rdv>().ToTable("Rdvs");
        }
    }
}
