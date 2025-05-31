using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Entities.Entities;

public partial class GranHotelDesamparadosContext : DbContext
{
    public GranHotelDesamparadosContext()
    {
    }

    public GranHotelDesamparadosContext(DbContextOptions<GranHotelDesamparadosContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Empleado> Empleados { get; set; }

    public virtual DbSet<Habitacione> Habitaciones { get; set; }

    public virtual DbSet<LimpiezaHabitacione> LimpiezaHabitaciones { get; set; }

    public virtual DbSet<Producto> Productos { get; set; }

    public virtual DbSet<Reserva> Reservas { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<UbicacionHabitacione> UbicacionHabitaciones { get; set; }

    public virtual DbSet<UbicacionProducto> UbicacionProductos { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    public virtual DbSet<Ventum> Venta { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Empleado>(entity =>
        {
            entity.HasKey(e => e.IdEmpleado).HasName("PK__Empleado__CE6D8B9E81CC9C37");

            entity.ToTable("Empleado");

            entity.Property(e => e.IdEmpleado).ValueGeneratedNever();
            entity.Property(e => e.PuestoEmpleado)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.SalarioEmpleado).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.IdEmpleadoNavigation).WithOne(p => p.Empleado)
                .HasForeignKey<Empleado>(d => d.IdEmpleado)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Empleado__IdEmpl__3F466844");

            entity.HasOne(d => d.IdRolNavigation).WithMany(p => p.Empleados)
                .HasForeignKey(d => d.IdRol)
                .HasConstraintName("FK__Empleado__IdRol__403A8C7D");
        });

        modelBuilder.Entity<Habitacione>(entity =>
        {
            entity.HasKey(e => e.IdHabitacion).HasName("PK__Habitaci__8BBBF90118A565A6");

            entity.Property(e => e.IdHabitacion).ValueGeneratedNever();
            entity.Property(e => e.DescripcionHabitacion)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.NombreHabitacion)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.PrecioHabitacion).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.IdProductoNavigation).WithMany(p => p.Habitaciones)
                .HasForeignKey(d => d.IdProducto)
                .HasConstraintName("FK__Habitacio__IdPro__70DDC3D8");

            entity.HasOne(d => d.IdUbicacionHabitacionNavigation).WithMany(p => p.Habitaciones)
                .HasForeignKey(d => d.IdUbicacionHabitacion)
                .HasConstraintName("FK__Habitacio__IdUbi__6FE99F9F");
        });

        modelBuilder.Entity<LimpiezaHabitacione>(entity =>
        {
            entity.HasKey(e => e.IdLimpiezaHabitacion).HasName("PK__Limpieza__CFFB3464307D4001");

            entity.Property(e => e.IdLimpiezaHabitacion).ValueGeneratedNever();
            entity.Property(e => e.ComentariosLimpiezaHabitacion)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.FechaLimpiezaHabitacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.IdEmpleadoNavigation).WithMany(p => p.LimpiezaHabitaciones)
                .HasForeignKey(d => d.IdEmpleado)
                .HasConstraintName("FK__LimpiezaH__IdEmp__75A278F5");

            entity.HasOne(d => d.IdHabitacionNavigation).WithMany(p => p.LimpiezaHabitaciones)
                .HasForeignKey(d => d.IdHabitacion)
                .HasConstraintName("FK__LimpiezaH__IdHab__74AE54BC");
        });

        modelBuilder.Entity<Producto>(entity =>
        {
            entity.HasKey(e => e.IdProducto).HasName("PK__Producto__098892107D595501");

            entity.ToTable("Producto");

            entity.Property(e => e.IdProducto).ValueGeneratedNever();
            entity.Property(e => e.DescripcionProducto)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.MarcaProducto)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.NombreProducto)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.IdUbicacionProductoNavigation).WithMany(p => p.Productos)
                .HasForeignKey(d => d.IdUbicacionProducto)
                .HasConstraintName("FK__Producto__IdUbic__52593CB8");
        });

        modelBuilder.Entity<Reserva>(entity =>
        {
            entity.HasKey(e => e.IdReserva).HasName("PK__Reserva__0E49C69DF4F9F9B6");

            entity.ToTable("Reserva");

            entity.Property(e => e.IdReserva).ValueGeneratedNever();

            entity.HasOne(d => d.IdHabitacionNavigation).WithMany(p => p.Reservas)
                .HasForeignKey(d => d.IdHabitacion)
                .HasConstraintName("FK__Reserva__IdHabit__797309D9");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Reservas)
                .HasForeignKey(d => d.IdUsuario)
                .HasConstraintName("FK__Reserva__IdUsuar__787EE5A0");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.IdRol).HasName("PK__Roles__2A49584CF23723BC");

            entity.Property(e => e.IdRol).ValueGeneratedNever();
            entity.Property(e => e.DescripcionRol)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.NombreRol)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<UbicacionHabitacione>(entity =>
        {
            entity.HasKey(e => e.IdUbicacionHabitacion).HasName("PK__Ubicacio__F8F04B2B942FCD96");

            entity.Property(e => e.IdUbicacionHabitacion).ValueGeneratedNever();
            entity.Property(e => e.DescripcionUbicacionHabitacion)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.NombreUbicacionHabitacion)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<UbicacionProducto>(entity =>
        {
            entity.HasKey(e => e.IdUbicacionProducto).HasName("PK__Ubicacio__323AE4A9DBA0CF26");

            entity.ToTable("UbicacionProducto");

            entity.Property(e => e.IdUbicacionProducto).ValueGeneratedNever();
            entity.Property(e => e.DescripcionUbicacionProducto)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.NombreUbicacionProducto)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("PK__Usuario__5B65BF973C47CAC2");

            entity.ToTable("Usuario");

            entity.Property(e => e.IdUsuario).ValueGeneratedNever();
            entity.Property(e => e.ContrasenaUsuario)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.CorreoUsuario)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.IdentificadorUsuario)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.NombreUsuario)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasMany(d => d.IdRols).WithMany(p => p.IdUsuarios)
                .UsingEntity<Dictionary<string, object>>(
                    "RolesUsuario",
                    r => r.HasOne<Role>().WithMany()
                        .HasForeignKey("IdRol")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__RolesUsua__IdRol__3C69FB99"),
                    l => l.HasOne<Usuario>().WithMany()
                        .HasForeignKey("IdUsuario")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__RolesUsua__IdUsu__3B75D760"),
                    j =>
                    {
                        j.HasKey("IdUsuario", "IdRol").HasName("PK__RolesUsu__89C12A1316D1657F");
                        j.ToTable("RolesUsuarios");
                    });
        });

        modelBuilder.Entity<Ventum>(entity =>
        {
            entity.HasKey(e => e.IdVenta).HasName("PK__Venta__BC1240BD29340A55");

            entity.Property(e => e.IdVenta).ValueGeneratedNever();
            entity.Property(e => e.DescripcionVenta)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.FechaVenta)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.MetodoPagoVenta)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.MontoDescuento).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.MontoVenta).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.IdEmpleadoNavigation).WithMany(p => p.Venta)
                .HasForeignKey(d => d.IdEmpleado)
                .HasConstraintName("FK__Venta__IdEmplead__04E4BC85");

            entity.HasOne(d => d.IdReservaNavigation).WithMany(p => p.Venta)
                .HasForeignKey(d => d.IdReserva)
                .HasConstraintName("FK__Venta__IdReserva__03F0984C");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Venta)
                .HasForeignKey(d => d.IdUsuario)
                .HasConstraintName("FK__Venta__IdUsuario__02FC7413");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
