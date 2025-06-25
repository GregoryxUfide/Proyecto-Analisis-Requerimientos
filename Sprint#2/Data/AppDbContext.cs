using Microsoft.EntityFrameworkCore;
using Sprint_2.Models;

namespace Sprint_2.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Rol> Roles { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuraciones adicionales si deseas
            base.OnModelCreating(modelBuilder);

            // Nombre de tablas explícito (opcional)
            modelBuilder.Entity<Usuario>().ToTable("Usuarios");
            modelBuilder.Entity<Rol>().ToTable("Roles");

            // Relación entre Usuario y Rol (opcional si usas convenciones)
            modelBuilder.Entity<Usuario>()
                .HasOne(u => u.Rol)
                .WithMany(r => r.Usuarios)
                .HasForeignKey(u => u.RolId);

        }
    }
}