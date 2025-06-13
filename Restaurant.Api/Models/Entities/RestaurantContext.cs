using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace Restaurant.Api.Models.Entities;

public partial class RestaurantContext : DbContext
{
    public RestaurantContext()
    {
    }

    public RestaurantContext(DbContextOptions<RestaurantContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Categoria> Categoria { get; set; }

    public virtual DbSet<Mesa> Mesa { get; set; }

    public virtual DbSet<Producto> Producto { get; set; }

    public virtual DbSet<Sesion> Sesion { get; set; }

    public virtual DbSet<Ticket> Ticket { get; set; }

    public virtual DbSet<TicketItem> TicketItem { get; set; }

    public virtual DbSet<Usuario> Usuario { get; set; }

    public virtual DbSet<Variante> Variante { get; set; }

    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb3_general_ci")
            .HasCharSet("utf8mb3");

        modelBuilder.Entity<Categoria>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("categoria");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Mesa>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("mesa");

            entity.HasIndex(e => e.Numero, "numero").IsUnique();

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.Disponible)
                .HasDefaultValueSql("'1'")
                .HasColumnName("disponible");
            entity.Property(e => e.Numero)
                .HasColumnType("int(11)")
                .HasColumnName("numero");
        });

        modelBuilder.Entity<Producto>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("producto");

            entity.HasIndex(e => e.CategoriaId, "producto_categoria_FK");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.ACocina).HasColumnName("aCocina");
            entity.Property(e => e.Activo)
                .HasDefaultValueSql("'1'")
                .HasColumnName("activo");
            entity.Property(e => e.CategoriaId)
                .HasColumnType("int(11)")
                .HasColumnName("categoriaId");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .HasColumnName("nombre");
            entity.Property(e => e.PrecioBase)
                .HasPrecision(8, 2)
                .HasColumnName("precioBase");

            entity.HasOne(d => d.Categoria).WithMany(p => p.Producto)
                .HasForeignKey(d => d.CategoriaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("producto_categoria_FK");
        });

        modelBuilder.Entity<Sesion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("sesion");

            entity.HasIndex(e => e.UsuarioId, "sesion_usuario_FK");

            entity.Property(e => e.Id).HasColumnType("int(11)");
            entity.Property(e => e.Hash).HasMaxLength(64);
            entity.Property(e => e.UsuarioId).HasColumnType("int(11)");

            entity.HasOne(d => d.Usuario).WithMany(p => p.Sesion)
                .HasForeignKey(d => d.UsuarioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("sesion_usuario_FK");
        });

        modelBuilder.Entity<Ticket>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("ticket");

            entity.HasIndex(e => e.MesaId, "ticket_mesa_FK");

            entity.HasIndex(e => e.MeseroId, "ticket_usuario_FK");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.Estado)
                .HasDefaultValueSql("'abierto'")
                .HasColumnType("enum('abierto','cerrado')")
                .HasColumnName("estado");
            entity.Property(e => e.MesaId)
                .HasColumnType("int(11)")
                .HasColumnName("mesaId");
            entity.Property(e => e.MeseroId)
                .HasColumnType("int(11)")
                .HasColumnName("meseroId");
            entity.Property(e => e.Total)
                .HasPrecision(10, 2)
                .HasDefaultValueSql("'0.00'")
                .HasColumnName("total");

            entity.HasOne(d => d.Mesa).WithMany(p => p.Ticket)
                .HasForeignKey(d => d.MesaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ticket_mesa_FK");

            entity.HasOne(d => d.Mesero).WithMany(p => p.Ticket)
                .HasForeignKey(d => d.MeseroId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ticket_usuario_FK");
        });

        modelBuilder.Entity<TicketItem>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("ticket_item");

            entity.HasIndex(e => e.ProductoId, "ticket_item_producto_FK");

            entity.HasIndex(e => e.TicketId, "ticket_item_ticket_FK");

            entity.HasIndex(e => e.VarianteId, "ticket_item_variante_FK");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.Cantidad)
                .HasDefaultValueSql("'1'")
                .HasColumnType("int(11)")
                .HasColumnName("cantidad");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("createdAt");
            entity.Property(e => e.Estado)
                .HasColumnType("enum('en preparación','listo')")
                .HasColumnName("estado");
            entity.Property(e => e.Notas)
                .HasColumnType("text")
                .HasColumnName("notas");
            entity.Property(e => e.PrecioUnitario)
                .HasPrecision(10)
                .HasColumnName("precioUnitario");
            entity.Property(e => e.ProductoId)
                .HasColumnType("int(11)")
                .HasColumnName("productoId");
            entity.Property(e => e.Subtotal)
                .HasPrecision(10)
                .HasColumnName("subtotal");
            entity.Property(e => e.TicketId)
                .HasColumnType("int(11)")
                .HasColumnName("ticketId");
            entity.Property(e => e.VarianteId)
                .HasColumnType("int(11)")
                .HasColumnName("varianteId");

            entity.HasOne(d => d.Producto).WithMany(p => p.TicketItem)
                .HasForeignKey(d => d.ProductoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ticket_item_producto_FK");

            entity.HasOne(d => d.Ticket).WithMany(p => p.TicketItem)
                .HasForeignKey(d => d.TicketId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ticket_item_ticket_FK");

            entity.HasOne(d => d.Variante).WithMany(p => p.TicketItem)
                .HasForeignKey(d => d.VarianteId)
                .HasConstraintName("ticket_item_variante_FK");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("usuario");

            entity.HasIndex(e => e.Username, "username").IsUnique();

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.Activo)
                .HasDefaultValueSql("'1'")
                .HasColumnName("activo");
            entity.Property(e => e.NombreCompleto)
                .HasMaxLength(100)
                .HasColumnName("nombreCompleto");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .HasColumnName("password");
            entity.Property(e => e.Rol)
                .HasColumnType("enum('admin','mesero','cocinero')")
                .HasColumnName("rol");
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .HasColumnName("username");
        });

        modelBuilder.Entity<Variante>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("variante");

            entity.HasIndex(e => e.ProductoId, "variante_producto_FK");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.Activo)
                .HasDefaultValueSql("'1'")
                .HasColumnName("activo");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .HasColumnName("nombre");
            entity.Property(e => e.PrecioAdicional)
                .HasPrecision(8, 2)
                .HasDefaultValueSql("'0.00'")
                .HasColumnName("precioAdicional");
            entity.Property(e => e.ProductoId)
                .HasColumnType("int(11)")
                .HasColumnName("productoId");

            entity.HasOne(d => d.Producto).WithMany(p => p.Variante)
                .HasForeignKey(d => d.ProductoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("variante_producto_FK");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
