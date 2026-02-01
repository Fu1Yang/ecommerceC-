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
        public DbSet<Estimate> Estimate { get; set; }


        //personnaliser le mapping
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Users>().ToTable("Users");
            modelBuilder.Entity<Produits>().ToTable("Produits");
            modelBuilder.Entity<Rdv>().ToTable("Rdvs");
            modelBuilder.Entity<Panier>().ToTable("Paniers");
            modelBuilder.Entity<Tuto>().ToTable("Tuto");
            modelBuilder.Entity<Estimate>().ToTable("Estimate");

            modelBuilder.Entity<Estimate>()
                .Property(e => e.TarifTotal)
                .HasColumnType("decimal(18,2)");
            
            modelBuilder.Entity<Produits>()
                .Property(p => p.Price)
                .HasColumnType("decimal(18,2)");

            base.OnModelCreating(modelBuilder);

        }
        public DbSet<ecommerce.Models.Profiles> Profiles { get; set; } = default!;
    }
}
