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
        public DbSet<Panier> Paniers { get; set; }

        public DbSet<Tuto> Tuto { get; set; }


        //personnaliser le mapping
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Users>().ToTable("Users");
            modelBuilder.Entity<Produits>().ToTable("Produits");
            modelBuilder.Entity<Rdv>().ToTable("Rdvs");
            modelBuilder.Entity<Panier>().ToTable("Paniers");
            modelBuilder.Entity<Tuto>().ToTable("Tuto");

        }
        public DbSet<ecommerce.Models.Profiles> Profiles { get; set; } = default!;
    }
}
